// EpochWorlds
// Phoneme.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    /// <summary>
    ///   Represents a phoneme.
    /// </summary>
    public class Phoneme
    {
        /// <summary>
        /// The phoneme's unique identifier.
        /// </summary>
        public int PhonemeId { get; set; }

        /// <summary>
        /// The phoneme's character representation.
        /// </summary>
        public string PhonemeChar { get; set; }

        /// <summary>
        /// The phoneme's audio file.
        /// </summary>
        public string AudioFile { get; set; }
    }
}