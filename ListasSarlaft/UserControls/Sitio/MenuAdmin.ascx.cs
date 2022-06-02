using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls
{
    public partial class MenuAdmin : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["NombreUsuario"] != null && Session["Usuario"] != null)
                {
                    TBmenu.Visible = true;
                    this.LblNombreDato.Text = Session["NombreUsuario"].ToString().Trim();
                    this.LblUsuarioDato.Text = Session["Usuario"].ToString().Trim();
                    this.LblNombreTime.Text = System.DateTime.Now.ToString().Trim();
                }
                /*else
                {
                    Response.Redirect("~/Formularios/Sitio/Login.aspx");
                }*/
            }
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            TBmenu.Visible = false;
            //cCuenta.Login_SN(Session["IdUsuario"].ToString().Trim(), 0);
            Session.Abandon();
            cCuenta.notAuthenticated();
            Response.Redirect("~/Formularios/Sitio/Login.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            TBmenu.Visible = false;
            Session.Abandon();
            //cCuenta.Login_SN(Session["IdUsuario"].ToString().Trim(), 0);
            cCuenta.notAuthenticated();
            Response.Redirect("~/Formularios/Sitio/Login.aspx");
        }
    }
}