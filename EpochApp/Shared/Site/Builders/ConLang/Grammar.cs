// EpochWorlds
// Grammar.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Language grammar settings.
    /// </summary>
    [Serializable]
    public class Grammar
    {
        /// <summary>
        ///     Word order of this language.
        /// </summary>
        public WordOrderType WordOrder { get; set; } = WordOrderType.Random;

        /// <summary>
        ///     Adjective order of this language.
        /// </summary>
        public AdjectiveOrderType AdjectiveOrder { get; set; } = AdjectiveOrderType.Random;

        /// <summary>
        ///     Adposition setting of this language.
        /// </summary>
        public AdPositionType AdPosition { get; set; } = AdPositionType.Random;

        /// <summary>
        ///     Affix type of this language to use in generation.
        /// </summary>
        public RandomAffixType AffixType { get; set; } = RandomAffixType.Random;

        /// <summary>
        ///     Toggle for noun genders.
        /// </summary>
        public bool UseNounGender { get; set; } = false;

        /// <summary>
        ///     List of this conlangs noun-genders.
        /// </summary>
        public List<NounGender> NounGenders { get; set; } = new List<NounGender>();
    }
}