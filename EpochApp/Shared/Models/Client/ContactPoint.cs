// EpochWorlds
// ContactPoint.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 17-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Describes a point of contact from an end user to the server.
    /// </summary>
    /// <remarks>
    ///     Represents a contact form.
    /// </remarks>
    public class ContactPoint
    {
        /// <summary>
        ///     The unique identifier of the contact point.
        /// </summary>
        public long ContactPointId { get; set; }

        /// <summary>
        ///     The name of the contact.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        ///     The email of the contact.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Phone number to contact the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary> Type of contact form. </summary>
        public ContactType ContactType { get; set; }

        /// <summary>
        ///     The message of the contact.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Date this contact form was created on.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     Date this was resolved. Null if not resolved.
        /// </summary>
        public DateTime? ResolvedOn { get; set; }
    }
}