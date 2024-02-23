// EpochWorlds
// ClientSetting.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
namespace EpochApp.Shared.Client
{
    /// <summary>
    ///     Settings for different areas of the site.
    /// </summary>
    public class ClientSetting
    {
        /// <summary> The setting id. </summary>
        public int SettingId { get; set; }

        /// <summary> The setting name. </summary>
        public string FieldName { get; set; }

        /// <summary> The setting value. </summary>
        public string SettingValue { get; set; }

        /// <summary> The setting field id. </summary>
        public int SettingFieldId { get; set; }
    }

}