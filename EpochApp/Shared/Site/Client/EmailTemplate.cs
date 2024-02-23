// EpochWorlds
// EmailTemplate.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
namespace EpochApp.Shared.Client
{
    /// <summary>
    ///     Email templates for user verification and password recovery, etc.
    /// </summary>
    public class EmailTemplate
    {
        public EmailTemplateType TemplateId { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
    }
}