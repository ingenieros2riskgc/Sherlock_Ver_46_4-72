using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
namespace ListasSarlaft.UserControls.Riesgos.ViewImg
{
    public partial class ViewImg : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strErrMsg = string.Empty;
                int op = Convert.ToInt32(Request["op"]);
                Byte[] file;
                if (op == 1)
                {
                    clsBLLParaImpacto cImpacto = new clsBLLParaImpacto();
                    file = cImpacto.mtdDownLoadFile(ref strErrMsg);
                }
                else
                {
                    clsBLLParaProbabilidad cProbabilidad = new clsBLLParaProbabilidad();
                    file = cProbabilidad.mtdDownLoadFile(ref strErrMsg);
                }

                if (file != null)
                {
                    ImgImpacto.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(file);
                }
                else
                {
                    lblTexto.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                
        }
    }
}