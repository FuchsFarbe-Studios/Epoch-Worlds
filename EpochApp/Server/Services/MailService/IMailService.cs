// EpochWorlds
// IMailService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024

namespace EpochApp.Server.Services.MailService
{
    /// <summary>
    /// Interface for sending emails.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Send an email to the specified email address.
        /// </summary>
        /// <param name="toEmail"> The email address to send the email to</param>
        /// <param name="subject"> The subject of the email</param>
        /// <param name="content"> The content of the email</param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task SendEmail(string toEmail, string subject, string content);

        /// <summary>
        /// Send a verification email to the specified email address.
        /// </summary>
        /// <param name="email"> The email address to send the email to.</param>
        /// <param name="userName"> The username of the user.</param>
        /// <param name="token"> The token to verify the user's email.</param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task SendVerificationEmailAsync(string email, string userName, string token);

        /// <summary>
        /// Send a password reset email to the specified email address.
        /// </summary>
        /// <param name="userEmail"> The email address to send the email to.</param>
        /// <param name="userName"> The username of the user.</param>
        /// <param name="token"> The token to reset the user's password.</param>
        /// <returns> A task representing the asynchronous operation.</returns>
        Task SendResetPasswordEmailAsync(string userEmail, string userName, string token);
    }

}