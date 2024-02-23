// EpochWorlds
// ConsonantPlace.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared.Utils;

namespace EpochApp.Shared.Config
{
    public enum ConsonantPlace
    {
        [Description("Bilabial")]
        Bilabial,

        [Description("Labio-Dental")]
        LabioDental,

        [Description("Dental")]
        Dental,

        [Description("Alveolar")]
        Alveolar,

        [Description("Post-Alveolar")]
        PostAlveolar,

        [Description("Retroflex")]
        Retroflex,

        [Description("Palatal")]
        Palatal,

        [Description("Velar")]
        Velar,

        [Description("Uvular")]
        Uvular,

        [Description("Pharyngeal")]
        Pharyngeal,

        [Description("Glottal")]
        Glottal
    }
}