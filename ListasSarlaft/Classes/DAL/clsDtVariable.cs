using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtVariable
    {
        /// <summary>
        /// Realiza la consulta para traer la Variable
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarVariable(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PV.[Id], PV.[Descripcion], PV.[Formato], PV.[FechaRegistro], PV.[IdUsuario], LU.[Usuario] NombreUsuario, PV.[Estado] " +
                    "FROM [Procesos].[tblVariable] PV " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PV.[IdUsuario] = LU.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Variable. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer la Variable
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarVariable(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PV.[Id], PV.[Descripcion], PV.[Formato], PV.[FechaRegistro], PV.[IdUsuario], LU.[Usuario] NombreUsuario, PV.[Estado] " +
                    "FROM [Procesos].[tblVariable] PV " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PV.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PV.[Estado] = {0}", booEstado == true ? 1 : 0);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Variable. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la consulta para traer la Variable
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public bool mtdConsultarVariable(clsVariable objVarIn, ref DataTable dtInfo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PV.[Id], PV.[Descripcion], PV.[Formato], PV.[FechaRegistro], PV.[IdUsuario], LU.[Usuario] NombreUsuario, PV.[Estado] " +
                    "FROM [Procesos].[tblVariable] PV " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PV.[IdUsuario] = LU.[IdUsuario] " +
                    "WHERE PV.[Id] = {0}", objVarIn.intId);

                cDatabase.conectar();
                dtInfo = cDatabase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdConsultarVariable(bool booEstado, clsIndicador objIndicador, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                /*strConsulta = string.Format("SELECT DISTINCT PV.[Id], PV.[Descripcion], PV.[Formato], PV.[IdUsuario], LU.[Usuario] NombreUsuario,  PV.[Estado], " +
                    "ISNULL(PDV.[Id],0) IdDetalleVariable, ISNULL(PDV.[Valor],0) Valor, " +
                    "PDV.IdDetallePeriodo, PDP.[Nombre] Periodo, " +
                    "ISNULL(PF.[IdIndicador],0) IdIndicador, PV.[FechaRegistro] " +
                    "FROM [Procesos].[tblVariable] PV " +
                    "INNER JOIN [Procesos].[tblFormula] PF  ON PF.[Valor] = CAST( PV.[Id] AS VARCHAR) " +
                    "LEFT JOIN [Procesos].[tblDetalleVariable] PDV ON  PDV.[IdVariable] = PV.[Id] " +
                    "LEFT JOIN [Procesos].[tblIndicador] PIn ON PIn.[Id] = PF.[IdIndicador] AND PIn.[Id] = PDV.[IdIndicador] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PV.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Procesos].[tblDetallePeriodo] PDP ON PDV.[IdDetallePeriodo] = PDP.[Id] " +
                    "WHERE PF.[IdOperando] = 1 AND PV.[Estado] = {0} AND PF.[IdIndicador] = {1} ",
                    booEstado == true ? 1 : 0, objIndicador.intId);*/
                strConsulta = string.Format("SELECT Descripcion, Formato, IdUsuario, NombreUsuario, Estado, IdDetalleVariable, " + "\n" +
                    "Valor, IdDetallePeriodo, Periodo, PeriodoAnual, IdIndicador, FechaRegistro, Id " + "\n" +
                    "FROM  vwListValorVariable " + "\n" +
                    "where Estado = {0} AND IdIndicador = {1} and PeriodoAnual = 0 and IdDetallePeriodo = {2} order by id, [IdDetallePeriodo], valor",
                    booEstado == true ? 1 : 0, objIndicador.intId, objIndicador.intIdPeriodicidad);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarVariableByYear(bool booEstado, clsIndicador objIndicador, ref DataTable dtInformacion, ref string strErrMsg, int year)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                /*strConsulta = string.Format("SELECT DISTINCT PV.[Id], PV.[Descripcion], PV.[Formato], PV.[IdUsuario], LU.[Usuario] NombreUsuario,  PV.[Estado], " +
                    "ISNULL(PDV.[Id],0) IdDetalleVariable, ISNULL(PDV.[Valor],0) Valor, " +
                    "PDV.IdDetallePeriodo, PDP.[Nombre] Periodo, " +
                    "ISNULL(PF.[IdIndicador],0) IdIndicador, PV.[FechaRegistro] " +
                    "FROM [Procesos].[tblVariable] PV " +
                    "INNER JOIN [Procesos].[tblFormula] PF  ON PF.[Valor] = CAST( PV.[Id] AS VARCHAR) " +
                    "LEFT JOIN [Procesos].[tblDetalleVariable] PDV ON  PDV.[IdVariable] = PV.[Id] " +
                    "LEFT JOIN [Procesos].[tblIndicador] PIn ON PIn.[Id] = PF.[IdIndicador] AND PIn.[Id] = PDV.[IdIndicador] " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PV.[IdUsuario] = LU.[IdUsuario] " +
                    "LEFT JOIN [Procesos].[tblDetallePeriodo] PDP ON PDV.[IdDetallePeriodo] = PDP.[Id] " +
                    "WHERE PF.[IdOperando] = 1 AND PV.[Estado] = {0} AND PF.[IdIndicador] = {1} ",
                    booEstado == true ? 1 : 0, objIndicador.intId);*/
                strConsulta = string.Format("SELECT        Descripcion, Formato, IdUsuario, NombreUsuario, Estado, IdDetalleVariable, " +
                    "Valor, IdDetallePeriodo, Periodo, PeriodoAnual, IdIndicador, FechaRegistro, Id " +
                    "FROM            vwListValorVariable " +
                    "where Estado = {0} AND IdIndicador = {1} and PeriodoAnual = {2} order by id, [IdDetallePeriodo], valor",
                    booEstado == true ? 1 : 0, objIndicador.intId, year);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdConsultarVariable(clsIndicador objIndicador, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT DISTINCT PV.[Id], PV.[Descripcion], PF.[IdIndicador] " +
                    "FROM [Procesos].[tblFormula] PF " +
                    "INNER JOIN [Procesos].[tblVariable] PV ON CAST(PF.[Valor] AS INT) = PV.[Id] " +
                    "WHERE PF.[IdOperando] = 1 AND  PV.[Estado] = 1 AND PF.[IdIndicador] = {0} ",
                    objIndicador.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la insercion del Variable.
        /// </summary>
        /// <param name="objVariable">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarVariable(clsVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblVariable]([Descripcion],[Formato],[FechaRegistro],[IdUsuario],[Estado]) " +
                    "VALUES('{0}','{1}',GETDATE(),{2},{3}) ",
                    objVariable.strDescripcion, objVariable.strFormato, objVariable.intIdUsuario, objVariable.booEstado == false ? 0 : 1);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la insercion del Variable.
        /// </summary>
        /// <param name="objVariable">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdActualizarVariable(clsVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblVariable] SET [Descripcion] = '{0}', [Formato] = '{1}' " +
                    "WHERE [Id] = {2} ",
                    objVariable.strDescripcion, objVariable.strFormato, objVariable.intId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        /// <summary>
        /// Realiza la actualizacion de la informacion en la base de datos
        /// </summary>
        /// <param name="objObjetivo">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsVariable objVariable, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblVariable]  SET [Estado] = {1}  WHERE [Id] = {0}",
                    objVariable.intId, objVariable.booEstado == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarDetalle(clsDetalleVariable objDetalle, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) ExisteDetalle FROM [Procesos].[tblDetalleVariable] " +
                    "WHERE [IdDetallePeriodo] = {0} AND [IdIndicador] = {1} AND idVariable = {2} and DATEPART(YEAR, FechaRegistro) = DATEPART(YEAR,GETDATE())",
                    //[FechaDesde] = CONVERT(DATETIME,'{0}',120) AND [FechaHasta] = CONVERT(DATETIME,'{1}',120)",
                    objDetalle.intIdDetallePeriodo, objDetalle.intIdIndicador, objDetalle.intValor);
                    

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);

                if (Convert.ToInt32(dtInformacion.Rows[0][0].ToString().Trim()) == 0 || objDetalle.Flag)
                {
                    if (!objDetalle.Flag)
                    {
                        strConsulta = string.Format("INSERT INTO[Procesos].[tblDetalleVariable] " +
                        "([IdVariable],[Valor],[IdDetallePeriodo],[FechaRegistro],[PeriodoAnual],[IdUsuario],[IdIndicador]) " +
                        "VALUES({0},{1},{2},GETDATE(),{3},{4},{5}) ",
                        objDetalle.intIdVariable, objDetalle.intValor, objDetalle.intIdDetallePeriodo, objDetalle.intPeriodoAnual,
                        objDetalle.intIdUsuario, objDetalle.intIdIndicador);
                    }

                    //else
                    //{
                    //    strConsulta = $"UPDATE [Procesos].[tblDetalleVariable] SET [Valor] = {objDetalle.intValor}, " +
                    //        $"[IdDetallePeriodo] = {objDetalle.intIdDetallePeriodo}, [PeriodoAnual]= {objDetalle.intPeriodoAnual}" +
                    //        $"WHERE ID = {objDetalle.IdDetalleVariable}";
                    //}

                    cDatabase.ejecutarQuery(strConsulta);
                    booResult = true;
                }
                else
                {
                    booResult = false;
                    strErrMsg = string.Format("El periodo seleccionado ya se encuentra registrado.");
                        
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el detalle de la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarDetalle(clsDetalleVariable objDetalle, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                //strConsulta = string.Format("SELECT COUNT(*) ExisteDetalle FROM [Procesos].[tblDetalleVariable] " +
                //    "WHERE [IdDetallePeriodo] = {0} AND [IdIndicador] = {1} AND idVariable = {2} and DATEPART(YEAR, FechaRegistro) = DATEPART(YEAR,GETDATE())",
                //    //[FechaDesde] = CONVERT(DATETIME,'{0}',120) AND [FechaHasta] = CONVERT(DATETIME,'{1}',120)",
                //    objDetalle.intIdDetallePeriodo, objDetalle.intIdIndicador, objDetalle.intValor);



                //dtInformacion = cDatabase.ejecutarConsulta(strConsulta);

                //if (Convert.ToInt32(dtInformacion.Rows[0][0].ToString().Trim()) == 0 || objDetalle.Flag)
                //{
                //    if (!objDetalle.Flag)
                //    {
                //strConsulta = string.Format("INSERT INTO[Procesos].[tblDetalleVariable] " +
                //"([IdVariable],[Valor],[IdDetallePeriodo],[FechaRegistro],[PeriodoAnual],[IdUsuario],[IdIndicador]) " +
                //"VALUES({0},{1},{2},GETDATE(),{3},{4},{5}) ",
                //objDetalle.intIdVariable, objDetalle.intValor, objDetalle.intIdDetallePeriodo, objDetalle.intPeriodoAnual,
                //objDetalle.intIdUsuario, objDetalle.intIdIndicador);
                // }

                //else
                //{
                strConsulta = $"UPDATE [Procesos].[tblDetalleVariable] SET [Valor] = {objDetalle.intValor}, " + "\n"+
                    $"[IdVariable] = {objDetalle.intIdVariable}," + "\n" +
                    $"[IdDetallePeriodo] = {objDetalle.intIdDetallePeriodo}, [PeriodoAnual]= {objDetalle.intPeriodoAnual} " + "\n" +
                    $"WHERE ID = {objDetalle.IdDetalleVariable}";
                //}
                cDatabase.conectar();
                    cDatabase.ejecutarQuery(strConsulta);
                    booResult = true;
                //}
                //else
                //{
                //    booResult = false;
                //    strErrMsg = string.Format("El periodo seleccionado ya se encuentra registrado.");

                //}
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el detalle de la Variable. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarNroVariables(clsDetalleVariable objDetalle, ref int intNumeroVariables, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) NumeroVars FROM " +
                    "(SELECT DISTINCT PV.[ID], PV.[Descripcion] FROM [Procesos].[tblFormula] PF " +
                    "INNER JOIN [Procesos].[tblVariable] PV ON CAST (PF.[Valor] AS INT) = PV.[Id] " +
                    "WHERE PF.[IdOperando] = 1 AND PF.[IdIndicador] = {0}) TEMP  ",
                    objDetalle.intIdIndicador);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);

                if (dtInformacion != null)
                    if (dtInformacion.Rows.Count > 0)
                        intNumeroVariables = Convert.ToInt32(dtInformacion.Rows[0][0].ToString().Trim());

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdConsultarNroCalificaciones(clsDetalleVariable objDetalle, ref int intNumeroCalificaciones, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT COUNT(*) NumeroCalif FROM [Procesos].[tblDetalleVariable] PDV " +
                    "WHERE PDV.[IdIndicador] = {0} AND PDV.[IdDetallePeriodo] = {1} and PDV.PeriodoAnual = {2}",
                    objDetalle.intIdIndicador, objDetalle.intIdDetallePeriodo, objDetalle.intPeriodoAnual);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);

                if (dtInformacion != null)
                    if (dtInformacion.Rows.Count > 0)
                        intNumeroCalificaciones = Convert.ToInt32(dtInformacion.Rows[0][0].ToString().Trim());

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Calificación de las variables del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
    }
}