// EpochWorlds
// UserProfileDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
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
        public DateTime? MemberDate { get; set; }
        public List<UserSocialDTO> Socials { get; set; } = new List<UserSocialDTO>();
        public List<ArticleDTO> UserArticles { get; set; } = new List<ArticleDTO>();
        public List<WorldDTO> UserWorlds { get; set; } = new List<WorldDTO>();
    }
}