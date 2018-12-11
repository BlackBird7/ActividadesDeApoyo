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
    public class FicSrvExportar : IFicSrvExportar
    {
        private readonly FicDBContext FicLoDBContext;
        private readonly HttpClient FicCliente;
        private String URL = "http://localhost:52315/";

        public FicSrvExportar()
        {
            FicLoDBContext = new FicDBContext(DependencyService.Get<IFicConfigSQLite>().FicGetDBPath());
            FicCliente = new HttpClient();
            FicCliente.MaxResponseContentBufferSize = 256000;
        }//CONSTRUCTOR



        public async Task<string> FicPostExportActividades()
        {
            return await FicPostListActividades(new CatActividadesApoyo()
            {
                    cat_Actividades = await (
                    from a in FicLoDBContext.cat_actividades
                    select a).AsNoTracking().ToListAsync()
            });
        }//METODO DE EXPORT INVENTARIOS

        private async Task<string> FicPostListActividades(CatActividadesApoyo item)
        {
            string url = URL + "ExportActividades";
            HttpResponseMessage response = await FicCliente.PostAsync(
                new Uri(string.Format(url, string.Empty)),
                new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            );
            return await response.Content.ReadAsStringAsync();
        }//POST: A INVENTARIOS

    }//CLASS
}
