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
    public partial class ReporteIndicadoresMetasIncumplidas : System.Web.UI.UserControl
    {
        string IdFormulario = "4036";
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
            if (mtdLoadIndicador(ref strErrMsg, ref fechaInicial, ref fechaFinal) == false)
                omb.ShowMessage("No hay información registrada para generar el reporte", 2, "Atención");
            else
                BodyGridRIMC.Visible = true;
        }
        private bool mtdLoadIndicador(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            #region Vars
            bool booResult = false;
            clsMatrizIndicadores objMatriz = new clsMatrizIndicadores();
            List<clsMatrizIndicadores> lstMatriz = new List<clsMatrizIndicadores>();
            clsBLLMatrizIndicadores cMatriz = new clsBLLMatrizIndicadores();
            #endregion Vars
            lstMatriz = cMatriz.mtdConsultarIndicadorIncumplido(ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (lstMatriz != null && lstMatriz.Count > 0)
            {

                /*foreach (clsMatrizIndicadores objMatriz2 in lstMatriz)
                {
                    DataTable cuadro = objMatriz.dtCuadro;
                    
                    if (objMatriz2.intMetasCumplidas > 0)
                    {*/
                mtdLoadIndicador();
                mtdLoadIndicador(lstMatriz);
                GVIndicadoresMetasIncumplidas.DataSource = lstMatriz;
                GVIndicadoresMetasIncumplidas.PageIndex = pagIndex1;
                GVIndicadoresMetasIncumplidas.DataBind();
                booResult = true;
                Dbutton.Visible = true;
                //}
                //}
            }
            else
                booResult = false;

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadIndicador()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strPoliticaCalidad", typeof(string));
            grid.Columns.Add("intIdIndicador", typeof(string));
            //grid.Columns.Add("strDescripcionObjetivo", typeof(string));
            grid.Columns.Add("strNombreIndicador", typeof(string));
            grid.Columns.Add("strFormula", typeof(string));
            //grid.Columns.Add("strDescripcionInicador", typeof(string));
            grid.Columns.Add("strProcesoIndicador", typeof(string));
            grid.Columns.Add("intMeta", typeof(string));
            grid.Columns.Add("strNombrePeriodo", typeof(string));
            grid.Columns.Add("intMetasIncumplidas", typeof(string));
            grid.Columns.Add("intIdPeriodicidad", typeof(string));
            grid.Columns.Add("intIdProcesoIndicador", typeof(string));
            grid.Columns.Add("strArrayPeriodo", typeof(string));
            grid.Columns.Add("strArrayResultado", typeof(string));

            GVIndicadoresMetasIncumplidas.DataSource = grid;
            GVIndicadoresMetasIncumplidas.DataBind();
            InfoGrid1 = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadIndicador(List<clsMatrizIndicadores> lstMatriz)
        {
            string strErrMsg = String.Empty;

            foreach (clsMatrizIndicadores objMatriz in lstMatriz)
            {
                /*DataTable cuadro = objMatriz.dtCuadro;
                */
                //if (objMatriz.intMetasCumplidas > 0)
                //{
                InfoGrid1.Rows.Add(new Object[] {
                    objMatriz.strPoliticaCalidad,
                objMatriz.intIdIndicador,
                //objMatriz.strDescripcionObjetivo,
                objMatriz.strNombreIndicador,
                objMatriz.strFormula,
                //objMatriz.strDescripcionInicador,
                objMatriz.strProcesoIndicador,
                objMatriz.intMeta,
                objMatriz.strNombrePeriodo,
                objMatriz.intMetasIncumplidas,
                objMatriz.intIdPeriodicidad,
                objMatriz.intIdProcesoIndicador,
                objMatriz.strArrayPeriodo,
                objMatriz.strArrayResultado
                    });
                //}
            }
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            TXfechaInicial.Text = "";
            TXfechaFinal.Text = "";
            BodyGridRIMC.Visible = false;
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

            Document pdfDocument = new Document(PageSize.A4.Rotate(), 1, 1, 5, 20);
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
            /*Font font1 = new Font();
            font1.Color = new Color(GVCumplimiento.HeaderStyle.ForeColor);
            Color BackColor = new Color(GVCumplimiento.HeaderStyle.BackColor);
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Politica de Calidad:", font1));
            pdfCellEncabezado.BackgroundColor = new Color(GVCumplimiento.HeaderStyle.BackColor);
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXpolitica.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Objetico de Calidad:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxCabObjetivo.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Nombre Indicador:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXindicador.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Formula:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxCabFormula.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Descripcion Indicador:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXdescripcion.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Proceso:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxCabProceso.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Meta:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxCabMeta.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Periodo:", font1));
            pdfCellEncabezado.BackgroundColor = BackColor;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(tbxCabFrecuencia.Text));
            pdfTableData.AddCell(pdfCellEncabezado);*/

            #region ImprimirGrilla 
            Tools tools = new Tools();
            PdfPTable pdfpTableCumplimiento = tools.createPdftable(GVIndicadoresMetasIncumplidas);



            foreach (GridViewRow GridViewRow in GVIndicadoresMetasIncumplidas.Rows)
            {
                //string strPoliticaCalidad = ((Label)GridViewRow.FindControl("strPoliticaCalidad")).Text;
                string intIdIndicador = ((Label)GridViewRow.FindControl("intIdIndicador")).Text;
                //string strDescripcionObjetivo = ((Label)GridViewRow.FindControl("strDescripcionObjetivo")).Text;
                string strNombreIndicador = ((Label)GridViewRow.FindControl("strNombreIndicador")).Text;
                //string strFormula = ((Label)GridViewRow.FindControl("strFormula")).Text;
                //string strDescripcionInicador = ((Label)GridViewRow.FindControl("strDescripcionInicador")).Text;
                string strProcesoIndicador = ((Label)GridViewRow.FindControl("strProcesoIndicador")).Text;
                string intMeta = ((Label)GridViewRow.FindControl("intMeta")).Text;
                //string strNombrePeriodo = ((Label)GridViewRow.FindControl("strNombrePeriodo")).Text;
                string intMetasCumplidas = ((Label)GridViewRow.FindControl("intMetasIncumplidas")).Text;
                string strArrayPeriodo = ((Label)GridViewRow.FindControl("strArrayPeriodo")).Text;
                string strArrayResultado = ((Label)GridViewRow.FindControl("strArrayResultado")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intIdIndicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    /*if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcionObjetivo));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }*/
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strNombreIndicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    /*if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcionInicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }*/
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strProcesoIndicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intMeta));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    /*if (iteracion == 6)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasCumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strArrayPeriodo));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasCumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }*/
                    if (iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intMetasCumplidas));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strArrayPeriodo));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 6)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresMetasIncumplidas.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strArrayResultado));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresMetasIncumplidas.RowStyle.BackColor);
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

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Indicadores Metas Incumplidas"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            //pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableCumplimiento);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteIndicadoresMetasIncumplidas.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteIndicadoresMetasIncumplidas_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable grid = new DataTable();
            grid.Columns.Add("Id Indicador");
            //grid.Columns.Add("Descripcion Objetivo");
            grid.Columns.Add("Nombre Indicador");
            //grid.Columns.Add("Descripcion Inicador");
            grid.Columns.Add("Meta");
            grid.Columns.Add("Proceso Indicador");
            grid.Columns.Add("Metas Incumplidas");
            grid.Columns.Add("Periodo");
            grid.Columns.Add("Resultado");
            DataRow row;
            foreach (GridViewRow GridViewRow in GVIndicadoresMetasIncumplidas.Rows)
            {
                row = grid.NewRow();
                string intIdIndicador = ((Label)GridViewRow.FindControl("intIdIndicador")).Text;
                //string strDescripcionObjetivo = ((Label)GridViewRow.FindControl("strDescripcionObjetivo")).Text;
                string strNombreIndicador = ((Label)GridViewRow.FindControl("strNombreIndicador")).Text;
                //string strFormula = ((Label)GridViewRow.FindControl("strFormula")).Text;
                //string strDescripcionInicador = ((Label)GridViewRow.FindControl("strDescripcionInicador")).Text;
                string strProcesoIndicador = ((Label)GridViewRow.FindControl("strProcesoIndicador")).Text;
                string intMeta = ((Label)GridViewRow.FindControl("intMeta")).Text;
                //string strNombrePeriodo = ((Label)GridViewRow.FindControl("strNombrePeriodo")).Text;
                string intMetasCumplidas = ((Label)GridViewRow.FindControl("intMetasIncumplidas")).Text;
                string strArrayPeriodo = ((Label)GridViewRow.FindControl("strArrayPeriodo")).Text;
                string strArrayResultado = ((Label)GridViewRow.FindControl("strArrayResultado")).Text;
                row["Id Indicador"] = intIdIndicador;
                //row["Descripcion Objetivo"] = strDescripcionObjetivo;
                row["Nombre Indicador"] = strNombreIndicador;
                //row["Descripcion Inicador"] = strDescripcionInicador;
                row["Proceso Indicador"] = strProcesoIndicador;
                row["Meta"] = intMeta;
                row["Metas Incumplidas"] = intMetasCumplidas;
                row["Periodo"] = strArrayPeriodo;
                row["Resultado"] = strArrayResultado;
                grid.Rows.Add(row);
            }
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            //workbook.Worksheets.Add(gridEncabezado, "Indicador");
            workbook.Worksheets.Add(grid, "Indicador Metas Incumplidas");
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
        protected void GVIndicadoresMetasIncumplidas_PreRender(object sender, EventArgs e)
        {
            MergeRowsMatrizData(GVIndicadoresMetasIncumplidas);
        }
        public void MergeRowsMatrizData(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    //string text = ((Label)row.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;

                    //string previousText = ((Label)previousRow.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;
                    /*if (cellIndex == 0)
                    {
                        string text = ((Label)row.FindControl("strPoliticaCalidad")).Text;
                        string previousText = ((Label)previousRow.FindControl("strPoliticaCalidad")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }*/
                    /*if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("strDescripcionObjetivo")).Text;
                        string previousText = ((Label)previousRow.FindControl("strDescripcionObjetivo")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }*/
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("strNombreIndicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("strNombreIndicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    /*if (cellIndex == 3)
                    {
                        string text = ((Label)row.FindControl("strDescripcionInicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("strDescripcionInicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }*/
                    if (cellIndex == 2)
                    {
                        string text = ((Label)row.FindControl("strProcesoIndicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("strProcesoIndicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    /*if (cellIndex == 6)
                    {
                        string text = ((Label)row.FindControl("strNombrePeriodo")).Text;
                        string previousText = ((Label)previousRow.FindControl("strNombrePeriodo")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }*/

                }
            }
        }
    }
}