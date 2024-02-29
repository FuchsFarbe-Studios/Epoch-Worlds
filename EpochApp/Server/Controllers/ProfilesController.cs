using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     Profiles Controller for user profiles.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///     Constructor for ProfilesController
        /// </summary>
        /// <param name="context"> EpochDataDbContext </param>
        public ProfilesController(EpochDataDbContext context)
        {
            _context = context;
        }

        /// <summary> Get all profiles. </summary>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="IEnumerable{T}" /> where T is <see cref="ProfileDTO" />.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetProfiles()
        {
            return await _context.Profiles.Select(x => MapToProfileDTO(x))
                                 .ToListAsync();
        }

        /// <summary> Get a profile by id. </summary>
        /// <param name="id"> Guid </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="ProfileDTO" />.
        /// </returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProfileDTO>> GetProfile(Guid id)
        {
            var profile = await _context.Profiles
                                        .Include(x => x.Socials)
                                        .ThenInclude(x => x.Social)
                                        .Select(x => new ProfileDTO
                                                     {
                                                         UserID = x.UserID,
                                                         UserName = x.User.UserName ?? "",
                                                         CreateDate = x.User.DateCreated,
                                                         FirstName = x.FirstName,
                                                         LastName = x.LastName,
                                                         Bio = x.Bio,
                                                         Signature = x.Signature,
                                                         AvatarImg = x.AvatarImg,
                                                         CoverImg = x.CoverImg,
                                                         WebAddress = x.WebAddress,
                                                         Socials = x.Socials.Select(s => new SocialDTO
                                                                                         {
                                                                                             Social = s.Social,
                                                                                             Handle = s.SocialHandle
                                                                                         })
                                                                    .ToList()
                                                     })
                                        .FirstOrDefaultAsync(x => x.UserID == id);

            if (profile == null)
                return NotFound();

            return profile;
        }

        [HttpGet("PublicProfile")]
        public async Task<ActionResult<UserProfileDTO>> GetPublicProfileAsync([FromQuery] Guid userId)
        {
            var userProfile = await _context.Users
                                            .Include(x => x.Profile)
                                            .Include(x => x.OwnedArticles)
                                            .ThenInclude(a => a.Sections)
                                            .Include(x => x.OwnedWorlds)
                                            .ThenInclude(w => w.CurrentWorldDate)
                                            .Select(x => MapToUserProfileDTO(x))
                                            .FirstOrDefaultAsync(x => x.UserId == userId);
            return Ok(userProfile);
        }

        /// <summary>
        ///     Update a profile by id.
        /// </summary>
        /// <param name="id"> Guid </param>
        /// <param name="profileData">
        ///     <see cref="ProfileDTO" />
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="IActionResult" />.
        /// </returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutProfile(Guid id, ProfileDTO profileData)
        {
            if (id != profileData.UserID)
                return BadRequest();

            var profile = await _context.Profiles
                                        .Include(u => u.Socials)
                                        .ThenInclude(userSocial => userSocial.Social)
                                        .FirstOrDefaultAsync(x => x.UserID == id);
            // Map profile data to profile
            profile.FirstName = profileData.FirstName;
            profile.LastName = profileData.LastName;
            profile.Bio = profileData.Bio;
            profile.Signature = profileData.Signature;
            profile.AvatarImg = profileData.AvatarImg;
            profile.CoverImg = profileData.CoverImg;
            profile.WebAddress = profileData.WebAddress;
            // Clear existing socials
            profile.Socials.Clear();

            // Add or update socials
            foreach (var socialData in profileData.Socials)
            {
                var social = _context.UserSocials.FirstOrDefault(x => x.Social == socialData.Social && x.UserID == profile.UserID);

                if (social != null)
                {
                    profile.Socials.Add(new UserSocial
                                        {
                                            // ReSharper disable once EntityFramework.NPlusOne.IncompleteDataUsage
                                            Social = social.Social,
                                            SocialHandle = socialData.Handle
                                        });
                }
                // If social is not found, you may want to handle this case
            }

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        /// <summary> Create a new profile. </summary>
        /// <param name="profileData">
        ///     <see cref="ProfileDTO" />
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is <see cref="Profile" />.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(ProfileDTO profileData)
        {
            var profile = new Profile
                          {
                              UserID = profileData.UserID,
                              FirstName = profileData.FirstName,
                              LastName = profileData.LastName,
                              Bio = profileData.Bio,
                              Signature = profileData.Signature,
                              AvatarImg = profileData.AvatarImg,
                              CoverImg = profileData.CoverImg,
                              WebAddress = profileData.WebAddress,
                              Socials = profileData.Socials.Select(x => new UserSocial
                                                                        {
                                                                            Social = x.Social,
                                                                            SocialHandle = x.Handle
                                                                        })
                                                   .ToList()

                          };
            _context.Profiles.Add(profile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfileExists(profile.UserID))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtAction("GetProfile", new { id = profile.UserID }, profile);
        }

        /// <summary>
        ///     Delete a profile by id.
        /// </summary>
        /// <param name="id"> Guid </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="IActionResult" />.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
                return NotFound();

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(Guid id)
        {
            return _context.Profiles.Any(e => e.UserID == id);
        }

        private static ProfileDTO MapToProfileDTO(Profile x)
        {
            return new ProfileDTO
                   {
                       UserID = x.UserID,
                       FirstName = x.FirstName,
                       LastName = x.LastName,
                       Bio = x.Bio,
                       Signature = x.Signature,
                       AvatarImg = x.AvatarImg,
                       CoverImg = x.CoverImg,
                       WebAddress = x.WebAddress,
                       Socials = x.Socials.Select(s => new SocialDTO
                                                       {
                                                           Social = s.Social,
                                                           Handle = s.SocialHandle
                                                       })
                                  .ToList()
                   };
        }

        private static UserProfileDTO MapToUserProfileDTO(User x)
        {
            return new UserProfileDTO
                   {
                       UserId = x.UserID,
                       UserName = x.UserName,
                       ProfileImage = x.Profile.AvatarImg,
                       CoverImage = x.Profile.CoverImg,
                       Bio = x.Profile.Bio,
                       Website = x.Profile.WebAddress,
                       WorldCount = x.OwnedWorlds.Count,
                       ArticleCount = x.OwnedArticles.Count,
                       MemberDate = x.DateCreated,
                       UserWorlds = x.OwnedWorlds.Select(w => new WorldDTO
                                                              {
                                                                  AuthorID = x.UserID,
                                                                  WorldID = w.WorldId,
                                                                  WorldName = w.WorldName,
                                                                  Pronunciation = w.Pronunciation,
                                                                  Description = w.Description,
                                                                  DateCreated = w.DateCreated,
                                                                  DateModified = w.DateModified,
                                                                  DateRemoved = w.DateRemoved,
                                                                  MetaData = null,
                                                                  IsActiveWorld = w.IsActiveWorld,
                                                                  WorldDate = new WorldDateDTO
                                                                              {
                                                                                  WorldId = w.WorldId,
                                                                                  CurrentDay = w.CurrentWorldDate.CurrentDay,
                                                                                  CurrentMonth = w.CurrentWorldDate.CurrentMonth,
                                                                                  CurrentYear = w.CurrentWorldDate.CurrentYear,
                                                                                  CurrentAge = w.CurrentWorldDate.CurrentAge,
                                                                                  BeforeEra = w.CurrentWorldDate.BeforeEraName,
                                                                                  AfterEra = w.CurrentWorldDate.AfterEraName,
                                                                                  BeforeEraAbbreviation = w.CurrentWorldDate.BeforeEraAbbreviation,
                                                                                  AfterEraAbbreviation = w.CurrentWorldDate.AfterEraAbbreviation,
                                                                                  CurrentEra = w.CurrentWorldDate.CurrentAge
                                                                              }
                                                              })
                                     .ToList(),
                       UserArticles = x.OwnedArticles.Select(a => new ArticleDTO
                                                                  {
                                                                      ArticleId = a.ArticleId,
                                                                      AuthorId = x.UserID,
                                                                      WorldId = a.WorldId,
                                                                      CategoryId = a.CategoryId,
                                                                      Title = a.Title,
                                                                      Content = a.Content,
                                                                      IsPublished = a.IsPublished,
                                                                      IsNSFW = a.IsNSFW,
                                                                      DisplayAuthor = a.DisplayAuthor,
                                                                      ShowInTableOfContents = a.ShowInTableOfContents,
                                                                      ShowTableOfContents = a.ShowTableOfContents,
                                                                      CreatedOn = a.CreatedOn,
                                                                      ModifiedOn = a.ModifiedOn,
                                                                      Sections = a.Sections.Select(sec => new SectionDTO
                                                                                                          {
                                                                                                              SectionID = sec.SectionID,
                                                                                                              Title = sec.Title,
                                                                                                              Content = sec.Content,
                                                                                                              CreatedOn = sec.CreatedOn
                                                                                                          })
                                                                                  .ToList()
                                                                  })
                                       .ToList(),
                       Socials = x.Profile.Socials.Select(s => new UserSocialDTO
                                                               {
                                                                   Icon = s.Social.Icon,
                                                                   SocialName = s.Social.SocialMediaName,
                                                                   Handle = s.SocialHandle
                                                               })
                                  .ToList()
                   };
        }
    }
}