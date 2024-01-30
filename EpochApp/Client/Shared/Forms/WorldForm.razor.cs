using EpochApp.Shared;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Shared.Forms
{
    /// <summary>
    ///     The world form component.
    /// </summary>
    public partial class WorldForm
    {
        /// <summary> The user data. </summary>
        [Parameter] public UserData UserData { get; set; }

        /// <summary> The world. </summary>
        [Parameter] public World World { get; set; } = new World();
        [Inject] public HttpClient Client { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override void OnParametersSet()
        {
            if (UserData != null && World != null)
                World.OwnerID = UserData.UserID;
            base.OnParametersSet();
        }

        private Task SaveWorldAsync(EditContext arg)
        {
            return null;
        }
    }
}