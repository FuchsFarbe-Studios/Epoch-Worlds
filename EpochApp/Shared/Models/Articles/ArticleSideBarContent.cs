// EpochWorlds
// ArticleSideBarContent.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class ArticleSideBarContent
    {
        public Guid ArticleId { get; set; }
        public string SideBarTop { get; set; }
        public string SideBarTopContent { get; set; }
        public string SideBarBottom { get; set; }
        public string SideBarBottomContent { get; set; }
        public bool DisplaySidebar { get; set; } = true;
        public virtual Article Article { get; set; }
    }
}