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
        private ManuscriptDTO _model = new ManuscriptDTO();

        [Inject] private ILogger<ManuscriptForm> Logger { get; set; }

        private Task OnManuscriptSubmit(EditContext arg)
        {
            Logger.LogInformation("Form Submitted 🥰");
            return Task.CompletedTask;
        }
    }
}