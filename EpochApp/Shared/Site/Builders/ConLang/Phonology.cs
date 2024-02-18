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
        /// <summary>
        ///     Permitted language consonants.
        /// </summary>
        public string Consonants { get; set; }

        /// <summary>
        ///     Permitted language vowels.
        /// </summary>
        public string Vowels { get; set; }

        /// <summary>
        ///     Toggle for intermediate settings.
        /// </summary>
        public bool? UseIntermediateWordStructure { get; set; }

        /// <summary> Initial consonants. </summary>
        public string InitialConsonants { get; set; }

        /// <summary> Medial consonants. </summary>
        public string MedialConsonants { get; set; }

        /// <summary> Final consonants. </summary>
        public string FinalConsonants { get; set; }

        /// <summary>
        ///     Setting to toggle vowel harmony.
        /// </summary>
        public bool? UseVowelHarmony { get; set; }

        /// <summary>
        ///     Complimentary vowel pairing.
        /// </summary>
        public string HarmonicVowels { get; set; }

        /// <summary> Frequency of phonemes. </summary>
        public PhonemeFrequency PhonemeFrequency { get; set; } = PhonemeFrequency.Medium;

        /// <summary>
        ///     Advanced phonology setting toggle.
        /// </summary>
        public bool? UseAdvancedWordStructure { get; set; }

        /// <summary> Phoneme classes. </summary>
        public string PhonemeClasses { get; set; }

        /// <summary> Word patterns. </summary>
        public string WordPatterns { get; set; }

        /// <summary> Affix patterns. </summary>
        public string AffixPatterns { get; set; }

        /// <summary>
        ///     Illegal phoneme combinations.
        /// </summary>
        public string IllegalCombos { get; set; }

        /// <summary>
        ///     Bans the same vowel twice in a row.
        /// </summary>
        public bool BanSameVowelTwiceInARow { get; set; } = true;

        /// <summary>
        ///     Bans the same syllable twice in a row.
        /// </summary>
        public bool BanSameSyllableTwiceInARow { get; set; } = false;

        /// <summary>
        ///     Uses vowel probability settings.
        /// </summary>
        public bool UseVowelProbabilities { get; set; } = false;

        /// <summary>
        ///     Probability of a vowel at the start of a word.
        /// </summary>
        public float? VowelAtStartProbability { get; set; }

        /// <summary>
        ///     Probability of a vowel at the end of a word.
        /// </summary>
        public float? VowelAtEndProbability { get; set; }

        /// <summary>
        ///     Toggles the usage of vowel tones.
        /// </summary>
        public bool UseVowelTones { get; set; } = false;

        /// <summary> Vowel tone settings. </summary>
        public string Tones { get; set; }

        /// <summary> Tone representation. </summary>
        public ToneRepresentation ToneRepresentation { get; set; } = ToneRepresentation.Diacritics;

        /// <summary>
        ///     Toggles the use of sound changes.
        /// </summary>
        public bool UseSoundChanges { get; set; } = false;

        /// <summary> Sound change settings. </summary>
        public string SoundChanges { get; set; }

        /// <summary>
        ///     Reflects sound changes in spelling.
        /// </summary>
        public bool ReflectSoundChangeInSpelling { get; set; } = false;

        /// <summary>
        ///     Shows changes in brackets.
        /// </summary>
        public bool ShowChangesInBrackets { get; set; } = false;

        /// <summary>
        ///     Removes slash around IPA spelling.
        /// </summary>
        public bool RemoveSlashAroundIPA { get; set; } = false;

        /// <summary>
        ///     Determines word stress patterns.
        /// </summary>
        public StressPattern StressPattern { get; set; } = StressPattern.None;

        /// <summary>
        ///     Allows contrastive stress.
        /// </summary>
        public bool AllowContrastiveStress { get; set; } = false;

        /// <summary>
        ///     Overrides vocabulary stress.
        /// </summary>
        public bool OverrideVocabStress { get; set; } = false;
    }
}