using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsCriterioProveedorBLL
    {
        /// <summary>
        /// Metodo para consultar el aspecto
        /// </summary>
        /// <param name="lstAspecto">Lista con la informacion del aspecto</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarAspecto(ref List<clsAspectoProveedor> lstAspecto, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdConsultarAspecto(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsAspectoProveedor objAspecto = new clsAspectoProveedor(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["NombreAspecto"].ToString().Trim(),
                                Convert.ToDecimal(dr["Valor"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                            lstAspecto.Add(objAspecto);
                        }
                    }
                    else
                        lstAspecto = null;
                }
                else
                    lstAspecto = null;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para consultar el aspecto
        /// </summary>
        /// <param name="objAspectoIn">Objeto con la informacion del aspecto de entrada</param>
        /// <param name="objAspectoOut">Objeto con la informacion del aspecto de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarAspecto(clsAspectoProveedor objAspectoIn, ref clsAspectoProveedor objAspectoOut, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdConsultarAspecto(objAspectoIn, ref dtInfo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objAspectoOut = new clsAspectoProveedor(
                            Convert.ToInt32(dr["Id"].ToString().Trim()),
                            dr["NombreAspecto"].ToString().Trim(),
                            Convert.ToDecimal(dr["Valor"].ToString().Trim()),
                            Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                            dr["NombreUsuario"].ToString().Trim(),
                            dr["FechaRegistro"].ToString().Trim());
                    }
                }
                else
                    objAspectoOut = null;
            }
            else
                objAspectoOut = null;

            return booResult;
        }

        /// <summary>
        /// Metodo para realizar la sumatoria
        /// </summary>
        /// <param name="decPonderado">Informacion del ponderado</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdSumatoriaPonderado(ref Decimal decPonderado, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdSumatoriaPonderadoAspecto(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            decPonderado = Convert.ToDecimal(dr[0].ToString().Trim());
                        }
                    }
                    else
                        decPonderado = 0;
                }
                else
                    decPonderado = 0;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar el aspecto
        /// </summary>
        /// <param name="objAspecto">Objeto con la informacion del aspecto</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarAspecto(clsAspectoProveedor objAspecto, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();

            booResult = cDtProveedor.mtdInsertarAspecto(objAspecto, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para actualizar el aspecto
        /// </summary>
        /// <param name="objAspectoIn">Objeto con la informacion del aspecto</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarAspecto(clsAspectoProveedor objAspectoIn, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();

            booResult = cDtProveedor.mtdActualizarAspecto(objAspectoIn, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para consultar el criterio
        /// </summary>
        /// <param name="objAspectoIn">Objeto con la informacion del aspecto</param>
        /// <param name="lstCriterio">Lista con la informacion del criterio</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarCriterio(clsAspectoProveedor objAspectoIn, ref List<clsCriterioProveedor> lstCriterio, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdConsultarCriterio(objAspectoIn, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsCriterioProveedor objCriterio = new clsCriterioProveedor(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                Convert.ToInt32(dr["IdAspectoProveedor"].ToString().Trim()),
                                dr["Descripcion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                            lstCriterio.Add(objCriterio);
                        }
                    }
                    else
                        lstCriterio = null;
                }
                else
                    lstCriterio = null;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar el criterio
        /// </summary>
        /// <param name="objCriterio">Objeto con la informacion del criterio</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCriterio(clsCriterioProveedor objCriterio, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();

            booResult = cDtProveedor.mtdInsertarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para actualizar el criterio
        /// </summary>
        /// <param name="objCriterio">Objeto con la informacion del criterio</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarCriterio(clsCriterioProveedor objCriterio, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();

            booResult = cDtProveedor.mtdActualizarCriterio(objCriterio, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para consultar los parametros
        /// </summary>
        /// <param name="objCriterio">Objeto con la informacion del criterio</param>
        /// <param name="lstParametro">Lista con la informacion del parametro</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdConsultarParametros(clsCriterioProveedor objCriterio, ref List<clsParametros> lstParametro, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();
            #endregion Vars

            booResult = cDtProveedor.mtdConsultarParametro(objCriterio, ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsParametros objParam = new clsParametros(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["Descripcion"].ToString().Trim(),
                                Convert.ToInt32(dr["IdCriterioProv"].ToString().Trim()),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                dr["NombreUsuario"].ToString().Trim(),
                                dr["FechaRegistro"].ToString().Trim());
                            lstParametro.Add(objParam);
                        }
                    }
                    else
                        lstParametro = null;
                }
                else
                    lstParametro = null;
            }

            return booResult;
        }

        /// <summary>
        /// Metodo para insertar el parametro
        /// </summary>
        /// <param name="objParametro">Objeto con la informacion del parametro</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarParametro(clsParametros objParametro, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();

            booResult = cDtProveedor.mtdInsertarParametro(objParametro, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Metodo para actualizar el parametro
        /// </summary>
        /// <param name="objParametro">Objeto con la informacion del parametro</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarParametro(clsParametros objParametro, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtCriterioProveedor cDtProveedor = new clsDtCriterioProveedor();

            booResult = cDtProveedor.mtdActualizarParametro(objParametro, ref strErrMsg);

            return booResult;
        }

    }
}