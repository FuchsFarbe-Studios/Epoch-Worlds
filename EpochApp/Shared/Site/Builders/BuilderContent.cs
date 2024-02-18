// EpochWorlds
// BuilderContent.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 9-2-2024
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Stores the content of a builder.
    /// </summary>
    public class BuilderContent
    {
        /// <summary>
        ///     Unique content identifier.
        /// </summary>
        public Guid ContentID { get; set; }

        /// <summary>
        ///     Author of this content.
        /// </summary>
        public Guid? AuthorID { get; set; }

        /// <summary>
        ///     World this content is associated with.
        /// </summary>
        public Guid? WorldID { get; set; }

        /// <summary> Type of content. </summary>
        public ContentType ContentType { get; set; }

        /// <summary> Serialized content. </summary>
        public string ContentXml { get; set; }

        /// <summary>
        ///     Serialized content that has been generated.
        /// </summary>
        public string GeneratedXml { get; set; }

        /// <summary>
        ///     Date this content was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///     Date this content was last modified.
        /// </summary>
        public DateTime? DateModified { get; set; }

        /// <summary>
        ///     Date this content was removed.
        /// </summary>
        public DateTime? DateRemoved { get; set; }

        /// <summary>
        ///     Author property of this content.
        /// </summary>
        public virtual User? Author { get; set; }

        /// <summary>
        ///     World property of this content.
        /// </summary>
        public virtual World? World { get; set; }
    }
}