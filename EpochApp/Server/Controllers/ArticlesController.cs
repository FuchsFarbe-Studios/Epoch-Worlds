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

namespace EpochApp.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArticlesController : ControllerBase
    {
        public readonly EpochDataDbContext _context;

        public ArticlesController(EpochDataDbContext context)
        {
            _context = context;
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
            var articles = await _context.Articles
                                         .Include(a => a.Sections)
                                         .Include(a => a.Category)
                                         .Include(a => a.World)
                                         .Include(a => a.Author)
                                         .Select(x => new ArticleDTO
                                                      {
                                                          ArticleId = x.ArticleId,
                                                          Title = x.Title,
                                                          Content = x.Content,
                                                          CreatedOn = x.CreatedOn,
                                                          ModifiedOn = x.ModifiedOn,
                                                          Sections = x.Sections.Select(s => new SectionDTO
                                                                                            {
                                                                                                SectionID = s.SectionID,
                                                                                                Title = s.Title,
                                                                                                Content = s.Content,
                                                                                                CreatedOn = s.CreatedOn
                                                                                            })
                                                      })
                                         .ToListAsync();

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
            var article = await _context.Articles
                                        .Include(a => a.Sections)
                                        .Include(a => a.Category)
                                        .Include(a => a.World)
                                        .Include(a => a.Author)
                                        .Select(x => new ArticleDTO
                                                     {
                                                         ArticleId = x.ArticleId,
                                                         Title = x.Title,
                                                         CategoryId = x.CategoryId,
                                                         Content = x.Content,
                                                         CreatedOn = x.CreatedOn,
                                                         ModifiedOn = x.ModifiedOn,
                                                         Sections = x.Sections.Select(s => new SectionDTO
                                                                                           {
                                                                                               SectionID = s.SectionID,
                                                                                               Title = s.Title,
                                                                                               Content = s.Content,
                                                                                               CreatedOn = s.CreatedOn
                                                                                           })
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
            // Get articles by user id
            var articles = await _context.Articles
                                         .Include(a => a.Sections)
                                         .Include(a => a.Category)
                                         .Include(a => a.World)
                                         .Include(a => a.Author)
                                         .Select(x => new ArticleDTO
                                                      {
                                                          ArticleId = x.ArticleId,
                                                          AuthorId = x.AuthorId,
                                                          Title = x.Title,
                                                          Content = x.Content,
                                                          CreatedOn = x.CreatedOn,
                                                          ModifiedOn = x.ModifiedOn,
                                                          Sections = x.Sections.Select(s => new SectionDTO
                                                                                            {
                                                                                                SectionID = s.SectionID,
                                                                                                Title = s.Title,
                                                                                                Content = s.Content,
                                                                                                CreatedOn = s.CreatedOn
                                                                                            })
                                                      })
                                         .Where(x => x.AuthorId == userId)
                                         .ToListAsync();
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
            // Get articles by world id
            var articles = await _context.Articles
                                         .Include(a => a.Sections)
                                         .Include(a => a.Category)
                                         .Include(a => a.World)
                                         .Include(a => a.Author)
                                         .Select(x => new ArticleDTO
                                                      {
                                                          ArticleId = x.ArticleId,
                                                          WorldId = x.WorldId,
                                                          AuthorId = x.AuthorId,
                                                          Title = x.Title,
                                                          Content = x.Content,
                                                          CreatedOn = x.CreatedOn,
                                                          ModifiedOn = x.ModifiedOn,
                                                          Sections = x.Sections.Select(s => new SectionDTO
                                                                                            {
                                                                                                SectionID = s.SectionID,
                                                                                                Title = s.Title,
                                                                                                Content = s.Content,
                                                                                                CreatedOn = s.CreatedOn
                                                                                            })
                                                      })
                                         .Where(x => x.WorldId == worldId)
                                         .ToListAsync();
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
                          CreatedOn = DateTime.Now,
                          AuthorId = article.AuthorId,
                          WorldId = article.WorldId,
                          Sections = article.Sections.Select(x => new ArticleSection
                                                                  {
                                                                      Title = x.Title,
                                                                      Content = x.Content,
                                                                      CreatedOn = DateTime.Now
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
            if (articleToUpdate == null)
                return NotFound();
            if (articleToUpdate.AuthorId != userId)
                return Unauthorized();

            articleToUpdate.Title = article.Title;
            articleToUpdate.Content = article.Content;
            articleToUpdate.ModifiedOn = DateTime.Now;
            articleToUpdate.Sections.Clear();
            articleToUpdate.Sections = article.Sections.Select(x => new ArticleSection
                                                                    {
                                                                        Title = x.Title,
                                                                        Content = x.Content,
                                                                        CreatedOn = DateTime.Now
                                                                    })
                                              .ToList();

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