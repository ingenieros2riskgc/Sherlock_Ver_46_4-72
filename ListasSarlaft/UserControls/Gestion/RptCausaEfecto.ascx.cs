using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class RptCausaEfecto : System.Web.UI.UserControl
    {
                     
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
       
        #region Propierties

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)ViewState["infGrid"];
                return infGrid;
            }
            set
            {
                infGrid = value;
                ViewState["infGrid"] = infGrid;
            }
        }

        private int idexRow;
        private int IdexRow
        {
            get
            {
                idexRow = (int)ViewState["idexRow"];
                return idexRow;
            }
            set
            {
                idexRow = value;
                ViewState["idexRow"] = idexRow;
            }
        }

        #endregion
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
        }
    }
}