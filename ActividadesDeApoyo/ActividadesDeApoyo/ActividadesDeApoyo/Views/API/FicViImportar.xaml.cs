using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ActividadesDeApoyo.Views.API;
using ActividadesDeApoyo.ViewModels.API;

namespace ActividadesDeApoyo.Views.API
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViImportar : ContentPage
	{
		public FicViImportar ()
		{
			InitializeComponent ();
            BindingContext = App.FicMetLocator.FicVmImportar;
        }
        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmImportar;
            if (viewModel != null)
            {
                viewModel.OnAppearing();
            }
        }
    }
}