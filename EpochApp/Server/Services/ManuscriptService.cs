// EpochWorlds
// ManuscriptService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    #pragma warning disable CS1591
    public class ManuscriptService : IManuscriptService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IManuscriptService> _logger;
        private readonly IMapper _mapper;

        public ManuscriptService(EpochDataDbContext context, ILogger<ManuscriptService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<ManuscriptDTO>> GetUserManuscripts(Guid userId)
        {
            var userManuscripts = await _context.Manuscripts
                                                .Where(m => m.UserId == userId)
                                                .Include(x => x.Chapters)
                                                .Select(x => _mapper.Map<ManuscriptDTO>(x))
                                                .ToListAsync();
            return userManuscripts;
        }

        /// <inheritdoc />
        public async Task<ManuscriptDTO> GetManuscriptAsync(long manuscriptId)
        {
            var manuscript = await _context.Manuscripts
                                           .Where(m => m.ManuscriptId == manuscriptId)
                                           .Include(x => x.Chapters)
                                           .Select(x => _mapper.Map<ManuscriptDTO>(x))
                                           .FirstOrDefaultAsync();
            return manuscript;
        }

        /// <inheritdoc />
        public async Task<ManuscriptDTO> CreateManuscriptAsync(ManuscriptDTO manuscript)
        {
            var newManuscript = _mapper.Map<Manuscript>(manuscript);
            _context.Manuscripts.Add(newManuscript);
            await _context.SaveChangesAsync();
            return _mapper.Map<ManuscriptDTO>(newManuscript);
        }

        /// <inheritdoc />
        public async Task<ManuscriptDTO> UpdateManuscript(Guid userId, long manuscriptId, ManuscriptDTO manuscript)
        {
            var manuscriptToUpdate = await _context.Manuscripts
                                                   .AsSplitQuery()
                                                   .FirstOrDefaultAsync(x => x.UserId == manuscript.UserID && x.ManuscriptId == manuscriptId);
            _mapper.Map(manuscript, manuscriptToUpdate);
            _context.Manuscripts.Update(manuscriptToUpdate);
            await _context.SaveChangesAsync();
            return _mapper.Map<ManuscriptDTO>(manuscriptToUpdate);
        }
    }
}