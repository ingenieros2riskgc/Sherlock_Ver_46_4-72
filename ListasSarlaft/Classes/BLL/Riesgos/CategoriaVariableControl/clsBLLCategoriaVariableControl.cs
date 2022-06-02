using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLCategoriaVariableControl
    {
        /// <summary>
        /// Metodo para insertar la categoria de la variable de calificacion control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarCategoriaVariableControl(clsDTOCategoriasVariableControl categoria, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALCategoriaVariableControl cDALcategoria = new clsDALCategoriaVariableControl();

            booResult = cDALcategoria.mtdInsertarCategoriaVariableControl(categoria, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Categorias de las Variables de Calificación Control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOCategoriasVariableControl> mtdConsultarCategoriaVariablesControl(ref List<clsDTOCategoriasVariableControl> lstCategoria, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALCategoriaVariableControl cDtRegistro = new clsDALCategoriaVariableControl();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarCategoriaVariablesControl(ref dtInfo, ref strErrMsg);

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
                            objCategoria.IdVariable = dr["IdVariable"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["IdVariable"].ToString().Trim());
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
        /// Metodo para consultar y visualizar las Categorias de las Variables de Calificación Control Activas
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOCategoriasVariableControl> mtdConsultarCategoriaActiva(ref List<clsDTOCategoriasVariableControl> lstCategoria, ref string strErrMsg, int IdVariable)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALCategoriaVariableControl cDtRegistro = new clsDALCategoriaVariableControl();
            #endregion Vars

            booResult = cDtRegistro.mtdConsultarCategoriaActivas(ref dtInfo, ref strErrMsg, IdVariable);

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
        /// Metodo para activar o inactivar el registro de categoria de la variable de calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActivarCategariaVariableControl(clsDTOCategoriasVariableControl categoria, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALCategoriaVariableControl cDALcategoria = new clsDALCategoriaVariableControl();

            booResult = cDALcategoria.mtdActivarVariableCalificacionControl(categoria, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el registro de categoria de la variable de calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateCategoriaVariableControl(clsDTOCategoriasVariableControl categoria, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALCategoriaVariableControl cDALcategoria = new clsDALCategoriaVariableControl();

            booResult = cDALcategoria.mtdUpdateCategoriaVariableControl(categoria, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para tomar el valor total de registros
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdCantidadTotalPeso(ref string strErrMsg)
        {
            int Valormaximo = 0;
            clsDALCategoriaVariableControl cDALcategoia = new clsDALCategoriaVariableControl();

            Valormaximo = cDALcategoia.mtdCantidadTotalPeso(ref strErrMsg);

            return Valormaximo;
        }
        /// <summary>
        /// Metodo para tomar el valor del peso de la categoria
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdPesoCategoria(ref string strErrMsg, int IdCategoria)
        {
            int Valormaximo = 0;
            clsDALCategoriaVariableControl cDALcategoia = new clsDALCategoriaVariableControl();

            Valormaximo = cDALcategoia.mtdPesoCategoria(ref strErrMsg, IdCategoria);

            return Valormaximo;
        }
    }
}