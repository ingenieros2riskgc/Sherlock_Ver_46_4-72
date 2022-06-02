using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTORegistroRequerimientos
    {
        private string strId;
        private string strNombre;
        private string strEmpresa;
        private string strNumReq;
        private string strFechaRegistro;
        private string strTipoFalla;
        private string ddlText;
        private string strDescripcion;
        private string strRutaError;

        public string StrId
        {
            get { return strId; }
            set { strId = value; }
        }
        public string StrNombre
        {
            get { return strNombre; }
            set { strNombre = value; }
        }
        public string StrEmpresa
        {
            get { return strEmpresa; }
            set { strEmpresa = value; }
        }
        public string StrNumReq
        {
            get { return strNumReq; }
            set { strNumReq = value; }
        }
        public string StrFechaRegistro
        {
            get { return strFechaRegistro; }
            set { strFechaRegistro = value; }
        }
        public string StrTipoFalla
        {
            get { return strTipoFalla; }
            set { strTipoFalla = value; }
        }
        public string DDLText
        {
            get { return ddlText; }
            set { ddlText = value; }
        }
        public string StrDescripcion
        {
            get { return strDescripcion; }
            set { strDescripcion = value; }
        }
        public string StrRutaError
        {
            get { return strRutaError; }
            set { strRutaError = value; }
        }

        public clsDTORegistroRequerimientos(string strId, string strNombre, string strEmpresa, string strNumReq, string strFechaRegistro, 
            string strTipoFalla, string ddlText, string strDescripcion, string strRutaError)
        {
            this.StrId = strId;
            this.StrNombre = strNombre;
            this.StrEmpresa = strEmpresa;
            this.StrNumReq = strNumReq;
            this.StrFechaRegistro = strFechaRegistro;
            this.StrTipoFalla = strTipoFalla;
            this.DDLText = ddlText;
            this.StrDescripcion = strDescripcion;
            this.StrRutaError = strRutaError;
        }
        public clsDTORegistroRequerimientos()
        {
        }

    }
}
