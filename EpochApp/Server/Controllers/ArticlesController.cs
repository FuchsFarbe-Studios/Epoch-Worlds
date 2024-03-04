// EpochWorlds
// ArticleController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        ///   Constructor for the <see cref="ArticlesController" />.
        /// </summary>
        /// <param name="articleService"> The <see cref="IArticleService" />. </param>
        /// <param name="lookupService"> The <see cref="ILookupService" />. </param>
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
        [Authorize("ADMIN,INTERNAL")]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> UpdateArticleAsync([FromQuery] Guid userId, [FromQuery] Guid articleId, ArticleEditDTO article)
        {
            var updatedArticle = await _articleService.UpdateArticleAsync(article, articleId, userId);
            if (updatedArticle == null)
                return NotFound();

            return Ok(updatedArticle);
        }

        /// <summary>
        ///   Deletes an article.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <param name="articleId"> The article's unique identifier. </param>
        /// <returns> A <see cref="IActionResult"/>. </returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteArticleAsync([FromQuery] Guid userId, [FromQuery] Guid articleId)
        {
            await _articleService.DeleteArticleAsync(userId, articleId);
            return Ok();
        }

        /// <summary>
        ///    Get all article templates.
        /// </summary>
        /// <returns> A <see cref="IEnumerable{T}" /> of <see cref="ArticleTemplateDTO"/>. </returns>
        [HttpGet("Templates")]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<ActionResult<IEnumerable<ArticleTemplateDTO>>> GetArticleTemplatesAsync()
        {
            var template = await _articleService.GetArticleTemplatesAsync();
            return Ok(template);
        }

        /// <summary>
        ///    Get the article template for a category.
        /// </summary>
        /// <param name="categoryId"> The category's unique identifier. </param>
        /// <returns> A <see cref="ArticleTemplate"/>. </returns>
        [HttpGet("Template/{categoryId:int}")]
        public async Task<IActionResult> GetArticleTemplateAsync(int categoryId)
        {
            var template = await _articleService.GetArticleTemplateAsync(categoryId);
            return Ok(template);
        }

        /// <summary>
        ///   Create a new article template.
        /// </summary>
        /// <param name="template"> The <see cref="ArticleTemplateDTO"/>. </param>
        /// <returns> A <see cref="ArticleTemplate"/>. </returns>
        [HttpPost("Template")]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<IActionResult> PostArticleTemplateAsync([FromBody] ArticleTemplateDTO template)
        {
            var newTemplate = await _articleService.CreateArticleTemplateAsync(template);
            return CreatedAtAction("GetArticleTemplate", new { categoryId = newTemplate.CategoryId }, newTemplate);
        }

        /// <summary>
        ///  Update an article template.
        /// </summary>
        /// <param name="template"> The <see cref="ArticleTemplateDTO"/>. </param>
        /// <returns> A <see cref="ArticleTemplate"/>. </returns>
        [HttpPut("Template")]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<IActionResult> PutArticleTemplateAsync([FromBody] ArticleTemplateDTO template)
        {
            var updatedTemplate = await _articleService.UpdateArticleTemplateAsync(template);
            if (updatedTemplate == null)
                return NotFound();

            return Ok(updatedTemplate);
        }

        /// <summary>
        ///  Delete an article template.
        /// </summary>
        /// <param name="templateId"> The template's unique identifier. </param>
        /// <returns> A <see cref="IActionResult"/>. </returns>
        [HttpDelete("Template")]
        [Authorize(Roles = "ADMIN,INTERNAL")]
        public async Task<IActionResult> DeleteArticleTemplateAsync([FromQuery] int templateId)
        {
            var deleted = await _articleService.DeleteArticleTemplateAsync(templateId);
            if (!deleted)
                return NotFound();

            return Ok();
        }

        ///// <summary> Deletes an article. </summary>
        // [HttpDelete]
        // public async Task<IActionResult> DeleteArticleAsync([FromQuery] Guid userId, [FromQuery] Guid articleId)
        // {
        //     var deleted = await _articleService.DeleteArticleAsync(articleId, userId);
        //     if (!deleted)
        //         return NotFound();
        //
        //     return Ok();
        // }
    }
}