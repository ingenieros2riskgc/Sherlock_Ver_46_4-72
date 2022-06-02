using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOServicios
    {
        #region Variables
        private int _Id;
        private string _Descripcion;
        private int _IdUsuario;
        private string _Usuario;
        private string _FechaRegistro;
        #endregion Variables
        #region Get/Set
        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
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
        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOServicios() { }
        #endregion Constructor
    }
}