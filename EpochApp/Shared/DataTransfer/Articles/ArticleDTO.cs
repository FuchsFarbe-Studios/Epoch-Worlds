// EpochWorlds
// ArticleDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for articles.
    /// </summary>
    public class ArticleDTO
    {
        /// <summary>
        ///     The unique identifier for this article.
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        ///     The unique identifier for the world this article belongs to.
        /// </summary>
        public Guid? WorldId { get; set; }

        /// <summary>
        ///     The unique identifier for the author of this article.
        /// </summary>
        public Guid? AuthorId { get; set; }

        /// <summary>
        ///     The name of the author of this article.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     The id of the article's category.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        ///     The title of this article.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     The content of this article.
        /// </summary>
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
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     The sections of this article.
        /// </summary>
        public List<SectionDTO> Sections { get; set; } = new List<SectionDTO>();

        /// <summary>
        ///     The tags associated with this article.
        /// </summary>
        public List<ArticleTagDTO> ArticleTags { get; set; } = new List<ArticleTagDTO>();
    }

}