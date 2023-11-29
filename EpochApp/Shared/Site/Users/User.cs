// EpochWorlds
// User.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Blog;
using EpochApp.Shared.Worlds;

namespace EpochApp.Shared.Users
{
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
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
        public string NormalizedUserName { get => UserName.ToUpper(); }
        public string NormalizedEmail { get => Email.ToUpper(); }

        public virtual Profile Profile { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<BlogOwner> OwnedBlogs { get; set; }
        public ICollection<World> OwnedWorlds { get; set; }
    }
}