// EpochWorlds
// MetaDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class MetaDTO
    {
        public int MetaCategoryID { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string Placeholder { get; set; }
        public string HelpText { get; set; }

        public MetaCategoryDTO MetaCategory { get; set; }
    }
}