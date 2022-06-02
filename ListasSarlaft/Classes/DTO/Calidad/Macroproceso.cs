using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class Macroproceso
    {
        public int IdMacroProceso { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public int Responsable { get; set; }
    }
}