using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


namespace ListasSarlaft.UserControls.Proceso
{
    public partial class ucMapaProcesos : System.Web.UI.UserControl
    {
        string IdFormulario = "4019";
        cCuenta cCuenta = new cCuenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            string strErrMsg = string.Empty;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                //if (!Page.IsPostBack)
                //{
                if (!mtdCargarProcesos(ref strErrMsg))
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                //}
            }
        }

        private bool mtdCargarProcesos(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsCadenaValor> lstCadenas = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadena = new clsCadenaValorBLL();
            clsMacroProcesoBLL cMP = new clsMacroProcesoBLL();

            booResult = cCadena.mtdConsultarCadenaValor(true, ref lstCadenas, ref strErrMsg);

            if (booResult)
            {
                foreach (clsCadenaValor objCadena in lstCadenas)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow tRowC = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell tCellC = new System.Web.UI.HtmlControls.HtmlTableCell();
                    tblProcesos.Rows.Add(tRowC);
                    tRowC.Cells.Add(tCellC);

                    Label lblCadenas = new Label();
                    lblCadenas.BackColor = System.Drawing.Color.Blue;
                    lblCadenas.ForeColor = System.Drawing.Color.White;
                    lblCadenas.Text = objCadena.strNombreCadenaValor;
                    lblCadenas.Width = 400;

                    tCellC.Controls.Add(lblCadenas);

                    List<clsMacroproceso> lstMP = new List<clsMacroproceso>();
                    booResult = cMP.mtdConsultarMacroproceso(true, objCadena, ref lstMP, ref strErrMsg);

                    if (booResult)
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow tRowMP = new System.Web.UI.HtmlControls.HtmlTableRow();
                        tblProcesos.Rows.Add(tRowMP);
                        System.Web.UI.HtmlControls.HtmlTableCell tCellMP = new System.Web.UI.HtmlControls.HtmlTableCell();
                        int i = 0;
                        tRowMP.Cells.Add(tCellMP);
                        if (lstMP != null)

                            foreach (clsMacroproceso objMP in lstMP)
                            {

                                Button btnMP = new Button();
                                //Clean house.
                                //graphicImage.Dispose();
                                //bitMapImage.Dispose();
                                //btnimgB.ImageUrl = "~/Imagenes/Aplicacion/circuloProceso.png";
                                //btnMP.BackColor = System.Drawing.Color.LightBlue;
                                btnMP.Text = objMP.strNombreMacroproceso;
                                btnMP.CssClass = "btnMapaProcesos";
                                btnMP.CommandName = "Caraterizacion";
                                btnMP.CommandArgument =
                                    string.Format("CV={0}&P={1}", objCadena.intId, objMP.intId);
                                btnMP.Click += (s, e) =>
                                {
                                    //omb.ShowMessage("Estamos en construcción", 2, "Atención");
                                    Response.Redirect("~\\Formularios\\Proceso\\Admin\\PrcVerCaracterizacion.aspx?IdProceso=" + objMP.intId);
                                    //string str = "window.open('ReporteKnowCliente.aspx?IdConocimientoCliente=1','Reporte','width=950px,height=900px,scrollbars=yes,resizable=yes')";
                                    //str = "window.open('.aspx?" + ((Button)s).CommandArgument.ToString() + "','Caraterización','width=950px,height=900px,scrollbars=yes,resizable=yes')";
                                    //Response.Write("<script languaje=javascript>" + str + "</script>");

                                };

                                //btnMP.Click += new EventHandler(btnMP_Click);

                                tCellMP.Controls.Add(btnMP);
                                //tCellMP.Controls.Add(btnimgB);
                                i++;
                            }


                    }
                    else
                    {
                        strErrMsg = "Error al cargar la información";
                    }
                }
            }
            else
            {
                strErrMsg = "La información no esta completa";
            }

            return booResult;
        }

        //private void btnMP_Click(object sender, CommandEventArgs e)
        //private void btnMP_Click(object sender, EventArgs e)
        //{
        //    System.Web.UI.ClientScriptManager csm = Page.ClientScript;

        //    csm.RegisterClientScriptBlock(this.GetType(), "lnk",
        //    "<script type = 'text/javascript'>alert('LinkButton Clicked');</script>");

        //    //Button btn = sender as Button;
        //    //omb.ShowMessage(btn.CommandArgument.ToString(), 3, "Atención");

        //}
    }
}