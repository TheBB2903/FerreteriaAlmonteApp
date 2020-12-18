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
    public partial class AddUserPage : ContentPage
    {
        public AddUserPage()
        {
            InitializeComponent();
        }

        private async void addUsuarioButton_Clicked(object sender, EventArgs e)
        {
            bool isDescripcionEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            bool isStatusEmpty = string.IsNullOrEmpty(statusEntry.Text);

            if (!isDescripcionEmpty || !isPasswordEmpty || !isStatusEmpty)
            {
                Usuarios usuario = new Usuarios()
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text,
                    Status = Convert.ToBoolean(statusEntry.Text)
                };

                await App.MobileService.GetTable<Usuarios>().InsertAsync(usuario);

                await DisplayAlert("Categoria", "Usuario registrado con exito", "OK");

            }
            else
            {
                await DisplayAlert("Campos Obligatorios", "Debe rellenar los campos", "OK");
            }
        }
    }
}