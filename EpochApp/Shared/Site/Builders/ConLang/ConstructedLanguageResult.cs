// EpochWorlds
// ConstructedLanguageResult.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using System.Xml.Serialization;

namespace EpochApp.Shared
{

    /// <summary>
    ///     Holds the result data from generating a constructed language.
    /// </summary>
    [Serializable]
    [XmlRoot("LanguageResult", Namespace = "http://www.epochapp.com/conglang")]
    public class ConstructedLanguageResult
    {
        [XmlElement("GeneratedWords")]
        public List<GeneratedWord> Words { get; set; }
    }
}