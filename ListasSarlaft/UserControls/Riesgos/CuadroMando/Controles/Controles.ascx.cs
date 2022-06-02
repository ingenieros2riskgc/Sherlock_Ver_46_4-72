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

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando.Controles
{
    public partial class Controles : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        string IdFormulario = "5033";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBprocess);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.GVefectividad);
            scriptManager.RegisterPostBackControl(this.GVjerarquias);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();

                }
            }
        }
        private void mtdStard()
        {
            mtdCargarDDLs();
        }
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;
            if (mtdLoadDDLCadenaValor(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (mtdLoadDDLArea(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (mtdLoadDDLClasificacion(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            if (loadDDLPlanes(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            //mtdLoadDDLMacroProceso(ref strErrMsg);
        }
        #region LoadDDL
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Procesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los subprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
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
                        booResult = false;
                    }
                    //else
                    //    booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de Subprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLArea(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsDTOAreas> lstArea = new List<clsDTOAreas>();
            clsBLLAreas cArea = new clsBLLAreas();
            #endregion Vars

            try
            {
                lstArea = cArea.mtdConsultarAreas(ref strErrMsg);
                ddlAreas.Items.Clear();
                ddlAreas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstArea != null)
                    {
                        int intCounter = 1;

                        foreach (clsDTOAreas objArea in lstArea)
                        {
                            ddlAreas.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(objArea.strNombreArea, objArea.intIdArea.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de areas. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }
        private bool mtdLoadDDLClasificacion(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                ddlRiesgoGlobal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlRiesgoGlobal.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar clasificación riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        private bool loadDDLPlanes(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLPlanes();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLPlanEstrategico.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdPlan"].ToString()));
                }
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar los planes riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        private bool loadDDLObjetivos(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                //Camilo 12/02/2014
                //dtInfo = cRiesgo.loadDDLObjetivos();
                dtInfo = cRiesgo.loadDDLObjetivos(DDLPlanEstrategico.SelectedValue.ToString());
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLObjetivoEstrategico.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreObjetivos"].ToString().Trim(), dtInfo.Rows[i]["IdObjetivos"].ToString()));
                }
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar los objetivos estrategicos riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        private void loadDDLClasificacionGeneral(String IdClasificacionRiesgo, int Tipo)
        {
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionGeneral(IdClasificacionRiesgo);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            ddlClasificacionGeneral.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionGeneralRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar clasificación general. " + ex.Message, 2, "Atención");
            }
        }
        private void mtdLoadFactorRiesgo(int Tipo)
        {
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                dtInfo = cRiesgo.mtdCargarDDLFactorRiesgo(Tipo);

                switch (Tipo)
                {
                    case 1://FactorRiesgo
                        break;

                    case 2://FactorRiesgoLAFT
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            ddlFactorRiesgo.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreFactorRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar el Factor de Riesgo. " + ex.Message, 2, "Atención");
            }
        }
        private void loadDDLObjetivos()
        {
            try
            {
                System.Data.DataTable dtInfo = new System.Data.DataTable();
                //Camilo 12/02/2014
                //dtInfo = cRiesgo.loadDDLObjetivos();
                dtInfo = cRiesgo.loadDDLObjetivos(DDLPlanEstrategico.SelectedValue.ToString());
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLObjetivoEstrategico.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtInfo.Rows[i]["NombreObjetivos"].ToString().Trim(), dtInfo.Rows[i]["IdObjetivos"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar objetivos. " + ex.Message, 2, "Atención");
            }
        }
        #endregion LoadDDL
        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            /*if (ddlProceso.SelectedValue == "0")
                rfvProceso.Enabled = false;
            else
                rfvProceso.Enabled = true;
                */
            ddlSubproceso.Items.Clear();

            if (mtdLoadDDLSubproceso(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ddlRiesgoGlobal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRiesgoGlobal.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionGeneral(ddlRiesgoGlobal.SelectedValue.ToString().Trim(), 2);
            }
            if (ddlRiesgoGlobal.SelectedValue.ToString().Trim() == "2")
            {
                mtdLoadFactorRiesgo(2);
            }
        }
        protected void DDLPlanEstrategico_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLObjetivoEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---", "0"));
            if (DDLPlanEstrategico.SelectedValue.ToString().Trim() != "---")
                loadDDLObjetivos();
        }
        private bool loadListJerarquia(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                string selectCommand = "SELECT PJO.IdHijo, PJO.IdPadre, PJO.NombreHijo, PDJ.NombreResponsable, PDJ.CorreoResponsable " +
                "FROM Parametrizacion.JerarquiaOrganizacional PJO LEFT JOIN Parametrizacion.DetalleJerarquiaOrg PDJ ON PJO.idHijo = PDJ.idHijo";
                string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
                SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                System.Data.DataTable dtblDiscuss = new System.Data.DataTable();
                dad.Fill(dtblDiscuss);
                cbJerarquia.DataSource = dtblDiscuss;
                cbJerarquia.DataValueField = "IdHijo";
                cbJerarquia.DataTextField = "NombreHijo";
                cbJerarquia.DataBind();
                /*for (int i = 0; i < dtblDiscuss.Rows.Count; i++)
                {
                    cbJerarquia.Items.Insert(i + 1, new System.Web.UI.WebControls.ListItem(dtblDiscuss.Rows[i]["NombreHijo"].ToString().Trim(), dtblDiscuss.Rows[i]["IdHijo"].ToString()));
                }*/
            }
            catch (Exception ex)
            {
                strErrMsg = "Error al cargar los planes riesgo. " + ex.Message;
                booResult = true;
            }
            return booResult;
        }
        #endregion

        protected void IBprocess_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlTipoReporte.SelectedValue == "1")
            {
                //string prueba = Session["Jerarquias"].ToString();
                LoadInfoReporteControles();
                /*if (Session["Jerarquias"] != null && Session["Jerarquias"].ToString() == "0")
                {
                    dvTablaEfectividad.Visible = false;
                    dvGraficosEficacia.Visible = false;
                    dvJerarquias.Visible = true;
                }
                else*/
                {
                    dvTablaEfectividad.Visible = true;
                    dvGraficosEficacia.Visible = true;
                    dvJerarquias.Visible = false;

                }
                Dbutton.Visible = true;
            }
            if(ddlTipoReporte.SelectedValue == "2")
            {
                LoadInfoReporteControlvsJerarquia();
                if (Session["Jerarquias"] != null && Session["Jerarquias"].ToString() == "1")
                {
                    dvTablaEfectividad.Visible = false;
                    dvGraficosEficacia.Visible = false;
                    dvJerarquias.Visible = true;
                    dvResponsables.Visible = false;
                    dvTableJerarquias.Visible = true;
                }
                if (Session["Jerarquias"] != null && Session["Responsables"] != null && Session["Jerarquias"].ToString() == "0" && Session["Responsables"].ToString() == "0")
                {
                    dvTablaEfectividad.Visible = true;
                    dvGraficosEficacia.Visible = true;
                    dvJerarquias.Visible = false;
                    dvResponsables.Visible = false;
                    dvTableJerarquias.Visible = false;

                }
                if(Session["Responsables"] != null && Session["Responsables"].ToString() == "1")
                {
                    dvTablaEfectividad.Visible = false;
                    dvGraficosEficacia.Visible = false;
                    dvJerarquias.Visible = false;
                    dvTableJerarquias.Visible = true;
                    dvResponsables.Visible = true;
                }
                Dbutton.Visible = true;
            }
        }

        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            if(ddlTipoReporte.SelectedValue == "1")
            {
                trEfectividad.Visible = true;
                trJerarquia.Visible = false;
            }
            if(ddlTipoReporte.SelectedValue == "2")
            {
                trEfectividad.Visible = false;
                trJerarquia.Visible = true;
                loadListJerarquia(ref strErrMsg);
            }
        }

        protected void cbComparativo_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlTipoReporte.SelectedValue == "1")
            {
                LoadInfoReporteControles();
            }
            if (ddlTipoReporte.SelectedValue == "2")
                LoadInfoReporteControlvsJerarquia();
            /*
            if (ddlTipoReporte.SelectedValue == "3")
                mtdLoadReporteCausasSinControl();
            if (ddlTipoReporte.SelectedValue == "4")
                LoadInfoReporteRiesgosPlanes();*/
            // Creamos el tipo de Font que vamos utilizar
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
            pdfDocument.AddTitle("Cuadro de Mando Controles");
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
            PdfPTable pdfTableData = new PdfPTable(4);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Tipo Reporte:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlTipoReporte.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            
            if (ddlTipoReporte.SelectedValue == "1")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Efectividad:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                string Efectividad = string.Empty;
                for (int i = 0; i < cblEfectividades.Items.Count; i++)
                {
                    if (cblEfectividades.Items[i].Selected)
                    {
                        Efectividad += cblEfectividades.Items[i].Text + ",";
                    }
                }
                pdfCellEncabezado = new PdfPCell(new Phrase(Efectividad));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Jerarquias Organizacionales:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                string Jerarquia = string.Empty;
                for (int i = 0; i < cbJerarquia.Items.Count; i++)
                {
                    if (cbJerarquia.Items[i].Selected)
                    {
                        Jerarquia += cbJerarquia.Items[i].Text + ",";
                    }
                }
                pdfCellEncabezado = new PdfPCell(new Phrase(Jerarquia));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            /*pdfCellEncabezado = new PdfPCell(new Phrase("Cadena Valor:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlCadenaValor.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            if (ddlMacroproceso.SelectedItem != null)
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Macroproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlMacroproceso.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Macroproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("No seleccionado"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if (ddlProceso.SelectedItem != null)
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Proceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlProceso.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Proceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("No seleccionado"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            if (ddlSubproceso.SelectedItem != null)
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Subproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(ddlSubproceso.SelectedItem.Text));
                pdfTableData.AddCell(pdfCellEncabezado);
            }
            else
            {
                pdfCellEncabezado = new PdfPCell(new Phrase("Subproceso:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableData.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("No seleccionado"));
                pdfTableData.AddCell(pdfCellEncabezado);
            }

            pdfCellEncabezado = new PdfPCell(new Phrase("Área:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlAreas.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);

            PdfPTable pdfTableComparativo = new PdfPTable(2);
            if (cbComparativo.Checked)
            {

                pdfCellEncabezado = new PdfPCell(new Phrase("Comparativo:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Esta seleccionado como tal"));
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Inicio:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaInicial.Text));
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Final:", font1));
                pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
                pdfTableComparativo.AddCell(pdfCellEncabezado);
                pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaFinal.Text));
                pdfTableComparativo.AddCell(pdfCellEncabezado);
            }
            pdfCellEncabezado = new PdfPCell(new Phrase("Clasificación general:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlClasificacionGeneral.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Factor de Riesgo LA/FT:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlFactorRiesgo.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Plan Estratégico:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(DDLPlanEstrategico.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Objetivo Estratégico:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(DDLObjetivoEstrategico.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);*/
            #endregion
            MemoryStream streamControles = new MemoryStream();
            //MemoryStream streamRiesgosSarlaft = new MemoryStream();
            if (ddlTipoReporte.SelectedValue == "1")
            {
                ChartEficacia.SaveImage(streamControles, ChartImageFormat.Png);
            }
            if(ddlTipoReporte.SelectedValue == "2")
            {
                if (Session["Jerarquias"] != null && Session["Jerarquias"].ToString() == "1")
                {
                    ChartJerarquias.SaveImage(streamControles, ChartImageFormat.Png);
                }
                if (Session["Jerarquias"] != null && Session["Responsables"] != null && Session["Jerarquias"].ToString() == "0" && Session["Responsables"].ToString() == "0")
                {
                    ChartEficacia.SaveImage(streamControles, ChartImageFormat.Png);
                }
                if (Session["Responsables"] != null && Session["Responsables"].ToString() == "1")
                {
                    ChartResponsable.SaveImage(streamControles, ChartImageFormat.Png);
                }
                    
            }
            /*if (ddlTipoReporte.SelectedValue == "2")
                ChartSaro.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);
            if (ddlTipoReporte.SelectedValue == "3")
                ChartSaro.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);
            if (ddlTipoReporte.SelectedValue == "4")
                ChartSaro.SaveImage(streamRiesgosSaro, ChartImageFormat.Png);*/
            PdfPTable pdftblImageGraficoControles = new PdfPTable(1);
            //PdfPTable pdftblImageGraficoRiesgosSarlaft = new PdfPTable(1);
            //PdfPTable pdftblImageGrafico2 = new PdfPTable(1);
            //PdfPTable pdftblImageGrafico3 = new PdfPTable(1);
            if (ddlTipoReporte.SelectedValue == "1")
            {
                iTextSharp.text.Image imagenGraficoControles = iTextSharp.text.Image.GetInstance(streamControles.GetBuffer());
                //iTextSharp.text.Image imagenGraficoSarlaft = iTextSharp.text.Image.GetInstance(streamRiesgosSarlaft.GetBuffer());

                PdfPCell pdfcellImageGraficoControles = new PdfPCell(imagenGraficoControles, true);
                //PdfPCell pdfcellImageGraficoRiesgosSarlaft = new PdfPCell(imagenGraficoSarlaft, true);
                pdftblImageGraficoControles.AddCell(pdfcellImageGraficoControles);
                //pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoControles);
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                iTextSharp.text.Image imagenGraficoControles = iTextSharp.text.Image.GetInstance(streamControles.GetBuffer());
                //iTextSharp.text.Image imagenGraficoSarlaft = iTextSharp.text.Image.GetInstance(streamRiesgosSarlaft.GetBuffer());

                PdfPCell pdfcellImageGraficoControles = new PdfPCell(imagenGraficoControles, true);
                //PdfPCell pdfcellImageGraficoRiesgosSarlaft = new PdfPCell(imagenGraficoSarlaft, true);
                pdftblImageGraficoControles.AddCell(pdfcellImageGraficoControles);
                //pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoControles);
            }
            /*if (ddlTipoReporte.SelectedValue == "2")
            {
                iTextSharp.text.Image imagenGraficoSaro = iTextSharp.text.Image.GetInstance(streamRiesgosSaro.GetBuffer());

                PdfPCell pdfcellImageGraficoRiesgosSaro = new PdfPCell(imagenGraficoSaro, true);

                pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSaro);
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {
                iTextSharp.text.Image imagenGraficoSaro = iTextSharp.text.Image.GetInstance(streamRiesgosSaro.GetBuffer());

                PdfPCell pdfcellImageGraficoRiesgosSaro = new PdfPCell(imagenGraficoSaro, true);

                pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSaro);
            }
            if (ddlTipoReporte.SelectedValue == "4")
            {
                iTextSharp.text.Image imagenGraficoSaro = iTextSharp.text.Image.GetInstance(streamRiesgosSaro.GetBuffer());

                PdfPCell pdfcellImageGraficoRiesgosSaro = new PdfPCell(imagenGraficoSaro, true);

                pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoRiesgosSaro);
            }*/
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
            /*if (cbComparativo.Checked)
            {
                pdfDocument.Add(pdfTableComparativo);
                pdfDocument.Add(Chunk.NEWLINE);
            }*/
            if (ddlTipoReporte.SelectedValue == "1")
            {

                pdfDocument.Add(pdftblImageGraficoControles);
                pdfDocument.Add(Chunk.NEWLINE);
                /*pdfDocument.Add(pdftblImageGraficoRiesgosSarlaft);
                pdfDocument.Add(Chunk.NEWLINE);*/
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {

                pdfDocument.Add(pdftblImageGraficoControles);
                pdfDocument.Add(Chunk.NEWLINE);
                /*pdfDocument.Add(pdftblImageGraficoRiesgosSarlaft);
                pdfDocument.Add(Chunk.NEWLINE);*/
            }
            /*if (ddlTipoReporte.SelectedValue == "2")
            {

                pdfDocument.Add(pdftblImageGraficoRiesgosSaro);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {

                pdfDocument.Add(pdftblImageGraficoRiesgosSaro);
                pdfDocument.Add(Chunk.NEWLINE);
                /*pdfDocument.Add(pdftblImageGraficoRiesgosSarlaft);
                pdfDocument.Add(Chunk.NEWLINE);
            }
            if (ddlTipoReporte.SelectedValue == "4")
            {

                pdfDocument.Add(pdftblImageGraficoRiesgosSaro);
                pdfDocument.Add(Chunk.NEWLINE);
            }*/
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CuadroMandoControles.pdf");
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
            if (ddlTipoReporte.SelectedValue == "1")
                LoadInfoReporteControles();
            if (ddlTipoReporte.SelectedValue == "2")
                LoadInfoReporteControlvsJerarquia();
            /*if (ddlTipoReporte.SelectedValue == "3")
                mtdLoadReporteCausasSinControl();
            if (ddlTipoReporte.SelectedValue == "4")
                LoadInfoReporteRiesgosPlanes();*/
            string tmpChartNameControl = "Grafico" + ddlTipoReporte.SelectedValue + ".jpg";
            string imgPathControl = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartNameControl;
            
            if (ddlTipoReporte.SelectedValue == "1" || ddlTipoReporte.SelectedValue == "2")
            {

                if (Session["Jerarquias"] != null && Session["Jerarquias"].ToString() == "1")
                {
                    ChartJerarquias.SaveImage(imgPathControl);
                }
                if (Session["Jerarquias"] == null && Session["Responsables"] == null)
                {
                    ChartEficacia.SaveImage(imgPathControl);
                }
                if (Session["Responsables"] != null && Session["Responsables"].ToString() == "1")
                {
                    ChartResponsable.SaveImage(imgPathControl);
                }
            }
            /*if (ddlTipoReporte.SelectedValue == "2")
                ChartSaro.SaveImage(imgPathSaro);
            if (ddlTipoReporte.SelectedValue == "3")
                ChartSaro.SaveImage(imgPathSaro);
            if (ddlTipoReporte.SelectedValue == "4")
                ChartSaro.SaveImage(imgPathSaro);*/
            string imgPathCotrolLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameControl);
            //string imgPathSarlaftLocal = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartNameRiesgosSarlaft);
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Tipo reporte:");
            if (ddlTipoReporte.SelectedValue == "1")
            {
                gridEncabezado.Columns.Add("Efectividad:");
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                gridEncabezado.Columns.Add("Jeraraquias Organizacionales:");
            }
                /*gridEncabezado.Columns.Add("Riesgo Global:");
                gridEncabezado.Columns.Add("Cadena Valor:");
                gridEncabezado.Columns.Add("Macroproceso:");
                gridEncabezado.Columns.Add("Proceso:");
                gridEncabezado.Columns.Add("Subproceso:");
                gridEncabezado.Columns.Add("Área:");
                gridEncabezado.Columns.Add("Clasificación general:");
                gridEncabezado.Columns.Add("Factor de Riesgo LA/FT:");
                gridEncabezado.Columns.Add("Plan Estratégico:");
                gridEncabezado.Columns.Add("Objetivo Estratégico:");
                if (cbComparativo.Checked)
                {
                    gridEncabezado.Columns.Add("Fecha Inicial:");
                    gridEncabezado.Columns.Add("Fecha Final:");
                }*/

                DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Tipo reporte:"] = ddlTipoReporte.SelectedItem.Text;
            if (ddlTipoReporte.SelectedValue == "1")
            {
                string Efectividad = string.Empty;
                for (int ident = 0; ident < cblEfectividades.Items.Count; ident++)
                {
                    if (cblEfectividades.Items[ident].Selected)
                    {
                        Efectividad += cblEfectividades.Items[ident].Text + ",";
                    }
                }
                rowEncabezado["Efectividad:"] = Efectividad;
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                string Jerarquia = string.Empty;
                for (int ident = 0; ident < cbJerarquia.Items.Count; ident++)
                {
                    if (cbJerarquia.Items[ident].Selected)
                    {
                        Jerarquia += cbJerarquia.Items[ident].Text + ",";
                    }
                }
                rowEncabezado["Jeraraquias Organizacionales:"] = Jerarquia;
            }
            /*rowEncabezado["Riesgo Global:"] = ddlRiesgoGlobal.SelectedItem.Text;
            rowEncabezado["Cadena Valor:"] = ddlCadenaValor.SelectedItem.Text;
            if (ddlMacroproceso.SelectedItem != null)
            {
                rowEncabezado["Macroproceso:"] = ddlCadenaValor.SelectedItem.Text;
            }
            else
            {
                rowEncabezado["Macroproceso:"] = "No seleccionado";
            }
            if (ddlProceso.SelectedItem != null)
                rowEncabezado["Proceso:"] = ddlProceso.SelectedItem.Text;
            else
                rowEncabezado["Proceso:"] = "No seleccionado";
            if (ddlSubproceso.SelectedItem != null)
                rowEncabezado["Subproceso:"] = ddlSubproceso.SelectedItem.Text;
            else
                rowEncabezado["Subproceso:"] = "No seleccionado";
            rowEncabezado["Área:"] = ddlAreas.SelectedItem.Text;
            if (cbComparativo.Checked)
            {
                rowEncabezado["Fecha Inicial:"] = txbFechaInicial.Text;
                rowEncabezado["Fecha Final:"] = txbFechaFinal.Text;
            }
            rowEncabezado["Clasificación general:"] = ddlClasificacionGeneral.SelectedItem.Text;
            rowEncabezado["Factor de Riesgo LA/FT:"] = ddlFactorRiesgo.SelectedItem.Text;
            rowEncabezado["Plan Estratégico:"] = DDLPlanEstrategico.SelectedItem.Text;
            rowEncabezado["Objetivo Estratégico:"] = DDLObjetivoEstrategico.SelectedItem.Text;*/
            gridEncabezado.Rows.Add(rowEncabezado);
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=CuadroMandoControles.xls;");
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

            if (ddlTipoReporte.SelectedValue == "1" || ddlTipoReporte.SelectedValue == "2")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathCotrolLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
                /*StringWriter stringWrite2 = new StringWriter();
                HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
                string headerTableSarlaft = @"<Table><tr><td><img src='" + imgPathSarlaftLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSarlaft);
                Response.Write(stringWrite2.ToString());*/
            }
            /*if (ddlTipoReporte.SelectedValue == "2")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathSaroLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
            }
            if (ddlTipoReporte.SelectedValue == "3")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathSaroLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
                /*StringWriter stringWrite2 = new StringWriter();
                HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
                string headerTableSarlaft = @"<Table><tr><td><img src='" + imgPathSarlaftLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSarlaft);
                Response.Write(stringWrite2.ToString());
            }
            if (ddlTipoReporte.SelectedValue == "4")
            {

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTableSaro = @"<Table><tr><td><img src='" + imgPathSaroLocal + @"' \></td></tr></Table>";
                Response.Write(headerTableSaro);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
            }*/
            Response.End();

        }
        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            Dbutton.Visible = false;
            cblEfectividades.ClearSelection();
            ddlTipoReporte.SelectedIndex = 0;
            ddlRiesgoGlobal.Items.Clear();
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            ddlAreas.Items.Clear();
            ddlClasificacionGeneral.Items.Clear();
            ddlFactorRiesgo.Items.Clear();
            DDLPlanEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Clear();
            cbComparativo.Checked = false;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            txbPerfilInicial.Text = string.Empty;
            txbPerfilFinal.Text = string.Empty;

            dvTablaEfectividad.Visible = false;
            dvGraficosEficacia.Visible = false;
            dvJerarquias.Visible = false;
            dvTableJerarquias.Visible = false;
            dvResponsables.Visible = false;
            trEfectividad.Visible = false;
            trJerarquia.Visible = false;

            trHistoricoInicial.Visible = false;
            trHistoricoFinal.Visible = false;
            trPerfilHistoricoInicial.Visible = false;
            trPerfilHistoricoFinal.Visible = false;
        }
        private bool LoadInfoReporteControles()
        {
            bool booResult = false;
            string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            int valorDeficiente = 0;
            int valorRegular = 0;
            int valorBueno = 0;
            int valorExcelente = 0;
            List<clsDTOCuadroMandoControles> lstReporte = new List<clsDTOCuadroMandoControles>();
            clsBLLCuadroMandoControles cCuadroRiesgos = new clsBLLCuadroMandoControles();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            //clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            /*objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            if (ddlMacroproceso.Items.Count > 0)
                objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            else
                objFiltros.intIdMacroProceso = 0;
            if (ddlProceso.Items.Count > 0)
                objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            else
                objFiltros.intIdProceso = 0;
            if (ddlSubproceso.Items.Count > 0)
                objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            else
                objFiltros.intIdSubProceso = 0;
            objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (ddlClasificacionGeneral.SelectedValue != "---")
                objFiltros.intIdClasificacionGeneral = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
            else
                objFiltros.intIdClasificacionGeneral = 0;
            if (ddlFactorRiesgo.SelectedValue != "---")
                objFiltros.intIdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            else
                objFiltros.intIdFactor = 0;
            if (DDLPlanEstrategico.SelectedValue != "---")
            {
                if (DDLObjetivoEstrategico.SelectedValue != "---")
                {
                    objFiltros.intIdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                }
                else
                {
                    objFiltros.intIdObjetivo = 0;
                }
            }
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }*/
            int IdJerarquia = 0;
            /**********************Filtros  de Consulta****************************/
            int valorTotalRiesgo = 0;
            booResult = cCuadroRiesgos.LoadInfoReporteControles(ref strErrMgs, ref lstReporte, IdJerarquia, ref valorTotalRiesgo);
            //string ListaEficacia = string.Empty;
            
            if (lstReporte != null)
            {
                foreach (clsDTOCuadroMandoControles objCuadro in lstReporte)
                {
                    for (int i = 0; i < cblEfectividades.Items.Count; i++)
                    {
                        if (cblEfectividades.Items[i].Selected)
                        {
                            if (objCuadro.strEfectividad == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Deficiente")
                                valorDeficiente = valorDeficiente + 1;
                            if (objCuadro.strEfectividad == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Regular")
                                valorRegular = valorRegular + 1;
                            if (objCuadro.strEfectividad == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Bueno")
                                valorBueno = valorBueno + 1;
                            if (objCuadro.strEfectividad == cblEfectividades.Items[i].Value.ToString().Trim() && cblEfectividades.Items[i].Value.ToString().Trim() == "Excelente")
                                valorExcelente = valorExcelente + 1;
                        }
                    }
                }
                System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                DataColumn dcColumn;

                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Efectividad";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "CantidadControl";
                dtCuadroMando.Columns.Add(dcColumn);
                int CantControls = 0;
                if(cCuadroRiesgos.GetAllControles(ref strErrMgs, ref CantControls) == true)
                    Session["TotalControles"] = CantControls;
                for (int i = 0; i < cblEfectividades.Items.Count; i++)
                {
                    if (cblEfectividades.Items[i].Selected)
                    {
                        DataRow dr;
                        if (cblEfectividades.Items[i].Value.ToString().Trim() == "Deficiente")
                        {
                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Deficiente";
                            dr["CantidadControl"] = valorDeficiente;
                            dtCuadroMando.Rows.Add(dr);
                        }
                        if (cblEfectividades.Items[i].Value.ToString().Trim() == "Regular")
                        {
                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Regular";
                            dr["CantidadControl"] = valorRegular;
                            dtCuadroMando.Rows.Add(dr);
                        }
                        if (cblEfectividades.Items[i].Value.ToString().Trim() == "Bueno")
                        {
                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Bueno";
                            dr["CantidadControl"] = valorBueno;
                            dtCuadroMando.Rows.Add(dr);
                        }
                        if (cblEfectividades.Items[i].Value.ToString().Trim() == "Excelente")
                        {
                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Excelente";
                            dr["CantidadControl"] = valorExcelente;
                            dtCuadroMando.Rows.Add(dr);
                        }
                            
                    }
                }

                mtdViewChartEfectividad(dtCuadroMando);
            }

            return booResult;
        }
        public void mtdViewChartEfectividad(System.Data.DataTable dtInfo)
        {
            GVefectividad.DataSource = dtInfo;
            GVefectividad.DataBind();
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
            }
            ChartEficacia.Series[0].Points.DataBindXY(x, y);
            ChartEficacia.Series[0].Name = "Efectividad de Controles";
            ChartEficacia.Series[0].XValueMember = "Efectividad";
            ChartEficacia.Series[0].YValueMembers = "Cantidad Controles";
            ChartEficacia.Series[0].ChartType = SeriesChartType.Column;
            ChartEficacia.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            ChartEficacia.Legends[0].Enabled = true;
            //ChartEficacia.Titles.Add("NewTitle");
            ChartEficacia.Titles[0].Text = "Controles Totales: " + Total;
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartEficacia.Series)
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
                    point.Label = string.Format("{0:0} - {1}", Math.Round((point.YValues[0]/ Convert.ToInt32(Session["TotalControles"].ToString()))*100,2) +"%", point.AxisLabel);

                }
            }
        }

        protected void GVefectividad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str;
            int RowGridRiesgos = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVefectividad.Rows[RowGridRiesgos];
            string Efectividad = row.Cells[0].Text;
            int IdJerarquia = 0;
            
            

            if (ddlTipoReporte.SelectedValue == "1")
            {
                LoadInfoReporteControles();
            }
            if (ddlTipoReporte.SelectedValue == "2")
            {
                if (Session["IdJerarquia"] != null)
                    IdJerarquia = Convert.ToInt32(Session["IdJerarquia"].ToString());
                LoadInfoReporteControlvsJerarquia();
            }
                
            str = "window.open('ViewDetalleControl/ViewDetalleControl.aspx?Efectividad=" + Efectividad + "&IdJerarquia=" + IdJerarquia + "','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }
        public bool LoadInfoReporteControlvsJerarquia()
        {
            bool booResult = false;
            string strErrMgs = string.Empty;
            //string NombreRiesgoInherente = string.Empty;
            
            List<clsDTOCuadroMandoControles> lstReporte = new List<clsDTOCuadroMandoControles>();
            clsBLLCuadroMandoControles cCuadroRiesgos = new clsBLLCuadroMandoControles();
            //clsDTOCuadroMandoConsolidado cuadroMando = new clsDTOCuadroMandoConsolidado();
            //clsDTOCuadroMandoConsolidadoFiltros objFiltros = new clsDTOCuadroMandoConsolidadoFiltros();
            /**********************Filtros de Consulta****************************/
            /*objFiltros.intRiesgoGlobal = Convert.ToInt32(ddlRiesgoGlobal.SelectedValue);
            objFiltros.intIdCadenaValor = Convert.ToInt32(ddlCadenaValor.SelectedValue);
            if (ddlMacroproceso.Items.Count > 0)
                objFiltros.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue);
            else
                objFiltros.intIdMacroProceso = 0;
            if (ddlProceso.Items.Count > 0)
                objFiltros.intIdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
            else
                objFiltros.intIdProceso = 0;
            if (ddlSubproceso.Items.Count > 0)
                objFiltros.intIdSubProceso = Convert.ToInt32(ddlSubproceso.SelectedValue);
            else
                objFiltros.intIdSubProceso = 0;
            objFiltros.intArea = Convert.ToInt32(ddlAreas.SelectedValue);
            if (ddlClasificacionGeneral.SelectedValue != "---")
                objFiltros.intIdClasificacionGeneral = Convert.ToInt32(ddlClasificacionGeneral.SelectedValue);
            else
                objFiltros.intIdClasificacionGeneral = 0;
            if (ddlFactorRiesgo.SelectedValue != "---")
                objFiltros.intIdFactor = Convert.ToInt32(ddlFactorRiesgo.SelectedValue);
            else
                objFiltros.intIdFactor = 0;
            if (DDLPlanEstrategico.SelectedValue != "---")
            {
                if (DDLObjetivoEstrategico.SelectedValue != "---")
                {
                    objFiltros.intIdObjetivo = Convert.ToInt32(DDLObjetivoEstrategico.SelectedValue);
                }
                else
                {
                    objFiltros.intIdObjetivo = 0;
                }
            }
            if (cbComparativo.Checked)
            {
                objFiltros.dtFechaHistoricoInicial = Convert.ToDateTime(txbFechaInicial.Text);
                objFiltros.dtFechaHistoricoFinal = Convert.ToDateTime(txbFechaFinal.Text);
            }*/
            /**********************Filtros  de Consulta****************************/
            int CantJerarquia = 0;
            for (int i = 0; i < cbJerarquia.Items.Count; i++)
            {
                if (cbJerarquia.Items[i].Selected)
                {
                    CantJerarquia++;
                    
                }
            }
            if(CantJerarquia <= 4)
            {
                Session["Jerarquias"] = 1;
                Session["Responsables"] = 0;
                int IdJerarquia = 0;
                System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                DataColumn dcColumn;
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Jerarquia";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Deficiente";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Regular";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Bueno";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Excelente";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Codigo";
                dtCuadroMando.Columns.Add(dcColumn);
                int valorTotalRiesgo = 0;
                for (int i = 0; i < cbJerarquia.Items.Count; i++)
                {
                    lstReporte = new List<clsDTOCuadroMandoControles>();
                    if (cbJerarquia.Items[i].Selected)
                    {
                        int valorDeficiente = 0;
                        int valorRegular = 0;
                        int valorBueno = 0;
                        int valorExcelente = 0;
                        IdJerarquia = Convert.ToInt32(cbJerarquia.Items[i].Value);
                        booResult = cCuadroRiesgos.LoadInfoReporteControles(ref strErrMgs, ref lstReporte, IdJerarquia, ref valorTotalRiesgo);
                        if (lstReporte != null)
                        {
                            foreach (clsDTOCuadroMandoControles objCuadro in lstReporte)
                            {

                                if (objCuadro.strEfectividad == "Deficiente")
                                    valorDeficiente = valorDeficiente + 1;
                                if (objCuadro.strEfectividad == "Regular")
                                    valorRegular = valorRegular + 1;
                                if (objCuadro.strEfectividad == "Bueno")
                                    valorBueno = valorBueno + 1;
                                if (objCuadro.strEfectividad == "Excelente")
                                    valorExcelente = valorExcelente + 1;
                            }
                            
                            
                            int CantControls = 0;
                            if (cCuadroRiesgos.GetAllControles(ref strErrMgs, ref CantControls) == true)
                                Session["TotalControles"] = CantControls;

                            /*DataRow dr;

                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Deficiente";
                            dr["CantidadControl"] = valorDeficiente;
                            dtCuadroMando.Rows.Add(dr);

                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Regular";
                            dr["CantidadControl"] = valorRegular;
                            dtCuadroMando.Rows.Add(dr);

                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Bueno";
                            dr["CantidadControl"] = valorBueno;
                            dtCuadroMando.Rows.Add(dr);

                            dr = dtCuadroMando.NewRow();
                            dr["Efectividad"] = "Excelente";
                            dr["CantidadControl"] = valorExcelente;
                            dtCuadroMando.Rows.Add(dr);*/
                            DataRow dr1 = dtCuadroMando.NewRow();
                            dr1["Jerarquia"] = cbJerarquia.Items[i].Text;
                            dr1["Deficiente"] = valorDeficiente;
                            dr1["Regular"] = valorRegular;
                            dr1["Bueno"] = valorBueno;
                            dr1["Excelente"] = valorExcelente;
                            dr1["Codigo"] = IdJerarquia;
                            dtCuadroMando.Rows.Add(dr1);

                        }
                    }
                }
                LoadChartJerarquias(dtCuadroMando);

            }
            if(CantJerarquia > 4)
            {
                Session["Responsables"] = 1;
                Session["Jerarquias"] = 0;
                int IdJerarquia = 0;
                System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                DataColumn dcColumn;
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Jerarquia";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Deficiente";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Regular";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Bueno";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Excelente";
                dtCuadroMando.Columns.Add(dcColumn);
                dcColumn = new DataColumn();
                dcColumn.ColumnName = "Codigo";
                dtCuadroMando.Columns.Add(dcColumn);
                int valorTotalRiesgo = 0;
                for (int i = 0; i < cbJerarquia.Items.Count; i++)
                {
                    if (cbJerarquia.Items[i].Selected)
                    {
                        lstReporte = new List<clsDTOCuadroMandoControles>();
                        int valorDeficiente = 0;
                        int valorRegular = 0;
                        int valorBueno = 0;
                        int valorExcelente = 0;
                        IdJerarquia = Convert.ToInt32(cbJerarquia.Items[i].Value);
                        booResult = cCuadroRiesgos.LoadInfoReporteControles(ref strErrMgs, ref lstReporte, IdJerarquia, ref valorTotalRiesgo);
                        if (lstReporte != null)
                        {
                            foreach (clsDTOCuadroMandoControles objCuadro in lstReporte)
                            {

                                if (objCuadro.strEfectividad == "Deficiente")
                                    valorDeficiente = valorDeficiente + 1;
                                if (objCuadro.strEfectividad == "Regular")
                                    valorRegular = valorRegular + 1;
                                if (objCuadro.strEfectividad == "Bueno")
                                    valorBueno = valorBueno + 1;
                                if (objCuadro.strEfectividad == "Excelente")
                                    valorExcelente = valorExcelente + 1;
                            }


                            int CantControls = 0;
                            if (cCuadroRiesgos.GetAllControles(ref strErrMgs, ref CantControls) == true)
                                Session["TotalControles"] = CantControls;
                            DataRow dr1 = dtCuadroMando.NewRow();
                            dr1["Jerarquia"] = cbJerarquia.Items[i].Text;
                            dr1["Deficiente"] = valorDeficiente;
                            dr1["Regular"] = valorRegular;
                            dr1["Bueno"] = valorBueno;
                            dr1["Excelente"] = valorExcelente;
                            dr1["Codigo"] = IdJerarquia;
                            dtCuadroMando.Rows.Add(dr1);

                        }
                    }
                }
                LoadChartResponsables(dtCuadroMando);
            }
            if(CantJerarquia == 1)
            {
                Session["Jerarquias"] = 0;
                Session["Responsables"] = 0;
                int valorDeficiente = 0;
                int valorRegular = 0;
                int valorBueno = 0;
                int valorExcelente = 0;
                int IdJerarquia = 0;
                for (int i = 0; i < cbJerarquia.Items.Count; i++)
                {
                    if (cbJerarquia.Items[i].Selected)
                    {
                        IdJerarquia = Convert.ToInt32(cbJerarquia.SelectedValue);
                        Session["IdJerarquia"] = IdJerarquia.ToString();
                    }
                }
                int valorTotalRiesgo = 0;
                booResult = cCuadroRiesgos.LoadInfoReporteControles(ref strErrMgs, ref lstReporte, IdJerarquia, ref valorTotalRiesgo);
                //string ListaEficacia = string.Empty;

                if (lstReporte != null)
                {
                    foreach (clsDTOCuadroMandoControles objCuadro in lstReporte)
                    {

                        if (objCuadro.strEfectividad == "Deficiente")
                            valorDeficiente = valorDeficiente + 1;
                        if (objCuadro.strEfectividad == "Regular")
                            valorRegular = valorRegular + 1;
                        if (objCuadro.strEfectividad == "Bueno")
                            valorBueno = valorBueno + 1;
                        if (objCuadro.strEfectividad == "Excelente")
                            valorExcelente = valorExcelente + 1;
                    }
                    System.Data.DataTable dtCuadroMando = new System.Data.DataTable();
                    DataColumn dcColumn;

                    dcColumn = new DataColumn();
                    dcColumn.ColumnName = "Efectividad";
                    dtCuadroMando.Columns.Add(dcColumn);
                    dcColumn = new DataColumn();
                    dcColumn.ColumnName = "CantidadControl";
                    dtCuadroMando.Columns.Add(dcColumn);
                    int CantControls = 0;
                    if (cCuadroRiesgos.GetAllControles(ref strErrMgs, ref CantControls) == true)
                        Session["TotalControles"] = CantControls;

                    DataRow dr;

                    dr = dtCuadroMando.NewRow();
                    dr["Efectividad"] = "Deficiente";
                    dr["CantidadControl"] = valorDeficiente;
                    dtCuadroMando.Rows.Add(dr);

                    dr = dtCuadroMando.NewRow();
                    dr["Efectividad"] = "Regular";
                    dr["CantidadControl"] = valorRegular;
                    dtCuadroMando.Rows.Add(dr);

                    dr = dtCuadroMando.NewRow();
                    dr["Efectividad"] = "Bueno";
                    dr["CantidadControl"] = valorBueno;
                    dtCuadroMando.Rows.Add(dr);

                    dr = dtCuadroMando.NewRow();
                    dr["Efectividad"] = "Excelente";
                    dr["CantidadControl"] = valorExcelente;
                    dtCuadroMando.Rows.Add(dr);


                    mtdViewChartJerarquia(dtCuadroMando);
                }
            }
            
            return booResult;
        }
        public void mtdViewChartJerarquia(System.Data.DataTable dtInfo)
        {
            GVefectividad.DataSource = dtInfo;
            GVefectividad.DataBind();
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtInfo.Rows[i][1]);
                Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
            }
            ChartEficacia.Series[0].Points.DataBindXY(x, y);
            ChartEficacia.Series[0].Name = "Controles por Responsable";
            ChartEficacia.Series[0].XValueMember = "Responsable";
            ChartEficacia.Series[0].YValueMembers = "Cantidad Controles";
            ChartEficacia.Series[0].ChartType = SeriesChartType.Pie;
            ChartEficacia.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            ChartEficacia.Legends[0].Enabled = true;
            //ChartEficacia.Titles.Add("NewTitle");
            ChartEficacia.Titles[0].Text = "Controles Totales: " + Total;
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartEficacia.Series)
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
            }
        }
        private void LoadChartJerarquias(System.Data.DataTable initialDataSource)
        {

            GVjerarquias.DataSource = initialDataSource;
            GVjerarquias.DataBind();
            int Identity = 1;
            //System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();

            foreach (DataRow dr in initialDataSource.Rows)
            {

                System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();
                series.Name = dr[0].ToString();
                series.XValueMember = "Efectividad";
                series.YValueMembers = "CantidadControl";
                //series.LabelBackColor = System.Drawing.Color.Green;
                int iteracion = 0;
                for (int i = 1; i < initialDataSource.Columns.Count; i++)
                {

                    //series.Legend = dr[0].ToString();
                    int y = Convert.ToInt32(dr[i].ToString());
                    //series.Points.AddXY(dr[0].ToString(), y);
                    //series.Points.AddY(y);
                    if (i == 1)
                    {
                        series.Points.AddXY("Deficiente", y);
                        //series.Points[i].Label = y.ToString();
                    }
                    if (i == 2)
                    {
                        series.Points.AddXY("Regular", y);
                        //series.Points[i].Label = y.ToString();
                    }
                    if (i == 3)
                    {
                        series.Points.AddXY("Bueno", y);
                        //series.Points[i].Label = y.ToString();
                    }
                    if (i == 4)
                    {
                        series.Points.AddXY("Excelente", y);
                        //.Points[i].Label = y.ToString();
                    }
                }
                //Chart4.Legends[0].Enabled = true;
                ChartJerarquias.Series.Add(series);
            }
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartJerarquias.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    
                    point.Label = string.Format("{0:0}", point.YValues[0]);

                }
            }
            //Chart4.Legends[0].Enabled = true;

        }
        private void LoadChartResponsables(System.Data.DataTable initialDataSource)
        {

            GVjerarquias.DataSource = initialDataSource;
            GVjerarquias.DataBind();
            int Identity = 1;
            //System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();

            foreach (DataRow dr in initialDataSource.Rows)
            {

                System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series();
                series.Name = dr[0].ToString();
                series.XValueMember = "Efectividad";
                series.YValueMembers = "CantidadControl";
                series.ChartType = SeriesChartType.Point;
                series.MarkerSize = 27;
                //series.LabelBackColor = System.Drawing.Color.Green;
                int iteracion = 0;
                for (int i = 1; i < initialDataSource.Columns.Count; i++)
                {

                    //series.Legend = dr[0].ToString();
                    int y = Convert.ToInt32(dr[i].ToString());
                    //series.Points.AddXY(dr[0].ToString(), y);
                    //series.Points.AddY(y);
                    if (i == 1)
                    {
                        series.Points.AddXY("Deficiente", y);
                        //series.Points[i].Label = y.ToString();
                    }
                    if (i == 2)
                    {
                        series.Points.AddXY("Regular", y);
                        //series.Points[i].Label = y.ToString();
                    }
                    if (i == 3)
                    {
                        series.Points.AddXY("Bueno", y);
                        //series.Points[i].Label = y.ToString();
                    }
                    if (i == 4)
                    {
                        series.Points.AddXY("Excelente", y);
                        //.Points[i].Label = y.ToString();
                    }
                }
                //Chart4.Legends[0].Enabled = true;
                ChartResponsable.Series.Add(series);
                //ChartResponsable.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                
            }
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartJerarquias.Series)
            {
                foreach (DataPoint point in charts.Points)
                {

                    point.Label = string.Format("{0:0}", point.YValues[0]);

                }
            }
            //Chart4.Legends[0].Enabled = true;

        }
        protected void GVjerarquias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str;
            int RowGridRiesgos = Convert.ToInt16(e.CommandArgument);
            GridViewRow row = GVjerarquias.Rows[RowGridRiesgos];
            string Jerarquia = row.Cells[0].Text;
            if (ddlTipoReporte.SelectedValue == "1")
            {
                LoadInfoReporteControles();
            }
            if (ddlTipoReporte.SelectedValue == "2")
                LoadInfoReporteControlvsJerarquia();
            str = "window.open('ViewDetalleControl/ViewDetalleControl.aspx?Jerarquia=" + Jerarquia + "','Visualizar','Width=1200,Height=680,left=50,top=0,scrollbars=yes,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
            
        }

        protected void ImbCleanFiltro_Click(object sender, ImageClickEventArgs e)
        {
            Dbutton.Visible = false;
            cblEfectividades.ClearSelection();
            ddlTipoReporte.SelectedIndex = 0;
            ddlRiesgoGlobal.Items.Clear();
            ddlCadenaValor.Items.Clear();
            ddlMacroproceso.Items.Clear();
            ddlProceso.Items.Clear();
            ddlSubproceso.Items.Clear();
            ddlAreas.Items.Clear();
            ddlClasificacionGeneral.Items.Clear();
            ddlFactorRiesgo.Items.Clear();
            DDLPlanEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Clear();
            cbComparativo.Checked = false;
            txbFechaInicial.Text = string.Empty;
            txbFechaFinal.Text = string.Empty;
            txbPerfilInicial.Text = string.Empty;
            txbPerfilFinal.Text = string.Empty;

            dvTablaEfectividad.Visible = false;
            dvGraficosEficacia.Visible = false;
            dvJerarquias.Visible = false;
            dvTableJerarquias.Visible = false;
            dvResponsables.Visible = false;
            trEfectividad.Visible = false;
            trJerarquia.Visible = false;
            trHistoricoInicial.Visible = false;
            trHistoricoFinal.Visible = false;
            trPerfilHistoricoInicial.Visible = false;
            trPerfilHistoricoFinal.Visible = false;
        }
    }
}