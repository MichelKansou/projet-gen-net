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
        private static UploadPage uploadPage;
        private static UserPage userPage;
        private static AboutPage aboutPage;
        private static SettingsPage settingsPage;

        // Initialize Main app
        public AppWindow()
        {
            InitializeComponent();
            this.user = new User();
            this.appInfo = new DispatchingServiceReference.Application(); 

            // Navigate to the upload page.
            Navigation.Navigation.Frame = new Frame(); //SplitViewFrame;
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;
            this.Loaded += (sender, args) => Navigation.Navigation.Navigate(UploadPage);
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
                switch (menuItem.NavigationDestination)
                {
                    case "uploadPage":
                        Navigation.Navigation.Navigate(UploadPage);
                        break;
                    case "userPage":
                        Navigation.Navigation.Navigate(UserPage);
                        break;
                    case "aboutPage":
                        Navigation.Navigation.Navigate(AboutPage);
                        break;
                    case "settingsPage":
                        Navigation.Navigation.Navigate(SettingsPage);
                        break;
                    default:
                        Navigation.Navigation.Navigate(UploadPage);
                        break;
                }
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



        public UserPage UserPage
        {
            get { return (userPage == null) ? userPage = new UserPage() : userPage; }
            set { userPage = value; }
        }

        public UploadPage UploadPage
        {
            get { return (uploadPage == null) ? uploadPage = new UploadPage() : uploadPage; }
            set { uploadPage = value; }
        }

        public AboutPage AboutPage
        {
            get { return (aboutPage == null) ? aboutPage = new AboutPage() : aboutPage; }
            set { aboutPage = value; }
        }

        public SettingsPage SettingsPage
        {
            get { return (settingsPage == null) ? settingsPage = new SettingsPage() : settingsPage; }
            set { settingsPage = value; }
        }
    }
}
