using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActividadesDeApoyo.Interfaces.API
{
    public interface IFicSrvExportar
    {
        Task<string> FicPostExportActividades();
    }
}
