// EpochWorlds
// ArticleService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;

namespace EpochApp.Server.Services
{
    public class ArticleService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(EpochDataDbContext context, ILogger<ArticleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ArticleDTO> CreateArticleAsync(ArticleDTO article)
        {
            var newArticle = new Article
                             {
                                 Title = article.Title,
                                 Content = article.Content,
                                 IsPublished = article.IsPublished,
                                 IsNSFW = article.IsNSFW,
                                 DisplayAuthor = article.DisplayAuthor,
                                 ShowInTableOfContents = article.ShowInTableOfContents,
                                 ShowTableOfContents = article.ShowTableOfContents,
                                 CreatedOn = DateTime.Now,
                                 ModifiedOn = null,
                                 DeletedOn = null,
                                 ArticleTags = null,
                                 AuthorId = article.AuthorId,
                                 WorldId = article.WorldId,
                                 CategoryId = article.CategoryId

                             };
            foreach (var section in article.Sections)
            {
                newArticle.Sections.Add(new ArticleSection
                                        {
                                            Title = section.Title,
                                            Content = section.Content,
                                            CreatedOn = DateTime.Now,
                                            ArticleId = newArticle.ArticleId
                                        });
            }
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            return new ArticleDTO
                   {
                       ArticleId = newArticle.ArticleId,
                       Title = newArticle.Title,
                       Content = newArticle.Content,
                       CreatedOn = newArticle.CreatedOn,
                       AuthorId = newArticle.AuthorId,
                       WorldId = newArticle.WorldId
                   };
        }
    }
}