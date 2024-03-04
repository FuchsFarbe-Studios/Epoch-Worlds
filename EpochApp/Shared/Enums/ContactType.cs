// EpochWorlds
// ContactType.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 17-2-2024
using EpochApp.Shared.Utils;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared
{
    public enum ContactType
    {
        [Description("General")]
        General,

        [Description("Bug Report")]
        BugReport,

        [Description("Support")]
        Support,

        [Description("Sales")]
        Sales,

        [Description("Marketing")]
        Marketing,

        [Description("Technical")]
        Technical,

        [Description("Billing")]
        Billing,

        [Description("Legal")]
        Legal,

        [Description("Abuse")]
        Abuse
    }
}