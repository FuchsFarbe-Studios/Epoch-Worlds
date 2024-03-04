// EpochWorlds
// Vowel.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class Vowel : Phoneme
    {
        public VowelDepth Depth { get; set; }
        public VowelVerticality Verticality { get; set; }
        public bool IsRounded { get; set; }
    }
}