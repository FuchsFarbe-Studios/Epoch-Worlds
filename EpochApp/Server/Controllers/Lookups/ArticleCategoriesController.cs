using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public ArticleCategoriesController(EpochDataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetArticleCategories()
        {
            return await _context.ArticleCategories.Select(c => new CategoryDTO(){CategoryID = c.CategoryID.ToString(), Description = c.Description}).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetArticleCategory(int id)
        {
            var articleCategory = await _context.ArticleCategories.FindAsync(id);

            if (articleCategory == null)
            {
                return NotFound();
            }

            return new CategoryDTO(){ CategoryID = articleCategory.CategoryID.ToString(), Description = articleCategory.Description };
        }

        private bool ArticleCategoryExists(int id)
        {
            return _context.ArticleCategories.Any(e => e.CategoryID == id);
        }
    }
}