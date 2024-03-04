// EpochWorlds
// UserFileDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class UserFileDTO
    {
        public long FileId { get; set; }
        public string FilePath { get; set; }
        public string FileData { get; set; }
        public string SafeName { get; set; }
        public string Alias { get; set; }
        public string AltText { get; set; }
        public DateTime? UploadedOn { get; set; }
    }
}