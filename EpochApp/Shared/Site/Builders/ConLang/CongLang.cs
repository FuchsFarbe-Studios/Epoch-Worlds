// EpochWorlds
// CongLang.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
namespace EpochApp.Shared
{
    [Serializable]
    public class CongLang
    {
        public string LangName { get; set; }
        public string NativePronunciation { get; set; }
        public string Description { get; set; }

        public Phonology Phonology { get; set; } = new Phonology();
    }

}