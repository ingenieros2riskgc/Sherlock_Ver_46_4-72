using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class PaActividadResponsable
    {
        public int IdPlanAccionActividadResponsable { get; set; }
        public int IdPlanAccionActividad { get; set; }
        public int IdResponsable { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Usuario { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModifica { get; set; }
    }
}