using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ActividadesDeApoyo.Views.CatActividades;

namespace ActividadesDeApoyo.Views.Navigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicMasterPageMaster : ContentPage
	{
        public ListView ListView;
        public FicMasterPageMaster ()
		{
			InitializeComponent ();

            BindingContext = new FicMasterPageMasterViewModel();
            ListView = MenuItemsListView;
        }
        class FicMasterPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FicMasterPageMenuItem> MenuItems { get; set; }

            public FicMasterPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<FicMasterPageMenuItem>(new[]
                {
                    new FicMasterPageMenuItem { Id = 0, Title = "Actividades",FicPageName="FicViCatActividadesList",TargetType=typeof(FicViCatActividadesList) },
                   
                    //new FicMasterPageMenuItem { Id = 2, Title = "Page 3" },
                    //new FicMasterPageMenuItem { Id = 3, Title = "Page 4" },
                    //new FicMasterPageMenuItem { Id = 4, Title = "Page 5" }, 
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}