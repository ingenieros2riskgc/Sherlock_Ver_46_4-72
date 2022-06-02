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
using System.Configuration;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class ProgramaCapacitacion : System.Web.UI.UserControl
    {
        string IdFormulario = "4026";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.GVprogramaCapacitacion);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
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
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridCompetencias;
        private int rowGridCompetencias;
        private int pagIndexCompetencias;

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
            }
        }
        private DataTable InfoGridCompetencias
        {
            get
            {
                infoGridCompetencias = (DataTable)ViewState["infoGridCompetencias"];
                return infoGridCompetencias;
            }
            set
            {
                infoGridCompetencias = value;
                ViewState["infoGridCompetencias"] = infoGridCompetencias;
            }
        }

        private int RowGridCompetencias
        {
            get
            {
                rowGridCompetencias = (int)ViewState["rowGridCompetencias"];
                return rowGridCompetencias;
            }
            set
            {
                rowGridCompetencias = value;
                ViewState["rowGridCompetencias"] = rowGridCompetencias;
            }
        }

        private int PagIndexCompetencias
        {
            get
            {
                pagIndexCompetencias = (int)ViewState["pagIndexCompetencias"];
                return pagIndexCompetencias;
            }
            set
            {
                pagIndexCompetencias = value;
                ViewState["pagIndexCompetencias"] = pagIndexCompetencias;
            }
        }
        #endregion
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            /*mtdLoadEvaluacionProveedor(ref strErrMsg);*/
            if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            PopulateTreeView();
            mtdLoadControlInfraestructura(ref strErrMsg);
        }
        protected void mtdResetFields()
        {
            BodyFormPC.Visible = false;
            BodyGridPC.Visible = true;

            txtId.Text = "";
            tbxResponsable.Text = "";
            TXactividad.Text = "";
            TXdirigido.Text = "";
            TXfechapro.Text = "";
            TXasistentes.Text = "";
            TXfecharealizada.Text = "";
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
            TXevaluacion.Text = "";

            ddlCadenaValor.SelectedIndex = 0;
            ddlMacroproceso.SelectedIndex = 0;
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            mtdCargarDDLs();
            GridViewRow row = GVprogramaCapacitacion.Rows[RowGrid];
            var colsNoVisible = GVprogramaCapacitacion.DataKeys[RowGrid].Values;
            txtId.Text = row.Cells[0].Text;
            ddlCadenaValor.Enabled = false;
            ddlMacroproceso.Enabled = false;
            lblIdDependencia4.Text = colsNoVisible[2].ToString();
            tbxResponsable.Text = ((Label)row.FindControl("NombreHijo")).Text;
            tbxResponsable.Enabled = false;
            TXactividad.Text = ((Label)row.FindControl("actividad")).Text;
            TXactividad.Enabled = false;
            TXdirigido.Text = ((Label)row.FindControl("dirigido")).Text;
            TXdirigido.Enabled = false;
            TXfechapro.Text = row.Cells[7].Text;
            TXfechapro.Enabled = false;
            
            TXfecharealizada.Text = ((Label)row.FindControl("dtFechaRealizada")).Text;
            //if (row.Cells[9].Text != "0")
                TXevaluacion.Text = ((Label)row.FindControl("intEvaluacion")).Text;
            /*else
                TXevaluacion.Text = "";*/
            string reco = ((Label)row.FindControl("asistentes")).Text;
            string[] texto = reco.Split('|');
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i].ToString() != "")
                    TXasistentes.Text += texto[i].ToString();//+ Environment.NewLine;
            }
            tbxUsuarioCreacion.Text = colsNoVisible[0].ToString();
            txtFecha.Text = colsNoVisible[1].ToString();
            #region Procesos
            //Se DEBE CARGAR INFORMACION DEBE PROCESOS DEBE ACUERDO A LA INFORMACION DEL GRID
            string strErrMsg = string.Empty;
            tbxProcIndica.Text = colsNoVisible[2].ToString();
            clsProcesoIndicador objProcInd = new clsProcesoIndicador(Convert.ToInt32(colsNoVisible[3].ToString()), 0, 0, 0, string.Empty);
            clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
            object[] objProcesos = cProcInd.mtdConsultarProcIndicadorIdProceso(objProcInd, ref strErrMsg);

            switch (objProcesos[0].ToString())
            {
                case "M":
                    clsMacroproceso objMP = (clsMacroproceso)objProcesos[1];
                    ddlCadenaValor.SelectedValue = objMP.intIdCadenaDeValor.ToString();
                    mtdLoadDDLMacroProceso(ref strErrMsg);
                    ddlMacroproceso.SelectedValue = objMP.intId.ToString();
                    //mtdLoadDDLProceso(ref strErrMsg);
                    //ddlProceso.SelectedValue = "0";
                    break;
                /*case "P":
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
                        break;*/
            }
            #endregion
        }

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
        #region ButtonEvents
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            BodyFormPC.Visible = true;
            BodyGridPC.Visible = false;

            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;

            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.Enabled = true;
            tbxResponsable.Enabled = true;
            imgDependencia4.Visible = true;
            TXactividad.Enabled = true;
            TXdirigido.Enabled = true;
            TXfechapro.Enabled = true;
        }
        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdInsertarProgramaCapacitacion(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
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
            if (mtdUpdateProgramaCapacitacion(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
            mtdStard();
        }
        #endregion
        #region DLLeventos
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();
            
            if (!mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
        #endregion
        #region LoadMetodos
        private bool mtdCargarDDLs(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadDDLCadenaValor(ref strErrMsg);
            //if (booResult)
            //    booResult = mtdLoadDDLMacroProceso(ref strErrMsg);

            return booResult;
        }
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
                        booResult = true;
                    }
                    else
                        booResult = false;
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }
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
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }
        private bool mtdLoadPrograma(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsProgramacionCapacitacion> lstPrograma = new List<clsProgramacionCapacitacion>();
            clsProgramaCapacitacionBLL cPrograma = new clsProgramaCapacitacionBLL();

            lstPrograma = cPrograma.mtdConsultarPrograma(ref lstPrograma, ref strErrMsg);

            if (lstPrograma.Count > 0)
            {
                mtdLoadGridPrograma();
                mtdLoadGridPrograma(lstPrograma, ref strErrMsg);
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridPrograma()
        {
            DataTable gridPrograma = new DataTable();

            gridPrograma.Columns.Add("intId", typeof(string));
            gridPrograma.Columns.Add("strNombreProceso", typeof(string));
            gridPrograma.Columns.Add("strCargoResponsable", typeof(string));
            gridPrograma.Columns.Add("strActividad", typeof(string));
            gridPrograma.Columns.Add("strDirigidoA", typeof(string));
            gridPrograma.Columns.Add("dtFechaProgramada", typeof(string));
            gridPrograma.Columns.Add("dtFechaRealizada", typeof(string));
            gridPrograma.Columns.Add("intEvaluacion", typeof(string));
            gridPrograma.Columns.Add("strAsistentes", typeof(string));
            gridPrograma.Columns.Add("strUsuario", typeof(string));

            GVprogramaCapacitacion.DataSource = gridPrograma;
            GVprogramaCapacitacion.DataBind();
            InfoGrid = gridPrograma;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de los Detalles Factor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadGridPrograma(List<clsProgramacionCapacitacion> lstPrograma, ref string strErrMsg)
        {
            if (lstPrograma != null)
            {
                mtdLoadGridPrograma(lstPrograma);
                GVprogramaCapacitacion.DataSource = lstPrograma;
                GVprogramaCapacitacion.DataBind();
            }
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los criterios</param>
        private void mtdLoadGridPrograma(List<clsProgramacionCapacitacion> lstPrograma)
        {
            foreach (clsProgramacionCapacitacion objDetFactor in lstPrograma)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objDetFactor.intId.ToString().Trim(),
                    objDetFactor.strNombreProceso.ToString().Trim(),
                    objDetFactor.strCargoResponsable.ToString().Trim(),
                    objDetFactor.strActividad.ToString().Trim(),
                    objDetFactor.strDirigidoA.ToString().Trim(),
                    objDetFactor.dtFechaProgramada.ToString().Trim(),
                    objDetFactor.dtFechaRealizada.ToString().Trim(),
                    objDetFactor.intEvaluacion.ToString().Trim(),
                    objDetFactor.strAsistentes.ToString().Trim(),
                    objDetFactor.strUsuario
                    });
            }
        }
        private bool mtdLoadControlInfraestructura(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsControlInfraestructura objCrlInfra = new clsControlInfraestructura();
            List<clsProgramacionCapacitacion> lstPrograma = new List<clsProgramacionCapacitacion>();
            clsProgramaCapacitacionBLL cPrograma = new clsProgramaCapacitacionBLL();
            #endregion Vars
            lstPrograma = cPrograma.mtdConsultarPrograma(ref lstPrograma, ref strErrMsg);

            if (lstPrograma != null)
            {
                mtdLoadGridControlInfraestructura();
                mtdLoadGridControlInfraestructura(lstPrograma);
                GVprogramaCapacitacion.DataSource = lstPrograma;
                GVprogramaCapacitacion.PageIndex = pagIndex;
                GVprogramaCapacitacion.DataBind();
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridControlInfraestructura()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("IdMacroproceso", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("IdCargoResponsable", typeof(string));
            grid.Columns.Add("strCargoResponsable", typeof(string));
            grid.Columns.Add("strActividad", typeof(string));
            grid.Columns.Add("strDirigidoA", typeof(string));
            grid.Columns.Add("dtFechaProgramada", typeof(string));
            grid.Columns.Add("dtFechaRealizada", typeof(string));
            grid.Columns.Add("intEvaluacion", typeof(string));
            grid.Columns.Add("strAsistentes", typeof(string));
            grid.Columns.Add("strUsuario", typeof(string));

            GVprogramaCapacitacion.DataSource = grid;
            GVprogramaCapacitacion.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridControlInfraestructura(List<clsProgramacionCapacitacion> lstControl)
        {
            string strErrMsg = String.Empty;
            //clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsProgramacionCapacitacion objEvaComp in lstControl)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.IdMacroProceso.ToString().Trim(),
                    objEvaComp.strNombreProceso.ToString().Trim(),
                    objEvaComp.IdCargoResponsable.ToString().Trim(),
                    objEvaComp.strCargoResponsable.ToString().Trim(),
                    objEvaComp.strActividad.ToString().Trim(),
                    objEvaComp.strDirigidoA.ToString().Trim(),
                    objEvaComp.dtFechaProgramada.ToString().Trim(),
                    objEvaComp.dtFechaRealizada.ToString().Trim(),
                    objEvaComp.intEvaluacion.ToString().Trim(),
                    objEvaComp.strAsistentes.ToString().Trim(),
                    objEvaComp.strUsuario
                    });
            }
        }
        #endregion
        #region Metodos
        private bool mtdInsertarProgramaCapacitacion(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsProgramacionCapacitacion objPrograInd = new clsProgramacionCapacitacion();
            clsProgramaCapacitacionBLL cProgramaCapacitacion = new clsProgramaCapacitacionBLL();
            //clsEvaluacionDesempeno objeValorEvaluacionDesempeño = new clsEvaluacionDesempeno();
            #endregion
            objPrograInd.intId = 0;
            objPrograInd.IdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            objPrograInd.IdCargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
            objPrograInd.strActividad = TXactividad.Text;
            objPrograInd.strDirigidoA = TXdirigido.Text;
            //if (TXfechapro.Text != "")
            objPrograInd.dtFechaProgramada = TXfechapro.Text;
            
            objPrograInd.dtFechaRealizada = TXfecharealizada.Text;
            if (TXevaluacion.Text != "")
                objPrograInd.intEvaluacion = TXevaluacion.Text;
            else
                objPrograInd.intEvaluacion = "";
            objPrograInd.dtFechaRegistro = DateTime.Now;
            objPrograInd.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            string[] textos = TXasistentes.Text.Split('\n');
            string asistentes = string.Empty;
            if (textos.Length > 0)
            {
                for (int i = 0; i < textos.Length; i++)
                {
                    if (textos[i].ToString() != "")
                        asistentes += "|" + (i + 1) + "." + textos[i].ToString();
                }
            }
            objPrograInd.strAsistentes = asistentes;
            booResult = cProgramaCapacitacion.mtdInsertarProgramaCapacitacion(objPrograInd, ref strErrMsg);
            if (booResult == true)
            {
                strErrMsg = "Programación capacitación registrada exitosamente";
            }
            else
            {
                strErrMsg = "Error al registrar la programación capacitación";
            }
            return booResult;
        }
        private bool mtdUpdateProgramaCapacitacion(ref string strErrMsg)
        {
            bool booResult = false;
            clsProgramacionCapacitacion objPrograma = new clsProgramacionCapacitacion();
            objPrograma.intId = Convert.ToInt32(txtId.Text);
            objPrograma.dtFechaRealizada = TXfecharealizada.Text;
            objPrograma.intEvaluacion = TXevaluacion.Text;
            string[] textos = TXasistentes.Text.Split('\n');
            string Compromisos = string.Empty;
            if (textos.Length > 0)
            {

                for (int i = 0; i < textos.Length; i++)
                {
                    if (textos[i].ToString() != "")
                    {
                        Compromisos += "|" + textos[i].ToString();
                    }
                }
            }
            objPrograma.strAsistentes = Compromisos;
            clsProgramaCapacitacionBLL cPrograma = new clsProgramaCapacitacionBLL();

            booResult = cPrograma.mtdUpdateProgramaCapacitacion(ref objPrograma, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Programación capacitación actualizada  exitosamente";
            else
                strErrMsg = "Error al actualizar la programación de la capacitación";
            return booResult;
        }
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;

            mtdLoadDDLCadenaValor(ref strErrMsg);
            mtdLoadDDLMacroProceso(ref strErrMsg);
        }
        #endregion

        protected void GVprogramaCapacitacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVprogramaCapacitacion.PageIndex = PagIndex;
            GVprogramaCapacitacion.DataBind();
            string strErrMsg = "";
            mtdLoadControlInfraestructura(ref strErrMsg);
        }

        protected void GVprogramaCapacitacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    BodyFormPC.Visible = true;
                    BodyGridPC.Visible = false;
                    break;
            }
        }

    }
}