// EpochWorlds
// ReportType.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using EpochApp.Shared.Utils;

namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public enum ReportType
    {
        [Description("Hate Speech")]
        HateSpeech = 0,

        [Description("Harassment")]
        Harassment = 1,

        [Description("Spam")]
        Spam = 2,

        [Description("Inappropriate Content")]
        InappropriateContent = 3,

        [Description("Other (Please Specify)")]
        Other = 4
    }
}