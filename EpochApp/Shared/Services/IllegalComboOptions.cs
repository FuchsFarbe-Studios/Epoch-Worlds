// EpochWorlds
// IllegalComboOptions.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 29-12-2023
namespace EpochApp.Shared.Services
{
    public class IllegalComboOptions
    {
        public Guid OptionsID { get; set; }
        public Guid OwnerID { get; set; }
        public Guid PhonologyID { get; set; }
        public Guid IllegalComboOptionsID { get; set; }
        public string IllegalCombos { get; set; }
        public bool BanSameVowelTwiceInARow { get; set; } = false;
        public bool BanSameSyllableTwiceInARow { get; set; } = false;

        public virtual PhonologyOptions PhonologyOpts { get; set; }
    }
}