// EpochWorlds
// WorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Client;
using EpochApp.Shared.Social;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;
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

                var UserWorldDTO = MapWorldToUserWorldDTO(world);
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
            var newWorld = MapUserWorldDTOToExistingWorld(world);
            _logger.LogInformation("Saving new world...");
            await _context.Worlds.AddAsync(newWorld);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Returning saved world...");
            var w = MapWorldToUserWorldDTO(newWorld);
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
                                           .Include(x => x.WorldFiles)
                                           .Include(x => x.WorldArticles)
                                           .ThenInclude(article => article.Sections)
                                           .AsSplitQuery()
                                           .ToListAsync();
            var worldData = new List<UserWorldDTO>();
            foreach (var world in userWorlds)
            {
                var UserWorldDTO = MapWorldToUserWorldDTO(world);
                worldData.Add(UserWorldDTO);
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
                                      .Include(x => x.WorldFiles)
                                      .Include(x => x.WorldArticles)
                                      .ThenInclude(article => article.Sections)
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync(x => x.WorldId == worldId);
            var UserWorldDTO = MapWorldToUserWorldDTO(world);
            return await Task.FromResult(UserWorldDTO);
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
            var existingWorld = MapUserWorldDTOToExistingWorld(world);
            try
            {
                _context.Entry(existingWorld).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation("World updated!");
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await WorldExists(existingWorld.WorldId);
                if (!exists)
                    return null;

                _logger.LogError("World does not exist!");
            }
            var updatedWorld = MapWorldToUserWorldDTO(existingWorld);
            return await Task.FromResult(updatedWorld);
        }

        /// <inheritdoc />
        public World MapUserWorldDTOToExistingWorld(UserWorldDTO dto)
        {
            var world = _context.Worlds
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
                                .FirstOrDefault(x => x.WorldId == dto.WorldId);
            if (world == null)
                world = new World();
            world.OwnerId = dto.OwnerId;
            world.WorldName = dto.WorldName;
            world.Pronunciation = dto.Pronunciation;
            world.Excerpt = dto.Excerpt;
            world.Image = dto.Image;
            world.Header = dto.Header;
            world.SubHeader = dto.SubHeader;
            world.Description = dto.Description;
            world.LanguageCode = dto.LanguageCode;
            world.FollowerNamingSingular = dto.FollowerNamingSingular;
            world.FollowerNamingPlural = dto.FollowerNamingPlural;
            world.IsActiveWorld = dto.IsActiveWorld;
            world.DateModified = DateTime.UtcNow;

            if (world.CurrentWorldDate == null)
                world.CurrentWorldDate = new WorldDate();
            world.CurrentWorldDate.CurrentDay = dto.CurrentWorldDate.CurrentDay;
            world.CurrentWorldDate.CurrentMonth = dto.CurrentWorldDate.CurrentMonth;
            world.CurrentWorldDate.CurrentYear = dto.CurrentWorldDate.CurrentYear;
            world.CurrentWorldDate.CurrentAge = dto.CurrentWorldDate.CurrentAge;
            world.CurrentWorldDate.BeforeEraName = dto.CurrentWorldDate.BeforeEra;
            world.CurrentWorldDate.AfterEraName = dto.CurrentWorldDate.AfterEra;
            world.CurrentWorldDate.BeforeEraAbbreviation = dto.CurrentWorldDate.BeforeEraAbbreviation;
            world.CurrentWorldDate.AfterEraAbbreviation = dto.CurrentWorldDate.AfterEraAbbreviation;
            world.MetaData = dto.MetaData.Select(x => new WorldMeta
                                                      {
                                                          Content = x.Content,
                                                          Template = _context.MetaTemplates.FirstOrDefault(y => y.TemplateId == x.TemplateId && y.CategoryId == x.CategoryId)
                                                      })
                                .ToList();
            world.WorldTags = dto.WorldTags.Select(x => new WorldTag
                                                        {
                                                            World = world,
                                                            Tag = new Tag
                                                                  {
                                                                      Text = x.Description
                                                                  }
                                                        })
                                 .ToList();
            world.WorldArticles = dto.WorldArticles.Select(x => new Article
                                                                {
                                                                    AuthorId = x.AuthorId,
                                                                    WorldId = x.WorldId,
                                                                    CategoryId = x.CategoryId,
                                                                    Title = x?.Title,
                                                                    Content = x?.Content,
                                                                    IsPublished = x.IsPublished,
                                                                    IsNSFW = x.IsNSFW,
                                                                    DisplayAuthor = x.DisplayAuthor,
                                                                    ShowInTableOfContents = x.ShowInTableOfContents,
                                                                    ShowTableOfContents = x.ShowTableOfContents,
                                                                    CreatedOn = x.CreatedOn,
                                                                    ModifiedOn = x?.ModifiedOn,
                                                                    Sections = x?.Sections.Select(y => new ArticleSection
                                                                                                       {
                                                                                                           Title = y.Title,
                                                                                                           Content = y.Content,
                                                                                                           CreatedOn = DateTime.UtcNow
                                                                                                       })
                                                                                .ToList()
                                                                })
                                     .ToList();
            world.WorldFiles = dto.WorldFiles.Select(x => new UserFile
                                                          {
                                                              FilePath = x.FilePath,
                                                              SafeName = x.SafeName,
                                                              Alias = x.Alias,
                                                              ImageAlt = x.AltText,
                                                              UploadedOn = x.UploadedOn
                                                          })
                                  .ToList();
            world.WorldGenres = dto.WorldGenres.Select(x => new WorldGenre
                                                            {
                                                                WorldID = x.WorldID,
                                                                GenreID = x.GenreId,
                                                                Genre = _context.Genres.FirstOrDefault(y => y.GenreId == x.GenreId)
                                                            })
                                   .ToList();
            return world;
        }

        /// <inheritdoc />
        public UserWorldDTO MapWorldToUserWorldDTO(World world)
        {
            var metas = world.MetaData.Select(x => new WorldMetaDTO
                                                   {
                                                       WorldId = x.WorldId,
                                                       Content = x.Content,
                                                       TemplateId = x.Template.TemplateId,
                                                       CategoryId = x.Template.CategoryId
                                                   })
                             .ToList();
            var articles = world.WorldArticles.Select(x => new ArticleDTO
                                                           {
                                                               ArticleId = x.ArticleId,
                                                               AuthorId = x.AuthorId,
                                                               WorldId = x.WorldId,
                                                               CategoryId = x.CategoryId,
                                                               Title = x?.Title,
                                                               Content = x?.Content,
                                                               IsPublished = x.IsPublished,
                                                               IsNSFW = x.IsNSFW,
                                                               DisplayAuthor = x.DisplayAuthor,
                                                               ShowInTableOfContents = x.ShowInTableOfContents,
                                                               ShowTableOfContents = x.ShowTableOfContents,
                                                               CreatedOn = x.CreatedOn,
                                                               ModifiedOn = x?.ModifiedOn,
                                                               Sections = x?.Sections.Select(y => new SectionDTO
                                                                                                  {
                                                                                                      SectionID = y.SectionID,
                                                                                                      Title = y?.Title,
                                                                                                      Content = y?.Content,
                                                                                                      CreatedOn = y?.CreatedOn
                                                                                                  })
                                                                           .ToList()
                                                           })
                                .ToList();
            var files = world.WorldFiles.Select(x => new UserFileDTO
                                                     {
                                                         FileId = x.FileId,
                                                         FilePath = x?.FilePath,
                                                         SafeName = x?.SafeName,
                                                         Alias = x?.Alias,
                                                         AltText = x?.ImageAlt,
                                                         UploadedOn = x?.UploadedOn
                                                     })
                             .ToList();
            var tags = world.WorldTags.Select(x => new WorldTagDTO
                                                   {
                                                       TagId = x.TagId,
                                                       WorldId = x.WorldId,
                                                       Description = x?.Tag?.Text
                                                   })
                            .ToList();
            var genres = world.WorldGenres.Select(x => new WorldGenreDTO
                                                       {
                                                           WorldID = x.WorldID,
                                                           GenreId = x.GenreID,
                                                           GenreName = x?.Genre?.GenreName
                                                       })
                              .ToList();
            var date = new WorldDateDTO
                       {
                           WorldId = world.WorldId,
                           CurrentDay = world.CurrentWorldDate.CurrentDay,
                           CurrentMonth = world.CurrentWorldDate.CurrentMonth,
                           CurrentYear = world.CurrentWorldDate.CurrentYear,
                           CurrentAge = world?.CurrentWorldDate.CurrentAge,
                           BeforeEra = world?.CurrentWorldDate.BeforeEraName,
                           AfterEra = world?.CurrentWorldDate.AfterEraName,
                           BeforeEraAbbreviation = world?.CurrentWorldDate.BeforeEraAbbreviation,
                           AfterEraAbbreviation = world?.CurrentWorldDate.AfterEraAbbreviation,
                           CurrentEra = world?.CurrentWorldDate.CurrentAge
                       };
            var UserWorldDTO = new UserWorldDTO
                               {
                                   OwnerId = world.OwnerId,
                                   WorldId = world.WorldId,
                                   WorldName = world?.WorldName,
                                   Pronunciation = world?.Pronunciation,
                                   Excerpt = world?.Excerpt,
                                   Image = world?.Image,
                                   Header = world?.Header,
                                   SubHeader = world?.SubHeader,
                                   Description = world?.Description,
                                   LanguageCode = world?.LanguageCode,
                                   FollowerNamingSingular = world?.FollowerNamingSingular,
                                   FollowerNamingPlural = world?.FollowerNamingPlural,
                                   IsActiveWorld = world.IsActiveWorld,
                                   DateCreated = world.DateCreated,
                                   DateModified = world?.DateModified,
                                   DateRemoved = world?.DateRemoved,
                                   MetaData = metas,
                                   WorldArticles = articles,
                                   WorldTags = tags,
                                   WorldFiles = files,
                                   WorldGenres = genres,
                                   CurrentWorldDate = date
                               };
            return UserWorldDTO;
        }

        public async Task<UserWorldDTO> DeleteWorldAsync(Guid userId, Guid worldId)
        {
            var world = await _context.Worlds.Include(x => x.Owner)
                                      .Include(x => x.WorldArticles)
                                      .FirstOrDefaultAsync(x => x.WorldId == worldId);
            if (world.OwnerId != userId)
                return null;

            if (world != null)
            {
                world.DateRemoved = DateTime.UtcNow;
                foreach (var article in world.WorldArticles)
                    article.DeletedOn = DateTime.UtcNow;
                _context.Entry(world).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return await Task.FromResult(MapWorldToUserWorldDTO(world));
            }
            return null;
        }

        private async Task<bool> WorldExists(Guid existingWorldWorldId)
        {
            return await _context.Worlds.AnyAsync(x => x.WorldId == existingWorldWorldId);
        }
    }
}