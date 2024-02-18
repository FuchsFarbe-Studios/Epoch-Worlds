// EpochWorlds
// WorldDTO.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 26-12-2023
using EpochApp.Shared.Worlds;

namespace EpochApp.Shared
{
    public class WorldDTO
    {
        public Guid AuthorID { get; set; }
        public Guid WorldID { get; set; }

        public string? WorldName { get; set; }
        public string? Pronunciation { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }

        // WorldDate
        public int? CurrentDay { get; set; }
        public int? CurrentMonth { get; set; }
        public int? CurrentYear { get; set; }
        public string? CurrentAge { get; set; }

        public virtual ICollection<WorldMeta> MetaData { get; set; } = new List<WorldMeta>();
        public bool? IsActiveWorld { get; set; }
    }
}