// EpochWorlds
// Article.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 26-12-2023
using EpochApp.Shared.Users;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared
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

        /// <summary>
        ///     The builder content associated with this article.
        /// </summary>
        public Guid? BuilderId { get; set; }

        /// <summary>
        ///    Parent article identifier.
        /// </summary>
        public Guid? ParentArticleId { get; set; }// Reference to parent Article

        /// <summary>
        /// Article's category identifier.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// User's category.
        /// </summary>
        public string UserCategory { get; set; }

        /// <summary>
        /// Title of this article.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Subtitle of this article.
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        ///     Content of this article.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Short summary of the article.
        /// </summary>
        public string Excerpt { get; set; }

        /// <summary>
        /// Snippet that displays when the mouse is hovered over and article link.
        /// </summary>
        public string MouseOverSnippet { get; set; }

        /// <summary>
        ///     Generated article content will go here.
        /// </summary>
        public string GeneratedContentXml { get; set; }

        /// <summary>
        ///     Determines the type of content to generate.
        /// </summary>
        public ContentType? ContentType { get; set; }

        /// <summary>
        /// FontAwesome class for the article icon.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Cover image of the article.
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// Alt text for the cover image.
        /// </summary>
        public string CoverImageAlt { get; set; }

        /// <summary>
        ///     Published articles are viewable by other users.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        ///    Indicates if this article is public.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        ///    Indicates if this article is a work in progress.
        /// </summary>
        public bool IsWorkInProgress { get; set; }

        /// <summary>
        ///     Indicates if this article is not safe for work.
        /// </summary>
        public bool IsNSFW { get; set; }

        /// <summary>
        ///     Display the author of this article.
        /// </summary>
        public bool DisplayAuthor { get; set; }

        /// <summary>
        ///    Allow comments on this article.
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        ///   Allow copy of this article.
        /// </summary>
        public bool AllowCopy { get; set; }

        /// <summary>
        ///     Show this article in the table of contents.
        /// </summary>
        public bool ShowInTableOfContents { get; set; }

        /// <summary>
        ///     Show the table of content for this article.
        /// </summary>
        public bool ShowTableOfContents { get; set; }

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
        public virtual User Author { get; set; }

        /// <summary>
        ///     World navigation property.
        /// </summary>
        public virtual World World { get; set; }

        /// <summary>
        ///     Category navigation property.
        /// </summary>
        public virtual ArticleCategory Category { get; set; }

        /// <summary>
        ///    Header navigation property.
        /// </summary>
        public virtual ArticleHeader Header { get; set; }

        /// <summary>
        ///   SideBar navigation property.
        /// </summary>
        public virtual ArticleSideBarContent SideBar { get; set; }

        /// <summary>
        ///   Footer navigation property.
        /// </summary>
        public virtual ArticleFooter Footer { get; set; }

        /// <summary>
        ///    Parent navigation property.
        /// </summary>
        public virtual Article ParentArticle { get; set; }// Navigation property for parent

        /// <summary>
        ///     The builder content associated with this article.
        /// </summary>
        public virtual BuilderContent Builder { get; set; }

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

        /// <summary>
        ///   SubArticles navigation property.
        /// </summary>
        public virtual ICollection<Article> SubArticles { get; set; }
    }

}