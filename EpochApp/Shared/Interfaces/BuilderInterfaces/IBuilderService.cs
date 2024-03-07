// EpochWorlds
// IBuilder.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
namespace EpochApp.Shared
{
    /// <summary>
    /// Interface for the Builder services.
    /// </summary>
    public interface IBuilderService
    {
        /// <summary>
        ///    Get the generated content.
        /// </summary>
        /// <param name="contentId"> The builder content id. </param>
        /// <param name="userId"> The current user id. </param>
        /// <returns> A <see cref="Task{TResult}" /> of <see cref="BuilderContentDTO" />. </returns>
        Task<BuilderContentDTO> GenerateContentAsync(Guid contentId, Guid userId);

        /// <summary>
        ///   Create a new builder content.
        /// </summary>
        /// <param name="content"> The content to create. </param>
        /// <returns> A <see cref="Task{TResult}" /> of <see cref="BuilderContentDTO" />. </returns>
        Task<BuilderContentDTO> CreateNewBuilderContentAsync(BuilderContentDTO content);

        /// <summary>
        ///   Update the builder content.
        /// </summary>
        /// <param name="userId"> The current user id. </param>
        /// <param name="contentId"> The builder content id. </param>
        /// <param name="content"> The content to update. </param>
        /// <returns> A <see cref="Task{TResult}" /> of <see cref="BuilderContentDTO" />. </returns>
        Task<BuilderContentDTO> UpdateBuilderAsync(Guid userId, Guid contentId, BuilderContentDTO content);

        /// <summary>
        /// Retrieves a list of BuilderContentDTO objects authored by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A Task of IEnumerable&lt;BuilderContentDTO&gt; representing the list of Builder content authored by the user.</returns>
        Task<IEnumerable<BuilderContentDTO>> GetBuilderContentByAuthorAsync(Guid userId);

        /// <summary>
        /// Retrieves the Builder content by its ID.
        /// </summary>
        /// <param name="contentId">The ID of the Builder content.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result is a <see cref="BuilderContentDTO"/>.</returns>
        Task<BuilderContentDTO> GetBuilderContentByIdAsync(Guid contentId);
    }
}