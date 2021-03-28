using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaIllico.Mobile.Controls;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Accounts;
using PizzaIllico.Mobile.Dtos.Authentications;
using PizzaIllico.Mobile.Services;
using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InscriptionPage : ContentPage
    {
        public InscriptionPage()
        {
            InitializeComponent();
        }

        private void OnButtonInscriptionClicked(object sender, EventArgs e)
        { 
            Register(Navigation ,email.Text, firstname.Text, lastname.Text, phone.Text, password.Text);
        }
        public static async void Register(INavigation nav,string email, string firstname, string lastname, string phone, string password)
        {
            IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
            Response<LoginResponse> response; 
            try 
            { 
                response = await service.Register(User.client_id,User.client_secret, email, firstname, lastname, phone, password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception();
            }
            
            if (response.IsSuccess)
            { 
                Console.WriteLine($"Access Token : {response.Data}");
                Console.WriteLine($"Access Token : {response.Data.AccessToken}");
                Console.WriteLine($"Access Token : {response.Data.RefreshToken}");
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