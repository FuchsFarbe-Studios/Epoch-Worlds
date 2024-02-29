// EpochWorlds
// WorldGenre.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
using EpochApp.Shared.Config;

namespace EpochApp.Shared.Worlds
{
    /// <summary>
    ///     Describes a relationship with a world and a genre.
    /// </summary>
    public class WorldGenre
    {
        public Guid WorldID { get; set; }
        public int GenreID { get; set; }

        public virtual World World { get; set; }
        public virtual Genre Genre { get; set; }
    }
}