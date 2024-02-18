// EpochWorlds
// MailService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Shared.Utils;
using System.Net;
using System.Net.Mail;

namespace EpochApp.Shared.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailConfig;

        public MailService(MailSettings mailConfig)
        {
            _mailConfig = mailConfig;
        }

        /// <inheritdoc />
        public async Task SendEmail(string toEmail, string subject, string content)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_mailConfig.FromEmail);
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = content;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient())
            {
                client.Port = _mailConfig.Port;
                client.Host = _mailConfig.Host;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                await client.SendMailAsync(message);
            }
        }
    }
}