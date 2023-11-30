// EpochWorlds
// BlogTypeInfo.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

namespace EpochApp.Shared
{
    public enum BlogTypeInfo
    {
        GENERAL = 0,
        NEWS = 1,
        UPDATES = 2 >> 1,
        EVENTS = 3 >> 1,
        FAQ = 4 >> 1,
        DOCUMENTATION = 5 >> 1,
    }
}