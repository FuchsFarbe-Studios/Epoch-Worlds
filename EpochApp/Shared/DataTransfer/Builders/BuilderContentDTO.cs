// EpochWorlds
// BuilderContentDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
namespace EpochApp.Shared
{
    /// <summary>
    /// Data transfer object for Builder content.
    /// </summary>
    public class BuilderContentDTO
    {
        /// <summary>
        /// Unique content identifier.
        /// </summary>
        public Guid ContentID { get; set; }

        /// <summary>
        /// Author of this content.
        /// </summary>
        public Guid? AuthorID { get; set; }

        /// <summary>
        /// World this content is associated with.
        /// </summary>
        public Guid? WorldID { get; set; }

        /// <summary>
        ///    Name of the content.
        /// </summary>
        public string ContentName { get; set; }

        /// <summary> Type of content. </summary>
        public ContentType ContentType { get; set; }

        /// <summary> Serialized content. </summary>
        public string ContentXml { get; set; }

        /// <summary>
        /// Serialized content that has been generated.
        /// </summary>
        public string GeneratedXml { get; set; }

        /// <summary>
        /// Date this content was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Date this content was last modified.
        /// </summary>
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Date this content was removed.
        /// </summary>
        public DateTime? DateRemoved { get; set; }
    }
}