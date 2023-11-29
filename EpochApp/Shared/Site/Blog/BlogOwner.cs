// EpochWorlds
// BlogOwners.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Users;

namespace EpochApp.Shared.Blog
{
    public class BlogOwner
    {
        public int BlogID { get; set; }
        public Guid OwnerID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? RemovedOn { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual User Owner { get; set; }
    }
}