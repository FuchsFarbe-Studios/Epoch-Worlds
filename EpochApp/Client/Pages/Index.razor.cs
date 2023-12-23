// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Kit.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages
{

    public partial class Index
    {

        private DataStore _dataStore = new DataStore();
        [Inject] public ILocalStorage Storage { get; set; }
        public string StoredValue { get; set; } = "";

        public async Task SetValueAsync()
        {
            await Storage.SetValueAsync(_dataStore.Key, _dataStore.Value);
        }

        public async Task GetValueAsync()
        {
            StoredValue = await Storage.GetValueAsync<String>(_dataStore.Key);
        }

        public async Task RemoveAsync()
        {
            await Storage.RemoveAsync(_dataStore.Key);
        }

        public async Task ClearAllAsync()
        {
            await Storage.Clear();
            StoredValue = "";
        }

        private class DataStore
        {
            public String Key { get; } = "";
            public String Value { get; set; } = "";
            public Boolean BoolVal { get; set; }
        }
    }
}