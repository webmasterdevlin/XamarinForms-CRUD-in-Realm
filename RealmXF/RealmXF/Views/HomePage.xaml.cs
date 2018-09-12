using RealmXF.ViewModels;
using Xamarin.Forms;

namespace RealmXF.Views
{
 
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            
            BindingContext = new HomePageViewModel();
        }
    }
}