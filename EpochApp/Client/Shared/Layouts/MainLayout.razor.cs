using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <inheritdoc />
    public partial class MainLayout
    {
        private List<ClientSetting> _clientSettings = new List<ClientSetting>();
        private bool _drawerOpen;
        private bool _isDarkMode = false;
        private WorldDTO _newWorld;
        private SiteSettings _settings = new SiteSettings();

        private MudTheme _theme = new MudTheme
                                  {
                                      Palette = new PaletteLight
                                                {
                                                    Black = new MudColor("#090D10"),
                                                    White = new MudColor("#FFFFFF"),
                                                    Primary = new MudColor("#3EBCF7"),
                                                    PrimaryContrastText = new MudColor("#FFFFFF"),
                                                    Secondary = new MudColor("#CB7CFF"),
                                                    SecondaryContrastText = new MudColor("#FFFFFF"),
                                                    Tertiary = new MudColor("#FF96D7"),
                                                    TertiaryContrastText = new MudColor("#FFFFFF"),
                                                    Info = new MudColor("#1CB0F6"),
                                                    InfoContrastText = new MudColor("#FFFFFF"),
                                                    Success = new MudColor("#58CC02"),
                                                    SuccessContrastText = new MudColor("#FFFFFF"),
                                                    Warning = new MudColor("#FF9600"),
                                                    WarningContrastText = new MudColor("#FFFFFF"),
                                                    Error = new MudColor("#FF4B4B"),
                                                    ErrorContrastText = new MudColor("#FFFFFF"),
                                                    Dark = new MudColor("#2E4051"),
                                                    DarkContrastText = new MudColor("#FFFFFF"),
                                                    TextPrimary = new MudColor("#090D10"),
                                                    //TextSecondary = new MudColor("#CB7CFF"),
                                                    TextDisabled = new MudColor("#D2DCE6")
                                                    // ActionDefault = null,
                                                    // ActionDisabled = null,
                                                    // ActionDisabledBackground = null,
                                                    // Background = null,
                                                    // BackgroundGrey = null,
                                                    // Surface = null,
                                                    // DrawerBackground = null,
                                                    // DrawerText = null,
                                                    // DrawerIcon = null,
                                                    // AppbarBackground = null,
                                                    // AppbarText = null,
                                                    // LinesDefault = null,
                                                    // LinesInputs = null,
                                                    // TableLines = null,
                                                    // TableStriped = null,
                                                    // TableHover = null,
                                                    // Divider = null,
                                                    // DividerLight = null,
                                                    // ChipDefault = null,
                                                    // ChipDefaultHover = null,
                                                    // PrimaryDarken = null,
                                                    // PrimaryLighten = null,
                                                    // SecondaryDarken = null,
                                                    // SecondaryLighten = null,
                                                    // TertiaryDarken = null,
                                                    // TertiaryLighten = null,
                                                    // InfoDarken = null,
                                                    // InfoLighten = null,
                                                    // SuccessDarken = null,
                                                    // SuccessLighten = null,
                                                    // WarningDarken = null,
                                                    // WarningLighten = null,
                                                    // ErrorDarken = null,
                                                    // ErrorLighten = null,
                                                    // DarkDarken = null,
                                                    // DarkLighten = null,
                                                    // HoverOpacity = 0,
                                                    // GrayDefault = null,
                                                    // GrayLight = null,
                                                    // GrayLighter = null,
                                                    // GrayDark = null,
                                                    // GrayDarker = null,
                                                    // OverlayDark = null,
                                                    // OverlayLight = null
                                                },
                                      //PaletteDark = null,
                                      Typography = new Typography
                                                   {
                                                       Default = new Default
                                                                 {
                                                                     FontFamily = new[]
                                                                                  {
                                                                                      "Nunito", "sans-serif"
                                                                                  }
                                                                 },
                                                       H1 = new H1
                                                            {
                                                                FontFamily = new[]
                                                                             {
                                                                                 "Nunito", "sans-serif"
                                                                             }
                                                            },
                                                       H2 = new H2
                                                            {
                                                                FontFamily = new[]
                                                                             {
                                                                                 "Nunito", "sans-serif"
                                                                             }
                                                            },
                                                       H3 = new H3
                                                            {
                                                                FontFamily = new[]
                                                                             {
                                                                                 "Nunito", "sans-serif"
                                                                             }
                                                            },
                                                       H4 = new H4
                                                            {
                                                                FontFamily = new[]
                                                                             {
                                                                                 "Nunito", "sans-serif"
                                                                             }
                                                            },
                                                       H5 = new H5
                                                            {
                                                                FontFamily = new[]
                                                                             {
                                                                                 "Nunito", "sans-serif"
                                                                             }
                                                            },
                                                       H6 = new H6
                                                            {
                                                                FontFamily = new[]
                                                                             {
                                                                                 "Nunito", "sans-serif"
                                                                             }
                                                            },
                                                       Subtitle1 = new Subtitle1
                                                                   {
                                                                       FontFamily = new[]
                                                                                    {
                                                                                        "Nunito", "sans-serif"
                                                                                    }
                                                                   },
                                                       Subtitle2 = new Subtitle2
                                                                   {
                                                                       FontFamily = new[]
                                                                                    {
                                                                                        "Fira Code", "Menlo", "Monaco", "Consolas", "Liberation Mono", "Courier New", "monospace"
                                                                                    }
                                                                   },
                                                       Body1 = new Body1
                                                               {
                                                                   FontFamily = new[]
                                                                                {
                                                                                    "Nunito", "sans-serif"
                                                                                }
                                                               },
                                                       Body2 = new Body2
                                                               {
                                                                   FontFamily = new[]
                                                                                {
                                                                                    "Fira Code", "Menlo", "Monaco", "Consolas", "Liberation Mono", "Courier New", "monospace"
                                                                                }
                                                               },
                                                       Button = new Button
                                                                {
                                                                    FontFamily = new[]
                                                                                 {
                                                                                     "Nunito", "sans-serif"
                                                                                 }
                                                                },
                                                       Caption = new Caption
                                                                 {
                                                                     FontFamily = new[]
                                                                                  {
                                                                                      "Nunito", "sans-serif"
                                                                                  }
                                                                 },
                                                       Overline = new Overline
                                                                  {
                                                                      FontFamily = new[]
                                                                                   {
                                                                                       "Nunito", "sans-serif"
                                                                                   }
                                                                  }
                                                   }
                                  };

        [Inject] private HttpClient Client { get; set; }

        [Inject] private IWorldService WorldService { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private ILogger<MainLayout> Logger { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var clientSettings = await Client.GetFromJsonAsync<List<ClientSetting>>("api/v1/Settings/ClientSettings");
            _clientSettings = clientSettings;

            _settings = new SiteSettings()
                        {
                            AboutSettings = clientSettings.Where(x => x.FieldName == "About").ToList(),
                            ContactSettings = new ContactSettings(clientSettings.Where(x => x.FieldName == "Company").ToList()),
                            FAQSettings = clientSettings.Where(x => x.FieldName == "FAQ").ToList()
                        };

            await base.OnInitializedAsync();
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task HandleNewWorldChanged(WorldDTO arg)
        {
            Logger.LogInformation($"New World Changed: {arg.WorldName} {arg.WorldId}");
            var activeWorld = await WorldService.GetActiveWorldAsync(Auth.CurrentUser.UserID);
            if (activeWorld.WorldId == arg.WorldId)
                _newWorld = arg;
            await Task.CompletedTask;
        }

        private Task ToggleDarkModeAsync(bool arg)
        {
            _isDarkMode = arg;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}