using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class AcmGrupoTrabajo
    {
        public int IdAcmGrupoTrabajo { get; set; }
        public int IdGrupoTrabajo { get; set; }
        public int IdAcm { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Usuario { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModifica { get; set; }
        public string Nombre { get; set; }
        public int Flag { get; set; }
    }
}