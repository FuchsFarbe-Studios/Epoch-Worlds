// EpochWorlds
// SlugService.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    /// <summary>
    /// Service for retrieving data by their slugs.
    /// </summary>
    public class SlugService : ISlugService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ISlugService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for SlugService.
        /// </summary>
        /// <param name="context"> The database context. </param>
        /// <param name="logger"> The logger. </param>
        public SlugService(EpochDataDbContext context, ILogger<SlugService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<WorldDTO> GetWorldBySlugAsync(string slug)
        {
            var world = await _context.Worlds
                                      .Select(x => _mapper.Map(x, new WorldDTO()))
                                      .FirstOrDefaultAsync(w => w.Slug == slug);
            return world;
        }

        /// <inheritdoc />
        public async Task<ArticleDTO> GetArticleBySlugAsync(string slug)
        {
            var article = await _context.Articles
                                        .Select(x => _mapper.Map(x, new ArticleDTO()))
                                        .FirstOrDefaultAsync(a => a.Slug == slug);
            return article;
        }
    }
}