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
    /// Interaction logic for WordPage.xaml
    /// </summary>
    public partial class WordPage : Page
    {
        static HttpClient client = new HttpClient();

        public WordPage()
        {
            InitializeComponent();
        }

        static async Task RunAsync()
        {
            // New code:
            client.BaseAddress = new Uri("http://172.20.10.14:8080/server/remote/services/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        static async Task<Words> GetWordAsync(string path)
        {
            Words words = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                words = await response.Content.ReadAsAsync<Words>();
            }
            return words;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            WordData.Items.Clear();;
            Words items = new Words();
           RunAsync().Wait();
            try
            {
                items = await GetWordAsync("words?contains=" + SearchTextBox.Text);
                WordData.ItemsSource = items.Word.ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine("error : " + ex.Message);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    public class Words
    {
        public List<Word> Word { get; set; }
    }

    public class Word
    {
        public string Id { get; set; }
        public string Label { get; set; }
    }
}
