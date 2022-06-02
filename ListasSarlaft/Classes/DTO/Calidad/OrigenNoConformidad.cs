using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class OrigenNoConformidad
    {
        public int IdOrigenNoConformidad { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModifica { get; set; }
        public string NombreUsuario { get; set; }
    }
}