// EpochWorlds
// ArticleEditDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using System.ComponentModel.DataAnnotations;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for articles that need to be edited.
    /// </summary>
    public class ArticleEditDTO
    {
        public Guid? ArticleId { get; set; }
        public Guid? WorldId { get; set; }
        public Guid? AuthorId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Content { get; set; }

        /// <summary>
        ///     Published articles are viewable by other users.
        /// </summary>
        public bool IsPublished { get; set; } = false;

        /// <summary>
        ///     Indicates if this article is not safe for work.
        /// </summary>
        public bool IsNSFW { get; set; } = false;

        /// <summary>
        ///     Display the author of this article.
        /// </summary>
        public bool DisplayAuthor { get; set; } = false;

        /// <summary>
        ///     Show this article in the table of contents.
        /// </summary>
        public bool ShowInTableOfContents { get; set; } = false;

        /// <summary>
        ///     Show the table of content for this article.
        /// </summary>
        public bool ShowTableOfContents { get; set; } = false;
        public int CategoryId { get; set; } = 0;
        public List<SectionEditDTO> Sections { get; set; } = new List<SectionEditDTO>();
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}