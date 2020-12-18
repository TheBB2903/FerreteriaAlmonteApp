using FerreteriaAlmonteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FerreteriaAlmonteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioUsuarioPage : ContentPage
    {
        private Timer timer;
        public InicioUsuarioPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var producto = await App.MobileService.GetTable<Producto>().ToListAsync();
            cvBanners.ItemsSource = producto;
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
            //productListView.ItemsSource = producto;
        }

        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (cvBanners.Position == 2)
                {
                    cvBanners.Position = 0;
                    return;
                }

                cvBanners.Position += 1;
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        //private void productListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var selectedPost = productListView.SelectedItem as Producto;

        //    if (selectedPost != null)
        //    {
        //        Navigation.PushAsync(new ProductDetailPage(selectedPost));
        //    }

        //}

    }

}