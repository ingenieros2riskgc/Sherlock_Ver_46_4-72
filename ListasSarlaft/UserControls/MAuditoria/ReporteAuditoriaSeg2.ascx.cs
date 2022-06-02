using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace ListasSarlaft.UserControls.MAuditoria
{
    
    public partial class ReporteAuditoriaSeg2 : System.Web.UI.UserControl
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //if (Page.PreviousPage != null)
                //{
                //    Control placeHolder = Page.PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                //    Control usercontrol = placeHolder.FindControl("PlanesAccion");
                //    TextBox txtIdAuditoria = (TextBox)usercontrol.FindControl("txtCodAuditoriaSel");

                //    TextBox SourceTextBox = (TextBox)Page.PreviousPage.FindControl("txtId");
                //    if (txtIdAuditoria != null)
                //    {
                //        txtCodAuditoria.Text = txtIdAuditoria.Text;
                //    }
                //}
                //else
                //{
                //    txtCodAuditoria.Text = "0";
                //}

                Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[1];
                parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("Modulo", "Paccion");
                ReportViewer1.LocalReport.SetParameters(parameters);

                string Ca;
                Ca = (Request.QueryString["Ca"]);
                txtCodAuditoria.Text = Ca;


                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                this.ReportViewer1.LocalReport.Refresh();
                this.ReportViewer1.LocalReport.DisplayName = "InformeAuditoria";

            }
            
            
 
        }

        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DSRecomendacionesHallazgo", "odsRecomendaciones"));
            e.DataSources.Add(new ReportDataSource("DSRiesgosHallazgo", "odsRiesgos"));
        }
    }
}