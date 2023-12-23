using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Kit.Forms
{
    public partial class EpochValidator
    {
        private ValidationMessageStore _store;
        [CascadingParameter] public EditContext Context { get; set; }

        #region Overrides of ComponentBase

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            if (Context == null)
            {
                throw new InvalidOperationException($"{nameof(EpochValidator)} requires a cascading " + $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(EpochValidator)} " + $"inside an {nameof(EditForm)}.");
            }
            _store = new ValidationMessageStore(Context);
            Context.OnValidationRequested += (s, e) => _store.Clear();
            Context.OnFieldChanged += (s, e) => _store.Clear(e.FieldIdentifier);
        }

        #endregion

        public void DisplayErrors(Dictionary<string, List<string>> errors)
        {
            if (Context is not null)
            {
                foreach (var err in errors)
                    _store.Add(Context.Field(err.Key), err.Value);
                Context.NotifyValidationStateChanged();
            }
        }
        public void ClearErrors()
        {
            _store?.Clear();
            Context?.NotifyValidationStateChanged();
        }
    }
}