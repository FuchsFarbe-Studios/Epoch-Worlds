// EpochWorlds
// ArticleProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 1-3-2024
using AutoMapper;
using EpochApp.Shared;
using EpochApp.Shared.Articles;

namespace EpochApp.Server.Maps
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();
        }
    }

    public class ManuscriptProfile : Profile
    {
        public ManuscriptProfile()
        {
            CreateMap<Manuscript, ManuscriptDTO>();
            CreateMap<ManuscriptDTO, Manuscript>();
            CreateMap<ManuscriptChapter, ManuscriptChapterDTO>();
            CreateMap<ManuscriptChapterDTO, ManuscriptChapter>();
        }
    }
}