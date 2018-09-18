using RealmXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace RealmXF.Views
{
 
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = new HomePageViewModel();
        }
    }
}