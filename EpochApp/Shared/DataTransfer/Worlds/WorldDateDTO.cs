// EpochWorlds
// WorldDateDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared
{
    public class WorldDateDTO
    {
        public Guid WorldId { get; set; }
        public int CurrentDay { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public string CurrentAge { get; set; }
        public string BeforeEra { get; set; }
        public string AfterEra { get; set; }
        public string BeforeEraAbbreviation { get; set; }
        public string AfterEraAbbreviation { get; set; }
        public string CurrentEra { get; set; }
    }
}