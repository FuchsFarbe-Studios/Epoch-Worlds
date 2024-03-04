// EpochWorlds
// GeneratedWord.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Generated constructed language word.
    /// </summary>
    [Serializable]
    public class GeneratedWord
    {
        /// <summary>
        ///     Translations of the word separated by commas.
        /// </summary>
        public string Translations { get; set; }

        /// <summary>
        ///     This words part of speech.
        /// </summary>
        public string PartOfSpeech { get; set; }

        /// <summary>
        ///     This words IPA pronunciation.
        /// </summary>
        public string IPA { get; set; }

        /// <summary> This words spelling. </summary>
        public string ConLangWord { get; set; }

        /// <summary>
        ///     Alternate spelling, if any.
        /// </summary>
        public string? ConLangWordAlt { get; set; }
    }
}