// EpochWorlds
// BuilderService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EpochApp.Server.Services
{
    /// <inheritdoc />
    public class BuilderService : IBuilderService
    {
        private const string SECTION_OPEN = "<section>";
        private const string SECTION_CLOSE = "</section>";
        private const string ARTICLE_OPEN = "<article>";
        private const string ARTICLE_CLOSE = "</article>";
        private const string HEADER_OPEN = "<h3>";
        private const string HEADER_CLOSE = "</h3>";
        private const string PARAGRAPH_OPEN = "<p>";
        private const string PARAGRAPH_CLOSE = "</p>";
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IBuilderService> _logger;
        private readonly IMapper _mapper;
        private readonly ISerializationService _serialization;

        /// <summary>
        /// Constructor for the <see cref="BuilderService" />.
        /// </summary>
        /// <param name="context"> The <see cref="EpochDataDbContext" />. </param>
        /// <param name="logger"> The <see cref="ILogger{TCategoryName}" />. </param>
        /// <param name="mapper"> The <see cref="IMapper" />. </param>
        public BuilderService(EpochDataDbContext context, ILogger<BuilderService> logger, IMapper mapper, ISerializationService serialization)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _serialization = serialization;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BuilderContentDTO>> GetBuilderContentByAuthorAsync(Guid userId)
        {
            return await _context.BuilderContents
                                 .Where(x => x.AuthorID == userId)
                                 .Include(x => x.Author)
                                 .Select(x => _mapper.Map<BuilderContentDTO>(x))
                                 .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> GetBuilderContentByIdAsync(Guid contentId)
        {
            var content = await _context.BuilderContents
                                        .FirstOrDefaultAsync(x => x.ContentID == contentId);
            return _mapper.Map<BuilderContentDTO>(content);
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> GenerateContentAsync(Guid contentId, Guid userId)
        {
            var content = await _context.BuilderContents
                                        .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            switch (content.ContentType)
            {
                case ContentType.ConstructedLanguage:
                    _logger.LogInformation("Generating language...");
                    //await _language.GenerateLanguage(content);
                    break;
                case ContentType.Character:
                    break;
                case ContentType.Map:
                    break;
                case ContentType.World:
                    break;
                case ContentType.Calendar:
                    break;
                case ContentType.Religion:
                    break;
                case ContentType.System:
                    break;
                default:
                    return null;
            }
            return _mapper.Map<BuilderContentDTO>(content);
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> CreateNewBuilderContentAsync(BuilderContentDTO content)
        {
            var newContent = _mapper.Map<BuilderContent>(content);
            newContent.DateCreated = DateTime.UtcNow;
            await _context.BuilderContents.AddAsync(newContent);
            await _context.SaveChangesAsync();
            return _mapper.Map<BuilderContentDTO>(newContent);
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> UpdateBuilderAsync(Guid userId, Guid contentId, BuilderContentDTO content)
        {
            var existingContent = await _context.BuilderContents
                                                .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            if (existingContent == null)
                return null;

            _mapper.Map(content, existingContent);
            _context.Entry(existingContent).State = EntityState.Modified;
            try
            {
                _context.BuilderContents.Update(existingContent);
                await _context.SaveChangesAsync();
                return _mapper.Map<BuilderContentDTO>(existingContent);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating builder content...");
                return null;
            }
        }

        public async Task<ArticleDTO> BuildArticleAsync(Guid userId, Guid contentId)
        {
            var content = await _context.BuilderContents
                                        .Include(x => x.Author)
                                        .Include(x => x.World)
                                        .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            if (content == null)
                return null;

            switch (content.ContentType)
            {
                case ContentType.ConstructedLanguage:
                    return await GenerateLanguageArticleAsync(content);
                case ContentType.Character:
                    return null;
                case ContentType.Map:
                    return null;
                case ContentType.World:
                    return null;
                case ContentType.Calendar:
                    return null;
                case ContentType.Religion:
                    return null;
                case ContentType.System:
                    return null;
                default:
                    return null;
            }
        }

        private async Task<ArticleDTO> GenerateLanguageArticleAsync(BuilderContent builderContent)
        {
            var buildContent = await _serialization.DeserializeFromXmlAsync<ConstructedLanguage>(builderContent.ContentXml);
            var genContent = await _serialization.DeserializeFromXmlAsync<ConstructedLanguageResult>(builderContent.GeneratedXml);
            StringBuilder sb = new StringBuilder();
            sb.Append(PARAGRAPH_OPEN);
            sb.Append(PARAGRAPH_CLOSE);
            var dictionarySection = new ArticleSection
                                    {
                                        Title = "Dictionary",
                                        CreatedOn = DateTime.UtcNow,
                                    };
            var grammarSection = new ArticleSection()
                                 {
                                     Title = "Grammar",
                                     CreatedOn = DateTime.UtcNow,
                                 };
            var phonologySection = new ArticleSection()
                                   {
                                       Title = "Phonology",
                                       CreatedOn = DateTime.UtcNow,
                                   };
            var article = new Article
                          {
                              AuthorId = builderContent.AuthorID,
                              WorldId = builderContent.WorldID,
                              BuilderId = builderContent.ContentID,
                              Title = builderContent.ContentName,
                              SubTitle = genContent.Pronunciation,
                              Content = builderContent.GeneratedXml,
                              GeneratedContentXml = null,
                              ContentType = builderContent.ContentType,
                              IsPublished = true,
                              IsPublic = true,
                              IsWorkInProgress = false,
                              IsNSFW = false,
                              DisplayAuthor = true,
                              AllowComments = true,
                              AllowCopy = true,
                              ShowInTableOfContents = true,
                              ShowTableOfContents = true,
                              CreatedOn = DateTime.UtcNow,
                              Header = new ArticleHeader
                                       {
                                           Credits = builderContent.Author.UserName,
                                       },
                              SideBar = new ArticleSideBarContent
                                        {
                                            DisplaySidebar = true,
                                        },
                              Footer = new ArticleFooter
                                       {

                                       },
                              Sections = new List<ArticleSection>(),
                              ArticleTags = new List<ArticleTag>()
                                            {
                                                new ArticleTag
                                                {
                                                    Tag = new Tag
                                                          {
                                                              Text = "Conlang",
                                                          }
                                                }
                                            },
                              Meta = new List<ArticleMeta>(),
                          };
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return _mapper.Map<ArticleDTO>(article);
        }
    }
}