// EpochWorlds
// File.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
using EpochApp.Shared.Users;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared
{
    /// <summary>
    ///     Represents a user-uploaded file.
    /// </summary>
    public class UserFile
    {
        public long FileId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? WorldId { get; set; }
        public string FileName { get; set; }
        public string SafeName { get; set; }
        public string Alias { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public string ImageAlt { get; set; }
        public DateTime? UploadedOn { get; set; }
        public DateTime? RemovedOn { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual World World { get; set; }
    }
}