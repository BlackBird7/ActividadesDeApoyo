using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ActividadesDeApoyo.ViewModels.CatActividades;

namespace ActividadesDeApoyo.Views.CatActividades
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViCatActividadesCreate : ContentPage
	{
        private object FicLoParameter;
        public FicViCatActividadesCreate(object FicParameter)
        {
            InitializeComponent();
            FicLoParameter = FicParameter;
            BindingContext = App.FicMetLocator.FicVmCatActividadesCreate;
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmCatActividadesCreate;
            if (viewModel != null)
            {
                viewModel.OnAppearing(FicLoParameter);
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmCatActividadesCreate;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}