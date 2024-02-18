// EpochWorlds
// AdjectiveOrderType.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Shared.Utils;

namespace EpochApp.Shared
{
    /// <summary>
    ///     A conlangs adjective construction.
    /// </summary>
    public enum AdjectiveOrderType
    {
        [Description("Random")]
        Random = 0,

        [Description("Before the Noun")]
        BeforeNoun = 1,

        [Description("After the Noun")]
        AfterNoun = 2
    }
}