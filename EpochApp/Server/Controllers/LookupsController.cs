using EpochApp.Server.Data;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     A controller for all lookup tables.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public LookupsController(EpochDataDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     A get request for a list of all social media lookups.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="IEnumerable{T}" /> where T is <see cref="SocialMedia" />.
        /// </returns>
        [HttpGet("lkSocials")]
        public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMedias()
        {
            return await _context.SocialMedias.ToListAsync();
        }

        /// <summary>
        ///     A get request for a specific social media lookup.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="SocialMedia" />.
        /// </returns>
        [HttpGet("lkSocials/{id:int}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
        {
            var socialMedia = await _context.SocialMedias.FindAsync(id);

            if (socialMedia == null)
            {
                return NotFound();
            }

            return socialMedia;
        }
    }

}