using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDALTratamientos
    {
        public bool mtdConsultarTratamientos(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT PT.[IdTratamiento],PT.[NombreTratamiento],PT.[UsuarioCreacion], Users.Usuario,PT.[FechaCreacion]"
                    + " FROM [Parametrizacion].[Tratamiento] as PT"
                    + " inner join Listas.Usuarios as Users on Users.IdUsuario = PT.UsuarioCreacion");

                cDataBase.conectar();
                dtCaracOut = cDataBase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los tratamientos. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDataBase.desconectar();
            }

            return booResult;
        }
        public bool mtdInsertarTratamiento(clsDTOTratamientos objTratamiento, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("INSERT INTO [Parametrizacion].[Tratamiento] ([NombreTratamiento],[UsuarioCreacion],[FechaCreacion])" +
                    "VALUES('{0}',{1},'{2}') ",
                    objTratamiento.strTratamiento, objTratamiento.intIdUsuario, objTratamiento.dtFechaRegistro);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear el tratamiento. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdUpdateTratamiento(clsDTOTratamientos objTratamiento, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("UPDATE [Parametrizacion].[Tratamiento] SET [NombreTratamiento] = '{0}'" +
                    " WHERE IdTratamiento = {1}",
                    objTratamiento.strTratamiento, objTratamiento.intIdTratamiento);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar el tratamiento. [{0}]", ex.Message);
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