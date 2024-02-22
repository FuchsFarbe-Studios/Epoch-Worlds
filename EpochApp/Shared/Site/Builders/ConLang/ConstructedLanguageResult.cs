// EpochWorlds
// ConstructedLanguageResult.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared
{
    [Serializable]
    public class GrammarTableBlock
    {
        public List<GrammarTable> Tables { get; set; } = new List<GrammarTable>();
    }

    [Serializable]
    public class GrammarTableColumn
    {
        [XmlAttribute("FieldName")]
        public string ColName { get; set; }
        [XmlElement("Value")]
        public string ColValue { get; set; }
    }

    [Serializable]
    public class GrammarTableRow
    {
        [XmlElement("Row")]
        public int RowNumber { get; set; }
        [XmlElement("Name")]
        public string RowIdentifier { get; set; }
        [XmlElement("Fields")]
        public List<GrammarTableColumn> Cols { get; set; } = new List<GrammarTableColumn>();
    }

    [Serializable]
    public class GrammarTable
    {
        [XmlElement("Table")]
        public List<GrammarTableRow> TableData { get; set; } = new List<GrammarTableRow>();
    }

    /// <summary>
    ///     Holds the result data from generating a constructed language.
    /// </summary>
    [Serializable]
    [XmlRoot("LanguageResult", Namespace = "http://www.epochapp.com/conglang")]
    public class ConstructedLanguageResult
    {
        [XmlElement("Owner")]
        public Guid? Author { get; set; }

        [XmlElement("Name")]
        public string LanguageName { get; set; }

        [XmlElement("NameIPA")]
        public string Pronunciation { get; set; }

        [XmlElement("GeneratedWords")]
        public List<GeneratedWord> Words { get; set; }
    }
}