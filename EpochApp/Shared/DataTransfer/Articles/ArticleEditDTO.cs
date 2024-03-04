// EpochWorlds
// ArticleEditDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for articles that need to be edited.
    /// </summary>
    public class ArticleEditDTO
    {
        public ArticleEditDTO()
        {
        }

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

        public Guid? BuilderId { get; set; }

        /// <summary>
        ///     The id of the article's category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        ///     The title of this article.
        /// </summary>
        [MaxLength(255)]
        public string? Title { get; set; }

        /// <summary>
        /// Subtitle of this article.
        /// </summary>
        [MaxLength(255)]
        public string? SubTitle { get; set; }

        /// <summary>
        ///     Content of this article.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Short summary of the article.
        /// </summary>
        [MaxLength(500)]
        public string? Excerpt { get; set; }

        /// <summary>
        /// Snippet that displays when the mouse is hovered over and article link.
        /// </summary>
        [MaxLength(500)]
        public string MouseOverSnippet { get; set; }

        /// <summary>
        ///     Generated article content will go here.
        /// </summary>
        public string? GeneratedContentXml { get; set; }

        /// <summary>
        ///     Determines the type of content to generate.
        /// </summary>
        public ContentType? ContentType { get; set; }

        /// <summary>
        /// FontAwesome class for the article icon.
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// Cover image of the article.
        /// </summary>
        [MaxLength(255)]
        public string CoverImage { get; set; }

        /// <summary>
        /// Alt text for the cover image.
        /// </summary>
        [MaxLength(500)]
        public string CoverImageAlt { get; set; }

        /// <summary>
        ///     Published articles are viewable by other users.
        /// </summary>
        public bool IsPublished { get; set; } = false;

        /// <summary>
        ///    Indicates if this article is public.
        /// </summary>
        public bool IsPublic { get; set; } = false;

        /// <summary>
        ///    Indicates if this article is a work in progress.
        /// </summary>
        public bool IsWorkInProgress { get; set; } = false;

        /// <summary>
        ///     Indicates if this article is not safe for work.
        /// </summary>
        public bool IsNSFW { get; set; } = false;

        /// <summary>
        ///     Display the author of this article.
        /// </summary>
        public bool DisplayAuthor { get; set; } = false;

        /// <summary>
        ///    Allow comments on this article.
        /// </summary>
        public bool AllowComments { get; set; } = false;

        /// <summary>
        ///   Allow copy of this article.
        /// </summary>
        public bool AllowCopy { get; set; } = false;

        /// <summary>
        ///     Show this article in the table of contents.
        /// </summary>
        public bool ShowInTableOfContents { get; set; } = false;

        /// <summary>
        ///     Show the table of content for this article.
        /// </summary>
        public bool ShowTableOfContents { get; set; } = false;

        /// <summary>
        ///     ID of the template used to generate this article.
        /// </summary>
        public int? TemplateId { get; set; }

        /// <summary>
        ///    Header navigation property.
        /// </summary>
        public virtual ArticleHeaderDTO Header { get; set; } = new ArticleHeaderDTO();

        /// <summary>
        ///   SideBar navigation property.
        /// </summary>
        public virtual SideBarDTO SideBar { get; set; } = new SideBarDTO();

        /// <summary>
        ///   Footer navigation property.
        /// </summary>
        public virtual ArticleFooterDTO Footer { get; set; } = new ArticleFooterDTO();

        public List<SectionEditDTO> Sections { get; set; } = new List<SectionEditDTO>();
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}