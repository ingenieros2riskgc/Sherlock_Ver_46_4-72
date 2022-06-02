using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsValorEvaluacionServicio
    {
        private int _IdPregunta;
        private string _NombreEncuesta;
        private int _CantidadPregunta;
        private string _DescripcionEmpresa;
        private string _TextoPregunta;

        public int intIdPregunta
        {
            get { return _IdPregunta; }
            set { _IdPregunta = value; }
        }
        public string strNombreEncuesta
        {
            get { return _NombreEncuesta; }
            set { _NombreEncuesta = value; }
        }
        public int intCantidadPregunta
        {
            get { return _CantidadPregunta; }
            set { _CantidadPregunta = value; }
        }
        public string strDescripcionEmpresa
        {
            get { return _DescripcionEmpresa; }
            set { _DescripcionEmpresa = value; }
        }
        public string strTextoPregunta
        {
            get { return _TextoPregunta; }
            set { _TextoPregunta = value; }
        }

        public clsValorEvaluacionServicio() { }
    }
    public class clsValorRespuesta
    {
        private int _IdEncuestaQ;
        private int _IdEvaluacionServicio;
        private int _IdPregunta;
        private string _Pregunta;
        private string _Respuesta;

        public int intIdEncuestaQ
        {
            get { return intIdEncuestaQ; }
            set { intIdEncuestaQ = value; }
        }
        public int intIdEvaluacionServicio
        {
            get { return _IdEvaluacionServicio; }
            set { _IdEvaluacionServicio = value; }
        }
        public int intIdPregunta
        {
            get { return _IdPregunta; }
            set { _IdPregunta = value; }
        }
        public string strRespuesta
        {
            get { return _Respuesta; }
            set { _Respuesta = value; }
        }
        public string strPregunta
        {
            get { return _Pregunta; }
            set { _Pregunta = value; }
        }

        public clsValorRespuesta() { }
    }
    public class clsObservacionServicio
    {
        private int _IdObservacion;
        private int _IdEvaServicio;
        private string _Observacion;
        private DateTime _fecha;
        private int _IdUsuario;

        public int intIdObservacion
        {
            get { return _IdObservacion; }
            set { _IdObservacion = value; }
        }
        public int intIdEvaServicio
        {
            get { return _IdEvaServicio; }
            set { _IdEvaServicio = value; }
        }
        public string strObservacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }
        public DateTime dtfecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public clsObservacionServicio() { }
    }
}