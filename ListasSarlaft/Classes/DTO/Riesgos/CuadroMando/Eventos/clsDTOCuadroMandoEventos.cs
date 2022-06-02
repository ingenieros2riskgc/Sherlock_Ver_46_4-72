using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroMandoEventos
    {
        #region Variables
        private double _NumEventos;
        private int _IdEstado;
        private string _DescripcionEstado;
        private int _IdUsuario;
        private string _Empresa;
        private DateTime _FechaEvento;
        private string _Registro;
        private int _IdEvento;
        private string _Area;
        #endregion Variables
        #region Get/Set
        public double intNumEventos
        {
            get { return _NumEventos; }
            set { _NumEventos = value; }
        }
        public int intIdEstado
        {
            get { return _IdEstado; }
            set { _IdEstado = value; }
        }
        public string strDescripcionEstado
        {
            get { return _DescripcionEstado; }
            set { _DescripcionEstado = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string strEmpresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        public DateTime dtFechaEvento
        {
            get { return _FechaEvento; }
            set { _FechaEvento = value; }
        }
        public string strRegistro
        {
            get { return _Registro; }
            set { _Registro = value; }
        }
        public int intIdEvento
        {
            get { return _IdEvento; }
            set { _IdEvento = value; }
        }
        public string strArea
        {
            get { return _Area; }
            set { _Area = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroMandoEventos() { }
        #endregion Constructor
    }
}