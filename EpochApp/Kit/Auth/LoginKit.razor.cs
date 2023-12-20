// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.DataTransfer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Kit.Auth
{
    public partial class LoginKit
    {
        private Boolean _attemptingLogin = false;
        private Dictionary<String, List<String>> _errors = new Dictionary<String, List<String>>();
        private LoginDTO _login = new LoginDTO();
        [Inject] private NavigationManager Nav { get; set; }

        public async Task AttemptLoginAsync(EditContext ctx)
        {
            await Task.CompletedTask;
        }
    }
}