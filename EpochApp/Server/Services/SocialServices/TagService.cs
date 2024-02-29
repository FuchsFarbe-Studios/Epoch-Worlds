// EpochWorlds
// TagService.cs
// FuchsFarbe Studios 2024
// Modified: 29-2-2024

using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Social;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    public class TagService : ITagService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ITagService> _logger;

        public TagService(EpochDataDbContext context, ILogger<TagService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TagDTO>> GetTagsAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            return tags.Select(tag => new TagDTO
                                      {
                                          Id = tag.TagId,
                                          Text = tag.Text
                                      });
        }

        public async Task<TagDTO> GetTagAsync(long id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.TagId == id);
            var dto = new TagDTO
                      {
                          Id = tag.TagId,
                          Text = tag.Text
                      };
            return await Task.FromResult(dto);
        }

        public async Task<TagDTO> CreateTagAsync(TagDTO tag)
        {
            var newTag = new Tag { Text = tag.Text };

            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();

            var dto = new TagDTO { Id = newTag.TagId, Text = newTag.Text };
            return await Task.FromResult(dto);
        }

        public async Task<IEnumerable<UserTagDTO>> GetUserTagsAsync(Guid userId)
        {
            var userTags = await _context.UserTags
                                         .Where(x => x.UserId == userId)
                                         .Include(userTag => userTag.Tag)
                                         .ToListAsync();
            return userTags.Select(tag => new UserTagDTO
                                          {
                                              UserId = tag.UserId,
                                              TagId = tag.TagId,
                                              Text = tag.Tag.Text
                                          });
        }

        public async Task<UserTagDTO> CreateUserTagAsync(UserTagDTO tag)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Text.ToLower() == tag.Text.ToLower());
            if (existingTag == null)
            {
                var newTag = new Tag { Text = tag.Text };
                _context.Tags.Add(newTag);
                await _context.SaveChangesAsync();
                tag.TagId = newTag.TagId;
            }
            else
            {
                tag.TagId = existingTag.TagId;
            }

            var newUserTag = new UserTag { TagId = tag.TagId, UserId = tag.UserId };

            _context.UserTags.Add(newUserTag);
            await _context.SaveChangesAsync();

            var dto = new UserTagDTO { UserId = newUserTag.UserId, TagId = newUserTag.TagId, Text = tag.Text };
            return await Task.FromResult(dto);
        }

        // Remove user tag
        public async Task RemoveUserTagAsync(UserTagDTO tag)
        {
            var userTag = await _context.UserTags.FirstOrDefaultAsync(x => x.UserId == tag.UserId && x.Tag.Text.ToLower() == tag.Text.ToLower());
            if (userTag != null)
            {
                _context.UserTags.Remove(userTag);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"User tag removed for user {tag.UserId} and tag {tag.Text}");
                return;
            }
            _logger.LogWarning($"User tag not found for user {tag.UserId} and tag {tag.Text}");
        }
    }
}