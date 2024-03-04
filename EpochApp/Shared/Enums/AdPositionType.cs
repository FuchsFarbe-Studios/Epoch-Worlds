// EpochWorlds
// AdPositionType.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Shared.Utils;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared
{
    /// <summary>
    ///     A conlangs adposition construction.
    /// </summary>
    public enum AdPositionType
    {
        [Description("Random")]
        Random = 0,

        [Description("Preposition")]
        Preposition = 1,

        [Description("Postposition")]
        Postposition = 2,

        [Description("Circumposition")]
        Circumposition = 3
    }
}