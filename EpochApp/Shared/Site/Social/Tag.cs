// EpochWorlds
// Tag.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
namespace EpochApp.Shared.Social
{
    public class Tag
    {
        public Tag()
        {
            ArticleTags = new HashSet<ArticleTag>();
            UserTags = new HashSet<UserTag>();
            WorldTags = new HashSet<WorldTag>();
        }

        public long TagId { get; set; }
        public string Text { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<UserTag> UserTags { get; set; }
        public virtual ICollection<WorldTag> WorldTags { get; set; }
    }

}