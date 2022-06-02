using ListasSarlaft.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class ParConLimites : System.Web.UI.UserControl
    {
        cControl cControl = new cControl();
        cCuenta cCuenta = new cCuenta();
        string IdFormulario = "5001";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    cLimite Limite = new cLimite();
                    Limite = cControl.SeleccionarLimites();
                    txtExcelenteInferior.Text = Limite.ExcelenteLimiteInferior.ToString();
                    txtExcelenteSuperior.Text = Limite.ExcelenteLimiteSuperior.ToString();
                    txtBuenoInferior.Text = Limite.BuenoLimiteInferior.ToString();
                    txtBuenoSuperior.Text = Limite.BuenoLimiteSuperior.ToString();
                    txtRegularInferior.Text = Limite.RegularLimiteInferior.ToString();
                    txtRegularSuperior.Text = Limite.RegularLimiteSuperior.ToString();
                    txtDeficienteInferior.Text = Limite.DeficienteLimiteInferior.ToString();
                    txtDeficienteSuperior.Text = Limite.DeficienteLimiteSuperior.ToString();
                }
                catch (Exception ex)
                {
                    omb.ShowMessage(ex.Message, 1, "Atención");
                }
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 3, "Atención");
                return;
            }

            if (Convert.ToDouble(txtExcelenteInferior.Text) > Convert.ToDouble(txtExcelenteSuperior.Text)
                || Convert.ToDouble(txtBuenoInferior.Text) > Convert.ToDouble(txtBuenoSuperior.Text)
                || Convert.ToDouble(txtRegularInferior.Text) > Convert.ToDouble(txtRegularSuperior.Text)
                || Convert.ToDouble(txtDeficienteInferior.Text) > Convert.ToDouble(txtDeficienteSuperior.Text))
            {
                omb.ShowMessage("El limite inferior debe ser menor que el limite superior", 2, "Atención");
                return;
            }
            try
            {
                cLimite Limite = new cLimite();
                Limite.ExcelenteLimiteInferior = Convert.ToDouble(txtExcelenteInferior.Text);
                Limite.ExcelenteLimiteSuperior =  Convert.ToDouble(txtExcelenteSuperior.Text);
                Limite.BuenoLimiteInferior = Convert.ToDouble(txtBuenoInferior.Text);
                Limite.BuenoLimiteSuperior = Convert.ToDouble(txtBuenoSuperior.Text);
                Limite.RegularLimiteInferior = Convert.ToDouble(txtRegularInferior.Text);
                Limite.RegularLimiteSuperior = Convert.ToDouble(txtRegularSuperior.Text);
                Limite.DeficienteLimiteInferior = Convert.ToDouble(txtDeficienteInferior.Text);
                Limite.DeficienteLimiteSuperior = Convert.ToDouble(txtDeficienteSuperior.Text);
                cControl.RegistrarLimites(Limite);
                omb.ShowMessage("Rango de limites registrados con exito", 3, "Atención");
            }
            catch (Exception ex)
            {
                omb.ShowMessage(ex.Message, 1, "Atención");
            }
        }
    }
}