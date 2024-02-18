// EpochWorlds
// Article.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 26-12-2023
namespace EpochApp.Shared.Articles
{
    /// <summary>
    ///     Represents a user-created article for a world.
    /// </summary>
    public class Article
    {
        public Guid ArticleID { get; set; }
        public Guid AuthorID { get; set; }
        public Guid WorldID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}