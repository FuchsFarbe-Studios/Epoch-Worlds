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
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Signature { get; set; }
        public string AvatarImg { get; set; }
        public string CoverImg { get; set; }
        public string WebAddress { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<SocialDTO> Socials { get; set; }
    }
}