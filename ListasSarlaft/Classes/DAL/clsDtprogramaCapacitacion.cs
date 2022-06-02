using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtprogramaCapacitacion
    {
        public bool mtdInsertarProgramaCapacitacion(clsProgramacionCapacitacion objPrograma, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars
            DateTime date1 = new DateTime(0001, 1, 1, 0, 0, 0);
            //int result = DateTime.Compare(objPrograma.dtFechaProgramada, date1);
            //int result2 = DateTime.Compare(objPrograma.dtFechaRealizada, date1);
            try
            {
                
                    /*if (result == 0)
                    {
                        strConsulta = string.Format("INSERT INTO [Procesos].[tblProgramacionCapacitacion] ([IdMacroproceso],[CargoResponsable],[Actividad],[Dirigido],[FechaProgramada],[FechaRealizada],[Evaluacion],[FechaRegistro],[IdUsuario],[Asistentes])" +
                                "VALUES({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}') ",
                                objPrograma.IdMacroProceso, objPrograma.IdCargoResponsable, objPrograma.strActividad, objPrograma.strDirigidoA, null,
                                objPrograma.dtFechaRealizada, objPrograma.intEvaluacion, objPrograma.dtFechaRegistro, objPrograma.intIdUsuario, objPrograma.strAsistentes);
                    }
                    else
                    {*/
                        strConsulta = string.Format("INSERT INTO [Procesos].[tblProgramacionCapacitacion] ([IdMacroproceso],[CargoResponsable],[Actividad],[Dirigido],[FechaProgramada],[FechaRealizada],[Evaluacion],[FechaRegistro],[IdUsuario],[Asistentes])" +
                                "VALUES({0},{1},'{2}','{3}',CONVERT(date,'{4}',126),CONVERT(date,'{5}',126),'{6}',GETDATE(),{8},'{9}') ",
                                objPrograma.IdMacroProceso, objPrograma.IdCargoResponsable, objPrograma.strActividad, objPrograma.strDirigidoA, objPrograma.dtFechaProgramada,
                                objPrograma.dtFechaRealizada, objPrograma.intEvaluacion, "GETDATE()", objPrograma.intIdUsuario, objPrograma.strAsistentes);
                    //}
                    cDatabase.conectar();
                
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarPrograma(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Id],a.[IdMacroproceso],b.[Nombre],a.[CargoResponsable],c.NombreHijo,a.[Actividad],a.[Dirigido],a.[FechaProgramada]"+
                ",a.[FechaRealizada],a.[Evaluacion],a.[FechaRegistro],a.[IdUsuario],a.[asistentes],d.Usuario"
                    + " FROM [Procesos].[tblProgramacionCapacitacion] as a INNER JOIN"
                    + " Procesos.Macroproceso AS b ON b.IdMacroProceso = a.IdMacroproceso INNER JOIN"
                    + " Parametrizacion.JerarquiaOrganizacional AS c ON c.idHijo = a.CargoResponsable INNER JOIN"
                    + " Listas.Usuarios AS d ON d.IdUsuario = a.IdUsuario");

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
        public bool mtdConsultarReportePrograma(ref DataTable dtCaracOut, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[Id],a.[IdMacroproceso],b.[Nombre],a.[CargoResponsable],c.NombreHijo,a.[Actividad],a.[Dirigido],a.[FechaProgramada]" +
                ",a.[FechaRealizada],a.[Evaluacion],a.[FechaRegistro],a.[IdUsuario],a.[asistentes],d.Usuario"
                    + " FROM [Procesos].[tblProgramacionCapacitacion] as a INNER JOIN"
                    + " Procesos.Macroproceso AS b ON b.IdMacroProceso = a.IdMacroproceso INNER JOIN"
                    + " Parametrizacion.JerarquiaOrganizacional AS c ON c.idHijo = a.CargoResponsable INNER JOIN"
                    + " Listas.Usuarios AS d ON d.IdUsuario = a.IdUsuario where a.FechaRegistro between '{0} 00:00:00' and '{1} 23:59:59'",fechaInicial, fechaFinal);

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
        public bool mtdUpdateProgramaCapacitacion(ref clsProgramacionCapacitacion objPrograma, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblProgramacionCapacitacion] SET [FechaRealizada] = '{0}' ,[Evaluacion] = '{1}',[asistentes] ='{2}'" +
                    " WHERE Id = {3} ",
                    objPrograma.dtFechaRealizada, objPrograma.intEvaluacion, objPrograma.strAsistentes, objPrograma.intId);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Evaluacion de Competencia. [{0}]", ex.Message);
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