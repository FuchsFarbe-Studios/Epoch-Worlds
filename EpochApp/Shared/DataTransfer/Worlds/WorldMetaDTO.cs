// EpochWorlds
// WorldMetaDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
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