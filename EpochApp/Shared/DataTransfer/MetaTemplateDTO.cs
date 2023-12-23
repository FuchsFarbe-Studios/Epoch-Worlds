// EpochWorlds
// MetaTemplateDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// MetaTemplateDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// MetaTemplateDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023


// EpochWorlds
// MetaTemplateDTO.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Config;

namespace EpochApp.Shared
{
    public class MetaTemplateDTO
    {
        public String TemplateName { get; set; }
        public String Description { get; set; }
        public String Placeholder { get; set; }
        public String HelpText { get; set; }
        public virtual MetaCategory Category { get; set; }
    }
}