// EpochWorlds
// MarkupService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Services
{
    /// <summary>
    ///    Service for translating and validating markup.
    /// </summary>
    public class MarkupService
    {
        /// <summary>
        ///    Translates markup to a MarkupString.
        /// </summary>
        /// <param name="markupString"></param>
        /// <returns></returns>
        public Task<MarkupString> TranslateMarkupAsync(string markupString)
        {
            return null;
        }

        /// <summary>
        ///   Validates markup.
        /// </summary>
        /// <param name="markup"> The markup to validate. </param>
        /// <returns> A collection of validation messages. </returns>
        public IEnumerable<string> ValidateMarkup(string markup)
        {
            yield return "";
        }
    }
}