// EpochWorlds
// SectionDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for article sections.
    /// </summary>
    public class SectionDTO
    {
        public Guid SectionID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}