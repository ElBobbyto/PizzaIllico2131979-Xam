using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
        }
        async void clickMdp(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new UserPage());
        }
        async void clickModif(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new UserPage());
        }
    }
}