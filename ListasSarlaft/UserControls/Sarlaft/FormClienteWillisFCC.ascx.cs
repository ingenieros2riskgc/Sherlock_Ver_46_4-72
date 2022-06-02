using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.IO;
using System.Data;
using System.Web.SessionState;
using Microsoft.Security.Application;
using System.Net;

namespace ListasSarlaft.UserControls
{
    public partial class FormClienteWillisFCC : System.Web.UI.UserControl
    {
        string IdFormulario = "6007";
        private cKnowClient cKnowClient = new cKnowClient();
        cCuenta cCuenta = new cCuenta();
        public string URL_FCC = System.Configuration.ConfigurationManager.AppSettings["URL_FCC"].ToString().Trim();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
            else
            {
                if (cCuenta.permisosConsulta(IdFormulario) == "False")
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void ImgBtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IframeFCC.Visible = true;
                URL_FCC += "Home/Find?TypeDocument=" + DDLTipoIden.SelectedValue.ToString() + "&Document=" + TxtNumeroIden.Text.Trim() + "&TypePerson=" + DDLTipoPersona.SelectedValue.ToString() + "&UserWP=" + ConfigurationManager.AppSettings["UsuarioWillis"].ToString();
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(URL_FCC));
                webReq.Method = "GET";
                StreamReader reader = new StreamReader(webReq.GetResponse().GetResponseStream());
                IFrame_1.Attributes.Add("src", URL_FCC);
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar información.\nDescripción:" + ex.Message);
            }
        }
    }
}
