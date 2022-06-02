using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsControlPropiedadCPbll
    {
        /// <summary>
        /// Metodo que permite la insercion de los controles de propiedad cliente proveedor
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la programacion de capacitaciones</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarControlPropiedad(clsControlPropiedadCP objCrlPropiedad, ref string strErrMsg)
        {
            bool booResult = false;
            clsControlPropiedadCPdal cDtCrlPropiedad = new clsControlPropiedadCPdal();

            booResult = cDtCrlPropiedad.mtdInsertarControlPropiedad(objCrlPropiedad, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para consultar y visualizar los Programas de las Capacitaciones
        /// </summary>
        /// <param name="objCaracOut">Objecto con la informacion de evaluacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsControlPropiedadCP> mtdConsultarControlPropiedad(ref List<clsControlPropiedadCP> lstCrlPropiedad, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsControlPropiedadCPdal cDtCrlPropiedad = new clsControlPropiedadCPdal();
            #endregion Vars

            booResult = cDtCrlPropiedad.mtdConsultarControlPropiedad(ref dtInfo, ref strErrMsg);

            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsControlPropiedadCP objControl = new clsControlPropiedadCP();
                            objControl.intIdCrlPropiedad = Convert.ToInt32(dr["IdControlPropiedadClienteProveeedor"].ToString().Trim());
                            objControl.strDescripcion = dr["Descripcion"].ToString().Trim();
                            objControl.strCaracteristicas = dr["Caracteristicas"].ToString().Trim();
                            objControl.strProveedorCliente = dr["ProveedorCliente"].ToString().Trim();
                            objControl.dtFechaIngreso = dr["FechaIngreso"].ToString().Trim();
                            objControl.dtFechaSalida = dr["FechaSalida"].ToString().Trim();
                            objControl.strObservaciones = dr["Observaciones"].ToString().Trim();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString().Trim());
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString().Trim());
                            objControl.strUsuario = dr["Usuario"].ToString().Trim();
                            objControl.strNombre = dr["Nombre"].ToString().Trim();

                            lstCrlPropiedad.Add(objControl);
                        }
                    }
                    else
                        lstCrlPropiedad = null;
                }
                else
                    lstCrlPropiedad = null;
            }

            return lstCrlPropiedad;
        }
        /// <summary>
        /// Metodo que permite la insercion del detalle de la evaluacion de competencia
        /// </summary>
        /// <param name="objCompetencia">objeto con la informacion de la evaluacion de competencia</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateControlPropiedad(ref clsControlPropiedadCP objPrograma, ref string strErrMsg)
        {
            bool booResult = false;
            clsControlPropiedadCPdal cDtPrograma = new clsControlPropiedadCPdal();

            booResult = cDtPrograma.mtdUpdateControlPropiedad(ref objPrograma, ref strErrMsg);

            return booResult;
        }
    }
}