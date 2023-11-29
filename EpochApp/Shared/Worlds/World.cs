// EpochWorlds
// World.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Worlds
{
    public class WorldAuthor
    {
        public Guid AuthorID { get; set; }
        public string AuthorName { get; set; }
    }

    public class World
    {
        public Guid WorldID { get; set; }
        public string WorldName { get; set; }
        public string Description { get; set; }
    }
}