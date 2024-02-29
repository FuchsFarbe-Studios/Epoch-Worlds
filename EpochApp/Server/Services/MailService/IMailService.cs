// EpochWorlds
// IMailService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
namespace EpochApp.Server.Services
{
    public interface IMailService
    {
        Task SendEmail(string toEmail, string subject, string content);
        Task SendVerificationEmailAsync(string email, string userName, string token);
        Task SendResetPasswordEmailAsync(string userEmail, string userName, string token);
    }

}