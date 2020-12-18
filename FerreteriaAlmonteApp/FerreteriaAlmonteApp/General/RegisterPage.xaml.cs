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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            bool isConfirmPasswordEmpty = string.IsNullOrEmpty(confirmPasswordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty || isConfirmPasswordEmpty)
            {
                await DisplayAlert("Campos Obligatorios", "Debe rellenar los campos", "OK");
            }
            else if (isPasswordEmpty == isConfirmPasswordEmpty)
            {
                Usuarios user = new Usuarios()
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text,
                    Status = true
                };

                await App.MobileService.GetTable<Usuarios>().InsertAsync(user);


                await DisplayAlert("Registrado", "Login from your account", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Password", "passwords must match", "OK");
            }
        }
    }
}