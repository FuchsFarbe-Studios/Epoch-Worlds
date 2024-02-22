// EpochWorlds
// RegistrationDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for user registration.
    /// </summary>
    public class RegistrationDTO
    {
        /// <summary>
        ///     The users email address.
        /// </summary>
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool TermAgreement { get; set; } = false;
        public string WorldName { get; set; }
    }
}