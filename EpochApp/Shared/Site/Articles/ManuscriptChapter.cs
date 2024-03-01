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
        /// <summary>
        ///     The user who wrote this chapter.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     The manuscript this chapter is associated with.
        /// </summary>
        public long ManuscriptId { get; set; }

        /// <summary>
        ///     Unique identifier for this chapter.
        /// </summary>
        public long ChapterId { get; set; }

        /// <summary> Title of this chapter. </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Content of this chapter.
        /// </summary>

        public string Content { get; set; }

        /// <summary> Order of this chapter. </summary>
        public int? Order { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? RemovedOn { get; set; }

        /// <summary>
        ///     The user who wrote this chapter.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     The manuscript this chapter is associated with.
        /// </summary>
        public virtual Manuscript Manuscript { get; set; }
    }
}