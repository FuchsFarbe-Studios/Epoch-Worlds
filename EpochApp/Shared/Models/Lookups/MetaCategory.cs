// EpochWorlds
// MetaCategory.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
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