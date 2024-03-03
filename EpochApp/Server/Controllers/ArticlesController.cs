// EpochWorlds
// ArticleController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ILookupService _lookupService;

        public ArticlesController(IArticleService articleService, ILookupService lookupService)
        {
            _articleService = articleService;
            _lookupService = lookupService;
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
            var categories = await _lookupService.GetArticleCategoriesAsync();
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

        /// <summary>
        ///    Get an article to edit by its unique identifier.
        /// </summary>
        /// <param name="articleId"> The unique identifier for the article. </param>
        /// <returns> A <see cref="ArticleEditDTO"/>. </returns>
        [HttpGet("Article/Edited/{articleId:guid}")]
        public async Task<ActionResult<ArticleEditDTO>> GetEditArticleAsync(Guid articleId)
        {
            var article = await _articleService.GetEditArticleAsync(articleId);
            if (article == null)
                return NotFound();

            return Ok(article);
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
            var newArt = await _articleService.CreateArticleAsync(article);
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
            var updatedArticle = await _articleService.UpdateArticleAsync(article, articleId, userId);
            if (updatedArticle == null)
                return NotFound();

            return Ok(updatedArticle);
        }
    }
}