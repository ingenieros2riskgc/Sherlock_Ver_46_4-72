using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class ReportePlanAuditoria : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.PreviousPage != null)
            {
                Control placeHolder = Page.PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                Control usercontrol = placeHolder.FindControl("PlanAuditoria");
                TextBox txtIdPlaneacion = (TextBox)usercontrol.FindControl("txtCodPlaneacion");
                txtCodPlaneacion.Text = txtIdPlaneacion.Text;
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}