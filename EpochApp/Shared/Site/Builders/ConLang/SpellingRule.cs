// EpochWorlds
// SpellingRule.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Represents settings for a constructed language's spelling rule.
    /// </summary>
    [Serializable]
    public class SpellingRule
    {
        /// <summary>
        ///     Predicate for this spelling rule.
        /// </summary>
        public string Predicate { get; set; } = "";

        /// <summary>
        ///     Replaces the <see cref="Predicate" />.
        /// </summary>
        public string Replacement { get; set; } = "";

        /// <summary>
        ///     Environment for this spelling rule, if exists.
        /// </summary>
        public string Environment { get; set; } = "";
    }
}