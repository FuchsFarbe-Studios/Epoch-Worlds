// EpochWorlds
// ITemplateService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Service for fetching site templates.
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        ///     Get all article templates.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="ArticleTemplateDTO" />.
        /// </returns>
        Task<List<ArticleTemplateDTO>> GetArticleTemplatesAsync();
    }
}