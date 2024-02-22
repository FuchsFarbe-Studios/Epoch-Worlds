// EpochWorlds
// ArticleEditDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using System.ComponentModel.DataAnnotations;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for articles that need to be edited.
    /// </summary>
    public class ArticleEditDTO
    {
        public Guid? ArticleId { get; set; }
        public Guid? WorldId { get; set; }
        public Guid? AuthorId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; } = false;
        public int CategoryId { get; set; } = 0;
        public IEnumerable<SectionEditDTO> Sections { get; set; } = new List<SectionEditDTO>();
    }
}