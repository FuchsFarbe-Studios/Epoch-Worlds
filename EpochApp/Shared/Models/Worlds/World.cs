// EpochWorlds
// World.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Users;

namespace EpochApp.Shared
{
    /// <summary>
    ///     A world is a collection of world-related data that is owned by a user. It is a container for all the data that is
    ///     related to a specific world.
    /// </summary>
    public class World
    {
        public World()
        {
            MetaData = new HashSet<WorldMeta>();
            WorldArticles = new HashSet<Article>();
            WorldTags = new HashSet<WorldTag>();
            WorldFiles = new HashSet<UserFile>();
            WorldGenres = new HashSet<WorldGenre>();
        }

        /// <summary>
        ///     The user that owns this world.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        ///     Unique identifier for this world.
        /// </summary>
        public Guid WorldId { get; set; }

        /// <summary> The name of the world. </summary>
        public string WorldName { get; set; }

        /// <summary>
        ///     The pronunciation of the <see cref="WorldName" />.
        /// </summary>
        public string Pronunciation { get; set; }

        /// <summary>
        ///     The short blurb to be used when linked to social media sites.
        /// </summary>
        public string Excerpt { get; set; }

        /// <summary>
        ///     The image to display when the world is viewed.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///     The header to display when the world is viewed.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        ///     The sub-header to display when the world is viewed.
        /// </summary>
        public string SubHeader { get; set; }

        /// <summary>
        ///     A brief description of the world.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Language this world's data is written in.
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        ///     What the followers of this world are called, singularly.
        /// </summary>
        public string FollowerNamingSingular { get; set; }

        /// <summary>
        ///     What the followers of this world are called, plurally.
        /// </summary>
        public string FollowerNamingPlural { get; set; }

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

        /// <summary>
        ///     Articles associated with this world.
        /// </summary>
        public virtual ICollection<Article> WorldArticles { get; set; }

        /// <summary>
        ///     Tags associated with this world.
        /// </summary>
        public virtual ICollection<WorldTag> WorldTags { get; set; }

        /// <summary>
        ///     Files associated with this world.
        /// </summary>
        public virtual ICollection<UserFile> WorldFiles { get; set; }

        /// <summary>
        ///     Genres associated with this world.
        /// </summary>
        public virtual ICollection<WorldGenre> WorldGenres { get; set; }
    }

}