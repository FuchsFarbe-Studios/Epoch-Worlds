// EpochWorlds
// RegistrationDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    public class RegistrationDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Month { get; set; } = "01";
        public string Day { get; set; } = "01";
        public string Year { get; set; } = "1990";
        public DateTime? DateOfBirth { get; set; }
        public bool TermAgreement { get; set; } = false;
    }
}