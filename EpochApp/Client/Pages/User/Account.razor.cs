// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;

using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.User
{
	public partial class Account
	{
		[Inject] public EpochAuthProvider Auth { get; set; }
	}
}