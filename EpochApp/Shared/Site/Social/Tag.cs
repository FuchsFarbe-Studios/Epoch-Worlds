// EpochWorlds
// Tag.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
using EpochApp.Shared.Articles;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;

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

    public class ArticleTag
    {
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }

    public class UserTag
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }

    public class WorldTag
    {
        public Guid WorldId { get; set; }
        public virtual World World { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }

}