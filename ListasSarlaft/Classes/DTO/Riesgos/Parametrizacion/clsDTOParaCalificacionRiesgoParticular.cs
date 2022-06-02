using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaCalificacionRiesgoParticular
    {
        #region Variables
        private int _IdClasificacionParticularRiesgo;
        private int _IdClasificacionRiesgo;
        private string _NombreClasificacionParticularRiesgo;
        private int _IdUsuario;
        private DateTime _FechaRegistro;
        #endregion Variables
        #region Get/Set
        public int intIdClasificacionParticularRiesgo
        {
            get { return _IdClasificacionParticularRiesgo; }
            set { _IdClasificacionParticularRiesgo = value; }
        }
        public int intIdClasificacionRiesgo
        {
            get { return _IdClasificacionRiesgo; }
            set { _IdClasificacionRiesgo = value; }
        }
        public string strNombreClasificacionParticularRiesgo
        {
            get { return _NombreClasificacionParticularRiesgo; }
            set { _NombreClasificacionParticularRiesgo = value; }
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
        public clsDTOParaCalificacionRiesgoParticular() { }
        #endregion Constructor
    }
}