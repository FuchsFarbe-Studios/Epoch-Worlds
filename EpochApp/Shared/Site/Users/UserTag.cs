// EpochWorlds
// UserTag.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Shared.Users;

namespace EpochApp.Shared.Social
{
    public class UserTag
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}