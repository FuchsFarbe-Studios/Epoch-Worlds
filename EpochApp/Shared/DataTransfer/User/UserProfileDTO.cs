// EpochWorlds
// UserProfileDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
namespace EpochApp.Shared
{

    public class UserProfileDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public int WorldCount { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public int ArticleCount { get; set; }
        public List<UserSocialDTO> Socials { get; set; } = new List<UserSocialDTO>();
    }
}