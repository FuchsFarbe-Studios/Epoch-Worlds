// EpochWorlds
// ArticleTemplate.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class ArticleTemplate
    {
        public ArticleTemplate()
        {
            Sections = new HashSet<ArticleTemplateSection>();
            Meta = new HashSet<ArticleTemplateMeta>();
        }

        public int TemplateId { get; set; }

        public int? CategoryId { get; set; }

        public string TemplateName { get; set; }

        public string Description { get; set; }

        public string Placeholder { get; set; }

        public string HelpText { get; set; }

        public virtual ArticleCategory Category { get; set; }

        public virtual ICollection<ArticleTemplateSection> Sections { get; set; }

        public virtual ICollection<ArticleTemplateMeta> Meta { get; set; }
    }

}