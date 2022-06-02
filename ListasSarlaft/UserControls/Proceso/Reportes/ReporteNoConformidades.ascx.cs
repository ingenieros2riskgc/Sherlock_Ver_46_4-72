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
using System.Net;

namespace ListasSarlaft.UserControls.Proceso.Reportes
{
    public partial class ReporteNoConformidades : System.Web.UI.UserControl
    {
        string IdFormulario = "4034";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            /*scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);*/
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            //scriptManager.RegisterPostBackControl(this.ImButtonDownload);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    //mtdStard();
                    mtdInicializarValores();

                }
            }
        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridControl;
        private int rowGridControl;
        private int pagIndexControl;

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
        private DataTable InfoGridControl
        {
            get
            {
                infoGridControl = (DataTable)ViewState["infoGridControl"];
                return infoGridControl;
            }
            set
            {
                infoGridControl = value;
                ViewState["infoGridControl"] = infoGridControl;
            }
        }

        private int RowGridControl
        {
            get
            {
                rowGridControl = (int)ViewState["rowGridControl"];
                return rowGridControl;
            }
            set
            {
                rowGridControl = value;
                ViewState["rowGridControl"] = rowGridControl;
            }
        }

        private int PagIndexControl
        {
            get
            {
                pagIndexControl = (int)ViewState["pagIndexControl"];
                return pagIndexControl;
            }
            set
            {
                pagIndexControl = value;
                ViewState["pagIndexControl"] = pagIndexControl;
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
            /*if (!mtdCargarDDLs(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            PopulateTreeView();*/
            /*if (!mtdLoadControlNoConformidad(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");*/

        }
        private bool mtdLoadControlNoConformidad(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            clsControlNoConformidad objCrlInfra = new clsControlNoConformidad();
            List<clsControlNoConformidad> lstCrlConformidad = new List<clsControlNoConformidad>();
            clsRegistroNoConformidadBLL cCrtno = new clsRegistroNoConformidadBLL();
            #endregion Vars
            lstCrlConformidad = cCrtno.mtdConsultarControlNoConformidadSinCierre(ref lstCrlConformidad, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (lstCrlConformidad != null)
            {
                mtdLoadControlNoConformidad();
                mtdLoadControlNoConformidad(lstCrlConformidad);
                GVcontrolNoConformidad.DataSource = lstCrlConformidad;
                GVcontrolNoConformidad.PageIndex = pagIndex;
                GVcontrolNoConformidad.DataBind();
                booResult = true;
                Dbutton.Visible = true;
            }else
            {
                strErrMsg = "No hay datos para generar el reporte";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadControlNoConformidad()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strProceso", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("dtFechaInicio", typeof(string));
            grid.Columns.Add("strSeguimiento", typeof(string));
            grid.Columns.Add("strCargoResponsable", typeof(string));
            grid.Columns.Add("strPathFile", typeof(string));

            GVcontrolNoConformidad.DataSource = grid;
            GVcontrolNoConformidad.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadControlNoConformidad(List<clsControlNoConformidad> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsControlNoConformidad objEvaComp in lstControl)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strProceso.ToString().Trim(),
                    objEvaComp.strDescripcion.ToString().Trim(),
                    objEvaComp.dtFechaInicio.ToString().Trim(),
                    objEvaComp.strSeguimiento.ToString().Trim(),
                    objEvaComp.strCargoResponsable.ToString().Trim(),
                    objEvaComp.strPathFile.ToString().Trim()
                    });
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

            Document pdfDocument = new Document(PageSize.A4.Rotate(), 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de No Conformidades");
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
            PdfPTable pdfpTableNoConformidad = tools.createPdftable(GVcontrolNoConformidad);



            foreach (GridViewRow GridViewRow in GVcontrolNoConformidad.Rows)
            {
                string Id = ((Label)GridViewRow.FindControl("intId")).Text;
                string Proceso = ((Label)GridViewRow.FindControl("proceso")).Text;
                string descipcion = ((Label)GridViewRow.FindControl("descipcion")).Text;
                string seguimiento = ((Label)GridViewRow.FindControl("seguimiento")).Text;
                string fechaInicio = ((Label)GridViewRow.FindControl("dtFechaInicio")).Text;
                string NombreHijo = ((Label)GridViewRow.FindControl("NombreHijo")).Text;
                string parthFile = ((Label)GridViewRow.FindControl("parthFile")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVcontrolNoConformidad.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(Id));
                        pdfCell.BackgroundColor = new Color(GVcontrolNoConformidad.RowStyle.BackColor);
                        pdfpTableNoConformidad.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVcontrolNoConformidad.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(Proceso));
                        pdfCell.BackgroundColor = new Color(GVcontrolNoConformidad.RowStyle.BackColor);
                        pdfpTableNoConformidad.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVcontrolNoConformidad.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(descipcion));
                        pdfCell.BackgroundColor = new Color(GVcontrolNoConformidad.RowStyle.BackColor);
                        pdfpTableNoConformidad.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVcontrolNoConformidad.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(fechaInicio));
                        pdfCell.BackgroundColor = new Color(GVcontrolNoConformidad.RowStyle.BackColor);
                        pdfpTableNoConformidad.AddCell(pdfCell);
                    }
                    if (iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVcontrolNoConformidad.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(seguimiento));
                        pdfCell.BackgroundColor = new Color(GVcontrolNoConformidad.RowStyle.BackColor);
                        pdfpTableNoConformidad.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVcontrolNoConformidad.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(NombreHijo));
                        pdfCell.BackgroundColor = new Color(GVcontrolNoConformidad.RowStyle.BackColor);
                        pdfpTableNoConformidad.AddCell(pdfCell);
                    }
                    if (iteracion == 6)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVcontrolNoConformidad.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(parthFile));
                        pdfCell.BackgroundColor = new Color(GVcontrolNoConformidad.RowStyle.BackColor);
                        pdfpTableNoConformidad.AddCell(pdfCell);
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

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de No Conformidades pendientes por Cierre"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfpTableNoConformidad);
            pdfDocument.Add(Chunk.NEWLINE);
            //pdfDocument.Add(pdfpTableCumplimiento);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteNoConformidadesSinCierre.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteNoConformidadesSinCierre_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            /*Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;*/
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg1 = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dgEncabezado = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dgData = new System.Web.UI.WebControls.DataGrid();
            System.Web.UI.WebControls.DataGrid dg2 = new System.Web.UI.WebControls.DataGrid();

            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Código");
            gridEncabezado.Columns.Add("Proceso");
            gridEncabezado.Columns.Add("Descripción");
            gridEncabezado.Columns.Add("Fecha Inicio");
            gridEncabezado.Columns.Add("Seguimiento");
            gridEncabezado.Columns.Add("Cargo Responsable");
            gridEncabezado.Columns.Add("Archivo Cargado");
            DataRow rowEncabezado;

            foreach (GridViewRow GridViewRow in GVcontrolNoConformidad.Rows)
            {
                rowEncabezado = gridEncabezado.NewRow();
                string Id = ((Label)GridViewRow.FindControl("intId")).Text;
                string Proceso = ((Label)GridViewRow.FindControl("proceso")).Text;
                string descipcion = ((Label)GridViewRow.FindControl("descipcion")).Text;
                string seguimiento = ((Label)GridViewRow.FindControl("seguimiento")).Text;
                string fechaInicio = ((Label)GridViewRow.FindControl("dtFechaInicio")).Text;
                string NombreHijo = ((Label)GridViewRow.FindControl("NombreHijo")).Text;
                string parthFile = ((Label)GridViewRow.FindControl("parthFile")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        rowEncabezado["Código"] = Id;
                    }
                    if (iteracion == 1)
                    {
                        rowEncabezado["Proceso"] = Proceso;
                    }
                    if (iteracion == 2)
                    {
                        rowEncabezado["Descripción"] = descipcion;
                    }
                    if (iteracion == 3)
                    {
                        rowEncabezado["Fecha Inicio"] = fechaInicio;
                    }
                    if (iteracion == 4)
                    {
                        rowEncabezado["Seguimiento"] = seguimiento;
                    }
                    if (iteracion == 5)
                    {
                        rowEncabezado["Cargo Responsable"] = NombreHijo;
                    }
                    if (iteracion == 6)
                    {
                        rowEncabezado["Archivo Cargado"] = parthFile;
                    }
                    iteracion++;
                }
                gridEncabezado.Rows.Add(rowEncabezado);
            }
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "ReporteNoConformidadesSinCierre");
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

        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string fechaInicial = TXfechaInicial.Text;
            string fechaFinal = TXfechaFinal.Text;
            if (mtdLoadControlNoConformidad(ref strErrMsg, ref fechaInicial, ref fechaFinal) == false)
                omb.ShowMessage(strErrMsg, 2, "Atención");
            else
                BodyGridRNC.Visible = true;
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            TXfechaInicial.Text = "";
            TXfechaFinal.Text = "";
            BodyGridRNC.Visible = false;
            Dbutton.Visible = false;
        }
    }
}