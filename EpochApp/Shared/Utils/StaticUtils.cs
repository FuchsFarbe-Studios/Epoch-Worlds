// EpochWorlds
// Utils.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared.Utils
{
    public static class StaticUtils
    {
        public static class AwesomeIcons
        {
            public static readonly Dictionary<AwesomeIconType, string> FontDict = new Dictionary<AwesomeIconType, string>
                                                                                  {
                                                                                      { AwesomeIconType.Microchip, "fa-solid fa-microchip" },
                                                                                      { AwesomeIconType.Keyboard, "fa-solid fa-keyboard" },
                                                                                      { AwesomeIconType.Cube, "fa-solid fa-cube" },
                                                                                      { AwesomeIconType.File, "fa-solid fa-file" },
                                                                                      { AwesomeIconType.MagicWand, "fa-solid fa-wand-magic" },
                                                                                      { AwesomeIconType.Heart, "fa-solid fa-heart" },
                                                                                      { AwesomeIconType.Dragon, "fa-solid fa-dragon" },
                                                                                      { AwesomeIconType.Dice, "fa-solid fa-dice" },
                                                                                      { AwesomeIconType.D20, "fa-solid fa-dice-d20" },
                                                                                      { AwesomeIconType.Scroll, "fa-solid fa-scroll" },
                                                                                      { AwesomeIconType.Diamond, "fa-solid fa-diamond" },
                                                                                      { AwesomeIconType.SparkleWand, "fa-solid fa-wand-sparkles" },
                                                                                      { AwesomeIconType.Ghost, "fa-solid fa-ghost" },
                                                                                      { AwesomeIconType.Cloud, "fa-solid fa-cloud" },
                                                                                      { AwesomeIconType.Sun, "fa-solid fa-sun" },
                                                                                      { AwesomeIconType.Bolt, "fa-solid fa-bolt" },
                                                                                      { AwesomeIconType.Snowflake, "fa-solid fa-snowflake" },
                                                                                      { AwesomeIconType.Volcano, "fa-solid fa-volcano" },
                                                                                      { AwesomeIconType.Tornado, "fa-solid fa-tornado" },
                                                                                      { AwesomeIconType.Rainbow, "fa-solid fa-rainbow" },
                                                                                      { AwesomeIconType.Meteor, "fa-solid fa-meteor" },
                                                                                      { AwesomeIconType.Hurricane, "fa-solid fa-hurricane" },
                                                                                      { AwesomeIconType.CloudBolt, "fa-solid fa-cloud-bolt" },
                                                                                      { AwesomeIconType.CloudRain, "fa-solid fa-cloud-rain" },
                                                                                      { AwesomeIconType.Paperclip, "fa-solid fa-paperclip" },
                                                                                      { AwesomeIconType.City, "fa-solid fa-city" },
                                                                                      { AwesomeIconType.Globe, "fa-solid fa-globe" },
                                                                                      { AwesomeIconType.Building, "fa-solid fa-building" },
                                                                                      { AwesomeIconType.AddressCard, "fa-solid fa-address-card" },
                                                                                      { AwesomeIconType.Register, "fa-regular fa-registered" },
                                                                                      { AwesomeIconType.Trademark, "fa-solid fa-trademark" },
                                                                                      { AwesomeIconType.Copyright, "fa-regular fa-copyright" },
                                                                                      { AwesomeIconType.FloppyDisk, "fa-solid fa-floppy-disk" },
                                                                                      { AwesomeIconType.Table, "fa-solid fa-table" },
                                                                                      { AwesomeIconType.Compass, "fa-solid fa-compass" },
                                                                                      { AwesomeIconType.Skull, "fa-solid fa-skull" },
                                                                                      { AwesomeIconType.MortarPestle, "fa-solid fa-mortar-pestle" },
                                                                                      { AwesomeIconType.Radiation, "fa-solid fa-circle-radiation" },
                                                                                      { AwesomeIconType.Bone, "fa-solid fa-bone" },
                                                                                      { AwesomeIconType.Language, "fa-solid fa-language" },
                                                                                      { AwesomeIconType.EarthAfrica, "fa-solid fa-earth-africa" },
                                                                                      { AwesomeIconType.EarthOceania, "fa-solid fa-earth-oceania" },
                                                                                      { AwesomeIconType.EarthAsia, "fa-solid fa-earth-asia" },
                                                                                      { AwesomeIconType.EarthAmericas, "fa-solid fa-earth-americas" }
                                                                                  };
        }

        public static class Auth
        {
            public const string LoginNav = "/Auth/Login";
            public const string RegistrationNav = "/Auth/Register";
            public const string LogoutNav = "/Auth/Logout";
        }

        public static class Constants
        {
            public const string CompanyName = "FuchsFarbe Studios";
            public const string ProjectName = "The Epoch World Exchange";
            public const string AppName = "Epoch Worlds";
            public const string AppVersion = "0.0.1";
            public const string AppDescription = "A world exchange for the Epoch World Engine";
            public const string WorldFilesDirectory = "WorldContent";
            public const string UserFilesDirectory = "UserContent";
            public static readonly string[] Authors = { "Oliver Conover", "Eloi Genier" };
        }
    }
}