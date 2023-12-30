// EpochWorlds
// LangOptions.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 29-12-2023
namespace EpochApp.Shared.Services
{

    /// <summary>
    ///     Language-specific options for database storage.
    /// </summary>
    public class LangOptions : ContentOptions
    {
        public string LangName { get; set; }
        public string Pronunciation { get; set; }

        public virtual PhonologyOptions Phonology { get; set; }
    }
}