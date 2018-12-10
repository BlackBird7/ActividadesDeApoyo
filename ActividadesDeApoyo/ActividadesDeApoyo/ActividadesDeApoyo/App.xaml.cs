using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ActividadesDeApoyo.ViewModels.Base;
using ActividadesDeApoyo.Views.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ActividadesDeApoyo
{
    public partial class App : Application
    {
        private static FicViewModelLocator ficVmLocator;

        public static FicViewModelLocator FicMetLocator
        {
            get
            {
                return ficVmLocator = ficVmLocator ?? new FicViewModelLocator();
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new FicMasterPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
