// EpochWorlds
// SlugsController.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
using EpochApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    /// Gets appropriate data from slugs.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SlugsController : ControllerBase
    {
        private readonly ILogger<SlugsController> _logger;
        private readonly ISlugService _slugService;

        /// <summary>
        /// Constructor for SlugsController.
        /// </summary>
        /// <param name="slugService"> The slug service. </param>
        /// <param name="logger"> The logger. </param>
        public SlugsController(ISlugService slugService, ILogger<SlugsController> logger)
        {
            _slugService = slugService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a world by its slug.
        /// </summary>
        /// <param name="slug"> The slug of the world. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="ActionResult{TValue}"/> of <see cref="WorldDTO"/>. </returns>
        [HttpGet("World/{slug}")]
        public async Task<ActionResult<WorldDTO>> GetWorldBySlugAsync(string slug)
        {
            var world = await _slugService.GetWorldBySlugAsync(slug);
            if (world == null)
            {
                return NotFound();
            }
            return Ok(world);
        }

        /// <summary>
        /// Gets an article by its slug.
        /// </summary>
        /// <param name="slug"> The slug of the article. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="ActionResult{TValue}"/> of <see cref="ArticleDTO"/>. </returns>
        [HttpGet("Article/{slug}")]
        public async Task<ActionResult<ArticleDTO>> GetArticleBySlugAsync(string slug)
        {
            var article = await _slugService.GetArticleBySlugAsync(slug);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }
    }
}