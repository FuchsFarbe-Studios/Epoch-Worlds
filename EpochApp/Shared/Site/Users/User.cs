// EpochWorlds
// User.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Services;
using EpochApp.Shared.Worlds;

namespace EpochApp.Shared.Users
{
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            OwnedBlogs = new HashSet<BlogOwner>();
            OwnedWorlds = new HashSet<World>();
            ContentOptions = new HashSet<ContentOptions>();
        }

        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        public string NormalizedUserName => UserName.ToUpper();
        public string NormalizedEmail => Email.ToUpper();

        public virtual Profile Profile { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<BlogOwner> OwnedBlogs { get; set; }
        public ICollection<World> OwnedWorlds { get; set; }

        public ICollection<ContentOptions> ContentOptions { get; set; }
    }
}