// EpochWorlds
// MetaTemplate.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    /// <summary>
    ///     A meta template for directing users in world-meta generation.
    /// </summary>
    public class MetaTemplate
    {
        public int TemplateId { get; set; }
        public int CategoryId { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string Placeholder { get; set; }
        public string HelpText { get; set; }

        public virtual MetaCategory Category { get; set; }
    }
}