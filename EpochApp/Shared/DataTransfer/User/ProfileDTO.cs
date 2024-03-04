// EpochWorlds
// UserProfileDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class ProfileDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
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
        public DateTime? DateOfBirth { get; set; }
        public DateTime MemberSince { get; set; }
        public List<UserSocialDTO> Socials { get; set; } = new List<UserSocialDTO>();
        public List<ArticleDTO> UserArticles { get; set; } = new List<ArticleDTO>();
        public List<WorldDTO> UserWorlds { get; set; } = new List<WorldDTO>();
    }
}