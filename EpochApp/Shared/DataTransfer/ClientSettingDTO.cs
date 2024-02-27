// EpochWorlds
// ClientSettingDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
namespace EpochApp.Shared
{
    public class ClientSettingDTO
    {
        public string SettingGroup { get; set; }
        public string? Setting { get; set; }
        public string? SettingVal { get; set; }
        public int? FieldId { get; set; }
    }
}