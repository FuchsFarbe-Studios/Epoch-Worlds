// EpochWorlds
// Consonant.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Config
{
    public class Consonant : Phoneme
    {
        public ConsonantManner Manner { get; set; }
        public ConsonantPlace Place { get; set; }
        public bool IsVoiced { get; set; }
    }
}