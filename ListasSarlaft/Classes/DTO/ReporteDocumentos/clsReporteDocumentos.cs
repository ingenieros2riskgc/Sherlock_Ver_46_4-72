using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteDocumentos
    {
        private int _Id;
        private string _NombreDocumento;
        private string _NombreArchivo;
        private string _CodigoDocumento;
        private string _FechaImplementacion;
        private string _Version;
        private string _NombreProceso;
        private string _NombreResponsable;
        private string _FechaModificacion;
        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string strNombreDocumento
        {
            get { return _NombreDocumento; }
            set { _NombreDocumento = value; }
        }
        public string strNombreArchivo
        {
            get { return _NombreArchivo; }
            set { _NombreArchivo = value; }
        }
        public string strCodigoDocumento
        {
            get { return _CodigoDocumento; }
            set { _CodigoDocumento = value; }
        }
        public string strFechaImplementacion
        {
            get { return _FechaImplementacion; }
            set { _FechaImplementacion = value; }
        }
        public string strVersion
        {
            get { return _Version; }
            set { _Version = value; }
        }
        public string strNombreProceso
        {
            get { return _NombreProceso; }
            set { _NombreProceso = value; }
        }
        public string strNombreResponsable
        {
            get { return _NombreResponsable; }
            set { _NombreResponsable = value; }
        }
        public string strFechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }
        public clsReporteDocumentos() { }
    }
}