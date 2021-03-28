using System;
using System.Threading.Tasks;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private string _firstname;
        private int _lastname;
        
        public string Fistname
        {
            get { return _firstname; }
            set { SetProperty<string>(ref _firstname, value); }
        }

        private static string clientID = "MOBILE";
        private static string clientSECRET = "UNIV";
        
        public static async void Register(INavigation nav,string email, string firstname, string lastname, string phone, string password)
        {
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<LoginResponse> response; 
            try 
            { 
                response = await service.Register(clientID, clientSECRET, email, firstname, lastname, phone, password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception();
            }
            Console.WriteLine("Appel HTTP : {response.IsSuccess} ");
            if (response.IsSuccess)
            { 
                Console.WriteLine($"Access Token : {response.Data}");
                String token = response.Data.AccessToken;
                String refreshToken = response.Data.RefreshToken;
                String tokenType=response.Data.TokenType;
                int expireIn = response.Data.ExpiresIn;
                nav.PopAsync();
            }
            else
            {
                Console.WriteLine($"Error {response.ErrorCode} : {response.ErrorMessage}");
            } 
        }

        public static async void Connection(string login, string password)
        {
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<LoginResponse> response;
            try
            {
                response = await service.Connect(login, password, clientID, clientSECRET);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine($"Appel HTTP : {response.IsSuccess} ");
            if (response.IsSuccess)
            { 
                Console.WriteLine($"Access Token : {response.Data}");
                Console.WriteLine("Connexion Réussi");
            }
            else
            { 
                Console.WriteLine($"Error {response.ErrorCode} : {response.ErrorMessage}");
            }
        }
        
    }
}