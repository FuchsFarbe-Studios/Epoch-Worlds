// EpochWorlds
// TagService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared.Social;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    /// <inheritdoc />
    public class TagService : ITagService
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///     Constructor for the TagService.
        /// </summary>
        /// <param name="context"> The database context. </param>
        public TagService(EpochDataDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Tag> CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        /// <inheritdoc />
        public async Task AddTagToUser(long tagId, Guid userId)
        {
            var userTag = new UserTag { TagId = tagId, UserId = userId };
            _context.UserTags.Add(userTag);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Tag>> GetUserTags(Guid userId)
        {
            return await _context.UserTags
                                 .Where(ut => ut.UserId == userId)
                                 .Select(ut => ut.Tag)
                                 .ToListAsync();
        }
    }
}