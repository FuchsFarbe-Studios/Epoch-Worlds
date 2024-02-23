// EpochWorlds
// WorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.EntityFrameworkCore;

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

        /// <inheritdoc />
        public async Task<List<MetaTemplate>> IndexMetaTemplatesAsync()
        {
            return await _context.MetaTemplates
                                 .Include(x => x.Category)
                                 .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<WorldDateDTO> GetWorldDate(Guid worldId)
        {
            var worldDate = await _context.Worlds
                                          .Include(x => x.CurrentWorldDate)
                                          .Select(x => new WorldDateDTO
                                                       {
                                                           WorldId = x.WorldID,
                                                           CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                           CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                           CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                           CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                           BeforeEra = x.CurrentWorldDate.BeforeEraName,
                                                           AfterEra = x.CurrentWorldDate.AfterEraName,
                                                           BeforeEraAbbreviation = x.CurrentWorldDate.BeforeEraAbbreviation,
                                                           AfterEraAbbreviation = x.CurrentWorldDate.AfterEraAbbreviation,
                                                           CurrentEra = x.CurrentWorldDate.CurrentAge
                                                       })
                                          .FirstOrDefaultAsync(x => x.WorldId == worldId);
            return await Task.FromResult(worldDate);
        }

        /// <inheritdoc />
        public async Task<WorldDTO> GetWorldByIdAsync(Guid worldId)
        {
            var world = await _context.Worlds
                                      .Include(x => x.MetaData)
                                      .ThenInclude(x => x.Template)
                                      .ThenInclude(x => x.Category)
                                      .Include(x => x.CurrentWorldDate)
                                      .Include(x => x.WorldArticles)
                                      .Select(x => new WorldDTO
                                                   {
                                                       AuthorID = x.OwnerID,
                                                       WorldID = x.WorldID,
                                                       WorldName = x.WorldName,
                                                       Pronunciation = x.Pronunciation,
                                                       Description = x.Description,
                                                       DateCreated = x.DateCreated,
                                                       DateModified = x.DateModified,
                                                       DateRemoved = x.DateRemoved,
                                                       MetaData = x.MetaData,
                                                       IsActiveWorld = x.IsActiveWorld,
                                                       WorldDate = new WorldDateDTO
                                                                   {
                                                                       CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                                       CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                                       CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                                       CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                                       BeforeEra = x.CurrentWorldDate.BeforeEraName,
                                                                       AfterEra = x.CurrentWorldDate.AfterEraName,
                                                                       BeforeEraAbbreviation = x.CurrentWorldDate.BeforeEraAbbreviation,
                                                                       AfterEraAbbreviation = x.CurrentWorldDate.AfterEraAbbreviation,
                                                                       CurrentEra = x.CurrentWorldDate.CurrentAge
                                                                   }
                                                   })
                                      .FirstOrDefaultAsync(x => x.WorldID == worldId);
            return await Task.FromResult(world);
        }
    }
}