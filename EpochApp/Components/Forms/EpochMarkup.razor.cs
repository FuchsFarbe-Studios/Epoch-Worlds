using EpochApp.Components.Services;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace EpochApp.Components.Forms
{
    public partial class EpochMarkup
    {
        private bool _previewMarkup = false;
        private MarkupString _previewString = new MarkupString("");

        [Parameter] public string Label { get; set; }

        [Parameter] public string Placeholder { get; set; }

        [Parameter] public string HelpText { get; set; }

        [Parameter] public string MarkupString { get; set; } = "";

        [Parameter] public EventCallback<string> MarkupStringChanged { get; set; }

        [Parameter] public Expression<Func<string>>? For { get; set; }

        [Inject] private MarkupService MarkupService { get; set; }

        private async Task TogglePreviewAsync()
        {
            _previewMarkup = !_previewMarkup;
            if (_previewMarkup)
                _previewString = await MarkupService.TranslateMarkupAsync(MarkupString);
            await Task.CompletedTask;
        }

        private async Task UpdateMarkupString(string arg)
        {
            MarkupString = arg;
            await MarkupStringChanged.InvokeAsync(arg);
        }

        private Task AddHeadingMarkup()
        {
            MarkupString += "<h1></h1>";
            return Task.CompletedTask;
        }

        private Task AddItalicMarkup()
        {
            MarkupString += "<i></i>";
            return Task.CompletedTask;
        }

        private Task AddBoldMarkup()
        {
            MarkupString += "<b></b>";
            return Task.CompletedTask;
        }

        private Task AddParagraphMarkup()
        {
            MarkupString += "<p></p>";
            return Task.CompletedTask;
        }

        private Task AddStrikethroughMarkup()
        {
            MarkupString += "<s></s>";
            return Task.CompletedTask;
        }

        private Task AddUnderlineMarkup()
        {
            MarkupString += "<u></u>";
            return Task.CompletedTask;
        }
    }
}