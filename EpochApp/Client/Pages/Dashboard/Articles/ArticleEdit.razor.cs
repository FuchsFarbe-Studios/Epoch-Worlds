using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///     Allows a user to edit an article or select an article to edit.
    /// </summary>
    public partial class ArticleEdit
    {
        [Parameter] public string ArticleId { get; set; }
    }
}