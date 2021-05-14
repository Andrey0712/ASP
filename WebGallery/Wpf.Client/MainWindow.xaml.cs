using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
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
using WebGallery.Models;

namespace Wpf.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//https://coderoad.ru/41441089/%D0%98%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-JsonConvert-DeserializeObject-%D0%B4%D0%BB%D1%8F-%D0%B4%D0%B5%D1%81%D0%B5%D1%80%D0%B8%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D0%B8-Json
    {
        //String URI = "http://localhost:64098/api/Girls/search";
        
        public MainWindow()
        {
            InitializeComponent();

            
            //AddGirlDataGrid();//подгружаем при инициализации
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadDataCompleted += AsyncDownloadDataCompleted;
                Uri uri = new Uri("http://localhost:5000/api/Cars/search");
                webClient.DownloadDataAsync(uri);
            }

           


           
        }

        private  void Window_Loaded(object sender, RoutedEventArgs e)
        {

            

        }
       
        
        public void AsyncDownloadDataCompleted(Object sender, DownloadDataCompletedEventArgs e)
        {
            string result = Encoding.Default.GetString(e.Result);
            var cars = JsonConvert.DeserializeObject<List<CarVM>>(result);
            dgSimple.ItemsSource = cars;
        }
        public void AddCar()
        {
            // отправляем запрос по вебу
            WebRequest request = WebRequest.Create("http://localhost:5000/api/cars/add");
            // метод
            request.Method = "POST";

            // формируется запрос и отправляються в масив с кодировкой
            string postData = JsonConvert.SerializeObject(new
            {
                Mark = "Mazda",
                Model = "3",
                Year = 2015,
                Fuel = "Бензин",
                Сapacity = 2.2F,
                Image = "мазда.jpg"
            });
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // тип даных
            request.ContentType = "application/json";
            // длина масива
            request.ContentLength = byteArray.Length;

            // создается поток
            Stream dataStream = request.GetRequestStream();
            // закидывается туда масив
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            try
            {
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Console.WriteLine(responseFromServer);
                }

                // Close the response.
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => AddCar());
            MessageBox.Show("+1 Car");
        }

        private void btnEdt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDell_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
