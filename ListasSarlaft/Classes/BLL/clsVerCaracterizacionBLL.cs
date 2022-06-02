using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using clsDatos;
using clsDTO;
using ListasSarlaft.Classes.DTO.Calidad;
using ListasSarlaft.Classes.DTO;

namespace ListasSarlaft.Classes
{
    public class clsVerCaracterizacionBLL
    {
        /// <summary>
        /// Metodo para consultar y visualizar la caracterizacion
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="objCaracOut">Objecto con la informacion de caracterizacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVerCaracterizacion> mtdConsultarVerCaracterizacion(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacion objCaracOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            clsVerCaracterizacion VerCaracterizacion = new clsVerCaracterizacion();
            List<clsVerCaracterizacion> ListVerCaracterizacion = new List<clsVerCaracterizacion>();
            booResult = cCarat.mtdConsultarVerCaracterizacion(objCaracIn, ref dtCaracOut, ref strErrMsg);

            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objCaracOut = new clsVerCaracterizacion(
                                Convert.ToInt32(dr["Id"].ToString().Trim()),
                                dr["FechaRegistro"].ToString().Trim(),
                                Convert.ToInt32(dr["IdUsuario"].ToString().Trim()),
                                Convert.ToInt32(dr["IdProceso"].ToString().Trim()),
                                dr["Nombre"].ToString().Trim(),
                                dr["objetivo"].ToString().Trim(),
                                dr["NombreResponsable"].ToString().Trim(),
                                dr["CargoResponsable"].ToString().Trim(),
                                dr["Recursos"].ToString().Trim(),
                                dr["Numerales"].ToString().Trim(),
                                dr["Responsables"].ToString().Trim(),
                                dr["Codigo"].ToString().Trim()
                                );
                            ListVerCaracterizacion.Add(objCaracOut);
                        }
                    }
                    else
                        ListVerCaracterizacion = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    ListVerCaracterizacion = null;
            }
            else
            {
                strErrMsg = "No hay información para mostrar";
            }

            return ListVerCaracterizacion;
        }

        public List<clsVerCaracterizacion> VerCamposCaracterizacion(clsVerCaracterizacion obj)
        {
            try
            {
                List<clsVerCaracterizacion> lst = new List<clsVerCaracterizacion>();
                clsDtVerCaracterizacion datos = new clsDtVerCaracterizacion();
                DataTable dt =  datos.VerCamposCaracterizacion(obj);
                if (dt!= null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        lst.Add(new clsVerCaracterizacion()
                        {
                            Recursos = Row["Recursos"].ToString(),
                            Numerales = Row["Numerales"].ToString(),
                            Responsables = Row["Responsables"].ToString(),
                            Codigo = Row["Codigo"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private String remplazarCaracteres(String cadena)
        {
            return cadena.Replace("'", "").Replace(",", "").Replace(".", "").Replace(";", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(":", "").Replace("" + (char)34, "").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("Ñ", "N").Replace("ñ", "n").Replace("" + (char)13, "").Replace("" + (char)10, "").Replace("´", "").Replace("|", String.Empty).ToUpper();
        }
        /// <summary>
        /// Metodo para consultar y visualizar el detalle de la caracterizacion
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="objCaracOut">Objecto con la informacion de caracterizacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVerCaracterizacionDetalle> mtdConsultarVerCaracterizacionDetalle(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacionDetalle objCaracDetalleOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            clsVerCaracterizacionDetalle VerCaracterizacionDetalle = new clsVerCaracterizacionDetalle();
            List<clsVerCaracterizacionDetalle> ListVerCaracterizacionDetalle = new List<clsVerCaracterizacionDetalle>();

            booResult = cCarat.mtdConsultarVerCaracterizacionDetalle(objCaracIn, ref dtCaracOut, ref strErrMsg);
            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objCaracDetalleOut = new clsVerCaracterizacionDetalle(
                                remplazarCaracteres(dr["DescripcionEntrada"].ToString().Trim()),
                                dr["Proveedor"].ToString().Trim(),
                                remplazarCaracteres(dr["DescripcionActividad"].ToString().Trim()),
                                remplazarCaracteres(dr["CargoResponsable"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionSalida"].ToString().Trim()),
                                remplazarCaracteres(dr["Cliente"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionProcedimiento"].ToString().Trim())
                                );
                            ListVerCaracterizacionDetalle.Add(objCaracDetalleOut);
                        }
                    }
                    else
                        ListVerCaracterizacionDetalle = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    ListVerCaracterizacionDetalle = null;
            }
            else
            {
                strErrMsg = "No hay información para mostrar";
            }

            return ListVerCaracterizacionDetalle;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Entradas de la caracterizacion
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="objCaracOut">Objecto con la informacion de caracterizacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVerCaracterizacionEntradas> mtdConsultarVerCaracterizacionEntradas(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacionEntradas objCaracDetalleOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            clsVerCaracterizacionEntradas verCaracterizacionEntradas = new clsVerCaracterizacionEntradas();
            List<clsVerCaracterizacionEntradas> ListVerCaracterizacionEntradas = new List<clsVerCaracterizacionEntradas>();

            booResult = cCarat.mtdConsultarVerCaracterizacionEntradas(objCaracIn, ref dtCaracOut, ref strErrMsg);
            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            /*objCaracDetalleOut = new clsVerCaracterizacionDetalle(
                                remplazarCaracteres(dr["DescripcionEntrada"].ToString().Trim()),
                                dr["Proveedor"].ToString().Trim(),
                                remplazarCaracteres(dr["DescripcionActividad"].ToString().Trim()),
                                remplazarCaracteres(dr["CargoResponsable"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionSalida"].ToString().Trim()),
                                remplazarCaracteres(dr["Cliente"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionProcedimiento"].ToString().Trim())
                                );*/
                            objCaracDetalleOut = new clsVerCaracterizacionEntradas(
                            remplazarCaracteres(dr["DescripcionEntrada"].ToString().Trim()),
                            dr["Proveedor"].ToString().Trim()
                            );
                            ListVerCaracterizacionEntradas.Add(objCaracDetalleOut);
                        }
                    }
                    else
                        ListVerCaracterizacionEntradas = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    ListVerCaracterizacionEntradas = null;
            }
            else
            {
                strErrMsg = "No hay información para mostrar";
            }

            return ListVerCaracterizacionEntradas;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Entradas de la caracterizacion
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="objCaracOut">Objecto con la informacion de caracterizacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVerCaracterizacionActividades> mtdConsultarVerCaracterizacionActividades(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacionActividades objCaracDetalleOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            clsVerCaracterizacionActividades verCaracterizacionActividades = new clsVerCaracterizacionActividades();
            List<clsVerCaracterizacionActividades> ListVerCaracterizacionActividades = new List<clsVerCaracterizacionActividades>();

            booResult = cCarat.mtdConsultarVerCaracterizacionActividades(objCaracIn, ref dtCaracOut, ref strErrMsg);
            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            /*objCaracDetalleOut = new clsVerCaracterizacionDetalle(
                                remplazarCaracteres(dr["DescripcionEntrada"].ToString().Trim()),
                                dr["Proveedor"].ToString().Trim(),
                                remplazarCaracteres(dr["DescripcionActividad"].ToString().Trim()),
                                remplazarCaracteres(dr["CargoResponsable"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionSalida"].ToString().Trim()),
                                remplazarCaracteres(dr["Cliente"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionProcedimiento"].ToString().Trim())
                                );*/
                            objCaracDetalleOut = new clsVerCaracterizacionActividades(
                            remplazarCaracteres(dr["DescripcionActividad"].ToString().Trim()),
                                remplazarCaracteres(dr["CargoResponsable"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionProcedimiento"].ToString().Trim()),
                                remplazarCaracteres(dr["PHVA"].ToString().Trim())
                            );
                            ListVerCaracterizacionActividades.Add(objCaracDetalleOut);
                        }
                    }
                    else
                        ListVerCaracterizacionActividades = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    ListVerCaracterizacionActividades = null;
            }
            else
            {
                strErrMsg = "No hay información para mostrar";
            }

            return ListVerCaracterizacionActividades;
        }
        /// <summary>
        /// Metodo para consultar y visualizar las Salidas de la caracterizacion
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="objCaracOut">Objecto con la informacion de caracterizacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVerCaracterizacionSalidas> mtdConsultarVerCaracterizacionSalidas(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacionSalidas objCaracDetalleOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            clsVerCaracterizacionSalidas verCaracterizacionSalidas = new clsVerCaracterizacionSalidas();
            List<clsVerCaracterizacionSalidas> ListVerCaracterizacionSalidas = new List<clsVerCaracterizacionSalidas>();

            booResult = cCarat.mtdConsultarVerCaracterizacionSalidas(objCaracIn, ref dtCaracOut, ref strErrMsg);
            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            /*objCaracDetalleOut = new clsVerCaracterizacionDetalle(
                                remplazarCaracteres(dr["DescripcionEntrada"].ToString().Trim()),
                                dr["Proveedor"].ToString().Trim(),
                                remplazarCaracteres(dr["DescripcionActividad"].ToString().Trim()),
                                remplazarCaracteres(dr["CargoResponsable"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionSalida"].ToString().Trim()),
                                remplazarCaracteres(dr["Cliente"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionProcedimiento"].ToString().Trim())
                                );*/
                            objCaracDetalleOut = new clsVerCaracterizacionSalidas(
                            remplazarCaracteres(dr["DescripcionSalida"].ToString().Trim()),
                                remplazarCaracteres(dr["Cliente"].ToString().Trim())
                            );
                            ListVerCaracterizacionSalidas.Add(objCaracDetalleOut);
                        }
                    }
                    else
                        ListVerCaracterizacionSalidas = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    ListVerCaracterizacionSalidas = null;
            }
            else
            {
                strErrMsg = "No hay información para mostrar";
            }

            return ListVerCaracterizacionSalidas;
        }
        /// <summary>
        /// Metodo para consultar y visualizar el detalle de la caracterizacion
        /// </summary>
        /// <param name="objCaracIn">Objecto con la informacion de caracterizacion de entrada</param>
        /// <param name="objCaracOut">Objecto con la informacion de caracterizacion de salida</param>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVerCaracterizacionIndicadorRiesgo> mtdConsultarVerCaracterizacionIndicadorle(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacionIndicadorRiesgo objCaracIndicadorOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            clsVerCaracterizacionIndicadorRiesgo VerCaracterizacionIndicador = new clsVerCaracterizacionIndicadorRiesgo();
            List<clsVerCaracterizacionIndicadorRiesgo> ListVerCaracterizacionIndicador = new List<clsVerCaracterizacionIndicadorRiesgo>();

            booResult = cCarat.mtdConsultarVerCaracterizacionIndicador(objCaracIn, ref dtCaracOut, ref strErrMsg);
            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objCaracIndicadorOut = new clsVerCaracterizacionIndicadorRiesgo(
                                remplazarCaracteres(dr["NombreIndicador"].ToString().Trim()),
                                remplazarCaracteres(dr["Descripcion"].ToString().Trim()),
                                Convert.ToInt32(dr["IdIndicador"].ToString().Trim())
                                /*,
                                remplazarCaracteres(dr["CodigoControl"].ToString().Trim()),
                                remplazarCaracteres(dr["NombreControl"].ToString().Trim())*/
                                );
                            ListVerCaracterizacionIndicador.Add(objCaracIndicadorOut);
                        }
                    }
                    else
                        ListVerCaracterizacionIndicador = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    ListVerCaracterizacionIndicador = null;
            }
            else
            {
                strErrMsg = "No hay información para mostrar";
            }

            return ListVerCaracterizacionIndicador;
        }

        public List<clsVerCaracterizacionRiesgos> mtdConsultarVerCaracterizacionRiesgosle(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacionRiesgos objCaracIndicadorOut, ref string strErrMsg)
        {
            bool booResult = false;
            DataTable dtCaracOut = new DataTable();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            clsVerCaracterizacionRiesgos VerCaracterizacionIndicador = new clsVerCaracterizacionRiesgos();
            List<clsVerCaracterizacionRiesgos> ListVerCaracterizacionRiesgos = new List<clsVerCaracterizacionRiesgos>();


            booResult = cCarat.mtdConsultarVerCaracterizacionRiesgos(objCaracIn, ref dtCaracOut, ref strErrMsg);
            if (booResult)
            {
                if (dtCaracOut != null)
                {
                    if (dtCaracOut.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCaracOut.Rows)
                        {
                            objCaracIndicadorOut = new clsVerCaracterizacionRiesgos(
                                dr["Codigo"].ToString().Trim(),
                                remplazarCaracteres(dr["NombreRiesgo"].ToString().Trim()),
                                remplazarCaracteres(dr["DescripcionRiesgo"].ToString().Trim()),
                                Convert.ToInt32(dr["IdRiesgo"].ToString().Trim())
                                /*,
                                remplazarCaracteres(dr["CodigoControl"].ToString().Trim()),
                                remplazarCaracteres(dr["NombreControl"].ToString().Trim())*/
                                );
                            ListVerCaracterizacionRiesgos.Add(objCaracIndicadorOut);
                        }
                    }
                    else
                                        ListVerCaracterizacionRiesgos = null;
                    //strErrMsg = "No hay información de Procesos.";
                }
                else
                    ListVerCaracterizacionRiesgos = null;
            }
            else
            {
                strErrMsg = "No hay información para mostrar";
            }

            return ListVerCaracterizacionRiesgos;
        }



        public System.Data.DataSet mtdConsultarVerCaracterizacionRPT(clsVerCaracterizacion objCaracIn, ref clsVerCaracterizacion objCaracOut, ref string strErrMsg)
        {
            System.Data.DataSet dsInfo = new System.Data.DataSet();
            clsDtVerCaracterizacion cCarat = new clsDtVerCaracterizacion();
            DataTable dtCaracOut = new DataTable();
            dsInfo = cCarat.mtdConsultarVerCaracterizacionRPT(objCaracIn, ref dtCaracOut, ref strErrMsg);

            return dsInfo;
        }

        public List<DocumentosCaracterizacion> ConsultarDocumentos(int idMacroProceso)
        {
            try
            {
                using (clsDtVerCaracterizacion objData = new clsDtVerCaracterizacion())
                {
                    return objData.ConsultarDocumentos(idMacroProceso);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}