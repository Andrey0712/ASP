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
    public partial class MainWindow : Window//https://coderoad.ru/41441089/%D0%98%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-JsonConvert-DeserializeObject-%D0%B4%D0%BB%D1%8F-%D0%B4%D0%B5%D1%81%D0%B5%D1%80%D0%B8%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D0%B8-Json
    {
        String URI = "http://localhost:64098/api/Girls/search";
        
        public MainWindow()
        {
             InitializeComponent();
           //AddGirlDataGrid();//подгружаем при инициализации

        }

        private  void Window_Loaded(object sender, RoutedEventArgs e)//асинхронный запуск
        {
            
            
            AddGirlDataGridAsync();

        }
       
        public void AddGirlDataGrid()
        {
            WebClient webClient = new WebClient();
            string reply = webClient.DownloadString(URI);


            List<GirlVM> add_girls = JsonConvert.DeserializeObject<List<GirlVM>>(reply);
            dgSimple.ItemsSource = new ObservableCollection<GirlVM>(add_girls);

        }
        public Task AddGirlDataGridAsync()
        {
            return Task.Run(() => AddGirlDataGrid());
        }
    }
}
