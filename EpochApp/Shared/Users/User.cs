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
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get => (int)((DateTime.Now - DateOfBirth).TotalDays / 365); set => _age = value; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<string> Roles { get; private set; } = new List<string>();

        public ClaimsPrincipal ToClaimsPrincipal()
        {
            var claims = new List<Claim>
                         {
                             new Claim(ClaimTypes.Name, UserName),
                             new Claim(ClaimTypes.Email, Email),
                             new Claim(ClaimTypes.DateOfBirth, DateOfBirth.ToString("yyyy-MM-dd")),
                             new Claim(nameof(Age), Age.ToString()),
                         };
            claims.AddRange(UserRoles.Select(r => new Claim(ClaimTypes.Role, r.Role.Description)));

            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }

        public static User FromClaimsPrincipal(ClaimsPrincipal principal) =>
            new()
            {
                UserName = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
                Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
                Age = Convert.ToInt32(principal.FindFirst(nameof(Age))?.Value),
                Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
            };
    }
}