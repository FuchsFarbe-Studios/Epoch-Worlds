// EpochWorlds
// Extensions.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared.Utils
{
    public static class Extensions
    {
        public static string ToDescriptionString(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if ((object)field == null)
                return value.ToString().ToLower();

            return !(Attribute.GetCustomAttributes(field, typeof(DescriptionAttribute), false) is DescriptionAttribute[] customAttributes) || customAttributes.Length <= 0
                       ? value.ToString().ToLower()
                       : customAttributes[0].Description;
        }
    }
}