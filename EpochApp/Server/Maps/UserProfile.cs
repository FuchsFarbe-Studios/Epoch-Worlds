// EpochWorlds
// UserProfile.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using EpochApp.Shared;
using EpochApp.Shared.Users;
using MapProfile=AutoMapper.Profile;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Server.Maps
{
    public class UserProfile : MapProfile
    {
        public UserProfile()
        {
            CreateMap<User, UserData>()
                .ForMember(x => x.UserID, opt => opt.MapFrom(x => x.UserID))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.Role.Description).ToList()))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.DateOfBirth));
            CreateMap<UserData, User>()
                .ForMember(x => x.UserID, opt => opt.MapFrom(x => x.UserID))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.UserRoles, opt => opt.MapFrom(x => x.Roles.Select(x => new UserRole { Role = new Role { Description = x } }).ToList()))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.DateOfBirth));
        }
    }
}