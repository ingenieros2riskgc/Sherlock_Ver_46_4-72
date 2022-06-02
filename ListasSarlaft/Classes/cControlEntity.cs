using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class cControlEntity
    {
        public int IdControl { get; set; }
        public string CodigoControl { get; set; }
        public string NombreControl { get; set; }
        public string DescripcionControl { get; set; }
        public string ObjetivoControl { get; set; }
        public int? Responsable { get; set; }
        public int IdPeriodicidad { get; set; }
        public int IdTest { get; set; }
        public int? IdClaseControl { get; set; }
        public int? IdTipoControl { get; set; }
        public int? IdResponsableExperiencia { get; set; }
        public int? IdDocumentacion { get; set; }
        public int? IdResponsabilidad { get; set; }
        public int IdCalificacionControl { get; set; }
        public int IdUsuario { get; set; }
        public int IdMitiga { get; set; }
        public string ResponsableEjecucion { get; set; }
        public int? Variable6 { get; set; }
        public int? Variable7 { get; set; }
        public int? Variable8 { get; set; }
        public int? Variable9 { get; set; }
        public int? Variable10 { get; set; }
        public int? Variable11 { get; set; }
        public int? Variable12 { get; set; }
        public int? Variable13 { get; set; }
        public int? Variable14 { get; set; }
        public int? Variable15 { get; set; }
        public string JustificacionCambios { get; set; }
        public string NombreUsuario { get; set; }
    }
}