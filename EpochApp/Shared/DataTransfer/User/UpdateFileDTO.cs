// EpochWorlds
// UpdateFileDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class UpdateFileDTO
    {
        public long FileId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? WorldId { get; set; }
        public string Alias { get; set; }
        public string Alt { get; set; }
    }
}