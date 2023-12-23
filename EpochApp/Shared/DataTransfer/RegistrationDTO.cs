// EpochWorlds
// RegistrationDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    public class RegistrationDTO
    {
        public String Email { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Password2 { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Boolean TermAgreement { get; set; } = false;
    }
}