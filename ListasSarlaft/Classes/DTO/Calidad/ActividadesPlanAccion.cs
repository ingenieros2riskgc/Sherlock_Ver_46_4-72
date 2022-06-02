using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class ActividadesPlanAccion
    {
        public int IdActividad { get; set; }
        public string Nombre { get; set; }
        public int IdAcm { get; set; }
        public int Estado { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaCierre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Usuario { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}