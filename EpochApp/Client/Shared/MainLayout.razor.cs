using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <inheritdoc />
    public partial class MainLayout
    {
        private WorldDTO _activeWorld;

        private bool _drawerOpen;

        private MudTheme _theme = new MudTheme();
        // private bool _isDarkMode;

        [Inject] private HttpClient Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private ILogger<MainLayout> Logger { get; set; }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _theme = new MudTheme
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
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task HandleWorldChanged(WorldDTO arg)
        {
            Logger.LogInformation("World Changed: {WorldID}", arg.WorldID);
            var activeWorld = await Client.GetFromJsonAsync<WorldDTO>($"api/v1/Worlds/ActiveWorld?ownerId={Auth.CurrentUser.UserID}");
            if (activeWorld.WorldID == arg.WorldID)
                _activeWorld = arg;
            await Task.CompletedTask;
        }
    }
}