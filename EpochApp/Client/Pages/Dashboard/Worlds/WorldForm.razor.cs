using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Pages.Dashboard.Worlds
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
        [Parameter] public UserWorldDTO World { get; set; } = null!;

        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private ILogger<WorldForm> Logger { get; set; }
        [Inject] private IWorldService Client { get; set; }
        [Inject] private NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            World ??= new UserWorldDTO
                      {
                          DateCreated = DateTime.Now,
                          IsActiveWorld = true,
                          CurrentWorldDate = new WorldDateDTO
                                             {
                                                 CurrentDay = 1,
                                                 CurrentMonth = 1,
                                                 CurrentYear = 1
                                             }
                      };
            if (Auth?.CurrentUser?.UserID != Guid.Empty)
                World.OwnerId = Auth.CurrentUser.UserID;
        }

        /// <summary>
        ///     Handles the form submission of the world data.
        /// </summary>
        /// <param name="ctx"> The edit context. </param>
        private async Task HandleWorldSubmit(EditContext ctx)
        {
            var world = ctx.Model as UserWorldDTO;
            world.OwnerId = Auth.CurrentUser.UserID;

            if (IsEditMode)
            {
                // Update
                var response = await Client.UpdateWorldAsync(world);
                if (response != null)
                    Nav.NavigateTo(NavRef.WorldNav.Index);
                else
                    Nav.NavigateTo(NavRef.WorldNav.Index);
            }
            else
            {
                // Create
                var newWorld = await Client.CreateWorldAsync(world);
                Nav.NavigateTo(NavRef.WorldNav.Index);
            }
        }
    }
}