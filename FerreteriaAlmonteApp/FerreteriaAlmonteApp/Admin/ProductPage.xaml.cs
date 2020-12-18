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
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var producto = await App.MobileService.GetTable<Producto>().ToListAsync();
            productListView.ItemsSource = producto;


        }

        private void productListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = productListView.SelectedItem as Producto;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new ProductDetailPage(selectedPost));
            }

        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddProductoPage());
        }
    }
}