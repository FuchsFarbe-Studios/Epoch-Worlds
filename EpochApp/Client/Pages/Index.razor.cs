// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages
{
    public partial class Index
    {
        [Inject] private ILogger<Index> Logger { get; set; }
        [Inject] public ILocalStorage Storage { get; set; }
    }
}