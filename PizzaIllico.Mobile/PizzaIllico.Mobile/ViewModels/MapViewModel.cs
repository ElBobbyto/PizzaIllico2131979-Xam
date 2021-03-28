using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Controls;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Pages;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PizzaIllico.Mobile.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private ObservableCollection<ShopItem> _shops;
        public INavigation Navigation { get;}
        public ObservableCollection<ShopItem> Shops
        {
            get => _shops;
            set => SetProperty(ref _shops, value);
        }

        private Map _map;

        public Map Map
        {
            get => _map;
            set => SetProperty(ref _map, value); 
        }
        public MapViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Map = new Map {MapType = 0, IsShowingUser = true};
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<ShopItem>> response = await service.ListShops();

            Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            if (response.IsSuccess)
            {
                Console.WriteLine($"Appel HTTP : {response.Data.Count}");
                Shops = new ObservableCollection<ShopItem>(response.Data);
                updateMap();
            }
        }

        private void updateMap()
        {
            Map.Pins.Clear();
            foreach (ShopItem shop in Shops)
            {
                Map.Pins.Add(new BindablePin
                {
                    Label = shop.Name,
                    Address = shop.Address,
                    Position = new Position(shop.Latitude,shop.Longitude),
                    Type = PinType.Place,
                    Command = new Command(
                    async () =>
                    {
                        await Navigation.PushAsync(new PizzaListPage(shop.Id));
                    })
                });
            }
        }
    }
}