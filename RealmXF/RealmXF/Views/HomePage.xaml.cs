using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Realms;
using RealmXF.Models;
using RealmXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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