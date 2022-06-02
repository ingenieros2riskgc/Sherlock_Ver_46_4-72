using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsPlanDeMejoramiento
    {
        private int _Id;
        private int _IdMacroProceso;
        private string _Proceso;
        private string _DescripcionActividad;
        private string _PeriodoEvaluarInicial;
        private string _PeriodoEvaluarFinal;
        private string _PlanMejoramiento;
        private string _Recursos;
        private DateTime _FechaRegistro;
        private int _IdUsuario;
        private string _Usuario;
        private int _IdTipoProceso;
        public int intId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int intIdMacroProceso
        {
            get { return _IdMacroProceso; }
            set { _IdMacroProceso = value; }
        }
        public string strProceso
        {
            get { return _Proceso; }
            set { _Proceso = value; }
        }

        public string strDescripcionActividad
        {
            get { return _DescripcionActividad; }
            set { _DescripcionActividad = value; }
        }

        public string dtPeriodoEvaluarInicial
        {
            get { return _PeriodoEvaluarInicial; }
            set { _PeriodoEvaluarInicial = value; }
        }

        public string dtPeriodoEvaluarFinal
        {
            get { return _PeriodoEvaluarFinal; }
            set { _PeriodoEvaluarFinal = value; }
        }

        public string strPlanMejoramiento
        {
            get { return _PlanMejoramiento; }
            set { _PlanMejoramiento = value; }
        }

        public string strRecursos
        {
            get { return _Recursos; }
            set { _Recursos = value; }
        }

        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
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
        public int intIdTipoProceso
        {
            get { return _IdTipoProceso; }
            set { _IdTipoProceso = value; }
        }
        #region
        public clsPlanDeMejoramiento()
        {
        }

        public clsPlanDeMejoramiento(int intId, int intIdMacroProceso, string strDescripcionActividad, string dtPeriodoEvaluarInicial,
            string dtPeriodoEvaluarFinal, string _PlanMejoramiento, string _Recursos, int _IdUsuario, DateTime _FechaRegistro)
        {
            this.intId = intId;
            this.intIdMacroProceso = intIdMacroProceso;
            this.strDescripcionActividad = strDescripcionActividad;
            this.dtPeriodoEvaluarInicial = dtPeriodoEvaluarInicial;
            this.dtPeriodoEvaluarFinal = dtPeriodoEvaluarFinal;
            this.strPlanMejoramiento = strPlanMejoramiento;
            this.strRecursos = strRecursos;
            this.dtFechaRegistro = dtFechaRegistro;
            this.intIdUsuario = intIdUsuario;
        }
        #endregion
    }
}