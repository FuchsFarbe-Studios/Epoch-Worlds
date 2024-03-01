using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Builders
{
    /// <summary>
    ///     Page for creating a new constructed language.
    /// </summary>
    public partial class ConLangCreate
    {
        /// <summary>
        ///     The active world the user has selected.
        /// </summary>
        [CascadingParameter] protected UserWorldDTO ActiveWorld { get; set; }
    }
}