using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.Formularios.Auditoria.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlTipoHallazgo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            Panel3.Visible = true;
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView3.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                //txtbMaestro.Text = "";
                //txtrowid .Text  = Gridview1.SelectedRow.Cells[1].Text.Trim(); en C#
                //TextBox1.Text = GridView1.SelectedRow.Cells(0).Text
                //txtNombre.Text = GridView1.SelectedRow.Cells(1).Text
                //txtMod.Text = GridView1.SelectedRow.Cells(2).Text
                //btnImgInsertar.Visible = false;
                //btnImgActualizar.Visible = true;
                Panel3.Visible = true;
            }

        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Panel3.Visible = false;
        }
        protected void imgBtnInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            Panel2.Visible = true;
        }

        protected void btnImgCancelarRec_Click_Click(object sender, ImageClickEventArgs e)
        {
            Panel2.Visible = false;
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView5.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                Panel2.Visible = true;
            }
        }

        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView7.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                Panel1.Visible = true;
            }
        }
        protected void imgBtnInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void btnImgCancelarRie_Click_Click(object sender, ImageClickEventArgs e)
        {
            Panel1.Visible = false;
        }

        protected void btnClosepp1_Click(object sender, ImageClickEventArgs e)
        {
            //ImageButton5_PopupControlExtender.Commit("");
        }

        protected void ddlProbabilidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlImpacto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    



    }
}