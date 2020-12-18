using FerreteriaAlmonteApp.Model;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FerreteriaAlmonteApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = true;
            this.LoginButton.Clicked += LoginButton_Clicked;

        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            

            if (isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Required fields", "There should be no empty fields", "OK");
            }
            else if (Regex.IsMatch(emailEntry.Text, emailPattern))
            {
                var usuario =  (await App.MobileService.GetTable<Usuarios>().Where(u => u.Email == emailEntry.Text).ToListAsync()).FirstOrDefault();

                if (usuario != null)
                {
                    App.usuario = usuario;
                    if(usuario.Password == passwordEntry.Text)
                    {
                        
                        if (usuario.Status != false)
                        {
                            loginhistory();
                            if (usuario.TipoUsuario == "Admin")
                            {
                                await Navigation.PushAsync(new HomePage());
                            }
                            else
                            {
                                await Navigation.PushAsync(new HomeUserPage());

                            }
                            
                        }
                        else
                        {
                            await DisplayAlert("Count Disabled", "Contact 'Ferreteria Almonte' for more details", "OK");
                        }
                        
                    }
                    else
                    {
                        await DisplayAlert("Incorrect Data", "Email or Password are incorrect", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "There was an error logging you in", "OK");
                }

                
            }
            else
            {
                emailEntry.TextColor = Color.Red;
                emailEntry.Focus();
            }
        }

        private void facebookButton_Clicked(object sender, EventArgs e)
        {

        }

        private void imagenPassword_Clicked(object sender, EventArgs e)
        {
            if(passwordEntry.IsPassword == true)
            {
                passwordEntry.IsPassword = false;
                imagenPassword.Source = "icons8_invisible_24px.png";
            }
            else
            {
                passwordEntry.IsPassword = true;
                imagenPassword.Source = "icons8_eye_24px.png";
            }
            
        }

        public async void dispositivo()
        {
            Dispositivo dispositivo = new Dispositivo()
            {
                Nombre = DeviceInfo.Name,
                IMEI = CrossDeviceInfo.Current.Id,
                Marca = DeviceInfo.Manufacturer,
                Modelo = DeviceInfo.Model
            };

            await App.MobileService.GetTable<Dispositivo>().InsertAsync(dispositivo);
            App.dispositivo = dispositivo;
        }

        public async void loginhistory()
        {
            
            Location location = await Geolocation.GetLocationAsync();
            if(location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            dispositivo();

            LoginHistory lg = new LoginHistory()
            {
                IdUsuario = App.usuario.Id,
                Longitud = location.Longitude.ToString(),
                Latitud = location.Latitude.ToString(),
                IdDispositivo = App.dispositivo.Id
            };

            await App.MobileService.GetTable<LoginHistory>().InsertAsync(lg);

        }

    }
}
