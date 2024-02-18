// EpochWorlds
// BlogDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
namespace EpochApp.Shared
{
    public class BlogDTO
    {
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
    }
}