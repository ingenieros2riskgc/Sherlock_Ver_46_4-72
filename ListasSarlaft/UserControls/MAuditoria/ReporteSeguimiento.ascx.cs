using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class ReporteSeguimiento : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string Ca = (Request.QueryString["Ca"]);
                txtCodAuditoria.Text = Ca;

                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                this.ReportViewer1.LocalReport.DisplayName = "PreinformeAuditoria";
                this.ReportViewer1.LocalReport.Refresh();
            }
        }

        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DSRecomendacionesHallazgo", "odsRecomendaciones"));
            e.DataSources.Add(new ReportDataSource("DSRiesgosHallazgo", "odsRiesgos"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
        }
    }
}