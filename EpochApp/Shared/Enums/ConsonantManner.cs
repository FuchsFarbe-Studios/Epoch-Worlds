// EpochWorlds
// ConsonantManner.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared.Utils;

namespace EpochApp.Shared
{
    public enum ConsonantManner
    {
        [Description("Nasal")]
        Nasal,

        [Description("Stop")]
        Stop,

        [Description("Fricative")]
        Fricative,

        [Description("Affricate")]
        Affricate,

        [Description("Approximant")]
        Approximant,

        [Description("Trill")]
        Trill,

        [Description("Flap")]
        Flap,

        [Description("Lateral Approximant")]
        LateralApproximant
    }
}