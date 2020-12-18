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
    public partial class AddCategoriaPage : ContentPage
    {
        public AddCategoriaPage()
        {
            InitializeComponent();
        }

        private async void categoriaButton_Clicked(object sender, EventArgs e)
        {
            bool isDescripcionEmpty = string.IsNullOrEmpty(descripcionEntry.Text);

            if (!isDescripcionEmpty)
            {
                Categoria categoria = new Categoria()
                {
                    Descripcion = descripcionEntry.Text
                };

                await App.MobileService.GetTable<Categoria>().InsertAsync(categoria);

                await DisplayAlert("Categoria", "Categoria registrada con exito", "OK");

            }
            else
            {
                await DisplayAlert("Campos Obligatorios", "Debe rellenar los campos", "OK");
            }
        }
    }
}