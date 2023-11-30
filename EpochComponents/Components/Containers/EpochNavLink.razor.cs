// EpochWorlds
// EpochDictionary.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace EpochComponents.Components.Containers
{
	public partial class EpochNavLink
	{
		[Parameter] public RenderFragment ChildContent { get; set; }
		[Parameter] public string Link { get; set; } = "#";
		[Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.All;
	}
}