using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///    The manuscripts page.
    /// </summary>
    public partial class Manuscripts
    {
        private List<ManuscriptDTO> _manuscripts = new List<ManuscriptDTO>();

        [Inject] private IManuscriptService ManuscriptService { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var manuscripts = await ManuscriptService.GetUserManuscripts(Auth.CurrentUser.UserID);
            _manuscripts = manuscripts ?? new List<ManuscriptDTO>();
            await base.OnInitializedAsync();
        }
    }
}