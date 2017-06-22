using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            startLoading();
            AuthServiceReference.AuthServiceClient auth = new AuthServiceReference.AuthServiceClient();

            if (auth.AuthSerivce(usernameTextBox.Text, passwordTextBox.Text))
            {
                stopLoading();
                MessageBox.Show("You are authenticated");
            } else
            {
                stopLoading();
                MessageBox.Show("Error : Wrong username or password");
            }

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
