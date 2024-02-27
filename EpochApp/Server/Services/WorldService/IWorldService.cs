// EpochWorlds
// IWorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;

namespace EpochApp.Server.Services.WorldService
{
    /// <summary>
    ///     Service for handling and modifying World Data.
    /// </summary>
    public interface IWorldService
    {

        /// <summary>
        ///    Get all worlds.
        /// </summary>
        /// <returns> A list of <see cref="UserWorldDTO"/>. </returns>
        Task<List<UserWorldDTO>> GetWorldsAsync();

        /// <summary>
        ///    Get all worlds for a specific user.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <returns> A list of <see cref="UserWorldDTO"/>. </returns>
        Task<List<UserWorldDTO>> GetUserWorldsAsync(Guid userId);

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
        ///     A <see cref="UserWorldDTO" />.
        /// </returns>
        Task<UserWorldDTO> CreateWorldAsync(UserWorldDTO world);

        /// <summary> Get a specific world. </summary>
        /// <param name="worldId">
        ///     The unique identifier for the world.
        /// </param>
        /// <returns>
        ///     A <see cref="UserWorldDTO" />.
        /// </returns>
        Task<UserWorldDTO> GetWorldAsync(Guid worldId);

        /// <summary> Get a specific world. </summary>
        /// <param name="worldId">
        ///     The unique identifier for the world.
        /// </param>
        /// <returns> A <see cref="World" />. </returns>
        Task<World> GetWorldViewAsync(Guid worldId);

        /// <summary>
        ///     Map a <see cref="UserWorldDTO" /> to a <see cref="World" />.
        /// </summary>
        /// <param name="dto">
        ///     The <see cref="UserWorldDTO" /> to map.
        /// </param>
        /// <returns> A <see cref="World" />. </returns>
        World MapUserWorldDTOToExistingWorld(UserWorldDTO dto);

        /// <summary>
        ///     Map a <see cref="World" /> to a <see cref="UserWorldDTO" />.
        /// </summary>
        /// <param name="world"> The world to map. </param>
        /// <returns>
        ///     A <see cref="UserWorldDTO" />.
        /// </returns>
        UserWorldDTO MapWorldToUserWorldDTO(World world);
    }
}