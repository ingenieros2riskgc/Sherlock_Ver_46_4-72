using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class SalidaDetalleCalificacion
    {
        public int IdDetalleCalificacion { get; set; }
        public int Posicion { get; set; }
        public string NombreVariable { get; set; }
        public double PesoVariable { get; set; }
        public string NombreCategoria { get; set; }
        public double PesoCategoria { get; set; }
        public double Calificacion { get; set; }
        public string Valor { get; set; }
        public string NumeroDocumento { get; set; }
        public int UltimoArchivo { get; set; }
    }
}
