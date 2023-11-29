// EpochWorlds
// Role.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Users
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Description { get; set; } = "";

        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }
}