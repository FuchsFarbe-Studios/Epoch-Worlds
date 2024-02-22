// EpochWorlds
// PartOfSpeech.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared.Config
{
    /// <summary>
    ///     Represents a part of speech for a language.
    /// </summary>
    public class PartOfSpeech
    {
        public int PartOfSpeechId { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}