using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ListasSarlaft.UserControls
{
    public partial class Reporte : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadValue();
            }
        }

        private void loadValue()
        {
            try
            {
                int row;
                row = Convert.ToInt16(Request.QueryString["row"]);
                reporte(row);           
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar la información. " + ex.Message);
            }            
        }

        private void reporte(int row)
        {
            Label3.Text = InfoGrid.Rows[row]["IdLista"].ToString().Trim();
            Label5.Text = InfoGrid.Rows[row]["TipoDocumento"].ToString().Trim();
            Label7.Text = InfoGrid.Rows[row]["DocumentoIdentidad"].ToString().Trim();
            Label9.Text = InfoGrid.Rows[row]["NombreCompleto"].ToString().Trim();
            Label11.Text = InfoGrid.Rows[row]["CodigoClaseLisa"].ToString().Trim();
            Label13.Text = InfoGrid.Rows[row]["NombreClaseLista"].ToString().Trim();
            Label15.Text = InfoGrid.Rows[row]["FuenteConsulta"].ToString().Trim();
            Label17.Text = InfoGrid.Rows[row]["TipoPersona"].ToString().Trim();
            Label19.Text = InfoGrid.Rows[row]["Alias"].ToString().Trim();
            Label21.Text = InfoGrid.Rows[row]["Delito"].ToString().Trim();
            Label23.Text = InfoGrid.Rows[row]["Zona"].ToString().Trim();
            Label27.Text = InfoGrid.Rows[row]["Link"].ToString().Trim();
            Label25.Text = InfoGrid.Rows[row]["OtraInformacion"].ToString().Trim();
            Label28.Text = InfoGrid.Rows[row]["Estado"].ToString().Trim();
        }

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
                infGrid = (DataTable)Session["infGrid"];
                return infGrid;
            }
        }

        #endregion
    }
}