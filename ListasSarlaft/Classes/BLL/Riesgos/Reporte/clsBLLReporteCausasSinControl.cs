using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class clsBLLReporteCausasSinControl
    {
        /// <summary>
        /// Metodo que permite obtener las causas sin control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoCantCusasSinControl(ref string strErrMsg, int IdRiesgo, int IdCausa, int IdControl)
        {
            bool booResult = false;
            string IdCausasvsControles = string.Empty;

            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoGetCausas(ref strErrMsg, IdRiesgo, IdCausa, IdControl);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        IdCausasvsControles = dr["IdCausasvsControles"].ToString();

                    }
                }
            }

            return IdCausasvsControles;
        }
        /// <summary>
        /// Metodo que permite consultar Info Reporte Riesgos 
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public DataTable mtdConsultarCausasSinControl(ref string strErrMsg, String IdCadenaValor, String IdMacroProceso,
            String IdProceso, String IdClasificacionRiesgo, String IdClasificacionGeneralRiesgo,
            String NombreRiesgoInherente, String NombreRiesgoResidual, String IdEmpresa,
            String IdRiesgo, String IdArea)
        {
            bool booResult = false;
            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            cRiesgo cRiesgo = new cRiesgo();
            DataTable dtInfo = new DataTable();
            DataTable dtInfoCausas = new DataTable();
            double promedio = 0;
            string ListaCausas = string.Empty;
            string IdCausasvsControles = string.Empty;
            dtInfo = cRiesgo.ReporteRiesgosCausasSinControl( IdCadenaValor,  IdMacroProceso,
             IdProceso,  IdClasificacionRiesgo,  IdClasificacionGeneralRiesgo,
             NombreRiesgoInherente,  NombreRiesgoResidual,  IdEmpresa,
             IdRiesgo,  IdArea);
            if (dtInfo != null)
            {
                
                if (dtInfo.Rows.Count > 0)
                {
                    int iteracion = 0;
                    
                    dtInfoCausas.Columns.Add("NombreCausas");
                    dtInfoCausas.Columns.Add("CodigoRiesgo");
                    dtInfoCausas.Columns.Add("NombreRiesgo");
                    dtInfoCausas.Columns.Add("Descripcion");
                    dtInfoCausas.Columns.Add("RiesgoInherente");
                    dtInfoCausas.Columns.Add("RiesgoResidual");
                    dtInfoCausas.Columns.Add("CodigoEvento");
                    dtInfoCausas.Columns.Add("DescripcionEvento");
                    dtInfoCausas.Columns.Add("NombreArea");
                    
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        dtInfoCausas.Rows.Add();
                        int CantCausas = 0;
                        dtInfoCausas.Rows[iteracion]["CodigoRiesgo"] = dr["CodigoRiesgo"].ToString();
                        dtInfoCausas.Rows[iteracion]["NombreRiesgo"] = dr["NombreRiesgo"].ToString();
                        dtInfoCausas.Rows[iteracion]["Descripcion"] = dr["Descripcion"].ToString();
                        dtInfoCausas.Rows[iteracion]["RiesgoInherente"] = dr["RiesgoInherente"].ToString();
                        dtInfoCausas.Rows[iteracion]["RiesgoResidual"] = dr["RiesgoResidual"].ToString();
                        dtInfoCausas.Rows[iteracion]["CodigoEvento"] = dr["CodigoEvento"].ToString();
                        dtInfoCausas.Rows[iteracion]["DescripcionEvento"] = dr["DescripcionEvento"].ToString();
                        dtInfoCausas.Rows[iteracion]["NombreArea"] = dr["NombreArea"].ToString();
                        DataTable dtInfoControl = new DataTable();

                        ListaCausas = dr["ListaCausas"].ToString().Trim();
                        string[] causas = ListaCausas.Split('|');
                        string NombreCausa = string.Empty;
                        foreach (var IdCausa in causas)
                        {
                            if (IdCausa != "")
                            {
                                /*if (dtInfoControl != null)
                                {
                                    if (dtInfoControl.Rows.Count > 0)
                                    {*/
                                        /*foreach (DataRow drControl in dtInfoControl.Rows)
                                        {*/
                                            IdCausasvsControles = LoadInfoCantCusasSinControl(ref strErrMsg, Convert.ToInt32(dr["IdRiesgo"].ToString()), Convert.ToInt32(IdCausa), Convert.ToInt32(dr["IdControl"].ToString()));
                                            if (IdCausasvsControles == null || IdCausasvsControles == "")
                                            {
                                                NombreCausa = LoadInfoNombreCausas(ref strErrMsg, Convert.ToInt32(IdCausa));
                                            }
                                        //}
                                    /*}
                                    else
                                    {
                                        NombreCausa = LoadInfoNombreCausas(ref strErrMsg, Convert.ToInt32(IdCausa));
                                    }*/
                                //}
                                /*else
                                {
                                    CantCausas++;
                                }*/
                            }
                            dtInfoCausas.Rows[iteracion]["NombreCausas"] = NombreCausa;
                        }
                        iteracion++;
                    }
                    booResult = true;
                }
                else
                    dtInfoCausas = null;
            }
            else
                dtInfoCausas = null;

            return dtInfoCausas;
        }
        /// <summary>
        /// Metodo que permite obtener las causas sin control
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string LoadInfoNombreCausas(ref string strErrMsg, int IdCausa)
        {
            bool booResult = false;
            string IdCausasvsControles = string.Empty;

            clsDALCuadroMandoRiesgosDetalle cDALreporte = new clsDALCuadroMandoRiesgosDetalle();
            DataTable dtInfo = new DataTable();
            dtInfo = cDALreporte.LoadInfoGetNombreCausas(ref strErrMsg, IdCausa);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        IdCausasvsControles = dr["NombreCausas"].ToString();

                    }
                }
            }

            return IdCausasvsControles;
        }
    }
}