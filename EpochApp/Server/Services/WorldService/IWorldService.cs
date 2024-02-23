// EpochWorlds
// IWorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared;
using EpochApp.Shared.Config;

namespace EpochApp.Server.Services.WorldService
{
    /// <summary>
    ///     Service for handling and modifying World Data.
    /// </summary>
    public interface IWorldService
    {
        /// <summary>
        ///     Indexes all MetaTemplates.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="List{T}" /> of <see cref="MetaTemplate" />.
        /// </returns>
        Task<List<MetaTemplate>> IndexMetaTemplatesAsync();

        /// <summary>
        ///     Retrieves a World by its ID.
        /// </summary>
        /// <param name="worldId"> The ID of the World. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="WorldDTO" />.
        /// </returns>
        Task<WorldDTO> GetWorldByIdAsync(Guid worldId);

        /// <summary>
        ///     Retrieves the current date of a World.
        /// </summary>
        /// <param name="worldId"> The ID of the World. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> of <see cref="WorldDateDTO" />.
        /// </returns>
        Task<WorldDateDTO> GetWorldDate(Guid worldId);
    }
}