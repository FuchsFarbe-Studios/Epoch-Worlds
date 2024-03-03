// EpochWorlds
// Article.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 26-12-2023
using EpochApp.Shared.Config;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;

namespace EpochApp.Shared.Articles
{
    /// <summary>
    ///     Represents a user-created article for a world.
    /// </summary>
    public class Article
    {
        public Article()
        {
            Sections = new HashSet<ArticleSection>();
            ArticleTags = new HashSet<ArticleTag>();
            Meta = new HashSet<ArticleMeta>();
        }

        /// <summary>
        ///     Unique identifier for this article.
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        ///     Author of this article.
        /// </summary>
        public Guid? AuthorId { get; set; }

        /// <summary>
        ///     World this article is associated with.
        /// </summary>
        public Guid? WorldId { get; set; }

        /// <summary> Article category. </summary>
        public int? CategoryId { get; set; }

        /// <summary> Title of this article. </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Content of this article.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     The builder content associated with this article.
        /// </summary>
        public Guid? BuilderId { get; set; }

        /// <summary>
        ///     Generated article content will go here.
        /// </summary>
        public string? GeneratedContentXml { get; set; }

        /// <summary>
        ///     Determines the type of content to generate.
        /// </summary>
        public ContentType? ContentType { get; set; }

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

        /// <summary>
        ///     ID of the template used to generate this article.
        /// </summary>
        public int? TemplateId { get; set; }

        /// <summary>
        ///     Date this article was created.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     Date this article was last modified.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     Date this article was deleted.
        /// </summary>
        public DateTime? DeletedOn { get; set; }

        /// <summary>
        ///     Author navigation property.
        /// </summary>
        public virtual User? Author { get; set; }

        /// <summary>
        ///     World navigation property.
        /// </summary>
        public virtual World? World { get; set; }

        /// <summary>
        ///     Category navigation property.
        /// </summary>
        public virtual ArticleCategory? Category { get; set; }

        /// <summary>
        ///     The builder content associated with this article.
        /// </summary>
        public virtual BuilderContent Builder { get; set; }

        // /// <summary>
        // ///   Template navigation property.
        // /// </summary>
        //public virtual ArticleTemplate Template { get; set; }

        /// <summary>
        ///     Sections navigation property.
        /// </summary>
        /// <remarks>
        ///     This contains any sections this article may have.
        /// </remarks>
        public virtual ICollection<ArticleSection> Sections { get; set; }

        /// <summary>
        /// Tags navigation property.
        /// </summary>
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }

        /// <summary>
        ///     Meta navigation property.
        /// </summary>
        public virtual ICollection<ArticleMeta> Meta { get; set; }
    }

}