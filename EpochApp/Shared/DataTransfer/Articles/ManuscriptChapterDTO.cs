// EpochWorlds
// ManuscriptChapterDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
namespace EpochApp.Shared
{
    public class ManuscriptChapterDTO
    {
        public long ChapterId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Order { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? RemovedOn { get; set; }
    }
}