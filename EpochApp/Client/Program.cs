using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Client
{
    /// <summary>
    /// Le Program 😍😍😍
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // ReSharper disable once UnusedParameter.Local
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddTransient<MarkupService>();
            builder.Services.AddScoped<ILocalStorage, LocalStorageAccessor>();
            builder.Services.AddScoped<IManuscriptService, ManuscriptService>();
            builder.Services.AddScoped<IProfileSerivce, ProfileService>();
            builder.Services.AddScoped<ILookupService, LookupService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IWorldService, WorldService>();
            builder.Services.AddScoped<IFileService, UserFileService>();
            builder.Services.AddScoped<IBuilderService, BuilderService>();
            builder.Services.AddScoped<ClientAuthData>();// Storage
            builder.Services.AddScoped<EpochUserService>();// Service
            builder.Services.AddScoped<EpochAuthProvider>();// Auth provider
            builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<EpochAuthProvider>());

            builder.Services.AddAuthorizationCore();
            builder.Services.AddMudServices();
            ConfigBuilder.ConfigureCommonServices(builder.Services);
            await builder.Build().RunAsync();
        }
    }
}