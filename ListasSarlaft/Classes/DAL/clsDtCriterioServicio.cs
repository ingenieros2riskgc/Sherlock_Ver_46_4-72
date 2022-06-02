using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDtCriterioServicio
    {
        public bool mtdInsertarCriterioServicio(clsCriterioServicio cCriterioServicio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                
                    strConsulta = string.Format("INSERT INTO [Procesos].[tblCriterioServicio] ([RangoInicial],[RangoFinal],[DescripcionAprobacion],[FechaRegistro],[IdUsuario])"
                    +" VALUES  ({0},{1},'{2}','{3}',{4})",
                    cCriterioServicio.intRangoInicial, clsUtilidades.mtdQuitarComasAPuntos(""+cCriterioServicio.intRangoFinal), clsUtilidades.mtdQuitarComasAPuntos(""+cCriterioServicio.strDescripcion), cCriterioServicio.dtFechaRegistro, cCriterioServicio.intIdUsuario
                    );
               
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al registrar el criterio evaluación de servicio. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdConsultarCriterioServicio(ref DataTable dtCaracOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDataBase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT a.[IdCriterioServicio],a.[RangoInicial],a.[RangoFinal],a.[DescripcionAprobacion],a.[FechaRegistro],a.[IdUsuario],b.Usuario"
                    + " FROM [Procesos].[tblCriterioServicio] as a"
                    + " inner join Listas.Usuarios as b on b.IdUsuario = a.IdUsuario"
                    );

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
        public bool mtdUpdateCriterioServicio(clsCriterioServicio cCriterioServicio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("UPDATE [Procesos].[tblCriterioServicio] SET [RangoInicial] = {1},[RangoFinal] = {2},[DescripcionAprobacion] = '{3}'"
                + " WHERE IdCriterioServicio={0}",
                cCriterioServicio.intIdCriterioServicio, clsUtilidades.mtdQuitarComasAPuntos(""+cCriterioServicio.intRangoInicial), clsUtilidades.mtdQuitarComasAPuntos(""+cCriterioServicio.intRangoFinal), cCriterioServicio.strDescripcion
                );
                
                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al registrar el criterio evaluación de servicio. [{0}]", ex.Message);
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