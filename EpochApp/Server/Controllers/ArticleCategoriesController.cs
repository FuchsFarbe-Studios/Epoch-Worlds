using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     Controller for the Article Categories lookups.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///     Constructor for the <see cref="ArticleCategoriesController" />
        /// </summary>
        /// <param name="context">
        ///     The injected <see cref="EpochDataDbContext" /> to use for the controller
        /// </param>
        public ArticleCategoriesController(EpochDataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetArticleCategories()
        {
            return await _context.ArticleCategories.Select(c => new CategoryDTO
                                                                { CategoryID = c.CategoryID.ToString(), Description = c.Description })
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetArticleCategory(int id)
        {
            var articleCategory = await _context.ArticleCategories.FindAsync(id);

            if (articleCategory == null)
            {
                return NotFound();
            }

            return new CategoryDTO
                   { CategoryID = articleCategory.CategoryID.ToString(), Description = articleCategory.Description };
        }

        private bool ArticleCategoryExists(int id)
        {
            return _context.ArticleCategories.Any(e => e.CategoryID == id);
        }
    }

}