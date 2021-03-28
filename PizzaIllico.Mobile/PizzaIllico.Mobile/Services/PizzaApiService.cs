using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Accounts;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Dtos.Pizzas;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PizzaIllico.Mobile.Services
{
    public interface IPizzaApiService
    {
        Task<Response<List<ShopItem>>> ListShops();
        Task<Response<List<PizzaItem>>> ListPizzas(long shopId);
        Task<Response<List<OrderItem>>> ListOrders();
        Task<Response<LoginResponse>> Enregistrer(string client_id, string client_secret, string email,
            string first_name, string last_name, string phone_number, string password);
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
        public async Task<Response<List<PizzaItem>>> ListPizzas(long shopId)
        {
            return await _apiService.Get<Response<List<PizzaItem>>>(Urls.LIST_PIZZA.Replace("{shopId}",shopId.ToString()));
        }
        
        public async Task<Response<List<OrderItem>>> ListOrders()
        {
            return await _apiService.Get<Response<List<OrderItem>>>(Urls.LIST_ORDERS);
        }
        public async Task<Response<LoginResponse>> Enregistrer(string client_id, string client_secret, string email, string first_name, string last_name, string phone_number, string password)
        {
            CreateUserRequest orderrequest= new CreateUserRequest();
            orderrequest.ClientId = client_id;
            orderrequest.ClientSecret = client_secret;
            orderrequest.Email = email;
            orderrequest.FirstName = first_name;
            orderrequest.LastName = last_name;
            orderrequest.PhoneNumber = phone_number;
            orderrequest.Password = password;
            string content = JsonConvert.SerializeObject(orderrequest);
            
            return await _apiService.Post<Response<LoginResponse>>(Urls.CREATE_USER,content);
        }
    }
}