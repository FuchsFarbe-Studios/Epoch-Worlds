// EpochWorlds
// ToneRepresentationType.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 29-12-2023

using EpochApp.Shared.Utils;

namespace EpochApp.Shared.Services
{
    public enum ToneRepresentationType
    {
        [Description("Tone Letters")]
        ToneLetters,

        [Description("Numbers")]
        SuperscriptNumbers,

        [Description("Diacritics")]
        Diacritics
    }
}