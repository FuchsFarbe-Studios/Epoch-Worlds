// EpochWorlds
// World.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Users;

namespace EpochApp.Shared.Worlds
{
    /// <summary>
    ///     A world is a collection of world-related data that is owned by a user. It is a container for all the data that is
    ///     related to a specific world.
    /// </summary>
    public class World
    {
        /// <summary>
        ///     The user that owns this world.
        /// </summary>
        public Guid OwnerID { get; set; }

        /// <summary>
        ///     Unique identifier for this world.
        /// </summary>
        public Guid WorldID { get; set; }

        /// <summary> The name of the world. </summary>
        public string WorldName { get; set; }

        /// <summary>
        ///     The pronunciation of the <see cref="WorldName" />.
        /// </summary>
        public string Pronunciation { get; set; }

        /// <summary>
        ///     A brief description of the world.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Determines if this is the active user world.
        /// </summary>
        public bool? IsActiveWorld { get; set; }

        /// <summary>
        ///     The date this world was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///     The date this world was last modified.
        /// </summary>
        public DateTime? DateModified { get; set; }

        /// <summary>
        ///     The date this world was removed or deleted by the owner.
        /// </summary>
        public DateTime? DateRemoved { get; set; }

        /// <summary>
        ///     The user that owns this world.
        /// </summary>
        public virtual User Owner { get; set; }

        /// <summary>
        ///     The current date information of the world.
        /// </summary>
        public virtual WorldDate CurrentWorldDate { get; set; }

        /// <summary>
        ///     The meta information of the world.
        /// </summary>
        public virtual ICollection<WorldMeta> MetaData { get; set; }
    }

}