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
    public partial class CategoryDetailPage : ContentPage
    {
        public Categoria selectedCategory;
        public CategoryDetailPage(Categoria selectedCategoria)
        {
            InitializeComponent();

            this.selectedCategory = selectedCategoria;
            descripcionEntry.Text = selectedCategory.Descripcion;

        }

        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedCategory.Descripcion = descripcionEntry.Text;

            await App.MobileService.GetTable<Categoria>().UpdateAsync(selectedCategory);
            await DisplayAlert("Completado", "El producto se actualizo", "Ok");
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            selectedCategory.Status = false;
            await App.MobileService.GetTable<Categoria>().UpdateAsync(selectedCategory);
            await DisplayAlert("Completado", "El producto se elimino", "Ok");
        }
    }
}