// EpochWorlds
// BuilderProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using AutoMapper;
using EpochApp.Shared;

namespace EpochApp.Server.Maps
{
    #pragma warning disable CS1591
    public class BuilderProfile : Profile
    {
        public BuilderProfile()
        {
            CreateMap<BuilderContent, BuilderContentDTO>();
            CreateMap<BuilderContentDTO, BuilderContent>();
        }
    }
}