// EpochWorlds
// ArticleService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Config;
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<ArticleEditDTO> GetEditArticleAsync(Guid articleId)
        {
            var article = await _context.Articles
                                        .Include(a => a.Sections)
                                        .Include(a => a.Category)
                                        .Include(a => a.World)
                                        .Include(a => a.Author)
                                        .FirstOrDefaultAsync(a => a.ArticleId == articleId);
            if (article == null)
                return null;

            var dto = _mapper.Map(article, new ArticleEditDTO());
            return dto;
        }

        public async Task<IEnumerable<ArticleTemplateDTO>> GetArticleTemplatesAsync()
        {
            var templates = await _context.ArticleTemplates
                                          .Include(x => x.Meta)
                                          .Include(x => x.Sections)
                                          .Select(x => _mapper.Map<ArticleTemplate, ArticleTemplateDTO>(x))
                                          .ToListAsync();
            return templates;
        }

        /// <inheritdoc />
        public async Task<ArticleTemplateDTO> GetArticleTemplateAsync(int categoryId)
        {
            var articleTemplate = await _context.ArticleTemplates
                                                .Include(x => x.Meta)
                                                .Include(x => x.Sections)
                                                .FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            return _mapper.Map<ArticleTemplate, ArticleTemplateDTO>(articleTemplate);
        }

        /// <inheritdoc />
        public async Task<ArticleTemplateDTO> CreateArticleTemplateAsync(ArticleTemplateDTO template)
        {
            var newTemplate = _mapper.Map<ArticleTemplateDTO, ArticleTemplate>(template);
            _context.ArticleTemplates.Add(newTemplate);
            await _context.SaveChangesAsync();
            var templateDto = _mapper.Map(newTemplate, new ArticleTemplateDTO());
            return templateDto;
        }

        /// <inheritdoc />
        public async Task<ArticleTemplateDTO> UpdateArticleTemplateAsync(ArticleTemplateDTO template)
        {
            var templateToUpdate = await _context.ArticleTemplates
                                                 .Include(x => x.Meta)
                                                 .Include(x => x.Sections)
                                                 .FirstOrDefaultAsync(x => x.TemplateId == template.TemplateId);
            _mapper.Map(template, templateToUpdate);
            _context.Entry(templateToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return template;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteArticleTemplateAsync(int templateId)
        {
            var template = await _context.ArticleTemplates.FirstOrDefaultAsync(x => x.TemplateId == templateId);
            if (template == null)
                return false;

            _context.ArticleTemplates.Remove(template);
            await _context.SaveChangesAsync();
            return true;
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
    }
}