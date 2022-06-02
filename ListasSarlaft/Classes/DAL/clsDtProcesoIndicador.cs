using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtProcesoIndicador
    {
        /// <summary>
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProcIndicador(clsProcesoIndicador ObjProcIN, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPI.[Id], PPI.[IdTipoProceso], PPI.[IdProceso], PPI.[FechaRegistro], PPI.[IdUsuario] " +
                    "FROM [Procesos].[tblProcesoIndicador] PPI " +
                    "INNER JOIN [Procesos].[tblTipoProceso] PTP ON PTP.[Id] = PPI.[IdTipoProceso] " +
                    "WHERE PPI.[IdProceso] = {0}", ObjProcIN.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProcIndicadorMod(clsProcesoIndicador ObjProcIN, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPI.[Id], PPI.[IdTipoProceso], PPI.[IdProceso], PPI.[FechaRegistro], PPI.[IdUsuario] " +
                    "FROM [Procesos].[tblProcesoIndicador] PPI " +
                    "INNER JOIN [Procesos].[tblTipoProceso] PTP ON PTP.[Id] = PPI.[IdTipoProceso] " +
                    "WHERE PPI.[Id] = {0}", ObjProcIN.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProcIndicadorIdProceso(clsProcesoIndicador ObjProcIN, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT top 1 PPI.[Id], PPI.[IdTipoProceso], PPI.[IdProceso], PPI.[FechaRegistro], PPI.[IdUsuario] " +
                    "FROM [Procesos].[tblProcesoIndicador] PPI " +
                    "INNER JOIN [Procesos].[tblTipoProceso] PTP ON PTP.[Id] = PPI.[IdTipoProceso] " +
                    "WHERE PPI.[IdProceso] = {0}", ObjProcIN.intId);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProcesoNoConformidad(clsProcesoIndicador ObjProcIN, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdNocoformidadProceso],[IdNoconformidad],[IdProceso],[IdTipoProceso]"
                    + " FROM [Procesos].[tblControlNoConformidadProceso] where IdProceso = {0}", ObjProcIN.intIdProceso);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        public DataTable mtdConsultarProcIndicadorIdProcesoDV(clsProcesoIndicador ObjProcIN, ref string strErrMsg, int IdTipoProceso)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT top 1 PPI.[Id], PPI.[IdTipoProceso], PPI.[IdProceso], PPI.[FechaRegistro], PPI.[IdUsuario] " +
                    "FROM [Procesos].[tblProcesoIndicador] PPI " +
                    "INNER JOIN [Procesos].[tblTipoProceso] PTP ON PTP.[Id] = PPI.[IdTipoProceso] " +
                    "WHERE PPI.[IdProceso] = {0} and IdTipoProceso ={1}", ObjProcIN.intId, IdTipoProceso);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del indicador. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProcesoControl(int IdControl, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdControlProceso],[IdControlProducto],[IdProceso],[IdTipoProceso]"
                    + " FROM[Procesos].[tblControlProductoProceso] where IdControlProducto = {0}", IdControl);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del Control Producto. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        /// <summary>
        /// Realiza la consulta para traer todos los procesos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarProcesoEvaCompetencia(int IdEvaluacion, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdEvaTipoProceso] ,[IdTipoProceso] ,[IdProceso] ,[IdEvaluacion]"
                    + " FROM [Procesos].[tblEvaluacionCompetenciaTipoProceso] where IdEvaluacion = {0}", IdEvaluacion);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del Control Producto. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        public bool mtdConsultarProcIndicador(clsProcesoIndicador ObjProcIN, ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPI.[Id], PPI.[IdTipoProceso], PPI.[IdProceso], PPI.[FechaRegistro], PPI.[IdUsuario] " +
                    "FROM [Procesos].[tblProcesoIndicador] PPI " +
                    "INNER JOIN [Procesos].[tblTipoProceso] PTP ON PTP.[Id] = PPI.[IdTipoProceso] " +
                    "WHERE PPI.[IdTipoProceso] = {0} AND PPI.[IdProceso] = {1}",
                    ObjProcIN.intIdTipoProceso, ObjProcIN.intIdProceso);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los Procesos del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }

        public bool mtdInsertarProcesoIndicador(clsProcesoIndicador ObjProcIN, ref int intIdProcInd, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblProcesoIndicador]([IdTipoProceso],[IdProceso],[FechaRegistro],[IdUsuario]) " +
                    "VALUES({0}, {1}, GETDATE(), {2}) " +
                    "SELECT SCOPE_IDENTITY() ",
                    ObjProcIN.intIdTipoProceso, ObjProcIN.intIdProceso, ObjProcIN.intIdUsuario);

                cDatabase.mtdConectarSql();
                dtInfo = cDatabase.mtdEjecutarConsultaSQL(strConsulta);

                if (dtInfo != null)
                    if (dtInfo.Rows.Count > 0)
                        intIdProcInd = Convert.ToInt32(dtInfo.Rows[0][0].ToString());

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear los Procesos del indicador. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.mtdDesconectarSql();
            }

            return booResult;
        }

        public bool mtdActualizarProcesoIndicador(clsProcesoIndicador ObjProcIN, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblProcesoIndicador] SET [IdTipoProceso] = {0}, [IdProceso] = {1} " +
                    "WHERE [Id] = {2} ",
                    ObjProcIN.intIdTipoProceso, ObjProcIN.intIdProceso, ObjProcIN.intId);

                cDatabase.conectar();
                cDatabase.ejecutarConsulta(strConsulta);

                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear los Procesos del indicador. [{0}]", ex.Message);
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