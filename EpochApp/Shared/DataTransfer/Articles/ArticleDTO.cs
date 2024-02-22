// EpochWorlds
// ArticleDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for articles.
    /// </summary>
    public class ArticleDTO
    {
        public Guid ArticleId { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? WorldId { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public IEnumerable<SectionDTO> Sections { get; set; } = new List<SectionDTO>();
    }

}