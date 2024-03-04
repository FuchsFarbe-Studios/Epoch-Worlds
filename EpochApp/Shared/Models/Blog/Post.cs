// EpochWorlds
// Post.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    public class Post
    {
        public Post()
        {
            BlogPosts = new HashSet<BlogPost>();
        }

        /// <summary> Unique post id. </summary>
        public Guid PostID { get; set; }

        /// <summary> The type of post. </summary>
        public PostType PostType { get; set; }

        /// <summary>
        ///     The author of the post.
        /// </summary>
        public string Author { get; set; }

        /// <summary> The title of the post. </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     The content of the post.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     The outside link for this post.
        /// </summary>
        public string? OutsideLink { get; set; }

        /// <summary>
        ///     The date the post is scheduled to be posted.
        /// </summary>
        public DateTime? ScheduledTime { get; set; }

        /// <summary>
        ///     The date the post was posted.
        /// </summary>
        public DateTime? PostedOn { get; set; }

        /// <summary>
        ///     The date the post was last modified.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     The username of the last person to modify the post.
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        ///     The blog posts that reference this post.
        /// </summary>
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}