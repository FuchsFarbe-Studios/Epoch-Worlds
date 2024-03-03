// EpochWorlds
// ManuscriptProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using AutoMapper;
using EpochApp.Shared;
using EpochApp.Shared.Articles;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

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