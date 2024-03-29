// EpochWorlds
// IWorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared.Users;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Service for handling and modifying World Data.
    /// </summary>
    public interface IWorldService
    {
        /// <summary>
        ///    Get all worlds.
        /// </summary>
        /// <returns> A list of <see cref="WorldDTO"/>. </returns>
        Task<List<WorldDTO>> GetWorldsAsync();

        /// <summary>
        ///    Get all worlds for a specific user.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <returns> A list of <see cref="WorldDTO"/>. </returns>
        Task<List<WorldDTO>> GetUserWorldsAsync(Guid userId);

        /// <summary> Create a new world. </summary>
        /// <param name="registration"> The registration data. </param>
        /// <param name="user">
        ///     The user creating the world.
        /// </param>
        /// <returns> A <see cref="Task" />. </returns>
        Task<World> CreateRegistrationWorldAsync(RegistrationDTO registration, User user);

        /// <summary> Create a new world. </summary>
        /// <param name="world"> The world to create. </param>
        /// <returns>
        ///     A <see cref="WorldDTO" />.
        /// </returns>
        Task<WorldDTO> CreateWorldAsync(WorldDTO world);

        /// <summary> Get a specific world. </summary>
        /// <param name="worldId">
        ///     The unique identifier for the world.
        /// </param>
        /// <returns>
        ///     A <see cref="WorldDTO" />.
        /// </returns>
        Task<WorldDTO> GetWorldAsync(Guid worldId);

        /// <summary> Get a specific world. </summary>
        /// <param name="worldId">
        ///     The unique identifier for the world.
        /// </param>
        /// <returns> A <see cref="World" />. </returns>
        Task<WorldDTO> GetWorldViewAsync(Guid worldId);

        /// <summary> Update a world. </summary>
        /// <param name="world"> The world to update. </param>
        /// <returns>
        ///     A <see cref="WorldDTO" />.
        /// </returns>
        Task<WorldDTO> UpdateWorldAsync(WorldDTO world);

        /// <summary>
        ///     Update the active world for a user.
        /// </summary>
        /// <param name="world">
        ///     The world to make active.
        /// </param>
        /// <returns>
        ///     A <see cref="WorldDTO" />.
        /// </returns>
        Task<WorldDTO> UpdateActiveUserWorldsAsync(WorldDTO world);

        /// <summary>
        ///     Remove a world from a user's list of worlds.
        /// </summary>
        /// <param name="userId">
        ///     The user's unique identifier.
        /// </param>
        /// <param name="worldId">
        ///     The world's unique identifier.
        /// </param>
        /// <returns>
        ///     A <see cref="WorldDTO" />.
        /// </returns>
        Task<WorldDTO> DeleteWorldAsync(Guid userId, Guid worldId);

        /// <summary>
        ///     Get the active world for a user.
        /// </summary>
        /// <param name="userId">
        ///     The user's unique identifier.
        /// </param>
        /// <returns>
        ///     A <see cref="WorldDTO" />.
        /// </returns>
        Task<WorldDTO> GetActiveWorldAsync(Guid userId);
    }
}