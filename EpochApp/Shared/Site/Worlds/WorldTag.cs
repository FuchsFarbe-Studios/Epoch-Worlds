// EpochWorlds
// WorldTag.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Shared.Social;

namespace EpochApp.Shared.Worlds
{
    public class WorldTag
    {
        public Guid WorldId { get; set; }
        public virtual World World { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}