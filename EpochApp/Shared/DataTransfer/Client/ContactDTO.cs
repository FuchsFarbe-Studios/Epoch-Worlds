// EpochWorlds
// ContactDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 17-2-2024
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared
{
    public class ContactDTO
    {
        /// <summary>
        ///     The name of the contact.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string UserName { get; set; }

        /// <summary>
        ///     The email of the contact.
        /// </summary>
        [Required]
        [EmailAddress]
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
        [Required]
        [MaxLength(1000)]
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