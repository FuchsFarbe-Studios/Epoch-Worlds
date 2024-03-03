// EpochWorlds
// FieldType.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Shared.Utils;

namespace EpochApp.Shared.Config
{
    public enum FieldType
    {
        [Description("Text")]
        Text,

        [Description("Text Area")]
        TextArea,

        [Description("Numeric")]
        Number,

        [Description("Date")]
        Date,

        [Description("Time")]
        Time,

        [Description("Email")]
        Email,

        [Description("Phone")]
        Phone,

        [Description("Image")]
        Image,

        [Description("Bool")]
        Boolean,

        [Description("Select Item")]
        Select,

        [Description("Select Many")]
        MultiSelect,

        [Description("Radio")]
        Radio,

        [Description("Check Box")]
        Checkbox,
    }
}