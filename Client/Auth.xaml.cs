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
        private MSG msg;
        private static DispatchingServiceReference.IDispatching proxy;


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
            this.loading = false;
            string uri = "http://localhost:58526/serviceWCF";
            EndpointAddress ep = new EndpointAddress(uri);
            proxy = ChannelFactory<IDispatching>.CreateChannel(new BasicHttpBinding(), ep);
            initialize_msg();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            startLoading();
            this.msg.username = usernameTextBox.Text;
            this.msg.user_password = passwordTextBox.Text;
            this.msg.op_name = "authentication";
            this.msg.op_statut = true;

            Console.Write(this.msg);

            proxy.dispatcher(this.msg);

            Console.Write(this.msg.data);
            Console.Write("Executed");

            /*
            if ((bool) msg.data[0])
            {
                stopLoading();
                MessageBox.Show("You are authenticated");
            } else
            {
                stopLoading();
                MessageBox.Show("Error : Wrong username or password");
            }
            */

        }

        void initialize_msg()
        {
            this.msg.app_name = "gen-client";
            this.msg.app_version = "1.0";
            this.msg.app_token = "zEAxsZ3iNwCfWWn46c";
            this. msg.username = null;
            this.msg.user_password = null;
            this.msg.user_token = null;
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
