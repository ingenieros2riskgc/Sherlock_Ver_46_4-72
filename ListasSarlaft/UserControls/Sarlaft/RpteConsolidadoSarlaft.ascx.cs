using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class RpteConsolidadoSarlaft : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        
        private string[] strMonths = new string[12] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" },
        strMeses = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
        String IdFormulario = "6009";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
               
            }
        }

        
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)Session["infGrid"];
                return infGrid;
            }
        }

        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            #region Fechas Desde y Hasta
            if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim())))
            {
                Label8.Text = mtdConvertirFecha(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), 1) + " 00:00:00:000";
            }
            if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim())))
            {
                Label9.Text = mtdConvertirFecha(Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), 2) + " 23:59:59:998";
            }
            #endregion Fechas Desde y Hasta
            ReportViewer1.LocalReport.Refresh();
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            TextBox7.Text = string.Empty;
            TextBox10.Text = string.Empty;
            Label8.Text = string.Empty;
            Label9.Text = string.Empty;
            ReportViewer1.LocalReport.Refresh();
        }

        
        private string mtdConvertirFecha(string strFechaIn, int intTipoDia)
        {
            string strFechaOut = string.Empty, strMes = string.Empty, strDia = string.Empty;
            string[] strFechaPartida = strFechaIn.Split('-');

            #region Asignar Mes
            for (int i = 0; i < 12; i++)
            {
                if (strFechaPartida[0].ToString().ToUpper() == strMeses[i].ToString() ||
                    strFechaPartida[0].ToString().ToUpper() == strMonths[i].ToString())
                {
                    if ((i + 1) <= 9)
                        strMes = string.Format("0{0}", (i + 1).ToString());
                    else
                        strMes = (i + 1).ToString().Trim();
                    break;
                }
            }
            #endregion Asignar Mes

            #region Asignar Dia
            switch (intTipoDia)
            {
                case 1:
                    strDia = "01";
                    break;
                case 2:
                    switch (strMes)
                    {
                        case "02":
                            strDia = "28";
                            break;
                        case "01":
                        case "03":
                        case "05":
                        case "07":
                        case "08":
                        case "10":
                        case "12":
                            strDia = "31";
                            break;
                        case "04":
                        case "06":
                        case "09":
                        case "11":
                            strDia = "30";
                            break;

                    }
                    break;
            }
            #endregion Asignar Dia

            if (!string.IsNullOrEmpty(strMes))
            {
                strFechaOut = string.Format("{0}-{1}-{2}", strFechaPartida[1].ToString().Trim(), strMes, strDia);
            }

            return strFechaOut;
        }

    }
}