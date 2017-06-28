using Client.DispatchingServiceReference;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using Client.Views;
using MenuItem = Client.ViewModels.MenuItem;
using System;
using Client.Components;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : MetroWindow
    {
        private User user;
        private DispatchingServiceReference.Application appInfo;

        // Initialize Main app
        public AppWindow()
        {
            InitializeComponent();
            this.user = new User();
            this.appInfo = new DispatchingServiceReference.Application(); 

            // Navigate to the upload page.
            Navigation.Navigation.Frame = new Frame(); //SplitViewFrame;
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;
            this.Loaded += (sender, args) => Navigation.Navigation.Navigate(new UploadPage());
        }

        //
        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            HamburgerMenuControl.Content = e.Content;
        }

        // Change View when an Item is clicked in the HamburgerMenu
        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            if (menuItem != null && menuItem.IsNavigation)
            {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination);
            }
        }

        // set current logged user
        public void setUser(User user)
        {
            this.user = user;
        }

        // get current logged user
        public User getUser()
        {
            return this.user;
        }

        // set AppInfo from middlewares
        public void setAppInfo(DispatchingServiceReference.Application appInfo)
        {
            this.appInfo = appInfo;
        }

        // get App Info from middleware
        public DispatchingServiceReference.Application getAppInfo()
        {
            return this.appInfo;
        }
    }
}
