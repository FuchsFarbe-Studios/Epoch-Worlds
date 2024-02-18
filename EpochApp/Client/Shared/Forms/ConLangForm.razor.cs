using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Shared.Forms
{
    public partial class ConLangForm
    {
        [Parameter] public ConstructedLanguage Lang { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Lang ??= new ConstructedLanguage();
        }

        private async Task SaveNewLanguageAsync(EditContext ctx)
        {
            Lang.CreatedOn = DateTime.Now;
            await Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override async Task GenerateAsync()
        {
            await base.GenerateAsync();
        }
    }
}