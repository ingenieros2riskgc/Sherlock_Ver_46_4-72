using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALprocesoRiesgoIndicador
    {
        public bool mtdInsertarProcesoRiesgoIndicador(clsDTOprocesoRiesgoIndicador objProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Riesgos].[RiesgosIndicadoresProcesos] ([IdRiesgoIndicador],[IdProceso],[NombreProceso],[TipoProceso])" +
                    "VALUES({0},{1},'{2}',{3}) ", objProceso.intIdRiesgoIndicador, objProceso.intIdProceso, objProceso.strNombreProceso, objProceso.intTipoProceso
                    );

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el proceso del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public int mtdGetLastId(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int LastId = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT max([IdProcesoRiesgoIndicador]) LastId FROM [Riesgos].[RiesgosIndicadoresProcesos]");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                LastId = Convert.ToInt32(dtCaracOut.Rows[0]["LastId"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ultimo id registrado. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return LastId;
        }
        public int mtdGetTipoProceso(ref string strErrMsg, int IdProceso, int IdRiesgoProceso)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            DataTable dtCaracOut = new DataTable();
            int TipoProceso = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [TipoProceso] FROM [Riesgos].[RiesgosIndicadoresProcesos] where [IdProceso] = {0} and [IdRiesgoIndicador] = {1}",IdProceso,IdRiesgoProceso);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                TipoProceso = Convert.ToInt32(dtCaracOut.Rows[0]["TipoProceso"].ToString());
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el tipo proceso. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return TipoProceso;
        }
    }
}