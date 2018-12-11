using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using ActividadesDeApoyo.Interfaces.Navigation;
using ActividadesDeApoyo.Views.CatActividades;
using ActividadesDeApoyo.ViewModels.CatActividades;

using ActividadesDeApoyo.Views.API;
using ActividadesDeApoyo.ViewModels.API;

namespace ActividadesDeApoyo.Services.Navigation
{
    public class FicSrvNavigationActividades : IFicSrvNavigationActividades
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            // Registrar la relación ViewModel - Vista en el siguiente formato para cada vista

            { typeof(FicVmCatActividadesList), typeof(FicViCatActividadesList)},
            { typeof(FicVmCatActividadesCreate), typeof(FicViCatActividadesCreate)},
            { typeof(FicVmCatActividadesRead), typeof(FicViCatActividadesRead)},
            { typeof(FicVmCatActividadesUpdate), typeof(FicViCatActividadesUpdate)},

            { typeof(FicVmExportar), typeof(FICViExportar)},
            { typeof(FicVmImportar), typeof(FicViImportar)},
        };

        public void FicMetNavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;//MasterDetailPage

            //if (page != null) Application.Current.MainPage.Navigation.PushAsync(page);
            if (page != null)
            {
                var mpd = Application.Current.MainPage as MasterDetailPage;
                mpd.Detail.Navigation.PushAsync(page);
            }
        }//FicMetNavigateTo


        public void FicMetNavigateTo<TDestinationViewModel>(object navigationContext = null, bool show = true)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext, show) as Page;

            //if (page != null) Application.Current.MainPage.Navigation.PushAsync(page);
            if (page != null)
            {
                var mpd = Application.Current.MainPage as MasterDetailPage;
                mpd.Detail.Navigation.PushAsync(page);
            }
        }

        public void FicMetNavigateTo(Type destinationType, object navigationContext = null)
        {
            Type pageType = viewModelRouting[destinationType];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            //if (page != null) Application.Current.MainPage.Navigation.PushAsync(page);
            if (page != null)
            {
                var mpd = Application.Current.MainPage as MasterDetailPage;
                mpd.Detail.Navigation.PushAsync(page);
            }
        }

        public void FicMetNavigateBack()
        {
            //Application.Current.MainPage.Navigation.PopAsync();
            var mpd = Application.Current.MainPage as MasterDetailPage;
            mpd.Detail.Navigation.PopAsync();

        }
    }
}
