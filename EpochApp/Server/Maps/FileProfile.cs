// EpochWorlds
// FileProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using AutoMapper;
using EpochApp.Shared;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Server.Maps
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<UserFile, UserFileDTO>()
                .ForMember(x => x.AltText, opt => opt.MapFrom(x => x.ImageAlt));
            CreateMap<UserFileDTO, UserFile>()
                .ForMember(x => x.ImageAlt, opt => opt.MapFrom(x => x.AltText));
            CreateMap<UserFile, UserFileDTO>()
                .ForMember(x => x.FilePath, opt => opt.MapFrom(x => x.FilePath))
                .ForMember(x => x.Alias, opt => opt.MapFrom(x => x.Alias))
                .ForMember(x => x.SafeName, opt => opt.MapFrom(x => x.SafeName))
                .ForMember(x => x.AltText, opt => opt.MapFrom(x => x.ImageAlt));
            CreateMap<UserFileDTO, UserFile>()
                .ForMember(x => x.FilePath, opt => opt.MapFrom(x => x.FilePath))
                .ForMember(x => x.Alias, opt => opt.MapFrom(x => x.Alias))
                .ForMember(x => x.SafeName, opt => opt.MapFrom(x => x.SafeName))
                .ForMember(x => x.ImageAlt, opt => opt.MapFrom(x => x.AltText));
        }
    }
}