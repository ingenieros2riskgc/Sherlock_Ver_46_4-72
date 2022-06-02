using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class Seguimiento
    {
        public int IdSeguimientoPlanAccionActividad { get; set; }
        public string Descripcion { get; set; }
        public int IdPlanAccionActividad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreActividad { get; set; }
        public string IdActividad { get; set; }
    }
}