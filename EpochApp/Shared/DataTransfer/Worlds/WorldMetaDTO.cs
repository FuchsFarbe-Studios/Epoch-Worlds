// EpochWorlds
// WorldMetaDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
namespace EpochApp.Shared
{
    public class WorldMetaDTO
    {
        public Guid WorldId { get; set; }
        public int TemplateId { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }
    }
}