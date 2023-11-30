// EpochWorlds
// Vowel.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Config
{
    public class Vowel : Phoneme
    {
        public string Depth { get; set; }
        public string Verticality { get; set; }
        public bool IsRounded { get; set; }
    }
}