// EpochWorlds
// UserProfile.cs
// FuchsFarbe Studios 2024
// Modified: 3-3-2024
using EpochApp.Shared;
using EpochApp.Shared.Users;
using MapProfile=AutoMapper.Profile;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Server.Maps
{
    public class UserMapProfile : MapProfile
    {
        public UserMapProfile()
        {
            CreateMap<UserReport, UserReportDTO>();
            CreateMap<UserReportDTO, UserReport>();
            CreateMap<BanTicket, BanTicketDTO>();
            CreateMap<BanTicketDTO, BanTicket>();
            CreateMap<ProfileDTO, Profile>();
            CreateMap<Profile, ProfileDTO>()
                .ForMember(x => x.MemberSince, opt => opt.MapFrom(x => x.User.DateCreated))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.User.DateOfBirth))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email));
            CreateMap<UserSocial, UserSocialDTO>();
            CreateMap<UserSocialDTO, UserSocial>();
            CreateMap<User, UserData>()
                .ForMember(x => x.UserID, opt => opt.MapFrom(x => x.UserID))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(userRole => userRole.Role.Description).ToList()))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.DateOfBirth));
            CreateMap<UserData, User>()
                .ForMember(x => x.UserID, opt => opt.MapFrom(x => x.UserID))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.UserRoles, opt => opt.MapFrom(x => x.Roles.Select(description => new UserRole { Role = new Role { Description = description } }).ToList()))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.DateOfBirth));
        }
    }
}