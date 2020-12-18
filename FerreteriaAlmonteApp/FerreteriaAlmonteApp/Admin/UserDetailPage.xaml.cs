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
    public partial class UserDetailPage : ContentPage
    {
        Usuarios selectedUsuario;
        public UserDetailPage(Usuarios selectedUsuario)
        {
            InitializeComponent();

            this.selectedUsuario = selectedUsuario;
            emailEntry.Text = selectedUsuario.Email;
            statusEntry.Text = selectedUsuario.Status.ToString();

        }

        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedUsuario.Email = emailEntry.Text;
            selectedUsuario.Status = Convert.ToBoolean(statusEntry.Text);

            await App.MobileService.GetTable<Usuarios>().UpdateAsync(selectedUsuario);
            await DisplayAlert("Completado", "El producto se actualizo", "Ok");
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            await App.MobileService.GetTable<Usuarios>().DeleteAsync(selectedUsuario);
            await DisplayAlert("Completado", "El producto se elimino", "Ok");
        }
    }
}