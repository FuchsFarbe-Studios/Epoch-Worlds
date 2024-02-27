using System.ComponentModel.DataAnnotations;

namespace EpochApp.Shared;

public class ManuscriptDTO
{
    public Guid UserID { get; set; }
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    public string CoverArt { get; set; }
    public string Summary { get; set; }
    public List<ChapterDTO> Chapters { get; set; } = new List<ChapterDTO>();
}

public class ChapterDTO
{
    public long ChapterId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}