using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using WebGallery.Entities;
using Wpf.Client.Models.Car.Validation;

namespace Wpf.Client
{
    /// <summary>
    /// Логика взаимодействия для AddCarWindow.xaml
    /// </summary>
    public partial class AddCarWindow : Window
    {
        public static string New_FileName { get; set; }
        private readonly EFDataContext _context;
        public long _id { get; set; }
        
        public AddCarWindow(long id, EFDataContext context)
        {
            InitializeComponent();
            _id = id;
            _context = context;
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                New_FileName = openFileDialog.FileName;
            }
        }

        private async void btnSaveChangs_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => Edit_Car());
        }
        public void Edit_Car()
        {
            var chang_Car = _context.Cars.FirstOrDefault(x => x.Id == _id);


            if (!string.IsNullOrEmpty(tbMark.Text))
                chang_Car.Mark = tbMark.Text.ToString();
            if (!string.IsNullOrEmpty(tbModel.Text))
                chang_Car.Model = tbModel.Text.ToString();
            if (!string.IsNullOrEmpty(tbFuel.Text))
                chang_Car.Fuel = tbFuel.Text.ToString();
            if (!string.IsNullOrEmpty(tbYear.Text))
                chang_Car.Year = int.Parse(tbYear.Text); 
            if (!string.IsNullOrEmpty(tbСapacity.Text))
                chang_Car.Сapacity = float.Parse(tbСapacity.Text);
            if (!string.IsNullOrEmpty(New_FileName))
            {
                var extension = System.IO.Path.GetExtension(New_FileName);
                var imageName = System.IO.Path.GetRandomFileName() + extension;
                var dir = Directory.GetCurrentDirectory();
                var saveDir = System.IO.Path.Combine(dir, "foto");
                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);

                var fileSave = System.IO.Path.Combine(saveDir, imageName);
                File.Copy(New_FileName, fileSave);
                chang_Car.Image = fileSave;
            }
            // отправляем запрос по вебу
            WebRequest request = WebRequest.Create("http://localhost:5000/api/cars/edit?id=_id");
            // метод
            request.Method = "PUT";

            // формируется запрос и отправляються в масив с кодировкой
           
            string json = JsonConvert.SerializeObject(new
            {
                chang_Car.Id,
                chang_Car.Mark,
                chang_Car.Model,
                chang_Car.Year,
                chang_Car.Сapacity,
                chang_Car.Fuel,
                chang_Car.Image,


            });

            byte[] byteArray = Encoding.UTF8.GetBytes(json);

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
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    MessageBox.Show("Error code: " + httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        var errors = JsonConvert.DeserializeObject<AddCarValidation>(text);
                        MessageBox.Show(text);
                        MessageBox.Show(errors.Errors.Mark[0]);
                        MessageBox.Show(errors.Errors.Model[0]);
                        MessageBox.Show(errors.Errors.Year[0]);
                        MessageBox.Show(errors.Errors.Fuel[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
