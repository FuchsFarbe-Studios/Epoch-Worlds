// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;

using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages
{
	public partial class Index
	{
		[Inject] public ILocalStorage Storage { get; set; }
		public string Key { get; set; } = "";
		public string Value { get; set; } = "";
		public string StoredValue { get; set; } = "";

		public async Task SetValueAsync()
		{
			await Storage.SetValueAsync(Key, Value);
		}

		public async Task GetValueAsync()
		{
			StoredValue = await Storage.GetValueAsync<string>(Key);
		}

		public async Task RemoveAsync()
		{
			await Storage.RemoveAsync(Key);
		}

		public async Task ClearAllAsync()
		{
			await Storage.Clear();
			StoredValue = "";
		}
	}
}