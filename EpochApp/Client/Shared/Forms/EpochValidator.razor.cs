using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     A validator that can display errors.
    /// </summary>
    public partial class EpochValidator
    {
        private ValidationMessageStore _store;

        /// <summary> The edit context. </summary>
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
            // ReSharper disable once UnusedParameter.Local
            Context.OnValidationRequested += (s, e) => _store.Clear();
            // ReSharper disable once UnusedParameter.Local
            Context.OnFieldChanged += (s, e) => _store.Clear(e.FieldIdentifier);
        }

        #endregion

        /// <summary>
        ///     Displays the given errors.
        /// </summary>
        /// <param name="errors"> Errors to display. </param>
        public void DisplayErrors(Dictionary<string, List<string>> errors)
        {
            if (Context is not null)
            {
                foreach (var err in errors)
                    _store.Add(Context.Field(err.Key), err.Value);
                Context.NotifyValidationStateChanged();
            }
        }

        /// <summary>
        ///     Displays the given errors.
        /// </summary>
        /// <param name="errors"> Errors to display. </param>
        public void DisplayErrors(IEnumerable<string> errors)
        {
            if (Context is not null)
            {
                foreach (var err in errors)
                    _store.Add(Context.Field("Error!"), err);
                Context.NotifyValidationStateChanged();
            }
        }

        /// <summary> Clears all errors. </summary>
        public void ClearErrors()
        {
            _store?.Clear();
            Context?.NotifyValidationStateChanged();
        }
    }
}