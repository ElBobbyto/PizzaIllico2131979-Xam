using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Pages;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private ObservableCollection<ShopItem> _shops;
        public INavigation Navigation { get;}
        public ICommand SelectedCommand { get; }
        public ObservableCollection<ShopItem> Shops
        {
            get => _shops;
            set => SetProperty(ref _shops, value);
        }
        public MapViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SelectedCommand = new Command<ShopItem>(SelectedAction);
        }

        private async void SelectedAction(ShopItem obj)
        {
            await Navigation.PushAsync(new PizzaListPage(obj.Id));
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
            }
        }
    }
}