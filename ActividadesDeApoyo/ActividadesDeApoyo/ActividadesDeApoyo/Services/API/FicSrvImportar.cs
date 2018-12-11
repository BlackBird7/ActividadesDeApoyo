using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

using ActividadesDeApoyo.Models.Actividades;
using ActividadesDeApoyo.Data;
using ActividadesDeApoyo.Interfaces.API;
using ActividadesDeApoyo.Interfaces.SQLite;

namespace ActividadesDeApoyo.Services.API
{
    class FicSrvImportar : IFicSrvImportar
    {
        private readonly FicDBContext FicLoDBContext;
        private readonly HttpClient FicCliente;
        private String URL = "http://localhost:52315/";

        public FicSrvImportar()
        {
            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDBPath());
            FicCliente = new HttpClient();
            FicCliente.MaxResponseContentBufferSize = 256000;
        }//CONSTRUCTOR

        #region FicGetListActividadesActualiza (Int32 id = 0)
        private async Task<CatActividadesApoyo> FicGetListActividadesActualiza(Int32 id = 0)
        {
            try
            {
                string url = "";
                if (id != 0) url = URL + "GetByIdActividad?IdActividad=" + id;
                else url = URL + "ImportarActividades";

                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var response = await FicCliente.GetAsync(new Uri(url));
                return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<CatActividadesApoyo>(await response.Content.ReadAsStringAsync()) : null;
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
                return null;
            }
        }
        #endregion


        #region FicGetListCatalogosActualiza()
        private async Task<CatActividadesApoyo> FicGetListCatalogosActualiza()
        {
            string url = URL + "ImportarActividades";

            try
            {
                var response = await FicCliente.GetAsync(url);
                return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<CatActividadesApoyo>(await response.Content.ReadAsStringAsync()) : null;
            }
            catch (Exception e)
            {
                await new Page().DisplayAlert("ALERTA", e.Message.ToString(), "OK");
                return null;
            }
        }
        #endregion


        #region FicExist_Actividades
        /*----------------------------checa si existe el Nivel de grado de competencias---------------------------------*/
        private async Task<cat_actividades> FicExist_Actividades(Int32 id)
        {
            return await (
                from n in FicLoDBContext.cat_actividades
                where n.IdActividad == id
                select n).AsNoTracking().SingleOrDefaultAsync();
        }
        /*-------------------------------------------------------------------*/

        #endregion


        #region  FicGetImportActividades(Int32 id = 0)
        public async Task<string> FicGetImportActividades(Int32 id = 0)
        {
            string FicMensaje = "";
            try
            {
                FicMensaje = "IMPORTACION: \n";
                var FicGetReultREST = new CatActividadesApoyo();
                if (id != 0) FicGetReultREST = await FicGetListActividadesActualiza(id);
                else FicGetReultREST = await FicGetListCatalogosActualiza();

                if (FicGetReultREST != null && FicGetReultREST.cat_Actividades != null)
                {
                    FicMensaje += "Se estan importando los datos de la tabla: eva_cat_niveles_grados_competencias \n";
                    foreach (cat_actividades inv in FicGetReultREST.cat_Actividades)
                    {
                        var respuesta = await FicExist_Actividades(inv.IdActividad);
                        if (respuesta != null)
                        {
                            try
                            {
                                respuesta.IdActividad = inv.IdActividad;
                                respuesta.DesActividad = inv.DesActividad;
                                respuesta.UsuarioReg = inv.UsuarioReg;
                                respuesta.FechaReg = inv.FechaReg;
                                respuesta.UsuarioMod = inv.UsuarioMod;
                                respuesta.FechaUltMod = inv.FechaUltMod;
                                respuesta.Activo = inv.Activo;
                                respuesta.Borrado = inv.Borrado;

                                FicLoDBContext.Update(respuesta); //actualiza

                                FicMensaje += await FicLoDBContext.SaveChangesAsync() > 0 ? "-Actualizando \n-> IdEdificio: " + inv.IdActividad + " \n" : " No es necesario actualizar  IdEdificio: " + inv.IdActividad + " \n";
                                FicLoDBContext.Entry<cat_actividades>(respuesta).State = EntityState.Detached; //desadjunta el id
                            }
                            catch (Exception e)
                            {
                                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                            }
                        }
                        else
                        {
                            try
                            {
                                FicLoDBContext.Add(inv);
                                FicMensaje += await FicLoDBContext.SaveChangesAsync() > 0 ? "Se va a importar:  IdEdificio: " + inv.IdActividad + " \n" : "Ha ocurrido un error al importar IdEdificio: " + inv.IdActividad + " \n";
                            }
                            catch (Exception e)
                            {
                                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                            }
                        }
                    }
                }
                else FicMensaje += "-> No hay datos para importar... \n";
            }
            catch (Exception e)
            {
                FicMensaje += "ALERTA: " + e.Message.ToString() + "\n";
            }
            return FicMensaje;
        }//FicGetImportInventarios()


        #endregion


        #region FicGetImportEdficios()
        public async Task<string> FicGetImportActividades()
        {
            string FicMensaje = "";
            try
            {
                FicMensaje = "IMPORTACION: \n";
                var FicGetReultREST = await FicGetListCatalogosActualiza();

                if (FicGetReultREST != null && FicGetReultREST.cat_Actividades != null)
                {
                    FicMensaje += "IMPORTANDO: cat_edificios \n";
                    foreach (cat_actividades inv in FicGetReultREST.cat_Actividades)
                    {
                        var respuesta = await FicExist_Actividades(inv.IdActividad);
                        if (respuesta != null)
                        {
                            try
                            {
                                respuesta.IdActividad = inv.IdActividad;
                                respuesta.DesActividad = inv.DesActividad;
                                respuesta.UsuarioReg = inv.UsuarioReg;
                                respuesta.FechaReg = inv.FechaReg;
                                respuesta.UsuarioMod = inv.UsuarioMod;
                                respuesta.FechaUltMod = inv.FechaUltMod;
                                respuesta.Activo = inv.Activo;
                                respuesta.Borrado = inv.Borrado;

                                FicLoDBContext.Update(respuesta);

                                FicMensaje += await FicLoDBContext.SaveChangesAsync() > 0 ? "Se va a importar:  IdEdificio: " + inv.IdActividad + " \n" : "Ha ocurrido un error al importar IdEdificio: " + inv.IdActividad + " \n";
                                FicLoDBContext.Entry<cat_actividades>(respuesta).State = EntityState.Detached;
                            }
                            catch (Exception e)
                            {
                                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                            }
                        }
                        else
                        {
                            try
                            {
                                FicLoDBContext.Add(inv);
                                FicMensaje += await FicLoDBContext.SaveChangesAsync() > 0 ? "-INSERT-> IdActividad: " + inv.IdActividad + " \n" : "-ERROR EN INSERTAR-> IdActividad: " + inv.IdActividad + " \n";
                            }
                            catch (Exception e)
                            {
                                FicMensaje += "-ALERTA-> " + e.Message.ToString() + " \n";
                            }
                        }
                    }
                }
                else FicMensaje += "-> No hay datos para importar. \n";
            }
            catch (Exception e)
            {
                FicMensaje += "ALERTA: " + e.Message.ToString() + "\n";
            }
            return FicMensaje;
        }//FicGetImportCatalogos()
        #endregion

    }//CLASS
}
