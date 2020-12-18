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
    public partial class UsuarioPage : ContentPage
    {
        public UsuarioPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var usuario = await App.MobileService.GetTable<Usuarios>().ToListAsync();
            UserListView.ItemsSource = usuario;


        }

        private void UserListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedUsuario = UserListView.SelectedItem as Usuarios;

            if (selectedUsuario != null)
            {
                Navigation.PushAsync(new UserDetailPage(selectedUsuario));
            }
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddUserPage());
        }
    }
}