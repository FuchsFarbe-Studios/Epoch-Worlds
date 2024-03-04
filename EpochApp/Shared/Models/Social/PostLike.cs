// EpochWorlds
// PostLike.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class PostLike : Like
    {
        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}