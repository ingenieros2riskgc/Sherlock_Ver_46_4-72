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
    public partial class ControlInfraestructura : System.Web.UI.UserControl
    {
        string IdFormulario = "4028";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    mtdStartLoad();
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
        #region ButtonEvents
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            mtdShowFields();
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdRestFields();
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = String.Empty;
            if (mtdUpdateControlInfra(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdStartLoad();
                mtdRestFields();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }

            string strErrMsg = String.Empty;
            if (mtdInsertarControlInfra(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdRestFields();
                mtdStartLoad();
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        #endregion
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;

            mtdLoadDDLCadenaValor(ref strErrMsg);
            mtdLoadDDLMacroProceso(ref strErrMsg);
        }
        #region DLLeventos
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();
            //ddlProceso.Items.Clear();
            //ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        
        #endregion
        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            /*PagIndex2 = 0;*/
            //txtNombreEva.Text = "";
            //TXfecharegistro.Text = "" + DateTime.Now;
            //tbxResponsable.Text = "";
            //TXjefe.Text = "";
        }
        private void mtdRestFields()
        {
            BodyFormCI.Visible = false;
            BodyGridCI.Visible = true;
            Dbutton.Visible = false;

            txtId.Text = "";
            tbxResponsable.Text = "";
            txtActividad.Text = "";
            TXfechaProg.Text = "";
            TXfechaCump.Text = "";
            tbxUsuarioCreacion.Text = "";
            txtFecha.Text = "";
            TXobservaciones.Text = "";
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.ClearSelection();
            CBallProcess.Checked = false;
        }
        private void mtdShowUpdate(int RowGrid)
        {
            mtdShowFields();
            string strErrMsg = string.Empty;
            GridViewRow row = GVcontrolInfraestructura.Rows[RowGrid];
            var colsNoVisible = GVcontrolInfraestructura.DataKeys[RowGrid].Values;
            
            int allProcess = Convert.ToInt32(colsNoVisible[4].ToString());
            txtId.Text = row.Cells[0].Text;
            lblIdDependencia4.Text = colsNoVisible[3].ToString();
            tbxResponsable.Text = row.Cells[4].Text;
            tbxResponsable.Enabled = false;
            ddlCadenaValor.Enabled = false;
            ddlMacroproceso.Enabled = false;
            CBallProcess.Enabled = false;
            if (allProcess == 0)
            {
                mtdCargarDDLs();
                Session["NombreProceso"] = ((Label)row.FindControl("NombreProceso")).Text;
                #region Procesos
                //Se DEBE CARGAR INFORMACION DEBE PROCESOS DEBE ACUERDO A LA INFORMACION DEL GRID
                tbxProcIndica.Text = colsNoVisible[2].ToString();
                clsProcesoIndicador objProcInd = new clsProcesoIndicador(Convert.ToInt32(colsNoVisible[2].ToString()), 0, 0, 0, string.Empty);
                clsProcesoIndicadorBLL cProcInd = new clsProcesoIndicadorBLL();
                object[] objProcesos = cProcInd.mtdConsultarProcIndicadorIdProceso(objProcInd, ref strErrMsg);

                //switch (objProcesos[0].ToString())
                //{
                //    case "M":
                //        clsMacroproceso objMP = (clsMacroproceso)objProcesos[1];
                //        ddlCadenaValor.SelectedValue = objMP.intIdCadenaDeValor.ToString();
                //        mtdLoadDDLMacroProceso(ref strErrMsg);
                //        ddlMacroproceso.SelectedValue = objMP.intId.ToString();
                //        //mtdLoadDDLProceso(ref strErrMsg);
                //        //ddlProceso.SelectedValue = "0";
                //        break;
                //        /*case "P":
                //            clsProceso objP = (clsProceso)objProcesos[1];
                //            ddlCadenaValor.SelectedValue = objP.intIdCadenaValor.ToString();
                //            mtdLoadDDLMacroProceso(ref strErrMsg);
                //            ddlMacroproceso.SelectedValue = objP.intIdMacroProceso.ToString();
                //            mtdLoadDDLProceso(ref strErrMsg);
                //            ddlProceso.SelectedValue = objP.intId.ToString();
                //            mtdLoadDDLSubproceso(ref strErrMsg);
                //            ddlSubproceso.SelectedValue = "0";
                //            break;
                //        case "S":
                //            clsSubproceso objSP = (clsSubproceso)objProcesos[1];
                //            ddlCadenaValor.SelectedValue = objSP.intIdCadenaValor.ToString();
                //            mtdLoadDDLMacroProceso(ref strErrMsg);
                //            ddlMacroproceso.SelectedValue = objSP.intIdMacroProceso.ToString();
                //            mtdLoadDDLProceso(ref strErrMsg);
                //            ddlProceso.SelectedValue = objSP.intIdProceso.ToString();
                //            mtdLoadDDLSubproceso(ref strErrMsg);
                //            ddlSubproceso.SelectedValue = objSP.intId.ToString();
                //            break;*/
                //}
                #endregion
            }
            else
            {
                CBallProcess.Checked = true;
            }
            imgDependencia4.Visible = false;
            txtActividad.Text = row.Cells[5].Text;
            txtActividad.Enabled = false;
            TXfechaProg.Text = row.Cells[6].Text;
            TXfechaProg.Enabled = false;
            
            TXfechaCump.Text = ((Label)row.FindControl("dtFechaCumplimiento")).Text;
            
            TXobservaciones.Text = ((Label)row.FindControl("Observaciones")).Text;
            tbxUsuarioCreacion.Text = colsNoVisible[1].ToString();
            txtFecha.Text = colsNoVisible[0].ToString();
            
            

        }
        private void mtdShowFields()
        {
            BodyGridCI.Visible = false;
            BodyFormCI.Visible = true;
            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.Enabled = true;
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            tbxResponsable.Enabled = true;
            txtActividad.Enabled = true;
            TXfechaProg.Enabled = true;
            imgDependencia4.Visible = true;
            CBallProcess.Enabled = true;
            //Dbutton.Visible = true;
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
        
        
        private void mtdStartLoad()
        {
            string strErrMsg = string.Empty;
            if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            PopulateTreeView();
            mtdLoadControlInfraestructura(ref strErrMsg);
        }
        private bool mtdLoadControlInfraestructura(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsControlInfraestructura objCrlInfra = new clsControlInfraestructura();
            List<clsControlInfraestructura> lstInfraestructura = new List<clsControlInfraestructura>();
            clsControlInfraestructuraBLL cCrtInfra = new clsControlInfraestructuraBLL();
            #endregion Vars
            lstInfraestructura = cCrtInfra.mtdConsultarControlInfraestructura(ref lstInfraestructura, ref strErrMsg);

            if (lstInfraestructura != null)
            {
                mtdLoadGridControlInfraestructura();
                mtdLoadGridControlInfraestructura(lstInfraestructura);
                GVcontrolInfraestructura.DataSource = lstInfraestructura;
                GVcontrolInfraestructura.PageIndex = pagIndex;
                GVcontrolInfraestructura.DataBind();
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
            grid.Columns.Add("intIdMacroProceso", typeof(string));
            grid.Columns.Add("strNombreproceso", typeof(string));
            grid.Columns.Add("intResponsable", typeof(string));
            grid.Columns.Add("strCargoResponsable", typeof(string));
            grid.Columns.Add("strActividad", typeof(string));
            grid.Columns.Add("dtFechaProgramada", typeof(string));
            grid.Columns.Add("dtFechaCumplimiento", typeof(string));
            grid.Columns.Add("strObservaciones", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("struserName", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));
            grid.Columns.Add("intAllProcess", typeof(string));
            GVcontrolInfraestructura.DataSource = grid;
            GVcontrolInfraestructura.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadGridControlInfraestructura(List<clsControlInfraestructura> lstControl)
        {
            string strErrMsg = String.Empty;
            clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            foreach (clsControlInfraestructura objEvaComp in lstControl)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.intIdMacroProceso.ToString().Trim(),
                    objEvaComp.strNombreProceso.ToString().Trim(),
                    objEvaComp.intResponsable.ToString().Trim(),
                    objEvaComp.strCargoResponsable.ToString().Trim(),
                    objEvaComp.strActividad.ToString().Trim(),
                    objEvaComp.dtFechaProgramada.ToString().Trim(),
                    objEvaComp.dtFechaCumplimiento.ToString().Trim(),
                    objEvaComp.strObservaciones.ToString().Trim(),
                    objEvaComp.intIdUsuario.ToString().Trim(),
                    objEvaComp.struserName.ToString().Trim(),
                    objEvaComp.dtFechaRegistro.ToString().Trim(),
                    objEvaComp.intAllProcess.ToString().Trim()
                    });
            }
        }
        #region LoadDLL
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
        #endregion
        private bool mtdInsertarControlInfra(ref string strErrMsg)
        {
            bool booResult = false;
            clsControlInfraestructura objCrlInfra = new clsControlInfraestructura();/*
               , , ,,,
               TXobservaciones.Text, , DateTime.Now);*/
               if(CBallProcess.Checked)
            {
                objCrlInfra.intIdMacroProceso = 0;
                objCrlInfra.strNombreProceso = "";
                objCrlInfra.intAllProcess = 1;
            }
            else {
                objCrlInfra.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                objCrlInfra.strNombreProceso = ddlMacroproceso.SelectedItem.Text;
                objCrlInfra.intAllProcess = 0;
            }
            
            //objCrlInfra.intResponsable = Convert.ToInt32(lblIdDependencia4.Text);
            objCrlInfra.strCargoResponsable = tbxResponsable.Text;
            objCrlInfra.strActividad = txtActividad.Text;
            objCrlInfra.dtFechaProgramada = TXfechaProg.Text;
            //if (TXfechaCump.Text != "")
            objCrlInfra.dtFechaCumplimiento = TXfechaCump.Text;
            objCrlInfra.strObservaciones = TXobservaciones.Text;
            objCrlInfra.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            objCrlInfra.dtFechaRegistro = DateTime.Now;
            clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            booResult = cCrlInfra.mtdInsertarControlInfraestructura(objCrlInfra, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Infraestructura  registrada exitosamente";
            else
                strErrMsg = "Error al registrar control de infraestructura";
            return booResult;
        }
        private bool mtdUpdateControlInfra(ref string strErrMsg)
        {
            bool booResult = false;
            clsControlInfraestructura objCrlInfra = new clsControlInfraestructura(Convert.ToInt32(txtId.Text), TXobservaciones.Text, TXfechaCump.Text);
            clsControlInfraestructuraBLL cCrlInfra = new clsControlInfraestructuraBLL();

            booResult = cCrlInfra.mtdUpdateControlInfraestructura(objCrlInfra, ref strErrMsg);
            if (booResult == true)
                strErrMsg = "Infraestructura  actualizado exitosamente";
            else
                strErrMsg = "Error al actualizar control de infraestructura";
            return booResult;
        }
        #endregion

        protected void GVcontrolInfraestructura_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    Dbutton.Visible = true;
                    break;
            }
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }
        private void mtdExportPdf()
        {
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Control de Infraestructura");
            //....header
            // we Add a Header that will show up on PAGE 1
            // Creamos la imagen y le ajustamos el tamaño
            string pathImg = Server.MapPath("~") + "Imagenes/Logos/Risk.png";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
            pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
            iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(pathImg);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            PdfPTable pdftblImage = new PdfPTable(2);
            PdfPCell pdfcellImage = new PdfPCell(imagen, true);
            pdfcellImage.FixedHeight = 40f;
            /*pdfcellImage.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcellImage.VerticalAlignment = Element.ALIGN_LEFT;*/
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdfcellImage.Border = Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();

            phHeader.Add(pdftblImage);
            //phHeader.Add(chnCompany);
            #region Tabla de Datos Principales
            Font font1 = new Font();
            font1.Color = new Color(GVcontrolInfraestructura.HeaderStyle.ForeColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Código:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtId.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Macroproceso:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            if(CBallProcess.Checked)
                pdfCellEncabezado = new PdfPCell(new Phrase("Todos los procesos"));
            else
                pdfCellEncabezado = new PdfPCell(new Phrase(Session["NombreProceso"].ToString()));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Cargo:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxResponsable.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Actividad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtActividad.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            

            PdfPTable pdfTableObservaciones = new PdfPTable(2);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Programación Actividad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaProg.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Cumplimiento Actividad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaCump.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Observaciones:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXobservaciones.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Usuario Registro:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxUsuarioCreacion.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Registro:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVcontrolInfraestructura.HeaderStyle.BackColor);
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txtFecha.Text));
            pdfTableObservaciones.AddCell(pdfCellEncabezado);
            #endregion

            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Control de Infraestructura"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTableObservaciones);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteControlInfraestructura.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteControlInfraestructura_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }

        protected void exportExcel(HttpResponse Response, string filename)
        {

            /*Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg1 = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dgEncabezado = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dgData = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dg2 = new System.Web.UI.WebControls.DataGrid();

            
            
            /*dg1.DataSource = InfoGridPrint;
            dg1.DataBind();
            dg1.RenderControl(htmlWrite);

            dgData.DataSource = gridData;
            dgData.DataBind();
            dgData.RenderControl(htmlWrite);

            dg2.DataSource = gridRecomendacion;
            dg2.DataBind();
            dg2.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();*/
            
            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Código:");
            gridEncabezado.Columns.Add("Macroproceso:");
            gridEncabezado.Columns.Add("Cargo:");
            gridEncabezado.Columns.Add("Actividad:");
            gridEncabezado.Columns.Add("Fecha Programación Actividad:");
            gridEncabezado.Columns.Add("Fecha Cumplimiento Actividad:");
            gridEncabezado.Columns.Add("Observaciones:");
            gridEncabezado.Columns.Add("Usuario:");
            gridEncabezado.Columns.Add("Fecha Registro:");
            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Código:"] = txtId.Text;
            if (CBallProcess.Checked)
                rowEncabezado["Macroproceso:"] = "Todos los procesos";
            else
                rowEncabezado["Macroproceso:"] = Session["NombreProceso"].ToString();

            rowEncabezado["Cargo:"] = tbxResponsable.Text;
            rowEncabezado["Actividad:"] = txtActividad.Text;
            rowEncabezado["Fecha Programación Actividad:"] = TXfechaProg.Text;
            rowEncabezado["Fecha Cumplimiento Actividad:"] = TXfechaCump.Text;
            rowEncabezado["Observaciones:"] = TXobservaciones.Text;
            rowEncabezado["Usuario:"] = tbxUsuarioCreacion.Text;
            rowEncabezado["Fecha Registro:"] = txtFecha.Text;
            gridEncabezado.Rows.Add(rowEncabezado);

           
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado,"ControlInfraestructura");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\""+filename+".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        protected void GVcontrolInfraestructura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            string strErrMsg = "";
            mtdLoadControlInfraestructura(ref strErrMsg);
        }

        protected void CBallProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (CBallProcess.Checked)
            {
                ddlCadenaValor.ClearSelection();
                ddlCadenaValor.Enabled = false;
                rfvCadenaValor.Enabled = false;
                ddlMacroproceso.ClearSelection();
                ddlMacroproceso.Enabled = false;
                rfvMacroProceso.Enabled = false;
            }
            else
            {
                ddlCadenaValor.Enabled = true;
                rfvCadenaValor.Enabled = true;
                
                ddlMacroproceso.Enabled = true;
                rfvMacroProceso.Enabled = true;
            }
        }
    }
}