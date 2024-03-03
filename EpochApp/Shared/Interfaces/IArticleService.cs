// EpochWorlds
// IManuscriptService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Shared.Articles;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Interface for the article service.
    /// </summary>
    public interface IArticleService
    {
        /// <summary> Index all articles. </summary>
        /// <returns>
        ///     A collection of articles.
        /// </returns>
        Task<List<ArticleDTO>> GetArticlesAsync();

        /// <summary>
        ///     Index all articles for a world.
        /// </summary>
        /// <param name="worldId">
        ///     The world's unique identifier.
        /// </param>
        /// <returns>
        ///     A collection of articles.
        /// </returns>
        Task<List<ArticleDTO>> GetWorldArticlesAsync(Guid worldId);

        /// <summary>
        ///     Get a specific world article.
        /// </summary>
        /// <param name="worldId">
        ///     The world's unique identifier.
        /// </param>
        /// <param name="articleId">
        ///     The article's unique identifier.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="ArticleDTO" />.
        /// </returns>
        Task<ArticleDTO> GetWorldArticleAsync(Guid worldId, Guid articleId);

        /// <summary>
        ///     Get all articles for a user.
        /// </summary>
        /// <param name="userId">
        ///     The user's unique identifier.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="List{T}" /> of <see cref="ArticleDTO" />.
        /// </returns>
        Task<List<ArticleDTO>> GetUserArticlesAsync(Guid userId);

        /// <summary>
        ///     Get an article by its unique identifier.
        /// </summary>
        /// <param name="articleId">
        ///     The article's unique identifier.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="ArticleDTO" />.
        /// </returns>
        Task<ArticleDTO> GetArticleByIdAsync(Guid articleId);

        /// <summary> Create a new article. </summary>
        /// <param name="article"> The article to create. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="ArticleDTO" />.
        /// </returns>
        Task<Article> CreateArticleAsync(ArticleEditDTO article);

        /// <summary>
        ///    Update an article.
        /// </summary>
        /// <param name="article"> The article information to update. </param>
        /// <param name="articleId"> The article's unique identifier. </param>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="ArticleEditDTO" />. </returns>
        Task<ArticleEditDTO> UpdateArticleAsync(ArticleEditDTO article, Guid articleId, Guid userId);

        /// <summary>
        ///    Get an article for editing.
        /// </summary>
        /// <param name="articleId"> The article's unique identifier. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="ArticleEditDTO" />. </returns>
        Task<ArticleEditDTO> GetEditArticleAsync(Guid articleId);
    }
}