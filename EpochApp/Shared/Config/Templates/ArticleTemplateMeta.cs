// EpochWorlds
// ArticleTemplateMeta.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
namespace EpochApp.Shared.Config
{
    #pragma warning disable CS1591
    public class ArticleTemplateMeta
    {
        public int TemplateId { get; set; }

        public string MetaName { get; set; }

        public string Description { get; set; }

        public FieldType Type { get; set; }

        public string Placeholder { get; set; }

        public string HelpText { get; set; }

        public virtual ArticleTemplate Template { get; set; }
    }
}