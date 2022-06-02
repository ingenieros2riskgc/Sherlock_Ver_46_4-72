using System;

namespace ListasSarlaft.Classes.DTO.Calidad
{
    public class PlanAccionActividad
    {
        public int IdActividad { get; set; }
        public string Nombre { get; set; }
        public int IdAcm { get; set; }
        public int Estado { get; set; }
        public string NombreEstado { get; set; }
        public string Observaciones { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaCierre { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int Usuario { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int UsuarioModifica { get; set; }
        public string Responsables { get; set; }
        public int IdPlanAccionActividadResponsable { get; set; }
        public int Flag { get; set; }
        public string ActividadCierre { get; set; }
    }
}