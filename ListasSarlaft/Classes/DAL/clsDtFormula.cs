using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsDtFormula
    {
        /// <summary>
        /// Realiza la insercion de la Formula.
        /// </summary>
        /// <param name="lstFormula">Informacion a insertar</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        public bool mtdInsertarFormula(List<object> lstFormula, int intIdIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                cDatabase.conectar();
                foreach (object objFormula in lstFormula)
                {
                    clsFormula objFormulaTmp = new clsFormula();
                    objFormulaTmp = (clsFormula)objFormula;

                    strConsulta = string.Format("INSERT INTO [Procesos].[tblFormula]([IdOperando],[Valor],[IdIndicador],[Posicion],[FechaRegistro],[IdUsuario]) " +
                        "VALUES ({0},'{1}',{2},{3},GETDATE(),{4}) ",
                        objFormulaTmp.intOperando, objFormulaTmp.strValor, intIdIndicador, objFormulaTmp.intPosicion, objFormulaTmp.intIdUsuario);

                    cDatabase.ejecutarQuery(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Fórmula. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }

        public bool mtdConsultarFormula(clsIndicador objIndicadorIn, ref DataTable dtInfo, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [Id],[IdOperando],[Valor],[IdIndicador],[Posicion],[FechaRegistro],[IdUsuario] " +
                    "FROM [Procesos].[tblFormula] " +
                    "WHERE [IdIndicador] = {0} ",
                    objIndicadorIn.intId);

                cDatabase.conectar();
                dtInfo = cDatabase.ejecutarConsulta(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la Fórmula. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }



        public bool mtdActualizarFormula(List<object> lstFormula, int intIdIndicador, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {
                cDatabase.conectar();

                strConsulta = string.Format("DELETE [Procesos].[tblFormula] WHERE [IdIndicador] = {0}", intIdIndicador);
                cDatabase.ejecutarQuery(strConsulta);

                foreach (object objFormula in lstFormula)
                {
                    clsFormula objFormulaTmp = new clsFormula();
                    objFormulaTmp = (clsFormula)objFormula;

                    strConsulta = string.Format("INSERT INTO [Procesos].[tblFormula]([IdOperando],[Valor],[IdIndicador],[Posicion],[FechaRegistro],[IdUsuario]) " +
                         "VALUES ({0},'{1}',{2},{3},GETDATE(),{4}) ",
                        objFormulaTmp.intOperando, objFormulaTmp.strValor, intIdIndicador, objFormulaTmp.intPosicion, objFormulaTmp.intIdUsuario);

                    cDatabase.ejecutarQuery(strConsulta);
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al actualizar la Fórmula. [{0}]", ex.Message);
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