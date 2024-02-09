// EpochWorlds
// BuilderController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 9-2-2024
using EpochApp.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     A controller for handling user builder requests.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuilderController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///     Constructor for the <see cref="BuilderController" />.
        /// </summary>
        /// <param name="context">
        ///     The injected <see cref="EpochDataDbContext" /> to use for the controller.
        /// </param>
        public BuilderController(EpochDataDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser()
        {
            return Ok();
        }
    }
}