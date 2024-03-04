// EpochWorlds
// Subscription.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared.Users
{
    #pragma warning disable CS1591
    public class Subscription
    {
        public long SubscriptionId { get; set; }
        public Guid UserId { get; set; }
        public long TierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }// Nullable, if it's an ongoing subscription
        public bool IsAutoRenew { get; set; }
        public bool IsActive { get; set; }
        public bool IsTrial { get; set; }
        public bool IsGift { get; set; }
        public virtual User User { get; set; }
        public virtual SubscriptionTier Tier { get; set; }
    }
}