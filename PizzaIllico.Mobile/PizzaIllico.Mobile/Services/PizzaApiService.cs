using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Services
{
    public interface IPizzaApiService
    {
        Task<Response<List<ShopItem>>> ListShops();
        Task<Response<List<PizzaItem>>> ListPizzas(int shopId);
        Task<Response<List<OrderItem>>> ListOrders();
    }
    
    public class PizzaApiService : IPizzaApiService
    {
        private readonly IApiService _apiService;

        public PizzaApiService()
        {
            _apiService = DependencyService.Get<IApiService>();
        }

        public async Task<Response<List<ShopItem>>> ListShops()
        {
	        return await _apiService.Get<Response<List<ShopItem>>>(Urls.LIST_SHOPS);
        }
        public async Task<Response<List<PizzaItem>>> ListPizzas(int shopId)
        {
            return await _apiService.Get<Response<List<PizzaItem>>>(Urls.LIST_PIZZA);
        }
        
        public async Task<Response<List<OrderItem>>> ListOrders()
        {
            return await _apiService.Get<Response<List<OrderItem>>>(Urls.LIST_ORDERS);
        }
    }
}