using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsVersionDocumento
    {
        private int _Id;
        private int _IdMacroProceso;
        private string _NombreProceso;
        private string _NombreDocumento;
        private string _CodigoDocumento;
        private string _FechaImplementacion;
        private double _VersionActual;
        private string _FechaModificacion;
        private string _FechaEliminacion;
        private int _IdTipoDocumento;
        private string _NombreCargo;
        private int _Responsable;
        private string _Almacenamiento;
        private string _Recuperacion;
        private string _TiempoRetencionActivo;
        private string _TiempoRetencionInactivo;
        private string _DisposicionFinal;
        private string _MedioSoporte;
        private string _Formato;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private string _Usuario;
        private int _IdTipoProceso;
        public int intId
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public int intIdMacroProceso
        {
            get { return _IdMacroProceso; }
            set { _IdMacroProceso = value; }
        }

        public int IdCadenaValor { get; set; }

        public string NombreCadenaValor { get; set; }

        public int? IdProceso { get; set; }

        public string NombreProceso { get; set; }

        public int? IdSubproceso { get; set; }

        public string NombreSubproceso { get; set; }

        public int IdEstadoDocumento { get; set; }

        public string CorreoResponsable { get; set; }

        public string CorreoUsuario { get; set; }

        public string NombreTipoDocumento { get; set; }

        public int IdJerarquiaResponsable { get; set; }

        public int IdJerarquiaUsuario { get; set; }

        public string strNombreProceso
        {
            get
            {
                return _NombreProceso;
            }

            set
            {
                _NombreProceso = value;
            }
        }
        public string strNombreDocumento
        {
            get
            {
                return _NombreDocumento;
            }

            set
            {
                _NombreDocumento = value;
            }
        }

        public string strCodigoDocumento
        {
            get
            {
                return _CodigoDocumento;
            }

            set
            {
                _CodigoDocumento = value;
            }
        }

        public string dtFechaImplementacion
        {
            get
            {
                return _FechaImplementacion;
            }

            set
            {
                _FechaImplementacion = value;
            }
        }
        public double intVersionActual
        {
            get
            {
                return _VersionActual;
            }

            set
            {
                _VersionActual = value;
            }
        }

        public int intIdTipoDocumento
        {
            get
            {
                return _IdTipoDocumento;
            }

            set
            {
                _IdTipoDocumento = value;
            }
        }

        public int intidCargoResponsable
        {
            get
            {
                return _Responsable;
            }

            set
            {
                _Responsable = value;
            }
        }
        public string strNombreCargo
        {
            get
            {
                return _NombreCargo;
            }

            set
            {
                _NombreCargo = value;
            }
        }
        public string strUbicacionAlmacenamiento
        {
            get
            {
                return _Almacenamiento;
            }

            set
            {
                _Almacenamiento = value;
            }
        }

        public string strRecuperacion
        {
            get
            {
                return _Recuperacion;
            }

            set
            {
                _Recuperacion = value;
            }
        }

        public string strTiempoRetencionActivo
        {
            get
            {
                return _TiempoRetencionActivo;
            }

            set
            {
                _TiempoRetencionActivo = value;
            }
        }

        public string strTiempoRetencionInactivo
        {
            get
            {
                return _TiempoRetencionInactivo;
            }

            set
            {
                _TiempoRetencionInactivo = value;
            }
        }

        public string strDisposicionFinal
        {
            get
            {
                return _DisposicionFinal;
            }

            set
            {
                _DisposicionFinal = value;
            }
        }

        public string strMedioSoporte
        {
            get
            {
                return _MedioSoporte;
            }

            set
            {
                _MedioSoporte = value;
            }
        }

        public string strFormato
        {
            get
            {
                return _Formato;
            }

            set
            {
                _Formato = value;
            }
        }

        public int intIdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
            }
        }
        public string strUsuario
        {
            get
            {
                return _Usuario;
            }

            set
            {
                _Usuario = value;
            }
        }
        public DateTime dtFechaRegistro
        {
            get
            {
                return _FechaRegistro;
            }

            set
            {
                _FechaRegistro = value;
            }
        }
        public string dtFechaModificacion
        {
            get
            {
                return _FechaModificacion;
            }

            set
            {
                _FechaModificacion = value;
            }
        }
        public string dtFechaEliminacion
        {
            get
            {
                return _FechaEliminacion;
            }

            set
            {
                _FechaEliminacion = value;
            }
        }
        public int intIdTipoProceso
        {
            get
            {
                return _IdTipoProceso;
            }

            set
            {
                _IdTipoProceso = value;
            }
        }
        #region
        public clsVersionDocumento()
        {
        }

        public clsVersionDocumento(int intId, int intIdMacroProceso, string strCodigoDocumento, string dtFechaImplementacion,
            int intIdTipoDocumento, int intidCargoResponsable, string strUbicacionAlmacenamiento, string strRecuperacion,
            string strTiempoRetencionActivo, string strTiempoRetencionInactivo, string strDisposicionFinal,
            string strMedioSoporte, string strFormato, int intIdUsuario, DateTime dtFechaRegistro, DateTime dtFechaModificacion, int intVersionActual)
        {
            this.intId = intId;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strCodigoDocumento = strCodigoDocumento;
            this.dtFechaImplementacion = dtFechaImplementacion;
            this.intIdTipoDocumento = intIdTipoDocumento;
            this.intidCargoResponsable = intidCargoResponsable;
            this.strUbicacionAlmacenamiento = strUbicacionAlmacenamiento;
            this.strRecuperacion = strRecuperacion;
            this.strTiempoRetencionActivo = strTiempoRetencionActivo;
            this.strTiempoRetencionInactivo = strTiempoRetencionInactivo;
            this.strDisposicionFinal = strDisposicionFinal;
            this.strMedioSoporte = strMedioSoporte;
            this.strFormato = strFormato;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
            //31-10-2018 13:09
            this.dtFechaModificacion = dtFechaModificacion.ToString();
            this.intVersionActual = intVersionActual;
        }
        #endregion
    }
}