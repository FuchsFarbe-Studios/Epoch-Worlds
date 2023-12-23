// EpochWorlds
// UserDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using System.Security.Claims;

namespace EpochApp.Shared
{
    public class UserData
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => (int)((DateTime.Now - DateOfBirth).TotalDays / 365);
        public List<string> Roles { get; set; } = new List<string>();

        public ClaimsPrincipal ToClaimsPrincipal()
        {
            var claims = new List<Claim>
                         {
                             new Claim(ClaimTypes.NameIdentifier, UserID.ToString()),
                             new Claim(ClaimTypes.Name, UserName),
                             new Claim(ClaimTypes.Email, Email),
                             new Claim(ClaimTypes.Hash, Hash),
                             new Claim(ClaimTypes.DateOfBirth, DateOfBirth.ToString("yyyy-MM-dd")),
                             new Claim(nameof(Age), Age.ToString())
                         };
            claims.AddRange(Roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }

        public static UserData FromClaimsPrincipal(ClaimsPrincipal principal)
        {
            var roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value.ToString()).ToList();
            return new UserData
                   {
                       UserID = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value != null
                                    ? Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                                    : Guid.Empty,
                       UserName = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
                       Email = principal.FindFirst(ClaimTypes.Email)?.Value ?? "",
                       Hash = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
                       DateOfBirth = principal.FindFirst(ClaimTypes.DateOfBirth)?.Value != null
                                         ? DateTime.Parse(principal.FindFirst(ClaimTypes.DateOfBirth)?.Value)
                                         : DateTime.Now,
                       Roles = roles
                   };
        }
    }
}