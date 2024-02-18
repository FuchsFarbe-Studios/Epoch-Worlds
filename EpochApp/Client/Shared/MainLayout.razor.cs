namespace EpochApp.Client.Shared
{
    public partial class MainLayout
    {
        private bool _drawerOpen;
        private bool _isDarkMode;
        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}