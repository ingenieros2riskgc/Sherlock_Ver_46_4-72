using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ListasSarlaft.Classes.DTO.Calidad;
using System.Data.SqlClient;

namespace ListasSarlaft.Classes
{
    public class clsDtPerfil: IDisposable
    {
        private cDataBase cDataBase = new cDataBase();

        /// <summary>
        /// Realiza la consulta para traer todos los Perfiles
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public DataTable mtdConsultarPerfil(clsPerfil objPerfil)
        {
            #region Vars
            cDataBase cDataBase = new cDataBase();
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                List<Macroproceso> lst = new List<Macroproceso>();
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                   new SqlParameter() { ParameterName = "@IdMacroproceso", SqlDbType = SqlDbType.VarChar, Value = objPerfil.intIdMacroproceso.ToString()},
                   new SqlParameter() { ParameterName = "@IdJerarquia", SqlDbType = SqlDbType.VarChar, Value =  objPerfil.intIdJOrganizacional.ToString() },
                   new SqlParameter() { ParameterName = "@IdEstado", SqlDbType = SqlDbType.VarChar, Value =  objPerfil.EstadoPerfil.ToString() }
                };
                dtInformacion = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarPerfiles]", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtInformacion;
        }

        /// <summary>
        /// Realiza la insercion del Perfil.
        /// </summary>
        /// <param name="objPerfil">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarActualizarPerfil(clsPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                   new SqlParameter() { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = objPerfil.intId},
                   new SqlParameter() { ParameterName = "@IdCargo", SqlDbType = SqlDbType.Int, Value =  objPerfil.intIdJOrganizacional },
                   new SqlParameter() { ParameterName = "@IdMacroproceso", SqlDbType = SqlDbType.Int, Value =  objPerfil.intIdMacroproceso },
                   new SqlParameter() { ParameterName = "@IdEstado", SqlDbType = SqlDbType.Int, Value =  objPerfil.EstadoPerfil },
                   new SqlParameter() { ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value =  objPerfil.intIdUsuario },
                   new SqlParameter() { ParameterName = "@Codigo", SqlDbType = SqlDbType.VarChar, Value =  objPerfil.Codigo },
                   new SqlParameter() { ParameterName = "@Perfil", SqlDbType = SqlDbType.VarChar, Value =  objPerfil.strPerfil },
                   new SqlParameter() { ParameterName = "@IdJerarquiaAprueba", SqlDbType = SqlDbType.Int, Value =  objPerfil.IdJerarquiaAprueba },
                   new SqlParameter() { ParameterName = "@Resultado", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };
                cDataBase.EjecutarSPParametrosReturnInteger("[Procesos].[InsertarActualizarPerfil]", parametros);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el Perfil. [{0}]", ex.Message);
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
        /// <param name="objPerfil">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarPerfil(clsPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblPerfil] " +
                    "SET [IdCargoJO] = {1},[IdMacroProceso] = {2},[ResumenCargo] = '{3}',[Funciones] = '{4}',  " +
                    "[Roles] = '{5}',[Perfil] = '{6}',[Educacion] = '{7}',[Habilidades] ='{8}',[Formacion] = '{9}',[Experiencia] = '{10}', [Estado]= {11} " +
                    "WHERE [Id] = {0}", objPerfil.intId,
                    objPerfil.intIdJOrganizacional, objPerfil.intIdMacroproceso, objPerfil.strResumenCargo, objPerfil.strFunciones,
                    objPerfil.strRol, objPerfil.strPerfil, objPerfil.strEducacion, objPerfil.strHabilidades, objPerfil.strFormacion, objPerfil.strExperiencia,
                    objPerfil.EstadoPerfil);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Perfil. [{0}]", ex.Message);
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
        /// <param name="objPerfil">Informacion a actualizar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarEstado(clsPerfil objPerfil, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Procesos].[tblPerfil]  SET [Estado] = {1}  WHERE [Id] = {0}",
                    objPerfil.intId, objPerfil.booEstado == false ? 0 : 1);
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al modificar el Perfil. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public List<Macroproceso> Macroprocesos()
        {
            try
            {
                List<Macroproceso> lst = new List<Macroproceso>();
                List<SqlParameter> parametros = new List<SqlParameter>();
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarMacroproceso]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        lst.Add(new Macroproceso()
                        {
                            IdMacroProceso = Convert.ToInt32(Row["IdMacroProceso"]),
                            Nombre = Row["Nombre"].ToString(),
                            Estado = Convert.ToInt32(Row["Estado"]),
                            Responsable = Convert.ToInt32(Row["Responsable"])
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoDocumento> OpcionesEstadoPerfil()
        {
            try
            {
                List<EstadoDocumento> lst = new List<EstadoDocumento>();
                List<SqlParameter> parametros = new List<SqlParameter>();
                DataTable dt = cDataBase.EjecutarSPParametrosReturnDatatable("[Procesos].[SeleccionarEstadosDocumento]", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        lst.Add(new EstadoDocumento()
                        {
                            IdEstadoDocumento = Convert.ToInt32(row["IdEstadoDocumento"]),
                            NombreEstadoDocumento = row["NombreEstadoDocumento"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {

        }
    }
}