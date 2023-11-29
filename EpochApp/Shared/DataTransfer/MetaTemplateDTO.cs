// EpochWorlds
// MetaTemplateDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Lookups;

namespace EpochApp.Shared
{
    public class MetaTemplateDTO
    {
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string Placeholder { get; set; }
        public string HelpText { get; set; }
        public virtual MetaCategory Category { get; set; }
    }
}