using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsInformacionGeneral
    {
        #region Declaracion
        private int _IdRespuestaServicio;
        private int _NumClientesEncuestados;
        private int _NumClientesAprobados;
        private int _IdIndicador;
        private string _NombreIndicador;
        private string _DescripcionIndicador;
        private int _IdPeriodicidad;
        private int _IdObjCalidad;
        private int _IdProcesoIndicador;
        private int _IdProceso;
        private string _Proceso;
        private int _NumMetasCumplidas;
        private int _NumMetasIncumplidas;
        private int _NumNoConformidad;
        private int _NumNoConformidadCierre;
        private int _NumAuditoria;
        private int _NumAuditoriaCumplida;
        #endregion Declaracion
        #region GET/SET
        public int intIdRespuestaServicio
        {
            get { return _IdRespuestaServicio; }
            set { _IdRespuestaServicio = value; }
        }
        public int intNumClientesEncuestados
        {
            get { return _NumClientesEncuestados; }
            set { _NumClientesEncuestados = value; }
        }
        public int intNumClientesAprobados
        {
            get { return _NumClientesAprobados; }
            set { _NumClientesAprobados = value; }
        }
        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }

        public string strNombreIndicador
        {
            get { return _NombreIndicador; }
            set { _NombreIndicador = value; }
        }
        public string strDescripcionIndicador
        {
            get { return _DescripcionIndicador; }
            set { _DescripcionIndicador = value; }
        }
        public int intIdPeriodicidad
        {
            get { return _IdPeriodicidad; }
            set { _IdPeriodicidad = value; }
        }
        public int intIdObjCalidad
        {
            get { return _IdObjCalidad; }
            set { _IdObjCalidad = value; }
        }
        public int intIdProcesoIndicador
        {
            get { return _IdProcesoIndicador; }
            set { _IdProcesoIndicador = value; }
        }
        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }
        public string strProceso
        {
            get { return _Proceso; }
            set { _Proceso = value; }
        }
        public int intNumMetasCumplidas
        {
            get { return _NumMetasCumplidas; }
            set { _NumMetasCumplidas = value; }
        }
        public int intNumMetasIncumplidas
        {
            get { return _NumMetasIncumplidas; }
            set { _NumMetasIncumplidas = value; }
        }
        public int intNumNoConformidad
        {
            get { return _NumNoConformidad; }
            set { _NumNoConformidad = value; }
        }
        public int intNumNoConformidadCierre
        {
            get { return _NumNoConformidadCierre; }
            set { _NumNoConformidadCierre = value; }
        }
        public int intNumAuditoria
        {
            get { return _NumAuditoria; }
            set { _NumAuditoria = value; }
        }
        public int intNumAuditoriaCumplida
        {
            get { return _NumAuditoriaCumplida; }
            set { _NumAuditoriaCumplida = value; }
        }
        #endregion GET/SET
        public clsInformacionGeneral() { }
    }
}