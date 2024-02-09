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
        public Guid ContentID { get; set; }
        public ContentType ContentType { get; set; }
        public string ContentXml { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }

        public Guid? AuthorID { get; set; }
        public virtual User? Author { get; set; }

        public Guid? WorldID { get; set; }
        public virtual World? World { get; set; }
    }
}