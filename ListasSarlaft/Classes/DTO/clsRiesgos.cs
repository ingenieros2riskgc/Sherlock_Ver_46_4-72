using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsRiesgos
    {
        private int _IdRiesgo;
        private string _Codigo;
        private string _Nombre;
        private string _Descripcion;
        private int _IdRegion;
        private int _IdPais;
        private int _IdDepartamento;
        private int _IdCiudad;
        private int _IdOficinaSucursal;
        private int _IdCadenaValor;
        private int _IdMacroproceso;
        private int _IdProceso;
        private int _IdSubProceso;
        private int _IdActividad;
        private int _IdClasificacionRiesgo;
        private int _IdClasificacionGeneralRiesgo;
        private int _IdClasificacionParticularRiesgo;
        private int _IdFactorRiesgoOperativo;
        private int _IdTipoRiesgoOperativo;
        private int _IdTipoEventoOperativo;
        private int _IdRiesgoAsociadoOperativo;
        private int _IdResponsableRiesgo;
        private int _IdProbabilidad;
        private int _IdProbabilidadResidual;
        private int _IdImpacto;
        private int _IdImpactoResidual;
        private string _ListaRiesgoAsociadoLA;
        private string _ListaFactorRiesgoLAFT;
        private string _ListaCausas;
        private string _ListaConsecuencias;
        private string _OcurrenciaEventoHasta;
        private string _OcurrenciaEventoDesde;
        private string _PerdidaEconomicaDesde;
        private string _PerdidaEconomicaHasta;
        private string _ListaTratamiento;
        private bool _Anulado;
        private string _FechaRegistro;
        private int _IdUsuario;

        public int intIdRiesgo
        {
            get { return _IdRiesgo; }
            set { _IdRiesgo = value; }
        }

        public string strCodigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        public string strNombreRiesgo
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string strDescripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int intIdRegion
        {
            get { return _IdRegion; }
            set { _IdRegion = value; }
        }

        public int intIdPais
        {
            get { return _IdPais; }
            set { _IdPais = value; }
        }

        public int intIdDepartamento
        {
            get { return _IdDepartamento; }
            set { _IdDepartamento = value; }
        }

        public int intIdCiudad
        {
            get { return _IdCiudad; }
            set { _IdCiudad = value; }
        }

        public int intIdOficinaSucursal
        {
            get { return _IdOficinaSucursal; }
            set { _IdOficinaSucursal = value; }
        }

        public int intIdCadenaValor
        {
            get { return _IdCadenaValor; }
            set { _IdCadenaValor = value; }
        }

        public int intIdMacroproceso
        {
            get { return _IdMacroproceso; }
            set { _IdMacroproceso = value; }
        }

        public int intIdProceso
        {
            get { return _IdProceso; }
            set { _IdProceso = value; }
        }

        public int intIdSubProceso
        {
            get { return _IdSubProceso; }
            set { _IdSubProceso = value; }
        }

        public int intIdActividad
        {
            get { return _IdActividad; }
            set { _IdActividad = value; }
        }

        public int intIdClasificacionRiesgo
        {
            get { return _IdClasificacionRiesgo; }
            set { _IdClasificacionRiesgo = value; }
        }

        public int intIdClasificacionGeneralRiesgo
        {
            get { return _IdClasificacionGeneralRiesgo; }
            set { _IdClasificacionGeneralRiesgo = value; }
        }

        public int intIdClasificacionParticularRiesgo
        {
            get { return _IdClasificacionParticularRiesgo; }
            set { _IdClasificacionParticularRiesgo = value; }
        }

        public int intIdFactorRiesgoOperativo
        {
            get { return _IdFactorRiesgoOperativo; }
            set { _IdFactorRiesgoOperativo = value; }
        }

        public int intIdTipoRiesgoOperativo
        {
            get { return _IdTipoRiesgoOperativo; }
            set { _IdTipoRiesgoOperativo = value; }
        }

        public int intIdTipoEventoOperativo
        {
            get { return _IdTipoEventoOperativo; }
            set { _IdTipoEventoOperativo = value; }
        }

        public int intIdRiesgoAsociadoOperativo
        {
            get { return _IdRiesgoAsociadoOperativo; }
            set { _IdRiesgoAsociadoOperativo = value; }
        }

        public int intIdResponsableRiesgo
        {
            get { return _IdResponsableRiesgo; }
            set { _IdResponsableRiesgo = value; }
        }

        public int intIdProbabilidad
        {
            get { return _IdProbabilidad; }
            set { _IdProbabilidad = value; }
        }

        public int intIdProbabilidadResidual
        {
            get { return _IdProbabilidadResidual; }
            set { _IdProbabilidadResidual = value; }
        }

        public int intIdImpacto
        {
            get { return _IdImpacto; }
            set { _IdImpacto = value; }
        }

        public int intIdImpactoResidual
        {
            get { return _IdImpactoResidual; }
            set { _IdImpactoResidual = value; }
        }

        public string strListaRiesgoAsociadoLA
        {
            get { return _ListaRiesgoAsociadoLA; }
            set { _ListaRiesgoAsociadoLA = value; }
        }

        public string strListaFactorRiesgoLAFT
        {
            get { return _ListaFactorRiesgoLAFT; }
            set { _ListaFactorRiesgoLAFT = value; }
        }

        public string strListaCausas
        {
            get { return _ListaCausas; }
            set { _ListaCausas = value; }
        }

        public string strListaConsecuencias
        {
            get { return _ListaConsecuencias; }
            set { _ListaConsecuencias = value; }
        }

        public string strOcurrenciaEventoHasta
        {
            get { return _OcurrenciaEventoHasta; }
            set { _OcurrenciaEventoHasta = value; }
        }

        public string strOcurrenciaEventoDesde
        {
            get { return _OcurrenciaEventoDesde; }
            set { _OcurrenciaEventoDesde = value; }
        }

        public string strPerdidaEconomicaDesde
        {
            get { return _PerdidaEconomicaDesde; }
            set { _PerdidaEconomicaDesde = value; }
        }

        public string strPerdidaEconomicaHasta
        {
            get { return _PerdidaEconomicaHasta; }
            set { _PerdidaEconomicaHasta = value; }
        }

        public string strListaTratamiento
        {
            get { return _ListaTratamiento; }
            set { _ListaTratamiento = value; }
        }

        public bool booAnulado
        {
            get { return _Anulado; }
            set { _Anulado = value; }
        }

        public string dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public clsRiesgos()
        {
        }

        public clsRiesgos(int intIdRiesgo, string strCodigo, string strNombreRiesgo, string strDescripcion,
            int intIdRegion, int intIdPais, int intIdDepartamento, int intIdCiudad, int intIdOficinaSucursal,
            int intIdCadenaValor, int intIdMacroproceso, int intIdProceso, int intIdSubProceso, int intIdActividad,
            int intIdClasificacionRiesgo, int intIdClasificacionGeneralRiesgo, int intIdClasificacionParticularRiesgo,
            int intIdFactorRiesgoOperativo, int intIdTipoRiesgoOperativo, int intIdTipoEventoOperativo,
            int intIdRiesgoAsociadoOperativo, int intIdResponsableRiesgo, int intIdProbabilidad,
            int intIdProbabilidadResidual, int intIdImpacto, int intIdImpactoResidual, string strListaRiesgoAsociadoLA,
            string strListaFactorRiesgoLAFT, string strListaCausas, string strListaConsecuencias, 
            string strOcurrenciaEventoHasta, string strOcurrenciaEventoDesde, string strPerdidaEconomicaDesde, 
            string strPerdidaEconomicaHasta, string strListaTratamiento, bool booAnulado, int intIdUsuario, string dtFechaRegistro)
        {
            this.intIdRiesgo = intIdRiesgo;
            this.strCodigo = strCodigo;
            this.strNombreRiesgo = strNombreRiesgo;
            this.strDescripcion = strDescripcion;
            this.intIdRegion = intIdRegion;
            this.intIdPais = intIdPais;
            this.intIdDepartamento = intIdDepartamento;
            this.intIdCiudad = intIdCiudad;
            this.intIdOficinaSucursal = intIdOficinaSucursal;
            this.intIdCadenaValor = intIdCadenaValor;
            this.intIdMacroproceso = intIdMacroproceso;
            this.intIdProceso = intIdProceso;
            this.intIdSubProceso = intIdSubProceso;
            this.intIdActividad = intIdActividad;
            this.intIdClasificacionRiesgo = intIdClasificacionRiesgo;
            this.intIdClasificacionGeneralRiesgo = intIdClasificacionGeneralRiesgo;
            this.intIdClasificacionParticularRiesgo = intIdClasificacionParticularRiesgo;
            this.intIdFactorRiesgoOperativo = intIdFactorRiesgoOperativo;
            this.intIdTipoRiesgoOperativo = intIdTipoRiesgoOperativo;
            this.intIdTipoEventoOperativo = intIdTipoEventoOperativo;
            this.intIdRiesgoAsociadoOperativo = intIdRiesgoAsociadoOperativo;
            this.intIdResponsableRiesgo = intIdResponsableRiesgo;
            this.intIdProbabilidad = intIdProbabilidad;
            this.intIdProbabilidadResidual = intIdProbabilidadResidual;
            this.intIdImpacto = intIdImpacto;
            this.intIdImpactoResidual = intIdImpactoResidual;
            this.strListaRiesgoAsociadoLA = strListaRiesgoAsociadoLA;
            this.strListaFactorRiesgoLAFT = strListaFactorRiesgoLAFT;
            this.strListaCausas = strListaCausas;
            this.strListaConsecuencias = strListaConsecuencias;
            this.strOcurrenciaEventoHasta = strOcurrenciaEventoHasta;
            this.strOcurrenciaEventoDesde = strOcurrenciaEventoDesde;
            this.strPerdidaEconomicaDesde = strPerdidaEconomicaDesde;
            this.strPerdidaEconomicaHasta = strPerdidaEconomicaHasta;
            this.strListaTratamiento = strListaTratamiento;
            this.booAnulado = booAnulado;
            this.intIdUsuario = intIdUsuario;
            this.dtFechaRegistro = dtFechaRegistro;
        }
    }
}