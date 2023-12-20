using EpochApp.Server.Data;
using EpochApp.Shared.DataTransfer;
using EpochApp.Shared.Site.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public ProfilesController(EpochDataDbContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _context.Profiles.ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDTO>> GetProfile(Guid id)
        {
            var profile = await _context.Profiles.Include(x => x.Socials)
                                        .FirstOrDefaultAsync(x => x.UserID == id);
            if (profile == null)
                return NotFound();

            var profData = new ProfileDTO
                           {
                               UserID = profile.UserID,
                               FirstName = profile.FirstName,
                               LastName = profile.LastName,
                               Bio = profile.Bio,
                               Signature = profile.Signature,
                               AvatarImg = profile.AvatarImg,
                               CoverImg = profile.CoverImg,
                               WebAddress = profile.WebAddress,
                               Socials = profile.Socials.Select(s => new SocialDTO
                                                                     { Social = s.Social, Handle = s.SocialHandle })
                                                .ToList()
                           };

            return Ok(profData);
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(Guid id, ProfileDTO profileData)
        {
            if (id != profileData.UserID)
            {
                return BadRequest();
            }

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserID == id);
            profile.FirstName = profileData.FirstName;
            profile.LastName = profileData.LastName;
            profile.Bio = profileData.Bio;
            profile.Signature = profileData.Signature;
            profile.AvatarImg = profileData.AvatarImg;
            profile.CoverImg = profileData.CoverImg;
            profile.WebAddress = profileData.WebAddress;
            profile.Socials = profileData.Socials.Select(s => new UserSocial
                                                              { Social = s.Social, SocialHandle = s.Handle })
                                         .ToList();
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

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
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

        // DELETE: api/Profiles/5
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

        private Boolean ProfileExists(Guid id)
        {
            return _context.Profiles.Any(e => e.UserID == id);
        }
    }
}