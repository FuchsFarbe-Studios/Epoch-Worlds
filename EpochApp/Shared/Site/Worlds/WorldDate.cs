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
        public Int32 CurrentDay { get; set; }
        public Int32 CurrentMonth { get; set; }
        public Int32 CurrentYear { get; set; }
        public String CurrentAge { get; set; }

        public virtual World World { get; set; }
    }
}