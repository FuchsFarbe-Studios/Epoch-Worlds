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
            return await _context.Profiles.Select(x => new ProfileDTO
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
                                                       })
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
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(Guid id)
        {
            return _context.Profiles.Any(e => e.UserID == id);
        }
    }
}