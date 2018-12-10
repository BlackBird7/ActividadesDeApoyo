using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActividadesDeApoyo.Models.Actividades
{
    public class cat_actividades
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 IdActividad { get; set; }
        public String DesActividad { get; set; }
        [StringLength(100)]
        public String Detalle { get; set; }
        [StringLength(3000)]
        public String Activo { get; set; }
        [StringLength(1)]
        public String Borrado { get; set; }
        [StringLength(1)]
        public String UsuarioReg { get; set; }
        [StringLength(20)]
        public String UsuarioMod { get; set; }
        [StringLength(20)]
        public DateTime FechaReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
    }
}
