// EpochWorlds
// User.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared.Users
{

    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            OwnedWorlds = new HashSet<World>();
            OwnedArticles = new HashSet<Article>();
            UserTags = new HashSet<UserTag>();
            UserFiles = new HashSet<UserFile>();
            Manuscripts = new HashSet<Manuscript>();
        }

        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsVerified { get; set; } = false;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? VerificationTokenCreated { get; set; }
        public DateTime? VerificationTokenExpires { get; set; }

        /// <summary>
        ///     The reset token for this user.
        /// </summary>
        public string ResetToken { get; set; }

        /// <summary>
        ///     The date the reset token was created.
        /// </summary>
        public DateTime? ResetTokenCreated { get; set; }

        /// <summary>
        ///     The date the reset token expires.
        /// </summary>
        public DateTime? ResetTokenExpires { get; set; }

        public string NormalizedUserName => UserName.ToUpper();

        public string NormalizedEmail => Email.ToUpper();

        /// <summary>
        ///     The profile of this user.
        /// </summary>
        public virtual Profile Profile { get; set; }

        /// <summary>
        ///     The roles this user has.
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        ///     The worlds owned by this user.
        /// </summary>
        public virtual ICollection<World> OwnedWorlds { get; set; }

        /// <summary>
        ///     The articles this user has created.
        /// </summary>
        public virtual ICollection<Article> OwnedArticles { get; set; }

        /// <summary>
        ///     The tags this user has created.
        /// </summary>
        public virtual ICollection<UserTag> UserTags { get; set; }

        /// <summary>
        ///     The files associated with this user.
        /// </summary>
        public virtual ICollection<UserFile> UserFiles { get; set; }

        /// <summary>
        ///     The manuscripts this user has written.
        /// </summary>
        public virtual ICollection<Manuscript> Manuscripts { get; set; }

        /// <summary>
        ///    The subscriptions this user has.
        /// </summary>
        public virtual ICollection<Subscription> Subscriptions { get; set; }

        /// <summary>
        ///    The ban tickets this user is associated with.
        /// </summary>
        public virtual ICollection<UserReport> AdminReports { get; set; }

        /// <summary>
        /// Reports filed by this user.
        /// </summary>
        public ICollection<UserReport> PlaintiffReports { get; set; }

        /// <summary>
        /// Reports this user has been reported in.
        /// </summary>
        public ICollection<UserReport> DefendantReports { get; set; }

        /// <summary>
        ///   The ban tickets this admin is associated with.
        /// </summary>
        public virtual ICollection<BanTicket> AdminTickets { get; set; }

        /// <summary>
        ///  The ban tickets this user is associated with.
        /// </summary>
        public virtual ICollection<BanTicket> UserTickets { get; set; }
    }
}