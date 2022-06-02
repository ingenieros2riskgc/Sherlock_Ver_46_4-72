using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsAspectoProveedorHistorico
    {
        private int _Id;
        private int _IdEvaluacionProveedor;
        private string _Aspecto;
        private decimal _Valor;
        private int _IdUsuario;
        private DateTime _FechaRegistro;

        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intEvaluacionProveedor
        {
            get { return _IdEvaluacionProveedor; }
            set { _IdEvaluacionProveedor = value; }
        }

        public string strAspecto
        {
            get { return _Aspecto; }
            set { _Aspecto = value; }
        }

        public decimal intValor
        {
            get { return _Valor; }
            set { _Valor = value; }
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

        #region Constructors
        public clsAspectoProveedorHistorico()
        {
        }

        public clsAspectoProveedorHistorico(int intId, int intEvaluacionProveedor, decimal intValor,
            string strAspecto, int intIdUsuario, DateTime dtFechaRegistro)
        {
            this.intId = intId;
            this.intEvaluacionProveedor = intEvaluacionProveedor;
            this.intValor = intValor;
            this.strAspecto = strAspecto;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}