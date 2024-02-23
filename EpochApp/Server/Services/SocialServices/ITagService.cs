// EpochWorlds
// ITagService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
using EpochApp.Shared.Social;

namespace EpochApp.Server.Services
{
    /// <summary>
    ///     Service for managing tags.
    /// </summary>
    public interface ITagService
    {
        /// <summary> Create a new tag. </summary>
        /// <param name="tag"> The tag to create. </param>
        /// <returns> The created tag. </returns>
        Task<Tag> CreateTag(Tag tag);

        /// <summary> Add a tag to a user. </summary>
        /// <param name="tagId"> The tag to add. </param>
        /// <param name="userId">
        ///     The user to add the tag to.
        /// </param>
        /// <returns> A task. </returns>
        Task AddTagToUser(long tagId, Guid userId);


        /// <summary>
        ///     Get all tags for a user.
        /// </summary>
        /// <param name="userId">
        ///     The user to get tags for.
        /// </param>
        /// <returns> A list of tags. </returns>
        Task<IEnumerable<Tag>> GetUserTags(Guid userId);
    }
}