using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class AsocCaracterizacion : System.Web.UI.UserControl
    {
        string IdFormulario = "4013";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdCargarDDLs();

                }
            }
        }

        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (ddlProceso.SelectedValue == "0")
                rfvProceso.Enabled = false;
            else
                rfvProceso.Enabled = true;

            ddlSubproceso.Items.Clear();

            if (mtdLoadDDLSubproceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlSubproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProceso.SelectedValue == "0")
                rfvSubproceso.Enabled = false;
            else
                rfvSubproceso.Enabled = true;
        }
        #endregion

        #region Buttons
        /// <summary>
        /// Permite buscar la informacion de caracterizacion de un proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            bool booResult = false;
            string strErrMsg = string.Empty;
            clsCaracterizacion objCaracIn = new clsCaracterizacion();

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
            else
            {
                booResult = mtdValidarProceso(0, ref objCaracIn, ref strErrMsg);

                if (booResult)
                {
                    if (!mtdCargarCheckBox(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    else
                    {
                        if (mtdCargarParametrizacion(objCaracIn, ref strErrMsg))
                            mtdHabilitarCampos(true);
                        else
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                    }
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");

            }
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            #region Vars
            string strErrMsg = string.Empty;
            #endregion

            if (mtdInsertarCaracterizacion(ref strErrMsg))
            {
                omb.ShowMessage("Asociación caracterización registrado exitosamente", 3, "Atención");
                mtdHabilitarCampos(false);
                mtdLimpiarCampos();
            }
            else
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            #region Vars
            string strErrMsg = string.Empty;
            #endregion

            if (mtdActualizarCaracterizacion(ref strErrMsg))
            {
                omb.ShowMessage("Asociación de caracterización modificado exitosamente", 3, "Atención");
                mtdHabilitarCampos(false);
                mtdLimpiarCampos();
            }
            else
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdHabilitarCampos(false);
            mtdLimpiarCampos();
        }
        #endregion

        #region Metodos
        #region Cargas
        #region DDLs
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;

            mtdLoadDDLCadenaValor(ref strErrMsg);
            mtdLoadDDLMacroProceso(ref strErrMsg);
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLCadenaValor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();
            #endregion Vars

            try
            {
                lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(true, ref strErrMsg);
                ddlCadenaValor.Items.Clear();
                ddlCadenaValor.Items.Insert(0, new ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLMacroProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCadenaValor objCadenaValor = new clsCadenaValor();
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                objCadenaValor = new clsCadenaValor(Convert.ToInt32(ddlCadenaValor.SelectedValue.ToString().Trim()), string.Empty, true, 0, string.Empty, string.Empty);
                lstMacroproceso = cMacroproceso.mtdConsultarMacroproceso(true, objCadenaValor, ref strErrMsg);

                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstMacroproceso != null)
                    {
                        int intCounter = 1;


                        foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
                        {
                            ddlMacroproceso.Items.Insert(intCounter, new ListItem(objMacroproceso.strNombreMacroproceso, objMacroproceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {
                objMProceso = new clsMacroproceso(Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()), string.Empty, string.Empty, string.Empty,
                    true, 0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty);
                lstProceso = cProceso.mtdConsultarProceso(true, objMProceso, ref strErrMsg);

                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstProceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsProceso objProceso in lstProceso)
                        {
                            ddlProceso.Items.Insert(intCounter, new ListItem(objProceso.strNombreProceso, objProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los subprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLSubproceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                objProceso = new clsProceso(Convert.ToInt32(ddlProceso.SelectedValue.ToString().Trim()),
                    0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty);
                lstSubproceso = cSubproceso.mtdConsultarSubProceso(true, objProceso, ref strErrMsg);

                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstSubproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsSubproceso objSubProceso in lstSubproceso)
                        {
                            ddlSubproceso.Items.Insert(intCounter, new ListItem(objSubProceso.strNombreSubproceso, objSubProceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        #endregion

        #region CheckBox
        private bool mtdCargarCheckBox(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadEntradas(ref strErrMsg);
            if (booResult)
            {
                booResult = mtdLoadSalidas(ref strErrMsg);
                if (booResult)
                    booResult = mtdLoadActividad(ref strErrMsg);
            }

            return booResult;
        }

        private bool mtdLoadEntradas(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            int intContador = 0;
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            clsEntradaBLL cEntrada = new clsEntradaBLL();
            #endregion

            lstEntrada = cEntrada.mtdConsultarEntrada(true, ref strErrMsg);
            if (string.IsNullOrEmpty(strErrMsg))
            {
                CheckBoxList1.Items.Clear();
                foreach (clsEntrada objEntrada in lstEntrada)
                {
                    CheckBoxList1.Items.Insert(intContador, new ListItem(objEntrada.strDescripcion, objEntrada.intId.ToString()));
                    intContador++;
                }
                booResult = true;
            }
            return booResult;
        }

        private bool mtdLoadSalidas(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            int intContador = 0;
            List<clsSalida> lstSalida = new List<clsSalida>();
            clsSalidaBLL cSalida = new clsSalidaBLL();
            #endregion

            lstSalida = cSalida.mtdConsultarSalida(true, ref strErrMsg);
            if (string.IsNullOrEmpty(strErrMsg))
            {
                CheckBoxList3.Items.Clear();
                foreach (clsSalida objSalida in lstSalida)
                {
                    CheckBoxList3.Items.Insert(intContador, new ListItem(objSalida.strDescripcion, objSalida.intId.ToString()));
                    intContador++;
                }
                booResult = true;
            }
            return booResult;
        }

        private bool mtdLoadActividad(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            int intContador = 0;
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsActividadBLL cActividad = new clsActividadBLL();
            #endregion

            lstActividad = cActividad.mtdConsultarActividad(true, ref strErrMsg);
            if (string.IsNullOrEmpty(strErrMsg))
            {
                CheckBoxList2.Items.Clear();
                foreach (clsActividad objActividad in lstActividad)
                {
                    CheckBoxList2.Items.Insert(intContador, new ListItem(objActividad.strDescripcion, objActividad.intId.ToString()));
                    intContador++;
                }
                booResult = true;
            }
            return booResult;
        }
        #endregion
        #endregion

        #region Habilitar
        private void mtdHabilitarCampos(bool booEstado)
        {
            botonBuscar.Visible = !booEstado;
            filaDetalle.Visible = booEstado;
            filaBotones.Visible = booEstado;
        }

        private void mtdHabilitarBotones(int intOpcion)
        {
            switch (intOpcion)
            {
                case 1:
                    btnImgInsertar.Visible = true;
                    btnImgActualizar.Visible = false;
                    btnImgCancelar.Visible = true;
                    break;

                case 2:
                    btnImgInsertar.Visible = false;
                    btnImgActualizar.Visible = true;
                    btnImgCancelar.Visible = true;
                    break;
            }
        }
        #endregion

        private void mtdLimpiarCampos()
        {
            string strErrMsg = string.Empty;

            TabDetalles.TabIndex = 0;
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            mtdCargarDDLs();
            if (!mtdCargarCheckBox(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        private bool mtdCargarParametrizacion(clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCaracterizacion objCaracOut = new clsCaracterizacion();
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            List<clsSalida> lstSalida = new List<clsSalida>();
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsCaracterizacionBLL cCarac = new clsCaracterizacionBLL();
            clsEntradaBLL cEntrada = new clsEntradaBLL();
            clsSalidaBLL cSalida = new clsSalidaBLL();
            clsActividadBLL cActividad = new clsActividadBLL();
            #endregion

            booResult = cCarac.mtdConsultarCaracterizacion(objCaracIn, ref objCaracOut, ref strErrMsg);
            if (booResult)
            {
                if (objCaracOut != null)
                {
                    mtdHabilitarBotones(2);

                    #region Cargar Entrada, Actividad y Salida
                    tbxId.Text = objCaracOut.intId.ToString().Trim();

                    lstEntrada = cEntrada.mtdConsultarEntrada(objCaracIn, ref strErrMsg);
                    if (string.IsNullOrEmpty(strErrMsg))
                    {
                        if (lstEntrada != null)
                        {
                            #region Recorrido Entradas
                            foreach (clsEntrada objEntrada in lstEntrada)
                            {
                                for (int j = 0; j < CheckBoxList1.Items.Count; j++)
                                {
                                    if (objEntrada.intId.ToString().Trim() == CheckBoxList1.Items[j].Value.ToString().Trim())
                                    {
                                        CheckBoxList1.Items[j].Selected = true;
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }

                        lstSalida = cSalida.mtdConsultarSalida(objCaracIn, ref strErrMsg);
                        if (string.IsNullOrEmpty(strErrMsg))
                        {
                            if (lstSalida != null)
                            {
                                #region Recorrido Salidas
                                foreach (clsSalida objSalida in lstSalida)
                                {
                                    for (int j = 0; j < CheckBoxList3.Items.Count; j++)
                                    {
                                        if (objSalida.intId.ToString().Trim() == CheckBoxList3.Items[j].Value.ToString().Trim())
                                        {
                                            CheckBoxList3.Items[j].Selected = true;
                                            break;
                                        }
                                    }
                                }
                                #endregion
                            }

                            lstActividad = cActividad.mtdConsultarActividad(objCaracIn, ref strErrMsg);
                            if (string.IsNullOrEmpty(strErrMsg))
                            {
                                if (lstActividad != null)
                                {
                                    #region Recorrido Actividades
                                    foreach (clsActividad objActividad in lstActividad)
                                    {
                                        for (int j = 0; j < CheckBoxList2.Items.Count; j++)
                                        {
                                            if (objActividad.intId.ToString().Trim() == CheckBoxList2.Items[j].Value.ToString().Trim())
                                            {
                                                CheckBoxList2.Items[j].Selected = true;
                                                break;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }

                        }
                        txtRecursos.Text = objCaracOut.Recursos;
                        txtNumerales.Text = objCaracOut.Numerales;
                        txtResponsables.Text = objCaracOut.Responsables;
                        txtCodigo.Text = objCaracOut.Codigo;
                    }
                    #endregion
                }
                else
                    mtdHabilitarBotones(1);
            }
            return booResult;
        }

        private bool mtdValidarProceso(int intId, ref clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            bool booResult = false;

            if ((ddlSubproceso.SelectedValue != "0") && (!string.IsNullOrEmpty(ddlSubproceso.SelectedValue)))
            {
                objCaracIn = new clsCaracterizacion(intId, 3, Convert.ToInt32(ddlSubproceso.SelectedValue),
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                booResult = true;
            }
            else
            {
                if ((ddlProceso.SelectedValue != "0") && (!string.IsNullOrEmpty(ddlProceso.SelectedValue)))
                {
                    objCaracIn = new clsCaracterizacion(intId, 2, Convert.ToInt32(ddlProceso.SelectedValue),
                        Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                    booResult = true;
                }
                else
                    if (ddlMacroproceso.SelectedValue != "0")
                    {
                        objCaracIn = new clsCaracterizacion(intId, 1, Convert.ToInt32(ddlMacroproceso.SelectedValue),
                            Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);
                        booResult = true;
                    }
            }

            if (!booResult)
                strErrMsg = "Error al asociar el proceso a la caraterización";

            return booResult;
        }

        private bool mtdSeleccionEntrada(ref List<clsCaracterizacionXEntrada> lstEntrada, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            bool booResult = false;

            try
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected)
                    {
                        clsCaracterizacionXEntrada objEntrada = new clsCaracterizacionXEntrada(0,
                            objCaracIn.intId, Convert.ToInt32(CheckBoxList1.Items[i].Value.ToString().Trim()), objCaracIn.intIdUsuario, string.Empty);

                        lstEntrada.Add(objEntrada);
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error en la selección de las Entradas. [{0}]", ex.Message);
            }
            return booResult;
        }

        private bool mtdSeleccionActividad(ref List<clsCaracterizacionXActividad> lstActividad, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            bool booResult = false;

            try
            {
                for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                {
                    if (CheckBoxList2.Items[i].Selected)
                    {
                        clsCaracterizacionXActividad objActividad = new clsCaracterizacionXActividad(0,
                            objCaracIn.intId, Convert.ToInt32(CheckBoxList2.Items[i].Value.ToString().Trim()), objCaracIn.intIdUsuario, string.Empty);

                        lstActividad.Add(objActividad);
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error en la selección de las Actividades. [{0}]", ex.Message);
            }
            return booResult;
        }

        private bool mtdSeleccionSalida(ref List<clsCaracterizacionXSalida> lstSalida, clsCaracterizacion objCaracIn, ref string strErrMsg)
        {
            bool booResult = false;

            try
            {
                for (int i = 0; i < CheckBoxList3.Items.Count; i++)
                {
                    if (CheckBoxList3.Items[i].Selected)
                    {
                        clsCaracterizacionXSalida objSalida = new clsCaracterizacionXSalida(0,
                            objCaracIn.intId, Convert.ToInt32(CheckBoxList3.Items[i].Value.ToString().Trim()), objCaracIn.intIdUsuario, string.Empty);

                        lstSalida.Add(objSalida);
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error en la selección de las Salidas. [{0}]", ex.Message);
            }
            return booResult;
        }

        private bool mtdInsertarCaracterizacion(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCaracterizacion objCaracIn = new clsCaracterizacion();
            List<clsCaracterizacionXEntrada> lstEntrada = new List<clsCaracterizacionXEntrada>();
            List<clsCaracterizacionXActividad> lstActividad = new List<clsCaracterizacionXActividad>();
            List<clsCaracterizacionXSalida> lstSalida = new List<clsCaracterizacionXSalida>();
            clsCaracterizacionBLL cCarac = new clsCaracterizacionBLL();
            #endregion

            booResult = mtdValidarProceso(0, ref objCaracIn, ref strErrMsg);

            if (booResult)
            {
                booResult = mtdSeleccionEntrada(ref lstEntrada, objCaracIn, ref strErrMsg);

                if (booResult)
                    booResult = mtdSeleccionActividad(ref lstActividad, objCaracIn, ref strErrMsg);

                if (booResult)
                    booResult = mtdSeleccionSalida(ref lstSalida, objCaracIn, ref strErrMsg);

                if (booResult)
                {
                    // Se agregan al objeto los nuevos valores
                    objCaracIn.Recursos = txtRecursos.Text;
                    objCaracIn.Numerales = txtNumerales.Text;
                    objCaracIn.Responsables = txtResponsables.Text;
                    objCaracIn.Codigo = txtCodigo.Text;
                    booResult = cCarac.mtdInsertarCaracterizacion(objCaracIn, lstEntrada, lstActividad, lstSalida, ref strErrMsg);
                }
            }
            return booResult;
        }

        private bool mtdActualizarCaracterizacion(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsCaracterizacion objCaracIn = new clsCaracterizacion();
            List<clsCaracterizacionXEntrada> lstEntrada = new List<clsCaracterizacionXEntrada>();
            List<clsCaracterizacionXActividad> lstActividad = new List<clsCaracterizacionXActividad>();
            List<clsCaracterizacionXSalida> lstSalida = new List<clsCaracterizacionXSalida>();
            clsCaracterizacionBLL cCarac = new clsCaracterizacionBLL();
            #endregion

            booResult = mtdValidarProceso(Convert.ToInt32(tbxId.Text.Trim()), ref objCaracIn, ref strErrMsg);

            if (booResult)
            {
                booResult = mtdSeleccionEntrada(ref lstEntrada, objCaracIn, ref strErrMsg);

                if (booResult)
                    booResult = mtdSeleccionActividad(ref lstActividad, objCaracIn, ref strErrMsg);

                if (booResult)
                    booResult = mtdSeleccionSalida(ref lstSalida, objCaracIn, ref strErrMsg);

                if (booResult)
                {
                    // Se agregan al objeto los nuevos valores
                    objCaracIn.Recursos = txtRecursos.Text;
                    objCaracIn.Numerales = txtNumerales.Text;
                    objCaracIn.Responsables = txtResponsables.Text;
                    objCaracIn.Codigo = txtCodigo.Text;
                    booResult = cCarac.mtdActualizarCaracterizacion(objCaracIn, lstEntrada, lstActividad, lstSalida, ref strErrMsg);
                }
            }

            return booResult;
        }
        #endregion
    }
}