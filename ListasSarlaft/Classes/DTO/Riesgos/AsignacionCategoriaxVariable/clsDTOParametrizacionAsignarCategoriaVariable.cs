using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParametrizacionAsignarCategoriaVariable
    {
        #region Variables
        private int _IdVariableCategoria;
        private int _IdVariable;
        private string _Variable;
        private int _IdCategoria;
        private string _Categoria;
        private int _IdUsuario;
        private string _Usuario;
        private DateTime _FechaRegistro;
        #endregion Variables
        #region Get/Set
        public int intIdVariableCategoria
        {
            get { return _IdVariableCategoria; }
            set { _IdVariableCategoria = value; }
        }
        public int intIdVariable
        {
            get { return _IdVariable; }
            set { _IdVariable = value; }
        }
        public string strVariable
        {
            get { return _Variable; }
            set { _Variable = value; }
        }
        public int intIdCategoria
        {
            get { return _IdCategoria; }
            set { _IdCategoria = value; }
        }
        public string strCategoria
        {
            get { return _Categoria; }
            set { _Categoria = value; }
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
        #endregion Get/Set
        #region Constructor
        public clsDTOParametrizacionAsignarCategoriaVariable() { }
        #endregion Constructor
    }
}