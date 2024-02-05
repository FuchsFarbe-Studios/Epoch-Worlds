// EpochWorlds
// CongLang.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;

namespace EpochApp.Shared.Builders
{
    public class CongLang
    {
        public Guid ConLangId { get; set; }
        public string LangName { get; set; }
        public string NativePronunciation { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }

        public Guid WorldId { get; set; }
        public virtual World World { get; set; }
    }

}