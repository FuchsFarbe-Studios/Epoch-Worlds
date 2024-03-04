// EpochWorlds
// LoginAttempt.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
namespace EpochApp.Shared.Users
{
    /// <summary>
    /// Represents a login attempt.
    /// </summary>
    public class LoginAttempt
    {
        /// <summary>
        /// Gets or sets the login attempt identifier.
        /// </summary>
        public int LoginAttempId { get; set; }

        /// <summary>
        /// Username or the email address used to attempt to login.
        /// </summary>
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// Whether or not the login attempt was successful.
        /// </summary>
        public bool IsSuccessful { get; set; } = false;

        /// <summary>
        /// The reason the login attempt failed.
        /// </summary>
        public string FailReason { get; set; }

        /// <summary>
        /// The ip address of the login attempt.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The user agent of the login attempt.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the login attempt.
        /// </summary>
        public DateTime? AttemptTime { get; set; }

        /// <summary>
        /// Location of the login attempt.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The device used to attempt to login.
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// The browser used to attempt to login.
        /// </summary>
        public string Browser { get; set; }
    }
}