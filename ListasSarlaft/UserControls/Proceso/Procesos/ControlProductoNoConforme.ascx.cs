using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class ControlProductoNoConforme : System.Web.UI.UserControl
    {
        string IdFormulario = "4031";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            /*scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.btnInsertarNuevo);*/
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdInicializarValores();

                }
            }
        }
        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;
        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable infoGridPrint;
        private int rowGridPrint;

        private DataTable InfoGrid1
        {
            get
            {
                infoGrid1 = (DataTable)ViewState["infoGrid1"];
                return infoGrid1;
            }
            set
            {
                infoGrid1 = value;
                ViewState["infoGrid1"] = infoGrid1;
            }
        }

        private int RowGrid1
        {
            get
            {
                rowGrid1 = (int)ViewState["rowGrid1"];
                return rowGrid1;
            }
            set
            {
                rowGrid1 = value;
                ViewState["rowGrid1"] = rowGrid1;
            }
        }

        private int PagIndex1
        {
            get
            {
                pagIndex1 = (int)ViewState["pagIndex1"];
                return pagIndex1;
            }
            set
            {
                pagIndex1 = value;
                ViewState["pagIndex1"] = pagIndex1;
            }
        }

        private DataTable InfoGrid2
        {
            get
            {
                infoGrid2 = (DataTable)ViewState["infoGrid2"];
                return infoGrid2;
            }
            set
            {
                infoGrid2 = value;
                ViewState["infoGrid2"] = infoGrid2;
            }
        }

        private int RowGrid2
        {
            get
            {
                rowGrid2 = (int)ViewState["rowGrid2"];
                return rowGrid2;
            }
            set
            {
                rowGrid2 = value;
                ViewState["rowGrid2"] = rowGrid2;
            }
        }

        private int PagIndex2
        {
            get
            {
                pagIndex2 = (int)ViewState["pagIndex2"];
                return pagIndex2;
            }
            set
            {
                pagIndex2 = value;
                ViewState["pagIndex2"] = pagIndex2;
            }
        }

        private DataTable InfoGrid3
        {
            get
            {
                infoGrid3 = (DataTable)ViewState["infoGrid3"];
                return infoGrid3;
            }
            set
            {
                infoGrid3 = value;
                ViewState["infoGrid3"] = infoGrid3;
            }
        }
        private int RowGrid3
        {
            get
            {
                rowGrid3 = (int)ViewState["rowGrid3"];
                return rowGrid3;
            }
            set
            {
                rowGrid3 = value;
                ViewState["rowGrid3"] = rowGrid3;
            }
        }

        private int PagIndex3
        {
            get
            {
                pagIndex3 = (int)ViewState["pagIndex3"];
                return pagIndex3;
            }
            set
            {
                pagIndex3 = value;
                ViewState["pagIndex3"] = pagIndex3;
            }
        }


        private DataTable InfoGridPrint
        {
            get
            {
                infoGridPrint = (DataTable)ViewState["infoGridPrint"];
                return infoGridPrint;
            }
            set
            {
                infoGridPrint = value;
                ViewState["infoGridPrint"] = infoGridPrint;
            }
        }

        private int RowGridPrint
        {
            get
            {
                rowGridPrint = (int)ViewState["rowGridPrint"];
                return rowGridPrint;
            }
            set
            {
                rowGridPrint = value;
                ViewState["rowGridPrint"] = rowGridPrint;
            }
        }
        #endregion
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
            //txtFecha.Text = "" + DateTime.Now;
            PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            if (!mtdLoadControlSalida(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            
            PopulateTreeView();
        }
        protected void mtdResetFields()
        {
            BodyGridCPNC.Visible = true;
            BodyFormCPNC.Visible = false;
            txtId.Text = "";
            TXnoconformidad.Text = "";
            TXnoconformidad.Enabled = true;
            TXacciones.Text = "";
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.ClearSelection();
            ddlProceso.ClearSelection();
            ddlSubproceso.ClearSelection();
            tbxResponsable.Text = "";
            TXobservaciones.Text = "";
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.Enabled = true;
            ddlProceso.Enabled = true;
            ddlSubproceso.Enabled = true;
        }
        #region DDLs
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;
            if (mtdLoadDDLEstados(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            strErrMsg = string.Empty;
            if (mtdLoadDDLCadenaValor(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            //mtdLoadDDLMacroProceso(ref strErrMsg);
        }
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLEstados(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsEstadoControlSalida> lstEstado = new List<clsEstadoControlSalida>();
            clsControlSalidaBLL cCrlSalida = new clsControlSalidaBLL();
            #endregion Vars

            try
            {
                lstEstado = cCrlSalida.mtdConsultarEstados(true, ref strErrMsg);
                DDLestados.Items.Clear();
                DDLestados.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstEstado != null)
                    {
                        int intCounter = 1;

                        foreach (clsEstadoControlSalida objCadenaValor in lstEstado)
                        {
                            DDLestados.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objCadenaValor.strNombre, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                    {
                        booResult = true;
                        strErrMsg = string.Format("No hay datos de estados");
                    }
                }
                else
                {
                    booResult = true;
                    strErrMsg = string.Format("No hay datos de estados");
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de los Estados. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
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
                ddlCadenaValor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
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
                ddlMacroproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstMacroproceso != null)
                    {
                        int intCounter = 1;


                        foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
                        {
                            ddlMacroproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objMacroproceso.strNombreMacroproceso, objMacroproceso.intId.ToString()));
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
                ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstProceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsProceso objProceso in lstProceso)
                        {
                            ddlProceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objProceso.strNombreProceso, objProceso.intId.ToString()));
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
                ddlSubproceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {

                    if (lstSubproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsSubproceso objSubProceso in lstSubproceso)
                        {
                            ddlSubproceso.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objSubProceso.strNombreSubproceso, objSubProceso.intId.ToString()));
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
        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView4.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT PJO.IdHijo, PJO.IdPadre, PJO.NombreHijo, PDJ.NombreResponsable, PDJ.CorreoResponsable " +
                "FROM Parametrizacion.JerarquiaOrganizacional PJO LEFT JOIN Parametrizacion.DetalleJerarquiaOrg PDJ ON PJO.idHijo = PDJ.idHijo";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";

            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeView4.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            tbxResponsable.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview
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
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            mtdCargarDDLs();
            BodyGridCPNC.Visible = false;
            BodyFormCPNC.Visible = true;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarControlSalida(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdUpdateControlSalida(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdStard();
                mtdResetFields();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdStard();
            mtdResetFields();
        }
        /// <summary>
        /// Realiza la insercion de la evaluacion
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        protected bool mtdInsertarControlSalida(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsControlSalida objCrlSalidaInd = new clsControlSalida();
            clsObservacionControlSalida objObservacion = new clsObservacionControlSalida();
            clsControlSalidaBLL cControlSalida = new clsControlSalidaBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            clsControlProceso objControlProceso = new clsControlProceso();
            #endregion
            if (ddlSubproceso.SelectedValue.ToString() != "0" && ddlSubproceso.SelectedValue.ToString() != "")
            {
                objCrlSalidaInd.intIdMacroProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                objControlProceso.intIdTipoProceso = 3;
                objControlProceso.intIdProcesos = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
            }
            else
            {
                if (ddlProceso.SelectedValue.ToString() != "0" && ddlProceso.SelectedValue.ToString() != "")
                {
                    objCrlSalidaInd.intIdMacroProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    objControlProceso.intIdTipoProceso = 2;
                    objControlProceso.intIdProcesos = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue.ToString() != "0" && ddlMacroproceso.SelectedValue.ToString() != "")
                    {
                        objCrlSalidaInd.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        objControlProceso.intIdTipoProceso = 1;
                        objControlProceso.intIdProcesos = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                    }
                    else
                    {
                        strErrMsg = "Debe seleccionar el Macroproceso para seguir";
                        return booResult;
                    }
                }
            }
            objCrlSalidaInd.intIdEstadoControlSalida = Convert.ToInt32(DDLestados.SelectedValue.ToString());
            objCrlSalidaInd.strNoConformidad = TXnoconformidad.Text;
            objCrlSalidaInd.strAccionesTomadas = TXacciones.Text;
            objCrlSalidaInd.intCargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
            objCrlSalidaInd.dtFechaRegistro = Convert.ToDateTime(DateTime.Now);
            objCrlSalidaInd.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);

            booResult = cControlSalida.mtdInsertarControlSalida(objCrlSalidaInd, ref strErrMsg);
            if (booResult == true)
            {
                int IdControlSalida = cControlSalida.mtdLastIdControlSalida(ref strErrMsg);
                //booResult = true;
                objControlProceso.intIdControl = IdControlSalida;
                booResult = cControlSalida.mtdInsertarControlProcesos(objControlProceso,ref strErrMsg);
                objObservacion.intIdControlSalida = IdControlSalida;
                objObservacion.strDescripcion = TXobservaciones.Text;
                objObservacion.dtFechaRegistro = DateTime.Now;
                objObservacion.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());

                booResult = cControlSalida.mtdInsertarObservacion(objObservacion, ref strErrMsg);
                strErrMsg = "Control producto no conforme registrado exitosamente";
                if (booResult != true)
                    strErrMsg = "Error al registrar el control producto no conforme";
            }
            return booResult;
        }
        private bool mtdLoadControlSalida(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsControlSalida objCrlSalida = new clsControlSalida();
            List<clsControlSalida> lstCrlSalida = new List<clsControlSalida>();
            clsControlSalidaBLL cCrtSalida = new clsControlSalidaBLL();
            #endregion Vars
            lstCrlSalida = cCrtSalida.mtdConsultarControlSalida(ref strErrMsg, ref lstCrlSalida);

            if (lstCrlSalida != null)
            {
                mtdLoadControlSalida();
                mtdLoadControlSalida(lstCrlSalida);
                GVcontrolSalida.DataSource = lstCrlSalida;
                GVcontrolSalida.PageIndex = pagIndex1;
                GVcontrolSalida.DataBind();
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadControlSalida()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdMacroProceso", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("intIdEstadoControlSalida", typeof(string));
            grid.Columns.Add("strNombreEstado", typeof(string));
            grid.Columns.Add("strNoConformidad", typeof(string));
            grid.Columns.Add("strAccionesTomadas", typeof(string));
            grid.Columns.Add("intCargoResponsable", typeof(string));
            grid.Columns.Add("strPersonaAutoriza", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));
            grid.Columns.Add("strObservaciones", typeof(string));

            GVcontrolSalida.DataSource = grid;
            GVcontrolSalida.DataBind();
            InfoGrid1 = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadControlSalida(List<clsControlSalida> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsControlSalida objControl in lstControl)
            {

                InfoGrid1.Rows.Add(new Object[] {
                    objControl.intId.ToString().Trim(),
                            objControl.intIdMacroProceso.ToString().Trim(),
                            objControl.strNombreProceso.ToString().Trim(),
                            objControl.intIdEstadoControlSalida.ToString().Trim(),
                            objControl.strNombreEstado.ToString().Trim(),
                            objControl.strNoConformidad.ToString().Trim(),
                            objControl.strAccionesTomadas.ToString().Trim(),
                            objControl.intCargoResponsable.ToString().Trim(),
                            objControl.strPersonaAutoriza.ToString().Trim(),
                            objControl.dtFechaRegistro.ToString().Trim(),
                            objControl.intIdUsuario.ToString().Trim(),
                            objControl.strUsuario.ToString().Trim(),
                            objControl.strObservaciones.ToString().Trim(),
                    });
            }
        }

        protected void GVcontrolSalida_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    break;
            }
        }
        private void mtdShowUpdate(int RowGrid)
        {
            mtdCargarDDLs();
            string strErrMsg = string.Empty;
            GridViewRow row = GVcontrolSalida.Rows[RowGrid];
            var colsNoVisible = GVcontrolSalida.DataKeys[RowGrid].Values;
            BodyGridCPNC.Visible = false;
            BodyFormCPNC.Visible = true;

            #region DatosControl
            txtId.Text = row.Cells[0].Text;
            int IdControl = Convert.ToInt32(row.Cells[0].Text);
            /*LtexProceso.Text = ((Label)row.FindControl("NombreProceso")).Text;
            LtexProceso.Visible = true;*/
            ddlSubproceso.Enabled = false;
            ddlProceso.Enabled = false;
            ddlMacroproceso.Enabled = false;
            ddlCadenaValor.Enabled = false;
            TXnoconformidad.Text = ((Label)row.FindControl("NoConformidad")).Text;
            TXnoconformidad.Enabled = false;
            TXacciones.Text = ((Label)row.FindControl("AccionesTomadas")).Text;
            tbxResponsable.Text = ((Label)row.FindControl("PersonaAutoriza")).Text;

            mtdLoadDDLEstados(ref strErrMsg);
            for(int i = 0; i < DDLestados.Items.Count; i++)
            {
                DDLestados.SelectedIndex = i;
                if(DDLestados.SelectedItem.Text == ((Label)row.FindControl("NombreEstado")).Text)
                {
                    break;
                }
            }
            //DDLestados.SelectedItem.Text = ((Label)row.FindControl("NombreEstado")).Text;
            /*LtextEstado.Text = ((Label)row.FindControl("NombreEstado")).Text;
            LtextEstado.Visible = true;*/
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();
            lblIdDependencia4.Text = colsNoVisible[2].ToString();
            TXobservaciones.Text = colsNoVisible[3].ToString();
            int intIdProceso = Convert.ToInt32(colsNoVisible[4].ToString());
            clsProcesoIndicador objProcInd = new clsProcesoIndicador(Convert.ToInt32(colsNoVisible[4].ToString()), 0, 0, 0, string.Empty);
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcesoControl(Convert.ToInt32(row.Cells[0].Text), ref strErrMsg);

            switch (objProcesos[0].ToString())
            {
                case "M":
                    clsMacroproceso objMP = (clsMacroproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objMP.intIdCadenaDeValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objMP.intId.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = "0";
                    break;
                case "P":
                    clsProceso objP = (clsProceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objP.intIdCadenaValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objP.intIdMacroProceso.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = objP.intId.ToString();
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = "0";
                    break;
                case "S":
                    clsSubproceso objSP = (clsSubproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objSP.intIdCadenaValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objSP.intIdMacroProceso.ToString();
                    mtdLoadDDLProceso(ref strErrMsg);
                    ddlProceso.SelectedValue = objSP.intIdProceso.ToString();
                    mtdLoadDDLSubproceso(ref strErrMsg);
                    ddlSubproceso.SelectedValue = objSP.intId.ToString();
                    break;
            }
            #endregion
        }
        private bool mtdUpdateControlSalida(ref string strErrMsg)
        {
            bool booResult = false;
            clsObservacionControlSalida objObservacion = new clsObservacionControlSalida();
            clsControlSalida cCrlSalida = new clsControlSalida();
            clsControlSalidaBLL cCSalidaBll = new clsControlSalidaBLL();
            cCrlSalida.intId = Convert.ToInt32(txtId.Text);
            cCrlSalida.strPersonaAutoriza = lblIdDependencia4.Text;
            cCrlSalida.strAccionesTomadas =TXacciones.Text;
            cCrlSalida.intIdEstadoControlSalida = Convert.ToInt32(DDLestados.SelectedValue.ToString());
            booResult = cCSalidaBll.mtdActualizarControlSalida(cCrlSalida, ref strErrMsg);

            objObservacion.intIdControlSalida = Convert.ToInt32(txtId.Text);
            objObservacion.strDescripcion = TXobservaciones.Text;

            booResult = cCSalidaBll.mtdActualizarObservacion(objObservacion, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Control salida no conforme actualizada  exitosamente";
            else
                strErrMsg = "Error al actualizar control salida no conforme";
            return booResult;
        }

        protected void GVcontrolSalida_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndex1 = e.NewPageIndex;
            string strErrMsg = "";
            mtdLoadControlSalida(ref strErrMsg);
        }
    }
}