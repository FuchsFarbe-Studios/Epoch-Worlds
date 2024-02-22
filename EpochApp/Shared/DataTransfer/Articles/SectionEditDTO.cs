// EpochWorlds
// SectionEditDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using System.ComponentModel.DataAnnotations;

namespace EpochApp.Shared
{
    /// <summary>
    ///     Data transfer object for article sections that need to be edited.
    /// </summary>
    public class SectionEditDTO
    {
        public Guid? ArticleId { get; set; }
        public Guid SectionId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}