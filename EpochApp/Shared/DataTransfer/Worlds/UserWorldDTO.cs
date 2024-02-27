// EpochWorlds
// UserWorldDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
namespace EpochApp.Shared
{
    public class UserWorldDTO
    {
        public Guid OwnerId { get; set; }
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
        ///     The date this world was last viewed.
        /// </summary>
        public WorldDateDTO CurrentWorldDate { get; set; }

        /// <summary>
        ///     MetaData associated with this world.
        /// </summary>
        public virtual ICollection<MetaDTO> MetaData { get; set; } = new List<MetaDTO>();

        /// <summary>
        ///     Articles associated with this world.
        /// </summary>
        public virtual ICollection<ArticleDTO> WorldArticles { get; set; }

        /// <summary>
        ///     Tags associated with this world.
        /// </summary>
        public virtual ICollection<WorldTagDTO> WorldTags { get; set; }

        /// <summary>
        ///     Files associated with this world.
        /// </summary>
        public virtual ICollection<UserFileDTO> WorldFiles { get; set; }

        /// <summary>
        ///     Genres associated with this world.
        /// </summary>
        public virtual ICollection<WorldGenreDTO> WorldGenres { get; set; }
    }
}