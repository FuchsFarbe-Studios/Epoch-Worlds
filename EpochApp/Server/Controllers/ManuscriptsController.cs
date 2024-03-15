// EpochWorlds
// ManuscriptsController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 15-3-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///   Controller for managing manuscripts.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ManuscriptsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private readonly IManuscriptService _manuscriptService;

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="manuscriptService"> The manuscript service. </param>
        /// <param name="context"> The database context. </param>
        public ManuscriptsController(IManuscriptService manuscriptService, EpochDataDbContext context)
        {
            _manuscriptService = manuscriptService;
            _context = context;
        }

        /// <summary>
        ///   Get all manuscripts for a user.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <returns> <see cref="ActionResult" /> of <see cref="IEnumerable{T}" /> of <see cref="ManuscriptDTO" />. </returns>
        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<IEnumerable<ManuscriptDTO>>> GetUserManuscriptsAsync(Guid userId)
        {
            var manuscripts = await _manuscriptService.GetUserManuscripts(userId);
            return Ok(manuscripts);
        }

        /// <summary>
        ///  Get a manuscript by its unique identifier.
        /// </summary>
        /// <param name="manuscriptId"> The manuscript's unique identifier. </param>
        /// <returns> <see cref="ActionResult" /> of <see cref="ManuscriptDTO" />. </returns>
        [HttpGet("Manuscript/{manuscriptId:long}")]
        public async Task<ActionResult<ManuscriptDTO>> GetManuscriptAsync(long manuscriptId)
        {
            var manuscript = await _manuscriptService.GetManuscriptAsync(manuscriptId);
            return Ok(manuscript);
        }

        /// <summary>
        /// Create a new manuscript.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <param name="manuscript"> The manuscript to create. </param>
        /// <returns> <see cref="ActionResult" /> of <see cref="ManuscriptDTO" />. </returns>
        [HttpPost("{userId:guid}/{manuscriptId:long}")]
        public async Task<IActionResult> CreateManuscriptAsync(Guid userId, ManuscriptDTO manuscript)
        {
            var newManuscript = await _manuscriptService.CreateManuscriptAsync(manuscript);
            return CreatedAtAction("GetManuscript", new { manuscriptId = newManuscript.ManuscriptId }, newManuscript);
        }
    }
}