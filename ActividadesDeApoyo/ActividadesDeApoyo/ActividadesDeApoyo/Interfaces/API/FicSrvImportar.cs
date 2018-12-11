using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace ActividadesDeApoyo.Interfaces.API
{
    public interface IFicSrvImportar
    {
        Task<string> FicGetImportActividades(Int32 id = 0); //ById

        Task<string> FicGetImportActividades(); //All
    }
}
