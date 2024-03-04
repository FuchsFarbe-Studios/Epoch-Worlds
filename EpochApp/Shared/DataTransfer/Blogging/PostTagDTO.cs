// EpochWorlds
// PostTagDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
#pragma warning disable CS1591
namespace EpochApp.Shared
{
    public class PostTagDTO
    {
        public Guid PostId { get; set; }
        public long TagId { get; set; }
        public TagDTO Tag { get; set; }
    }
}