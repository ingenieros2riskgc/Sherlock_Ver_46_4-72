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
    public partial class ReporteInformacionGeneral : System.Web.UI.UserControl
    {
        string IdFormulario = "4040";
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
            if (mtdLoadInfoGeneral(ref strErrMsg, ref fechaInicial, ref fechaFinal) == false)
                omb.ShowMessage("No hay información registrada para generar el reporte", 2, "Atención");
            else
            {
                BodyGridRIG.Visible = true;
                Dbutton.Visible = true;
            }
        }
        #region Gridview Evaluacion Desempeño
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGeneral(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            bool booResult = false;

            
            booResult = mtdLoadGridInfoGeneral(ref strErrMsg, ref fechaInicial, ref fechaFinal);

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridInfoGeneral()
        {
            DataTable gridInfoGeneral = new DataTable();


            //gridInfoGeneral.Columns.Add("intIdRespuestaServicio", typeof(string));
            gridInfoGeneral.Columns.Add("intNumClientesEncuestados", typeof(string));
            gridInfoGeneral.Columns.Add("intNumClientesAprobados", typeof(string));
            //gridInfoGeneral.Columns.Add("intIdIndicador", typeof(string));
            //gridInfoGeneral.Columns.Add("strNombreIndicador", typeof(string));
            //gridInfoGeneral.Columns.Add("strDescripcionIndicador", typeof(string));
            gridInfoGeneral.Columns.Add("intNumMetasCumplidas", typeof(string));
            gridInfoGeneral.Columns.Add("intNumMetasIncumplidas", typeof(string));
            gridInfoGeneral.Columns.Add("intNumNoConformidad", typeof(string));
            gridInfoGeneral.Columns.Add("intNumNoConformidadCierre", typeof(string));
            gridInfoGeneral.Columns.Add("intNumAuditoria", typeof(string));
            gridInfoGeneral.Columns.Add("intNumAuditoriaCumplida", typeof(string));

            GVReporteInfoGeneral.DataSource = gridInfoGeneral;
            GVReporteInfoGeneral.DataBind();
            InfoGrid1 = gridInfoGeneral;
        }
        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la evaluacion de desempeño al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadGridInfoGeneral(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal)
        {
            bool booResult = false;
            List<clsInformacionGeneral> lstInfoGeneral = new List<clsInformacionGeneral>();
            clsInfoGeneralBLL cInfoGeneral = new clsInfoGeneralBLL();
            clsInformacionGeneral objInfoGeneralOut = new clsInformacionGeneral();
            lstInfoGeneral = cInfoGeneral.mtdConsultarReporteInformacionGeneral(ref objInfoGeneralOut, ref strErrMsg, ref fechaInicial, ref fechaFinal);

            if (lstInfoGeneral != null)
            {
                mtdLoadGridInfoGeneral();
                mtdLoadGridInfoGeneral(lstInfoGeneral, ref fechaInicial, ref fechaFinal);
                /*int valorNumclientesEncuestados = 0;
                int valorNumClienteAprovador = 0;
                int valorintNumMetasCumplidas = 0;
                int valorintNumMetasIncumplidas = 0;
                int valorintNumNoConformidad = 0;
                int valorintNumNoConformidadCierre = 0;
                int valorintNumAuditoria = 0;
                int valorintNumAuditoriaCumplida = 0;
                foreach (clsInformacionGeneral objEvaDesempeño in lstInfoGeneral)
                {
                    valorNumclientesEncuestados = valorNumclientesEncuestados + objEvaDesempeño.intNumClientesEncuestados;
                    valorNumClienteAprovador = valorNumClienteAprovador + objEvaDesempeño.intNumClientesAprobados;
                    valorintNumMetasCumplidas = valorintNumMetasCumplidas + objEvaDesempeño.intNumMetasCumplidas;
                    valorintNumMetasIncumplidas = valorintNumMetasIncumplidas + objEvaDesempeño.intNumMetasIncumplidas;
                    valorintNumNoConformidad = valorintNumNoConformidad + objEvaDesempeño.intNumNoConformidad;
                    valorintNumNoConformidadCierre = valorintNumNoConformidadCierre + objEvaDesempeño.intNumNoConformidadCierre;
                    valorintNumAuditoria = valorintNumAuditoria + objEvaDesempeño.intNumAuditoria;
                    valorintNumAuditoriaCumplida = valorintNumAuditoriaCumplida + objEvaDesempeño.intNumAuditoriaCumplida;
                    
                }
                InfoGrid1.Rows.Add(new Object[] {
                    //objEvaDesempeño.intIdRespuestaServicio.ToString().Trim(),
                    valorNumclientesEncuestados,
                    valorNumClienteAprovador,
                    valorintNumMetasCumplidas,
                    valorintNumMetasIncumplidas,
                    valorintNumNoConformidad,
                    valorintNumNoConformidadCierre,
                    valorintNumAuditoria,
                    valorintNumAuditoriaCumplida
                    });*/
                GVReporteInfoGeneral.DataSource = InfoGrid1;
                GVReporteInfoGeneral.PageIndex = PagIndex1;
                GVReporteInfoGeneral.DataBind();

                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los Factores</param>
        private void mtdLoadGridInfoGeneral(List<clsInformacionGeneral> lstInforGeneral, ref string fechaInicial, ref string fechaFinal)
        {
            int valorNumclientesEncuestados = 0;
            int valorNumClienteAprovador = 0;
            int valorintNumMetasCumplidas = 0;
            int valorintNumMetasIncumplidas = 0;
            /*int valorintNumNoConformidad = 0;
            int valorintNumNoConformidadCierre = 0;*/
            int valorintNumAuditoria = 0;
            int valorintNumAuditoriaCumplida = 0;
            clsInfoGeneralBLL cInfoGeneral = new clsInfoGeneralBLL();
            int NumNoConformidad = cInfoGeneral.mtdNumNoConformidad(ref fechaInicial, ref fechaFinal);
            int NumNoConformidadCierre = cInfoGeneral.mtdNumNoConformidadCierre(ref fechaInicial, ref fechaFinal);
            foreach (clsInformacionGeneral objEvaDesempeño in lstInforGeneral)
            {
                

                valorNumclientesEncuestados = objEvaDesempeño.intNumClientesEncuestados;
                valorNumClienteAprovador = valorNumClienteAprovador + objEvaDesempeño.intNumClientesAprobados;
                valorintNumMetasCumplidas = valorintNumMetasCumplidas + objEvaDesempeño.intNumMetasCumplidas;
                valorintNumMetasIncumplidas = valorintNumMetasIncumplidas + objEvaDesempeño.intNumMetasIncumplidas;
                //valorintNumNoConformidad = valorintNumNoConformidad + objEvaDesempeño.intNumNoConformidad;
                //valorintNumNoConformidadCierre = valorintNumNoConformidadCierre + objEvaDesempeño.intNumNoConformidadCierre;
                valorintNumAuditoria = valorintNumAuditoria + objEvaDesempeño.intNumAuditoria;
                valorintNumAuditoriaCumplida = valorintNumAuditoriaCumplida + objEvaDesempeño.intNumAuditoriaCumplida;
                /*InfoGrid1.Rows.Add(new Object[] {
                    //objEvaDesempeño.intIdRespuestaServicio.ToString().Trim(),
                    objEvaDesempeño.intNumClientesEncuestados.ToString().Trim(),
                    objEvaDesempeño.intNumClientesAprobados.ToString().Trim(),
                    //objEvaDesempeño.intIdIndicador.ToString().Trim(),
                    //objEvaDesempeño.strNombreIndicador.ToString().Trim(),
                    //objEvaDesempeño.strDescripcionIndicador.ToString().Trim(),
                    objEvaDesempeño.intNumMetasCumplidas.ToString().Trim(),
                    objEvaDesempeño.intNumMetasIncumplidas,
                    //objEvaDesempeño.strProceso.ToString().Trim(),
                    NumNoConformidad,
                    NumNoConformidadCierre,
                    objEvaDesempeño.intNumAuditoria.ToString().Trim(),
                    objEvaDesempeño.intNumAuditoriaCumplida.ToString().Trim()
                    });*/
            }
            InfoGrid1.Rows.Add(new Object[] {
                    //objEvaDesempeño.intIdRespuestaServicio.ToString().Trim(),
                    valorNumclientesEncuestados,
                    valorNumClienteAprovador,
                    valorintNumMetasCumplidas,
                    valorintNumMetasIncumplidas,
                    NumNoConformidad,
                    NumNoConformidadCierre,
                    valorintNumAuditoria,
                    valorintNumAuditoriaCumplida
                    });

        }
        #endregion
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            TXfechaInicial.Text = "";
            TXfechaFinal.Text = "";
            BodyGridRIG.Visible = false;
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

            Document pdfDocument = new Document(iTextSharp.text.PageSize.A4.Rotate(), 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Información General");
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
            PdfPTable pdfpTableCumplimiento = tools.createPdftable(GVReporteInfoGeneral);


            int fila = 0;
            foreach (GridViewRow GridViewRow in GVReporteInfoGeneral.Rows)
            {
                if (fila == 0)
                {
                    //string intIdRespuestaServicio = ((Label)GridViewRow.FindControl("intIdRespuestaServicio")).Text;
                    string intNumClientesEncuestados = ((Label)GridViewRow.FindControl("intNumClientesEncuestados")).Text;
                    string intNumClientesAprobados = ((Label)GridViewRow.FindControl("intNumClientesAprobados")).Text;
                    //string intIdIndicador = ((Label)GridViewRow.FindControl("intIdIndicador")).Text;
                    //string strNombreIndicador = ((Label)GridViewRow.FindControl("strNombreIndicador")).Text;
                    //string strDescripcionIndicador = ((Label)GridViewRow.FindControl("strDescripcionIndicador")).Text;
                    string intNumMetasCumplidas = ((Label)GridViewRow.FindControl("intNumMetasCumplidas")).Text;
                    string intNumMetasIncumplidas = ((Label)GridViewRow.FindControl("intNumMetasIncumplidas")).Text;
                    //string strProceso = ((Label)GridViewRow.FindControl("strProceso")).Text;
                    string intNumNoConformidad = ((Label)GridViewRow.FindControl("intNumNoConformidad")).Text;
                    string intNumNoConformidadCierre = ((Label)GridViewRow.FindControl("intNumNoConformidadCierre")).Text;
                    string intNumAuditoria = ((Label)GridViewRow.FindControl("intNumAuditoria")).Text;
                    string intNumAuditoriaCumplida = ((Label)GridViewRow.FindControl("intNumAuditoriaCumplida")).Text;
                    int iteracion = 0;
                    foreach (TableCell tableCell in GridViewRow.Cells)
                    {
                        /*if (iteracion == 0)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intIdRespuestaServicio));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }*/
                        if (iteracion == 0)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumClientesEncuestados));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        if (iteracion == 1)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumClientesAprobados));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        /*if (iteracion == 3)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intIdIndicador));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        if (iteracion == 4)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(strNombreIndicador));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        if (iteracion == 5)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcionIndicador));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }*/
                        if (iteracion == 2)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumMetasCumplidas));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        if (iteracion == 3)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumMetasIncumplidas));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        /*if (iteracion == 8)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(strProceso));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }*/
                        if (iteracion == 4)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumNoConformidad));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        if (iteracion == 5)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumNoConformidadCierre));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        if (iteracion == 6)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumAuditoria));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        if (iteracion == 7)
                        {
                            Font font = new Font();
                            font.Color = new Color(GVReporteInfoGeneral.RowStyle.ForeColor);
                            PdfPCell pdfCell = new PdfPCell(new Phrase(intNumAuditoriaCumplida));
                            pdfCell.BackgroundColor = new Color(GVReporteInfoGeneral.RowStyle.BackColor);
                            pdfpTableCumplimiento.AddCell(pdfCell);
                        }
                        iteracion++;
                    
                    }
                    fila++;
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
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Información General"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            //pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableCumplimiento);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteInformacionGeneral.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteInformacionGeneral_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable grid = new DataTable();
            //grid.Columns.Add("Codigó");
            grid.Columns.Add("# Clientes Encuestados");
            grid.Columns.Add("# Clientes Aprobados");
            //grid.Columns.Add("Codigó Indicador");
            //grid.Columns.Add("Nombre Indicador");
            //grid.Columns.Add("Descripción Indicador");
            grid.Columns.Add("# Metas Cumplidas");
            grid.Columns.Add("# Metas Incumplidas");
            //grid.Columns.Add("Proceso");
            grid.Columns.Add("# No Conformidad");
            grid.Columns.Add("# No Conformidad Cerradas");
            grid.Columns.Add("# Auditoria");
            grid.Columns.Add("# Auditoria Cumplida");
            DataRow row;
            int fila = 0;
            foreach (GridViewRow GridViewRow in GVReporteInfoGeneral.Rows)
            {
                if (fila == 0)
                {
                    row = grid.NewRow();
                    //string intIdRespuestaServicio = ((Label)GridViewRow.FindControl("intIdRespuestaServicio")).Text;
                    string intNumClientesEncuestados = ((Label)GridViewRow.FindControl("intNumClientesEncuestados")).Text;
                    string intNumClientesAprobados = ((Label)GridViewRow.FindControl("intNumClientesAprobados")).Text;
                    //string intIdIndicador = ((Label)GridViewRow.FindControl("intIdIndicador")).Text;
                    //string strNombreIndicador = ((Label)GridViewRow.FindControl("strNombreIndicador")).Text;
                    //string strDescripcionIndicador = ((Label)GridViewRow.FindControl("strDescripcionIndicador")).Text;
                    string intNumMetasCumplidas = ((Label)GridViewRow.FindControl("intNumMetasCumplidas")).Text;
                    string intNumMetasIncumplidas = ((Label)GridViewRow.FindControl("intNumMetasIncumplidas")).Text;
                    //string strProceso = ((Label)GridViewRow.FindControl("strProceso")).Text;
                    string intNumNoConformidad = ((Label)GridViewRow.FindControl("intNumNoConformidad")).Text;
                    string intNumNoConformidadCierre = ((Label)GridViewRow.FindControl("intNumNoConformidadCierre")).Text;
                    string intNumAuditoria = ((Label)GridViewRow.FindControl("intNumAuditoria")).Text;
                    string intNumAuditoriaCumplida = ((Label)GridViewRow.FindControl("intNumAuditoriaCumplida")).Text;
                    //row["Codigó"] = intIdRespuestaServicio;
                    row["# Clientes Encuestados"] = intNumClientesEncuestados;
                    row["# Clientes Aprobados"] = intNumClientesAprobados;
                    //row["Codigó Indicador"] = intIdIndicador;
                    //row["Nombre Indicador"] = strNombreIndicador;
                    //row["Descripción Indicador"] = strDescripcionIndicador;
                    row["# Metas Cumplidas"] = intNumMetasCumplidas;
                    row["# Metas Incumplidas"] = intNumMetasIncumplidas;
                    //row["Proceso"] = strProceso;
                    row["# No Conformidad"] = intNumNoConformidad;
                    row["# No Conformidad Cerradas"] = intNumNoConformidadCierre;
                    row["# Auditoria"] = intNumAuditoria;
                    row["# Auditoria Cumplida"] = intNumAuditoriaCumplida;
                    grid.Rows.Add(row);
                    fila++;
                }
            }
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            //workbook.Worksheets.Add(gridEncabezado, "Indicador");
            workbook.Worksheets.Add(grid, "Informacion General");
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
        protected void GVReporteInfoGeneral_PreRender(object sender, EventArgs e)
        {
            //MergeRowsMatrizData(GVReporteInfoGeneral);
            //SumDataGrid(GVReporteInfoGeneral);
            //SumGridTotal(GVReporteInfoGeneral);
        }
        public void SumGridTotal(GridView gridView)
        {
            int intNumClientesEncuestadosPreviuos = 0;
            int iteracion = 0;
            int resutlNumClienteEncuestados = 0;
            foreach (GridViewRow GridViewRow in gridView.Rows)
            {
                int intNumClientesEncuestados = Convert.ToInt32(((Label)GridViewRow.FindControl("intNumClientesEncuestados")).Text);
                if (iteracion > 0)
                    resutlNumClienteEncuestados = intNumClientesEncuestados + intNumClientesEncuestadosPreviuos;
                else
                    resutlNumClienteEncuestados = intNumClientesEncuestados;
                //string puntaje = ((Label)GridViewRow.FindControl("puntaje")).Text;
                iteracion++;
                intNumClientesEncuestadosPreviuos = resutlNumClienteEncuestados;
            }
        }
        public void SumDataGrid(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 0)
                    {
                        string text = ((Label)row.FindControl("intNumClientesEncuestados")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumClientesEncuestados")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumClientesEncuestados")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                        /*if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }*/
                    }
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("intNumClientesAprobados")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumClientesAprobados")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumClientesAprobados")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                    }
                    if (cellIndex == 2)
                    {
                        string text = ((Label)row.FindControl("intNumMetasCumplidas")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumMetasCumplidas")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumMetasCumplidas")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                    }
                    if (cellIndex == 3)
                    {
                        string text = ((Label)row.FindControl("intNumMetasIncumplidas")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumMetasIncumplidas")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumMetasIncumplidas")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                    }
                    if (cellIndex == 4)
                    {
                        string text = ((Label)row.FindControl("intNumNoConformidad")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumNoConformidad")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumNoConformidad")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                    }
                    if (cellIndex == 5)
                    {
                        string text = ((Label)row.FindControl("intNumNoConformidadCierre")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumNoConformidadCierre")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumNoConformidadCierre")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                    }
                    if (cellIndex == 6)
                    {
                        string text = ((Label)row.FindControl("intNumAuditoria")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumAuditoria")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumAuditoria")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                    }
                    if (cellIndex == 7)
                    {
                        string text = ((Label)row.FindControl("intNumAuditoriaCumplida")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumAuditoriaCumplida")).Text;
                        int result = Convert.ToInt32(text) + Convert.ToInt32(previousText);
                        ((Label)row.FindControl("intNumAuditoriaCumplida")).Text = result.ToString();
                        row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                        previousRow.Cells[cellIndex].Visible = false;
                        
                    }
                }
            }
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
                    if (cellIndex == 0)
                    {
                        string text = ((Label)row.FindControl("intIdRespuestaServicio")).Text;
                        string previousText = ((Label)previousRow.FindControl("intIdRespuestaServicio")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("intNumClientesEncuestados")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumClientesEncuestados")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 2)
                    {
                        string text = ((Label)row.FindControl("intNumClientesAprobados")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumClientesAprobados")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 3)
                    {
                        string text = ((Label)row.FindControl("intIdIndicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("intIdIndicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 4)
                    {
                        string text = ((Label)row.FindControl("strNombreIndicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("strNombreIndicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 5)
                    {
                        string text = ((Label)row.FindControl("strDescripcionIndicador")).Text;
                        string previousText = ((Label)previousRow.FindControl("strDescripcionIndicador")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 6)
                    {
                        string text = ((Label)row.FindControl("intNumMetasCumplidas")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumMetasCumplidas")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 7)
                    {
                        string text = ((Label)row.FindControl("intNumMetasIncumplidas")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumMetasIncumplidas")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 8)
                    {
                        string text = ((Label)row.FindControl("strProceso")).Text;
                        string previousText = ((Label)previousRow.FindControl("strProceso")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 9)
                    {
                        string text = ((Label)row.FindControl("intNumNoConformidad")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumNoConformidad")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 10)
                    {
                        string text = ((Label)row.FindControl("intNumNoConformidadCierre")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumNoConformidadCierre")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 11)
                    {
                        string text = ((Label)row.FindControl("intNumAuditoria")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumAuditoria")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 12)
                    {
                        string text = ((Label)row.FindControl("intNumAuditoriaCumplida")).Text;
                        string previousText = ((Label)previousRow.FindControl("intNumAuditoriaCumplida")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                }
            }
        }
    }
}