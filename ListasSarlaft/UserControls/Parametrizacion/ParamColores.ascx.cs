using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Parametrizacion
{
    public partial class ParamColores : System.Web.UI.UserControl
    {
        string IdFormulario = "3001";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                    CargarColores();
            }
        }

        private void CargarColores()
        {
            try
            {
                DataTable DtInfo = new DataTable();
                DtInfo = cAu.LoadColores();
                txt1.Text = DtInfo.Rows[0]["ColorMinimo"].ToString().Trim();
                txt2.Text = DtInfo.Rows[0]["ColorMaximo"].ToString().Trim();
                txt3.Text = DtInfo.Rows[1]["ColorMinimo"].ToString().Trim();
                txt4.Text = DtInfo.Rows[1]["ColorMaximo"].ToString().Trim();
                txt5.Text = DtInfo.Rows[2]["ColorMinimo"].ToString().Trim();
                txt6.Text = DtInfo.Rows[2]["ColorMaximo"].ToString().Trim();
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar la información." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
            }
        }

        private void ResetValues()
        {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            txt5.Text = "";
            txt6.Text = "";
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    cAu.ActualizarColores(Sanitizer.GetSafeHtmlFragment(txt1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(txt2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(txt3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(txt4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(txt5.Text.Trim()), Sanitizer.GetSafeHtmlFragment(txt6.Text.Trim()));
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    ResetValues();
                    CargarColores();
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }
    }
}
