using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for DecodedFilePage.xaml
    /// </summary>
    public partial class DecodedFilePage : Page
    {
        static HttpClient client = new HttpClient();
        private DecodedFiles items;

        public DecodedFilePage()
        {
            InitializeComponent();
            FileListData.Items.Clear();
            RunAsync().Wait();
            loadFiles();
        }

        static async Task RunAsync()
        {
            // New code:
            client.BaseAddress = new Uri("http://172.20.10.14:8080/server/remote/services/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void loadFiles()
        {
            DecodedFiles items = new DecodedFiles();
            try
            {
                items = await GetDecodedFilesAsync("decodedFiles");
                FileListData.ItemsSource = items.DecodedFile.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }


        static async Task<DecodedFiles> GetDecodedFilesAsync(string path)
        {
            DecodedFiles words = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                words = await response.Content.ReadAsAsync<DecodedFiles>();
            }
            return words;
        }
    }

    public class DecodedFiles
    {
        public List<DecodedFile> DecodedFile { get; set; }
    }

    public class DecodedFile
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string DecodeKey { get; set; }
        public string Md5 { get; set; }
        public string FirstWord { get; set; }
        public string Secret { get; set; }

    }
}
