// EpochWorlds
// EmailTemplate.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
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