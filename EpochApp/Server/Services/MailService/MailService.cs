// EpochWorlds
// MailService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared.Client;
using EpochApp.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace EpochApp.Server.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailConfig;
        private readonly EpochDataDbContext _context;

        public MailService(MailSettings mailConfig, EpochDataDbContext context)
        {
            _mailConfig = mailConfig;
            _context = context;
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

        /// <inheritdoc />
        public async Task SendVerificationEmailAsync(string email, string userName, string token)
        {
            var emailTemplate = await _context.EmailTemplates.Where(x => x.TemplateId == EmailTemplateType.AccountVerification).FirstOrDefaultAsync();
            if (emailTemplate == null)
                return;

            var subject = emailTemplate.Subject;
            subject = subject.Replace("{USER}", userName);
            var body = emailTemplate.HtmlBody;
            body = body.Replace("{TOKEN}", token);
            body = body.Replace("{USER}", userName);
            await SendEmail(email, subject, body);
        }
    }
}