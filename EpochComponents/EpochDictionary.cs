// EpochWorlds
// EpochDictionary.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochComponents.Enums;

namespace EpochComponents
{
    public static class EpochDictionary
    {
        public static readonly Dictionary<EpochColor, string> ColorDictionary = new Dictionary<EpochColor, string>()
        {
            { EpochColor.Blue, "style-blue" },
            { EpochColor.Green, "style-green" },
            { EpochColor.Yellow, "style-yellow" },
            { EpochColor.Orange, "style-orange" },
            { EpochColor.Red, "style-red" },
            { EpochColor.Pink, "style-pink" },
            { EpochColor.Purple, "style-purple" },
            { EpochColor.Light, "style-light" },
            { EpochColor.Dark, "style-dark" },
            { EpochColor.LightLight, "style-light-light" },
            { EpochColor.LightDark, "style-dark-light" },
            { EpochColor.LightBlue, "style-blue-light" },
            { EpochColor.LightGreen, "style-green-light" },
            { EpochColor.LightYellow, "style-yellow-light" },
            { EpochColor.LightOrange, "style-orange-light" },
            { EpochColor.LightRed, "style-red-light" },
            { EpochColor.LightPink, "style-pink-light" },
            { EpochColor.LightPurple, "style-purple-light" },
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

        public static readonly Dictionary<EpochColor, string> TextColorDictionary = new Dictionary<EpochColor, string>()
        {
            { EpochColor.Blue, "color-blue" },
            { EpochColor.Green, "color-green" },
            { EpochColor.Yellow, "color-yellow" },
            { EpochColor.Orange, "color-orange" },
            { EpochColor.Red, "color-red" },
            { EpochColor.Pink, "color-pink" },
            { EpochColor.Purple, "color-purple" },
            { EpochColor.Light, "color-light" },
            { EpochColor.Dark, "color-dark" },
            { EpochColor.LightLight, "color-light-light" },
            { EpochColor.LightDark, "color-dark-light" },
            { EpochColor.LightBlue, "color-blue-light" },
            { EpochColor.LightGreen, "color-green-light" },
            { EpochColor.LightYellow, "color-yellow-light" },
            { EpochColor.LightOrange, "color-orange-light" },
            { EpochColor.LightRed, "color-red-light" },
            { EpochColor.LightPink, "color-pink-light" },
            { EpochColor.LightPurple, "color-purple-light" },
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