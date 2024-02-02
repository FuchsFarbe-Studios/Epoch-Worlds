// EpochWorlds
// RefreshToken.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
namespace EpochApp.Shared
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}