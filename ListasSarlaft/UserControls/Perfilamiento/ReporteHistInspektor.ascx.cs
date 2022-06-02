using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
//using ListasSarlaft.Classes;
using Microsoft.Reporting.WebForms;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class ReporteHistInspektor : System.Web.UI.UserControl
    {
        string IdFormulario = "11005";
        clsCuenta cCuenta = new clsCuenta();
        ListasSarlaft.Classes.cCuenta ccCuenta = new ListasSarlaft.Classes.cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
        }

        #region Buttons
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            DataSets.DataSet dsReporte = new DataSets.DataSet();
            clsPerfil cPerfil = new clsPerfil();

            trRptPerfiles.Visible = true;

            dsReporte = cPerfil.mtdConsultarRptHistInspektor(Sanitizer.GetSafeHtmlFragment(tbFechaIni.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbFechaFin.Text.Trim()),
               Sanitizer.GetSafeHtmlFragment(tbNroIdentificacion.Text.Trim()), ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
            {
                ReportDataSource rdsSource = new ReportDataSource("DataSet1", dsReporte.Tables["DataSet1"]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rdsSource);
            }
            else
            {
                trRptPerfiles.Visible = false;
                mtdMensaje(strErrMsg);
            }

            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdLimpiarCampos();           
        }
        #endregion Buttons

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdLimpiarCampos()
        {
            tbFechaFin.Text = string.Empty;
            tbFechaIni.Text = string.Empty;
            tbNroIdentificacion.Text = string.Empty;            
            tbPerfilTemp.Text = string.Empty;
            trRptPerfiles.Visible = false;
        }
        #endregion Methods

        protected void TheReport_ReportError(object sender, ReportErrorEventArgs e)
        {
            e.Handled = true;
        }
    }
}