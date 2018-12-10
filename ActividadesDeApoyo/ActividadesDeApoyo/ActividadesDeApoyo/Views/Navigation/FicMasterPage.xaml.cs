using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ActividadesDeApoyo.Views.CatActividades;


namespace ActividadesDeApoyo.Views.Navigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicMasterPage : MasterDetailPage
    {
		public FicMasterPage ()
		{
			InitializeComponent ();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as FicMasterPageMenuItem;
                if (item == null)
                    return;

                var FicPagina = item.FicPageName as string;
                switch (FicPagina)
                {

                    case "FicViCatActividadesList":
                        item.TargetType = typeof(FicViCatActividadesList);
                        break;

                    default:
                        break;
                }

                object[] FicObjeto = new object[1];


                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;
                MasterPage.ListView.SelectedItem = null;
            }
            catch (Exception exception)
            {
                new Page().DisplayAlert("ERROR", exception.Message.ToString(), "OK");

            }

        }//AL SELECCIONAR UN ITEM DEL MENU
    }
}