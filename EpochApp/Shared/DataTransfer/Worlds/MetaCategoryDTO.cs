// EpochWorlds
// MetaCategoryDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
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