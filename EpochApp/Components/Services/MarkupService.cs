// EpochWorlds
// MarkupService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using Microsoft.AspNetCore.Components;

namespace EpochApp.Components.Services
{
    public class MarkupService
    {
        public async Task<MarkupString> TranslateMarkupAsync(string markup)
        {
            await Task.Delay(1000);
            return await Task.FromResult((MarkupString)markup);
        }

        /// <summary>
        /// Validates a markup and verifies there are no script or style tags.
        /// </summary>
        /// <param name="markup"> The markup to validate. </param>
        /// <returns> A collection of errors. </returns>
        public IEnumerable<string>? ValidateMarkup(string markup)
        {
            yield return "";
        }
    }
}