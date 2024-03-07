// EpochWorlds
// BuilderService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    /// <inheritdoc />
    public class BuilderService : IBuilderService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IBuilderService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the <see cref="BuilderService" />.
        /// </summary>
        /// <param name="context"> The <see cref="EpochDataDbContext" />. </param>
        /// <param name="logger"> The <see cref="ILogger{TCategoryName}" />. </param>
        /// <param name="mapper"> The <see cref="IMapper" />. </param>
        public BuilderService(EpochDataDbContext context, ILogger<BuilderService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BuilderContentDTO>> GetBuilderContentByAuthorAsync(Guid userId)
        {
            return await _context.BuilderContents
                                 .Where(x => x.AuthorID == userId)
                                 .Include(x => x.Author)
                                 .Select(x => _mapper.Map<BuilderContentDTO>(x))
                                 .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> GetBuilderContentByIdAsync(Guid contentId)
        {
            var content = await _context.BuilderContents
                                        .FirstOrDefaultAsync(x => x.ContentID == contentId);
            return _mapper.Map<BuilderContentDTO>(content);
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> GenerateContentAsync(Guid contentId, Guid userId)
        {
            var content = await _context.BuilderContents
                                        .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            switch (content.ContentType)
            {
                case ContentType.ConstructedLanguage:
                    _logger.LogInformation("Generating language...");
                    //await _language.GenerateLanguage(content);
                    break;
                case ContentType.Character:
                    break;
                case ContentType.Map:
                    break;
                case ContentType.World:
                    break;
                case ContentType.Calendar:
                    break;
                case ContentType.Religion:
                    break;
                case ContentType.System:
                    break;
                default:
                    return null;
            }
            return _mapper.Map<BuilderContentDTO>(content);
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> CreateNewBuilderContentAsync(BuilderContentDTO content)
        {
            var newContent = _mapper.Map<BuilderContent>(content);
            newContent.DateCreated = DateTime.UtcNow;
            await _context.BuilderContents.AddAsync(newContent);
            await _context.SaveChangesAsync();
            return _mapper.Map<BuilderContentDTO>(newContent);
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> UpdateBuilderAsync(Guid userId, Guid contentId, BuilderContentDTO content)
        {
            var existingContent = await _context.BuilderContents
                                                .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            if (existingContent == null)
                return null;

            _mapper.Map(content, existingContent);
            _context.Entry(existingContent).State = EntityState.Modified;
            try
            {
                _context.BuilderContents.Update(existingContent);
                await _context.SaveChangesAsync();
                return _mapper.Map<BuilderContentDTO>(existingContent);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating builder content...");
                return null;
            }
        }
    }
}