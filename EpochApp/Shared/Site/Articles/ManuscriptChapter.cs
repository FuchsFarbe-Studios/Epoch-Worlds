// EpochWorlds
// ManuscriptChapter.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
using EpochApp.Shared.Users;

namespace EpochApp.Shared.Articles
{
    /// <summary>
    ///     A chapter of a user's manuscript.
    /// </summary>
    public class ManuscriptChapter
    {
        public Guid UserID { get; set; }
        public long ManuscriptId { get; set; }
        public long ChapterId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual User User { get; set; }
        public virtual Manuscript Manuscript { get; set; }
    }
}