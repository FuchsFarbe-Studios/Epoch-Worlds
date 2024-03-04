// EpochWorlds
// Blog.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// Blog.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    /// <summary> A blog entity. </summary>
    public class Blog
    {
        public Blog()
        {
            BlogPosts = new HashSet<BlogPost>();
        }

        /// <summary> Unique blog id. </summary>
        public int BlogID { get; set; }

        /// <summary> This blog's type. </summary>
        public BlogType BlogType { get; set; }

        /// <summary> The name of the blog. </summary>
        public string Name { get; set; }

        /// <summary>
        ///     When the blog was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     The username of the blog creator.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        ///     When the blog was last modified.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     The username of the last person to modify the blog.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary> The blog's posts. </summary>
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}