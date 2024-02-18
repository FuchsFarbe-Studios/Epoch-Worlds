using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Pages.Dashboard.Builders
{
    /// <summary>
    ///     A form for creating or editing a constructed language.
    /// </summary>
    public partial class ConLangForm
    {
        /// <summary>
        ///     Whether the form is in edit mode or create mode.
        /// </summary>
        [Parameter] public bool IsEditMode { get; set; } = false;

        /// <summary>
        ///     The constructed language model for editing.
        /// </summary>
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