// EpochWorlds
// UserCategoryDTO.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
namespace EpochApp.Shared
{
    #pragma warning disable CS1591
    public class UserCategoryDTO
    {
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconAlt { get; set; }
        public UserCategoryDTO ParentCategory { get; set; }
        public ICollection<UserCategoryDTO> ChildCategories { get; set; }
    }
}