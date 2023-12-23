// EpochWorlds
// Profile.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// Profile.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// Profile.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// Profile.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Users
{
    public class Profile
    {
        public Profile()
        {
            Socials = new HashSet<UserSocial>();
        }

        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Signature { get; set; }
        public string AvatarImg { get; set; }
        public string CoverImg { get; set; }
        public string WebAddress { get; set; }
        public virtual User User { get; set; }

        public ICollection<UserSocial> Socials { get; set; }
    }
}