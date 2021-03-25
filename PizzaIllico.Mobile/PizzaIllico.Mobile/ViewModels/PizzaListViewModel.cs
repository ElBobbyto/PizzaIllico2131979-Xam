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
    public class PizzaListViewModel : ViewModelBase
    {
        private ObservableCollection<PizzaItem> _pizzas;
        private int _currentShop;
        
        public ObservableCollection<PizzaItem> Pizzas
        {
            get => _pizzas;
            set => SetProperty(ref _pizzas, value);
        }
        
        public int CurrentShop
        {
            get => _currentShop;
            set => SetProperty(ref _currentShop, value);
        }
        public ICommand SelectedCommand { get; }

        public PizzaListViewModel()
        {
            SelectedCommand = new Command<PizzaItem>(SelectedAction);
        }

        private void SelectedAction(PizzaItem obj)
        {
		    
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

            Response<List<PizzaItem>> response = await service.ListPizzas(CurrentShop);

            Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            if (response.IsSuccess)
            {
                Console.WriteLine($"Appel HTTP : {response.Data.Count}");
                Pizzas = new ObservableCollection<PizzaItem>(response.Data);
            }
        }
    }
}