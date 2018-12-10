using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ActividadesDeApoyo.Services.CatActividades;
using ActividadesDeApoyo.ViewModels.CatActividades;
using ActividadesDeApoyo.Helpers;

using ActividadesDeApoyo.Data;
using ActividadesDeApoyo.Interfaces.SQLite;
using ActividadesDeApoyo.Models.Actividades;

namespace ActividadesDeApoyo.Views.CatActividades
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCatActividadesList : ContentPage
    {
        private readonly FicDBContext FicLoDBContext;
        private readonly static IFicAsyncLock ficMutex = new IFicAsyncLock();
        private object FicPa { get; set; }
        FicSrvAppActividades Service { get; set; } //Se crea instancia de servidor

        //CONSTRUCTOR
        public FicViCatActividadesList()
        {
            InitializeComponent();

            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDBPath());
            BindingContext = App.FicMetLocator.FicVmCatActividadesList;
            this.FicPa = null;
            Service = new FicSrvAppActividades();
        }

        #region METODOS: OnAppearing & OnDisappearing
        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmCatActividadesList;
            if (viewModel != null) viewModel.OnAppearing(FicPa);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmCatActividadesList;
            if (viewModel != null) viewModel.OnDisappearing();
        }
        #endregion

        #region METODO PARA ELIMINAR ACTIVIDAD
        protected async void FicMetDeleteCommand(object sender, EventArgs e)
        {
            var context = BindingContext as FicVmCatActividadesList;
            if (context.SelectItem_Actividad == null)
            {
                return;
            }
            bool agree = await DisplayAlert("¡Cuidado!", "¿Desea eliminar este elemento?", "Aceptar", "Cancelar");
            if (agree)
            {
                await FicMetDeleteActividad(context.SelectItem_Actividad);
                context.ItemSource_Actividades.Remove(context.SelectItem_Actividad);
            }
            dataGrid.View.Refresh();
        }

        public async Task FicMetDeleteActividad(cat_actividades actividad)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoDBContext.Entry(actividad).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                FicLoDBContext.SaveChanges();
            }
        }
        #endregion
    }
}