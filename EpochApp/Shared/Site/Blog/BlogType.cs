// EpochWorlds
// BlogTypeInfo.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Utils;

namespace EpochApp.Shared
{
    public enum BlogType
    {
        [Description("General")]
        GENERAL = 0,

        [Description("News")]
        NEWS = 1,

        [Description("Updates")]
        UPDATES = 2,

        [Description("Events")]
        EVENTS = 3,

        [Description("FAQ")]
        FAQ = 4,

        [Description("Documentation")]
        DOCUMENTATION = 5
    }
}