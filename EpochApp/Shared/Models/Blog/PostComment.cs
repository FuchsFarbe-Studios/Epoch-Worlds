// EpochWorlds
// PostComment.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using EpochApp.Shared.Users;

namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class PostComment
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? RemovedOn { get; set; }
        public string RemovedBy { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}