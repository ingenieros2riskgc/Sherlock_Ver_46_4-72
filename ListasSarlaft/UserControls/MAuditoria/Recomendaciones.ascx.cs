using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using System.IO;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Recomendaciones : System.Web.UI.UserControl
    {
        string IdFormulario = "3006";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();

        private static int LastInsertIdCE;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.BtnAdjuntar);
            scriptManager.RegisterPostBackControl(this.GridView100);
        }

        #region Buttons
        protected void imgBtnAuditoria_Click(object sender, ImageClickEventArgs e) { }

        protected void btnImgCancelarRec_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRecomendacion.Visible = false;
            filaGridRec.Visible = true;
            filaCierreRec.Visible = true;
        }

        protected void btnImgInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    //Inserta el maestro del nodo hijo
                    try
                    {
                        SqlDataSource25.InsertParameters["IdRecomendacion"].DefaultValue = txtCodRec.Text;
                        SqlDataSource25.InsertParameters["Estado"].DefaultValue = ddlEstado.SelectedValue;
                        SqlDataSource25.InsertParameters["Observacion"].DefaultValue = txtDescAct.Text;
                        SqlDataSource25.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        SqlDataSource25.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                        SqlDataSource25.Insert();
                    }
                    catch (Exception except)
                    {
                        //Handle the Exception.
                        omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }

                    if (!err)
                    {
                        try
                        {
                            SqlDataSource24.UpdateParameters["IdRecomendacion"].DefaultValue = txtCodRec.Text;
                            SqlDataSource24.UpdateParameters["Estado"].DefaultValue = ddlEstado.SelectedValue;
                            SqlDataSource24.Update();
                        }
                        catch (Exception except)
                        {
                            //Handle the Exception.
                            omb.ShowMessage("Error en la Actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                            err = true;
                        }

                        if (!err)
                        {
                            omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                            filaDetalleRecomendacion.Visible = false;
                            filaGridRec.Visible = true;
                        }
                    }
                }
            }
        }

        protected void btnImgCancelarLog_Click(object sender, ImageClickEventArgs e)
        {
            filaLogEstados.Visible = false;
            filaGridRec.Visible = true;
            filaCierreRec.Visible = true;
        }

        protected void btnCierreRec_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "CERRAR";
                lblMsgBox.Text = "Desea cerrar todas las recomendaciones de la auditoría?";
                mpeMsgBox.Show();
            }

        }

        protected void btnRevertirEstado_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "REVERTIR";
                lblMsgBox.Text = "Desea devolver la Auditoria a estado de Verificación?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgok_Click(object sender, EventArgs e)
        {
            bool err = false;
            string TextoAdicional = "", selectCommand;
            int nodoAuditoria = 0;

            mpeMsgBox.Hide();

            if (lblAccion.Text == "REVERTIR")
            {
                try
                {
                    SqlDataSource2.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource2.Update();
                    cAu.ActualizarLogHistoricoAudutoria(txtCodAuditoriaSel.Text, "NULL", "GETDATE()", "NULL", "SI");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización del estado." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                    //Trae el nodo del grupo de Auditoria
                    string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
                    selectCommand = "SELECT JO.idHijo FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO WHERE JO.TipoArea = 'A'";
                    SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                    DataTable dtblDiscuss = new DataTable();
                    dad.Fill(dtblDiscuss);

                    DataView view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        nodoAuditoria = Convert.ToInt32(row["idHijo"].ToString().Trim());
                    }

                    TextoAdicional = "Planeación Código: " + txtCodPlaneacion.Text + ", Nombre: " + txtNomPlaneacion.Text + "<br>";
                    TextoAdicional = TextoAdicional + "Auditoría Código: " + txtCodAuditoriaSel.Text + ", Nombre: " + txtNomAuditoriaSel.Text + "<div><br></div>";

                    boolEnviarNotificacion(1, Convert.ToInt32(txtCodAuditoriaSel.Text.Trim()), nodoAuditoria, "", TextoAdicional);
                }
            }
            else if (lblAccion.Text == "CERRAR")
            {
                try
                {
                    SqlDataSource26.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource26.UpdateParameters["Estado"].DefaultValue = "CUMPLIDA";
                    SqlDataSource26.Update();
                    cAu.CerrarLogHistoricoAudutoria(txtCodAuditoriaSel.Text, "NULL", "NULL", "NULL", "NO", "GETDATE()");

                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización del estado de la auditoría." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

            }

            if (!err)
            {
                txtNomPlaneacion.Text = "";
                txtCodPlaneacion.Text = "";
                txtCodAuditoriaSel.Text = "";
                txtCodObjetivoSel.Text = "";
                txtCodEnfoqueSel.Text = "";
                txtCodLiteralSel.Text = "";
                txtCodHallazgoSel.Text = "";
                txtNomAuditoriaSel.Text = "";
                txtNomObjetivoSel.Text = "";
                txtNomEnfoqueSel.Text = "";
                txtNomLiteralSel.Text = "";
                txtNomHallazgoSel.Text = "";
                txtNomEnfoqueSel.Height = 18;
                txtNomEnfoqueSel.Width = 402;
                txtNomLiteralSel.Height = 18;
                txtNomLiteralSel.Width = 402;
                txtNomHallazgoSel.Height = 18;
                txtNomHallazgoSel.Width = 402;
                filaGridRec.Visible = false;
                filaCierreRec.Visible = false;
                GridView8.DataBind();
                imgBtnAuditoria.Focus();
            }
        }

        protected void bntInformeRec_Click(object sender, EventArgs e)
        {
            string str = "window.open('AudAdmReporteSeguimiento.aspx?Ca=" + txtCodAuditoriaSel.Text + "','Reporte','width=800,height=600,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }
        #endregion

        protected void ddlEstado_DataBound(object sender, EventArgs e)
        {
            ddlEstado.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

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

            if (filaGridAud.Visible == true)
                filaGridAud.Visible = false;

            if (filaSubirAud.Visible == true)
                filaSubirAud.Visible = false;

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

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Variables

            try
            {
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = " + idNodoJerarquia;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = row["CorreoResponsable"].ToString().Trim();
                        idJefeInmediato = row["idPadre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeInmediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                            idJefeMediato = row["idPadre"].ToString().Trim();
                        }
                    }
                }
                #endregion

                #region correo del Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeMediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        }
                    }
                }
                #endregion

                //Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = "1"; //Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource200.Insert();
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = "1";//Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource201.Insert();
                }

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    //MailAddress fromAddress = new MailAddress("risksherlock@hotmail.com", "Software Sherlock");
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                    message.From = fromAddress;//here you can set address

                    #region Destinatario
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region Copia
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region Otros
                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                    #endregion
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envio de la notificacion." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource200.Update();
                }
            }

            return (err);
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        #region adjuntos
        protected void BtnAdjuntar_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                mtdCargarPdfAud();
                GridView100.DataBind();
                omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                VerAdjuntos();
            }
            else
            {
                lblMsgBox.Text = "No hay archivos para cargar.";
                mpeMsgBox.Show();
            }
        }

        private void mtdCargarPdfAud()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cAu.loadCodigoArchivoAud();

            #region Nombre Archivo

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}",
                    FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cAu.mtdAgregarArchivoAud(txtCodAuditoriaSel.Text.Trim(), txtDescArchivo.Text.Trim(), strNombreArchivo, bPdfData);

            filaGridAud.Visible = true;
            filaSubirAud.Visible = false;
        }

        protected void mtdDescargarPdfAud(string strNombreArchivo)
        {
            #region Vars
            byte[] bPdfData = cAu.mtdDescargarArchivoAud(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                //Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }

        protected void bntAdjuntos_Click(object sender, EventArgs e)
        {
            VerAdjuntos();
        }

        private void VerAdjuntos()
        {
            filaGridAud.Visible = true;
            filaCierreRec.Visible = false;
            filaSubirAud.Visible = false;
            txtDescArchivo.Text = string.Empty;
            string CodRegAuditoria = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
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

        protected void imgBtnInsertarArchivoAud_Click(object sender, ImageClickEventArgs e)
        {
            filaSubirAud.Visible = true;
        }

        protected void btnImgCancelarArchivoAud_Click(object sender, ImageClickEventArgs e)
        {
            filaSubirAud.Visible = false;
        }

        protected void btnImgCancelarAdjunto_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAud.Visible = false;
            filaSubirAud.Visible = false;
            filaCierreRec.Visible = true;
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
    }
}