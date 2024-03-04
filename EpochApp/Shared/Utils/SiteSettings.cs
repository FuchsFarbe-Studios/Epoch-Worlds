// EpochWorlds
// ClientSettings.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using EpochApp.Shared.Client;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared.Utils
{
    public class SiteSettings
    {
        public List<ClientSetting> AboutSettings { get; set; } = new List<ClientSetting>();
        public List<ClientSetting> FAQSettings { get; set; } = new List<ClientSetting>();
        public List<ClientSetting> PolicySettings { get; set; } = new List<ClientSetting>();
        public List<ClientSetting> ToSSettings { get; set; } = new List<ClientSetting>();
        public ContactSettings ContactSettings { get; set; }
    }

    public class ContactSettings
    {
        public ContactSettings(List<ClientSetting> settings)
        {
            CompanyName = settings.FirstOrDefault(s => s.SettingField == "Name")?.SettingValue;
            CompanyAddress = settings.FirstOrDefault(s => s.SettingField == "Address")?.SettingValue;
            CompanyPhone = settings.FirstOrDefault(s => s.SettingField == "Phone")?.SettingValue;
            SupportEmail = settings.FirstOrDefault(s => s.SettingField == "SupportEmail")?.SettingValue;
            SiteName = settings.FirstOrDefault(s => s.SettingField == "SiteName")?.SettingValue;
            ContactLink = settings.FirstOrDefault(s => s.SettingField == "ContactLink")?.SettingValue;
        }

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string SupportEmail { get; set; }
        public string SiteName { get; set; }
        public string ContactLink { get; set; }
    }

}