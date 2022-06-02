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

using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using System.Configuration;
using ClosedXML.Excel;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ListasSarlaft.UserControls.Reportes
{
    public partial class RPTreporteDocumentosVigentes : System.Web.UI.UserControl
    {
        string IdFormulario = "4033";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            //scriptManager.RegisterPostBackControl(GVversiones);
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
        private DataTable infoGridVersiones;
        private int rowGridVersiones;
        private int pagIndexVersiones;
        private DataTable infoGridFile;
        private int rowGridFile;
        private int pagIndexFile;

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
        private DataTable InfoGridVersiones
        {
            get
            {
                infoGridVersiones = (DataTable)ViewState["infoGridVersiones"];
                return infoGridVersiones;
            }
            set
            {
                infoGridVersiones = value;
                ViewState["infoGridVersiones"] = infoGridVersiones;
            }
        }

        private int RowGridVersiones
        {
            get
            {
                rowGridVersiones = (int)ViewState["rowGridVersiones"];
                return rowGridVersiones;
            }
            set
            {
                rowGridVersiones = value;
                ViewState["rowGridVersiones"] = rowGridVersiones;
            }
        }

        private int PagIndexVersiones
        {
            get
            {
                pagIndexVersiones = (int)ViewState["pagIndexVersiones"];
                return pagIndexVersiones;
            }
            set
            {
                pagIndexVersiones = value;
                ViewState["pagIndexVersiones"] = pagIndexVersiones;
            }
        }
        private DataTable InfoGridFile
        {
            get
            {
                infoGridFile = (DataTable)ViewState["infoGridFile"];
                return infoGridFile;
            }
            set
            {
                infoGridFile = value;
                ViewState["infoGridFile"] = infoGridFile;
            }
        }

        private int RowGridFile
        {
            get
            {
                rowGridFile = (int)ViewState["rowGridFile"];
                return rowGridFile;
            }
            set
            {
                rowGridFile = value;
                ViewState["rowGridFile"] = rowGridFile;
            }
        }

        private int PagIndexFile
        {
            get
            {
                pagIndexFile = (int)ViewState["pagIndexFile"];
                return pagIndexFile;
            }
            set
            {
                pagIndexFile = value;
                ViewState["pagIndexFile"] = pagIndexFile;
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
            /* mtdCargarDDLs();
             PopulateTreeView();
             if (!mtdLoadControlVersion(ref strErrMsg))
                 omb.ShowMessage(strErrMsg, 1, "Atención");*/
            mtdLoadDDLTipo();
        }
        public void mtdLoadDDLTipo()
        {
            DDLtipo.Items.Clear();
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Documento", "1"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Registro", "2"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Eliminación", "3"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Procedimiento", "4"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Politica", "5"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Manual", "6"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Instructivo", "7"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Reglamento", "8"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Formato", "9"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Circular", "10"));
            DDLtipo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
        }

        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            int Tipo = Convert.ToInt32(DDLtipo.SelectedValue);
            string strErrMsg = string.Empty;
            if (mtdLoadReporteDocumentos(ref strErrMsg, Tipo) == false)
            {
                omb.ShowMessage("No hay información registrada para generar el reporte", 2, "Atención");
                BodyGridRD.Visible = false;
                Dbutton.Visible = false;
            }
            else
            {
                BodyGridRD.Visible = true;
                Dbutton.Visible = true;
            }
        }
        private bool mtdLoadReporteDocumentos(ref string strErrMsg, int IdTipoDocumento)
        {
            #region Vars
            bool booResult = false;
            clsReporteDocumentos objReporte = new clsReporteDocumentos();
            List<clsReporteDocumentos> lstReporte = new List<clsReporteDocumentos>();
            clsReporteDocumentoBLL cReporteDoc = new clsReporteDocumentoBLL();
            #endregion Vars
            lstReporte = cReporteDoc.mtdConsultarReporteDocumentos(ref strErrMsg, IdTipoDocumento);

            if (lstReporte != null)
            {
                mtdLoadReporteDocumentos();
                mtdLoadReporteDocumentos(lstReporte);
                GVcontrolDocumento.DataSource = lstReporte;
                GVcontrolDocumento.PageIndex = pagIndex;
                GVcontrolDocumento.DataBind();
                GVprint.DataSource = lstReporte;
                GVprint.DataBind();
                booResult = true;
                //BodyGridRCI.Visible = true;
                Dbutton.Visible = true;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadReporteDocumentos(List<clsReporteDocumentos> lstReporte)
        {
            string strErrMsg = String.Empty;
            clsReporteDocumentos cReporte = new clsReporteDocumentos();

            foreach (clsReporteDocumentos objReporte in lstReporte)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objReporte.intId.ToString().Trim(),
                    objReporte.strNombreDocumento.ToString().Trim(),
                    objReporte.strNombreArchivo.ToString().Trim(),
                    objReporte.strCodigoDocumento.ToString().Trim(),
                    objReporte.strFechaImplementacion.ToString().Trim(),
                    objReporte.strVersion.ToString().Trim(),
                    objReporte.strNombreProceso.ToString().Trim(),
                    objReporte.strNombreResponsable.ToString().Trim(),
                    objReporte.strFechaModificacion.ToString().Trim()
                    });
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadReporteDocumentos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreDocumento", typeof(string));
            grid.Columns.Add("strNombreArchivo", typeof(string));
            grid.Columns.Add("strCodigoDocumento", typeof(string));
            grid.Columns.Add("strFechaImplementacion", typeof(string));
            grid.Columns.Add("strVersion", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("strNombreResponsable", typeof(string));
            grid.Columns.Add("strFechaModificacion", typeof(string));


            GVcontrolDocumento.DataSource = grid;
            GVcontrolDocumento.DataBind();
            GVprint.DataSource = grid;
            GVprint.DataBind();
            InfoGrid = grid;
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridRD.Visible = false;
            Dbutton.Visible = false;
            DDLtipo.ClearSelection();
        }

        protected void GVcontrolDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVcontrolDocumento.PageIndex = PagIndex;
            GVcontrolDocumento.DataBind();
            string strErrMsg = "";
            int IdTipoDocumento = Convert.ToInt32(DDLtipo.SelectedValue);
            mtdLoadReporteDocumentos(ref strErrMsg, IdTipoDocumento);
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }
        private void mtdExportPdf()
        {
            // Creamos el tipo de Font que vamos utilizar
            Font titleFont = new Font(Font.HELVETICA, 10, Font.NORMAL, Color.WHITE);
            Font textFont = new Font(Font.HELVETICA, 8, Font.NORMAL, Color.BLACK);
            List<PdfPRow> pRows = new List<PdfPRow>();

            // Crea la informacion de documentos en el pdf
            PdfPTable pdfTable = new PdfPTable(GVcontrolDocumento.HeaderRow.Cells.Count);

            foreach (TableCell headerCell in GVcontrolDocumento.HeaderRow.Cells)
            {
                Font font = new Font
                {
                    Color = new Color(GVcontrolDocumento.HeaderStyle.ForeColor)
                };
                PdfPCell pdfCell = new PdfPCell(new Phrase(Context.Server.HtmlDecode(headerCell.Text), titleFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE };
                pdfCell.BackgroundColor = new Color(GVcontrolDocumento.HeaderStyle.BackColor);
                pdfTable.AddCell(pdfCell);
            }

            // Crea las filas de la tabla

            foreach (GridViewRow GridViewRow in GVcontrolDocumento.Rows)
            {

                string intId = ((Label)GridViewRow.FindControl("intId")).Text;
                string strNombreDocumento = ((Label)GridViewRow.FindControl("strNombreDocumento")).Text;
                string strNombreArchivo = ((Label)GridViewRow.FindControl("strNombreArchivo")).Text;
                string CodigoDocumento = ((Label)GridViewRow.FindControl("CodigoDocumento")).Text;
                string strFechaImplementacion = ((Label)GridViewRow.FindControl("strFechaImplementacion")).Text;
                string strVersion = ((Label)GridViewRow.FindControl("strVersion")).Text;
                string strNombreProceso = ((Label)GridViewRow.FindControl("strNombreProceso")).Text;
                string strNombreResponsable = ((Label)GridViewRow.FindControl("strNombreResponsable")).Text;
                string strFechaModificacion = ((Label)GridViewRow.FindControl("strFechaModificacion")).Text;
                List<PdfPCell> rowTable = new List<PdfPCell>();
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(intId), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strNombreDocumento), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strNombreArchivo), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(CodigoDocumento), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strFechaImplementacion), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strVersion), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strNombreProceso), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strNombreResponsable), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                rowTable.Add(new PdfPCell(new Phrase(Context.Server.HtmlDecode(strFechaModificacion), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                pRows.Add(new PdfPRow(rowTable.ToArray()));

            }
            pdfTable.Rows.AddRange(pRows);

            Document pdfDocument = new Document(PageSize.A4.Rotate(), 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Documentos y Registros Vigentes");
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
            Phrase phHeader = new Phrase();
            phHeader.Add(pdftblImage);
            HeaderFooter header = new HeaderFooter(phHeader, true);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Documentos y Registros Vigentes"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfTable);
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteDocumentosVigentes.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteDocumentosVigentes_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable grid = new DataTable();
            grid.Columns.Add("Código");
            grid.Columns.Add("Nombre");
            grid.Columns.Add("Nombre Archivo");
            grid.Columns.Add("Código Documento");
            grid.Columns.Add("Fecha Implementación");
            grid.Columns.Add("Versión");
            grid.Columns.Add("Nombre Proceso");
            grid.Columns.Add("Nombre Responsable");
            grid.Columns.Add("Fecha Modificación");
            DataRow row;
            foreach (GridViewRow GridViewRow in GVprint.Rows)
            {
                row = grid.NewRow();
                string intId = ((Label)GridViewRow.FindControl("intId")).Text;
                string strNombreDocumento = ((Label)GridViewRow.FindControl("strNombreDocumento")).Text;
                string strNombreArchivo = ((Label)GridViewRow.FindControl("strNombreArchivo")).Text;
                string CodigoDocumento = ((Label)GridViewRow.FindControl("CodigoDocumento")).Text;
                string strFechaImplementacion = ((Label)GridViewRow.FindControl("strFechaImplementacion")).Text;
                string strVersion = ((Label)GridViewRow.FindControl("strVersion")).Text;
                string strNombreProceso = ((Label)GridViewRow.FindControl("strNombreProceso")).Text;
                string strNombreResponsable = ((Label)GridViewRow.FindControl("strNombreResponsable")).Text;
                string strFechaModificacion = ((Label)GridViewRow.FindControl("strFechaModificacion")).Text;
                row["Código"] = intId;
                row["Nombre"] = strNombreDocumento;
                row["Nombre Archivo"] = strNombreArchivo;
                row["Código Documento"] = CodigoDocumento;
                row["Fecha Implementación"] = strFechaImplementacion;
                row["Versión"] = strVersion;
                row["Nombre Proceso"] = strNombreProceso;
                row["Nombre Responsable"] = strNombreResponsable;
                row["Fecha Modificación"] = strFechaModificacion;
                grid.Rows.Add(row);
            }
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            //workbook.Worksheets.Add(gridEncabezado, "Indicador");
            workbook.Worksheets.Add(grid, "DocumentosVigentes");
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