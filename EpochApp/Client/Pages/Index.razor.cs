// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace EpochApp.Client.Pages
{

    public class TestModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string? TextData { get; set; }
        public int IntData { get; set; } = 0;
        public float FloatData { get; set; } = 0.0f;
        public double DoubleData { get; set; } = 0.0;
        public decimal DecimalData { get; set; } = 0.0m;
        public bool BoolData { get; set; } = true;
        public DateTime DateData { get; set; } = DateTime.Now;
        public string SelectData { get; set; }
    }

    public partial class Index
    {
        private DataStore _dataStore = new DataStore();
        private TestModel _testModel = new TestModel();
        [Inject] public ILocalStorage Storage { get; set; }
        public string StoredValue { get; set; } = "";
        public int TabIndex { get; set; }

        public async Task SetValueAsync()
        {
            await Storage.SetValueAsync(_dataStore.Key, _dataStore.Value);
        }

        public async Task GetValueAsync()
        {
            StoredValue = await Storage.GetValueAsync<string>(_dataStore.Key);
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

        private Task RefreshData(EditContext editContext)
        {
            if (!editContext.Validate())
            {
                Console.WriteLine("Invalid");
            }
            else
            {
                Console.WriteLine("Validated");
                var mod = (TestModel)editContext.Model;
                Console.WriteLine(mod.TextData);
            }
            StateHasChanged();
            return Task.CompletedTask;
        }

        private Task OnItemSelected(string arg)
        {
            return null;
        }

        private class DataStore
        {
            public string Key { get; } = "";
            public string Value { get; set; } = "";
            public bool BoolVal { get; set; }
        }
    }
}