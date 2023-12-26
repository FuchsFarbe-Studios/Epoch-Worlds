// EpochWorlds
// Manual.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 24-12-2023
using EpochApp.Shared.Users;

namespace EpochApp.Shared.Manuals
{

    /// <summary>
    ///     Base class for all manuals.
    /// </summary>
    public class Manual
    {
        /// <summary>
        ///     Identifier for a Manual.
        /// </summary>
        public int ManualID { get; set; }
        /// <summary> Name of a Manual. </summary>
        public string ManualName { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    ///     A manual that is authored by a user.
    /// </summary>
    /// <remarks>
    ///     Ties a specific manual's data to a user.
    /// </remarks>
    public class AuthoredManual
    {
        /// <summary>
        ///     User that generated the content.
        /// </summary>
        public Guid AuthorID { get; set; }

        /// <summary>
        ///     The unique identifier for this content instance.
        /// </summary>
        public Guid ContentID { get; set; }

        /// <summary>
        ///     When this manual was created.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     When this manual was last modified.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     When this manual was deleted.
        /// </summary>
        public DateTime? DeletedOn { get; set; }

        /// <summary>
        ///     Navigation property for the related Author (User).
        /// </summary>
        public virtual User Author { get; set; }
    }

    public class LanguageManual : AuthoredManual
    {
    }
}