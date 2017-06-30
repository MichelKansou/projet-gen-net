using System;
using MahApps.Metro.IconPacks;
using Client.Views;

namespace Client.ViewModels
{
    internal class ShellViewModel : ViewModelBase
    {
        // Add Item to the Hamburger menu
        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.Upload}, Text = "Upload File", NavigationDestination = "uploadPage" });
            this.Menu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.Search}, Text = "Search Word", NavigationDestination = "wordPage" });
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.File }, Text = "Decoded File", NavigationDestination = "decodedFilePage" });

            this.OptionsMenu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.InfoCircle}, Text = "About", NavigationDestination = "aboutPage"});
        }
    }
}