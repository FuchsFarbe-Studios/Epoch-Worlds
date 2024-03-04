// EpochWorlds
// Blog.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared
{
    #pragma warning disable CS1591

    public class Blog
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
        public virtual ICollection<Post> Posts { get; set; }
    }
}