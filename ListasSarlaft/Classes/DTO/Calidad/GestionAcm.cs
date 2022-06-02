using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class GestionAcm
    {
        public int IdAcm { get; set; }
        public int Proceso { get; set; }
        public int MacroProceso { get; set; }
        public int CadenaValor { get; set; }
        public int? Subproceso { get; set; }
        public string DescripcionNoConformidad { get; set; }
        public int OrigenNoConformidad { get; set; }
        public string CausasRaiz { get; set; }
        public string Codigo { get; set; }
        public byte[] AnalisisCausa { get; set; }
        public string NombreArchivo { get; set; }
        public string Extension { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; }
        public string VerificacionEficacia { get; set; }
        public string Observaciones { get; set; }
        public int UsuarioRegistra { get; set; }
        public int? UsuarioRevisa { get; set; }
        public int? UsuarioAprueba { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaCierre { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModifica { get; set; }
        public string NombreProceso { get; set; }
        public string NombreOrigenNoConformidad { get; set; }
        public string NombreEstadoAcm { get; set; }
        public string NombreUsuarioRegistra { get; set; }
        public string NombreUsuarioRevisa { get; set; }
        public string NombreUsuarioAprueba { get; set; }
        public string NombreUsuarioModifica { get; set; }
        //Se adiciona la columna de grupos de trabajo de forma manual
        public string GrupoTrabajo { get; set; }
    }
}