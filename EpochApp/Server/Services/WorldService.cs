// EpochWorlds
// WorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Client;
using EpochApp.Shared.Social;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    /// <inheritdoc />
    public class WorldService : IWorldService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IWorldService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Constructor for WorldService.
        /// </summary>
        /// <param name="context"> Database Context. </param>
        /// <param name="logger"> Logger. </param>
        public WorldService(EpochDataDbContext context, ILogger<WorldService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<UserWorldDTO>> GetWorldsAsync()
        {
            _logger.LogInformation("Getting all worlds...");
            var worlds = await _context.Worlds
                                       .Include(x => x.CurrentWorldDate)
                                       .Include(x => x.WorldGenres)
                                       .ThenInclude(x => x.Genre)
                                       .Include(x => x.WorldTags)
                                       .ThenInclude(x => x.Tag)
                                       .Include(x => x.MetaData)
                                       .ThenInclude(x => x.Template)
                                       .ThenInclude(x => x.Category)
                                       .Include(x => x.WorldFiles)
                                       .Include(x => x.WorldArticles)
                                       .ThenInclude(article => article.Sections)
                                       .AsSplitQuery()
                                       .ToListAsync();
            var worldData = new List<UserWorldDTO>();
            foreach (var world in worlds)
            {
                var UserWorldDTO = _mapper.Map<World, UserWorldDTO>(world);
                worldData.Add(UserWorldDTO);
            }
            _logger.LogInformation("Returning all worlds...");
            return worldData;
        }

        /// <inheritdoc />
        public async Task<World> CreateRegistrationWorldAsync(RegistrationDTO registration, User user)
        {
            _logger.LogInformation("Creating new world from registration...");
            var metaTemplates = await _context.MetaTemplates
                                              .Include(x => x.Category)
                                              .ToListAsync();

            var meta = new List<WorldMeta>();
            var files = new List<UserFile>();
            var articles = new List<Article>();
            var tags = new List<WorldTag>();
            var genres = new List<WorldGenre>();
            var date = new WorldDate
                       {
                           CurrentDay = 0,
                           CurrentMonth = 0,
                           CurrentYear = 0
                       };
            var world = new World
                        {
                            WorldName = registration?.WorldName,
                            IsActiveWorld = true,
                            DateCreated = DateTime.UtcNow
                            //Owner = user,
                        };
            metaTemplates.ForEach(x => meta.Add(new WorldMeta
                                                {
                                                    MetaID = x.TemplateId,
                                                    Template = x,
                                                    World = world
                                                }));
            world.MetaData = meta;
            world.WorldFiles = files;
            world.WorldArticles = articles;
            world.WorldTags = tags;
            world.WorldGenres = genres;
            world.CurrentWorldDate = date;
            return await Task.FromResult(world);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> CreateWorldAsync(UserWorldDTO world)
        {
            _logger.LogInformation("Creating new world...");
            var newWorld = _mapper.Map<UserWorldDTO, World>(world);
            _logger.LogInformation("Saving new world...");
            await _context.Worlds.AddAsync(newWorld);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Returning saved world...");
            var w = _mapper.Map<World, UserWorldDTO>(newWorld);
            return await Task.FromResult(w);
        }

        /// <inheritdoc />
        public async Task<List<UserWorldDTO>> GetUserWorldsAsync(Guid userId)
        {
            var userWorlds = await _context.Worlds.Where(x => x.OwnerId == userId)
                                           .Include(x => x.CurrentWorldDate)
                                           .Include(x => x.WorldGenres)
                                           .ThenInclude(x => x.Genre)
                                           .Include(x => x.WorldTags)
                                           .ThenInclude(x => x.Tag)
                                           .Include(x => x.MetaData)
                                           .ThenInclude(x => x.Template)
                                           .ThenInclude(x => x.Category)
                                           .Include(x => x.WorldFiles)
                                           .Include(x => x.WorldArticles)
                                           .ThenInclude(article => article.Sections)
                                           .Where(x => x.DateRemoved == null || x.DateRemoved > DateTime.UtcNow)
                                           .AsSplitQuery()
                                           .ToListAsync();
            var worldData = new List<UserWorldDTO>();
            foreach (var world in userWorlds)
            {
                var userWorldDTO = _mapper.Map<World, UserWorldDTO>(world);
                worldData.Add(userWorldDTO);
            }
            return await Task.FromResult(worldData);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> GetWorldAsync(Guid worldId)
        {
            var world = await _context.Worlds
                                      .Include(x => x.CurrentWorldDate)
                                      .Include(x => x.WorldGenres)
                                      .ThenInclude(x => x.Genre)
                                      .Include(x => x.WorldTags)
                                      .ThenInclude(x => x.Tag)
                                      .Include(x => x.MetaData)
                                      .ThenInclude(x => x.Template)
                                      .ThenInclude(x => x.Category)
                                      .Include(x => x.WorldFiles)
                                      .Include(x => x.WorldArticles)
                                      .ThenInclude(article => article.Sections)
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync(x => x.WorldId == worldId);
            var userWorldDTO = _mapper.Map<World, UserWorldDTO>(world);
            return await Task.FromResult(userWorldDTO);
        }

        /// <inheritdoc />
        public async Task<World> GetWorldViewAsync(Guid worldId)
        {
            var world = await _context.Worlds.FirstOrDefaultAsync(x => x.WorldId == worldId);
            return await Task.FromResult(world);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> UpdateWorldAsync(UserWorldDTO world)
        {
            var existingWorld = _context.Worlds
                                        .Include(w => w.CurrentWorldDate)
                                        .Include(w => w.WorldTags)
                                        .ThenInclude(w => w.Tag)
                                        .Include(w => w.WorldFiles)
                                        .Include(w => w.MetaData)
                                        .ThenInclude(worldMeta => worldMeta.Template)
                                        .FirstOrDefault(x => x.WorldId == world.WorldId && x.OwnerId == world.OwnerId);
            if (existingWorld == null)
            {
                _logger.LogError("World does not exist!");
                return null;
            }

            // Handling WorldMetas entities
            // Remove the WorldMetas that are not in the incoming DTO world
            var worldMetasToRemove = existingWorld.MetaData
                                                  .Where(meta => !world.MetaData.Any(m => m.TemplateId == meta.Template.TemplateId));
            foreach (var wm in worldMetasToRemove)
            {
                existingWorld.MetaData.Remove(wm);
            }

            foreach (var wm in world.MetaData)
            {
                var existingMeta = existingWorld.MetaData.FirstOrDefault(m => m.Template.TemplateId == wm.TemplateId);
                if (existingMeta == null)
                {
                    // Add new WorldMeta
                    existingWorld.MetaData.Add(_mapper.Map(wm, new WorldMeta()));
                }
                else
                {
                    // Update existing WorldMeta
                    _mapper.Map(wm, existingMeta);
                }
            }
            _mapper.Map(world, existingWorld);
            try
            {
                _context.Entry(existingWorld).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation("World updated!");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exists = await WorldExists(existingWorld.WorldId);
                if (!exists)
                    return null;

                _logger.LogError($"Error updating world: {ex.Message}");
            }
            var updatedWorld = _mapper.Map<World, UserWorldDTO>(existingWorld);
            return await Task.FromResult(updatedWorld);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> UpdateActiveUserWorldsAsync(UserWorldDTO world)
        {
            var userWorlds = await _context.Worlds.Where(x => x.OwnerId == world.OwnerId).ToListAsync();
            var activeWorld = userWorlds.FirstOrDefault(x => x.WorldId == world.WorldId);
            if (activeWorld == null)
            {
                _logger.LogError("World does not exist!");
                return null;
            }

            foreach (var w in userWorlds)
            {
                w.IsActiveWorld = false;
                if (w.WorldId == activeWorld.WorldId)
                    w.IsActiveWorld = true;
                _context.Entry(w).State = EntityState.Modified;
                _context.Update(w);
            }
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("World updated!");
                return await GetActiveWorldAsync(world.OwnerId);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exists = await WorldExists(activeWorld.WorldId);
                if (!exists)
                    return null;

                _logger.LogError("World does not exist!");
            }
            return null;
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> DeleteWorldAsync(Guid userId, Guid worldId)
        {
            var worldToDelete = await _context.Worlds.FirstOrDefaultAsync(x => x.WorldId == worldId && x.OwnerId == userId);
            if (worldToDelete == null)
            {
                _logger.LogError("World does not exist!");
                return null;
            }
            worldToDelete.DateRemoved = DateTime.UtcNow;
            _context.Entry(worldToDelete).State = EntityState.Modified;
            _context.Update(worldToDelete);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("World deleted!");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exists = await WorldExists(worldToDelete.WorldId);
                if (!exists)
                    return null;

                _logger.LogError("World does not exist!");
            }
            var deletedWorld = _mapper.Map<World, UserWorldDTO>(worldToDelete);
            return await Task.FromResult(deletedWorld);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> GetActiveWorldAsync(Guid userId)
        {
            var activeWorld = await _context.Worlds.Where(x => x.OwnerId == userId && x.IsActiveWorld == true).FirstOrDefaultAsync();
            var userWorldDTO = _mapper.Map<World, UserWorldDTO>(activeWorld);
            return await Task.FromResult(userWorldDTO);
        }

        private async Task<bool> WorldExists(Guid existingWorldWorldId)
        {
            return await _context.Worlds.AnyAsync(x => x.WorldId == existingWorldWorldId);
        }
    }
}