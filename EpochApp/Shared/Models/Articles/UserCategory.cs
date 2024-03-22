// EpochWorlds
// UserCategory.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using EpochApp.Shared.Users;

namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class UserCategory
    {
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconAlt { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual User User { get; set; }
        public virtual UserCategory ParentCategory { get; set; }
        public virtual ICollection<UserCategory> ChildCategories { get; set; }
    }
}