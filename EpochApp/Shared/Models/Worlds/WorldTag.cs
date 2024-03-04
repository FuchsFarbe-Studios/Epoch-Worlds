// EpochWorlds
// WorldTag.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class WorldTag
    {
        public Guid WorldId { get; set; }
        public virtual World World { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}