using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class ReporteAuditoriaSeg : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Page.PreviousPage != null)
                {
                    Control placeHolder = Page.PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                    Control usercontrol = placeHolder.FindControl("Seguimiento");
                    TextBox txtIdAuditoria = (TextBox)usercontrol.FindControl("txtCodAuditoriaSel");

                    //                TextBox SourceTextBox = (TextBox)Page.PreviousPage.FindControl("txtId");
                    if (txtIdAuditoria != null)
                    {
                        txtCodAuditoria.Text = txtIdAuditoria.Text;
                    }
                }
                else
                {
                    txtCodAuditoria.Text = "0";
                }

                ReportViewer1.LocalReport.Refresh();

            }
        }
    }
}