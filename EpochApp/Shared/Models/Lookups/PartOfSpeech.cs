// EpochWorlds
// PartOfSpeech.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
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