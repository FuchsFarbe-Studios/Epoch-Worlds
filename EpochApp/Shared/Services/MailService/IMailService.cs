// EpochWorlds
// IMailService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
namespace EpochApp.Shared.Services
{
    public interface IMailService
    {
        Task SendEmail(string toEmail, string subject, string content);
    }

}