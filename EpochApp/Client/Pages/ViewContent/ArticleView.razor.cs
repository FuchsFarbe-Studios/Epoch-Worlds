using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     Displays an article's data in a read-only format for public display.
    /// </summary>
    public partial class ArticleView
    {
 #pragma warning disable CS0414// Field is assigned but its value is never used
        private ArticleDTO _article = null!;
 #pragma warning restore CS0414// Field is assigned but its value is never used

        /// <summary>
        ///     The world id related to the article.
        /// </summary>
        [Parameter] public string WorldId { get; set; }

        /// <summary>
        ///     The article id to display.
        /// </summary>
        [Parameter] public string ArticleId { get; set; }
    }
}