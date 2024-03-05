// EpochWorlds
// ManuscriptService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using EpochApp.Shared;

namespace EpochApp.Client.Services
{
    /// <inheritdoc />
    public class ManuscriptService : IManuscriptService
    {
        /// <inheritdoc />
        public Task<List<ManuscriptDTO>> GetUserManuscripts(Guid userId)
        {
            return null;
        }
    }
}