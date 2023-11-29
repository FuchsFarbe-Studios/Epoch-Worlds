// EpochWorlds
// User.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using System.Security.Claims;

namespace EpochApp.Shared
{
    public class User
    {
        private int _age;
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string NormalizedUserName { get => UserName.ToUpper(); }
        public string NormalizedEmailName { get => Email.ToUpper(); }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}