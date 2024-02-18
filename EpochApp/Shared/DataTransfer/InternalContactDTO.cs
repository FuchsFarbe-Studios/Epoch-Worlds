// EpochWorlds
// InternalContactDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 17-2-2024
using EpochApp.Shared.Client;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Used to display contact information for internal use.
    /// </summary>
    public class InternalContactDTO
    {

        /// <summary> The contact point id. </summary>
        public long ContactPointId { get; set; }

        /// <summary> The contact type. </summary>
        public ContactType ContactType { get; set; }

        /// <summary>
        ///     The user that initiated the contact.
        /// </summary>
        public string UserName { get; set; }

        /// <summary> The email of the user. </summary>
        public string Email { get; set; }

        /// <summary>
        ///     The phone number of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     The message sent by the user.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The date the contact was created.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     The date the contact was resolved.
        /// </summary>
        public DateTime? ResolvedOn { get; set; }
    }
}