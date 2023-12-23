// EpochWorlds
// WorldDate.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

// EpochWorlds
// WorldDate.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

// EpochWorlds
// WorldDate.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

// EpochWorlds
// WorldDate.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
namespace EpochApp.Shared.Worlds
{
    public class WorldDate
    {
        public Guid WorldID { get; set; }
        public int CurrentDay { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public string CurrentAge { get; set; }

        public virtual World World { get; set; }
    }
}