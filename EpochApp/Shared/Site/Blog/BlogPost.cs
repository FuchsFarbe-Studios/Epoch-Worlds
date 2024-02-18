// EpochWorlds
// BlogPost.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    public class BlogPost
    {
        public int BlogID { get; set; }
        public Guid PostID { get; set; }
        public DateTime? PostedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Post Post { get; set; }
    }
}