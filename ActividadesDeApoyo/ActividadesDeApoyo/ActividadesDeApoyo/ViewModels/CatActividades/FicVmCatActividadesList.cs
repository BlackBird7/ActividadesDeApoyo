using System;
using System.Collections.Generic;
using System.Text;

using ActividadesDeApoyo.ViewModels.Base;
using ActividadesDeApoyo.Models.Actividades;
using ActividadesDeApoyo.Interfaces.Navigation;
using ActividadesDeApoyo.Interfaces.CatActividades;

using System.Windows.Input; //IComand}
using System.Collections.ObjectModel; //Observable Collection

namespace ActividadesDeApoyo.ViewModels.CatActividades
{
    public class FicVmCatActividadesList : FicViewModelBase
    {
        private IFicSrvNavigationActividades FicLoSrvNav;
        private IFicSrvAppActividades FicLoSrvApp;

        public FicVmCatActividadesList(
            IFicSrvNavigationActividades FicPaSrvNav,
            IFicSrvAppActividades FicPaSrvApp)
        {
            FicLoSrvApp = FicPaSrvApp;
            FicLoSrvNav = FicPaSrvNav;
        } 

        #region SelectItem e ItemSource
        private ObservableCollection<cat_actividades> _ItemSource_Actividades;
        public ObservableCollection<cat_actividades> ItemSource_Actividades
        {
            get { return _ItemSource_Actividades; }
            set
            {
                _ItemSource_Actividades = value;
                RaisePropertyChanged();
            }
        }

        private cat_actividades _SelectItem_Actividad;
        public cat_actividades SelectItem_Actividad
        {
            get { return _SelectItem_Actividad; }
            set
            {
                _SelectItem_Actividad = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region METODO: MUESTRA LISTADO DE Actividades EN GRID
        public async override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            var lista = await FicLoSrvApp.FicMetGetListActividades();
            ItemSource_Actividades = new ObservableCollection<cat_actividades>();

            foreach (var actividad in lista)
            {
                ItemSource_Actividades.Add(actividad);
            }
        }
        #endregion

        #region METODO: ENVIA A VIEW PARA CREAR ACTIVIDAD
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
            FicLoSrvNav.FicMetNavigateTo<FicVmCatActividadesCreate>(null);
        }
        #endregion

        #region METODO: ENVIA A VIEW PARA ACTUALIZAR CON LA ACTIVIDAD SELECCIONADA COMO PARAMETRO
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
            if (SelectItem_Actividad != null)
            {
                FicLoSrvNav.FicMetNavigateTo<FicVmCatActividadesUpdate>(SelectItem_Actividad);
            }
        }
        #endregion

        #region METODO: ENVIA A VIEW PARA VER DETALLE DE COLONIA SELECCIONADA
        private ICommand ReadActividad;
        public ICommand FicMetReadCommand
        {
            get { return ReadActividad = ReadActividad ?? new FicVmDelegateCommand(ReadActividadExecute); }
        }

        private void ReadActividadExecute()
        {
            if (SelectItem_Actividad != null)
                FicLoSrvNav.FicMetNavigateTo<FicVmCatActividadesRead>(SelectItem_Actividad);
        }
        #endregion
    }
}