// EpochWorlds
// UserRole.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// UserRole.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// UserRole.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// UserRole.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Site.Users
{
	public class UserRole
	{
		public Guid UserID { get; set; }
		public Int32 RoleID { get; set; }
		public DateTime DateAssigned { get; set; }
		public DateTime DateRemoved { get; set; }

		public virtual User User { get; set; }
		public virtual Role Role { get; set; }
	}
}