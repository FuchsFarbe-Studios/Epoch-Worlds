// EpochWorlds
// MailSettings.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared.Utils
{
    public class MailSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string FromEmail { get; set; }
        public string Host { get; set; }
    }
}