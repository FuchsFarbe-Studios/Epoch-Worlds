using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Builders
{
    /// <summary>
    ///     A form for creating or editing a constructed language.
    /// </summary>
    public partial class ConLangForm
    {
        private SpellingRule _altSpellingRuleModel = new SpellingRule();
        private DerivedWord _derivedWordModel = new DerivedWord();
        private NounGender _genderModel = new NounGender();
        private bool _generatingContent = false;
        private ConstructedLanguageResult _results = null!;
        private bool _savingOrUpdatingContent = false;
        private SpellingRule _spellingRuleModel = new SpellingRule();
        private LangWord _wordModel = new LangWord();

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


        /// <inheritdoc />
        protected override async Task GenerateAsync(EditContext ctx)
        {
            _generatingContent = true;
            await base.GenerateAsync(ctx);
            // await HandleConLangSubmit(ctx);

            await Task.Delay(1000);
            var response = await Client.GetFromJsonAsync<BuilderContent>($"api/v1/Builder/GeneratedContent?contentId={Content.ContentID}&userId={Auth.CurrentUser.UserID}");
            if (response == null)
                return;

            Content = response;
            _results = await Serializer.DeserializeFromXmlAsync<ConstructedLanguageResult>(Content.GeneratedXml);
            _generatingContent = false;
            StateHasChanged();
        }

        private async Task HandleConLangSubmit(EditContext ctx)
        {
            if (!IsEditMode)
            {
                Content = new BuilderContent
                          {
                              AuthorID = Auth.CurrentUser.UserID,
                              WorldID = ActiveWorld.WorldID,
                              ContentName = Lang?.LangName,
                              ContentType = ContentType.ConstructedLanguage,
                              DateCreated = DateTime.Now
                          };
                var contextXml = await Serializer.SerializeToXmlAsync(Lang) ?? "";
                Content.ContentXml = contextXml;
                Logger.LogInformation(contextXml);
                await SaveNewLanguageAsync(ctx);
            }

            if (IsEditMode)
                await UpdateConLangAsync(ctx);
        }

        private async Task SaveNewLanguageAsync(EditContext ctx)
        {
            _savingOrUpdatingContent = true;
            var newLang = ctx.Model as ConstructedLanguage;
            newLang.CreatedOn = DateTime.Now;

            // Send data to database
            var response = await Client.PostAsJsonAsync<BuilderContent>("api/v1/Builder/Content", Content);
            // Verify it has been saved to database
            if (!response.IsSuccessStatusCode)
                return;

            var newContent = await response.Content.ReadFromJsonAsync<BuilderContent>();
            _savingOrUpdatingContent = false;
            // If it has, redirect to the edit language page with the new content ID
            Nav.NavigateTo($"{NavRef.BuilderNav.CongLang.Edit}/{newContent.ContentID}");
        }

        private async Task UpdateConLangAsync(EditContext ctx)
        {
            _savingOrUpdatingContent = true;
            var newLang = ctx.Model as ConstructedLanguage;
            Content.ContentName = newLang.LangName;
            Content.ContentXml = await Serializer.SerializeToXmlAsync(newLang) ?? "";
            var response = await Client.PutAsJsonAsync<BuilderContent>($"api/v1/Builder/Content?userId={Auth.CurrentUser.UserID}&contentId={Content.ContentID}", Content);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogWarning("Updating language un-successful!");
                var error = await response.Content.ReadAsStringAsync();
                Logger.LogWarning(error);
                return;
            }
            var updatedContent = await response.Content.ReadFromJsonAsync<BuilderContent>();
            Logger.LogInformation("Updating language successful!");
            _savingOrUpdatingContent = false;
            Nav.NavigateTo($"{NavRef.BuilderNav.CongLang.Edit}/{updatedContent.ContentID}");
        }

        private Task HandleSpellingRuleSubmitted(EditContext arg)
        {
            var rule = arg.Model as SpellingRule;
            if (rule.Order == 0)
                rule.Order = Lang.Spelling.SpellingRules.Count + 1;
            Lang.Spelling.SpellingRules.Add(rule);
            _spellingRuleModel = new SpellingRule
                                 { Order = Lang.Spelling.SpellingRules.Count + 1 };
            StateHasChanged();
            return Task.CompletedTask;
        }

        private Task HandleAltSpellingRuleSubmitted(EditContext arg)
        {
            var rule = arg.Model as SpellingRule;
            if (rule.Order == 0)
                rule.Order = Lang.Spelling.SecondSpelling.Count + 1;
            Lang.Spelling.SecondSpelling.Add(rule);
            _altSpellingRuleModel = new SpellingRule
                                    { Order = Lang.Spelling.SecondSpelling.Count + 1 };
            StateHasChanged();
            return Task.CompletedTask;
        }

        private Task HandleNounGenderSubmitted(EditContext arg)
        {
            var noun = arg.Model as NounGender;
            Lang.Grammar.NounGenders.Add(noun);
            _genderModel = new NounGender();
            StateHasChanged();
            return Task.CompletedTask;
        }

        private Task HandleDerivedWordSubmitted(EditContext arg)
        {
            var word = arg.Model as DerivedWord;
            Lang.Vocabulary.DerivedWords.Add(word);
            _derivedWordModel = new DerivedWord();
            StateHasChanged();
            return Task.CompletedTask;
        }

        private Task OnVocabSumbitted(EditContext arg)
        {
            var word = arg.Model as LangWord;
            Lang.Vocabulary.SavedWords.Add(word);
            _wordModel = new LangWord();
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}