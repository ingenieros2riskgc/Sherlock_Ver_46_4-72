using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
//using ListasSarlaft.Classes;
using Microsoft.Security.Application;
using ClosedXML.Excel;
using System.IO;
using System.Data;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class ReportePerfiles : System.Web.UI.UserControl
    {
        string IdFormulario = "11004";
        clsCuenta cCuenta = new clsCuenta();
        ListasSarlaft.Classes.cCuenta ccCuenta = new ListasSarlaft.Classes.cCuenta();

        #region Properties
        private int pagIndexInfoGridReportePerfiles;
        private DataSets.DataTable infoGridReportePerfiles;

        private int PagIndexInfoGridReportePerfiles
        {
            get
            {
                pagIndexInfoGridReportePerfiles = (int)ViewState["pagIndexInfoGridReportePerfiles"];
                return pagIndexInfoGridReportePerfiles;
            }
            set
            {
                pagIndexInfoGridReportePerfiles = value;
                ViewState["pagIndexInfoGridReportePerfiles"] = pagIndexInfoGridReportePerfiles;
            }
        }

        private DataSets.DataTable InfoGridReportePerfiles
        {
            get
            {
                infoGridReportePerfiles = (DataSets.DataTable)ViewState["infoGridReportePerfiles"];
                return infoGridReportePerfiles;
            }
            set
            {
                infoGridReportePerfiles = value;
                ViewState["infoGridReportePerfiles"] = infoGridReportePerfiles;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
                mtdLoadDDLPerfiles();
        }

        #region Loads
        private void mtdLoadDDLPerfiles()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsPerfil cPerfil = new clsPerfil();
            List<clsDTOPerfil> lstPerfil = new List<clsDTOPerfil>();
            #endregion Vars

            lstPerfil = cPerfil.mtdCargarInfoPerfiles(ref strErrMsg);

            if (lstPerfil != null)
            {
                int intCounter = 1;
                ddlPerfil.Items.Clear();
                ddlPerfil.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOPerfil objPerfil in lstPerfil)
                {
                    ddlPerfil.Items.Insert(intCounter, new ListItem(objPerfil.StrNombrePerfil, objPerfil.StrIdPerfil));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }
        #endregion Loads

        #region Buttons
        protected void btnConsultar_Click(object sender, EventArgs e)
        {

            if (!Page.IsValid)
                return;

            string strErrMsg = string.Empty;
            DataSets.DataSet dsReporte = new DataSets.DataSet();
            clsLogica.clsPerfil cPerfil = new clsLogica.clsPerfil();

            //trRptPerfiles.Visible = true;
            //tbPerfilTemp.Text = ddlPerfil.SelectedValue.Trim() == "0" ? string.Empty : ddlPerfil.SelectedValue.Trim();

            dsReporte = cPerfil.mtdConsultarRptPerfiles(Sanitizer.GetSafeHtmlFragment(tbFechaIni.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbFechaFin.Text.Trim()),
                Sanitizer.GetSafeHtmlFragment(tbNroIdentificacion.Text.Trim()), ddlPerfil.SelectedValue.Trim() == "0" ? string.Empty : ddlPerfil.SelectedValue.Trim(),
                ref strErrMsg);

            if (dsReporte != null)
            {
                if (dsReporte.Tables[0].Rows.Count != 0)
                {
                    trButton.Visible = true;
                    InfoGridReportePerfiles  = dsReporte.Tables[0];
                    gvPerfiles.DataSource = dsReporte;
                    gvPerfiles.DataBind();
                }
                else
                {
                    gvPerfiles.DataSource = null;
                    gvPerfiles.DataBind();
                }
            }
            //if (string.IsNullOrEmpty(strErrMsg))
            //{
            //    ReportDataSource rdsSource = new ReportDataSource("DataSet1", dsReporte.Tables["DataSet1"]);

            //    ReportViewer1.LocalReport.DataSources.Clear();
            //    ReportViewer1.LocalReport.DataSources.Add(rdsSource);
            //}
            //else
            //{
            //    trRptPerfiles.Visible = false;
            //    mtdMensaje(strErrMsg);
            //}

            //ReportViewer1.LocalReport.Refresh();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            trButton.Visible = false;
            mtdLimpiarCampos();
            mtdInicializarValores();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            //mtdExportExcel(InfoGridReportePerfiles, Response, "Reporte Perfiles");
        }
        #endregion Buttons

        protected void TheReport_ReportError(object sender, ReportErrorEventArgs e)
        {
            e.Handled = true;
        }

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdInicializarValores()
        {
            PagIndexInfoGridReportePerfiles = 0;
        }

        private void mtdLimpiarCampos()
        {
            tbFechaFin.Text = string.Empty;
            tbFechaIni.Text = string.Empty;
            tbNroIdentificacion.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0;
            tbPerfilTemp.Text = string.Empty;
            //trRptPerfiles.Visible = false;
        }

        private static void mtdExportExcel(DataSets.DataTable dtInfo, HttpResponse Response, string filename)
        {
            //Response.Clear();
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xls");
            //Response.ContentEncoding = System.Text.Encoding.Default;
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            //System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            //dg.DataSource = dtInfo;
            //dg.DataBind();
            //dg.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();
        }

        public void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (tbFechaIni.Text != String.Empty && tbFechaFin.Text == String.Empty)
            {
                CustomValidator1.ErrorMessage = "Debe ingresar una fecha fin";
                args.IsValid = false;

            }
            else if (tbFechaIni.Text == String.Empty && tbFechaFin.Text != String.Empty)
            {
                CustomValidator1.ErrorMessage = "Debe ingresar una fecha inicio";
                args.IsValid = false;
            }
        }



        #endregion Methods

        protected void gvPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReportePerfiles = e.NewPageIndex;
            gvPerfiles.PageIndex = PagIndexInfoGridReportePerfiles;
            gvPerfiles.DataSource = InfoGridReportePerfiles;
            gvPerfiles.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReportePerfiles, Response, "Reporte Perfiles");
        }
        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {
            /*Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            dg.DataSource = dt;
            dg.DataBind();
            dg.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();*/
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(dt);
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