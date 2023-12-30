// EpochWorlds
// PhonologyOptions.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 29-12-2023
namespace EpochApp.Shared.Services
{
    public class PhonologyOptions
    {

        public Guid OptionsID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid PhonologyID { get; set; }

        public string Consonants { get; set; }
        public string Vowels { get; set; }
        public bool UseWordStructure { get; set; } = false;
        public bool UseAdvancedWordStructure { get; set; } = false;
        public string WordInitialConsonants { get; set; }
        public string WordMedialConsonants { get; set; }
        public string WordFinalConsonants { get; set; }
        public string HarmonicVowels { get; set; }

        public virtual LangOptions LangOpts { get; set; }
        public virtual IllegalComboOptions IllegalOpts { get; set; }
        public virtual VowelOptions VowelOpts { get; set; }
    }
}