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
    // {VERIFICATION-LINK}
    // {VERIFICATION-URL}
    // {PASSWORD-RESET-LINK}
    // {PASSWORD-RESET-URL}
    // {COMMUNITY-LINK}
    // {COMMUNITY-UPDATE}
    // {CONTACT-LINK}
    // {EXPIRATION}
    // {SUPPORT-EMAIL}
    // {SITE-NAME}
    // {PHONE-NUMBER}
    // {SUPPORT-EMAIL}
    // {WEBSITE}
    // {ADDRESS}
    // {SOCIALS}
    // {USERNAME}
    // {PROMO}
    // {NOTIFICATION}
    public class MailService : IMailService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly EpochDataDbContext _context;
        private readonly ILogger<MailService> _logger;
        private readonly MailSettings _mailConfig;

        public MailService(MailSettings mailConfig, EpochDataDbContext context, IHttpContextAccessor accessor, ILogger<MailService> logger)
        {
            _mailConfig = mailConfig;
            _context = context;
            _accessor = accessor;
            _logger = logger;
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
            // Name
            // Address
            // Phone
            // SupportEmail
            // SiteName
            // ContactLink

            var companySettings = await _context.ClientSettings.Where(x => x.FieldName == "Company").ToListAsync();
            var companyName = companySettings.FirstOrDefault(x => x.SettingField == "Name");
            var companyEmail = companySettings.FirstOrDefault(x => x.SettingField == "SupportEmail");
            var companyPhone = companySettings.FirstOrDefault(x => x.SettingField == "Phone");
            var companyAddress = companySettings.FirstOrDefault(x => x.SettingField == "Address");
            var companySite = companySettings.FirstOrDefault(x => x.SettingField == "SiteName");
            var contactPage = companySettings.FirstOrDefault(x => x.SettingField == "ContactLink");
            // var companySocials = companySettings.FirstOrDefault(x => x.FieldName == "Socials");


            var emailTemplate = await _context.EmailTemplates.Where(x => x.TemplateId == EmailTemplateType.AccountVerification).FirstOrDefaultAsync();
            if (emailTemplate == null)
                return;

            var request = _accessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            _logger.LogWarning("Base URI: " + baseUrl);
            // /Verification/{Token?}
            var verificationUrl = $"{baseUrl}/Verification/{token}";
            var verificationLink = $"<a href=\"{verificationUrl}\">Verify!</a>";
            var body = emailTemplate.HtmlBody;
            body = body.Replace("{VERIFICATION-LINK}", verificationLink);
            body = body.Replace("{VERIFICATION-URL}", verificationUrl);
            body = body.Replace("{USERNAME}", userName);
            body = body.Replace("{SUPPORT-EMAIL}", companyEmail.SettingValue);
            body = body.Replace("{SITE-NAME}", companySite.SettingValue);
            body = body.Replace("{PHONE-NUMBER}", companyPhone.SettingValue);
            body = body.Replace("{ADDRESS}", companyAddress.SettingValue);
            body = body.Replace("{CONTACT-LINK}", baseUrl + contactPage.SettingValue);

            var subject = emailTemplate.Subject;
            subject = subject.Replace("{USERNAME}", userName);
            subject = subject.Replace("{SITE-NAME}", companySite.SettingValue);

            _logger.LogWarning("Sending Verification Email to: " + email);
            await SendEmail(email, subject, body);
        }
    }
}