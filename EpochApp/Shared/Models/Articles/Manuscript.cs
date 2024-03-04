// EpochWorlds
// Manuscript.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
using EpochApp.Shared.Users;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
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

        /// <summary>
        ///     Author of the manuscript.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     Unique identifier for this manuscript.
        /// </summary>
        public long ManuscriptId { get; set; }

        /// <summary>
        ///     Title of this manuscript.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Cover art for this manuscript.
        /// </summary>
        public string CoverArt { get; set; }

        /// <summary>
        ///     Summary of this manuscript.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///     Date this manuscript was created.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     Date this manuscript was last modified.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     Date this manuscript was removed.
        /// </summary>
        public DateTime? RemovedOn { get; set; }

        /// <summary>
        ///     Author of the manuscript.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     Chapters of this manuscript.
        /// </summary>
        public virtual ICollection<ManuscriptChapter> Chapters { get; set; }
    }

}