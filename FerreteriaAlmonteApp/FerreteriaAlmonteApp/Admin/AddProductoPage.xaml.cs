using FerreteriaAlmonteApp.Model;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FerreteriaAlmonteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductoPage : ContentPage
    {
        private String img = "";
        public AddProductoPage()
        {
            InitializeComponent();

        }

        private async void addProductButton_Clicked(object sender, EventArgs e)
        {
            bool isDescripcionEmpty = string.IsNullOrEmpty(descripcionEntry.Text);
            bool isExcistenciaEmpty = string.IsNullOrEmpty(excistenciaEntry.Text);
            bool isPrecioEmpty = string.IsNullOrEmpty(precioEntry.Text);
            
            if(isDescripcionEmpty || isExcistenciaEmpty || isPrecioEmpty || img == "")
            {
                await DisplayAlert("Required fields", "There should be no empty fields", "OK");
            }
            else
            {
                Producto producto = new Producto()
                {
                    Descripcion = descripcionEntry.Text,
                    Existencia = float.Parse(excistenciaEntry.Text),
                    Precio = float.Parse(precioEntry.Text),
                    Id_Categoria = "bef966ea-779f-4571-ad14-d8366b4254df",
                    Image = img
                };

                await App.MobileService.GetTable<Producto>().InsertAsync(producto);
                await DisplayAlert("Producto", "Producto registrado con exito", "OK");
                descripcionEntry.Text = "";
                excistenciaEntry.Text = "";
                precioEntry.Text = "";

            }

        }

        private async void imgButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("ERROR", "This is not supported on your device", "Ok");
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await DisplayAlert("ERROR", "There was an error when trying to get your image", "Ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

            UploadImage(selectedImageFile.GetStream());

        }

        private async void UploadImage(Stream stream)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=imagestorageferreteria;AccountKey=KgdH+i5oyhUdpgjRKZlqDkP5ahMWQckfzyt+b5H500/KDRJw3/88Z+9e+9qmitl2Razy6s6BAUpAbXXNmHXeFQ==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("imagecontainer");
            await container.CreateIfNotExistsAsync();

            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.jpg");
            await blockBlob.UploadFromStreamAsync(stream);

            string url = blockBlob.Uri.OriginalString;
            img = url;
        }
    }
}