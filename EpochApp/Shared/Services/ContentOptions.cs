// EpochWorlds
// ContentOptions.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 29-12-2023
using EpochApp.Shared.Users;

namespace EpochApp.Shared.Services
{
    /// <summary>
    ///     Abstract class that all content options inherit from.
    /// </summary>
    public abstract class ContentOptions
    {
        public Guid OptionsID { get; set; }
        public Guid OwnerID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }

        public virtual User Owner { get; set; }
    }
}