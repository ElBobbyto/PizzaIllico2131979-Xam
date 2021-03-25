using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    public class OrderListViewModel : ViewModelBase
    {
        private ObservableCollection<OrderItem> _orders;

        public ObservableCollection<OrderItem> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        public ICommand SelectedCommand { get; }

        public OrderListViewModel()
        {
            SelectedCommand = new Command<OrderItem>(SelectedAction);
        }

        private void SelectedAction(OrderItem obj)
        {
		    
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<OrderItem>> response = await service.ListOrders();

            Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            if (response.IsSuccess)
            {
                Console.WriteLine($"Appel HTTP : {response.Data.Count}");
                Orders = new ObservableCollection<OrderItem>(response.Data);
            }
        }
    }
}