// EpochWorlds
// ManuscriptProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using AutoMapper;
using EpochApp.Shared;
using EpochApp.Shared.Articles;

namespace EpochApp.Server.Maps
{
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