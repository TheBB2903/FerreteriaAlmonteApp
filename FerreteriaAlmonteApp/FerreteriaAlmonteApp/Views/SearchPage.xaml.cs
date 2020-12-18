using FerreteriaAlmonteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FerreteriaAlmonteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private async void ProductSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            var Keyboard = ProductSearchBar.Text;
            var producto = await App.MobileService.GetTable<Producto>().Where(a => a.Descripcion.ToLower().Contains(Keyboard.ToLower())).ToListAsync();
            ProductListView.ItemsSource = producto;
        }

        private void ProductListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = ProductListView.SelectedItem as Producto;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new ProductDetailPage(selectedPost));
            }
        }

        private async void ProductSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Keyboard = ProductSearchBar.Text;
            var producto = await App.MobileService.GetTable<Producto>().Where(a => a.Descripcion.ToLower().Contains(Keyboard.ToLower())).ToListAsync();
            ProductListView.ItemsSource = producto;
        }
    }
}