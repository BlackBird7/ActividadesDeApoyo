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
    public class FicVmExportar : INotifyPropertyChanged
    {

        private string _FicTextAreaExport;
        private ICommand _FicMetExport;

        private IFicSrvNavigationActividades IFicSrvNavigation;
        private IFicSrvExportar IFicSrvExportar;

        public FicVmExportar(IFicSrvNavigationActividades IFicSrvNavigationPa, IFicSrvExportar IFicSrvExportarPa)
        {
            IFicSrvNavigation = IFicSrvNavigationPa;
            IFicSrvExportar = IFicSrvExportarPa;
        }//CONSTRUCTOR


        public string FicTextAreaExpInv
        {
            get { return _FicTextAreaExport; }
        }

        public void OnAppearing()
        {

        }//AL INICIAR DE LA VIEW

        public ICommand FicMetExpo
        {
            get
            {
                return _FicMetExport = _FicMetExport ??
                      new FicVmDelegateCommand(FicMetExportActividades);
            }
        }//ESTE METODO SE AGREGA EL COMANDO AL BOTON EN LA VIEW

        private async void FicMetExportActividades()
        {
            try
            {
                _FicTextAreaExport = await IFicSrvExportar.FicPostExportActividades();
                RaisePropertyChanged("FicTextAreaExport");
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
