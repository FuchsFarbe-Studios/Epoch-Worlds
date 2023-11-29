// EpochWorlds
// BlogPost.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Blog
{
    public class BlogPost
    {
        public int BlogID { get; set; }
        public Guid PostID { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public Blog Blog { get; set; }
        public Post Post { get; set; }
    }
}