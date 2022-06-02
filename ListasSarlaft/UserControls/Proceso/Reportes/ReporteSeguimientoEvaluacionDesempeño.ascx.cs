using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Proceso.Reportes
{
    public partial class ReporteSeguimientoEvaluacionDesempeño : System.Web.UI.UserControl
    {
        string IdFormulario = "4039";
        cCuenta cCuenta = new cCuenta();
        #region Properties
        private DataTable infoGrid1;
        private int rowGrid1;
        private int pagIndex1;
        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;

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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.GVMatriz);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    //mtdStartLoad();
                    PopulateTreeView();
                }
            }
        }
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
            //txtNombreEva.Text = "";
            //TXfecharegistro.Text = "" + DateTime.Now;
            //tbxResponsable.Text = "";
            //TXjefe.Text = "";
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
            TXevaluado.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview
        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string fechaInicial = TXfechaInicial.Text;
            string fechaFinal = TXfechaFinal.Text;
            int Cargo = 0;
            if (lblIdDependencia4.Text != "")
                Cargo = Convert.ToInt32(lblIdDependencia4.Text);

            if (mtdCargarEvaDesempeño(ref strErrMsg, ref fechaInicial, ref fechaFinal, ref Cargo) == false)
                omb.ShowMessage("No hay información registrada para generar el reporte", 2, "Atención");
            else
            {
                BodyGridRSED.Visible = true;
                Dbutton.Visible = true;
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            TXfechaInicial.Text = "";
            TXfechaFinal.Text = "";
            TXevaluado.Text = "";
            BodyGridRSED.Visible = false;
            Dbutton.Visible = false;
        }
        #region Gridview Evaluacion Desempeño
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarEvaDesempeño(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal, ref int IdResponsable)
        {
            bool booResult = false;

            mtdLoadGridEvaDesempeño();
            booResult = mtdLoadInfoGridEvaDesempeño(ref strErrMsg, ref fechaInicial, ref fechaFinal, ref IdResponsable);

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridEvaDesempeño()
        {
            DataTable gridDesempeño = new DataTable();

            //gridDesempeño.Columns.Add("intId", typeof(string));
            gridDesempeño.Columns.Add("strNombre", typeof(string));
            //gridDesempeño.Columns.Add("intCargoResponsable", typeof(string));
            //gridDesempeño.Columns.Add("strCargo", typeof(string));
            //gridDesempeño.Columns.Add("dtFechaEvaluacion", typeof(string));
            //gridDesempeño.Columns.Add("strEvaluador", typeof(string));
            //gridDesempeño.Columns.Add("intCalificacion", typeof(string));
            gridDesempeño.Columns.Add("strRecomendacionCapacitacion", typeof(string));
            gridDesempeño.Columns.Add("strRecomendacionCompromisos", typeof(string));
            gridDesempeño.Columns.Add("strOtros", typeof(string));
            gridDesempeño.Columns.Add("strDescripcionOtros", typeof(string));
            gridDesempeño.Columns.Add("dtFechaProximaEvaluacion", typeof(string));
            //gridDesempeño.Columns.Add("strUsuario", typeof(string));
            //gridDesempeño.Columns.Add("dtFechaRegistro", typeof(string));

            GVReporteSeguimientoEvaDesempeño.DataSource = gridDesempeño;
            GVReporteSeguimientoEvaDesempeño.DataBind();
            InfoGrid1 = gridDesempeño;
        }
        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la evaluacion de desempeño al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridEvaDesempeño(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal, ref int IdResponsable)
        {
            bool booResult = false;
            List<clsEvaluacionDesempeno> lstEvaluacionDesempeño = new List<clsEvaluacionDesempeno>();
            clsValorEvaluacionDesempeñoBLL cEvaDesempeño = new clsValorEvaluacionDesempeñoBLL();
            clsEvaluacionDesempeno objEvaDempOut = new clsEvaluacionDesempeno();
            if(IdResponsable == 0)
                lstEvaluacionDesempeño = cEvaDesempeño.mtdConsultarReporteEvaluacionDesempeño(ref objEvaDempOut, ref strErrMsg, ref fechaInicial, ref fechaFinal);
            else
                lstEvaluacionDesempeño = cEvaDesempeño.mtdConsultarReporteEvaluacionDesempeñoResponsable(ref objEvaDempOut, ref strErrMsg, ref fechaInicial, ref fechaFinal, ref IdResponsable);

            if (lstEvaluacionDesempeño != null && lstEvaluacionDesempeño.Count > 0)
            {
                mtdLoadInfoGridDesempeño(lstEvaluacionDesempeño);
                GVReporteSeguimientoEvaDesempeño.DataSource = lstEvaluacionDesempeño;
                GVReporteSeguimientoEvaDesempeño.PageIndex = PagIndex1;
                GVReporteSeguimientoEvaDesempeño.DataBind();

                booResult = true;
            }else
                booResult = false;

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los Factores</param>
        private void mtdLoadInfoGridDesempeño(List<clsEvaluacionDesempeno> lstEvaluacionDesempeño)
        {
            foreach (clsEvaluacionDesempeno objEvaDesempeño in lstEvaluacionDesempeño)
            {
                InfoGrid1.Rows.Add(new Object[] {
                    //objEvaDesempeño.intId.ToString().Trim(),
                    objEvaDesempeño.strNombre.ToString().Trim(),
                    //objEvaDesempeño.intCargoResponsable.ToString().Trim(),
                    //objEvaDesempeño.strCargo.ToString().Trim(),
                    //objEvaDesempeño.dtFechaEvaluacion.ToString().Trim(),
                    //objEvaDesempeño.strEvaluador.ToString().Trim(),
                    //objEvaDesempeño.intCalificacion.ToString().Trim(),
                    objEvaDesempeño.strRecomendacionCapacitacion.ToString().Trim(),
                    objEvaDesempeño.strRecomendacionCompromisos.ToString().Trim(),
                    objEvaDesempeño.strOtros.ToString().Trim(),
                    objEvaDesempeño.strDescripcionOtros.ToString().Trim(),
                    objEvaDesempeño.dtFechaProximaEvaluacion.ToString().Trim()
                    //objEvaDesempeño.strUsuario.ToString().Trim(),
                    //objEvaDesempeño.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }
        private void mtdExportPdf()
        {
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4.Rotate(), 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte Seguimiento Evaluacion de Desempeño");
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
           

            #region ImprimirGrilla 
            Tools tools = new Tools();
            PdfPTable pdfpTableCumplimiento = tools.createPdftable(GVReporteSeguimientoEvaDesempeño);



            foreach (GridViewRow GridViewRow in GVReporteSeguimientoEvaDesempeño.Rows)
            {
                string NombreEvaluado = ((Label)GridViewRow.FindControl("NombreEvaluado")).Text;
                string strRecomendacionCapacitacion = ((Label)GridViewRow.FindControl("strRecomendacionCapacitacion")).Text;
                string strRecomendacionCompromisos = ((Label)GridViewRow.FindControl("strRecomendacionCompromisos")).Text;
                string strOtros = ((Label)GridViewRow.FindControl("strOtros")).Text;
                string strDescripcionOtros = ((Label)GridViewRow.FindControl("strDescripcionOtros")).Text;
                string dtFechaProximaEvaluacion = ((Label)GridViewRow.FindControl("dtFechaProximaEvaluacion")).Text;
                
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(NombreEvaluado));
                        pdfCell.BackgroundColor = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strRecomendacionCapacitacion));
                        pdfCell.BackgroundColor = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strRecomendacionCompromisos));
                        pdfCell.BackgroundColor = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strOtros));
                        pdfCell.BackgroundColor = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcionOtros));
                        pdfCell.BackgroundColor = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dtFechaProximaEvaluacion));
                        pdfCell.BackgroundColor = new Color(GVReporteSeguimientoEvaDesempeño.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    
                    iteracion++;
                }
            }
            #endregion ImprimirGrilla

            #endregion Tabla de Datos Principales

            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();


            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte Seguimiento Evaluación de Desempeño"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            //pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableCumplimiento);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteSeguimientoEvaluacionDesempeño.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteSeguimientoEvaluacionDesempeño_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable grid = new DataTable();
            grid.Columns.Add("Nombre Evaluado");
            grid.Columns.Add("Recomendación Capacitación");
            grid.Columns.Add("Recomendación Compromisos");
            grid.Columns.Add("Otros");
            grid.Columns.Add("Descripción Otros");
            grid.Columns.Add("Fecha Próxima Evaluación");
            DataRow row;
            foreach (GridViewRow GridViewRow in GVReporteSeguimientoEvaDesempeño.Rows)
            {
                row = grid.NewRow();
                string NombreEvaluado = ((Label)GridViewRow.FindControl("NombreEvaluado")).Text;
                string strRecomendacionCapacitacion = ((Label)GridViewRow.FindControl("strRecomendacionCapacitacion")).Text;
                string strRecomendacionCompromisos = ((Label)GridViewRow.FindControl("strRecomendacionCompromisos")).Text;
                string strOtros = ((Label)GridViewRow.FindControl("strOtros")).Text;
                string strDescripcionOtros = ((Label)GridViewRow.FindControl("strDescripcionOtros")).Text;
                string dtFechaProximaEvaluacion = ((Label)GridViewRow.FindControl("dtFechaProximaEvaluacion")).Text;
                row["Nombre Evaluado"] = NombreEvaluado;
                row["Recomendación Capacitación"] = strRecomendacionCapacitacion;
                row["Recomendación Compromisos"] = strRecomendacionCompromisos;
                row["Otros"] = strOtros;
                row["Descripción Otros"] = strDescripcionOtros;
                row["Fecha Próxima Evaluación"] = dtFechaProximaEvaluacion;
                grid.Rows.Add(row);
            }
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            //workbook.Worksheets.Add(gridEncabezado, "Indicador");
            workbook.Worksheets.Add(grid, "SeguimientoEvaDesempeño");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
    }
}