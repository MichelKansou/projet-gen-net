using Client.DispatchingServiceReference;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using Client.Views;
using MenuItem = Client.ViewModels.MenuItem;
using System;

namespace Client
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : MetroWindow
    {
        private User user;
        private DispatchingServiceReference.Application appInfo;

        public AppWindow()
        {
            InitializeComponent();
            this.user = new User();
            this.appInfo = new DispatchingServiceReference.Application(); 

            // Navigate to the home page.
            Navigation.Navigation.Frame = new Frame(); //SplitViewFrame;
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;
            this.Loaded += (sender, args) => Navigation.Navigation.Navigate(new UploadPage());
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            HamburgerMenuControl.Content = e.Content;
        }

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            if (menuItem != null && menuItem.IsNavigation)
            {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination);
            }
        }

        public void setUser(User user)
        {
            this.user = user;
        }

        public User getUser()
        {
            return this.user;
        }

        public void setAppInfo(DispatchingServiceReference.Application appInfo)
        {
            this.appInfo = appInfo;
        }

        public DispatchingServiceReference.Application getAppInfo()
        {
            return this.appInfo;
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            Console.WriteLine("current user token : " + this.user.token);
        }
    }
}
