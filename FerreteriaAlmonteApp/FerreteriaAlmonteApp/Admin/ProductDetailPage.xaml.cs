using FerreteriaAlmonteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FerreteriaAlmonteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        Producto selectedProduct;
        public ProductDetailPage(Producto selectedProducto)
        {
            InitializeComponent();

            this.selectedProduct = selectedProducto;
            selectedImage.Source = selectedProduct.Image;
            descripcionEntry.Text = selectedProduct.Descripcion;
            excistenciaEntry.Text = selectedProduct.Existencia.ToString();
            precioEntry.Text = selectedProduct.Precio.ToString();
            categoriaEntry.Text = selectedProduct.Id_Categoria;
        }


        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedProduct.Descripcion = descripcionEntry.Text;
            selectedProduct.Existencia = Convert.ToSingle(excistenciaEntry.Text);
            selectedProduct.Precio = Convert.ToSingle(precioEntry.Text);

            await App.MobileService.GetTable<Producto>().UpdateAsync(selectedProduct);
            await DisplayAlert("Completado", "El producto se actualizo", "Ok");

        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            await App.MobileService.GetTable<Producto>().DeleteAsync(selectedProduct);
            await DisplayAlert("Completado", "El producto se elimino", "Ok");
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddProductoPage());
        }
    }
}