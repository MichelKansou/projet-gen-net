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
        private static WordPage wordPage;
        private static DecodedFilePage decodedFilePage;
        private static AboutPage aboutPage;

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
                    case "wordPage":
                        Navigation.Navigation.Navigate(WordPage);
                        break;
                    case "decodedFilePage":
                        Navigation.Navigation.Navigate(DecodedFilePage);
                        break;
                    case "aboutPage":
                        Navigation.Navigation.Navigate(AboutPage);
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



        public WordPage WordPage
        {
            get { return wordPage ?? (wordPage = new WordPage()); }
            set { wordPage = value; }
        }
        public DecodedFilePage DecodedFilePage
        {
            get { return decodedFilePage ?? (decodedFilePage = new DecodedFilePage()); }
            set { decodedFilePage = value; }
        }

        public UploadPage UploadPage
        {
            get { return uploadPage ?? (uploadPage = new UploadPage()); }
            set { uploadPage = value; }
        }

        public AboutPage AboutPage
        {
            get { return aboutPage ?? (aboutPage = new AboutPage()); }
            set { aboutPage = value; }
        }
    }
}
