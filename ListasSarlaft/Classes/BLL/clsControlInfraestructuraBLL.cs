using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsControlInfraestructuraBLL
    {
        /// <summary>
        /// Metodo para insertar el Control de Infraestructura
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarControlInfraestructura(clsControlInfraestructura cCrlInfra, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtControlInfraestructuraDAL cDtCrlInfra = new clsDtControlInfraestructuraDAL();

            booResult = cDtCrlInfra.mtdInsertarControlInfraestructura(cCrlInfra, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar el Control de Infraestructura
        /// </summary>
        /// <param name="lstCalificacion">Lista con la informacion del control</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsControlInfraestructura> mtdConsultarControlInfraestructura(ref List<clsControlInfraestructura> lstCrlInfra, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtControlInfraestructuraDAL cDtControl = new clsDtControlInfraestructuraDAL();
            #endregion Vars

            booResult = cDtControl.mtdConsultarControlInfraestructura(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsControlInfraestructura objControl = new clsControlInfraestructura();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim());
                             objControl.strNombreProceso = dr["Nombre"].ToString().Trim();
                              objControl.strCargoResponsable = dr["NombreHijo"].ToString().Trim();
                            objControl.intResponsable = Convert.ToInt32(dr["CargoResponsable"].ToString().Trim());
                             objControl.strActividad = dr["Actividad"].ToString().Trim();
                              objControl.dtFechaProgramada = dr["FechaProgramada"].ToString().Trim();
                            
                                objControl.dtFechaCumplimiento = dr["FechaCumplimiento"].ToString().Trim();
                            
                              
                              objControl.strObservaciones = dr["Observaciones"].ToString().Trim();
                               objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                               objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                               objControl.struserName = dr["Usuario"].ToString().Trim();
                            objControl.intAllProcess = Convert.ToInt32(dr["allProcess"].ToString().Trim());
                            lstCrlInfra.Add(objControl);
                        }
                    }
                    else
                        lstCrlInfra = null;
                }
                else
                    lstCrlInfra = null;
            }

            return lstCrlInfra;
        }
        /// <summary>
        /// Metodo para consultar el Control de Infraestructura
        /// </summary>
        /// <param name="lstCalificacion">Lista con la informacion del control</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsControlInfraestructura> mtdConsultarControlInfraestructuraReporte(ref List<clsControlInfraestructura> lstCrlInfra, ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtControlInfraestructuraDAL cDtControl = new clsDtControlInfraestructuraDAL();
            #endregion Vars

            booResult = cDtControl.mtdConsultarControlInfraestructuraReporte(ref dtInfo, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsControlInfraestructura objControl = new clsControlInfraestructura();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString().Trim());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroproceso"].ToString().Trim());
                            if(dr["allProcess"].ToString().Trim() == "1")
                                objControl.strNombreProceso = "Todos los procesos";
                            else
                                objControl.strNombreProceso = dr["Nombre"].ToString().Trim();

                            objControl.strCargoResponsable = dr["NombreHijo"].ToString().Trim();
                            objControl.intResponsable = Convert.ToInt32(dr["CargoResponsable"].ToString().Trim());
                            objControl.strActividad = dr["Actividad"].ToString().Trim();
                            objControl.dtFechaProgramada = dr["FechaProgramada"].ToString().Trim();

                            objControl.dtFechaCumplimiento = dr["FechaCumplimiento"].ToString().Trim();


                            objControl.strObservaciones = dr["Observaciones"].ToString().Trim();
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.struserName = dr["Usuario"].ToString().Trim();

                            lstCrlInfra.Add(objControl);
                        }
                    }
                    else
                        lstCrlInfra = null;
                }
                else
                    lstCrlInfra = null;
            }

            return lstCrlInfra;
        }
        /// <summary>
        /// Metodo para actualizar el Control de Infraestructura
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateControlInfraestructura(clsControlInfraestructura cCrlInfra, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtControlInfraestructuraDAL cDtCrlInfra = new clsDtControlInfraestructuraDAL();

            booResult = cDtCrlInfra.mtdUpdateControlInfraestructura(cCrlInfra, ref strErrMsg);

            return booResult;
        }
    }
}