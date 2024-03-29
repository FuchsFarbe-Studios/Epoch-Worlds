// EpochWorlds
// ArticleProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 1-3-2024
using EpochApp.Shared;
using Profile=AutoMapper.Profile;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Server.Maps
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleHeader, ArticleHeaderDTO>();
            CreateMap<ArticleHeaderDTO, ArticleHeader>();
            CreateMap<ArticleFooter, ArticleFooterDTO>();
            CreateMap<ArticleFooterDTO, ArticleFooter>();
            CreateMap<ArticleSideBarContent, SideBarDTO>();
            CreateMap<SideBarDTO, ArticleSideBarContent>();
            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();
            CreateMap<Article, ArticleEditDTO>();
            CreateMap<ArticleEditDTO, Article>();
            CreateMap<ArticleSection, SectionDTO>();
            CreateMap<SectionDTO, ArticleSection>();
            CreateMap<ArticleSection, SectionEditDTO>();
            CreateMap<SectionEditDTO, ArticleSection>();
            CreateMap<ArticleTag, ArticleTagDTO>();
            CreateMap<ArticleTagDTO, ArticleTag>();
            CreateMap<ArticleTemplateMeta, ArticleMetaDTO>();
            CreateMap<ArticleMetaDTO, ArticleTemplateMeta>();
            CreateMap<ArticleTemplateSection, SectionTemplateDTO>();
            CreateMap<SectionTemplateDTO, ArticleTemplateSection>();
            CreateMap<ArticleTemplate, ArticleTemplateDTO>();
            CreateMap<ArticleTemplateDTO, ArticleTemplate>();
            CreateMap<UserCategory, UserCategoryDTO>();
            CreateMap<UserCategoryDTO, UserCategory>();
        }
    }

}