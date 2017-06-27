using Client.Components;
using Client.DispatchingServiceReference;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
using static MahApps.Metro.SimpleChildWindow.ChildWindowManager;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for UploadPage.xaml
    /// </summary>
    public partial class UploadPage : Page
    {
        private ObservableCollection<string> files = new ObservableCollection<string>();


        private DispatchingServiceClient proxy;
        private AppWindow appWindow;

        public UploadPage()
        {
            InitializeComponent();
            this.DataContext = this;
            this.appWindow = (AppWindow)System.Windows.Application.Current.MainWindow;
            Console.WriteLine("current user token from Upload  : " + this.appWindow.getUser().token);
            this.proxy = new DispatchingServiceClient();
        }

        public ObservableCollection<string> Files
        {
            get
            {
                return this.files;
            }
        }

        // Detect file drop over listbox
        private void DropBox_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                this.files.Clear();

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string filePath in files)
                {
                    this.files.Add(filePath);
                }

                UploadFilesAsync(files);

            }

            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private async void UploadFilesAsync(string[] files)
        {
            foreach (var file in files)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox();
                string fileName = System.IO.Path.GetFileName(file);
                string fullPath = System.IO.Path.GetFullPath(file);
                string content = File.ReadAllText(fullPath);
                DispatchingServiceReference.DecodeFileIn decodeFileIn = new DispatchingServiceReference.DecodeFileIn
                {
                    FileName = fileName,
                    Content = content,
                    Md5 = ""
                };

                Message request = new Message()
                {
                    application = appWindow.getAppInfo(),
                    operation = "decode",
                    decodeFileIn = decodeFileIn,
                    userToken = appWindow.getUser().token
                };
                await this.proxy.DispatcherAsync(request).ContinueWith(t =>
                {
                    Response resp = t.Result;
                    customMessageBox.setEmailBody("Secret : " + resp.decodeFileout.Secret.ToString());
                    customMessageBox.setExportText("Secret : " + resp.decodeFileout.Secret.ToString());
                    customMessageBox.setPdfContent("<p>" + "Secret : " + resp.decodeFileout.Secret.ToString() + "</p>");
                });
                customMessageBox.ShowDialog();
            }
        }

        // Detect file over the listbox
        private void DropBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as ListBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        // Detect leave of file over the listbox
        private void DropBox_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

    }
}
