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
using System.Windows.Forms;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class PlanesAccion : System.Web.UI.UserControl
    {
        bool booPerteneceAAuditoria = false;
        private static int LastInsertId;
        private static int LastInsertIdCE;
        string IdFormulario = "3007";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                PagIndex = 0;
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
                scripManager.RegisterPostBackControl(imgBtnAdjuntar);
                scripManager.RegisterPostBackControl(GridView100);
                booPerteneceAAuditoria = mtdPerteneceAuditoria(Session["idUsuario"].ToString());
                GridView1.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
                GridView4.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");

            }
        }

        #region Buttons

        #region Botones Volver
        protected void btnVolverRecomendacion_Click(object sender, EventArgs e)
        {
            filaTabPlanAccion.Visible = true;
            filaVolverHallazgo.Visible = true;

            filaPlanAccion.Visible = false;
            filaDetallePlan.Visible = false;
            filaVolverRecomendacion.Visible = false;
            filaAvance.Visible = false;
            filaPlanAccionRiesgo.Visible = false;
            filaTabHallazgo.Visible = false;
        }

        protected void btnVolverHallazgo_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            string strConn = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            Datasets.DataSet dsInfo = new Datasets.DataSet();

            if (booPerteneceAAuditoria)
            {
                dsInfo = cAu.mtdConsultaHallazgos(Session["idUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text.Trim()), 1, strConn, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    GridView1.DataSource = dsInfo;
                    GridView1.DataBind();
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            else
            {
                dsInfo = cAu.mtdConsultaHallazgos(Session["idUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text.Trim()), 2, strConn, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (dsInfo.Tables[0].Rows.Count == 0)
                        omb.ShowMessage("El usuario no tiene Planes de Acción asignados", 1, "Atención");

                    GridView1.DataSource = dsInfo;
                    GridView1.DataBind();
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }

            filaTabHallazgo.Visible = true;

            filaVolverHallazgo.Visible = false;
            filaPlanAccion.Visible = false;
            filaTabPlanAccion.Visible = false;
            filaDetallePlan.Visible = false;
            filaVolverRecomendacion.Visible = false;
            filaAvance.Visible = false;
            filaPlanAccionRiesgo.Visible = false;
        }
        #endregion

        #region Cambiar estado
        protected void btnCierre_Click(object sender, EventArgs e)
        {
            #region Variables
            int TotalAbiertos = 0;
            bool err = false;
            #endregion

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                #region Conteo de Planes relacionados
                DataView dvDetJerarquia = (DataView)this.SqlDataSource5.Select(new DataSourceSelectArguments());
                TotalAbiertos = int.Parse(dvDetJerarquia[0]["Total"].ToString());
                #endregion

                if (TotalAbiertos > 0)
                    omb.ShowMessage("No se puede efectuar el cierre hasta que el Auditor no haya cerrado todos los Planes de Acción." + "<br/>" + "Existen  actualmente " + TotalAbiertos.ToString() + " Planes Abiertos.", 2, "Atención");
                else
                {
                    #region Actualizar Auditoria
                    //Inserta el maestro del nodo hijo
                    try
                    {
                        SqlDataSource3.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
                        SqlDataSource3.UpdateParameters["Estado"].DefaultValue = "SEGUIMIENTO";
                        SqlDataSource3.Update();
                    }
                    catch (Exception except)
                    {
                        //Handle the Exception.
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }
                    #endregion

                    if (!err)
                    {
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                        filaTabPlanAccion.Visible = false;
                        filaInformeAuditoria.Visible = false;
                    }
                }
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
                lblTipoCierre.Text = "3";
                lblMsgBox.Text = "Desea devolver la Auditoría a estado de Recomendaciones?";
                mpeMsgBox.Show();
            }
        }
        #endregion

        protected void btnImgCancelarDetPlan_Click(object sender, ImageClickEventArgs e)
        {
            filaDetallePlan.Visible = false;

            if (TabContainer1.ActiveTabIndex == 0)
                filaPlanAccion.Visible = true;
            else if (TabContainer1.ActiveTabIndex == 1)
                filaPlanAccionRiesgo.Visible = true;

            filaVolverRecomendacion.Visible = true;
        }

        protected void btnImgInsertarPlan_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;
            int nodoAuditoria = 0;
            string TextoAdicional = string.Empty, selectCommand = string.Empty;

            if (mtdVerificarCampos())
            {
                #region Insertar Info PA
                SqlDataSource25.InsertParameters["EstadoAuditado"].DefaultValue = "ABIERTO";
                SqlDataSource25.InsertParameters["EstadoAuditor"].DefaultValue = "ABIERTO";
                SqlDataSource25.InsertParameters["IdForanea"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text);
                SqlDataSource25.InsertParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescPlan.Text);
                SqlDataSource25.InsertParameters["TipoForanea"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtTipoForanea.Text);
                SqlDataSource25.InsertParameters["FechaCompromiso"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecPlan.Text);
                SqlDataSource25.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource25.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource25.Insert();
                }
                catch (Exception except)
                {
                    //Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
                #endregion

                if (!err)
                {
                    if (TabContainer1.ActiveTabIndex == 0)
                        filaPlanAccion.Visible = true;
                    else
                        filaPlanAccionRiesgo.Visible = true;

                    filaDetallePlan.Visible = false;

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");

                    #region Envio Notificacion
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

                    TextoAdicional = "CREACION DE PLAN DE ACCION" + "<br>";
                    TextoAdicional = TextoAdicional + "Planeación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br>";
                    TextoAdicional = TextoAdicional + "Auditoría Código: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoriaSel.Text) + "<br>";
                    TextoAdicional = TextoAdicional + "Plan de Acción Código: " + LastInsertId.ToString().Trim() + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtDescPlan.Text) + "<br>";

                    if (txtTipoForanea.Text == "RECOMENDACION")
                        TextoAdicional = TextoAdicional + "Recomendación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRecPlan.Text) + "<div><br></div>";
                    else if (txtTipoForanea.Text == "RIESGO")
                        TextoAdicional = TextoAdicional + "Riesgo Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRiesgoPlan.Text) + "<div><br></div>";

                    boolEnviarNotificacion(2, Convert.ToInt32(LastInsertId.ToString().Trim()), nodoAuditoria, Sanitizer.GetSafeHtmlFragment(txtFecPlan.Text), TextoAdicional);
                    #endregion
                }
            }
        }

        protected void btnImgActualizarPlan_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (mtdVerificarCampos())
                {
                    #region Actualizar Info
                    SqlDataSource2.UpdateParameters["FechaCompromiso"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecPlan.Text);
                    SqlDataSource2.UpdateParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtDescPlan.Text);
                    SqlDataSource2.UpdateParameters["IdPlanAccion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text);

                    //Inserta el maestro del nodo hijo
                    try
                    {
                        SqlDataSource2.Update();
                    }
                    catch (Exception except)
                    {
                        //Handle the Exception.
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }
                    #endregion

                    if (!err)
                    {
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                        if (TabContainer1.ActiveTabIndex == 0)
                        {
                            filaPlanAccion.Visible = true;
                            GridView4.DataBind();
                        }
                        else
                        {
                            filaPlanAccionRiesgo.Visible = true;
                            GridView5.DataBind();
                        }

                        filaDetallePlan.Visible = false;
                    }
                }
            }
        }

        protected void btnImgCancelarPlan_Click(object sender, ImageClickEventArgs e)
        {
            filaPlanAccion.Visible = false;
            filaTabPlanAccion.Visible = true;
        }

        protected void imgBtnInsertarPlan_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaTabPlanAccion.Visible = false;
                filaPlanAccion.Visible = false;
                filaPlanAccionRiesgo.Visible = false;
                filaDetallePlan.Visible = true;
                txtFecPlan.Text = "";
                txtDescPlan.Text = "";
                txtUsuarioPlan.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionPlan.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarPlan.Visible = true;
                btnImgActualizarPlan.Visible = false;
                txtFecPlan.Focus();
            }
        }

        protected void btnSI_Click(object sender, EventArgs e)
        {
            bool err = false;
            string TextoAdicional = string.Empty;

            mpeMsgBoxSN.Hide();

            try
            {
                SqlDataSource25.UpdateParameters["IdPlanAccion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text);
                SqlDataSource25.UpdateParameters["EstadoAuditado"].DefaultValue = "CERRADO";
                SqlDataSource25.UpdateParameters["FechaCierreAuditado"].DefaultValue = lblFecCierreAuditado.Text;
                SqlDataSource25.UpdateParameters["EstadoAuditor"].DefaultValue = "CERRADO";
                SqlDataSource25.UpdateParameters["FechaCierreAuditor"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource25.Update();
            }
            catch (Exception except)
            {
                //Handle the Exception.
                omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                //ENVIA NOTIFICACION DE CIERRE DE PLAN DE ACCION POR PARTE DEL AUDITOR
                TextoAdicional = "CIERRE DE PLAN DE ACCION POR PARTE DEL AUDITOR" + "<div><br></div>";
                TextoAdicional = TextoAdicional + "Planeación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br>";
                TextoAdicional = TextoAdicional + "Auditoría Código: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoriaSel.Text) + "<br>";
                TextoAdicional = TextoAdicional + "Plan de Acción Código: " + Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtPlanPlan.Text) + "<br>";

                if (txtTipoForanea.Text == "RECOMENDACION")
                    TextoAdicional = TextoAdicional + "Recomendación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRecPlan.Text) + "<div><br></div>";
                else if (txtTipoForanea.Text == "RIESGO")
                    TextoAdicional = TextoAdicional + "Riesgo Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRiesgoPlan.Text) + "<div><br></div>";

                boolEnviarNotificacion(2, Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text.Trim())), NodoAuditado(), "", TextoAdicional);
            }
        }

        protected void btnNO_Click(object sender, EventArgs e)
        {
            mpeMsgBoxSN.Hide();
            mpeMsgBoxTXT.Show();
            txtJustificacion.Focus();
        }

        protected void btnOKTXT_Click(object sender, EventArgs e)
        {
            bool err = false;
            string TextoAdicional = string.Empty;

            mpeMsgBoxTXT.Hide();
            mpeMsgBoxSN.Hide();

            try
            {
                SqlDataSource25.UpdateParameters["IdPlanAccion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text);
                SqlDataSource25.UpdateParameters["EstadoAuditado"].DefaultValue = "ABIERTO";
                SqlDataSource25.UpdateParameters["FechaCierreAuditado"].DefaultValue = null;
                SqlDataSource25.UpdateParameters["EstadoAuditor"].DefaultValue = "ABIERTO";
                SqlDataSource25.UpdateParameters["FechaCierreAuditor"].DefaultValue = null;
                SqlDataSource25.Update();
            }
            catch (Exception except)
            {
                //Handle the Exception.
                omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                //ENVIA NOTIFICACION DE REAPERTURA DE PLAN DE ACCION POR PARTE DEL AUDITOR
                TextoAdicional = "REAPERTURA DE PLAN DE ACCION POR PARTE DEL AUDITOR" + "<div><br></div>";
                TextoAdicional = TextoAdicional + "Planeación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br>";
                TextoAdicional = TextoAdicional + "Auditoría Código: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoriaSel.Text) + "<br>";
                TextoAdicional = TextoAdicional + "Plan de Acción Código: " + Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtPlanPlan.Text) + "<br>";

                if (txtTipoForanea.Text == "RECOMENDACION")
                    TextoAdicional = TextoAdicional + "Recomendación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRecPlan.Text) + "<div><br></div>";
                else if (txtTipoForanea.Text == "RIESGO")
                    TextoAdicional = TextoAdicional + "Riesgo Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRiesgoPlan.Text) + "<div><br></div>";

                TextoAdicional = TextoAdicional + "Justificación: " + Sanitizer.GetSafeHtmlFragment(txtJustificacion.Text) + "<div><br></div>";

                boolEnviarNotificacion(2, Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text.Trim())), NodoAuditado(), "", TextoAdicional);
            }
        }

        protected void btnImgokActualizarPA_Click(object sender, EventArgs e)
        {
            bool err = false;
            string TextoAdicional = string.Empty, selectCommand = string.Empty;
            int nodoAuditoria = -111;

            mpeMsgBox.Hide();

            if (lblTipoCierre.Text == "1")
            {
                try
                {
                    #region Actualizacion Plan Accion
                    SqlDataSource25.UpdateParameters["IdPlanAccion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text);
                    SqlDataSource25.UpdateParameters["EstadoAuditado"].DefaultValue = "CERRADO";
                    SqlDataSource25.UpdateParameters["FechaCierreAuditado"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource25.UpdateParameters["EstadoAuditor"].DefaultValue = lblEstadoAuditor.Text;

                    if (lblFecCierreAuditado.Text == "")
                        SqlDataSource25.UpdateParameters["FechaCierreAuditor"].DefaultValue = null;
                    else
                        SqlDataSource25.UpdateParameters["FechaCierreAuditor"].DefaultValue = lblFecCierreAuditado.Text;
                    SqlDataSource25.Update();
                    #endregion
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    #region Envio Notificacion
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

                    //ENVIA NOTIFICACION DE CIERRE DE PLAN DE ACCION POR PARTE DEL AUDITADO
                    TextoAdicional = "CIERRE DE PLAN DE ACCION POR PARTE DEL AUDITADO" + "<div><br></div>";
                    TextoAdicional = TextoAdicional + "Planeación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br>";
                    TextoAdicional = TextoAdicional + "Auditoría Código: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoriaSel.Text) + "<br>";
                    TextoAdicional = TextoAdicional + "Plan de Acción Código: " + Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtPlanPlan.Text) + "<br>";

                    if (txtTipoForanea.Text == "RECOMENDACION")
                        TextoAdicional = TextoAdicional + "Recomendación Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRecPlan.Text) + "<div><br></div>";
                    else if (txtTipoForanea.Text == "RIESGO")
                        TextoAdicional = TextoAdicional + "Riesgo Código: " + Sanitizer.GetSafeHtmlFragment(txtCodForaneaGen.Text) + ", Nombre: " + Sanitizer.GetSafeHtmlFragment(txtRiesgoPlan.Text) + "<div><br></div>";

                    boolEnviarNotificacionCierre(2, Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text.Trim())), nodoAuditoria, "", TextoAdicional);
                    #endregion
                }
            }
            else if (lblTipoCierre.Text == "2")
            {
                lblMsgBoxSN.Text = "Está de acuerdo con el Cierre del Auditado?";
                mpeMsgBoxSN.Show();
            }
            else if (lblTipoCierre.Text == "3")
            {
                #region Actualizar Auditoria

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource3.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text);
                    SqlDataSource3.UpdateParameters["Estado"].DefaultValue = "INFORME";
                    cAu.ActualizarLogHistoricoAudutoria(txtCodAuditoriaSel.Text, "NULL", "NULL", "GETDATE()", "SI");

                    SqlDataSource3.Update();
                }
                catch (Exception except)
                {
                    //Handle the Exception.
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
                #endregion

                if (!err)
                {
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabPlanAccion.Visible = false;
                }
            }
        }

        protected void btnImgCancelarRegAvances_Click(object sender, ImageClickEventArgs e)
        {
            filaRegistrarAvances.Visible = false;
            filaAvance.Visible = true;
        }

        protected void btnVolverPlanes_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
                filaPlanAccion.Visible = true;
            else
                if (TabContainer1.ActiveTabIndex == 1)
                    filaPlanAccionRiesgo.Visible = true;

            filaAvance.Visible = false;
        }

        protected void imgBtnInsertarAvance_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaRegistrarAvances.Visible = true;
                filaAvance.Visible = false;
                txtAvance.Focus();
                txtAvance.Text = "";
                txtUsuarioAvance.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionAvance.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnImgInsertarAvance_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;

            if (mtdVerificarCampos())
            {
                SqlDataSource26.InsertParameters["IdPlanAccion"].DefaultValue = lblIdPlanAccion.Text;
                SqlDataSource26.InsertParameters["Avance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAvance.Text);
                SqlDataSource26.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource26.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource26.Insert();
                }
                catch (Exception except)
                {
                    //Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaRegistrarAvances.Visible = false;
                    filaAvance.Visible = true;
                }
            }
        }

        protected void imgBtnAdjuntar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                {
                    mtdCargarPdfPlanAccion();
                    GridView100.DataBind();
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".docx")
                {
                    mtdCargarPdfPlanAccion();
                    GridView100.DataBind();
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".xlsx")
                {
                    mtdCargarPdfPlanAccion();
                    GridView100.DataBind();
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".txt")
                {
                    mtdCargarPdfPlanAccion();
                    GridView100.DataBind();
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".jpg")
                {
                    mtdCargarPdfPlanAccion();
                    GridView100.DataBind();
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }
                else if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".png")
                {
                    mtdCargarPdfPlanAccion();
                    GridView100.DataBind();
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }
                else
                    omb.ShowMessage("Solamente se permiten cargar archivos PDF!", 2, "Atención");
            }
            else
            {
                //omb.ShowMessage("¡Debe seleccionar un archivo PDF!", 2, "Atención");
                lblMsgBox.Text = "No hay archivos para cargar.";
                mpeMsgBox.Show();
            }
        }

        protected void imgBtnInsertarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaGridAnexos.Visible = false;
                filaSubirAnexos.Visible = true;
                FileUpload1.Focus();
                txtDescArchivo.Text = string.Empty;
            }
        }

        protected void btnImgCancelarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        protected void btnVolverArchivo_Click(object sender, EventArgs e)
        {
            filaVolverRecomendacion.Visible = true;
            filaAnexos.Visible = false;

            if (TabContainer1.ActiveTabIndex == 0)
                filaPlanAccion.Visible = true;
            else if (TabContainer1.ActiveTabIndex == 1)
                filaPlanAccionRiesgo.Visible = true;
        }

        protected void bntInformeAud_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            str = "window.open('AudAdmReporteAuditoriaPlan.aspx?Ca=" + Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text) + "','Reporte','width=800,height=600,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }
        #endregion Buttons

        #region GridViews

        #region Grid Planeacion
        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            string strConn = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            Datasets.DataSet dsInfo = new Datasets.DataSet();

            #region Mostrar informacion seleccionada
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();
            popupPlanea.Cancel();
            #endregion

            #region Consulta y muestra hallagos
            if (booPerteneceAAuditoria)
            {
                filaTabHallazgo.Visible = true;

                dsInfo = cAu.mtdConsultaHallazgos(Session["idUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text.Trim()), 1, strConn, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    GridView1.DataSource = dsInfo;
                    GridView1.DataBind();
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            else
            {
                filaTabHallazgo.Visible = true;

                dsInfo = cAu.mtdConsultaHallazgos(Session["idUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text.Trim()), 2, strConn, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (dsInfo.Tables[0].Rows.Count == 0)
                        omb.ShowMessage("El usuario no tiene Planes de Acción asignados", 1, "Atención");

                    GridView1.DataSource = dsInfo;
                    GridView1.DataBind();
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            #endregion
        }
        #endregion

        #region Hallazgos
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);

            if (e.CommandArgument.ToString() == "Recomendacion")
            {
                filaTabHallazgo.Visible = false;
                filaVolverHallazgo.Visible = true;
                filaVolverRecomendacion.Visible = false;

                if (booPerteneceAAuditoria)
                {
                    #region Habilita Controles
                    TabContainer1.Tabs[0].Visible = true;
                    TabContainer1.Tabs[1].Visible = true;
                    TabContainer1.ActiveTabIndex = 0;

                    filaTabPlanAccion.Visible = true;
                    filaVolverHallazgo.Visible = true;
                    filaInformeAuditoria.Visible = true;

                    filaDetallePlan.Visible = false;
                    filaPlanAccion.Visible = false;
                    filaPlanAccionRiesgo.Visible = false;
                    filaVolverRecomendacion.Visible = false;
                    filaAvance.Visible = false;
                    filaRegistrarAvances.Visible = false;
                    filaAnexos.Visible = false;
                    #endregion
                }
                else
                {
                    #region Habilita Controles
                    TabContainer1.Tabs[0].Visible = true;
                    TabContainer1.Tabs[1].Visible = true;
                    TabContainer1.ActiveTabIndex = 0;

                    filaTabPlanAccion.Visible = true;
                    filaVolverHallazgo.Visible = true;
                    filaInformeAuditoria.Visible = true;

                    filaDetallePlan.Visible = false;
                    filaPlanAccion.Visible = false;
                    filaPlanAccionRiesgo.Visible = false;
                    filaVolverRecomendacion.Visible = false;
                    filaAvance.Visible = false;
                    filaRegistrarAvances.Visible = false;
                    filaAnexos.Visible = false;
                    #endregion
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strConsultaPlan = string.Empty, strErrMsg = string.Empty;
            string strConn = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            Datasets.DataSet dsInfo = new Datasets.DataSet();

            if (booPerteneceAAuditoria)
            {
                #region Pertenece
                txtCodPlanAccion.Text = GridView1.SelectedRow.Cells[4].Text.Trim();
                txtCodAuditoriaSel.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtNomAuditoriaSel.Text = GridView1.SelectedRow.Cells[1].Text.Trim();

                dsInfo = cAu.mtdConsultaRecomendacion(Session["idUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text.Trim()),
                    GridView1.SelectedRow.Cells[2].Text.Trim(), 1, strConn, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (dsInfo.Tables[0].Rows.Count == 0)
                    {
                        //omb.ShowMessage("No hay Planes de Acción asignados", 1, "Atención");
                        TabContainer1.Tabs[0].Visible = false;
                        TabContainer1.Tabs[1].Visible = false;
                        TabContainer1.ActiveTabIndex = 2;
                    }
                    else
                    {
                        GridView1000.DataSource = dsInfo;
                        GridView1000.DataBind();
                    }
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
                #endregion
            }
            else
            {
                #region NO Pertenece
                txtCodPlanAccion.Text = GridView1.SelectedRow.Cells[4].Text.Trim();
                txtCodAuditoriaSel.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtNomAuditoriaSel.Text = GridView1.SelectedRow.Cells[1].Text.Trim();

                dsInfo = cAu.mtdConsultaRecomendacion(Session["idUsuario"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text.Trim()),
                   GridView1.SelectedRow.Cells[2].Text.Trim(), 2, strConn, ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (dsInfo.Tables[0].Rows.Count == 0)
                        omb.ShowMessage("El usuario no tiene Planes de Acción asignados", 1, "Atención");

                    GridView1000.DataSource = dsInfo;
                    GridView1000.DataBind();
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
                #endregion
            }
        }
        #endregion

        #region Recomendaciones
        protected void GridView1000_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "PlanAccion")
            {
                filaTabPlanAccion.Visible = false;
                filaVolverRecomendacion.Visible = true;
                filaVolverHallazgo.Visible = false;

                //ajustes para mostrar los planes de accion creados
                filaPlanAccion.Visible = true;

                if (TabContainer1.ActiveTabIndex == 0)
                {
                    filaPlanAccion.Visible = true;
                    GridView4.DataBind();
                }
                else
                    filaPlanAccionRiesgo.Visible = true;

                filaDetallePlan.Visible = false;
            }
            else if (e.CommandArgument.ToString() == "LogEstados")
                filaTabPlanAccion.Visible = false;
        }

        protected void GridView1000_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strConsultaPlan = string.Empty;

            if (GridView1000.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtFecPlan.Focus();

                #region Campos de texto
                txtCodRec.Text = GridView1000.SelectedRow.Cells[0].Text.Trim();
                txtCodRecPlan.Text = GridView1000.SelectedRow.Cells[0].Text.Trim();
                txtObjetivo.Text = GridView1000.SelectedRow.Cells[3].Text.Trim();
                txtTipoPoD.Text = GridView1000.SelectedRow.Cells[4].Text.Trim();
                txtNombrePoD.Text = GridView1000.SelectedDataKey[1].ToString().Trim();
                txtRecomendacion.Text = GridView1000.SelectedRow.Cells[1].Text.Trim();
                txtAvanceRec.Text = GridView1000.SelectedRow.Cells[1].Text.Trim();
                txtPlanRec.Text = GridView1000.SelectedRow.Cells[1].Text.Trim();
                txtRecPlan.Text = GridView1000.SelectedRow.Cells[1].Text.Trim();
                txtCodForaneaGen.Text = GridView1000.SelectedRow.Cells[0].Text.Trim();
                txtTipoForanea.Text = "RECOMENDACION";
                txtDescPlan.Text = string.Empty;
                #endregion

                if (txtTipoPoD.Text == "Procesos")
                {
                    lblPoD.Text = "Proceso";

                    #region Proceso
                    if (!booPerteneceAAuditoria)
                    {
                        #region No Pertenece
                        GridView4.Columns[6].Visible = false;
                        GridView4.Columns[11].Visible = true;
                        GridView4.Columns[12].Visible = false;
                        GridView4.Columns[13].Visible = true;
                        GridView4.Columns[14].Visible = false;
                        #endregion No Pertenece
                    }
                    else
                    {
                        #region Pertenece
                        GridView4.Columns[6].Visible = true;
                        GridView4.Columns[11].Visible = true;
                        GridView4.Columns[12].Visible = true;
                        GridView4.Columns[13].Visible = false;
                        GridView4.Columns[14].Visible = true;
                        #endregion Pertenece
                    }
                    #endregion
                }
                else
                {
                    lblPoD.Text = "Dependencia Auditada";

                    #region Dependencia
                    if (!booPerteneceAAuditoria)
                    {
                        #region No Pertenece
                        GridView4.Columns[6].Visible = false;
                        GridView4.Columns[11].Visible = true;
                        GridView4.Columns[12].Visible = false;
                        GridView4.Columns[13].Visible = true;
                        GridView4.Columns[14].Visible = false;
                        #endregion No Pertenece
                    }
                    else
                    {
                        #region Pertenece
                        GridView4.Columns[6].Visible = true;
                        GridView4.Columns[11].Visible = true;
                        GridView4.Columns[12].Visible = true;
                        GridView4.Columns[13].Visible = false;
                        GridView4.Columns[14].Visible = true;
                        #endregion Pertenece
                    }
                    #endregion
                }


                if (booPerteneceAAuditoria)
                {
                    #region Pertenece auditoria
                    strConsultaPlan = "SELECT APA.[IdPlanAccion], APA.[EstadoAuditado], APA.[IdForanea], APA.[Descripcion], " +
                            "CONVERT(VARCHAR(10), APA.[FechaCompromiso], 120) AS FechaCompromiso,  " +
                            "CONVERT(VARCHAR(10), APA.[FechaRegistro],120) AS FechaRegistro,  " +
                            "APA.[IdUsuario], APA.[TipoForanea], APA.[EstadoAuditor], APA.[FechaCierreAuditado],  " +
                            "APA.[FechaCierreAuditor],LU.[Usuario] " +
                        "FROM [Auditoria].[PlanAccion] APA " +
                        "INNER JOIN  [Listas].[Usuarios] LU ON APA.[IdUsuario] = LU.[IdUsuario] " +
                        "INNER JOIN [Auditoria].[Recomendacion] AR ON AR.IdRecomendacion  = APA.IdForanea " +
                        "WHERE APA.[TipoForanea] = @TipoForanea AND APA.[IdForanea] = @IdForanea ";
                    #endregion
                }
                else
                {
                    #region No Pertenece
                    DataTable dtInfo = cCuenta.mtdConsultarIdJerarquia(Session["IdUsuario"].ToString());

                    strConsultaPlan = "SELECT APA.[IdPlanAccion], APA.[EstadoAuditado], APA.[IdForanea], APA.[Descripcion], " +
                            "CONVERT(VARCHAR(10), APA.[FechaCompromiso], 120) AS FechaCompromiso,  " +
                            "CONVERT(VARCHAR(10), APA.[FechaRegistro],120) AS FechaRegistro,  " +
                            "APA.[IdUsuario], APA.[TipoForanea], APA.[EstadoAuditor], APA.[FechaCierreAuditado],  " +
                            "APA.[FechaCierreAuditor],LU.[Usuario] " +
                        "FROM   [Auditoria].[PlanAccion] APA " +
                        "INNER JOIN  [Listas].[Usuarios] LU ON APA.[IdUsuario] = LU.[IdUsuario] " +
                        "INNER JOIN [Auditoria].[Recomendacion] AR ON AR.IdRecomendacion  = APA.IdForanea " +
                        "WHERE " +
                        "APA.[TipoForanea] = '" + txtTipoForanea.Text + "' AND APA.[IdForanea] = " + txtCodForaneaGen.Text + " "
                        ;
                        //"AND AR.IdDependenciaAuditada = " + dtInfo.Rows[0]["idHijo"].ToString().Trim();
                    #endregion
                }

                SqlDataSource25.SelectCommand = strConsultaPlan;

                GridView4.DataBind();

                #region Longitudes
                int rows = 0, longMax = 0, rowsAdd = 0;
                double temp = 0;

                txtRecomendacion.Height = 18;
                txtRecomendacion.Width = 402;
                txtRecPlan.Height = 18;
                txtRecPlan.Width = 402;
                txtPlanRec.Height = 18;
                txtPlanRec.Width = 402;
                txtAvanceRec.Height = 18;
                txtAvanceRec.Width = 402;

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

                if (rows + rowsAdd > 1)
                {
                    txtRecomendacion.Height = (rows + rowsAdd) * 18;
                    txtRecPlan.Height = (rows + rowsAdd) * 18;
                    txtPlanRec.Height = (rows + rowsAdd) * 18;
                    txtAvanceRec.Height = (rows + rowsAdd) * 18;
                }

                if (longMax > 72)
                {
                    txtRecomendacion.Width = 700;
                    txtRecPlan.Width = 700;
                    txtPlanRec.Width = 700;
                    txtAvanceRec.Width = 700;
                }
                else
                {
                    txtRecomendacion.Width = 402;
                    txtRecPlan.Width = 402;
                    txtPlanRec.Width = 402;
                    txtAvanceRec.Width = 402;
                }
                #endregion
            }
        }
        #endregion

        #region Riesgos
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "PlanAccion")
            {
                filaTabPlanAccion.Visible = false;
                filaPlanAccionRiesgo.Visible = true;
                filaVolverRecomendacion.Visible = true;
            }
            else if (e.CommandArgument.ToString() == "LogEstados")
                filaTabPlanAccion.Visible = false;
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView3.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodRec.Text = GridView3.SelectedRow.Cells[0].Text.Trim();
                txtCodRiesgoPlan.Text = GridView3.SelectedRow.Cells[0].Text.Trim();
                txtObjetivo.Text = GridView3.SelectedRow.Cells[2].Text.Trim();
                txtTipoPoD.Text = GridView3.SelectedRow.Cells[3].Text.Trim();
                txtNombrePoD.Text = GridView3.SelectedDataKey[1].ToString().Trim();
                txtRecomendacion.Text = GridView3.SelectedRow.Cells[1].Text.Trim();
                txtAvanceRec.Text = GridView3.SelectedRow.Cells[1].Text.Trim();
                txtPlanRec.Text = GridView3.SelectedRow.Cells[1].Text.Trim();
                txtRiesgoPlan.Text = GridView3.SelectedRow.Cells[1].Text.Trim();
                txtCodForaneaGen.Text = GridView3.SelectedRow.Cells[0].Text.Trim();
                txtTipoForanea.Text = "RIESGO";
                txtDescPlan.Text = "";
                txtUsuarioPlan.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionPlan.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                if (txtTipoPoD.Text == "Procesos")
                {
                    lblPoD.Text = "Proceso";

                    #region Proceso
                    if (!booPerteneceAAuditoria)
                    {
                        #region NO Pertenece
                        GridView5.Columns[6].Visible = false;
                        GridView5.Columns[11].Visible = true;
                        GridView5.Columns[12].Visible = false;
                        GridView5.Columns[13].Visible = true;
                        GridView5.Columns[14].Visible = false;
                        #endregion
                    }
                    else
                    {
                        #region Pertenece
                        GridView5.Columns[6].Visible = true;
                        GridView5.Columns[11].Visible = true;
                        GridView5.Columns[12].Visible = true;
                        GridView5.Columns[13].Visible = false;
                        GridView5.Columns[14].Visible = true;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    lblPoD.Text = "Dependencia";

                    #region Dependencia
                    if (!booPerteneceAAuditoria)
                    {
                        #region NO Pertenece
                        GridView5.Columns[6].Visible = false;
                        GridView5.Columns[11].Visible = true;
                        GridView5.Columns[12].Visible = false;
                        GridView5.Columns[13].Visible = true;
                        GridView5.Columns[14].Visible = false;
                        #endregion
                    }
                    else
                    {
                        #region Pertenece
                        GridView5.Columns[6].Visible = true;
                        GridView5.Columns[11].Visible = true;
                        GridView5.Columns[12].Visible = true;
                        GridView5.Columns[13].Visible = false;
                        GridView5.Columns[14].Visible = true;
                        #endregion
                    }
                    #endregion
                }

                #region Longitudes
                int rows = 0, longMax = 0, rowsAdd = 0;
                double temp = 0;

                txtRecomendacion.Height = 18;
                txtRecomendacion.Width = 402;
                txtPlanRec.Height = 18;
                txtPlanRec.Width = 402;

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

                if (rows + rowsAdd > 1)
                {
                    txtRecomendacion.Height = (rows + rowsAdd) * 18;
                    txtPlanRec.Height = (rows + rowsAdd) * 18;
                }

                if (longMax > 72)
                {
                    txtRecomendacion.Width = 700;
                    txtPlanRec.Width = 700;
                }
                else
                {
                    txtRecomendacion.Width = 402;
                    txtPlanRec.Width = 402;
                }
                #endregion
            }
        }
        #endregion

        #region Planes de Accion
        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string EstadoAuditado = string.Empty;// value of the datakey 
            ImageButton imgBtn = (ImageButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)imgBtn.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview

            if (e.CommandArgument.ToString() == "ActualizarEstado1")
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    lblMsgBox.Text = "Esta seguro de efectuar el cierre del Plan de Acción?";
                    btnImgokActualizarPA.Text = "Ok";
                    btnCancelar.Text = "Cancelar";
                    lblTipoCierre.Text = "1";
                    mpeMsgBox.Show();
                }
            }
            else if (e.CommandArgument.ToString() == "ActualizarEstado2")
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    lblTipoCierre.Text = "2";
                    EstadoAuditado = myGrid.DataKeys[myRow.RowIndex].Values[0].ToString().Trim();
                    if (EstadoAuditado == "ABIERTO")
                        omb.ShowMessage("No puede efectuar el cierre hasta que el Auditado no haya cerrado el Plan de Acción." + "<br/>", 2, "Atención");
                    else
                    {
                        lblMsgBox.Text = "Esta seguro de efectuar el cierre del Plan de Acción?";
                        btnImgokActualizarPA.Text = "Ok";
                        btnCancelar.Text = "Cancelar";
                        mpeMsgBox.Show();
                    }
                }
            }
            else if (e.CommandArgument.ToString() == "Avances")
            {
                filaPlanAccion.Visible = false;
                filaAvance.Visible = true;
            }
            else if (e.CommandArgument.ToString() == "Actualizar")
            {
                filaPlanAccion.Visible = false;
                filaDetallePlan.Visible = true;
                txtFecPlan.Text = myRow.Cells[4].Text;
                txtDescPlan.Text = myRow.Cells[3].Text;
                btnImgInsertarPlan.Visible = false;
                btnImgActualizarPlan.Visible = true;
            }
            else if (e.CommandArgument.ToString() == "Anexar")
            {
                filaAnexos.Visible = true;
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
                filaVolverRecomendacion.Visible = false;
                filaPlanAccion.Visible = false;
                GridView100.DataBind();
            }
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image img1 = (Image)e.Row.FindControl("Image1");
            Image img2 = (Image)e.Row.FindControl("Image2");
            ImageButton imgbtn1 = (ImageButton)e.Row.FindControl("btnImgActualizarEstado");
            ImageButton imgbtn2 = (ImageButton)e.Row.FindControl("btnImgActualizarEstado2");
            ImageButton imgbtnActPlan = (ImageButton)e.Row.FindControl("btnImgActPlan");

            // Check if row is data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get value of third column. Index is zero based, to 
                // get text of third column we use Cells[2].Text

                // If value is ABIERTO then change to UNLOCK.PNG, otherwise change to LOCK.PNG
                if (((GridView)sender).DataKeys[e.Row.RowIndex].Values[0].ToString() == "ABIERTO")
                {
                    img1.ImageUrl = "~/Imagenes/Icons/unlock.png";
                    img1.ToolTip = "ABIERTO";
                    imgbtn1.Enabled = true;
                }
                else
                {
                    img1.ImageUrl = "~/Imagenes/Icons/lock.png";
                    img1.ToolTip = "CERRADO";
                    imgbtn1.Enabled = false;
                }

                if (((GridView)sender).DataKeys[e.Row.RowIndex].Values[1].ToString() == "ABIERTO")
                {
                    img2.ImageUrl = "~/Imagenes/Icons/unlock.png";
                    img2.ToolTip = "ABIERTO";
                    imgbtn2.Enabled = true;
                    imgbtnActPlan.Enabled = true;
                }
                else
                {
                    img2.ImageUrl = "~/Imagenes/Icons/lock.png";
                    img2.ToolTip = "CERRADO";
                    imgbtn2.Enabled = false;
                    imgbtnActPlan.Enabled = true;
                }
            }
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView4.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodPlanAccion.Text = GridView4.SelectedRow.Cells[0].Text.Trim();
                lblFecCierreAuditado.Text = GridView4.SelectedDataKey[2].ToString().Trim();
                lblFecCierreAuditor.Text = GridView4.SelectedDataKey[3].ToString().Trim();
                lblEstadoAuditor.Text = GridView4.SelectedDataKey[1].ToString().Trim();
                txtPlanPlan.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
                txtAvancePlan.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
                lblIdPlanAccion.Text = GridView4.SelectedRow.Cells[0].Text.Trim();
                txtUsuarioPlan.Text = GridView4.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionPlan.Text = GridView4.SelectedRow.Cells[5].Text.Trim();

                #region Longitudes
                int rows = 0, longMax = 0, rowsAdd = 0;
                double temp = 0;

                txtAvancePlan.Height = 18;
                txtAvancePlan.Width = 402;
                txtPlanPlan.Height = 18;
                txtPlanPlan.Width = 402;

                //Revisa la longitud max del texto y el número de líneas
                foreach (string strItem in Regex.Split(GridView4.SelectedRow.Cells[3].Text, "</div>"))
                {
                    rows = rows + 1;
                    if (strItem.Length > longMax) longMax = strItem.Length;
                    if (strItem.Length > 126)
                    {
                        temp = strItem.Length / 126;
                        rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                    }
                }

                if (rows + rowsAdd > 1)
                {
                    txtAvancePlan.Height = (rows + rowsAdd) * 18;
                    txtPlanPlan.Height = (rows + rowsAdd) * 18;
                }

                if (longMax > 72)
                {
                    txtAvancePlan.Width = 700;
                    txtPlanPlan.Width = 700;
                }
                else
                {
                    txtAvancePlan.Width = 402;
                    txtPlanPlan.Width = 402;
                }
                #endregion
            }
        }
        #endregion

        #region Plan Accion Riesgo
        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string EstadoAuditado = string.Empty;// value of the datakey 
            ImageButton imgBtn = (ImageButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)imgBtn.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview

            if (e.CommandArgument.ToString() != "Actualizar")
                lblMsgBox.Text = "Esta seguro de efectuar el cierre del Plan de Acción?";

            if (e.CommandArgument.ToString() == "ActualizarEstado1")
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    lblTipoCierre.Text = "1";
                    mpeMsgBox.Show();
                }
            }
            else if (e.CommandArgument.ToString() == "ActualizarEstado2")
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    lblTipoCierre.Text = "2";
                    EstadoAuditado = myGrid.DataKeys[myRow.RowIndex].Values[0].ToString().Trim();
                    if (EstadoAuditado == "ABIERTO")
                        omb.ShowMessage("No puede efectuar el cierre hasta que el Auditado no haya cerrado el Plan de Acción." + "<br/>", 2, "Atención");
                    else
                        mpeMsgBox.Show();
                }
            }
            else if (e.CommandArgument.ToString() == "Avances")
            {
                filaPlanAccion.Visible = false;
                filaPlanAccionRiesgo.Visible = false;
                filaAvance.Visible = true;
            }
            else if (e.CommandArgument.ToString() == "Actualizar")
            {
                filaPlanAccionRiesgo.Visible = false;
                filaDetallePlan.Visible = true;
                txtFecPlan.Text = myRow.Cells[4].Text;
                txtDescPlan.Text = myRow.Cells[3].Text;
                btnImgInsertarPlan.Visible = false;
                btnImgActualizarPlan.Visible = true;
            }
            else if (e.CommandArgument.ToString() == "Anexar")
            {
                filaAnexos.Visible = true;
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
                filaVolverRecomendacion.Visible = false;
                filaPlanAccionRiesgo.Visible = false;
                GridView100.DataBind();
            }
        }

        protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image img1 = (Image)e.Row.FindControl("Image1");
            Image img2 = (Image)e.Row.FindControl("Image2");
            ImageButton imgbtn1 = (ImageButton)e.Row.FindControl("btnImgActualizarEstado");
            ImageButton imgbtn2 = (ImageButton)e.Row.FindControl("btnImgActualizarEstado2");
            ImageButton imgbtnActPlan = (ImageButton)e.Row.FindControl("btnImgActPlan2");

            // Check if row is data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get value of third column. Index is zero based, to 
                // get text of third column we use Cells[2].Text

                // If value is ABIERTO then change to UNLOCK.PNG, otherwise change to LOCK.PNG
                if (((GridView)sender).DataKeys[e.Row.RowIndex].Values[0].ToString() == "ABIERTO")
                {
                    img1.ImageUrl = "~/Imagenes/Icons/unlock.png";
                    img1.ToolTip = "ABIERTO";
                    imgbtn1.Enabled = true;
                }
                else
                {
                    img1.ImageUrl = "~/Imagenes/Icons/lock.png";
                    img1.ToolTip = "CERRADO";
                    imgbtn1.Enabled = false;
                }

                if (((GridView)sender).DataKeys[e.Row.RowIndex].Values[1].ToString() == "ABIERTO")
                {
                    img2.ImageUrl = "~/Imagenes/Icons/unlock.png";
                    img2.ToolTip = "ABIERTO";
                    imgbtn2.Enabled = true;
                    imgbtnActPlan.Enabled = true;
                }
                else
                {
                    img2.ImageUrl = "~/Imagenes/Icons/lock.png";
                    img2.ToolTip = "CERRADO";
                    imgbtn2.Enabled = false;
                    imgbtnActPlan.Enabled = true;
                }
            }
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView5.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodPlanAccion.Text = GridView5.SelectedRow.Cells[0].Text.Trim();
                lblFecCierreAuditado.Text = GridView5.SelectedDataKey[2].ToString().Trim();
                lblFecCierreAuditor.Text = GridView5.SelectedDataKey[3].ToString().Trim();
                lblEstadoAuditor.Text = GridView5.SelectedDataKey[1].ToString().Trim();
                txtPlanPlan.Text = GridView5.SelectedRow.Cells[3].Text.Trim();
                txtAvancePlan.Text = GridView5.SelectedRow.Cells[3].Text.Trim();
                lblIdPlanAccion.Text = GridView5.SelectedRow.Cells[0].Text.Trim();
                txtUsuarioPlan.Text = GridView5.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionPlan.Text = GridView5.SelectedRow.Cells[5].Text.Trim();

                int rows = 0, longMax = 0, rowsAdd = 0;
                double temp = 0;

                txtAvancePlan.Height = 18;
                txtAvancePlan.Width = 402;
                txtPlanPlan.Height = 18;
                txtPlanPlan.Width = 402;

                //Revisa la longitud max del texto y el número de líneas
                foreach (string strItem in Regex.Split(GridView5.SelectedRow.Cells[3].Text, "</div>"))
                {
                    rows = rows + 1;
                    if (strItem.Length > longMax) longMax = strItem.Length;
                    if (strItem.Length > 126)
                    {
                        temp = strItem.Length / 126;
                        rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                    }
                }

                if (rows + rowsAdd > 1)
                {
                    txtAvancePlan.Height = (rows + rowsAdd) * 18;
                    txtPlanPlan.Height = (rows + rowsAdd) * 18;
                }

                if (longMax > 72)
                {
                    txtAvancePlan.Width = 700;
                    txtPlanPlan.Width = 700;
                }
                else
                {
                    txtAvancePlan.Width = 402;
                    txtPlanPlan.Width = 402;
                }
            }
        }
        #endregion

        #region Archivos
        protected void GridView100_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameFile = GridView100.SelectedRow.Cells[1].Text.Trim();
            mtdDescargarPdfPlanAccion(nameFile);
        }
        #endregion
        #endregion GridViews

        #region Tabs
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                btnVolverRecomendacion.Text = "Volver a Recomendaciones";
                lblRR.Text = "Recomendación:";
                lblAvanceRR.Text = "Recomendación:";
                lblPARR.Text = "Recomendación:";
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                btnVolverRecomendacion.Text = "Volver a Riesgos";
                lblRR.Text = "Riesgo:";
                lblAvanceRR.Text = "Riesgo:";
                lblPARR.Text = "Riesgo:";
            }
        }
        #endregion

        #region Verificacion
        protected bool mtdPerteneceAuditoria(string strUsuario)
        {
            #region Variables
            bool booResult = false;
            string strIdJerarquia = string.Empty, strIdTipoArea = string.Empty, strIdPadre = "-111", strConsulta = string.Empty;
            string strConn = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter sdaAdap = null;
            DataTable dtInfo = null;
            DataView dvView = null;
            #endregion

            #region Consulta Usuario
            //Consulta el Id de la Jerarquia a la que pertenece el Usuario logueado
            strConsulta = "SELECT IdJerarquia FROM [Listas].[Usuarios] WHERE IdUsuario = " + strUsuario;
            sdaAdap = new SqlDataAdapter(strConsulta, strConn);
            dtInfo = new DataTable();
            sdaAdap.Fill(dtInfo);
            dvView = new DataView(dtInfo);
            #endregion

            #region Recorrido Informacion Jerarquia
            foreach (DataRowView row in dvView)
            {
                strIdJerarquia = row["IdJerarquia"].ToString().Trim();
                txtCodJerarquia.Text = row["IdJerarquia"].ToString().Trim();
            }
            #endregion

            while (strIdPadre != "-1")
            {
                #region Consulta Usuario
                //Consulta el Id del Padre y el Tipo de Area
                strConsulta = "SELECT idPadre,TipoArea FROM [Parametrizacion].[JerarquiaOrganizacional] WHERE idHijo = " + strIdJerarquia;
                sdaAdap = new SqlDataAdapter(strConsulta, strConn);
                dtInfo = new DataTable();
                sdaAdap.Fill(dtInfo);
                dvView = new DataView(dtInfo);
                #endregion

                #region Recorrido Informacion
                foreach (DataRowView row in dvView)
                {
                    strIdPadre = row["idPadre"].ToString().Trim();
                    strIdJerarquia = strIdPadre;
                    strIdTipoArea = row["TipoArea"].ToString().Trim();

                    //Si encuentra el Nodo de Auditoria
                    if (strIdTipoArea == "A")
                    {
                        booResult = true;
                        break;
                    }
                }
                #endregion
            }

            //Verifica si el nodo de la Jerarquia pertenece al Grupo de Auditoria
            return booResult;
        }
        #endregion

        protected bool mtdVerificarCampos()
        {
            bool err = true;

            if (filaDetallePlan.Visible == true)
            {
                if (mtdValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecPlan.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Fecha de Compromiso.", 2, "Atención");
                    txtFecPlan.Focus();
                }
                else if (mtdValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtDescPlan.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Descripción del Plan.", 2, "Atención");
                    txtDescPlan.Focus();
                }
            }
            else if (filaRegistrarAvances.Visible == true)
            {
                if (mtdValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtAvance.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Avance.", 2, "Atención");
                    txtAvance.Focus();
                }
            }
            return err;
        }

        protected bool mtdValidarCadenaVacia(string cadena)
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

        protected Int32 NodoAuditado()
        {
            //Trae el nodo del grupo de Auditoria
            string selectCommand = "";
            int nodoAuditado = -111;

            if (Sanitizer.GetSafeHtmlFragment(txtTipoForanea.Text) == "RECOMENDACION")
            {
                if (Sanitizer.GetSafeHtmlFragment(txtTipoPoD.Text) == "Procesos")
                {
                    selectCommand = "SELECT P.[IdHijo] "
                         + "FROM [Auditoria].[PlanAccion] AS PL INNER JOIN Auditoria.[Recomendacion] AS R ON R.IdRecomendacion = PL.[IdForanea] INNER JOIN Procesos.Proceso AS P ON P.IdProceso = R.IdSubproceso"
                         + " WHERE  PL.IdPlanAccion = " + Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text);
                }
                else if (Sanitizer.GetSafeHtmlFragment(txtTipoPoD.Text) == "Dependencia")
                {
                    selectCommand = "SELECT J.[IdHijo] "
                         + "FROM [Auditoria].[PlanAccion] AS PL INNER JOIN Auditoria.[Recomendacion] AS R ON R.IdRecomendacion = PL.[IdForanea] INNER JOIN Parametrizacion.JerarquiaOrganizacional AS J ON J.idHijo = R.IdDependenciaAuditada"
                         + " WHERE  PL.IdPlanAccion = " + Sanitizer.GetSafeHtmlFragment(txtCodPlanAccion.Text);
                }
            }
            else if (txtTipoForanea.Text == "RIESGO")
            {
                if (txtTipoPoD.Text == "Procesos")
                {
                    selectCommand = "SELECT P.[IdHijo] "
                         + "FROM [Auditoria].[PlanAccion] AS PL INNER JOIN Auditoria.[Riesgo] AS R ON R.IdRiesgo = PL.[IdForanea] INNER JOIN Procesos.Proceso AS P ON P.IdProceso = R.IdSubproceso"
                         + " WHERE  PL.IdPlanAccion = " + txtCodPlanAccion.Text;
                }
                else if (txtTipoPoD.Text == "Dependencia")
                {
                    selectCommand = "SELECT J.[IdHijo] "
                         + "FROM [Auditoria].[PlanAccion] AS PL INNER JOIN Auditoria.[Riesgo] AS R ON R.IdRiesgo = PL.[IdForanea] INNER JOIN Parametrizacion.JerarquiaOrganizacional AS J ON J.idHijo = R.IdDependencia"
                         + " WHERE  PL.IdPlanAccion = " + txtCodPlanAccion.Text;
                }
            }

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                nodoAuditado = Convert.ToInt32(row["idHijo"].ToString().Trim());
            }

            return (nodoAuditado);
        }
        private DataTable infoGrid;
        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int rowGrid;
        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

        private int pagIndex;
        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;


            string strErrMsg = string.Empty;
            string strConn = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            Datasets.DataSet dsInfo = new Datasets.DataSet();
            dsInfo = cAu.mtdConsultaHallazgos(Session["idUsuario"].ToString(), txtCodPlaneacion.Text.Trim(), 1, strConn, ref strErrMsg);
            GridView1.DataSource = dsInfo;
            GridView1.DataBind();

            //GridView1.DataSource = InfoGrid;
            //GridView1.DataBind();
        }



        
        private void Devolver()
        {
            txtCodPlaneacion.Text = string.Empty;
            txtNomPlaneacion.Text = string.Empty;

            if (filaTabPlanAccion.Visible == true) filaTabPlanAccion.Visible = false;
            filaDetallePlan.Visible = false;
            filaInformeAuditoria.Visible = false;
            filaPlanAccion.Visible = false;
            filaPlanAccionRiesgo.Visible = false;
            filaVolverRecomendacion.Visible = false;
            filaAvance.Visible = false;
            filaRegistrarAvances.Visible = false;
            filaAnexos.Visible = false;
            popupPlanea.Cancel();
        }

        #region SqlDataSources
        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        protected void SqlDataSource25_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertId = (int)e.Command.Parameters["@NewParameter"].Value;
        }
        #endregion

        #region Notificacion
        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Vars
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Vars

            try
            {
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
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
                        //14/11/2014
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region correo del Destinatario
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
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
                    if (!string.IsNullOrEmpty(idJefeInmediato))
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
                    if (!string.IsNullOrEmpty(idJefeMediato))
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

                #region Registro en la tabla de Correos Enviados
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
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource200.Insert();
                #endregion
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
                #region creacion del registro
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource201.Insert();
                }
                #endregion

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
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
                    omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    #region  Actualiza el Estado del Correo Enviado
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource200.Update();
                    #endregion
                }
            }

            return (err);
        }

        private Boolean boolEnviarNotificacionCierre(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Vars
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion

            try
            {
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                #region Informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
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
                        //14/11/2014
                        Asunto = "CIERRE - " + row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString().Trim();
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

                #region Registro en la tabla de Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CIERRE";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource200.Insert();
                #endregion
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
                #region creacion del registro en el log de correos enviados
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource201.Insert();
                }
                #endregion

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
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
                    omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    #region Actualiza el Estado del Correo Enviado
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource200.Update();
                    #endregion
                }
            }

            return (err);
        }
        #endregion

        #region PDFs
        private void mtdCargarPdfPlanAccion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "8";
            #endregion Vars

            dtInfo = cAu.loadCodigoArchivo();

            #region Nombre Archivo

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(), strIdControl,
                    lblIdPlanAccion.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}-{2}",
                    strIdControl, lblIdPlanAccion.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cAu.mtdAgregarArchivoPdf(lblIdPlanAccion.Text.Trim(), strIdControl, txtDescArchivo.Text.Trim(),
                strNombreArchivo, bPdfData);

            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        private void mtdDescargarPdfPlanAccion(string strNombreArchivo)
        {
            #region Vars
            byte[] bPdfData = cAu.mtdDescargarArchivoPdf(strNombreArchivo);
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
        #endregion PDFs
    }
}