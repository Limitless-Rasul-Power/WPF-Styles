using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient http = new HttpClient();
        public dynamic Data { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TxtBxMovieName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();

                    response = http.GetAsync($@"http://www.omdbapi.com/?apikey=e0c9d947&s={TxtBxMovieName.Text}&plot=full").Result;
                    var str = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject(str);

                    response = http.GetAsync($@"http://www.omdbapi.com/?apikey=e0c9d947&s={Data.Search[0].imdbID}&plot=full").Result;

                    str = response.Content.ReadAsStringAsync().Result;

                    //exception
                    Data = JsonConvert.DeserializeObject(str);

                    ImgMovie.Source = Data.Search[0].Poster;
                    TxtBlckTitle.Text = Data.Search[0].Title;

                    TxtBlckGenre.Text = Data.Search[0].Genre;
                    TxtBlckGenre.Text = Data.Search[0].Plot;
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid movie Name");
                }
            }
           
        }
    }
}
