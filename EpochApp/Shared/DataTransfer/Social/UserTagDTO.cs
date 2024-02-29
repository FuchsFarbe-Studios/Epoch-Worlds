// EpochWorlds
// UserTagDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
namespace EpochApp.Shared
{
    public class UserTagDTO
    {
        public Guid UserId { get; set; }
        public long TagId { get; set; }
        public string Text { get; set; }
    }
}