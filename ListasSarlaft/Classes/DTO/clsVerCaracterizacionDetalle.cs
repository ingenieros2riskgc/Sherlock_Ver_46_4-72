using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsVerCaracterizacionDetalle
    {
        private string _DescripcionEntrada;
        private string _Proveedor;
        private string _DescripcionActividad;
        private string _CargoResponsable;
        private string _DescripcionSalida;
        private string _Cliente;
        private string _DescripcionProcedimiento;
        

        public string strDescripcionEntrada
        {
            get { return _DescripcionEntrada; }
            set { _DescripcionEntrada = value; }
        }
        public string strProveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }
        public string strDescripcionActividad
        {
            get { return _DescripcionActividad; }
            set { _DescripcionActividad = value; }
        }
        public string strCargoResponsable
        {
            get { return _CargoResponsable; }
            set { _CargoResponsable = value; }
        }
        public string strDescripcionSalida
        {
            get { return _DescripcionSalida; }
            set { _DescripcionSalida = value; }
        }
        public string strCliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }
        public string strDescripcionProcedimiento
        {
            get { return _DescripcionProcedimiento; }
            set { _DescripcionProcedimiento = value; }
        }
        
        #region Constructors
        public clsVerCaracterizacionDetalle()
        {
        }

        public clsVerCaracterizacionDetalle(string strDescripcionEntrada, string strProveedor, string strDescripcionActividad, string strCargoResponsable,
            string strDescripcionSalida, string strCliente, string strDescripcionProcedimiento)
        {
            this.strDescripcionEntrada = strDescripcionEntrada;
            this.strProveedor = strProveedor;
            this.strDescripcionActividad = strDescripcionActividad;
            this.strCargoResponsable = strCargoResponsable;
            this.strDescripcionSalida = strDescripcionSalida;
            this.strCliente = strCliente;
            this.strDescripcionProcedimiento = strDescripcionProcedimiento;
        }
        #endregion
    }
}