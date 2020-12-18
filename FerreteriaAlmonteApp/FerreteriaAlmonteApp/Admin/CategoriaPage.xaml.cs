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
    public partial class CategoriaPage : ContentPage
    {
        public CategoriaPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var categoria = await App.MobileService.GetTable<Categoria>().ToListAsync();
            categoryListView.ItemsSource = categoria;


        }

        private void categoryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = categoryListView.SelectedItem as Categoria;

            if (selectedPost != null)
            {
                Navigation.PushAsync(new CategoryDetailPage(selectedPost));
            }
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCategoriaPage());
        }
    }
}