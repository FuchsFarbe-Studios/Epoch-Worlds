using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///     A form to edit user manuscripts.
    /// </summary>
    public partial class ManuscriptForm
    {
        /// <summary>
        ///     If there is a manuscript to edit, it will be passed in here.
        /// </summary>
        [Parameter] public ManuscriptDTO Manuscript { get; set; } = null!;

        /// <summary>
        ///     Determines if this form is in edit mode.
        /// </summary>
        [Parameter] public bool IsEditMode { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (Manuscript == null)
                Model = new ManuscriptDTO
                        {
                            UserID = Auth.CurrentUser.UserID,
                            Chapters = new List<ManuscriptChapterDTO>()
                        };
            else
                Model = Manuscript;

            await base.OnInitializedAsync();
        }

        private Task OnManuscriptSubmit(EditContext arg)
        {
            Logger.LogInformation("Form Submitted 🥰");
            return Task.CompletedTask;
        }
    }
}