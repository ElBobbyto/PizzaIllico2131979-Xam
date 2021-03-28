using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PizzaIllico.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaListPage : ContentPage
    {
        public PizzaListPage(long idshop)
        {
            InitializeComponent();
            BindingContext = new PizzaListViewModel(idshop);
        }
    }
}
