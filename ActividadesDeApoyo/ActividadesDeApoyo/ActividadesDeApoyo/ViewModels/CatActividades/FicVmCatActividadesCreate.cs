using System;
using System.Collections.Generic;
using System.Text;
using ActividadesDeApoyo.ViewModels.Base;
using ActividadesDeApoyo.Models.Actividades;
using ActividadesDeApoyo.Interfaces.Navigation;
using ActividadesDeApoyo.Interfaces.CatActividades;

using System.Windows.Input;

namespace ActividadesDeApoyo.ViewModels.CatActividades
{
    public class  FicVmCatActividadesCreate : FicViewModelBase
    {
        private IFicSrvNavigationActividades FicLoSrvNavigation;
        private IFicSrvAppActividades FicLoSrvAppActividades;

        public FicVmCatActividadesCreate(
            IFicSrvNavigationActividades FicPaSrvNavigation,
            IFicSrvAppActividades FicPaSrvAppActividades)
        {
            FicLoSrvNavigation = FicPaSrvNavigation;
            FicLoSrvAppActividades = FicPaSrvAppActividades;
        }

        #region ATRAS / REGRESAR
        private ICommand BackNavigation; 
        public ICommand BackNavgCommand
        {
            get
            {
                return BackNavigation = BackNavigation ?? new FicVmDelegateCommand(BackNavgExecute);
            }
        }
        private void BackNavgExecute()
        {
            FicLoSrvNavigation.FicMetNavigateBack();
        }
        #endregion

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

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            _Actividades = new cat_actividades();
            _Actividades.Activo = "False";
            _Actividades.Borrado = "False";
        }

        private ICommand CreateActividad;
        public ICommand FicMetCreateCommand
        {
            get
            {
                return CreateActividad =
                  CreateActividad ?? new FicVmDelegateCommand(CreateActividadExecute);
            }
        }
        private void CreateActividadExecute()
        {
            System.Diagnostics.Debug.Write(Actividad);
            var maxid = FicLoSrvAppActividades.FicMetGetMaxLoId();
            //Validacion Id
            try
            {
                if (maxid.Result == null)
                {
                    Actividad.IdActividad = 1;
                }
                else
                {
                    Actividad.IdActividad = (Int32)(maxid.Result.IdActividad + 1);
                }
            }
            catch (System.AggregateException e)
            {
                Actividad.IdActividad = 1;
            }

            Actividad.FechaReg = DateTime.Now;
            Actividad.UsuarioReg = "Leo";
            //Actividad.FechaUltMod = DateTime.Now;

            if (Actividad.Activo.Equals("True"))
            {
                Actividad.Activo = "S";
            }
            else
            {
                Actividad.Activo = "N";
            }
            if (Actividad.Borrado.Equals("True"))
            {
                Actividad.Borrado = "S";
            }
            else
            {
                Actividad.Borrado = "N";
            }

            //Edificio.UsuarioMod = Edificio.UsuarioReg;
            FicLoSrvAppActividades.FicMetCreateActividades(Actividad);
            FicLoSrvNavigation.FicMetNavigateBack();
        }
    }
}
