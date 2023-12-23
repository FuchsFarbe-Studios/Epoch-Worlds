// EpochWorlds
// Consonant.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Config
{
    public class Consonant : Phoneme
    {
        public string Manner { get; set; }
        public string Place { get; set; }
        public bool IsVoiced { get; set; }
    }
}