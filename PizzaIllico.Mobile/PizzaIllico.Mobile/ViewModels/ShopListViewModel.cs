using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Pages;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
	public class ShopListViewModel : ViewModelBase
	{
		private ObservableCollection<Shop> _shops;
		public INavigation Navigation { get;}
		public ICommand SelectedCommand { get; }
		public ObservableCollection<Shop> Shops
		{
			get => _shops;
			set => SetProperty(ref _shops, value);
		}
		public ShopListViewModel(INavigation navigation)
		{
			Navigation = navigation;
			SelectedCommand = new Command<long>(SelectedAction);
		}

		private async void SelectedAction(long shopid)
		{
			await Navigation.PushAsync(new PizzaListPage(shopid));
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
				//Shops = new ObservableCollection<ShopItem>(response.Data);
				Location location = await Geolocation.GetLocationAsync();
				List<Shop> bufferList = new List<Shop>();
				foreach (ShopItem shopitem in response.Data)
				{
					bufferList.Add(new Shop(shopitem.Id, shopitem.Name, shopitem.Address,
						Location.CalculateDistance(location, shopitem.Latitude, shopitem.Longitude,
							DistanceUnits.Kilometers)));
				}
				//bufferList = bufferList.OrderBy(shop => shop.Distance).ToList();
				Shops = new ObservableCollection<Shop>(bufferList.OrderBy(shop => shop.Distance).ToList());
			}
		}
	}

	public class Shop
	{
		public long Id { get;}
		public string Name { get;}
		public string Address { get;}
		public double Distance { get;}

		public Shop(long _id, string _name, string _adress, double _distance)
		{
			this.Id = _id;
			this.Name = _name;
			this.Address = _adress;
			this.Distance = Math.Round(_distance, 2);
		} 
	}
	
}