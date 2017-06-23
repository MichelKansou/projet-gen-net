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
            this.proxy = new DispatchingServiceClient();
            this.msg = new Message();
            initialize_msg();
            Console.WriteLine("initialize message");
            //Console.Write(this.msg.app_token + "\n");
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            startLoading();
            this.msg.username = usernameTextBox.Text;
            this.msg.user_password = passwordTextBox.Text;
            this.msg.op_name = "authentication";
            this.msg.op_statut = true;
            Console.WriteLine("Operation " + this.msg.op_name + " Executed");

            this.msg = this.proxy.Dispatcher(this.msg);

            Console.WriteLine(this.msg.data[0]);
            if ((bool) this.msg.data[0])
            {
                stopLoading();
                MessageBox.Show("You are authenticated");
            } else
            {
                stopLoading();
                MessageBox.Show("Error : Wrong username or password");
            }
            

        }

        private void initialize_msg()
        {
            
            this.msg.app_name = "gen-client";
            this.msg.app_version = "1.0";
            this.msg.app_token = "zEAxsZ3iNwCfWWn46c";
            this.msg.username = null;
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
