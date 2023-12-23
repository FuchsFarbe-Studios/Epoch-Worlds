// EpochWorlds
// World.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Users;

namespace EpochApp.Shared.Worlds
{
    public class World
    {
        public Guid OwnerID { get; set; }
        public Guid WorldID { get; set; }
        public String WorldName { get; set; }
        public String Pronunciation { get; set; }
        public String Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }

        public virtual User Owner { get; set; }
        public virtual WorldDate CurrentWorldDate { get; set; }
        public ICollection<WorldMeta> MetaData { get; set; }
    }

}