// EpochWorlds
// ArticleSection.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     A section of an article.
    /// </summary>
    /// <remarks>
    ///     Contains additional information displayed in sections that relate to an article.
    /// </remarks>
    public class ArticleSection
    {
        /// <summary>
        ///     Unique identifier for this article section.
        /// </summary>
        public Guid SectionID { get; set; }

        /// <summary>
        ///     Article this section is associated with.
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary> Title of this section. </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Content of this section.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Date this section was created.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     Article navigation property.
        /// </summary>
        public virtual Article Article { get; set; }
    }
}