using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Builders
{
    /// <summary>
    ///     The results of a constructed language generation.
    /// </summary>
    public partial class ConLangResults
    {
        private List<List<GeneratedWord>> _wordRows = new List<List<GeneratedWord>>();

        /// <summary>
        ///     The generated language result.
        /// </summary>
        [Parameter] public ConstructedLanguageResult GeneratedResult { get; set; }

        /// <summary>
        ///     Whether the language is currently being generated.
        /// </summary>
        [Parameter] public bool Generating { get; set; } = false;
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var inputList = GeneratedResult?.Words;// replace with your list

            var chunkSize = 5;

            _wordRows = inputList
                        .Select((s, i) => new { Value = s, Index = i })
                        .GroupBy(x => x.Index / chunkSize)
                        .Select(grp => grp.Select(x => x.Value).ToList())
                        .ToList();

        }
    }
}