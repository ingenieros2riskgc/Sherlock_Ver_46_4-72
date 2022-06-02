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
    public class clsDtGestionGruposUsuarios
    {
        
        public DataTable mtdConsulGrupoTrabajo(ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [idGrupoSoporte], [NombreGrupoSoporte] FROM QA.parametrizacionGruposSoporte");
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;

        }


        public bool mtdActualizarGrupo(clsDTOGestionGrupos objUpdReq, ref string strErrMsg, Boolean booResult)
        {
            #region Vars
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [QA].[parametrizacionGruposSoporte] SET [NombreGrupoSoporte] = '{0}' WHERE idGrupoSoporte = {1}",
                    objUpdReq.StrNombreGrupoTrabajo, objUpdReq.StrIdGrupoTrabajo);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el grupo de trabajo. [{0}]", ex.Message);
            }
            finally
            {
                booResult = true;
                cDatabase.desconectar();
            }
            return booResult;
        }

        public void mtdActualizarIntegrante(clsDTOGestionUsuarios objUpdUsu, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInformacion = new DataTable();
            clsDatabase cDatabase = new clsDatabase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE QA.parametrizacionUsuariosSoporte SET [NombreUsuarioSoporte] = '{0}', " +
                    "UsuarioSoporte = '{1}', CorreUsuarioSoporte = '{2}' WHERE IdUsuarioSoporte = {3}",
                    objUpdUsu.StrNombreIntegrante, objUpdUsu.StrUsuarioIntegrante, objUpdUsu.StrCorreoIntegrante, objUpdUsu.StrIdIntegrante);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el grupo de trabajo. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }
        }


        public DataTable mtdConsulIntegrante(string strIds, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [IdUsuarioSoporte], [NombreUsuarioSoporte], [UsuarioSoporte], [CorreUsuarioSoporte]," +
                    "[idGrupoSoporte] FROM [QA].[parametrizacionUsuariosSoporte] WHERE [idGrupoSoporte] = {0}", strIds);
                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar las variables. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dtInformacion;

        }

        public DataTable mtdInsertarGrupoUsuario(clsDTOGestionGrupos objGrupUsu, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [QA].[parametrizacionGruposSoporte]([NombreGrupoSoporte]) " +
                    "VALUES ('{0}')",
                    objGrupUsu.StrNombreGrupoTrabajo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el requerimiento. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
            return dtInformacion;
        }

        public DataTable mtdInsertarUsuario(clsDTOGestionUsuarios objUsuario, ref string strErrMsg)
        {
            #region Vars
            clsDatabase cDatabase = new clsDatabase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO QA.parametrizacionUsuariosSoporte ([NombreUsuarioSoporte], [UsuarioSoporte], " +
                    "[CorreUsuarioSoporte], [idGrupoSoporte]) VALUES ('{0}', '{1}', '{2}', {3})", objUsuario.StrNombreIntegrante, 
                    objUsuario.StrUsuarioIntegrante, objUsuario.StrCorreoIntegrante, objUsuario.StrIdGrupoTrabajo);

                cDatabase.conectar();
                dtInformacion = cDatabase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el requerimiento. [{0}]", ex.Message);
                dtInformacion = null;
            }
            finally
            {
                cDatabase.desconectar();
            }
            return dtInformacion;
        }

    }
}
