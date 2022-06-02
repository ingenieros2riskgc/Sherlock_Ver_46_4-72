using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsDTO
{
    public class clsDTOHistoricoCalculoPerfil
    {
        #region Properties
        private string strIdHistorico;
        private string strIdPerfil;
        private string strIdArchivo;
        private string strTipoDocCliente;
        private string strNroDocCliente;
        private string strNombreCliente;
        private string strCalificacionPerfil;
        private string strCodProducto;
        private string strInfoInspektor;
        private string strSenalesAlerta;
        private string strFechaGeneracion;
        private string strLinea;

        public string StrIdHistorico
        {
            get { return strIdHistorico; }
            set { strIdHistorico = value; }
        }

        public string StrIdPerfil
        {
            get { return strIdPerfil; }
            set { strIdPerfil = value; }
        }

        public string StrIdArchivo
        {
            get { return strIdArchivo; }
            set { strIdArchivo = value; }
        }

        public string StrTipoDocCliente
        {
            get { return strTipoDocCliente; }
            set { strTipoDocCliente = value; }
        }

        public string StrNroDocCliente
        {
            get { return strNroDocCliente; }
            set { strNroDocCliente = value; }
        }

        public string StrNombreCliente
        {
            get { return strNombreCliente; }
            set { strNombreCliente = value; }
        }

        public string StrCalificacionPerfil
        {
            get { return strCalificacionPerfil; }
            set { strCalificacionPerfil = value; }
        }

        public string StrCodProducto
        {
            get { return strCodProducto; }
            set { strCodProducto = value; }
        }

        public string StrInfoInspektor
        {
            get { return strInfoInspektor; }
            set { strInfoInspektor = value; }
        }

        public string StrSenalesAlerta
        {
            get { return strSenalesAlerta; }
            set { strSenalesAlerta = value; }
        }

        public string StrFechaGeneracion
        {
            get { return strFechaGeneracion; }
            set { strFechaGeneracion = value; }
        }

        public string StrLinea
        {
            get { return strLinea; }
            set { strLinea = value; }
        }
        #endregion Properties

        public clsDTOHistoricoCalculoPerfil() { }

        public clsDTOHistoricoCalculoPerfil(string strIdHistorico, string strIdPerfil,
            string strIdArchivo, string strTipoDocCliente, string strNroDocCliente, string strNombreCliente,
            string strCalificacionPerfil, string strCodProducto, string strInfoInspektor,
            string strSenalesAlerta, string strFechaGeneracion, string strLinea)
        {
            this.StrIdHistorico = strIdHistorico;
            this.StrIdPerfil = strIdPerfil;
            this.StrIdArchivo = strIdArchivo;
            this.StrTipoDocCliente = strTipoDocCliente;
            this.StrNroDocCliente = strNroDocCliente;
            this.StrNombreCliente = strNombreCliente;
            this.StrCalificacionPerfil = strCalificacionPerfil;
            this.StrCodProducto = strCodProducto;
            this.StrInfoInspektor = strInfoInspektor;
            this.StrSenalesAlerta = strSenalesAlerta;
            this.StrFechaGeneracion = strFechaGeneracion;
            this.StrLinea = strLinea;
        }

        public void SetInfoInspektor(string strInfo)
        {
            this.StrInfoInspektor = strInfo;
        }
    }
}
