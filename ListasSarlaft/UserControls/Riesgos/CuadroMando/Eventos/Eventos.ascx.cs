using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
//using Microsoft.Office.Interop.Excel;
using System.Configuration;
using ClosedXML.Excel;
using Microsoft.Security.Application;
using System.Web.Services;
using ListasSarlaft.Classes.Utilerias;

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Eventos
{
    public partial class Eventos : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        string IdFormulario = "5034";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            /*scriptManager.RegisterPostBackControl(this.GVefectividad);
            scriptManager.RegisterPostBackControl(this.GVjerarquias);*/
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {

                }
            }
        }

        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMgs = string.Empty;
            if (ddlTipoReporte.SelectedValue == "1")
            {
                if (!LoadInfoReporteEstadosEventos(ref strErrMgs))
                {
                    omb.ShowMessage(strErrMgs, 2, "Atención");
                }else
                {
                    Dbutton.Visible = true;
                    dvGraficosEstadosEventos.Visible = true;
                    dvTablaEstadosEventos.Visible = true;
                }
                    
                
            }
            if(ddlTipoReporte.SelectedValue == "2")
            {
                if (!LoadInfoReporteEventosConsolidado(ref strErrMgs))
                {
                    omb.ShowMessage(strErrMgs, 2, "Atención");
                }else
                {
                    Dbutton.Visible = true;
                    dvGraficosEstadosEventos.Visible = false;
                    dvTablaEstadosEventos.Visible = false;
                    dvGraficoEventosConsolidado.Visible = true;
                }
                    
                
            }
        }


        #region Metodos
        private bool LoadInfoReporteEstadosEventos(ref string strErrMgs)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorDeficiente = 0;
            int valorRegular = 0;
            int valorBueno = 0;
            int valorExcelente = 0;
            List<clsDTOCuadroMandoEventos> lstReporte = new List<clsDTOCuadroMandoEventos>();
            clsBLLCuadroMandoEventos cCuadroRiesgos = new clsBLLCuadroMandoEventos();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoEventosFiltro objFiltros = new clsDTOCuadroMandoEventosFiltro();
            /**********************Filtros de Consulta****************************/
            if (txbFechaInicial.Text != "")
            {
                objFiltros.dtFechaInicial = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbFechaInicial.Text));
            }
            if(txbFechaFinal.Text != "")
                objFiltros.dtFechaFinal = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbFechaFinal.Text));
            /**********************Filtros  de Consulta****************************/
            int total = 0;
            booResult = cCuadroRiesgos.LoadInfoReporteEstadosEventos(ref strErrMgs, ref lstReporte, objFiltros, ref total);
            //string ListaEficacia = string.Empty;

            if (lstReporte != null)
            {
                System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                DataColumn dcColumn;

                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Estado";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Cantidad";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Participacion";
                dtCuadroMando.Columns.Add(dcColumn);
                double CantEventos = 0;
                bool Flag = cCuadroRiesgos.GetAllEventos(ref strErrMgs, ref CantEventos);
                Session["CantEventos"] = CantEventos;
                foreach (clsDTOCuadroMandoEventos objCuadro in lstReporte)
                {
                    DataRow dr;
                    dr = dtCuadroMando.NewRow();
                    dr["Estado"] = objCuadro.strDescripcionEstado;
                    dr["Cantidad"] = objCuadro.intNumEventos;
                    double Participacion = Convert.ToDouble(objCuadro.intNumEventos / total) * 100;
                    double d = Math.Round(Participacion, 2);
                    dr["Participacion"] = d;
                    dtCuadroMando.Rows.Add(dr);
                }

                mtdViewChartEstadosEventos(dtCuadroMando); 
            }else
            {
                strErrMgs = "No hay datos para generar el reporte";
            }

            return booResult;
        }
        private bool LoadInfoReporteEventosConsolidado(ref string strErrMsg)
        {
            bool booResult = false;
            //string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorRegistro = 0;
            int valorNoEvento = 0;
            
            List<clsDTOCuadroMandoEventos> lstReporte = new List<clsDTOCuadroMandoEventos>();
            clsBLLCuadroMandoEventos cCuadroRiesgos = new clsBLLCuadroMandoEventos();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            clsDTOCuadroMandoEventosFiltro objFiltros = new clsDTOCuadroMandoEventosFiltro();
            /**********************Filtros de Consulta****************************/
            if(txbFechaInicial.Text != "")
            objFiltros.dtFechaInicial = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbFechaInicial.Text));
            if(txbFechaFinal.Text != "")
            objFiltros.dtFechaFinal = Convert.ToDateTime(Sanitizer.GetSafeHtmlFragment(txbFechaFinal.Text));
            /**********************Filtros  de Consulta****************************/
            booResult = cCuadroRiesgos.LoadInfoReporteEventosConsolidado(ref strErrMsg, ref lstReporte, objFiltros);
            //string ListaEficacia = string.Empty;

            if (lstReporte != null)
            {
                System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                DataColumn dcColumn;

                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Registro";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Cantidad";
                dtCuadroMando.Columns.Add(dcColumn);
                /*dcColumn = new DataColumn();
                dcColumn.ColumnName = "Participacion";
                dtCuadroMando.Columns.Add(dcColumn);*/
                double CantEventos = 0;
                bool Flag = cCuadroRiesgos.GetAllEventos(ref strErrMsg, ref CantEventos);
                Session["CantEventos"] = CantEventos;
                foreach (clsDTOCuadroMandoEventos objCuadro in lstReporte)
                {
                    if(objCuadro.strRegistro == "RegistroEvento")
                        valorRegistro ++;
                    if (objCuadro.strRegistro == "No Hubo Eventos")
                        valorNoEvento ++;
                    /*DataRow dr;
                    dr = dtCuadroMando.NewRow();
                    dr["Estado"] = objCuadro.strDescripcionEstado;
                    dr["Cantidad"] = objCuadro.intNumEventos;
                    double Participacion = Convert.ToDouble(objCuadro.intNumEventos / CantEventos) * 100;
                    double d = Math.Round(Participacion, 2);
                    dr["Participacion"] = d;
                    dtCuadroMando.Rows.Add(dr);*/
                }
                DataRow dr;
                dr = dtCuadroMando.NewRow();
                dr["Registro"] = "Registro Evento";
                dr["Cantidad"] = valorRegistro;
                dtCuadroMando.Rows.Add(dr);
                dr = dtCuadroMando.NewRow();
                dr["Registro"] = "No Hubo Eventos";
                dr["Cantidad"] = valorNoEvento;
                dtCuadroMando.Rows.Add(dr);
                mtdViewChartEventosConsolidado(dtCuadroMando);
            }else
            {
                strErrMsg = "No hay datos para generar el reporte";
            }

            return booResult;
        }
        #endregion
        #region Generacion Graficos
        public void mtdViewChartEstadosEventos(System.Data.DataTable dtInfo)
        {
            GVestadosEventos.DataSource = dtInfo;
            GVestadosEventos.DataBind();
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
            }
            ChartEstadosEventos.Series[0].Points.DataBindXY(x, y);
            ChartEstadosEventos.Series[0].Name = "Estados Eventos";
            ChartEstadosEventos.Series[0].XValueMember = "Estado";
            ChartEstadosEventos.Series[0].YValueMembers = "Cantidad";
            ChartEstadosEventos.Series[0].ChartType = SeriesChartType.Pie;
            ChartEstadosEventos.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            ChartEstadosEventos.Legends[0].Enabled = true;
            //ChartEficacia.Titles.Add("NewTitle");
            ChartEstadosEventos.Titles[0].Text = "Eventos Totales: " + Total;
            /*foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartEstadosEventos.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Excelente": point.Color = System.Drawing.Color.Green; break;
                        case "Bueno": point.Color = System.Drawing.Color.Yellow; break;
                        case "Regular": point.Color = System.Drawing.Color.Orange; break;
                        case "Deficiente": point.Color = System.Drawing.Color.Red; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", Math.Round((point.YValues[0] / Convert.ToInt32(Session["TotalControles"].ToString())) * 100, 2) + "%", point.AxisLabel);

                }
            }*/
        }
        public void mtdViewChartEventosConsolidado(System.Data.DataTable dtInfo)
        {
            /*GVestadosEventos.DataSource = dtInfo;
            GVestadosEventos.DataBind();*/
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
            }
            ChartEventosConsolidado.Series[0].Points.DataBindXY(x, y);
            ChartEventosConsolidado.Series[0].Name = "Eventos Consolidado";
            ChartEventosConsolidado.Series[0].XValueMember = "Registro";
            ChartEventosConsolidado.Series[0].YValueMembers = "Cantidad";
            ChartEventosConsolidado.Series[0].ChartType = SeriesChartType.Column;
            ChartEventosConsolidado.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            ChartEventosConsolidado.Legends[0].Enabled = true;
            //ChartEficacia.Titles.Add("NewTitle");
            ChartEventosConsolidado.Titles[0].Text = "Eventos Totales: " + Total;
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartEventosConsolidado.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Registro Evento": point.Color = System.Drawing.Color.Green; break;
                        case "No Hubo Eventos": point.Color = System.Drawing.Color.Red; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
        }
        #endregion Generacion Graficos

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue == "1")
                LoadInfoReporteEstadosEventos(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "2")
                LoadInfoReporteEventosConsolidado(ref strErrMsg);

                /*
                if (ddlTipoReporte.SelectedValue == "3")
                    mtdLoadReporteCausasSinControl();
                if (ddlTipoReporte.SelectedValue == "4")
                    LoadInfoReporteRiesgosPlanes();*/
            // Creamos el tipo de Font que vamos utilizar
            Tools tools = new Tools();
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Cuadro de Mando Eventos");
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
            pdfcellImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdfcellImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();
            phHeader.Add(pdftblImage);
            #region Tabla de Datos Principales
            iTextSharp.text.Font font1 = new iTextSharp.text.Font();
            font1.Color = Color.WHITE;
            PdfPTable pdfTableData = new PdfPTable(2);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Tipo Reporte:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlTipoReporte.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Inicial:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaInicial.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Final:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaFinal.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            PdfPTable pdfpTable = new PdfPTable(2);
            if (ddlTipoReporte.SelectedValue == "1")
            {
                pdfpTable = new PdfPTable(GVestadosEventos.HeaderRow.Cells.Count);

                foreach (TableCell headerCell in GVestadosEventos.HeaderRow.Cells)
                {
                    iTextSharp.text.Font font = new iTextSharp.text.Font();
                    font.Color = new Color(GVestadosEventos.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                    pdfCell.BackgroundColor = new Color(GVestadosEventos.HeaderStyle.BackColor);
                    pdfpTable.AddCell(pdfCell);
                }

                foreach (GridViewRow GridViewRow in GVestadosEventos.Rows)
                {
                    foreach (TableCell tableCell in GridViewRow.Cells)
                    {
                        iTextSharp.text.Font font = new iTextSharp.text.Font();
                        font.Color = new Color(GVestadosEventos.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                        pdfCell.BackgroundColor = new Color(GVestadosEventos.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                }
            }
            
            #endregion
            MemoryStream streamControles = new MemoryStream();
            //MemoryStream streamRiesgosSarlaft = new MemoryStream();
            if (ddlTipoReporte.SelectedValue == "1")
            {
                ChartEstadosEventos.SaveImage(streamControles, ChartImageFormat.Png);
            }
            if(ddlTipoReporte.SelectedValue == "2")
            {
                ChartEventosConsolidado.SaveImage(streamControles, ChartImageFormat.Png);
            }
            
            PdfPTable pdftblImageGraficoControles = new PdfPTable(1);
            
            
                iTextSharp.text.Image imagenGraficoControles = iTextSharp.text.Image.GetInstance(streamControles.GetBuffer());
                //iTextSharp.text.Image imagenGraficoSarlaft = iTextSharp.text.Image.GetInstance(streamRiesgosSarlaft.GetBuffer());

                PdfPCell pdfcellImageGraficoControles = new PdfPCell(imagenGraficoControles, true);
                //PdfPCell pdfcellImageGraficoRiesgosSarlaft = new PdfPCell(imagenGraficoSarlaft, true);
                pdftblImageGraficoControles.AddCell(pdfcellImageGraficoControles);
                //pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoControles);
            
            
            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase(ddlTipoReporte.SelectedItem.Text));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfpTable);
            pdfDocument.Add(Chunk.NEWLINE);


            

                pdfDocument.Add(pdftblImageGraficoControles);
                pdfDocument.Add(Chunk.NEWLINE);
                
            
            
            
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CuadroMandoEventos.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "Reporte" + ddlTipoReporte.SelectedItem.Text + "_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {
            string strErrMsg = string.Empty;
            if (ddlTipoReporte.SelectedValue == "1")
                LoadInfoReporteEstadosEventos(ref strErrMsg);
            if (ddlTipoReporte.SelectedValue == "2")
                LoadInfoReporteEventosConsolidado(ref strErrMsg);
            string tmpChartNameControl = "Grafico" + ddlTipoReporte.SelectedValue + ".jpg";
            string imgPathControl = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartNameControl;

            if (ddlTipoReporte.SelectedValue == "1")
            {
                ChartEstadosEventos.SaveImage(imgPathControl);
            }
            if (ddlTipoReporte.SelectedValue == "2")
                ChartEventosConsolidado.SaveImage(imgPathControl);

            string imgPathCotrolLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameControl);
            //string imgPathSarlaftLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameRiesgosSarlaft);
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Tipo reporte:");
            gridEncabezado.Columns.Add("Fecha Inicial:");
            gridEncabezado.Columns.Add("Fecha Final:");


            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Tipo reporte:"] = ddlTipoReporte.SelectedItem.Text;
            rowEncabezado["Fecha Inicial:"] = txbFechaInicial.Text;
            rowEncabezado["Fecha Final:"] = txbFechaFinal.Text;
            System.Data.DataTable gridEvaValue = new System.Data.DataTable();
            if (ddlTipoReporte.SelectedValue == "1")
            {
                
                gridEvaValue.Columns.Add("Estado:");
                gridEvaValue.Columns.Add("Cantidad:");
                gridEvaValue.Columns.Add("Participación:");
                DataRow rowEvaValue;
                foreach (GridViewRow GridViewRow in GVestadosEventos.Rows)
                {
                    rowEvaValue = gridEvaValue.NewRow();

                    rowEvaValue["Estado:"] = GridViewRow.Cells[0].Text;
                    rowEvaValue["Cantidad:"] = GridViewRow.Cells[1].Text;
                    rowEvaValue["Participación:"] = GridViewRow.Cells[2].Text;
                    gridEvaValue.Rows.Add(rowEvaValue);
                }

                
            }
            gridEncabezado.Rows.Add(rowEncabezado);
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=CuadroMandoEventos.xls;");
            string tab = "";
            foreach (DataColumn dc in gridEncabezado.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in gridEncabezado.Rows)
            {
                tab = "";
                for (i = 0; i < gridEncabezado.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            string TableEventos = @"<Table><tr>";
            foreach (DataColumn dc in gridEvaValue.Columns)
            {
                TableEventos += @"<td>" + dc.ColumnName.ToString() + @"</td>";
            }
            TableEventos += @"</tr><tr>";
            //Response.Write("\n");

            foreach (DataRow dr in gridEvaValue.Rows)
            {
                tab = "";
                for (i = 0; i < gridEvaValue.Columns.Count; i++)
                {
                    TableEventos += @"<td>"+ dr[i].ToString() +@"</td>";
                }
                TableEventos += @"</tr><tr>";
            }
            TableEventos += @"</tr></table>";
            StringWriter stringWriteEventos = new StringWriter();
            HtmlTextWriter htmlWriteEventos = new HtmlTextWriter(stringWriteEventos);
            Response.Write(TableEventos);
            Response.Write(stringWriteEventos.ToString());
            Response.Write("\n");
            Response.Write("\n");
            

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathCotrolLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
                
            
            
            Response.End();

        }
        public void mtdRest()
        {
            Dbutton.Visible = false;
            dvGraficoEventosConsolidado.Visible = false;
            dvGraficosEstadosEventos.Visible = false;
            dvTablaEstadosEventos.Visible = false;

            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            GVestadosEventos.DataSource = null;
            GVestadosEventos.DataBind();
        }
        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            Dbutton.Visible = false;
            dvGraficoEventosConsolidado.Visible = false;
            dvGraficosEstadosEventos.Visible = false;
            dvTablaEstadosEventos.Visible = false;

            ddlTipoReporte.SelectedIndex = 0;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            GVestadosEventos.DataSource = null;
            GVestadosEventos.DataBind();
        }

        protected void ImbClean_Click(object sender, ImageClickEventArgs e)
        {
            Dbutton.Visible = false;
            dvGraficoEventosConsolidado.Visible = false;
            dvGraficosEstadosEventos.Visible = false;
            dvTablaEstadosEventos.Visible = false;

            ddlTipoReporte.SelectedIndex = 0;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            GVestadosEventos.DataSource = null;
            GVestadosEventos.DataBind();
        }

        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            mtdRest();
        }
    }
}