// EpochWorlds
// Role.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Site.Users
{
	public class Role
	{
		public Int32 RoleID { get; set; }
		public String Description { get; set; } = "";

		public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
	}
}