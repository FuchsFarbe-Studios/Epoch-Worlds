// EpochWorlds
// Tag.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
using EpochApp.Shared.Users;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{

    public class Tag
    {
        public Tag()
        {
            ArticleTags = new HashSet<ArticleTag>();
            UserTags = new HashSet<UserTag>();
            WorldTags = new HashSet<WorldTag>();
            PostTags = new HashSet<PostTag>();
        }

        public long TagId { get; set; }
        public string Text { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<UserTag> UserTags { get; set; }
        public virtual ICollection<WorldTag> WorldTags { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }

}