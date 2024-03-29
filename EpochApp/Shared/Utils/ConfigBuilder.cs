// EpochWorlds
// AppConfig.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 18-2-2024
using EpochApp.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Shared.Utils
{
    public static class ConfigBuilder
    {
        public static void ConfigureCommonServices(IServiceCollection builderServices)
        {
            builderServices.AddScoped<ISerializationService, SerializationService>();
        }
    }
}