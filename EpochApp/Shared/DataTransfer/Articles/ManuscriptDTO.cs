using System.ComponentModel.DataAnnotations;

namespace EpochApp.Shared;

public class ManuscriptDTO
{
    public Guid UserID { get; set; }
    [Required] [MaxLength(255)]
    public string Title { get; set; }

    public string CoverArt { get; set; }

    [MaxLength(1000)]
    public string Summary { get; set; }

    public List<ManuscriptChapterDTO> Chapters { get; set; } = new List<ManuscriptChapterDTO>();
}