// EpochWorlds
// ITagService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Interface for the tag service.
    /// </summary>
    public interface ITagService
    {
        /// <summary> Index all tags. </summary>
        /// <returns> A collection of tags. </returns>
        Task<List<TagDTO>> GetTagsAsync();

        /// <summary> Get a tag by its text. </summary>
        /// <param name="tagText"> The text of the tag. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="TagDTO" />.
        /// </returns>
        Task<TagDTO> GetTagAsync(string tagText);

        /// <summary>
        ///     Get all tags for a user.
        /// </summary>
        /// <param name="userId">
        ///     The user's unique identifier.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="List{T}" /> of <see cref="UserTagDTO" />.
        /// </returns>
        Task<List<UserTagDTO>> GetUserTagsAsync(Guid userId);

        /// <summary>
        ///     Get all tags for a world.
        /// </summary>
        /// <param name="worldId">
        ///     The world's unique identifier.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="List{T}" /> of <see cref="WorldTagDTO" />.
        /// </returns>
        Task<List<WorldTagDTO>> GetWorldTagsAsync(Guid worldId);

        /// <summary> Create a new tag. </summary>
        /// <param name="tag"> The tag to create. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="TagDTO" />.
        /// </returns>
        Task<TagDTO> CreateTagAsync(TagDTO tag);

        /// <summary> Create a new user tag. </summary>
        /// <param name="userTag">
        ///     The user tag to create.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="UserTagDTO" />.
        /// </returns>
        Task<UserTagDTO> CreateUserTagAsync(UserTagDTO userTag);

        /// <summary>
        ///     Create a new world tag.
        /// </summary>
        /// <param name="worldTag">
        ///     The world tag to create.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="WorldTagDTO" />.
        /// </returns>
        Task<WorldTagDTO> CreateWorldTagAsync(WorldTagDTO worldTag);

        /// <summary>
        ///     Get all tags for an article.
        /// </summary>
        /// <param name="articleTag">
        ///     The article tag to create.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="ArticleTagDTO" />.
        /// </returns>
        Task<ArticleTagDTO> CreateArticleTagAsync(ArticleTagDTO articleTag);
    }
}