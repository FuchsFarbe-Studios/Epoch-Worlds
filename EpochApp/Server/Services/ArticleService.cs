// EpochWorlds
// ArticleService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
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
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;

        public ArticleService(EpochDataDbContext context, ILogger<ArticleService> logger, ITagService tagService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _tagService = tagService;
            _mapper = mapper;
        }

        public async Task<List<ArticleDTO>> GetArticlesAsync()
        {
            var articles = await _context.Articles
                                         .Include(a => a.Sections)
                                         .Include(a => a.Category)
                                         .Include(a => a.World)
                                         .Include(a => a.Author)
                                         .Select(x => _mapper.Map(x, new ArticleDTO()))
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
                                             .Select(x => _mapper.Map(x, new ArticleDTO()))
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
                                         .Select(a => _mapper.Map(a, new ArticleDTO()))
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
                                         .Select(a => _mapper.Map(a, new ArticleDTO()))
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
            return _mapper.Map(article, new ArticleDTO());
        }

        /// <inheritdoc />
        public async Task<Article> CreateArticleAsync(ArticleEditDTO article)
        {
            _logger.LogInformation("Creating new article...");
            var articleToAdd = _mapper.Map<ArticleEditDTO, Article>(article);
            _context.Articles.Add(articleToAdd);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Article {articleToAdd.Title} created!");
            return await Task.FromResult(articleToAdd);
        }

        public async Task<ArticleEditDTO> GetEditArticleAsync(Guid articleId)
        {
            var article = await _context.Articles
                                        .Include(a => a.Sections)
                                        .Include(a => a.Category)
                                        .Include(a => a.World)
                                        .Include(a => a.Author)
                                        .FirstOrDefaultAsync(a => a.ArticleId == articleId);
            return _mapper.Map(article, new ArticleEditDTO());
        }

        /// <inheritdoc />
        public async Task<ArticleEditDTO> UpdateArticleAsync(ArticleEditDTO article, Guid articleId, Guid userId)
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
                return null;
            if (articleToUpdate.AuthorId != userId)
                return null;

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
                //return BadRequest(e.Message);
            }

            return await Task.FromResult(_mapper.Map<Article, ArticleEditDTO>(articleToUpdate));
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