// EpochWorlds
// ArticleMeta.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Represents meta-data of an article.
    /// </summary>
    public class ArticleMeta
    {
        /// <summary> Article ID. </summary>
        public Guid ArticleId { get; set; }

        /// <summary> Meta ID. </summary>
        public int MetaId { get; set; }

        /// <summary> Meta field. </summary>
        public string MetaField { get; set; }

        /// <summary> Meta field type. </summary>

        public FieldType Type { get; set; }

        /// <summary> Meta value. </summary>
        public string MetaValue { get; set; }

        /// <summary>
        ///     Article navigation property.
        /// </summary>
        public virtual Article Article { get; set; }
    }
}