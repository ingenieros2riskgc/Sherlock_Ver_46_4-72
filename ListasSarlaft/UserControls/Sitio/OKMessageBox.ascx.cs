using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Sitio
{
    public partial class OKMessageBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //The fnClickOk javascript function will force the btnOk button to post back.
            btnOk.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk.UniqueID, "");
            btnOk.Focus();
        }

        public void ShowMessage(string Message, int TipoImagen)
        {
            
            switch (TipoImagen)
            {
                case 1: //Imagen de Error
                    imgInfo.ImageUrl="~/Imagenes/Icons/icontexto-webdev-cancel.png";
                    break;
                case 2: //Imagen de Advertencia
                    imgInfo.ImageUrl="~/Imagenes/Icons/icontexto-webdev-alert.png";
                    break;
                case 3: //Imagen de Ejecucion Satisfactoria
                    imgInfo.ImageUrl="~/Imagenes/Icons/icontexto-webdev-ok.png";
                    break;
            }

            lblMessage.Text = Message;
            lblCaption.Text = "";
            tdCaption.Visible = false;
            mpext.Show();
        }

        public void ShowMessage(string Message, int TipoImagen, string Caption)
        {

            switch (TipoImagen)
            {
                case 1: //Imagen de Error
                    imgInfo.ImageUrl = "~/Imagenes/Icons/icontexto-webdev-cancel.png";
                    break;
                case 2: //Imagen de Advertencia
                    imgInfo.ImageUrl = "~/Imagenes/Icons/icontexto-webdev-alert.png";
                    break;
                case 3: //Imagen de Ejecucion Satisfactoria
                    imgInfo.ImageUrl = "~/Imagenes/Icons/icontexto-webdev-ok.png";
                    break;
            }

            lblMessage.Text = Message;
            lblCaption.Text = Caption;
            tdCaption.Visible = true;
            mpext.Show();
        }

        private void Hide()
        {
            lblMessage.Text = "";
            lblCaption.Text = "";
            mpext.Hide();
        }

        public void btnOk_Click(object sender, EventArgs e)
        {
            OnOkButtonPressed(e);
        }

        public delegate void OkButtonPressedHandler(object sender, EventArgs args);
        public event OkButtonPressedHandler OkButtonPressed;
        protected virtual void OnOkButtonPressed(EventArgs e)
        {
            if (OkButtonPressed != null)
                OkButtonPressed(btnOk, e);
        }
    }
}