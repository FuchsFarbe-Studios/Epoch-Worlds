// EpochWorlds
// VerificationDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    /// <summary>
    ///     The data transfer object for verifying the user's email.
    /// </summary>
    public class VerificationDTO
    {
        public string Token { get; set; }
    }
}