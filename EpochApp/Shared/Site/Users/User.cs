// EpochWorlds
// User.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

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
        public String UserName { get; set; }
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }
        public String NormalizedUserName => UserName.ToUpper();
        public String NormalizedEmail => Email.ToUpper();

        public virtual Profile Profile { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<BlogOwner> OwnedBlogs { get; set; }
        public ICollection<World> OwnedWorlds { get; set; }
    }
}