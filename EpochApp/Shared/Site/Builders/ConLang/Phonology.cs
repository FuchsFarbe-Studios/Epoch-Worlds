// EpochWorlds
// Phonology.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
namespace EpochApp.Shared
{
    [Serializable]
    public class Phonology
    {
        public int PhonologyId { get; set; }
        public string Consonants { get; set; }
        public string Vowels { get; set; }
        public bool? UseIntermediateWordStructure { get; set; }
        public string InitialConsonants { get; set; }
        public string MedialConsonants { get; set; }
        public string FinalConsonants { get; set; }
        public bool? UseVowelHarmony { get; set; }
        public string HarmonicVowels { get; set; }
        public PhonemeFrequency PhonemeFrequency { get; set; } = PhonemeFrequency.Medium;
        public bool? UseAdvancedWordStructure { get; set; }
        public string PhonemeClasses { get; set; }
        public string WordPatterns { get; set; }
        public string AffixPatterns { get; set; }
        public string IllegalCombos { get; set; }
        public bool BanSameVowelTwiceInARow { get; set; } = true;
        public bool BanSameSyllableTwiceInARow { get; set; } = false;
        public bool UseVowelProbabilities { get; set; } = false;
        public float? VowelAtStartProbability { get; set; }
        public float? VowelAtEndProbability { get; set; }
        public bool UseVowelTones { get; set; } = false;
        public string Tones { get; set; }
        public ToneRepresentation ToneRepresentation { get; set; } = ToneRepresentation.Diacritics;
        public bool UseSoundChanges { get; set; } = false;
        public string SoundChanges { get; set; }
        public bool ReflectSoundChangeInSpelling { get; set; } = false;
        public bool ShowChangesInBrackets { get; set; } = false;
        public bool RemoveSlashAroundIPA { get; set; } = false;
        public StressPattern StressPattern { get; set; } = StressPattern.None;
        public bool AllowContrastiveStress { get; set; } = false;
        public bool OverrideVocabStress { get; set; } = false;
    }
}