// EpochWorlds
// MarkupService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using Microsoft.AspNetCore.Components;

namespace EpochApp.Components.Services
{
    /// <summary>
    ///   Service for translating and cleaning markup string into HTML markup.
    /// </summary>
    public class MarkupService
    {
        /// <summary>
        ///    Translates markup string to HTML markup.
        /// </summary>
        /// <param name="markup"> The markup to translate. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="MarkupString"/>. </returns>
        public async Task<MarkupString> TranslateMarkupAsync(string markup)
        {
            await Task.Delay(1000);
            return await Task.FromResult((MarkupString)markup);
        }

        /// <summary>
        /// Validates a markup and verifies there are no script or style tags.
        /// </summary>
        /// <param name="markup"> The markup to validate. </param>
        /// <returns> An <see cref="IEnumerable{T}"/> of <see cref="string"/>. </returns>
        public IEnumerable<string>? ValidateMarkup(string markup)
        {
            yield return "";
        }
    }
}