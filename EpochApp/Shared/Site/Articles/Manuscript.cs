// EpochWorlds
// Manuscript.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
using EpochApp.Shared.Users;

namespace EpochApp.Shared.Articles
{
    /// <summary>
    ///     Represents a written novel or story by a user.
    /// </summary>
    public class Manuscript
    {
        public Manuscript()
        {
            Chapters = new HashSet<ManuscriptChapter>();
        }

        public Guid UserID { get; set; }
        public long ManuscriptId { get; set; }
        public string Title { get; set; }
        public string CoverArt { get; set; }
        public string Summary { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? RemovedOn { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ManuscriptChapter> Chapters { get; set; }
    }

}