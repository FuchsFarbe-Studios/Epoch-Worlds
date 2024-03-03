// EpochWorlds
// ArticleProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 1-3-2024
using AutoMapper;
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using EpochApp.Shared.Social;

namespace EpochApp.Server.Maps
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
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
        }
    }

}