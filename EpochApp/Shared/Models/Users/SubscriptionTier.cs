// EpochWorlds
// SubscriptionTier.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared.Users
{
    #pragma warning disable CS1591
    public class SubscriptionTier
    {
        public long TierId { get; set; }
        public string Name { get; set; }
        public string TierTitle { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconAlt { get; set; }
        public string Image { get; set; }
        public string ImageAlt { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public int MaxWorlds { get; set; }
        public short MaxCoAuthors { get; set; }
        public long MaxArticles { get; set; }
        public int MaxFiles { get; set; }
        public long TotalFileSizeLimit { get; set; }
        public bool BuilderUse { get; set; } = false;
        public bool AdvancedBuilderUse { get; set; } = false;
        public bool AccessWorldStats { get; set; } = false;

        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}