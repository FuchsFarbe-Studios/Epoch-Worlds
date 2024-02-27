// EpochWorlds
// FileUploadDto.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
namespace EpochApp.Shared
{
    public class FileUploadDto
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileData { get; set; }
        public string Alias { get; set; }
        public string Alt { get; set; }
        public Guid UserId { get; set; }
        public Guid? WorldId { get; set; }
    }
}