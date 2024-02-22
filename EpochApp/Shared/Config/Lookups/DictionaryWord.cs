// EpochWorlds
// DictionaryWord.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared.Config
{

    /// <summary>
    ///     A dictionary word used for language generation.
    /// </summary>
    public class DictionaryWord
    {
        /// <summary>
        ///     Word's unique identifier.
        /// </summary>
        public int WordId { get; set; }

        /// <summary>
        ///     The words english translation(s).
        /// </summary>
        public string Translations { get; set; }

        /// <summary>
        ///     The category of the word, for sorting and exclusion purposes.
        /// </summary>
        public WordCategory Category { get; set; } = WordCategory.General;
        public int PartOfSpeechId { get; set; }

        public virtual PartOfSpeech PartOfSpeech { get; set; }
    }
}