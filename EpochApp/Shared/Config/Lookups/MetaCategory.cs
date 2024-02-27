// EpochWorlds
// MetaCategory.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Config
{
    public class MetaCategory
    {
        public MetaCategory()
        {
            Templates = new HashSet<MetaTemplate>();
        }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public string CategoryInfo { get; set; }

        public virtual ICollection<MetaTemplate> Templates { get; set; }
    }
}