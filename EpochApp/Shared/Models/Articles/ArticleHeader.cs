// EpochWorlds
// ArticleHeader.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class ArticleHeader
    {
        public Guid ArticleId { get; set; }
        public string SubHeading { get; set; }
        public string Credits { get; set; }
        public virtual Article Article { get; set; }
    }
}