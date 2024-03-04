// EpochWorlds
// Profile.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared.Users
{
    /// <summary>
    /// Definition of a user profile.
    /// </summary>
    public class Profile
    {
        public Profile()
        {
            Socials = new HashSet<UserSocial>();
        }

        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomContent { get; set; }
        public string Signature { get; set; }
        public string CommunitySignature { get; set; }
        public string AvatarImg { get; set; }
        public string AvatarImgAlt { get; set; }
        public string CoverImg { get; set; }
        public string CoverImgAlt { get; set; }
        public string WebAddress { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserSocial> Socials { get; set; }
    }

}