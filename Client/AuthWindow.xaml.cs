﻿using Client.DispatchingServiceReference;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : MetroWindow
    {
        private Message msg;
        private DispatchingServiceClient proxy;
        private User user;
        private AppWindow appWindow;

        private DispatchingServiceReference.Application application;

        // Initialize Window Application
        public AuthWindow()
        {
            InitializeComponent();
            this.application = InitApplicationInfo();
            this.proxy = new DispatchingServiceClient();
            this.msg = new Message()
            {
                application = new DispatchingServiceReference.Application()
            };
            Console.WriteLine("initialize message");
        }

        // On submit button try to connect user 
        private async void submitButton_Click(object sender, RoutedEventArgs e)
        {
            this.startLoading();
            this.user = new User()
            {
                username = usernameTextBox.Text,
                password = passwordTextBox.Password
            };

            Message request = new Message()
            {
                application = this.application,
                operation = "authentication",
                user = user
            };
            
            Response response = await proxy.DispatcherAsync(request);
            Console.WriteLine("Operation " + request.operation  + " --- " + response.status);

            if (response.status == "SUCCESS")
            {
                stopLoading();
                User connectedUser = response.user;
                Console.WriteLine("User info : ");
                Console.WriteLine("Username : " + connectedUser.username);
                Console.WriteLine("Password : " + connectedUser.password);
                Console.WriteLine("Token : " + connectedUser.token);
                Console.WriteLine("LastConnection : " + connectedUser.lastConnection);
                Console.WriteLine("TokenExpiration : " + connectedUser.tokenExpiration);

                this.appWindow = new AppWindow();
                App.Current.MainWindow = this.appWindow;
                this.appWindow.setUser(connectedUser);
                this.appWindow.setAppInfo(this.application);
                // Close auth window 
                this.Close();
                
                // Open main window
                this.appWindow.Show();
            }
            else
            {
                stopLoading();
                MessageBox.Show("Error : Wrong username or password please try again");
            }
            

        }

        // Initialize Application info
        private DispatchingServiceReference.Application InitApplicationInfo()
        {
            return new DispatchingServiceReference.Application
            {
                Name = "gen-client",
                Version = "1.0",
                Token = "zEAxsZ3iNwCfWWn46c"
            };
        }

        private void usernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        // Hide Authentication grid and show loader grid
        private void startLoading()
        {
            this.AuthForm.Visibility = Visibility.Hidden;
            this.Loader.Visibility = Visibility.Visible;
        }

        // Hide loader grid and show Authentication grid
        private void stopLoading()
        {
            this.AuthForm.Visibility = Visibility.Visible;
            this.Loader.Visibility = Visibility.Hidden;
        }
    }
}
