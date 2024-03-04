// EpochWorlds
// Spelling.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Represents settings for a constructed language's spelling rules.
    /// </summary>
    [Serializable]
    public class Spelling
    {
        /// <summary>
        ///     Toggle for using spelling rules.
        /// </summary>
        public bool UseSpellingRules { get; set; } = false;

        /// <summary>
        ///     Spelling rules for this language.
        /// </summary>
        [XmlElement("SpellingRules")]
        public List<SpellingRule> SpellingRules { get; set; } = new List<SpellingRule>();

        /// <summary>
        ///     Toggle for using a second spelling system.
        /// </summary>
        public bool UseSecondSpelling { get; set; } = false;

        /// <summary>
        ///     Defines rules for the second spelling system when utilizing a font.
        /// </summary>
        [XmlElement("SecondSpellingRules")]
        public List<SpellingRule> SecondSpelling { get; set; } = new List<SpellingRule>();

        /// <summary>
        ///     Toggle for making spelling stress sensitive.
        /// </summary>
        public bool MakeSpellingStressSensitive { get; set; } = false;

        /// <summary>
        ///     Toggle for applying default spelling.
        /// </summary>
        public bool ApplyDefaultSpelling { get; set; } = false;

        /// <summary>
        ///     Converts diacritics to single characters.
        /// </summary>
        public bool ConvertDiacritics { get; set; } = false;

        /// <summary>
        ///     Arranges jamo characters.
        /// </summary>
        public bool ArrangeJamo { get; set; } = false;

        /// <summary>
        ///     No spelling rules will be applied.
        /// </summary>
        public bool NoSpelling { get; set; } = false;

        /// <summary>
        ///     Path to uploaded custom font file.
        /// </summary>
        public string CustomFontPath { get; set; } = "";

        /// <summary> Custom alphabet order. </summary>
        public string AlphabetOrder { get; set; } = "";
    }
}