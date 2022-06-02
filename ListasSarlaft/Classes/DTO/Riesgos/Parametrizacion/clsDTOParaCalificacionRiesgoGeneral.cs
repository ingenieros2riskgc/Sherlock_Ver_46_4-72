using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaCalificacionRiesgoGeneral
    {
        #region Variables
        private int _IdClasificacionGeneralRiesgo;
        private int _IdClasificacionRiesgo;
        private string _NombreClasificacionGeneralRiesgo;
        private int _IdUsuario;
        private DateTime _FechaRegistro;
        #endregion Variables
        #region Get/Set
        public int intIdClasificacionGeneralRiesgo
        {
            get { return _IdClasificacionGeneralRiesgo; }
            set { _IdClasificacionGeneralRiesgo = value; }
        }
        public int intIdClasificacionRiesgo
        {
            get { return _IdClasificacionRiesgo; }
            set { _IdClasificacionRiesgo = value; }
        }
        public string strNombreClasificacionGeneralRiesgo
        {
            get { return _NombreClasificacionGeneralRiesgo; }
            set { _NombreClasificacionGeneralRiesgo = value; }
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
        #endregion Get/Set
        #region Constructor
        public clsDTOParaCalificacionRiesgoGeneral() { }
        #endregion Constructor
    }
}