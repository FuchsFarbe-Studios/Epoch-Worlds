// EpochWorlds
// ProfileService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS1591
namespace EpochApp.Server.Services
{
    public class ProfileService : IProfileSerivce
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IProfileSerivce> _logger;
        private readonly IMapper _mapper;

        public ProfileService(ILogger<ProfileService> logger, IMapper mapper, EpochDataDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProfileDTO> GetProfileByUsername(string userName)
        {
            var user = await _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                _logger.LogWarning("User not found!");
                return null;
            }
            var profile = _mapper.Map(user, new ProfileDTO());
            return await Task.FromResult(profile);
        }

        public async Task<ProfileDTO> GetProfileByUserId(Guid userId)
        {
            var user = await _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null)
            {
                _logger.LogWarning("User not found!");
                return null;
            }
            var profile = _mapper.Map(user, new ProfileDTO());
            return await Task.FromResult(profile);
        }

        public async Task<ProfileDTO> UpdateProfile(Guid userId, ProfileDTO profile)
        {
            if (userId != profile.UserId)
            {
                _logger.LogWarning("User ID does not match profile ID!");
                return null;
            }
            var user = await _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.UserName == profile.UserName);
            if (user == null)
            {
                _logger.LogWarning("User not found!");
                return null;
            }
            _mapper.Map(profile, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return await Task.FromResult(_mapper.Map(user, new ProfileDTO()));
        }
    }
}