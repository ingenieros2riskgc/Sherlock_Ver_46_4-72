using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsControlVersion
    {
        private int _Id;
        private int _IdVersionDocumento;
        private string _Version;
        private string _FechaModificacion;
        private string _FechaEliminacion;
        private string _Observaciones;
        private string _pathFile;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private int _IdRegistro;
        private int _bitActivo;
        private int _IdTipoDocumento;
        private string _TipoDocumento;
        

        public int IdMacroProceso { get; set; }

        public int IdProceso { get; set; }

        public int IdSubproceso { get; set; }
        
        public int IdEstadoDocumento { get; set; }

        public string CodigoDocumento { get; set; }

        public string FechaImplementacion { get; set; }

        public int CargoResponsable { get; set; }

        public string UbicacionAlmacemiento { get; set; }

        public string Recuperacion { get; set; }
        public string TiempoRetencionActivo { get; set; }
        public string TiempoRetencionInactivo { get; set; }

        public string DisposionFinal { get; set; }

        public string MedioSoporte { get; set; }

        public string Formato { get; set; }

        public string NombreDocumento { get; set; }

        public int IdTipoProceso { get; set; }

        public string JustificacionCambios { get; set; }

        public string JustificacionAprobacion { get; set; }

        public int UsuarioAprobacion { get; set; }
        public string NomUsuarioAprobacion { get; set; }

        public string FechaProbacion { get; set; }

        public string Nombres { get; set; }

        public int intbitActivo
        {
            get { return _bitActivo; }
            set { _bitActivo = value; }
        }
        public int intIdTipoDocumento
        {
            get { return _IdTipoDocumento; }
            set { _IdTipoDocumento = value; }
        }
        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdVersionDocumento
        {
            get { return _IdVersionDocumento; }
            set { _IdVersionDocumento = value; }
        }

        public string strVersion
        {
            get { return _Version; }
            set { _Version = value; }
        }

        public string dtFechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }

        public string dtFechaEliminacion
        {
            get { return _FechaEliminacion; }
            set { _FechaEliminacion = value; }
        }

        public string strObservaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }
        public string strPathFIle
        {
            get { return _pathFile; }
            set { _pathFile = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public int intIdRegistro
        {
            get { return _IdRegistro; }
            set { _IdRegistro = value; }
        }
        public string strTipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }
        #region Constructors
        public clsControlVersion()
        {
        }

        public clsControlVersion(int intId, int intIdVersionDocumento, string strVersion, string dtFechaModificacion,
            string dtFechaEliminacion, string strObservaciones, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intIdVersionDocumento = intIdVersionDocumento;
            this.strVersion = strVersion;
            this.dtFechaModificacion = dtFechaModificacion;
            this.dtFechaEliminacion = dtFechaEliminacion;
            this.strObservaciones = strObservaciones;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}