using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Proceso.Reportes
{
    public partial class ReporteSeguimientoPlanFormacion : System.Web.UI.UserControl
    {
        string IdFormulario = "4038";
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
        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string fechaInicial = TXfechaInicial.Text;
            string fechaFinal = TXfechaFinal.Text;
            if (mtdLoadControlInfraestructura(ref strErrMsg, ref fechaInicial, ref fechaFinal) == false)
                omb.ShowMessage("No hay información registrada para generar el reporte", 2, "Atención");
            else
            {
                BodyGridRSPC.Visible = true;
                Dbutton.Visible = true;
            }
        }
        private bool mtdLoadControlInfraestructura(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            clsControlInfraestructura objCrlInfra = new clsControlInfraestructura();
            List<clsProgramacionCapacitacion> lstPrograma = new List<clsProgramacionCapacitacion>();
            clsProgramaCapacitacionBLL cPrograma = new clsProgramaCapacitacionBLL();
            #endregion Vars
            lstPrograma = cPrograma.mtdConsultarReportePrograma(ref lstPrograma, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (lstPrograma != null)
            {
                mtdLoadGridControlInfraestructura();
                mtdLoadGridControlInfraestructura(lstPrograma);
                GVReporteprogramaCapacitacion.DataSource = lstPrograma;
                GVReporteprogramaCapacitacion.PageIndex = pagIndex1;
                GVReporteprogramaCapacitacion.DataBind();
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

            grid.Columns.Add("strActividad", typeof(string));
            //grid.Columns.Add("IdMacroproceso", typeof(string));
            //grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("dtFechaProgramada", typeof(string));
            grid.Columns.Add("intEvaluacion", typeof(string));
            grid.Columns.Add("strCargoResponsable", typeof(string));

            GVReporteprogramaCapacitacion.DataSource = grid;
            GVReporteprogramaCapacitacion.DataBind();
            InfoGrid1 = grid;
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

                InfoGrid1.Rows.Add(new Object[] {
                    objEvaComp.strActividad.ToString().Trim(),
                    objEvaComp.dtFechaProgramada.ToString().Trim(),
                    objEvaComp.intEvaluacion.ToString().Trim(),
                    objEvaComp.strCargoResponsable.ToString().Trim()
                    });
            }
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            TXfechaInicial.Text = "";
            TXfechaFinal.Text = "";
            BodyGridRSPC.Visible = false;
            Dbutton.Visible = false;
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
            pdfDocument.AddTitle("Reporte de Seguimiento Plan de Formación");
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
            PdfPTable pdfpTableCumplimiento = tools.createPdftable(GVReporteprogramaCapacitacion);



            foreach (GridViewRow GridViewRow in GVReporteprogramaCapacitacion.Rows)
            {
                
                string strActividad = ((Label)GridViewRow.FindControl("actividad")).Text;
                string dtFechaProgramada = ((Label)GridViewRow.FindControl("dtFechaProgramada")).Text;
                string intEvaluacion = ((Label)GridViewRow.FindControl("intEvaluacion")).Text;
                string strCargoResponsable = ((Label)GridViewRow.FindControl("NombreHijo")).Text;
                
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteprogramaCapacitacion.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strActividad));
                        pdfCell.BackgroundColor = new Color(GVReporteprogramaCapacitacion.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteprogramaCapacitacion.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dtFechaProgramada));
                        pdfCell.BackgroundColor = new Color(GVReporteprogramaCapacitacion.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteprogramaCapacitacion.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intEvaluacion));
                        pdfCell.BackgroundColor = new Color(GVReporteprogramaCapacitacion.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVReporteprogramaCapacitacion.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strCargoResponsable));
                        pdfCell.BackgroundColor = new Color(GVReporteprogramaCapacitacion.RowStyle.BackColor);
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
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Seguimiento Plan de Formación"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            //pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableCumplimiento);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteSeguimientoPlanFormacion.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteIndicadoresMetasCumplidas_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable grid = new DataTable();
            grid.Columns.Add("Tema de Formación");
            grid.Columns.Add("Fecha Programada");
            grid.Columns.Add("Actividades Pendientes");
            grid.Columns.Add("Cargo Responsable");
            DataRow row;
            
            foreach (GridViewRow GridViewRow in GVReporteprogramaCapacitacion.Rows)
            {
                row = grid.NewRow();
                string strActividad = ((Label)GridViewRow.FindControl("actividad")).Text;
                string dtFechaProgramada = ((Label)GridViewRow.FindControl("dtFechaProgramada")).Text;
                string intEvaluacion = ((Label)GridViewRow.FindControl("intEvaluacion")).Text;
                string strCargoResponsable = ((Label)GridViewRow.FindControl("NombreHijo")).Text;

                row["Tema de Formación"] = strActividad;
                row["Fecha Programada"] = dtFechaProgramada;
                row["Actividades Pendientes"] = intEvaluacion;
                row["Cargo Responsable"] = strCargoResponsable;
                grid.Rows.Add(row);
            }
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            //workbook.Worksheets.Add(gridEncabezado, "Indicador");
            workbook.Worksheets.Add(grid, "Seguimiento Plan de Formación");
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