using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

using ActividadesDeApoyo.Interfaces.API;
using ActividadesDeApoyo.Interfaces.Navigation;
using ActividadesDeApoyo.ViewModels.Base;


namespace ActividadesDeApoyo.ViewModels.API
{
    public class FicVmImportar : INotifyPropertyChanged
    {

        private string _FicTextAreaImp, _FicLabelId;
        private ICommand _FicMecImportId, _FicMecImport, _FicMecImportCat;

        private IFicSrvNavigationActividades IFicSrvNavigation;
        private IFicSrvImportar IFicSrvImportar;


        public FicVmImportar(IFicSrvNavigationActividades IFicSrvNavigationPa, IFicSrvImportar IFicSrvImportarPa)
        {
            IFicSrvNavigation = IFicSrvNavigationPa;
            IFicSrvImportar = IFicSrvImportarPa;
        }//CONSTRUCTOR

        public async void OnAppearing() { }

        public string FicTextAreaImpInv
        {
            get { return _FicTextAreaImp; }
        }

        public string FicLabelIdInv
        {
            get { return _FicLabelId; }
            set
            {
                if (value != null) _FicLabelId = value;
            }
        }

        public ICommand FicMecImportId
        {
            get
            {
                return _FicMecImportId = _FicMecImportId ??
                      new FicVmDelegateCommand(FicMecImportActividadId);
            }
        }//ESTE VENTO AGREGA EL COMANDO AL BOTON EN LA VIEW

        private async void FicMecImportActividadId()
        {
            try
            {
                if (_FicLabelId.Length > 0)
                {
                    _FicTextAreaImp = await IFicSrvImportar.FicGetImportActividades(Int16.Parse(_FicLabelId));
                    RaisePropertyChanged("FicTextAreaImpInv");
                    await new Page().DisplayAlert("ALERTA", "Datos Actualizados.", "OK");
                }
                else await new Page().DisplayAlert("ALERTA", "ID NO VALIDO.", "OK");

            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }

        public ICommand FicMecImport
        {
            get
            {
                return _FicMecImport = _FicMecImport ??
                      new FicVmDelegateCommand(FicMecImportActividad);
            }
        }//ESTE VENTO AGREGA EL COMANDO AL BOTON EN LA VIEW
        private async void FicMecImportActividad()
        {
            try
            {
                _FicTextAreaImp = await IFicSrvImportar.FicGetImportActividades();
                RaisePropertyChanged("FicTextAreaImpInv");
                await new Page().DisplayAlert("ALERTA", "Datos Actualizados.", "OK");
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
            }
        }


        #region  INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
