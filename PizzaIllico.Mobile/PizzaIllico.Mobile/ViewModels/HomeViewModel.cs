using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {

        }
        async void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            Button pressed = sender as Button;
            switch (pressed.Tag)
            {
                case "BtnConnexion":
                    await Navigation.PushAsync(new ConnexionPage());
                    break;
                case "BtnEnregistrement":
                    await Navigation.PushAsync(new UserPage(true));
                    break;
                case "BtnCommande":
                    await Navigation.PushAsync(new ShopListPage());
                    break;
                case "BtnPanier":
                    await Navigation.PushAsync(new OrderPage(true));
                    break;
                case "BtnAnciennesCommandes":
                    await Navigation.PushAsync(new OrderListPage());
                    break;
                case "BtnCompte":
                    await Navigation.PushAsync(new UserPage(false));
                    break;
            }
            
        }
    }
}