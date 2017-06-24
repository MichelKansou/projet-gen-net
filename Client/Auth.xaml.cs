using Client.DispatchingServiceReference;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILoader
    {
        private bool _loading;
        private string _loaderText;
        private Message msg;
        private DispatchingServiceClient proxy;
        private User user;

        private DispatchingServiceReference.Application application;


        public bool loading
        {

            get { return _loading; }
            private set { _loading = value; }
        }

        public string loaderText
        {
            get { return _loaderText; }
            set { _loaderText = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.application = InitApplicationInfo();
            this.loading = false;
            this.proxy = new DispatchingServiceClient();
            this.msg = new Message();
            this.msg.application = new DispatchingServiceReference.Application();
            Console.WriteLine("initialize message");
            //Console.Write(this.msg.app_token + "\n");
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            startLoading();
            this.user = new User()
            {
                username = usernameTextBox.Text,
                password = passwordTextBox.Text
            };

            Message request = new Message()
            {
                application = this.application,
                operation = "authentication",
                user = user
            };
            
            Response response = this.proxy.Dispatcher(request);
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
                MessageBox.Show("You are authenticated");
            }
            else
            {

                stopLoading();
                MessageBox.Show("Error : Wrong username or password");
            }
            

        }

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

        private void passwordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void onLoad()
        {
            Task.Factory.StartNew(() =>
            {
                startLoading();
                updateText("Loading");
                Thread.Sleep(1000);
                stopLoading();
            });
        }

        public void startLoading()
        {
            this.loading = true;
        }

        public void updateText(string text)
        {
            this.loaderText = text;
        }

        public void stopLoading()
        {
            this.loading = false;
        }
    }
}
