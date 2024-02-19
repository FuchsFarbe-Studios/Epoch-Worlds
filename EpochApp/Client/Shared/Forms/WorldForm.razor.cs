using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared.Forms
{
    /// <summary>
    ///     A form for creating or editing a world.
    /// </summary>
    public partial class WorldForm
    {
        /// <summary>
        ///     Whether the form is in edit mode or not.
        /// </summary>
        [Parameter] public bool IsEditMode { get; set; } = false;

        /// <summary> The world to edit. </summary>
        [Parameter] public WorldDTO World { get; set; } = null!;


        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private ILogger<WorldForm> Logger { get; set; }
        [Inject] private HttpClient Client { get; set; }
        [Inject] private NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            World ??= new WorldDTO
                      {
                          DateCreated = DateTime.Now,
                          IsActiveWorld = true
                      };
            if (Auth?.CurrentUser?.UserID != Guid.Empty)
            {
                World.AuthorID = Auth.CurrentUser.UserID;
            }
        }

        /// <summary>
        ///     Handles the form submission of the world data.
        /// </summary>
        /// <param name="arg"> </param>
        private async Task HandleWorldSubmit(EditContext arg)
        {
            var world = arg.Model as WorldDTO;
            world.AuthorID = Auth.CurrentUser.UserID;

            if (IsEditMode)
            {
                // Update
                var response = await Client.PutAsJsonAsync<WorldDTO>($"api/v1/Worlds/{world.WorldID}", world);
                if (response.IsSuccessStatusCode)
                {
                    // Success
                    Logger.LogInformation("Updated world {WorldID}", world.WorldID);
                }
                else
                {
                    Logger.LogWarning("World failed to update!");
                }
            }
            else
            {
                // Create
                var response = await Client.PostAsJsonAsync<WorldDTO>("api/v1/Worlds/Create", world);
                if (response.IsSuccessStatusCode)
                    Nav.NavigateTo(NavRef.WorldNav.Index);
            }

        }
    }
}