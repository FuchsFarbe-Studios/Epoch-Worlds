// EpochWorlds
// CongLang.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Represents settings for a constructed language.
    /// </summary>
    [Serializable]
    [XmlRoot("Language", Namespace = "http://www.epochapp.com/conglang")]
    public class ConstructedLanguage
    {
        /// <summary> Name of this language. </summary>
        [XmlElement("Name")]
        public string LangName { get; set; }

        /// <summary>
        ///     Name of this language as its pronounced natively.
        /// </summary>
        [XmlElement("Pronunciation")]
        public string NativePronunciation { get; set; }

        /// <summary>
        ///     Brief description of this language.
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        ///     Language phonology settings.
        /// </summary>
        [XmlElement("PhonologySettings")]
        public Phonology Phonology { get; set; } = new Phonology();

        /// <summary>
        ///     Language spelling settings.
        /// </summary>
        [XmlElement("SpellingSettings")]
        public Spelling Spelling { get; set; } = new Spelling();

        /// <summary>
        ///     Language's vocabulary settings.
        /// </summary>
        [XmlElement("VocabularySettings")]
        public Vocabulary Vocabulary { get; set; } = new Vocabulary();

        /// <summary>
        ///     Grammar settings for this language.
        /// </summary>
        [XmlElement("GrammarSettings")]
        public Syntax Syntax { get; set; } = new Syntax();

        /// <summary>
        ///     Date this language was created.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     Date this language was modified.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }

}