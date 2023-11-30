// EpochWorlds
// EpochDictionary.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochComponents.Enums;

namespace EpochComponents.Utils
{
    public static class EpochDictionary
    {
        public static readonly Dictionary<ColorStyle, string> ColorDictionary = new Dictionary<ColorStyle, string>()
        {
            { ColorStyle.Blue, "style-blue" },
            { ColorStyle.Green, "style-green" },
            { ColorStyle.Yellow, "style-yellow" },
            { ColorStyle.Orange, "style-orange" },
            { ColorStyle.Red, "style-red" },
            { ColorStyle.Pink, "style-pink" },
            { ColorStyle.Purple, "style-purple" },
            { ColorStyle.Light, "style-light" },
            { ColorStyle.Dark, "style-dark" },
            { ColorStyle.LightLight, "style-light-light" },
            { ColorStyle.LightDark, "style-dark-light" },
            { ColorStyle.LightBlue, "style-blue-light" },
            { ColorStyle.LightGreen, "style-green-light" },
            { ColorStyle.LightYellow, "style-yellow-light" },
            { ColorStyle.LightOrange, "style-orange-light" },
            { ColorStyle.LightRed, "style-red-light" },
            { ColorStyle.LightPink, "style-pink-light" },
            { ColorStyle.LightPurple, "style-purple-light" },
        };

        public static readonly Dictionary<EpochTypoType, string> TypoDictionary = new Dictionary<EpochTypoType, string>()
        {
          {EpochTypoType.H1, "text-5x"},
          {EpochTypoType.H2, "text-4x"},
          {EpochTypoType.H3, "text-3x"},
          {EpochTypoType.H4, "text-2x"},
          {EpochTypoType.H5, "text-lg"},
          {EpochTypoType.H6, "text-md"},
          {EpochTypoType.Body1, "text-sm"},
          {EpochTypoType.Body2, "text-sm"},
          {EpochTypoType.Mono, "text-xs"},
        };

        public static readonly Dictionary<ColorStyle, string> TextColorDictionary = new Dictionary<ColorStyle, string>()
        {
            { ColorStyle.Blue, "color-blue" },
            { ColorStyle.Green, "color-green" },
            { ColorStyle.Yellow, "color-yellow" },
            { ColorStyle.Orange, "color-orange" },
            { ColorStyle.Red, "color-red" },
            { ColorStyle.Pink, "color-pink" },
            { ColorStyle.Purple, "color-purple" },
            { ColorStyle.Light, "color-light" },
            { ColorStyle.Dark, "color-dark" },
            { ColorStyle.LightLight, "color-light-light" },
            { ColorStyle.LightDark, "color-dark-light" },
            { ColorStyle.LightBlue, "color-blue-light" },
            { ColorStyle.LightGreen, "color-green-light" },
            { ColorStyle.LightYellow, "color-yellow-light" },
            { ColorStyle.LightOrange, "color-orange-light" },
            { ColorStyle.LightRed, "color-red-light" },
            { ColorStyle.LightPink, "color-pink-light" },
            { ColorStyle.LightPurple, "color-purple-light" },
        };

        public static readonly Dictionary<EpochState, string> StateDictionary = new Dictionary<EpochState, string>()
        {
            { EpochState.Success, "style-success" },
            { EpochState.Warning, "style-warning" },
            { EpochState.Danger, "style-danger" },
            { EpochState.Info, "style-info" },
            { EpochState.LightSuccess, "style-success-light" },
            { EpochState.LightWarning, "style-warning-light" },
            { EpochState.LightDanger, "style-danger-light" },
            { EpochState.LightInfo, "style-info-light" }
        };
    }
}