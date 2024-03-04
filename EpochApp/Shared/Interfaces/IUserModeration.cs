// EpochWorlds
// IUserModeration.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using EpochApp.Shared.Users;

// ReSharper disable UnusedMember.Global

namespace EpochApp.Shared
{
    /// <summary>
    /// Interface for user moderation.
    /// </summary>
    public interface IUserModeration
    {
        /// <summary>
        /// Report a user.
        /// </summary>
        /// <param name="report"> The report. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="UserReport"/>. </returns>
        Task<UserReport> ReportUserAsync(UserReportDTO report);
    }
}