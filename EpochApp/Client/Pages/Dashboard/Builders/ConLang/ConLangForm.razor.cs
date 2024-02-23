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
        private SpellingRule _altSpellingRuleModel = new SpellingRule();
        private DerivedWord _derivedWordModel = new DerivedWord();
        private NounGender _genderModel = new NounGender();
        private SpellingRule _spellingRuleModel = new SpellingRule();
        private LangWord _wordModel = new LangWord();



        /// <summary>
        ///     The constructed language model for editing.
        /// </summary>
        [Parameter] public ConstructedLanguage Lang { get; set; }

        /// <inheritdoc />
        protected override async Task UpdateExistingBuilderContentAsync()
        {
            await SetBuilderContentNameAsync(ContentModel.LangName);
            await base.UpdateExistingBuilderContentAsync();
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