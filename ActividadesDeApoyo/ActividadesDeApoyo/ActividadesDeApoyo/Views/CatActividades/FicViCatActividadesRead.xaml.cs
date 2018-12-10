using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ActividadesDeApoyo.ViewModels.CatActividades;
using ActividadesDeApoyo.Services.CatActividades;

namespace ActividadesDeApoyo.Views.CatActividades
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViCatActividadesRead : ContentPage
	{
        private object FicLoParameter { get; set; }
        FicSrvAppActividades Service { get; set; }

        public FicViCatActividadesRead(object FicPa)
        {
            InitializeComponent();

            Service = new FicSrvAppActividades();
            FicLoParameter = FicPa;
            BindingContext = App.FicMetLocator.FicVmCatActividadesRead;
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmCatActividadesRead;
            if (viewModel != null)
            {
                viewModel.OnAppearing(FicLoParameter);
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmCatActividadesRead;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}