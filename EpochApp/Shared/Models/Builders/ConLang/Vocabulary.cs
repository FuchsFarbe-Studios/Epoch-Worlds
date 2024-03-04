// EpochWorlds
// Vocabulary.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Vocabulary settings for a constructed language.
    /// </summary>
    [Serializable]
    public class Vocabulary
    {
        /// <summary>
        ///     Determines if words can be added and removed.
        /// </summary>
        public bool AddAndRemoveWords { get; set; } = false;

        /// <summary>
        ///     Removes default words from generation.
        /// </summary>
        public bool RemoveDefaults { get; set; } = false;

        /// <summary>
        ///     Removes translations from generation.
        /// </summary>
        public bool RemoveTranslations { get; set; } = false;

        /// <summary>
        ///     Saved words for this conlang.
        /// </summary>
        [XmlElement("Words")]
        public List<LangWord> SavedWords { get; set; } = new List<LangWord>();

        /// <summary>
        ///     Number base for this conlang.
        /// </summary>
        public int NumberBase { get; set; } = 10;

        /// <summary>
        ///     Determines if numbers are listed in the output.
        /// </summary>
        public bool ListNumbersInOutput { get; set; } = true;

        /// <summary>
        ///     List of derived conlang words.
        /// </summary>
        [XmlElement("DerivedWords")]
        public List<DerivedWord> DerivedWords { get; set; } = new List<DerivedWord>();

        /// <summary>
        ///     Toggle for using default derivations.
        /// </summary>
        public bool UseDefaultDerivations { get; set; } = true;

        /// <summary>
        ///     Toggle for using part of speech morphology.
        /// </summary>
        public bool UsePartOfSpeechMorphology { get; set; } = true;

        /// <summary>
        ///     Word morphological rules.
        /// </summary>
        public string MorphologyRules { get; set; }
    }
}