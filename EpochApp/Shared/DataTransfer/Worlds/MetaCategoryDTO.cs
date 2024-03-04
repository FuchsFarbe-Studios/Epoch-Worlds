// EpochWorlds
// MetaCategoryDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class MetaCategoryDTO
    {
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public string CategoryInfo { get; set; }

        public List<MetaDTO> MetaData { get; set; } = new List<MetaDTO>();
    }
}