// EpochWorlds
// ArticleTagDTO.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 23-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared
{
    public class ArticleTagDTO
    {
        public long TagId { get; set; }
        public Guid ArticleId { get; set; }
        public string Text { get; set; }
    }
}