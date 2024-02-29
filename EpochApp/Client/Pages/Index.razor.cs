// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages
{
    /// <summary> Index page. </summary>
    public partial class Index
    {
        [Inject] private ILogger<Index> Logger { get; set; }
        [Inject] private ILocalStorage Storage { get; set; }
    }
}