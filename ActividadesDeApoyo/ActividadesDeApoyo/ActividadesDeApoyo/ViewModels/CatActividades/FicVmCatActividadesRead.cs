using ActividadesDeApoyo.ViewModels.Base;
using ActividadesDeApoyo.Models.Actividades;
using ActividadesDeApoyo.Interfaces.Navigation;
using ActividadesDeApoyo.Interfaces.CatActividades;
using System.Windows.Input;

namespace ActividadesDeApoyo.ViewModels.CatActividades
{
    public class FicVmCatActividadesRead : FicViewModelBase
    {
        private IFicSrvNavigationActividades FicLoSrvNavigation;
        private IFicSrvAppActividades FicLoSrvApp;

        public FicVmCatActividadesRead(
            IFicSrvNavigationActividades FicPaSrvNav,
            IFicSrvAppActividades FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNav;
            FicLoSrvApp = FicPaSrvApp;
        }

        private cat_actividades _Actividad;
        public cat_actividades Actividad
        {
            get { return _Actividad; }
            set 
            {
                _Actividad = value;
                RaisePropertyChanged();
            }
        }

        private ICommand BackNavigation;
        public ICommand BackNavgCommand
        {
            get { return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute); }
        }
        public void BackNavgExecute()
        {
            FicLoSrvNavigation.FicMetNavigateBack();
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            Actividad = navigationContext as cat_actividades;
        }


    }
}
