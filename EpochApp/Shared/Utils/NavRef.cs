// EpochWorlds
// NavRef.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 2-2-2024
#pragma warning disable CS1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Shared.Utils
{
    public static class NavRef
    {
        public static class ArticleNav
        {
            public const string Index = "/Articles";
            public const string Create = "/Article/Create";
            public const string Edit = "/Article/Edit";
            public const string View = "/Article/View";
        }

        public static class Auth
        {
            public const string Login = "/Auth/Login";
            public const string Logout = "/Auth/Logout";
            public const string Registration = "/Auth/Register";
            public const string Reset = "/Auth/Reset";
            public const string Verification = "/Auth/Verification";
            public const string AccountRecovery = "/Auth/Forgot-Password";
        }

        public static class UserNav
        {
            public const string Account = "/User/Account";
            public const string Profile = "/User/Profile";
            public const string Settings = "/User/Settings";
            public const string Dashboard = "/Dashboard";
        }

        public static class AdminNav
        {
            public const string Index = "/Internal";
            public const string Blogs = "/Internal/Blogs";
            public const string Logs = "/Internal/Logs";
            public const string Contacts = "/Internal/Contacts";
            public const string Posts = "/Internal/Posts";
            public const string Users = "/Internal/Users";
            public const string Configuration = "/Internal/Configuration";
        }

        public static class ResourceNav
        {
            public const string Manuals = "/Resources/Manuals";
            public const string Tips = "/Resources/Tips";
            public const string Guides = "/Resources/Guides";
        }

        public static class WorldNav
        {
            public const string Index = "/Worlds";
            public const string Create = "/World/Create";
            public const string Edit = "/World/E";
            public const string Explore = "/Worlds/Explore";

            public static class Current
            {
                public const string View = "/World/Active";
                public const string Articles = "/World/Articles";
                public const string Files = "/World/Files";
            }
        }

        public static class BuilderNav
        {
            public const string Index = "/Builders";

            public static class CongLang
            {
                public const string Create = "/Builders/ConLang/C";
                public const string Edit = "/Builders/ConLang/E";
            }

            public static class Character
            {
                public const string Create = "/Builders/Character/C";
                public const string Edit = "/Builders/Character/E";
            }
        }

        public static class Site
        {
            public const string Index = "/";
            public const string About = "/About";
            public const string Contact = "/Contact";
            public const string Policy = "/Policy";
            public const string Support = "/Support";
            public const string Faq = "/FAQ";
            public const string Features = "/Features";
            public const string Dash = "/Dashboard";
        }
    }
}