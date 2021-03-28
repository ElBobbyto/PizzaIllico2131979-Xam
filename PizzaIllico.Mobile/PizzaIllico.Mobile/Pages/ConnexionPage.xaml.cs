using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaIllico.Mobile.Controls;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Services;
using PizzaIllico.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnexionPage : ContentPage
    {
        public ConnexionPage()
        {
            InitializeComponent();
        }
        private void OnClickedConnectionButton(object sender, EventArgs e)
        {
            Connection(Navigation,login.Text, password.Text);
        }
        public static async void Connection(INavigation nav, string login, string password)
        {
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<LoginResponse> response;
            try
            {
                response = await service.Connect(login, password, User.client_id, User.client_secret);
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
                User.Instance.access_token = response.Data.AccessToken;
                User.Instance.refresh_token = response.Data.RefreshToken;
                User.Instance.token_type=response.Data.TokenType;
                User.Instance.expires_in = response.Data.ExpiresIn;
                User.Instance.is_connected = true;
                await nav.PushAsync(new HomePage());
            }
            else
            { 
                Console.WriteLine($"Error {response.ErrorCode} : {response.ErrorMessage}");
            }
        }
    }
}