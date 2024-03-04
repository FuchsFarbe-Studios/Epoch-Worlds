// EpochWorlds
// VowelDepth.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared.Utils;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared
{
    public enum VowelDepth
    {
        [Description("Close")]
        Close,

        [Description("Near Close")]
        NearClose,

        [Description("Close Mid")]
        CloseMid,

        [Description("Mid")]
        Mid,

        [Description("Open Mid")]
        OpenMid,

        [Description("Near Open")]
        NearOpen,

        [Description("Open")]
        Open
    }
}