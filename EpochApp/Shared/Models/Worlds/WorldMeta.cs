// EpochWorlds
// WorldMeta.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
namespace EpochApp.Shared
{
    public class WorldMeta
    {
        public Guid WorldId { get; set; }
        public int MetaID { get; set; }
        public string Content { get; set; }

        public World World { get; set; }
        public MetaTemplate Template { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DateRemoved { get; set; }
    }
}