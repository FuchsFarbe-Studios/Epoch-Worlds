// EpochWorlds
// RandomAffixType.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Shared.Utils;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared
{
    /// <summary>
    ///     A conlangs affix creation type.
    /// </summary>
    public enum RandomAffixType
    {
        [Description("Random")]
        Random = 0,

        [Description("Prefix")]
        Prefix = 1,

        [Description("Suffix")]
        Suffix = 2,
        Either = 3
    }
}