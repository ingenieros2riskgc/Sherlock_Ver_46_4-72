using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class InfoArchivoSegmentacion
    {
        public int IdInfoArchivo { get; set; }
        public int IdArchivo { get; set; }
        public string ValorCampoArchivo { get; set; }
        public int Posicion { get; set; }
        public int NumeroLinea { get; set; }
    }
}
