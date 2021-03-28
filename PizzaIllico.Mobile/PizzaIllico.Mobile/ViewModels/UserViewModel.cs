using System;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private static string clientID = "MOBILE";
        private static string clientSECRET = "UNIV";
        public static async void Register(string email, string firstname, string lastname, string phone, string password)
        {
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<LoginResponse> response; 
            try 
            { 
                response = await service.Register(clientID, clientSECRET, email, firstname, lastname, phone, password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception();
            }
            Console.WriteLine("Appel HTTP : {response.IsSuccess} ");
            if (response.IsSuccess)
            { 
                Console.WriteLine($"Access Token : {response.Data}");
            }
            else
            { 
                Console.WriteLine($"Error {response.ErrorCode} : {response.ErrorMessage}");
            }
        }
    }
}