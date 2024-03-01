using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    public partial class ArticleTableOfContents
    {
        /// <summary>
        ///     The article to display the table of contents for.
        /// </summary>
        [Parameter] public ArticleDTO Article { get; set; }

        /// <summary>
        ///     The sections of the article.
        /// </summary>
        public List<SectionDTO> Sections => Article?.Sections.ToList();
    }
}