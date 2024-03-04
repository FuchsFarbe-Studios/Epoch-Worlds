// EpochWorlds
// TagService.cs
// FuchsFarbe Studios 2024
// Modified: 29-2-2024

using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Users;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    /// <summary>
    ///     Service for managing tags.
    /// </summary>
    public class TagService : ITagService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ITagService> _logger;

        /// <summary>
        ///    Constructor for the tag service.
        /// </summary>
        /// <param name="context"> The database context. </param>
        /// <param name="logger"> The logger. </param>
        public TagService(EpochDataDbContext context, ILogger<TagService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<TagDTO>> GetTagsAsync()
        {
            var tags = await _context.Tags.Select(x => new TagDTO
                                                       {
                                                           Id = x.TagId,
                                                           Text = x.Text
                                                       })
                                     .ToListAsync();
            return await Task.FromResult(tags);
        }

        /// <inheritdoc />
        public async Task<List<UserTagDTO>> GetUserTagsAsync(Guid userId)
        {
            var userTags = await _context.UserTags
                                         .Where(x => x.UserId == userId)
                                         .Include(userTag => userTag.Tag)
                                         .Select(x => new UserTagDTO
                                                      {
                                                          UserId = x.UserId,
                                                          TagId = x.TagId,
                                                          Text = x.Tag.Text
                                                      })
                                         .ToListAsync();
            return await Task.FromResult(userTags);
        }

        /// <inheritdoc />
        public async Task<List<WorldTagDTO>> GetWorldTagsAsync(Guid worldId)
        {
            var worldTags = await _context.WorldTags
                                          .Where(x => x.WorldId == worldId)
                                          .Include(worldTag => worldTag.Tag)
                                          .Select(x => new WorldTagDTO
                                                       {
                                                           WorldId = x.WorldId,
                                                           TagId = x.TagId,
                                                           Text = x.Tag.Text
                                                       })
                                          .ToListAsync();
            return await Task.FromResult(worldTags);
        }

        /// <inheritdoc />
        public async Task<TagDTO> GetTagAsync(string tagText)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Text.ToLower() == tagText.ToLower());
            var dto = new TagDTO
                      {
                          Id = tag.TagId,
                          Text = tag.Text
                      };
            return await Task.FromResult(dto);
        }

        /// <inheritdoc />
        public async Task<TagDTO> CreateTagAsync(TagDTO tag)
        {
            var newTag = new Tag { Text = tag.Text };
            // Check if tag already exists
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Text.ToLower() == tag.Text.ToLower());
            if (existingTag != null)
            {
                _logger.LogInformation($"Tag {tag.Text} already exists");
                return new TagDTO { Id = existingTag.TagId, Text = existingTag.Text };
            }

            _logger.LogInformation($"Creating tag {tag.Text}");
            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();

            var dto = new TagDTO { Id = newTag.TagId, Text = newTag.Text };
            return await Task.FromResult(dto);
        }

        /// <inheritdoc />
        public async Task<UserTagDTO> CreateUserTagAsync(UserTagDTO userTag)
        {
            var newUserTag = new UserTag
                             {
                                 UserId = userTag.UserId,
                                 TagId = userTag.TagId
                             };
            _context.UserTags.Add(newUserTag);
            await _context.SaveChangesAsync();

            var dto = new UserTagDTO
                      {
                          UserId = newUserTag.UserId,
                          TagId = newUserTag.TagId,
                          Text = newUserTag.Tag.Text
                      };
            return await Task.FromResult(dto);
        }

        /// <inheritdoc />
        public async Task<WorldTagDTO> CreateWorldTagAsync(WorldTagDTO worldTag)
        {
            var newWorldTag = new WorldTag
                              {
                                  WorldId = worldTag.WorldId,
                                  TagId = worldTag.TagId
                              };
            _context.WorldTags.Add(newWorldTag);
            await _context.SaveChangesAsync();

            var dto = new WorldTagDTO
                      {
                          WorldId = newWorldTag.WorldId,
                          TagId = newWorldTag.TagId,
                          Text = newWorldTag.Tag.Text
                      };
            return await Task.FromResult(dto);
        }

        /// <inheritdoc />
        public async Task<ArticleTagDTO> CreateArticleTagAsync(ArticleTagDTO articleTag)
        {
            var newArticleTag = new ArticleTag
                                {
                                    ArticleId = articleTag.ArticleId,
                                    TagId = articleTag.TagId
                                };
            // Check if Tag already exists
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Text.ToLower() == articleTag.Text.ToLower());
            if (existingTag != null)
            {
                _logger.LogInformation($"Tag {existingTag.Text} already exists");
                return new ArticleTagDTO
                       {
                           ArticleId = newArticleTag.ArticleId,
                           TagId = 0,
                           Text = existingTag.Text
                       };
            }

            _logger.LogInformation($"Creating ArticleTag for ArticleId: {articleTag.ArticleId} and TagId: {articleTag.TagId}");
            await _context.ArticleTags.AddAsync(newArticleTag);
            await _context.SaveChangesAsync();

            var dto = new ArticleTagDTO
                      {
                          ArticleId = newArticleTag.ArticleId,
                          TagId = newArticleTag.TagId,
                          Text = newArticleTag.Tag.Text
                      };
            return await Task.FromResult(dto);
        }
    }
}