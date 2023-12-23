// EpochWorlds
// UserSocial.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// UserSocial.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// UserSocial.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// UserSocial.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Config;

namespace EpochApp.Shared.Users
{
    public class UserSocial
    {
        public Int32 SocialID { get; set; }
        public Guid UserID { get; set; }
        public String SocialHandle { get; set; }

        public virtual SocialMedia Social { get; set; }
        public virtual Profile Profile { get; set; }
    }
}