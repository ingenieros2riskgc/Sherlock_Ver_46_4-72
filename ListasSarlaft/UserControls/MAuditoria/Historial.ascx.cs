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
using System.Net.Mail;
using ListasSarlaft.Classes;
using System.IO;
using Datasets = System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Historial : System.Web.UI.UserControl
    {
        string IdFormulario = "3006";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();

        private static int LastInsertIdCE;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.GridView100);
        }

        #region Buttons
        protected void imgBtnAuditoria_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAnexos.Visible = false;
            bntInformeRec.Visible = true;
            bntAdjuntos.Visible = true;
        }

        protected void btnImgCancelarRec_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRecomendacion.Visible = false;
            filaGridRec.Visible = true;
            filaCierreRec.Visible = true;
        }

        protected void btnImgCancelarLog_Click(object sender, ImageClickEventArgs e)
        {
            filaLogEstados.Visible = false;
            filaGridRec.Visible = true;
            filaCierreRec.Visible = true;
        }

        protected void bntInformeRec_Click(object sender, EventArgs e)
        {
            string str = "window.open('AudAdmReporteSeguimiento.aspx?Ca=" + txtCodAuditoriaSel.Text + "','Reporte','width=800,height=600,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }
        #endregion

        #region adjuntos
        protected void mtdDescargarPdfAud(string strNombreArchivo)
        {
            #region Vars
            byte[] bPdfData = cAu.mtdDescargarArchivoAud(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }
        protected void bntAdjuntos_Click(object sender, EventArgs e)
        {
            filaGridAnexos.Visible = true;
            string CodRegAuditoria =  Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
            loadGridAdjuntos();
            loadInfoAdjuntos(CodRegAuditoria);
        }

        private void loadGridAdjuntos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridAdjuntos = grid;
            GridView100.DataSource = InfoGridAdjuntos;
            GridView100.DataBind();
          
        }

        private void loadInfoAdjuntos(string CodRegAuditoria)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAu.ArchivoAud(CodRegAuditoria);
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAdjuntos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                        });
                }
                GridView100.DataSource = InfoGridAdjuntos;
                GridView100.DataBind();
            }
        }

        private DataTable infoGridAdjuntos;
        private DataTable InfoGridAdjuntos
        {
            get
            {
                infoGridAdjuntos = (DataTable)ViewState["infoGridAdjuntos"];
                return infoGridAdjuntos;
            }
            set
            {
                infoGridAdjuntos = value;
                ViewState["infoGridAdjuntos"] = infoGridAdjuntos;
            }
        }

        private int rowGridArchivoAud;
        private int RowGridArchivoAud
        {
            get
            {
                rowGridArchivoAud = (int)ViewState["rowGridArchivoAud"];
                return rowGridArchivoAud;
            }
            set
            {
                rowGridArchivoAud = value;
                ViewState["rowGridArchivoAud"] = rowGridArchivoAud;
            }
        }


        protected void btnImgCancelarAdjunto_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAnexos.Visible = false;
        }
       
        protected void GridView100_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoAud = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    string strNombreArchivo = InfoGridAdjuntos.Rows[RowGridArchivoAud]["UrlArchivo"].ToString().Trim();
                    mtdDescargarPdfAud(strNombreArchivo);
                    break;
            }
        }
        #endregion

        #region Gridview
        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();
            txtCodAuditoriaSel.Text = "";
            txtNomAuditoriaSel.Text = "";
            if (filaGridRec.Visible == true)
                filaGridRec.Visible = false;

            filaCierreRec.Visible = false;
            filaLogEstados.Visible = false;
            filaDetalleRecomendacion.Visible = false;
            txtCodObjetivoSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomHallazgoSel.Text = "";
            txtCodHallazgoSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            txtNomHallazgoSel.Height = 18;
            txtNomHallazgoSel.Width = 402;

            popupPlanea.Cancel();
        }

        protected void GridView6_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodAuditoriaSel.Text = GridView6.SelectedRow.Cells[0].Text.Trim();
            txtNomAuditoriaSel.Text = GridView6.SelectedRow.Cells[1].Text.Trim();
            if (filaGridRec.Visible == true)
                filaGridRec.Visible = false;

            if (filaGridAnexos.Visible == true)
                filaGridAnexos.Visible = false;

            filaCierreRec.Visible = true;
            filaLogEstados.Visible = false;
            filaDetalleRecomendacion.Visible = false;
            txtCodObjetivoSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomHallazgoSel.Text = "";
            txtCodHallazgoSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            txtNomHallazgoSel.Height = 18;
            txtNomHallazgoSel.Width = 402;
            popupAuditoria.Cancel();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlEstado.Focus();
                txtCodRec.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtObjetivo.Text = GridView1.SelectedRow.Cells[3].Text.Trim();
                txtTipoPoD.Text = GridView1.SelectedRow.Cells[4].Text.Trim();
                if (txtTipoPoD.Text == "Procesos")
                    lblPoD.Text = "Proceso";
                else
                    lblPoD.Text = "Dependencia Auditada";
                txtNombrePoD.Text = GridView1.SelectedDataKey[1].ToString().Trim();
                txtRecomendacion.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                txtCodRecGen.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                ddlEstado.SelectedValue = null;
                txtDescAct.Text = "";
                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                GridView2.DataBind();

                int rows = 0, longMax = 0, rowsAdd = 0;
                double temp = 0;

                txtRecomendacion.Height = 18;
                txtRecomendacion.Width = 402;

                //Revisa la longitud max del texto y el número de líneas
                foreach (string strItem in Regex.Split(GridView1.SelectedRow.Cells[1].Text, "</div>"))
                {
                    rows = rows + 1;
                    if (strItem.Length > longMax) longMax = strItem.Length;
                    if (strItem.Length > 126)
                    {
                        temp = strItem.Length / 126;
                        rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                    }
                }

                if (rows + rowsAdd > 1) txtRecomendacion.Height = (rows + rowsAdd) * 18;

                if (longMax > 72)
                    txtRecomendacion.Width = 700;

                else
                    txtRecomendacion.Width = 402;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandArgument.ToString() == "ActualizarEstado")
            {
                filaGridRec.Visible = false;
                filaCierreRec.Visible = false;
                filaDetalleRecomendacion.Visible = true;
            }
            else if (e.CommandArgument.ToString() == "LogEstados")
            {
                filaGridRec.Visible = false;
                filaCierreRec.Visible = false;
                filaLogEstados.Visible = true;
            }
        }

        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodObjetivoSel.Text = GridView7.SelectedRow.Cells[0].Text.Trim();
            txtNomObjetivoSel.Text = GridView7.SelectedRow.Cells[1].Text.Trim();
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtCodHallazgoSel.Text = "";
            txtNomHallazgoSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            txtNomHallazgoSel.Height = 18;
            txtNomHallazgoSel.Width = 402;

            if (filaGridRec.Visible == true) filaGridRec.Visible = false;
            filaLogEstados.Visible = false;
            filaDetalleRecomendacion.Visible = false;

            popupObjetivo.Cancel();

        }

        protected void GridView9_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodEnfoqueSel.Text = GridView9.SelectedRow.Cells[0].Text.Trim();
            txtNomEnfoqueSel.Text = GridView9.SelectedRow.Cells[1].Text.Trim();
            txtCodLiteralSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            txtNomHallazgoSel.Height = 18;
            txtNomHallazgoSel.Width = 402;
            txtCodHallazgoSel.Text = "";
            txtNomHallazgoSel.Text = "";
            if (filaGridRec.Visible == true) filaGridRec.Visible = false;
            filaLogEstados.Visible = false;
            filaDetalleRecomendacion.Visible = false;
            popupEnfoque.Cancel();

            int rows = 0, longMax = 0, rowsAdd = 0;
            double temp = 0;

            //Cambia la altura y el ancho del labol de Enfoque
            //Revisa la longitud max del texto y el número de líneas
            foreach (string strItem in Regex.Split(GridView9.SelectedRow.Cells[1].Text, "</div>"))
            {
                rows = rows + 1;
                if (strItem.Length > longMax) longMax = strItem.Length;
                if (strItem.Length > 126)
                {
                    temp = strItem.Length / 126;
                    rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                }
            }

            if (rows + rowsAdd > 1) txtNomEnfoqueSel.Height = (rows + rowsAdd) * 18;

            if (longMax > 72)
                txtNomEnfoqueSel.Width = 700;
            else
                txtNomEnfoqueSel.Width = 402;

        }

        protected void GridView10_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows = 0, longMax = 0, rowsAdd = 0;
            double temp = 0;

            txtCodHallazgoSel.Text = "";
            txtNomHallazgoSel.Text = "";
            txtNomHallazgoSel.Height = 18;
            txtNomHallazgoSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;

            //Revisa la longitud max del texto y el número de líneas
            foreach (string strItem in Regex.Split(GridView10.SelectedRow.Cells[1].Text, "</div>"))
            {
                rows = rows + 1;
                if (strItem.Length > longMax) longMax = strItem.Length;
                if (strItem.Length > 126)
                {
                    temp = strItem.Length / 126;
                    rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                }
            }

            txtNomLiteralSel.Text = GridView10.SelectedRow.Cells[1].Text.Trim();
            txtCodLiteralSel.Text = GridView10.SelectedRow.Cells[0].Text.Trim();

            if (rows + rowsAdd > 1) txtNomLiteralSel.Height = (rows + rowsAdd) * 18;

            if (longMax > 72)
                txtNomLiteralSel.Width = 700;
            else
                txtNomLiteralSel.Width = 402;


            if (filaGridRec.Visible == true) filaGridRec.Visible = false;
            filaLogEstados.Visible = false;
            filaDetalleRecomendacion.Visible = false;

            popupLiteral.Cancel();

            GridView1.DataBind();
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodHallazgoSel.Text = GridView3.SelectedRow.Cells[0].Text.Trim();
            txtNomHallazgoSel.Text = GridView3.SelectedRow.Cells[1].Text.Trim();

            int rows = 0, longMax = 0, rowsAdd = 0;
            double temp = 0;

            //Cambia la altura y el ancho del labol de Enfoque
            //Revisa la longitud max del texto y el número de líneas
            foreach (string strItem in Regex.Split(GridView3.SelectedRow.Cells[1].Text, "</div>"))
            {
                rows = rows + 1;
                if (strItem.Length > longMax) longMax = strItem.Length;
                if (strItem.Length > 126)
                {
                    temp = strItem.Length / 126;
                    rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                }
            }

            if (rows + rowsAdd > 1) txtNomHallazgoSel.Height = (rows + rowsAdd) * 18;

            if (longMax > 72)
                txtNomHallazgoSel.Width = 700;
            else
                txtNomHallazgoSel.Width = 402;

            if (filaGridRec.Visible == false) filaGridRec.Visible = true;
            filaLogEstados.Visible = false;
            filaDetalleRecomendacion.Visible = false;

            popupHallazgo.Cancel();
        }
        #endregion

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (ddlEstado.SelectedValue == "0")
            {
                err = false;
                omb.ShowMessage("Debe seleccionar el Tipo de Estado.", 2, "Atención");
                ddlEstado.Focus();
            }
            else if (ValidarCadenaVacia(txtDescAct.Text))
            {
                err = false;
                omb.ShowMessage("Debe ingresar la Observación del Cambio de Estado.", 2, "Atención");
                txtDescAct.Focus();
            }

            return err;
        }

        protected Boolean ValidarCadenaVacia(string cadena)
        {
            Regex rx = new Regex(@"^-?\d+(\.\d{2})?$");
            string Espacio = "<br>";
            string Div = "<div>";
            string Div2 = "</div>";
            string b = "<b>";
            string b2 = "</b>";
            string cadena2 = "";

            cadena2 = Regex.Replace(cadena, Espacio, " ");
            cadena2 = Regex.Replace(cadena2, Div, " ");
            cadena2 = Regex.Replace(cadena2, Div2, " ");
            cadena2 = Regex.Replace(cadena2, b, " ");
            cadena2 = Regex.Replace(cadena2, b2, " ");

            if (cadena2.Trim() == "")
                return (true);
            else
                return (false);
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }
    }
}