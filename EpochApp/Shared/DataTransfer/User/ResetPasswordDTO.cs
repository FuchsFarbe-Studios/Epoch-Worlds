// EpochWorlds
// ResetPasswordDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
namespace EpochApp.Shared
{
    public class ResetPasswordDTO
    {
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
    }
}