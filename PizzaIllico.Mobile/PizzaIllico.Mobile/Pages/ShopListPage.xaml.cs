using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopListPage
    {
        public ShopListPage()
        {
            InitializeComponent();
            BindingContext = new ShopListViewModel(Navigation);
        }
    }
}