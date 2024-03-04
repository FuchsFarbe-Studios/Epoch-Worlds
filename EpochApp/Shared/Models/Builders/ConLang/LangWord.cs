// EpochWorlds
// LangWord.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared
{
    /// <summary> A conlang word. </summary>
    [Serializable]
    public class LangWord
    {
        /// <summary>
        ///     Translations for the conlang word. Comma separated.
        /// </summary>
        [XmlElement("WordTranslations")]
        public string Translations { get; set; }

        /// <summary>
        ///     Part of speech for this word.
        /// </summary>
        public string PartOfSpeech { get; set; }

        /// <summary>
        ///     This word in the conlang's IPA.
        /// </summary>
        public string WordInIPA { get; set; } = "Random";

        /// <summary>
        ///     Spelling override for this word.
        /// </summary>
        public string SpellingOverride { get; set; }
    }
}