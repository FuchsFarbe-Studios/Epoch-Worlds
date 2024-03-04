using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Client.Shared.Forms
{
    public partial class EpochMarkup
    {
        private ElementReference _codeBlock;
        private bool _previewMarkup = false;
        private MarkupString _previewString = new MarkupString("");

        /// <summary>
        ///   The label for the input.
        /// </summary>
        [Parameter] public string Label { get; set; }

        /// <summary>
        /// The placeholder for the input.
        /// </summary>
        [Parameter] public string Placeholder { get; set; }

        /// <summary>
        /// The help text for the input.
        /// </summary>
        [Parameter] public string HelpText { get; set; }

        /// <summary>
        /// The value of the input, to be represented in markup.
        /// </summary>
        [Parameter] public string MarkupString { get; set; } = "";

        /// <summary>
        /// The event callback for when the value of the input changes.
        /// </summary>
        [Parameter] public EventCallback<string> MarkupStringChanged { get; set; }

        [Parameter] public Expression<Func<string>> For { get; set; }

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