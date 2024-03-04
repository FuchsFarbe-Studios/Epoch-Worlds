// EpochWorlds
// ArticleTag.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
namespace EpochApp.Shared
{
    public class ArticleTag
    {
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}