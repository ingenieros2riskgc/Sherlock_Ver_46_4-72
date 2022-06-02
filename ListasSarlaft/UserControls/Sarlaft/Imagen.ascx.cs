using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls
{
    public partial class Imagen : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Image1.ImageUrl = "../Archivos/ImagenesROI/" + Request.QueryString["url"].ToString().Trim();
            }
        }
    }
}