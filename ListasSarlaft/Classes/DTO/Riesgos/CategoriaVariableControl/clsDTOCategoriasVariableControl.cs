using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCategoriasVariableControl
    {
        #region Variables
        private int _IdCategoriaVariableControl;
        private string _DescripcionCategoria;
        private int _PesoCategoria;
        private int _Activo;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;
        #endregion Variables
        #region Get/Set
        public int intIdCategoriaVariableControl
        {
            get { return _IdCategoriaVariableControl; }
            set { _IdCategoriaVariableControl = value; }
        }
        public string strDescripcionCategoria
        {
            get { return _DescripcionCategoria; }
            set { _DescripcionCategoria = value; }
        }
        public int intPesoCategoria
        {
            get { return _PesoCategoria; }
            set { _PesoCategoria = value; }
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
        public int IdVariable { get; set; }

        #endregion Get/Set
        #region Constructor
        public clsDTOCategoriasVariableControl() { }
        #endregion Constructor
    }
}