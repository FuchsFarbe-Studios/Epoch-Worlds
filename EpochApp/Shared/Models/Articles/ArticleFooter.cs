// EpochWorlds
// ArticleFooter.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class ArticleFooter
    {
        public Guid ArticleId { get; set; }
        public string Footnotes { get; set; }
        public string FooterContent { get; set; }
        public virtual Article Article { get; set; }
    }
}