// EpochWorlds
// ArticleController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     Controller for managing articles and manuscripts.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ArticlesController> _logger;
        private readonly IMapper _mapper;

        public ArticlesController(EpochDataDbContext context, IArticleService articleService, IMapper mapper, ILogger<ArticlesController> logger)
        {
            _context = context;
            _articleService = articleService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     Gets all articles from the database.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="IEnumerable{T}" /> where T is <see cref="ArticleDTO" />.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> IndexArticlesAsync()
        {
            var articles = await _articleService.GetArticlesAsync();
            return Ok(articles);
        }

        /// <summary>
        ///     Gets all article categories from the database.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="IEnumerable{T}" /> where T is <see cref="ArticleCategory" />.
        /// </returns>
        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<ArticleCategory>>> IndexCategoriesAsync()
        {
            var categories = await _context.ArticleCategories
                                           .ToListAsync();
            return Ok(categories);
        }

        /// <summary>
        ///     Get an article by its unique identifier.
        /// </summary>
        /// <param name="articleId">
        ///     Unique identifier for the article.
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="ArticleDTO" />.
        /// </returns>
        [HttpGet("Article/{articleId:guid}")]
        public async Task<ActionResult<ArticleDTO>> GetArticleAsync(Guid articleId)
        {
            var article = await _articleService.GetArticleByIdAsync(articleId);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        /// <summary>
        ///     Get an article by its unique identifier.
        /// </summary>
        /// <param name="worldId">
        ///     Unique identifier for the world.
        /// </param>
        /// <param name="articleId">
        ///     Unique identifier for the article.
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="ArticleDTO" />.
        /// </returns>
        [HttpGet("Article/{worldId:guid}/{articleId:guid}")]
        public async Task<ActionResult<ArticleDTO>> GetWorldArticleAsync(Guid worldId, Guid articleId)
        {
            var article = await _articleService.GetWorldArticleAsync(worldId, articleId);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        [HttpGet("Article/Edited/{articleId:guid}")]
        public async Task<ActionResult<ArticleEditDTO>> GetEditArticleAsync(Guid articleId)
        {
            var article = await _context.Articles
                                        .Include(a => a.Sections)
                                        .Include(a => a.Category)
                                        .Include(a => a.World)
                                        .Include(a => a.Author)
                                        .FirstOrDefaultAsync(x => x.ArticleId == articleId);
            if (article == null)
                return NotFound();

            var dto = _mapper.Map<Article, ArticleEditDTO>(article);
            return Ok(dto);
        }

        /// <summary>
        ///     Get articles by user id.
        /// </summary>
        /// <param name="userId">
        ///     Unique identifier for the user.
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="IEnumerable{T}" /> where T is <see cref="ArticleDTO" />.
        /// </returns>
        [HttpGet("UserArticles")]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetUserArticlesAsync([FromQuery] Guid userId)
        {
            var articles = await _articleService.GetUserArticlesAsync(userId);
            return Ok(articles);
        }

        /// <summary>
        ///     Get articles by world id.
        /// </summary>
        /// <param name="worldId">
        ///     Unique identifier for the world.
        /// </param>
        /// <returns>
        ///     <see cref="Task{T}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="IEnumerable{T}" /> where T is <see cref="ArticleDTO" />.
        /// </returns>
        [HttpGet("WorldArticles")]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetWorldArticlesAsync([FromQuery] Guid worldId)
        {
            var articles = await _articleService.GetWorldArticlesAsync(worldId);
            return Ok(articles);
        }

        /// <summary> Creates a new article. </summary>
        /// <param name="article">
        ///     <see cref="ArticleEditDTO" />.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateArticleAsync(ArticleEditDTO article)
        {
            var newArt = _mapper.Map<ArticleEditDTO, Article>(article);
            newArt.Sections = article.Sections.Select(x => _mapper.Map<SectionEditDTO, ArticleSection>(x)).ToList();
            _context.Articles.Add(newArt);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetArticle", new { articleId = newArt.ArticleId }, newArt);
        }

        /// <summary> Updates an article. </summary>
        /// <param name="userId">
        ///     Unique identifier for the user.
        /// </param>
        /// <param name="articleId">
        ///     Unique identifier for the article.
        /// </param>
        /// <param name="article">
        ///     <see cref="ArticleEditDTO" />.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdateArticleAsync([FromQuery] Guid userId, [FromQuery] Guid articleId, ArticleEditDTO article)
        {
            _logger.LogInformation("Updating article...");
            var articleToUpdate = await _context.Articles.Where(x => x.ArticleId == articleId)
                                                .Include(x => x.World)
                                                .Include(x => x.Category)
                                                .Include(x => x.Author)
                                                .Include(x => x.Sections)
                                                .FirstOrDefaultAsync();
            var articleSections = articleToUpdate?.Sections.ToList() ?? new List<ArticleSection>();
            _logger.LogInformation($"Article sections: {articleSections.Count}");
            if (articleToUpdate == null)
                return NotFound();
            if (articleToUpdate.AuthorId != userId)
                return Unauthorized();

            var sections = article.Sections.Select(x => _mapper.Map<SectionEditDTO, ArticleSection>(x)).ToList();
            _mapper.Map(article, articleToUpdate);
            articleToUpdate.Sections = sections;

            _context.Entry(articleToUpdate).State = EntityState.Modified;
            try
            {
                _context.Articles.Update(articleToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error updating article: {e.Message}");
                return BadRequest(e.Message);
            }

            return Ok(articleToUpdate);
        }
    }
}