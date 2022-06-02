using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class AuditoriaGestionTemas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            Panel20.Visible = true;
        }

        protected void imgBtnRiesgo_Click(object sender, EventArgs e)
        {
            TabContainer1.ActiveTabIndex = 2;
        }

        protected void imgBtnRecomendacion_Click(object sender, EventArgs e)
        {

            TabContainer1.ActiveTabIndex = 1;

        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblCodHallazgoRec.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
            //lblHallazgoRec.Text = GridView4.SelectedRow.Cells[4].Text.Trim();
            //lblCodHallazgoRie.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
            //lblHallazgoRie.Text = GridView4.SelectedRow.Cells[4].Text.Trim();
        }


        protected void imgBtnInsertarP2_Click(object sender, ImageClickEventArgs e)
        {
            Panel20.Visible = true;
        }

        protected void btnImgCancelarP1_Click(object sender, ImageClickEventArgs e)
        {
            Panel20.Visible = false;
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Panel3.Visible = true;
        }

        protected void imgBtnInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            //Panel3.Visible = true;
        }

        protected void btnImgCancelarRec_Click_Click(object sender, ImageClickEventArgs e)
        {
            //Panel3.Visible = false;
        }

        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Panel1.Visible = true;
        }

        protected void imgBtnInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            //Panel1.Visible = true;
        }

        protected void btnImgCancelarRie_Click_Click(object sender, ImageClickEventArgs e)
        {
            //Panel1.Visible = false;
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            //Panel11.Visible = false;
        }


        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            //TextBox1.Text = "";
            //TextBox1.Enabled = true;
            //btnImgInsertar.Visible = true;
            //btnImgActualizar.Visible = false;
            //Panel11.Visible = true;
        }



        protected void btnGestion_Click(object sender, EventArgs e)
        {
            //pnlAuditoria.Visible = false;
            //pnlGestion.Visible = true;
        }

        protected void bntIrAuditoria3_Click(object sender, EventArgs e)
        {
            //pnlAuditoria.Visible = true;
            //pnlGestion.Visible = false;
        }

        protected void bntIrAuditoria2_Click(object sender, EventArgs e)
        {
            //pnlAuditoria.Visible = true;
            //pnlGestion.Visible = false;
        }

        protected void bntIrAuditoria1_Click(object sender, EventArgs e)
        {
            //pnlAuditoria.Visible = true;
            //pnlGestion.Visible = false;
        }

    }
}