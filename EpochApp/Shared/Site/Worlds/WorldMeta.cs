// EpochWorlds
// WorldMeta.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

// EpochWorlds
// WorldMeta.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

// EpochWorlds
// WorldMeta.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

// EpochWorlds
// WorldMeta.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochApp.Shared.Config.Lookups;

namespace EpochApp.Shared.Site.Worlds
{
	public class WorldMeta
	{
		public Guid WorldID { get; set; }
		public Int32 MetaID { get; set; }
		public String Content { get; set; }

		public World World { get; set; }
		public MetaTemplate Template { get; set; }
	}
}