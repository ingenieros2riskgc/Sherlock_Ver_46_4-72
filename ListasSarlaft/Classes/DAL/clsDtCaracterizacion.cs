using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtCaracterizacion
    {
        public bool mtdConsultarCaracterizacion(clsCaracterizacion objCaracIn, ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdTipoProceso],[IdProceso],[FechaRegistro],[IdUsuario], [Recursos], [Numerales], [Responsables], [Codigo] " +
                    "FROM [Procesos].[tblCaracterizacion] " +
                    "WHERE [IdTipoProceso] = {0} AND [IdProceso] = {1} ",
                    objCaracIn.intIdTipoProceso, objCaracIn.intIdProceso);

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarCaracterizacion(clsCaracterizacion objCaracIn, ref clsCaracterizacion objCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblCaracterizacion]([IdTipoProceso],[IdProceso],[FechaRegistro],[IdUsuario]) " +
                    "VALUES({0},{1},GETDATE(),{2}) " +
                    "SELECT SCOPE_IDENTITY() ",
                    objCaracIn.intIdTipoProceso, objCaracIn.intIdProceso, objCaracIn.intIdUsuario);

                cDatabase.mtdConectarSql();
                dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        objCaracOut = new clsCaracterizacion(Convert.ToInt32(dtInfo.Rows[0][0].ToString()),
                            objCaracIn.intIdTipoProceso, objCaracIn.intIdProceso, objCaracIn.intIdUsuario, string.Empty);

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdInsertarCaracterEntrada(List<clsCaracterizacionXEntrada> lstEntrada, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                cDatabase.mtdConectarSql();
                foreach (clsCaracterizacionXEntrada objTemp in lstEntrada)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblCaractXEntrada]([IdCaracterizacion],[IdEntrada],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objCaracIn.intId, objTemp.intIdEntrada, objCaracIn.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al asociar la caracterización con la entrada. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdInsertarCaracterActividad(List<clsCaracterizacionXActividad> lstActividad, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                cDatabase.mtdConectarSql();
                foreach (clsCaracterizacionXActividad objTemp in lstActividad)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblCaractXActividad]([IdCaracterizacion],[IdActividad],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objCaracIn.intId, objTemp.intIdActividad, objCaracIn.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al asociar la caracterización con la Actividad. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdInsertarCaracterSalida(List<clsCaracterizacionXSalida> lstSalida, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                cDatabase.mtdConectarSql();
                foreach (clsCaracterizacionXSalida objTemp in lstSalida)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblCaractXSalida]([IdCaracterizacion],[IdSalida],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objCaracIn.intId, objTemp.intIdSalida, objCaracIn.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al asociar la caracterización con la Salida. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdActualizarCaracterizacion(clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = $"UPDATE [Procesos].[tblCaracterizacion] SET [IdTipoProceso] = {objCaracIn.intIdTipoProceso},[IdProceso] = {objCaracIn.intIdProceso}, [Recursos] = '{objCaracIn.Recursos}'" +
                    $", [Numerales] = '{objCaracIn.Numerales}', [Responsables] = '{objCaracIn.Responsables}', [Codigo] = '{objCaracIn.Codigo}' WHERE [Id] = {objCaracIn.intId}";
                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la asociación de caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdActualizarCaracterEntrada(List<clsCaracterizacionXEntrada> lstEntrada, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Procesos].[tblCaractXEntrada] WHERE [IdCaracterizacion] = {0} ",
                    objCaracIn.intId);

                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                foreach (clsCaracterizacionXEntrada objTemp in lstEntrada)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblCaractXEntrada]([IdCaracterizacion],[IdEntrada],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objTemp.intIdCaracterizacion, objTemp.intIdEntrada, objTemp.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la asociación de caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdActualizarCaracterActividad(List<clsCaracterizacionXActividad> lstActividad, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Procesos].[tblCaractXActividad] WHERE [IdCaracterizacion] = {0} ",
                    objCaracIn.intId);

                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                foreach (clsCaracterizacionXActividad objTemp in lstActividad)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblCaractXActividad]([IdCaracterizacion],[IdActividad],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objTemp.intIdCaracterizacion, objTemp.intIdActividad, objTemp.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la asociación de caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdActualizarCaracterSalida(List<clsCaracterizacionXSalida> lstSalida, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Procesos].[tblCaractXSalida] WHERE [IdCaracterizacion] = {0} ",
                    objCaracIn.intId);

                cDatabase.mtdConectarSql();
                cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                foreach (clsCaracterizacionXSalida objTemp in lstSalida)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblCaractXSalida]([IdCaracterizacion],[IdSalida],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},{1},GETDATE(),{2}) ",
                        objTemp.intIdCaracterizacion, objTemp.intIdSalida, objTemp.intIdUsuario);

                    cDatabase.mtdEjecutarConsultaSQL(strConsulta);
                }

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la asociación de caracterización. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }
    }
}