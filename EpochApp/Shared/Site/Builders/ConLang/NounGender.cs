// EpochWorlds
// NounGender.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
namespace EpochApp.Shared
{
    /// <summary> Defines a noun-gender. </summary>
    [Serializable]
    public class NounGender
    {
        /// <summary> Name of this gender. </summary>
        public string Gender { get; set; }

        /// <summary> Gender abbreviation. </summary>
        public string Abbreviation { get; set; }
    }
}