// EpochWorlds
// WorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Server.Data;

namespace EpochApp.Server.Services.WorldService
{

    /// <inheritdoc />
    public class WorldService : IWorldService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IWorldService> _logger;

        /// <summary>
        ///     Constructor for WorldService.
        /// </summary>
        /// <param name="context"> Database Context. </param>
        /// <param name="logger"> Logger. </param>
        public WorldService(EpochDataDbContext context, ILogger<WorldService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}