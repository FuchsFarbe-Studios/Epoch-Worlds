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
namespace EpochApp.Shared
{
    public class WorldDate
    {
        /// <summary>
        ///     The unique identifier for the world date.
        /// </summary>
        public Guid WorldID { get; set; }

        /// <summary>
        ///     The current day of the world.
        /// </summary>
        public int CurrentDay { get; set; }

        /// <summary>
        ///     The current month of the world.
        /// </summary>
        public int CurrentMonth { get; set; }

        /// <summary>
        ///     The current year of the world.
        /// </summary>
        public int CurrentYear { get; set; }

        /// <summary>
        ///     The current era of the world.
        /// </summary>
        public string CurrentAge { get; set; }

        /// <summary>
        ///     The equivalent of BC/BCE in your world.
        /// </summary>
        public string? BeforeEraName { get; set; }

        /// <summary>
        ///     Abbreviation of <see cref="BeforeEraName" />.
        /// </summary>
        public string? BeforeEraAbbreviation { get; set; }

        /// <summary>
        ///     The equivalent of AD/CE in your world.
        /// </summary>
        public string? AfterEraName { get; set; }

        /// <summary>
        ///     Abbreviation of <see cref="AfterEraName" />.
        /// </summary>
        public string? AfterEraAbbreviation { get; set; }

        /// <summary>
        ///     World the date is associated with.
        /// </summary>
        public virtual World World { get; set; }
    }
}