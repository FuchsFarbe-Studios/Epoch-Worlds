namespace EpochApp.Client.Shared
{
    public partial class MainLayout
    {
        bool _drawerOpen = true;
        private bool _isDarkMode;
        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}