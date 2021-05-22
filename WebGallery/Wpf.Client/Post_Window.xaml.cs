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
using Ubiety.Dns.Core;
using WebGallery.Entities;
using WebGallery.Entities.Data;
using Wpf.Client.Models.Car.Validation;
using Path = System.IO.Path;

namespace Wpf.Client
{
    /// <summary>
    /// Логика взаимодействия для Post_Window.xaml
    /// </summary>
    public partial class Post_Window : Window
    {
        private string file_selected = string.Empty;
        public string file_name { get; set; }
        public static string New_FileName { get; set; }
        private readonly EFDataContext _context;
        

        public Post_Window( EFDataContext context)
        {
            InitializeComponent();
           
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

        //Кнопка зберігання
        private void btnSaveChangs_Click(object sender, RoutedEventArgs e)
        {
            _ = PostRequest();
            
        }

        public async Task<bool> PostRequest()
        {
            if (!string.IsNullOrEmpty(file_selected))
            {

                var extension = Path.GetExtension(New_FileName);
                var imageName = Path.GetRandomFileName() + extension;
                var dir = Directory.GetCurrentDirectory();
                var saveDir = Path.Combine(dir, "foto");
                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);
                var fileSave = Path.Combine(saveDir, imageName);
                File.Copy(New_FileName, fileSave);
                file_name = fileSave;
            }

            WebRequest request = WebRequest.Create("http://localhost:5000/api/Cars/add");
            {
                request.Method = "POST";
                request.ContentType = "application/json";
            };
            string json = JsonConvert.SerializeObject(new
            {
                Mark = tbMark.Text.ToString(),
                Model = tbModel.Text.ToString(),
                Year = int.Parse(tbYear.Text),
                Fuel = tbFuel.Text.ToString(),
                Capacity = float.Parse(tbСapacity.Text),
                Image = file_name
            });
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                await request.GetResponseAsync();
                return true;
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
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
    }
}
