// EpochWorlds
// Consonant.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class Consonant : Phoneme
    {
        public ConsonantManner Manner { get; set; }
        public ConsonantPlace Place { get; set; }
        public bool IsVoiced { get; set; }
    }
}