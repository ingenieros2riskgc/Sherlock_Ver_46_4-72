using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using clsDTO;

namespace clsDatos
{
    public class clsDtFactorRiesgo
    {
        #region Factor Riesgo
        public DataTable mtdConsultarFactor(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdFactorRiesgo], [CodigoFactorRiesgo], [DescFactorRiesgo],[IdUsuario] FROM [Perfiles].[tblFactorRiesgo] " +
                    "ORDER BY [IdFactorRiesgo]");

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Factores de Riesgo. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public int mtdAgregarFactorRet(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            int intRetorno = 0;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT [Perfiles].[tblFactorRiesgo] ([CodigoFactorRiesgo], [DescFactorRiesgo]) " +
                    "VALUES ('{0}', '{1}') SELECT @@IDENTITY Ultimo",
                    objFactor.StrCodigoFactorRiesgo, objFactor.StrDescFactorRiesgo);
                cDatabase.mtdConectarSql();
                intRetorno = cDatabase.mtdEjecutarConsultaSQLRetorno(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Factor de Riesgo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return intRetorno;
        }

        public void mtdActualizarFactor(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Perfiles].[tblFactorRiesgo] " +
                    "SET [CodigoFactorRiesgo] = '{1}', [DescFactorRiesgo] = '{2}' WHERE [IdFactorRiesgo] = {0}",
                    objFactor.StrIdFactorRiesgo, objFactor.StrCodigoFactorRiesgo, objFactor.StrDescFactorRiesgo);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Factor de Riesgo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdEliminarFactor(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Perfiles].[tblFactorRiesgo] WHERE [IdFactorRiesgo] = {0}",
                        objFactor.StrIdFactorRiesgo);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    strErrMsg = "Error en la eliminación de la información. <br/> La información a borrar tiene relación con algun objeto. <br/> Por favor revise la información.";
                else
                    strErrMsg = string.Format("Error en la eliminación de la información.<br/> Descripción: {0}.", odbcEx.Message.ToString());
            }
            catch (OleDbException odbcEx)
            {
                strErrMsg = mtdOdbcError(odbcEx);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al borrar el Factor de Riesgo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        #endregion

        #region Relación Factor de Riesgo - Señal
        public void mtdEliminarRelacion(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Perfiles].[tblFactorSenal] WHERE [IdFactorRiesgo] = {0}",
                        objFactor.StrIdFactorRiesgo);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    strErrMsg = "Error en la eliminación de la información. <br/> La información a borrar tiene relación con algun objeto. <br/> Por favor revise la información.";
                else
                    strErrMsg = string.Format("Error en la eliminación de la información.<br/> Descripción: {0}.", odbcEx.Message.ToString());
            }
            catch (OleDbException odbcEx)
            {
                strErrMsg = mtdOdbcError(odbcEx);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al borrar la Relación Factor de Riesgo - Señal. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public void mtdGuardarRelacion(clsDTOFactorSenal objRelacion, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                cDatabase.conectar();

                strConsulta = string.Format("INSERT [Perfiles].[tblFactorSenal] ([IdFactorRiesgo] ,[IdSenal]) VALUES ({0}, {1})",
                        objRelacion.StrIdFactorRiesgo, objRelacion.StrIdSenal);
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Relación Factor de Riesgo - Señal. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }

        public DataTable mtdConsultarRelacion(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdFactorSenal], [IdFactorRiesgo], [IdSenal] FROM [Perfiles].[tblFactorSenal] " +
                    "WHERE [IdFactorRiesgo] = {0}" +
                    "ORDER BY [IdFactorSenal]", objFactor.StrIdFactorRiesgo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Relación Factor de Riesgo - Señal. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;
        }

        public DataSet mtdConsultarRelRiesgoSenal(clsDTOFactorRiesgo objFactor, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataSet dsInformacion = new DataSet();
            SqlParameter[] objSqlParams = new SqlParameter[1];
            #endregion Vars

            try
            {

                objSqlParams[0] = new SqlParameter("@FactorRiesgo", objFactor.StrIdFactorRiesgo);

                cDatabase.conectar();
                dsInformacion = cDatabase.mtdEjecutarSPParametroSQL("SP_RptFactorSenal", "dsRptFactorSenal", objSqlParams);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Relación Factor de Riesgo - Señal. [{0}]", ex.Message);
                dsInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dsInformacion;
        }
        #endregion

        private string mtdOdbcError(OleDbException Ex)
        {
            string strError = string.Empty;

            switch (Ex.ErrorCode)
            {
                case -2147217873:
                    strError = "<br/> La información a borrar tiene relación con otro objeto. <br/> Por favor revise la información.";
                    break;
                default:
                    strError = "Descripción: " + Ex.Message.ToString();
                    break;
            }

            return strError;
        }
    }
}
