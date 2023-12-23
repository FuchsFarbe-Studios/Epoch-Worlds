// EpochWorlds
// ProfileDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    public class ProfileDTO
    {
        public Guid UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Bio { get; set; }
        public String Signature { get; set; }
        public String AvatarImg { get; set; }
        public String CoverImg { get; set; }
        public String WebAddress { get; set; }

        public List<SocialDTO> Socials { get; set; }
    }
}