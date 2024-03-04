// EpochWorlds
// TemplateService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;

#pragma warning disable 1591
namespace EpochApp.Server.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ITemplateService> _logger;

        public TemplateService(EpochDataDbContext context, ILogger<TemplateService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        ///     Get all article templates.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="ArticleTemplateDTO" />.
        /// </returns>
        public Task<List<ArticleTemplateDTO>> GetArticleTemplatesAsync()
        {

            return null;
        }
    }
}