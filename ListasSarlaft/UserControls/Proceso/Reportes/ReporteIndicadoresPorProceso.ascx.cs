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
    public partial class ReporteIndicadoresPorProceso : System.Web.UI.UserControl
    {
        string IdFormulario = "4037";
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
        /// <summary>
        /// Metodo que inicia valores del datagrid
        /// </summary>
        private void mtdInicializarValores()
        {
            PagIndex1 = 0;
            PagIndex2 = 0;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            /*scriptManager.RegisterPostBackControl(this.GridView1);*/
            scriptManager.RegisterPostBackControl(this.btnImgCancelar);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            string strErrMsg = string.Empty;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    //mtdHabilitarCampos(1);
                    if (!mtdCargarDDLs(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
        }
        #region DDLs
        private bool mtdCargarDDLs(ref string strErrMsg)
        {
            bool booResult = false;

            booResult = mtdLoadDDLCadenaValor(ref strErrMsg);

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
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

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
                    0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, true, 0, string.Empty, string.Empty, string.Empty);
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
                        booResult = true;
                    }
                }
                else
                    booResult = false;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = false;
            }

            return booResult;
        }

        #endregion
        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string fechaInicial = TXfechaInicial.Text;
            string fechaFinal = TXfechaFinal.Text;
            int proceso = 0;
            int IdTipoProceso = 0;
            if (ddlSubproceso.SelectedValue != "" && ddlSubproceso.SelectedValue != "0")
            {
                proceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                IdTipoProceso = 3;
            }
            else
            {
                if (ddlProceso.SelectedValue != "" && ddlProceso.SelectedValue != "0")
                {
                    proceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                    IdTipoProceso = 2;
                }
                else
                {
                    if (ddlMacroproceso.SelectedValue != "" && ddlMacroproceso.SelectedValue != "0")
                    {
                        proceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                        IdTipoProceso = 1;
                    }
                }
            }
            if (mtdLoadIndicador(ref strErrMsg, ref fechaInicial, ref fechaFinal, ref proceso, ref IdTipoProceso) == false)
                omb.ShowMessage("no hay información registrada para generar el reporte", 2, "Atención");
            else
                BodyGridRIPP.Visible = true;
        }
        private bool mtdLoadIndicador(ref string strErrMsg, ref string fechaInicial, ref string fechaFinal, ref int proceso, ref int IdTipoProceso)
        {
            #region Vars
            bool booResult = false;
            clsMatrizIndicadores objMatriz = new clsMatrizIndicadores();
            List<clsMatrizIndicadores> lstMatriz = new List<clsMatrizIndicadores>();
            clsBLLMatrizIndicadores cMatriz = new clsBLLMatrizIndicadores();
            #endregion Vars
            lstMatriz = cMatriz.mtdConsultarIndicadorPorProceso(ref strErrMsg, ref fechaInicial, ref fechaFinal, ref proceso, ref IdTipoProceso);

            if (lstMatriz != null)
            {

                /*foreach (clsMatrizIndicadores objMatriz2 in lstMatriz)
                {
                    DataTable cuadro = objMatriz.dtCuadro;
                    
                    if (objMatriz2.intMetasCumplidas > 0)
                    {*/
                mtdLoadIndicador();
                mtdLoadIndicador(lstMatriz);
                GVIndicadoresPorProceso.DataSource = lstMatriz;
                GVIndicadoresPorProceso.PageIndex = pagIndex1;
                GVIndicadoresPorProceso.DataBind();
                booResult = true;
                Dbutton.Visible = true;
                //}
                //}
            }

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
            //grid.Columns.Add("intMetasCumplidas", typeof(string));
            grid.Columns.Add("intIdPeriodicidad", typeof(string));
            grid.Columns.Add("intIdProcesoIndicador", typeof(string));
            grid.Columns.Add("strArrayPeriodo", typeof(string));
            grid.Columns.Add("strArrayResultado", typeof(string));
            grid.Columns.Add("strResponsable", typeof(string));
            GVIndicadoresPorProceso.DataSource = grid;
            GVIndicadoresPorProceso.DataBind();
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
                //objMatriz.intMetasCumplidas,
                objMatriz.intIdPeriodicidad,
                objMatriz.intIdProcesoIndicador,
                objMatriz.strArrayPeriodo,
                objMatriz.strArrayResultado,
                objMatriz.strResponsable
                    });
                //}
            }
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            TXfechaInicial.Text = "";
            TXfechaFinal.Text = "";
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.ClearSelection();
            ddlProceso.ClearSelection();
            ddlSubproceso.ClearSelection();

            BodyGridRIPP.Visible = false;
            Dbutton.Visible = false;
        }

        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();

            if (!mtdLoadDDLProceso(ref strErrMsg))
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

            if (!mtdLoadDDLSubproceso(ref strErrMsg))
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
            

            #region ImprimirGrilla 
            Tools tools = new Tools();
            PdfPTable pdfpTableCumplimiento = tools.createPdftable(GVIndicadoresPorProceso);



            foreach (GridViewRow GridViewRow in GVIndicadoresPorProceso.Rows)
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
                //string intMetasCumplidas = ((Label)GridViewRow.FindControl("intMetasCumplidas")).Text;
                string strArrayPeriodo = ((Label)GridViewRow.FindControl("strArrayPeriodo")).Text;
                string strArrayResultado = ((Label)GridViewRow.FindControl("strArrayResultado")).Text;
                string strResponsable = ((Label)GridViewRow.FindControl("strResponsable")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intIdIndicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    /*if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcionObjetivo));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }*/
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strNombreIndicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    /*if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strDescripcionInicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }*/
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strProcesoIndicador));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intMeta));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
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
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strArrayPeriodo));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strArrayResultado));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
                        pdfpTableCumplimiento.AddCell(pdfCell);
                    }
                    if (iteracion == 6)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVIndicadoresPorProceso.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strResponsable));
                        pdfCell.BackgroundColor = new Color(GVIndicadoresPorProceso.RowStyle.BackColor);
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
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Indicadores Por Proceso"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            //pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdfpTableCumplimiento);
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteIndicadoresPorProceso.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteIndicadoresPorProceso_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
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
            //grid.Columns.Add("Metas Cumplidas");
            grid.Columns.Add("Periodo");
            grid.Columns.Add("Resultado");
            grid.Columns.Add("Responsable de proceso");
            DataRow row;
            foreach (GridViewRow GridViewRow in GVIndicadoresPorProceso.Rows)
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
                //string intMetasCumplidas = ((Label)GridViewRow.FindControl("intMetasCumplidas")).Text;
                string strArrayPeriodo = ((Label)GridViewRow.FindControl("strArrayPeriodo")).Text;
                string strArrayResultado = ((Label)GridViewRow.FindControl("strArrayResultado")).Text;
                string strResponsable = ((Label)GridViewRow.FindControl("strResponsable")).Text;
                row["Id Indicador"] = intIdIndicador;
                //row["Descripcion Objetivo"] = strDescripcionObjetivo;
                row["Nombre Indicador"] = strNombreIndicador;
                //row["Descripcion Inicador"] = strDescripcionInicador;
                row["Proceso Indicador"] = strProcesoIndicador;
                row["Meta"] = intMeta;
                //row["Metas Cumplidas"] = intMetasCumplidas;
                row["Periodo"] = strArrayPeriodo;
                row["Resultado"] = strArrayResultado;
                row["Responsable de proceso"] = strResponsable;
                grid.Rows.Add(row);
            }
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            //workbook.Worksheets.Add(gridEncabezado, "Indicador");
            workbook.Worksheets.Add(grid, "Indicador Metas Cumplidas");
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
        protected void GVIndicadoresPorProceso_PreRender(object sender, EventArgs e)
        {
            MergeRowsMatrizData(GVIndicadoresPorProceso);
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
                    /*if (cellIndex == 7)
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