// EpochWorlds
// ClientSettingDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class ClientSettingDTO
    {
        public string SettingGroup { get; set; }
        public string Setting { get; set; }
        public string SettingVal { get; set; }
        public int? FieldId { get; set; }
    }
}