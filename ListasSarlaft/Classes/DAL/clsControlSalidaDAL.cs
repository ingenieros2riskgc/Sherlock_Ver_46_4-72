using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsControlSalidaDAL
    {
        public DataTable mtdConsultarEstados(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[NombreEstado],[FechaRegistro],[IdUsuario] " +
                    " FROM [Procesos].[tblEstadoControlSalida]");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cadena de valor. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }
        public bool mtdInsertarControlSalida(clsControlSalida objControlSalida, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblControlSalida] ([IdMacroProceso],[IdEstadoCtrlSalida],[NoConformidad],[AccionesTomadas],[PersonaAutoriza],[FechaRegistro],[IdUsuario])" + "VALUES({0},{1},'{2}','{3}','{4}',{5},{6}) ",
objControlSalida.intIdMacroProceso, objControlSalida.intIdEstadoControlSalida, objControlSalida.strNoConformidad, objControlSalida.strAccionesTomadas, objControlSalida.intCargoResponsable, "GETDATE()", objControlSalida.intIdUsuario);



                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de salida. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdControlSalida(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(Id) LastId FROM [Procesos].[tblControlSalida]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el control de salida. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public bool mtdInsertarValorObservacion(clsObservacionControlSalida objObservacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblObservacionControlSalida] ([IdControlSalida],[Descripcion],[FechaRegistro],[IdUsuario])" +
                    "VALUES({0},'{1}',{2},{3}) ",
                    objObservacion.intIdControlSalida, objObservacion.strDescripcion, "GETDATE()", objObservacion.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdInsertarControlProceso(clsControlProceso objControlProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblControlProductoProceso]([IdControlProducto],[IdProceso],[IdTipoProceso])" +
                    "VALUES({0},{1},{2}) ",
                    objControlProceso.intIdControl, objControlProceso.intIdProcesos, objControlProceso.intIdTipoProceso);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la tabla de procesos por control. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarControlSalida(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [Id],[IdMacroProceso],[IdEstadoCtrlSalida],[NoConformidad],[AccionesTomadas],[PersonaAutoriza],[FechaRegistro]" +
                ",[IdUsuario],[Descripcion],[NombreHijo],[Nombre],[Usuario],[NombreEstado]" +
                " FROM [dbo].[vwControlSalida]");

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la No conformidad. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public bool mtdActualizarControlSalida(clsControlSalida objCrlSalida, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblControlSalida] SET [AccionesTomadas] = '{1}',[PersonaAutoriza] = {2}, IdEstadoCtrlSalida={3}" +
                    " WHERE Id = {0} ",
                    objCrlSalida.intId, objCrlSalida.strAccionesTomadas, objCrlSalida.strPersonaAutoriza, objCrlSalida.intIdEstadoControlSalida);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdActualizarObservacion(clsObservacionControlSalida objObservacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblObservacionControlSalida] SET [Descripcion] = '{1}'" +
                    " WHERE IdControlSalida = {0} ",
                    objObservacion.intIdControlSalida, objObservacion.strDescripcion);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Observacion de la Evaluacion de Servicio. [{0}]", ex.Message);
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