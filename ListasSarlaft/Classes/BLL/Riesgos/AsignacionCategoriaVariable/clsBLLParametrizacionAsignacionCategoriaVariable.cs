using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLParametrizacionAsignacionCategoriaVariable
    {
        /// <summary>
        /// Metodo para consultar y visualizar las Categorias de las Variables de Calificación Control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOCategoriasVariableControl> mtdConsultarCategoriaxVariablesControl(ref List<clsDTOCategoriasVariableControl> lstCategoria, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALParametrizacionAsignarCategoriaVariable cDtRegistro = new clsDALParametrizacionAsignarCategoriaVariable();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarCategoriaxVariablesControl(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOCategoriasVariableControl objCategoria = new clsDTOCategoriasVariableControl();
                            objCategoria.intIdCategoriaVariableControl = Convert.ToInt32(dr["IdCategoriaVariableControl"].ToString().Trim());
                            objCategoria.strDescripcionCategoria = dr["DescripcionCategoria"].ToString().Trim();
                            objCategoria.intPesoCategoria = Convert.ToInt32(dr["PasoCategoria"].ToString().Trim());
                            objCategoria.booActivo = Convert.ToInt32(dr["Activo"].ToString().Trim());
                            objCategoria.intIdUsuario = Convert.ToInt32(dr["UsuarioCreacion"].ToString().Trim());
                            objCategoria.dtFechaRegistro = Convert.ToDateTime(dr["FechaCreacion"].ToString().Trim());
                            objCategoria.strUsuario = dr["Usuario"].ToString().Trim();

                            lstCategoria.Add(objCategoria);
                        }
                    }
                    else
                        lstCategoria = null;
                }
                else
                    lstCategoria = null;
            }

            return lstCategoria;
        }
        /// <summary>
        /// Metodo para asignar categorias a las variables
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarVariableCalificacion(clsDTOParametrizacionAsignarCategoriaVariable variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALParametrizacionAsignarCategoriaVariable cDALvariable = new clsDALParametrizacionAsignarCategoriaVariable();

            booResult = cDALvariable.mtdInsertarCategoriaxVariablesControl(variable, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Categoria ha asido asignada con exitosamente";
            else
                strErrMsg = "Error: no fue posible llevar a cabo la asginación";
            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Categorias asginadas
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOParametrizacionAsignarCategoriaVariable> mtdConsultarCategoriasAsignadas(ref List<clsDTOParametrizacionAsignarCategoriaVariable> lstCategoria, ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALParametrizacionAsignarCategoriaVariable cDtRegistro = new clsDALParametrizacionAsignarCategoriaVariable();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarCategoriasAsignadas(ref dtInfo, ref strErrMsg, IdVariable);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsDTOParametrizacionAsignarCategoriaVariable objCategoria = new clsDTOParametrizacionAsignarCategoriaVariable();
                            objCategoria.intIdVariableCategoria = Convert.ToInt32(dr["IdVariableCategoria"].ToString().Trim());
                            objCategoria.intIdVariable = Convert.ToInt32(dr["IdVariable"].ToString());
                            objCategoria.strVariable = dr["DescripcionVariable"].ToString().Trim();
                            objCategoria.intIdCategoria = Convert.ToInt32(dr["IdCategoria"].ToString().Trim());
                            objCategoria.strCategoria = dr["DescripcionCategoria"].ToString().Trim();
                            objCategoria.intIdUsuario = Convert.ToInt32(dr["UsuarioCreacion"].ToString().Trim());
                            objCategoria.dtFechaRegistro = Convert.ToDateTime(dr["FechaCreacion"].ToString().Trim());
                            objCategoria.strUsuario = dr["Usuario"].ToString().Trim();

                            lstCategoria.Add(objCategoria);
                        }
                    }
                    else
                        lstCategoria = null;
                }
                else
                    lstCategoria = null;
            }

            return lstCategoria;
        }
        /// <summary>
        /// Metodo para asignar categorias a las variables
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarVariableCalificacion(clsDTOParametrizacionAsignarCategoriaVariable variable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALParametrizacionAsignarCategoriaVariable cDALvariable = new clsDALParametrizacionAsignarCategoriaVariable();

            booResult = cDALvariable.mtdActializarCategoriaxVariablesControl(variable, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Categoria ha asido actualizada con exitosamente";
            else
                strErrMsg = "Error: no fue posible llevar a cabo la actualización";
            return booResult;
        }
    }
}