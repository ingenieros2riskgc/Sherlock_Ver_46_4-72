using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLControlxVariable
    {
        /// <summary>
        /// Metodo para insertar el registro del control con la variable correspondiente
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarControlxVariable(clsDTOControlxVariable controlxvariable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALControlxVariable cDALcontrolxvariable = new clsDALControlxVariable();

            booResult = cDALcontrolxvariable.mtdInsertarControlxVariable(controlxvariable, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para tomar ultimo Id del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdControl(ref string strErrMsg)
        {
            int LastId = 0;
            clsDALControlxVariable cDALcontrolxvariable = new clsDALControlxVariable();

            LastId = cDALcontrolxvariable.mtdLastIdControl(ref strErrMsg);

            return LastId;
        }
        /// <summary>
        /// Metodo para tomar el Id del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdGetIdControl(ref string strErrMsg, string CodControl)
        {
            int IdControl = 0;
            clsDALControlxVariable cDALcontrolxvariable = new clsDALControlxVariable();

            IdControl = cDALcontrolxvariable.mtdGetIdControl(ref strErrMsg, CodControl);

            return IdControl;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las variables registradas a un control
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de usuarios de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOControlxVariable> mtdConsultarVariablexContol(ref List<clsDTOControlxVariable> lstControlxVariable, ref string strErrMsg, int CodControl)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDALControlxVariable cDtRegistro = new clsDALControlxVariable();
            #endregion Vars


            cControl control = new cControl();
            

            //Se consulta en la tabla control el ID guardado para cada una de las variables
            DataTable dt = control.SeleccionarCategoriasControl(CodControl);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    clsDTOControlxVariable objControlxVariable = new clsDTOControlxVariable();
                    objControlxVariable.intIdCategoria = Convert.ToInt32(Row["IdCategoriaVariableControl"]);
                    objControlxVariable.strNombreCategoria = Row["DescripcionCategoria"].ToString();
                    objControlxVariable.strNombreVariable = Row["DescripcionVariable"].ToString();
                    objControlxVariable.intIdControl = Convert.ToInt32(Row["IdControl"]);
                    lstControlxVariable.Add(objControlxVariable);
                }
            }

            //booResult = cDtRegistro.mtdConsultarVariablexContol(ref dtInfo, ref strErrMsg, CodControl);

            return lstControlxVariable;
        }
        /// <summary>
        /// Metodo para actualizar el registro del control con la variable correspondiente
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateCategoria(clsDTOControlxVariable controlxvariable, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALControlxVariable cDALcontrolxvariable = new clsDALControlxVariable();

            booResult = cDALcontrolxvariable.mtdUpdateCategoria(controlxvariable, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar la calificacion del control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateCalificacionControl(int IdCalificacion, int IdControl, ref string strErrMsg)
        {
            bool booResult = false;
            clsDALControlxVariable cDALcontrolxvariable = new clsDALControlxVariable();

            booResult = cDALcontrolxvariable.mtdUpdateControlCalificacion(IdCalificacion, IdControl, ref strErrMsg);

            return booResult;
        }
    }
}