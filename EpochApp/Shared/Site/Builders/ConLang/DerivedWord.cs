// EpochWorlds
// DerivedWord.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Represents the derived words of a constructed language.
    /// </summary>
    public class DerivedWord
    {
        /// <summary>
        ///     Derived translations for the conlang word.
        /// </summary>
        [XmlElement("DerivedTranslations")]
        public List<string> Translations { get; set; } = new List<string>();

        /// <summary>
        ///     Part of speech for this word.
        /// </summary>
        public string PartOfSpeech { get; set; }

        /// <summary>
        ///     List of available words to create a compound or derived word.
        /// </summary>
        [XmlElement("ConLangWords")]
        public List<LangWord> Words { get; set; } = new List<LangWord>();
    }
}