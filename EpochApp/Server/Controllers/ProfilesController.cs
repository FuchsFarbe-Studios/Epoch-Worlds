using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///    The controller for the user profiles.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private IProfileSerivce _profileService;

        /// <summary>
        ///   Constructor for the ProfilesController.
        /// </summary>
        /// <param name="context"> The EpochDataDbContext. </param>
        /// <param name="profileService"> The IProfileSerivce. </param>
        public ProfilesController(EpochDataDbContext context, IProfileSerivce profileService)
        {
            _context = context;
            _profileService = profileService;
        }

        /// <summary>
        /// Get All profiles.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _context.Profiles.ToListAsync();
        }

        /// <summary>
        /// Get a specified profile.
        /// </summary>
        [HttpGet("Profile/{id:guid}")]
        [Authorize]
        public async Task<ActionResult<ProfileDTO>> GetProfileByIdAsync(Guid id)
        {
            var profile = await _profileService.GetProfileByUserIdAsync(id);
            return profile;
        }

        /// <summary>
        /// Get a specified profile by name.
        /// </summary>
        /// <param name="userName"> The name of the profile. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="ProfileDTO"/>. </returns>
        [HttpGet("Profile")]
        [Authorize]
        public async Task<ActionResult<ProfileDTO>> GetProfileByNameAsync([FromQuery] string userName)
        {
            var profile = await _profileService.GetProfileByUsername(userName);
            return profile;
        }

        /// <summary>
        /// Update a specified profile.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProfile(Guid id, ProfileDTO profile)
        {
            if (id != profile.UserId)
                return BadRequest();

            var updatedProfile = await _profileService.UpdateProfile(id, profile);
            return Ok(updatedProfile);
        }
    }
}