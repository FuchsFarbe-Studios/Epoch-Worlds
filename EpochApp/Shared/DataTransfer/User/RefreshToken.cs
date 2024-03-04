// EpochWorlds
// RefreshToken.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}