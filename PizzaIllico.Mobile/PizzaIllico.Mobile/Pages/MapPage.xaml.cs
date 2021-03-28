using PizzaIllico.Mobile.Controls;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage
    {
        public MapPage(Position startingposition)
        {
            InitializeComponent();
            BindingContext = new MapViewModel(startingposition,Navigation);
        }
    }
}