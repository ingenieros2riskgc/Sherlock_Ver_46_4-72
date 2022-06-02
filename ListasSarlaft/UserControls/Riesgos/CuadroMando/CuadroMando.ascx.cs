using ListasSarlaft.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Riesgos.CuadroMando
{
    public partial class CuadroMando : System.Web.UI.UserControl
    {
        string IdFormulario = "5030";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
        }

        protected void ImbConsolidado_Click(object sender, ImageClickEventArgs e)
        {
            dvConsolidado.Visible = true;
            dvButtonsCuadro.Visible = false;
            HeadCM.Visible = false;
        }

        protected void ImbRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            dvRiesgos.Visible = true;
            dvButtonsCuadro.Visible = false;
            HeadCM.Visible = false;
        }

        protected void ImbControles_Click(object sender, ImageClickEventArgs e)
        {
            dvControles.Visible = true;
            dvButtonsCuadro.Visible = false;
            HeadCM.Visible = false;
        }

        protected void ImbEventos_Click(object sender, ImageClickEventArgs e)
        {
            dvEventos.Visible = true;
            dvButtonsCuadro.Visible = false;
            HeadCM.Visible = false;
        }

        protected void ImbIndicador_Click(object sender, ImageClickEventArgs e)
        {
            dvIndicadores.Visible = true;
            dvButtonsCuadro.Visible = false;
            HeadCM.Visible = false;
        }
    }
}