// EpochWorlds
// WordOrderType.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Shared.Utils;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Conlangs word order structuring.
    /// </summary>
    public enum WordOrderType
    {
        [Description("Random")]
        Random = 0,

        [Description("Subject-Verb-Object (SVO)")]
        SVO = 1,

        [Description("Subject-Object-Verb (SOV)")]
        SOV = 2,

        [Description("Verb-Subject-Object (VSO)")]
        VSO = 3,

        [Description("Verb-Object-Subject (VOS)")]
        VOS = 4,

        [Description("Object-Verb-Subject (OVS)")]
        OVS = 5,

        [Description("Object-Subject-Verb (OSV)")]
        OSV = 6
    }
}