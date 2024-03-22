// EpochWorlds
// UserCategoryService.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    #pragma warning disable CS1591
    public class UserCategoryService : IUserCategoryService
    {
        private readonly EpochDataDbContext _context;
        private readonly IMapper _mapper;

        public UserCategoryService(EpochDataDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<UserCategoryDTO>> GetUserCategoriesAsync(Guid userId)
        {
            var categories = await _context.UserCategories
                                           .Select(x => _mapper.Map(x, new UserCategoryDTO()))
                                           .ToListAsync();
            return categories;
        }

        /// <inheritdoc />
        public async Task<UserCategoryDTO> CreateUserCategoryAsync(Guid userId, UserCategoryDTO category)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null)
                return null;
            if (userId != category.UserId)
                return null;

            var newCategory = _mapper.Map<UserCategoryDTO, UserCategory>(category);
            newCategory.CreatedOn = DateTime.UtcNow;
            newCategory.UserId = userId;
            _context.UserCategories.Add(newCategory);
            await _context.SaveChangesAsync();
            return _mapper.Map(newCategory, new UserCategoryDTO());
        }

        /// <inheritdoc />
        public async Task<UserCategoryDTO> UpdateUserCategoryAsync(Guid userId, int categoryId, UserCategoryDTO category)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null)
                return null;

            var categoryToUpdate = await _context.UserCategories.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.UserId == userId);
            if (categoryToUpdate == null)
                return null;

            _mapper.Map(category, categoryToUpdate);
            _context.Entry(categoryToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map(categoryToUpdate, new UserCategoryDTO());
        }

        /// <inheritdoc />
        public async Task<bool> DeleteUserCategoryAsync(Guid userId, int categoryId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null)
                return false;

            var categoryToDelete = await _context.UserCategories.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.UserId == userId);
            if (categoryToDelete == null)
                return false;

            _context.UserCategories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<UserCategoryDTO> GetUserCategoryAsync(Guid userId, int categoryId)
        {
            var category = await _context.UserCategories.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.UserId == userId);
            return _mapper.Map(category, new UserCategoryDTO());
        }
    }
}