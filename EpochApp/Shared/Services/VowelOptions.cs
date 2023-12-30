// EpochWorlds
// VowelOptions.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 29-12-2023
namespace EpochApp.Shared.Services
{
    public class VowelOptions
    {
        public Guid OptionsID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid PhonologyID { get; set; }
        public Guid VowelOptionsID { get; set; }
        public bool UseVowelProbabilities { get; set; } = false;
        public decimal VowelAtStart { get; set; }
        public decimal VowelAtEnd { get; set; }
        public bool UseVowelTones { get; set; } = false;
        public string VowelTones { get; set; }
        public ToneRepresentationType ToneRepresentation { get; set; } = ToneRepresentationType.ToneLetters;

        public virtual PhonologyOptions PhonologyOpts { get; set; }
    }
}