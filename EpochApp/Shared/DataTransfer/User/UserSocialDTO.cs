// EpochWorlds
// UserSocialDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class UserSocialDTO
    {
        public int SocialID { get; set; }
        public Guid UserID { get; set; }
        public string SocialHandle { get; set; }
    }
}