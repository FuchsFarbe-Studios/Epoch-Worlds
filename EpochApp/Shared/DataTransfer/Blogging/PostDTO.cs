// EpochWorlds
// PostDto.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
#pragma warning disable CS1591
namespace EpochApp.Shared
{
    public class PostDTO
    {
        public Guid PostId { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string ImageAlt { get; set; }
        public string ExternalLink { get; set; }
        public int Views { get; set; }
        public bool IsPublished { get; set; }
        public PostType Type { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string RemovedBy { get; set; }
        public string RemoveReason { get; set; }
    }
}