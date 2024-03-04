// EpochWorlds
// ModerationService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Users;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Server.Services
{
    /// <summary>
    /// Service for user moderation.
    /// </summary>
    public class ModerationService : IUserModeration
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IUserModeration> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the moderation service.
        /// </summary>
        /// <param name="context"> The database context. </param>
        /// <param name="mapper"> The mapper. </param>
        /// <param name="logger"> The logger. </param>
        public ModerationService(EpochDataDbContext context, IMapper mapper, ILogger<ModerationService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // Report a user
        public async Task<UserReport> ReportUser(UserReportDTO report)
        {
            var accuser = await _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.UserID == report.PlaintiffId);
            var defendant = await _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.UserID == report.DefendantId);
            var accuserIsAdmin = accuser.UserRoles.Any(r => r.Role.Description == "ADMIN" || r.Role.Description == "INTERNAL");
            var defendantIsAdmin = defendant.UserRoles.Any(r => r.Role.Description == "ADMIN" || r.Role.Description == "INTERNAL");

            if (accuserIsAdmin && defendantIsAdmin)
            {
                _logger.LogWarning("Admins cannot report other admins!");
                return null;
            }

            var newReport = _mapper.Map(report, new UserReport());
            _context.UserReports.Add(newReport);
            await _context.SaveChangesAsync();
            return await Task.FromResult(newReport);
        }
    }
}