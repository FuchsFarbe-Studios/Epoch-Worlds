// EpochWorlds
// ArticleService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Social;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    /// <summary>
    ///     Service for managing articles, manuscripts, and their tags.
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ArticleService> _logger;
        private readonly ITagService _tagService;

        public ArticleService(EpochDataDbContext context, ILogger<ArticleService> logger, ITagService tagService)
        {
            _context = context;
            _logger = logger;
            _tagService = tagService;
        }

        public async Task<List<ArticleDTO>> GetArticlesAsync()
        {
            var articles = await _context.Articles
                                         .Include(a => a.Sections)
                                         .Include(a => a.Category)
                                         .Include(a => a.World)
                                         .Include(a => a.Author)
                                         .Select(x => GetArticleDtoFromArticle(x))
                                         .ToListAsync();
            return articles;
        }

        /// <inheritdoc />
        public async Task<List<ArticleDTO>> GetUserArticlesAsync(Guid userId)
        {
            var userArticles = await _context.Articles.Where(x => x.AuthorId == userId)
                                             .Include(a => a.Sections)
                                             .Include(a => a.Category)
                                             .Include(a => a.World)
                                             .Include(a => a.Author)
                                             .Select(x => GetArticleDtoFromArticle(x))
                                             .ToListAsync();
            return await Task.FromResult(userArticles);
        }

        /// <inheritdoc />
        public async Task<ArticleDTO> GetWorldArticleAsync(Guid worldId, Guid articleId)
        {
            var articles = await _context.Articles
                                         .Include(a => a.Sections)
                                         .Include(a => a.Category)
                                         .Where(a => a.WorldId == worldId && a.ArticleId == articleId)
                                         .Select(a => GetArticleDtoFromArticle(a))
                                         .FirstOrDefaultAsync();
            return articles;
        }

        /// <inheritdoc />
        public async Task<List<ArticleDTO>> GetWorldArticlesAsync(Guid worldId)
        {
            var articles = await _context.Articles
                                         .Include(a => a.Sections)
                                         .Include(a => a.Category)
                                         .Where(a => a.WorldId == worldId)
                                         .Select(a => GetArticleDtoFromArticle(a))
                                         .ToListAsync();
            return articles;
        }

        /// <inheritdoc />
        public async Task<ArticleDTO> GetArticleByIdAsync(Guid articleId)
        {
            var article = await _context.Articles
                                        .Include(a => a.Sections)
                                        .Include(a => a.Category)
                                        .Include(a => a.World)
                                        .Include(a => a.Author)
                                        .FirstOrDefaultAsync(a => a.ArticleId == articleId);
            return GetArticleDtoFromArticle(article);
        }

        /// <inheritdoc />
        public async Task<ArticleDTO> CreateArticleAsync(ArticleDTO article)
        {
            _logger.LogInformation("Creating new article...");
            var newArticle = new Article
                             {
                                 Title = article.Title,
                                 Content = article.Content,
                                 IsPublished = article.IsPublished,
                                 IsNSFW = article.IsNSFW,
                                 DisplayAuthor = article.DisplayAuthor,
                                 ShowInTableOfContents = article.ShowInTableOfContents,
                                 ShowTableOfContents = article.ShowTableOfContents,
                                 CreatedOn = DateTime.UtcNow,
                                 ModifiedOn = null,
                                 DeletedOn = null,
                                 AuthorId = article.AuthorId,
                                 WorldId = article.WorldId,
                                 CategoryId = article.CategoryId,
                                 ArticleTags = article.ArticleTags.Select(t => new ArticleTag
                                                                               {
                                                                                   Tag = new Tag
                                                                                         {
                                                                                             Text = t.Text
                                                                                         }
                                                                               })
                                                      .ToList(),
                                 Sections = article.Sections.Select(s => new ArticleSection
                                                                         {
                                                                             Title = s.Title,
                                                                             Content = s.Content,
                                                                             CreatedOn = DateTime.UtcNow
                                                                         })
                                                   .ToList()
                             };

            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Article {newArticle.Title} created!");
            return await Task.FromResult(GetArticleDtoFromArticle(newArticle));
        }

        public static ArticleDTO GetArticleDtoFromArticle(Article article)
        {
            return new ArticleDTO
                   {
                       ArticleId = article.ArticleId,
                       AuthorId = article?.Author?.UserID,
                       Author = article?.Author?.UserName,
                       WorldId = article?.WorldId,
                       CategoryId = article?.CategoryId,
                       Title = article?.Title,
                       Content = article?.Content,
                       IsPublished = article.IsPublished,
                       IsNSFW = article.IsNSFW,
                       DisplayAuthor = article.DisplayAuthor,
                       ShowInTableOfContents = article.ShowInTableOfContents,
                       ShowTableOfContents = article.ShowTableOfContents,
                       CreatedOn = article?.CreatedOn,
                       ModifiedOn = article?.ModifiedOn,
                       Sections = article?.Sections?.Select(s => new SectionDTO
                                                                 {
                                                                     SectionID = s.SectionID,
                                                                     Title = s?.Title,
                                                                     Content = s?.Content,
                                                                     CreatedOn = s?.CreatedOn
                                                                 })
                                         .ToList(),
                       ArticleTags = article.ArticleTags.Select(t => new ArticleTagDTO
                                                                     {
                                                                         ArticleId = t.ArticleId,
                                                                         TagId = t.TagId,
                                                                         Text = t?.Tag?.Text
                                                                     })
                                            .ToList()
                   };
        }
    }
}