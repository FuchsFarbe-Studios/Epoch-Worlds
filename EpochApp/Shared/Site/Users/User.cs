// EpochWorlds
// User.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Articles;
using EpochApp.Shared.Worlds;

namespace EpochApp.Shared.Users
{
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            OwnedWorlds = new HashSet<World>();
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
    }
}