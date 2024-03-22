// EpochWorlds
// ISlugService.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///    Interface for the slug service.
    /// </summary>
    public interface ISlugService
    {
        /// <summary>
        ///    Get a world by its slug.
        /// </summary>
        /// <param name="slug"> The slug of the world. </param>
        /// <returns> A <see cref="Task{TResult}" /> of <see cref="WorldDTO" />. </returns>
        Task<WorldDTO> GetWorldBySlugAsync(string slug);

        /// <summary>
        /// Get an article by its slug.
        /// </summary>
        /// <param name="slug"> Article slug. </param>
        /// <returns> A <see cref="Task{TResult}" /> of <see cref="ArticleDTO" />. </returns>
        Task<ArticleDTO> GetArticleBySlugAsync(string slug);
    }
}