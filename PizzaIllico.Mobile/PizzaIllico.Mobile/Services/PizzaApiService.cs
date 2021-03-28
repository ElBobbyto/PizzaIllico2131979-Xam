using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Accounts;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Dtos.Authentications.Credentials;
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
        Task<Response<LoginResponse>> Register(string client_id, string client_secret, string email,
            string first_name, string last_name, string phone_number, string password);

        Task<Response<LoginResponse>> Connect(string login, string password, string client_id, string client_secret);
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
        public async Task<Response<LoginResponse>> Register(string client_id, string client_secret, string email, string first_name, string last_name, string phone_number, string password)       
        {
            CreateUserRequest user= new CreateUserRequest
            {
                ClientId = client_id,
                ClientSecret = client_secret,
                Email = email,
                FirstName = first_name,
                LastName=last_name,
                PhoneNumber = phone_number,
                Password = password
            };
            string content = JsonConvert.SerializeObject(user);
            return await _apiService.Post<Response<LoginResponse>>(Urls.CREATE_USER,content);
        }
        public async Task<Response<LoginResponse>> Connect(string login, string password, string clientid, string clientsecret)
        {
            LoginWithCredentialsRequest log = new LoginWithCredentialsRequest
            {
                ClientId = clientid,
                ClientSecret = clientsecret,
                Login = login,
                Password = password
            };
            string content = JsonConvert.SerializeObject(log);
            return await _apiService.Post<Response<LoginResponse>>(Urls.LOGIN_WITH_CREDENTIALS,content);
        }

        public async Task<Response<LoginResponse>> Refresh(string refreshToken, string clientId, string clientSecret)
        {
            RefreshRequest refreshRequest = new RefreshRequest
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                RefreshToken = refreshToken
            };
            string content = JsonConvert.SerializeObject(refreshRequest);
            return await _apiService.Post<Response<LoginResponse>>(Urls.REFRESH_TOKEN,content);
        }
    }
}