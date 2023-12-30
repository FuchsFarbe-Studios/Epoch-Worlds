// EpochWorlds
// GeneratedContent.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 29-12-2023
using EpochApp.Shared.Users;

namespace EpochApp.Shared.Services
{
    public abstract class GeneratedContent
    {
        public Guid ContentID { get; set; }
        public Guid OwnerID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public User Owner { get; set; }
    }
}