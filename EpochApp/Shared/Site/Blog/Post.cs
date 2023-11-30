// EpochWorlds
// Post.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Config;
using EpochApp.Shared.Users;

namespace EpochApp.Shared
{
    public class Post
    {
        public int PostTypeID { get; set; }
        public Guid? AuthorID { get; set; }
        public Guid PostID { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Href { get; set; }
        public string OutsideLink { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public PostType PostType { get; set; }
        public User Author { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}