using System.Collections.Generic;

using ActividadesDeApoyo.Interfaces.SQLite;
using ActividadesDeApoyo.Interfaces.CatActividades;
using ActividadesDeApoyo.Data;
using ActividadesDeApoyo.Models.Actividades;
using ActividadesDeApoyo.Helpers;

using Xamarin.Forms;
using System.Linq;
using Autofac;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ActividadesDeApoyo.Services.CatActividades
{
    public class FicSrvAppActividades : IFicSrvAppActividades
    {
        private static IFicAsyncLock ficMutex = new IFicAsyncLock();
        private FicDBContext FicLoDBContext;

        public FicSrvAppActividades()
        {
            FicLoDBContext = new FicDBContext(
                DependencyService.Get<IFicConfigSQLite>().FicGetDBPath());
        }

        #region SERVICIO: Trae lista de actividades 
        public async Task<IList<cat_actividades>> FicMetGetListActividades()
        {
            var listaActividades = new List<cat_actividades>();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var res = await (from actividades in FicLoDBContext.cat_actividades
                                 select actividades).AsNoTracking().ToListAsync();
                res.ToList().ForEach(actividad => listaActividades.Add(actividad));
            }
            return listaActividades;
        }
        #endregion

        #region SERVICIO: crea nueva actividad
        public async Task<cat_actividades> FicMetGetMaxLoId()
        {
            var actividad = new cat_actividades();
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                var consulta = FicLoDBContext.cat_actividades.Max(l => l.IdActividad);
                actividad.IdActividad = consulta;
            }
            return actividad;
        }

        public async Task FicMetCreateActividades(cat_actividades actividad)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoDBContext.cat_actividades.Add(actividad);
                FicLoDBContext.SaveChanges();
            }
        }
        #endregion

        #region SERVICIO: ACTUALIZA REGISTRO
        public async Task FicMetUpdateActividad(cat_actividades actividad)
        {
            using (await ficMutex.LockAsync().ConfigureAwait(false))
            {
                FicLoDBContext.Entry(actividad).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                FicLoDBContext.SaveChanges();
            }
        }
        #endregion


    }//CLASS
}
