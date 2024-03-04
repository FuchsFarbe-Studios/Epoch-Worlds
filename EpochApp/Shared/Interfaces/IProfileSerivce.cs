// EpochWorlds
// IProfileSerivce.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
#pragma warning disable CS1591
namespace EpochApp.Shared
{
    public interface IProfileSerivce
    {
        Task<ProfileDTO> GetProfileByUsername(string userName);

        Task<ProfileDTO> GetProfileByUserIdAsync(Guid userId);

        Task<ProfileDTO> UpdateProfile(Guid userId, ProfileDTO profile);
    }
}