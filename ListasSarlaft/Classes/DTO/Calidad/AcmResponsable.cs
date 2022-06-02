using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class AcmResponsable
    {
        public int IdAcmResponsable { get; set; }
        public int IdResponsable { get; set; }
        public int IdAcm { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Usuario { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModifica { get; set; }
        public string Nombre { get; set; }
        public int Flag { get; set; }
    }
}