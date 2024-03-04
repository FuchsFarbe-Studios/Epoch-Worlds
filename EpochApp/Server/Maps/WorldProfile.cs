// EpochWorlds
// WorldProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using AutoMapper;
using EpochApp.Shared;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Server.Maps
{
    public class WorldProfile : Profile
    {
        public WorldProfile()
        {
            CreateMap<WorldDate, WorldDateDTO>();
            CreateMap<WorldDateDTO, WorldDate>();
            CreateMap<World, WorldDTO>().ForMember(dest => dest.WorldArticles, act => act.Ignore());
            CreateMap<WorldDTO, World>()
                .ForMember(dest => dest.WorldArticles, act => act.Ignore());
            CreateMap<WorldMeta, WorldMetaDTO>()
                .ForMember(x => x.TemplateId, opt => opt.MapFrom(x => x.MetaID))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.Template.Category.CategoryId));
            CreateMap<WorldMetaDTO, WorldMeta>()
                .ForMember(x => x.MetaID, opt => opt.MapFrom(x => x.TemplateId));
            CreateMap<WorldGenre, WorldGenreDTO>();
            CreateMap<WorldGenreDTO, WorldGenre>();
            CreateMap<WorldTag, WorldTagDTO>();
            CreateMap<WorldTagDTO, WorldTag>();
        }
    }
}