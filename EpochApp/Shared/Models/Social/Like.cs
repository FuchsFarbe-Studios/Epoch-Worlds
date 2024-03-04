// EpochWorlds
// Like.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using EpochApp.Shared.Users;

namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class Like
    {
        public long LikeId { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}