using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAcmCierreAdjuntosDLL
    {
        public bool mtdInsertarAdjuntosCierre(clsAcmCierreAdjuntos objAdjuntos, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            DataTable dtInfo = new DataTable();
            cDataBase cDatabase = new cDataBase();
            clsActividad objLastAct = null;
            #endregion Vars
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                            new SqlParameter() { ParameterName = "@idAcmCierreAdjunto", SqlDbType = SqlDbType.Int, Value = objAdjuntos.intidAcmCierreAdjunto },
                            new SqlParameter() { ParameterName = "@nombreArchivo", SqlDbType = SqlDbType.VarChar, Value = objAdjuntos.strnombreArchivo },
                            new SqlParameter() { ParameterName = "@pathFile", SqlDbType = SqlDbType.VarChar, Value =  objAdjuntos.strpathFile },
                            new SqlParameter() { ParameterName = "@extension", SqlDbType = SqlDbType.VarChar, Value =  objAdjuntos.strextension },
                            new SqlParameter() { ParameterName = "@archivo", SqlDbType = SqlDbType.VarBinary, Value =  (byte[]) objAdjuntos.btArchivo },
                            new SqlParameter() { ParameterName = "@idAcm", SqlDbType = SqlDbType.Int, Value = objAdjuntos.intIdAcm }
                };
                cDatabase.EjecutarSPParametros("[Procesos].[InsertarActualizarAcmCierreAdjuntos]", parametros);
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                strErrMsg = "Error en el proceso: "+ex.Message;
            }
            return booResult;
        }
        /// <summary>
        /// Realiza la consulta para traer los archivos adjuntos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Tabla de datos con la informacion.</returns>
        public List<clsAcmCierreAdjuntos> mtdConsultarCierreAdjuntos(ref string strErrMsg, int idacm)
        {
            List<clsAcmCierreAdjuntos> lst = new List<clsAcmCierreAdjuntos>();
            try
                {
                cDataBase cDatabase = new cDataBase();
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                            new SqlParameter() { ParameterName = "@idAcm", SqlDbType = SqlDbType.Int, Value = idacm }
                };
                using (DataTable dt = cDatabase.EjecutarSPParametrosReturnDatatable("[Procesos].[ConsultaAdjuntosCierre]", parametros))
                    {
                        
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow Row in dt.Rows)
                            {
                                lst.Add(new clsAcmCierreAdjuntos()
                                {
                                    intidAcmCierreAdjunto = Convert.ToInt32(Row["idCierreAdjunto"].ToString()),
                                    strnombreArchivo = Row["nombreArchivo"].ToString(),
                                    strpathFile = Row["pathFile"].ToString(),
                                    strextension = Row["extension"].ToString(),
                                    btArchivo = Row["archivo"].Equals(DBNull.Value) ? null : (byte[])Row["archivo"],
                                    intIdAcm = Convert.ToInt32(Row["idacm"].ToString())
                                });
                            }
                        }
                        //return lst;
                    }
                }
                catch (Exception ex)
                {
                strErrMsg = "Error en la consulta: " + ex.Message;
                }
            return lst;
        }
        /// <summary>
        /// Realiza la eliminacion del archivo adjunto del Cierre de ACM
        /// </summary>
        /// <param name="intIdAcm">Id del acm</param>
        /// <param name="intIdAdjuntoAcm">Id del adjunto del acm</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdDeleteAdjuntoCierre(int idAcm,int idAdjuntoAcm, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            cDataBase cDatabase = new cDataBase();
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("DELETE FROM [Procesos].[tblAcmCierreAdjuntos] WHERE [idacm] = {0} and [idCierreAdjunto] = {1}",
                    idAcm, idAdjuntoAcm);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al eliminar el documento adjunto. [{0}]", ex.Message);
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