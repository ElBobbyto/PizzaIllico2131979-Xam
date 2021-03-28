using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaIllico.Mobile.Controls;
using PizzaIllico.Mobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private StackLayout layoutNotConnected;
        private StackLayout layoutConnected;
        public HomePage()
        {
            BindingContext = new ViewModels.HomeViewModel();
            InitializeComponent();
            layoutNotConnected = this.FindByName("NotConnected") as StackLayout;
            layoutConnected = this.FindByName("Connected") as StackLayout;
            actualiseAffichage();
        }
        async void clickConnexion(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new ConnexionPage());
        }
        async void clickCommande(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new ShopListPage());
        }
        async void clickMap(object sender, EventArgs eventArgs)
        {
            var location = await Geolocation.GetLocationAsync();
            await Navigation.PushAsync(new MapPage(new Position(location.Latitude,location.Longitude)));
        }
        async void clickPanier(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new ShopListPage());
        }
        async void clickAncienneCommande(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new OrderPage());
        }
        async void clickCompte(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new UserPage());
        }
        async void clickEnregistrement(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new InscriptionPage());
        }

        public  void actualiseAffichage()
        {
            this.layoutConnected.IsVisible = User.Instance.is_connected;
            this.layoutNotConnected.IsVisible = !User.Instance.is_connected;
        }
    }
    
}