// EpochWorlds
// EpochState.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochComponents.Utils;

namespace EpochComponents.Enums
{
    public enum EpochState
    {
        [Description("")]
        None,
        [Description("style-success")]
        Success,
        [Description("style-info")]
        Info,
        [Description("style-warning")]
        Warning,
        [Description("style-danger")]
        Danger,
        [Description("style-success-light")]
        LightSuccess,
        [Description("style-info-light")]
        LightInfo,
        [Description("style-warning-light")]
        LightWarning,
        [Description("style-danger-light")]
        LightDanger,
    }
}