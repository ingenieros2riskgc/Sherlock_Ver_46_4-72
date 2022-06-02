using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtPoliticaCalidad
    {
        /// <summary>
        /// Realiza la consulta para traer la Politica Calidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarPoliticaCalidad(ref string strErrMsg)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PPC.[Id], PPC.[Descripcion], PPC.[FechaRegistro], PPC.[IdUsuario], LU.[Usuario] NombreUsuario " +
                    "FROM [Procesos].[tblPoliticaCalidad] PPC " +
                    "INNER JOIN [Listas].[Usuarios] LU ON PPC.[IdUsuario] = LU.[IdUsuario] ");

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Política Calidad. [{0}]", ex.Message);
            }
            finally
            {
                cDataBase.desconectar();
            }

            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion de una Politica.
        /// </summary>
        /// <param name="objPolitica">objeto con la informacion de la Politica</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>booleano que resuelve si la operacion fue exitosa o no</returns>
        public bool mtdInsertarPoliticaCalidad(clsPoliticaCalidad objPolitica, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Procesos].[tblPoliticaCalidad]([Descripcion],[FechaRegistro],[IdUsuario]) " +
                    "VALUES('{0}',GETDATE(),{1})",
                    objPolitica.strDescripcion, objPolitica.intIdUsuario);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Política de Calidad. [{0}]", ex.Message);
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
        /// <param name="objPolitica">Informacion de Politica</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarPoliticaCalidad(clsPoliticaCalidad objPolitica, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblPoliticaCalidad] SET [Descripcion] = '{1}' " +
                    "WHERE [Id] = {0}",
                    objPolitica.intId, objPolitica.strDescripcion);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar la Política de Calidad. [{0}]", ex.Message);
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