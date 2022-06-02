using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class ReporteFactorSenal : System.Web.UI.UserControl
    {
        string IdFormulario = "11008";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
                mtdLoadDDLFactores();
        }

        #region Buttons
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            trRptFactorSenal.Visible = true;

            TextBox8.Text = ddlFactorRiesgo.SelectedValue.ToString().Trim();
            ReportViewer1.LocalReport.Refresh();

            /*string strErrMsg = string.Empty;
            DataSets.DataSet dsReporte = new DataSets.DataSet();
            clsFactorRiesgo cFactorSenal = new clsFactorRiesgo();

            if (ddlFactorRiesgo.SelectedValue.Trim() != "0")
            {
                trRptFactorSenal.Visible = true;

                clsDTOFactorRiesgo objFactor = new clsDTOFactorRiesgo(ddlFactorRiesgo.SelectedValue.Trim(), string.Empty, string.Empty);

                dsReporte = cFactorSenal.mtdConsultarRelRiesgoSenal(objFactor, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    //ReportDataSource rdsSource = new ReportDataSource("DataSet1", dsReporte.Tables["DataSet1"]);
                    ReportDataSource rdsSource = new ReportDataSource("DataSet1", dsReporte.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rdsSource);
                }
                else
                {
                    trRptFactorSenal.Visible = false;
                    mtdMensaje(strErrMsg);
                }

                ReportViewer1.LocalReport.Refresh();
            }*/
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mtdLimpiarCampos();            
        }
        #endregion

        #region Loads
        private void mtdLoadDDLFactores()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsFactorRiesgo cFactor = new clsFactorRiesgo();
            List<clsDTOFactorRiesgo> lstFactores = new List<clsDTOFactorRiesgo>();
            #endregion Vars

            lstFactores = cFactor.mtdConsultarFactor(ref strErrMsg);

            if (lstFactores != null)
            {
                int intCounter = 1;
                ddlFactorRiesgo.Items.Clear();
                ddlFactorRiesgo.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOFactorRiesgo objFactor in lstFactores)
                {
                    ddlFactorRiesgo.Items.Insert(intCounter, new ListItem(objFactor.StrDescFactorRiesgo, objFactor.StrIdFactorRiesgo));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }
        #endregion

        #region Methods
        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdLimpiarCampos()
        {
            ddlFactorRiesgo.SelectedIndex = 0;
            trRptFactorSenal.Visible = false;
        }
        #endregion Methods
    }
}