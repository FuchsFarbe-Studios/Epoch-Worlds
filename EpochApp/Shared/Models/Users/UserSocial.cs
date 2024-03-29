// EpochWorlds
// UserSocial.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared.Users
{
    public class UserSocial
    {
        public int SocialID { get; set; }
        public Guid UserID { get; set; }
        public string SocialHandle { get; set; }

        public virtual Profile Profile { get; set; }
    }
}