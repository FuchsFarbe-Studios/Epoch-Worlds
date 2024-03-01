// EpochWorlds
// ArticleController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public ArticlesController(EpochDataDbContext context, IArticleService articleService)
        {
            _context = context;
            _articleService = articleService;
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
                                        .Select(x => new ArticleEditDTO
                                                     {
                                                         ArticleId = x.ArticleId,
                                                         Title = x.Title,
                                                         CategoryId = x.CategoryId ?? 0,
                                                         Content = x.Content,
                                                         CreatedOn = x.CreatedOn,
                                                         ModifiedOn = x.ModifiedOn,
                                                         IsPublished = x.IsPublished,
                                                         IsNSFW = x.IsNSFW,
                                                         DisplayAuthor = x.DisplayAuthor,
                                                         ShowTableOfContents = x.ShowInTableOfContents,
                                                         ShowInTableOfContents = x.ShowInTableOfContents,
                                                         Sections = x.Sections.Select(s => new SectionEditDTO
                                                                                           {
                                                                                               SectionId = s.SectionID,
                                                                                               Title = s.Title,
                                                                                               Content = s.Content,
                                                                                               CreatedOn = s.CreatedOn
                                                                                           })
                                                                     .ToList()
                                                     })
                                        .FirstOrDefaultAsync(x => x.ArticleId == articleId);

            if (article == null)
            {
                return NotFound();
            }

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
            var art = new Article
                      {
                          Title = article.Title,
                          Content = article.Content,
                          CreatedOn = DateTime.UtcNow,
                          AuthorId = article.AuthorId,
                          WorldId = article.WorldId,
                          IsPublished = article.IsPublished,
                          IsNSFW = article.IsNSFW,
                          DisplayAuthor = article.DisplayAuthor,
                          ShowTableOfContents = article.ShowInTableOfContents,
                          ShowInTableOfContents = article.ShowInTableOfContents,
                          Sections = article.Sections.Select(x => new ArticleSection
                                                                  {
                                                                      Title = x.Title,
                                                                      Content = x.Content,
                                                                      CreatedOn = DateTime.UtcNow
                                                                  })
                                            .ToList()

                      };
            _context.Articles.Add(art);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetArticle", new { articleId = art.ArticleId }, art);
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
            var articleToUpdate = await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == articleId);
            var articleSections = await _context.ArticleSections
                                                .Where(x => x.ArticleId == articleId)
                                                .ToListAsync();
            if (articleToUpdate == null)
                return NotFound();
            if (articleToUpdate.AuthorId != userId)
                return Unauthorized();

            // Update or delete sections
            if (article.Sections.IsNullOrEmpty())
                if (articleSections.Any())
                    foreach (var section in articleSections)
                        _context.ArticleSections.Remove(section);

            // Remove sections not found in the updated article
            foreach (var section in articleSections)
            {
                var sectionToUpdate = article.Sections.FirstOrDefault(x => x.SectionId == section.SectionID);
                if (sectionToUpdate == null)
                    _context.ArticleSections.Remove(section);
            }

            foreach (var section in article.Sections)
            {
                var sectionToUpdate = articleSections.FirstOrDefault(x => x.SectionID == section.SectionId);
                if (sectionToUpdate == null)
                {
                    articleSections.Add(new ArticleSection
                                        {
                                            Title = section.Title,
                                            Content = section.Content,
                                            CreatedOn = DateTime.Now
                                        });
                }
                else
                {
                    sectionToUpdate.Title = section.Title;
                    sectionToUpdate.Content = section.Content;
                    sectionToUpdate.CreatedOn = DateTime.Now;
                    _context.Entry(sectionToUpdate).State = EntityState.Modified;
                }
            }

            articleToUpdate.Title = article.Title;
            articleToUpdate.Content = article.Content;
            articleToUpdate.ModifiedOn = DateTime.Now;
            articleToUpdate.IsPublished = article.IsPublished;
            articleToUpdate.IsNSFW = article.IsNSFW;
            articleToUpdate.DisplayAuthor = article.DisplayAuthor;
            articleToUpdate.ShowTableOfContents = article.ShowInTableOfContents;
            articleToUpdate.ShowInTableOfContents = article.ShowInTableOfContents;
            articleToUpdate.Sections = articleSections;
            _context.Entry(articleToUpdate).State = EntityState.Modified;
            try
            {
                _context.Articles.Update(articleToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(articleToUpdate);
        }
    }
}