using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
using WebGallery.Models;

namespace Wpf.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String URI = "http://localhost:64098/api/Girls/search";
        
        public MainWindow()
        {
             InitializeComponent();
           // AddGirlDataGrid();//подгружаем при инициализации

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)//асинхронный запуск
        {
            
            await Task.Run(() => AddGirlDataGrid());


        }
        
        public void AddGirlDataGrid()
        {
            WebClient webClient = new WebClient();
            string reply = webClient.DownloadString(URI);


            List<GirlVM> add_girls = JsonConvert.DeserializeObject<List<GirlVM>>(reply);
            dgSimple.ItemsSource = new ObservableCollection<GirlVM>(add_girls);
        }
        //public Task AddGirlDataGridAsync()
        //{
        //    return Task.Run(() => AddGirlDataGrid());
        //}
    }
}
