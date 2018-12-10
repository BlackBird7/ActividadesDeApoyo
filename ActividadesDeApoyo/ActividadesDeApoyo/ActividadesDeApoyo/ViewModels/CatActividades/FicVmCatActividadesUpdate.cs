using System;
using System.Collections.Generic;
using System.Text;
using ActividadesDeApoyo.ViewModels.Base;
using ActividadesDeApoyo.Models.Actividades;
using ActividadesDeApoyo.Interfaces.CatActividades;
using ActividadesDeApoyo.Interfaces.Navigation;
using System.Windows.Input;

namespace ActividadesDeApoyo.ViewModels.CatActividades
{
    public class FicVmCatActividadesUpdate : FicViewModelBase
    {

        private IFicSrvNavigationActividades FicLoSrvNavigation;
        private IFicSrvAppActividades FicLoSrvApp;

        public FicVmCatActividadesUpdate(
            IFicSrvNavigationActividades FicPaSrvNav,
            IFicSrvAppActividades FicPaSrvApp)
        {
            FicLoSrvNavigation = FicPaSrvNav;
            FicLoSrvApp = FicPaSrvApp;
        }

        private cat_actividades _Actividades; 
        public cat_actividades Actividad
        {
            get { return _Actividades; }
            set
            {
                _Actividades = value;
                RaisePropertyChanged();
            }
        }

        #region FicMetUpdateActividad
        private ICommand UpdateActividad;
        public ICommand FicMetUpdateCommand
        {
            get
            {
                return UpdateActividad =
                    UpdateActividad ?? new FicVmDelegateCommand(UpdateActividadExecute);
            }
        }
        private void UpdateActividadExecute()
        {
            Actividad.FechaUltMod = DateTime.Now;
            if (Actividad.Activo.Equals("True"))
            {
                Actividad.Activo = "S";
                Actividad.Borrado = "N";
            }
            else
            {
                Actividad.Activo = "N";
            }
            if (Actividad.Borrado.Equals("True"))
            {
                Actividad.Borrado = "S";
                Actividad.Activo = "N";
            }
            else
            {
                Actividad.Borrado = "N";
            }

            FicLoSrvApp.FicMetUpdateActividades(Actividad);
            FicLoSrvNavigation.FicMetNavigateBack();
        }
        #endregion

       

        public override void OnAppearing(object navigationContext)
        {

            base.OnAppearing(navigationContext);
            Actividad = (navigationContext as cat_actividades);

            if (Actividad.Activo.Equals("True"))
            {
                Actividad.Activo = "S";
                Actividad.Borrado = "N";
            }
            else
            {
                Actividad.Activo = "N";
            }
            if (Actividad.Borrado.Equals("True"))
            {
                Actividad.Borrado = "S";
                Actividad.Activo = "N";
            }
            else
            {
                Actividad.Borrado = "N";
            }
        }

        #region ATRAS / REGRESAR
        private ICommand BackNavigation;
        public ICommand BackNavgCommand
        {
            get { return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute); }
        }
        private void BackNavgExecute()
        {
            FicLoSrvNavigation.FicMetNavigateBack();
        }
        #endregion
    }
}
