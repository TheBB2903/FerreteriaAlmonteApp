using FerreteriaAlmonteApp.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FerreteriaAlmonteApp
{
    public partial class App : Application
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://ferreteriaalmonteapp.azurewebsites.net");
        public static Usuarios usuario = new Usuarios();
        public static Dispositivo dispositivo = new Dispositivo();
        public static LoginHistory login = new LoginHistory();
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
