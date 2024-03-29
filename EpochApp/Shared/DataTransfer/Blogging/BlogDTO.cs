// EpochWorlds
// BlogDto.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
#pragma warning disable CS1591
namespace EpochApp.Shared
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string RemovedBy { get; set; }
        public string RemovedReason { get; set; }
        public virtual List<PostDTO> Posts { get; set; } = new List<PostDTO>();
    }
}