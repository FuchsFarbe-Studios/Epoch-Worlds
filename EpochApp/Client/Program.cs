using EpochApp.Client;
using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace EpochApp.Client
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped<ILocalStorage, LocalStorageAccessor>();
			builder.Services.AddScoped<ClientAuthData>(); // Storage
			builder.Services.AddScoped<EpochUserService>(); // Service
			builder.Services.AddScoped<EpochAuthProvider>(); // Auth provider
			builder.Services.AddScoped<AuthenticationStateProvider>(sp=>sp.GetRequiredService<EpochAuthProvider>());
			builder.Services.AddAuthorizationCore();
			await builder.Build().RunAsync();
		}
	}
}