using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOVariableCalificacionControl
    {
        #region Variables
        private int _IdCalificacionControl;
        private string _DescripcionVariable;
        private int _Activo;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;
        #endregion Variables
        #region Get/Set
        public int intIdCalificacionControl
        {
            get { return _IdCalificacionControl; }
            set { _IdCalificacionControl = value; }
        }
        public string strDescripcionVariable
        {
            get { return _DescripcionVariable; }
            set { _DescripcionVariable = value; }
        }
        public int booActivo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string strUsuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public double FlPesoVariable { get; set; }

        #endregion Get/Set
        #region Constructor
        public clsDTOVariableCalificacionControl() { }
        #endregion Constructor
    }
}