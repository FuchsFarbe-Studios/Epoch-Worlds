using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     Displays an article's data in a read-only format for public display.
    /// </summary>
    public partial class ArticleView
    {
        private ArticleDTO _article;

        [Parameter] public string ArticleId { get; set; }
    }
}