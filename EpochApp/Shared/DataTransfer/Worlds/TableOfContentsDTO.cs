#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class TableOfContentsDTO
    {
        // list of ArticleTocDTOs
        // 
    }

    public class ArticleTocDTO
    {
        public Guid ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string CategoryName  { get; set; }
        public string ParentCategoryName  { get; set; }
        public string IconPath  { get; set; }
    }
}

/* ~/areas
 * ~/areas/cities
 * 
 * ~/characters
 * ~/characters/char1.char
 * ~/characters/char1/description.art/summary.art
 * 
 * ~/cultures
 * ~/cultures/green people
 * ~/cultures/green people/summary.art
 * ~/cultures/green people/traditions.art
 * ~/cultures/green people/religion.art
 * 
 * ~/languages
 * ~/languages/green tongue.lang
 */
