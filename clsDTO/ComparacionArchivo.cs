using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class ComparacionArchivo
    {
        public int IdArchivo1 { get; set; }
        public int IdArchivo2 { get; set; }
        public int IdCampoEvaluar1 { get; set; }
        public int IdCampoEvaluar2 { get; set; }
        public string Operador { get; set; }
        public string Filtro { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public int IdUsuario { get; set; }
    }
}
