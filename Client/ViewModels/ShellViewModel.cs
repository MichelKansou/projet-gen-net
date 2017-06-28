using System;
using MahApps.Metro.IconPacks;

namespace Client.ViewModels
{
    internal class ShellViewModel : ViewModelBase
    {
        // Add Item to the Hamburger menu
        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.Upload}, Text = "Upload File", NavigationDestination = new Uri("Views/UploadPage.xaml", UriKind.RelativeOrAbsolute)});
            this.Menu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.UserOutline}, Text = "User", NavigationDestination = new Uri("Views/UserPage.xaml", UriKind.RelativeOrAbsolute)});

            this.OptionsMenu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.Cogs}, Text = "Settings", NavigationDestination = new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute)});
            this.OptionsMenu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.InfoCircle}, Text = "About", NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute)});
        }
    }
}