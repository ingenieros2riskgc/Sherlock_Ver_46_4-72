using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace ListasSarlaft.Classes
{
    public class clsDtControlInfraestructuraDAL
    {
        public bool mtdInsertarControlInfraestructura(clsControlInfraestructura objCrlInfra, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                /*DateTime date1 = new DateTime(0001, 1, 1, 0, 0, 0);
                int result = DateTime.Compare(objCrlInfra.dtFechaCumplimiento, date1);
                if (result == 0)
                {
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblControlInfraestructura] ([IdMacroproceso],[CargoResponsable],[Actividad],[FechaProgramada],[FechaCumplimiento],[FechaRegistro],[IdUsuario],[Observaciones]) " +
                        "VALUES ({0},{1},'{2}','{3}','{4}','{5}',{6},'{7}')",
                        objCrlInfra.intIdMacroProceso, objCrlInfra.intResponsable, objCrlInfra.strActividad, objCrlInfra.dtFechaProgramada, null, objCrlInfra.dtFechaRegistro, objCrlInfra.intIdUsuario, objCrlInfra.strObservaciones);
                }
                else
                {*/

                    strConsulta = string.Format("INSERT INTO [Procesos].[tblControlInfraestructura] ([IdMacroproceso]," +
                        "[CargoResponsable],[Actividad],[FechaProgramada],[FechaCumplimiento],[FechaRegistro]," +
                        "[IdUsuario],[Observaciones],[allProcess]) " +
                        "VALUES ({0},{1},'{2}','{3}','{4}',{5},{6},'{7}',{8})",
                        objCrlInfra.intIdMacroProceso, objCrlInfra.intResponsable, objCrlInfra.strActividad, 
                        objCrlInfra.dtFechaProgramada, objCrlInfra.dtFechaCumplimiento, "GETDATE()", 
                        objCrlInfra.intIdUsuario, objCrlInfra.strObservaciones,objCrlInfra.intAllProcess);
                //}
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Factor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarControlInfraestructura(ref DataTable dtInformacion, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdMacroproceso],[CargoResponsable],[Actividad],[FechaProgramada],[FechaCumplimiento],[FechaRegistro]"
                + ",[IdUsuario],[NombreHijo],[Nombre],[Usuario],[Observaciones],[allProcess] FROM [dbo].[vwControlInfraestructura]");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Factor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarControlInfraestructuraReporte(ref DataTable dtInformacion, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDataBase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdMacroproceso],[CargoResponsable],[Actividad],[FechaProgramada],[FechaCumplimiento],[FechaRegistro]"
                + ",[IdUsuario],[NombreHijo],[Nombre],[Usuario],[Observaciones], allProcess FROM [dbo].[vwControlInfraestructura] where FechaRegistro between '{0} 00:00:00' and '{1} 23:59:59' and FechaCumplimiento = ''", fechaInicial, fechaFinal);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el Factor. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateControlInfraestructura(clsControlInfraestructura objCrlInfra, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Procesos].[tblControlInfraestructura] SET [Observaciones] = '{0}', [FechaCumplimiento] = '{2}' " +
                    "where Id = {1}",
                    objCrlInfra.strObservaciones, objCrlInfra.intId, objCrlInfra.dtFechaCumplimiento);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Factor. [{0}]", ex.Message);
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