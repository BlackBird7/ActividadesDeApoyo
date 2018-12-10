using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActividadesDeApoyo.Models.Actividades;

namespace ActividadesDeApoyo.Interfaces.CatActividades
{
    public interface IFicSrvAppActividades
    {
        Task<IList<cat_actividades>> FicMetGetListActividades();

        Task<cat_actividades> FicMetGetMaxLoId();
        Task FicMetCreateActividades(cat_actividades actividad);

        Task FicMetUpdateActividades(cat_actividades actividad);

    }
}
