using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOEventosVsUsuario
    {
        #region Definicion Parametros
        private int _IdEvento;
        private string _CodigoEvento;
        private string _DescripcionEvento;
        private decimal _CuantiaPerdida;
        private int _IdGeneraEvento;
        private int _GeneraEvento;
        private string _NombreGenerador;
        private int _IdResponsableEvento;
        private string _NombreResponsable;
        private int _IdClase;
        private string _NombreClaseEvento;
        #endregion Definicion Parametros
        #region Get/Set
        public int intIdEvento
        {
            get { return _IdEvento; }
            set { _IdEvento = value; }
        }
        public string strCodigoEvento
        {
            get { return _CodigoEvento; }
            set { _CodigoEvento = value; }
        }
        public string strDescripcionEvento
        {
            get { return _DescripcionEvento; }
            set { _DescripcionEvento = value; }
        }
        public decimal decCuantiaPerdida
        {
            get { return _CuantiaPerdida; }
            set { _CuantiaPerdida = value; }
        }
        public int intIdGeneraEvento
        {
            get { return _IdGeneraEvento; }
            set { _IdGeneraEvento = value; }
        }
        public int intGeneraEvento
        {
            get { return _GeneraEvento; }
            set { _GeneraEvento = value; }
        }
        public string strNombreGenerador
        {
            get { return _NombreGenerador; }
            set { _NombreGenerador = value; }
        }
        public int intIdResponsableEvento
        {
            get { return _IdResponsableEvento; }
            set { _IdResponsableEvento = value; }
        }
        public string strNombreResponsable
        {
            get { return _NombreResponsable; }
            set { _NombreResponsable = value; }
        }
        public int intIdClase
        {
            get { return _IdClase; }
            set { _IdClase = value; }
        }
        public string strNombreClaseEvento
        {
            get { return _NombreClaseEvento; }
            set { _NombreClaseEvento = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOEventosVsUsuario() { }
        #endregion Constructor
    }
}