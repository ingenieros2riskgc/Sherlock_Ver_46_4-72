using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class DocumentosCaracterizacion
    {
        public string NombreDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string CodigoDocumento { get; set; }
        public string FechaImplementacion { get; set; }
        public string NombreResponsable { get; set; }
        public string CargoResponsable { get; set; }
    }
}