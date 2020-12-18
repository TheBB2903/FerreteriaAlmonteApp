using FerreteriaAlmonteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FerreteriaAlmonteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopPage : ContentPage
    {
        private Timer timer;
        public ShopPage()
        {
            InitializeComponent();
            this.BindingContext = this;        }

        public List<Productos> CollectionList { get => GetCollectionsAsync(); }

        private List<Productos> GetCollectionsAsync()
        {
            var collectionList = new List<Productos>();
            collectionList.Add(new Productos
            {
                Image = "Banner_nd.jpg",
                Descripcion ="Papel Navidad",
                Precio = 499
            });
            collectionList.Add(new Productos
            {
                Image = "facebook.png",
                Descripcion = "Facebook Logo",
                Precio = 600
            });
            return collectionList;
        }
        public List<Productos> TrendsList { get => GetTrendsAsync(); }

        private List<Productos> GetTrendsAsync()
        {
            var trendsList = new List<Productos>();
            trendsList.Add(new Productos
            {
                Image = "Banner_nd.jpg",
                Descripcion = "Papel Navidad",
                Precio = 499
            });
            trendsList.Add(new Productos
            {
                Image = "facebook.png",
                Descripcion = "Facebook Logo",
                Precio = 600
            });
            return trendsList;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var banner = await App.MobileService.GetTable<Banner>().ToListAsync();
            cvBanners.ItemsSource = banner;

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

    }
    public class Productos
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public float Existencia { get; set; }
        public float Precio { get; set; }
        public string Id_Categoria { get; set; }
        public string Image { get; set; }
    }
}