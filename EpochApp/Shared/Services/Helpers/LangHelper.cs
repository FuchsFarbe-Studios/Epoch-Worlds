// EpochWorlds
// LangHelper.cs
//  2024
// Oliver Conover
// Modified: 22-3-2024
using System.Text;

namespace EpochApp.Shared.Services
{
    /// <summary>
    /// A helper class for language operations.
    /// </summary>
    public class LangHelper : ILangHelper
    {
        private readonly Random random = new Random();

        public Task<string> GenerateLegalWord(string consonantsArr, string vowelsArr)
        {
            var word = new StringBuilder();
            var consonants = consonantsArr.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var vowels = vowelsArr.Split(",", StringSplitOptions.RemoveEmptyEntries);
            return Task.FromResult(word.ToString());
        }
    }
}