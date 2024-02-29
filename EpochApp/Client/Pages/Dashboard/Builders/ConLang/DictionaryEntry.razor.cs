using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Builders
{
    /// <summary>
    ///     A dictionary entry for a generated word.
    /// </summary>
    public partial class DictionaryEntry
    {
        /// <summary> The word to display. </summary>
        [Parameter] public GeneratedWord Word { get; set; } = null!;
    }
}