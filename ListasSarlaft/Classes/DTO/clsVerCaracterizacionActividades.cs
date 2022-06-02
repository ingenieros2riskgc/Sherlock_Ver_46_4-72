using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsVerCaracterizacionActividades
    {
        private string _DescripcionActividad;
        private string _CargoResponsable;
        private string _DescripcionProcedimiento;
        private string _DescripcionPHVA;
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
        public string strDescripcionProcedimiento
        {
            get { return _DescripcionProcedimiento; }
            set { _DescripcionProcedimiento = value; }
        }
        public string strDescripcionPHVA
        {
            get { return _DescripcionPHVA; }
            set { _DescripcionPHVA = value; }
        }
        #region Constructors
        public clsVerCaracterizacionActividades()
        {
        }

        public clsVerCaracterizacionActividades(string strDescripcionActividad, string strCargoResponsable,
             string strDescripcionProcedimiento,string strDescripcionPHVA)
        {
            
            this.strDescripcionActividad = strDescripcionActividad;
            this.strCargoResponsable = strCargoResponsable;
            this.strDescripcionProcedimiento = strDescripcionProcedimiento;
            this.strDescripcionPHVA = strDescripcionPHVA;
        }
        #endregion
    }
}