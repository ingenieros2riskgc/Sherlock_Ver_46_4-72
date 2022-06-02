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
using System.IO;
using System.Net;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Seguimiento : System.Web.UI.UserControl
    {
        string IdFormulario = "3008";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
                scripManager.RegisterPostBackControl(imgBtnAdjuntar);
                scripManager.RegisterPostBackControl(GridView100);
                scripManager.RegisterPostBackControl(imgBtnInsertarArchivo);
                GridView1.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");

                if (!Page.IsPostBack)
                {
                    PopulateTreeView();
                    GridView1.DataBind();
                    TabContainer1.ActiveTabIndex = 0;
                }
            }
        }

        #region GridView
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleHallazgo.Visible = true;
                btnImgActualizarHallazgo.Visible = true;
                btnImgInsertarHallazgo.Visible = false;
            }
            else if (e.CommandArgument.ToString() == "Anexar")
            {
                filaAnexos.Visible = true;
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
                filaAcciones.Visible = false;
                filaTabGestion.Visible = false;
                imgBtnInsertarArchivo.Visible = true;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton imgBtn = (ImageButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)imgBtn.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview

            if (e.CommandArgument.ToString() == "Avances")
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
            }
            else if (e.CommandArgument.ToString() == "Anexar")
            {
                filaAnexos.Visible = true;
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;

                filaVolver.Visible = false;
                filaPlanAccion.Visible = false;
                GridView100.DataBind();
            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton imgBtn = (ImageButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)imgBtn.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview

            if (e.CommandArgument.ToString() == "Avances")
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
            }
            else if (e.CommandArgument.ToString() == "Anexar")
            {
                filaAnexos.Visible = true;
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;

                filaVolver.Visible = false;
                filaPlanAccionRiesgo.Visible = false;
                GridView100.DataBind();
            }
        }

        protected void GridView11_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleRecomendacion.Visible = true;
                btnImgInsertarRec.Visible = false;
                btnImgActualizarRec.Visible = true;

            }
            else if (e.CommandArgument.ToString() == "PlanAccion")
            {
                filaTabGestion.Visible = false;
                filaPlanAccion.Visible = true;
                filaVolver.Visible = true;
            }
        }

        protected void GridView13_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleRiesgo.Visible = true;
                btnImgInsertarRie.Visible = false;
                btnImgActualizarRie.Visible = true;
            }
            else if (e.CommandArgument.ToString() == "PlanAccion")
            {
                filaTabGestion.Visible = false;
                filaPlanAccionRiesgo.Visible = true;
                filaVolver.Visible = true;
            }
        }

        protected void GridViewSeguimiento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton imgBtn = (ImageButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)imgBtn.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview

            TbNuevoSeguimiento.Visible = true;
            FilaSeguimiento.Visible = false;
            ImBtAdd.Visible = false;
            ImBtGurdarSeguimiento.Visible = false;
            ImBtUpdateSeguimiento.Visible = true;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodHallazgo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                lblIdHallazgo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                SqlDataSource100.SelectParameters["IdControlUsuario"].DefaultValue = "9";
                SqlDataSource100.SelectParameters["IdControlUsuario2"].DefaultValue = "10";
                GridView100.DataBind();

                ddlTipoHallazgo.SelectedValue = GridView1.SelectedDataKey[1].ToString().Trim();
                ddlTipoHallazgo.Focus();
                txtHallazgo.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
                txtComentario.Text = GridView1.SelectedDataKey[3].ToString().Trim();
                ddlEstadoHallazgo.SelectedValue = GridView1.SelectedDataKey[2].ToString().Trim();
                txtUsuarioHallazgo.Text = GridView1.SelectedDataKey[5].ToString().Trim();
                txtFecCreacionHallazgo.Text = GridView1.SelectedRow.Cells[7].Text.Trim();
                txtHallazgoGen.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
                txtCodHallazgoGen.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtHallazgoRec.Text = txtHallazgoGen.Text;
                txtHallazgoRie.Text = txtHallazgoGen.Text;

                GridView11.DataBind();
                GridView13.DataBind();

                TabContainer1.Tabs[1].Enabled = true;
                TabContainer1.Tabs[2].Enabled = true;
            }
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView4.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodPlanAccion.Text = GridView4.SelectedRow.Cells[0].Text.Trim();
                lblIdHallazgo.Text = GridView4.SelectedRow.Cells[0].Text.Trim();
                SqlDataSource100.SelectParameters["IdControlUsuario"].DefaultValue = "8";
                SqlDataSource100.SelectParameters["IdControlUsuario2"].DefaultValue = "0";
                GridView100.DataBind();

                txtPlanPlan.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
                txtAvancePlan.Text = GridView4.SelectedRow.Cells[3].Text.Trim();
                lblIdPlanAccion.Text = GridView4.SelectedRow.Cells[0].Text.Trim();
                txtUsuarioPlan.Text = GridView4.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionPlan.Text = GridView4.SelectedRow.Cells[5].Text.Trim();
                txtFecCierreAuditado.Text = GridView4.SelectedRow.Cells[9].Text.Trim();
                txtFecCierreAuditor.Text = GridView4.SelectedRow.Cells[10].Text.Trim();
                int rows = 0, longMax = 0, rowsAdd = 0;
                double temp = 0;

                txtAvancePlan.Height = 18;
                txtAvancePlan.Width = 402;
                txtPlanPlan.Height = 18;
                txtPlanPlan.Width = 402;

                //string[] lines = Regex.Split(value, "</div");
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
            }
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView5.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodPlanAccion.Text = GridView5.SelectedRow.Cells[0].Text.Trim();
                lblIdHallazgo.Text = GridView5.SelectedRow.Cells[0].Text.Trim();
                SqlDataSource100.SelectParameters["IdControlUsuario"].DefaultValue = "8";
                SqlDataSource100.SelectParameters["IdControlUsuario2"].DefaultValue = "0";
                GridView100.DataBind();

                txtPlanPlan.Text = GridView5.SelectedRow.Cells[3].Text.Trim();
                txtAvancePlan.Text = GridView5.SelectedRow.Cells[3].Text.Trim();
                lblIdPlanAccion.Text = GridView5.SelectedRow.Cells[0].Text.Trim();
                txtUsuarioPlan.Text = GridView5.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionPlan.Text = GridView5.SelectedRow.Cells[5].Text.Trim();
                txtFecCierreAuditado.Text = GridView5.SelectedRow.Cells[9].Text.Trim();
                txtFecCierreAuditor.Text = GridView5.SelectedRow.Cells[10].Text.Trim();

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

        protected void GridView6_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodAuditoriaSel.Text = GridView6.SelectedRow.Cells[0].Text.Trim();
            txtNomAuditoriaSel.Text = GridView6.SelectedRow.Cells[1].Text.Trim();
            txtMetodologiaGen.Text = GridView6.SelectedDataKey[0].ToString().Trim();
            txtCodObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;

            filaMetodologia.Visible = false;
            popupAuditoria.Cancel();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
            filaAcciones.Visible = true;

            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;

            SqlDataSourceSeguimiento.SelectParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
            GridViewSeguimiento.DataBind();
        }

        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodObjetivoSel.Text = GridView7.SelectedRow.Cells[0].Text.Trim();
            txtNomObjetivoSel.Text = GridView7.SelectedRow.Cells[1].Text.Trim();
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            filaMetodologia.Visible = false;
            popupObjetivo.Cancel();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;

            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
        }

        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();

            txtCodAuditoriaSel.Text = "";
            txtCodObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomAuditoriaSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            filaAcciones.Visible = false;
            filaMetodologia.Visible = false;
            popupPlanea.Cancel();
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

            filaMetodologia.Visible = false;
            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            popupEnfoque.Cancel();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;

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

            popupLiteral.Cancel();

            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;
            filaDetalleHallazgo.Visible = false;
            filaMetodologia.Visible = false;
            GridView1.DataBind();
            ddlTipoHallazgo.Items.Clear();
            ddlTipoHallazgo.DataBind();
            txtMetodologia.Text = txtMetodologiaGen.Text;
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
        }

        protected void GridView11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView11.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlRecPoD.SelectedValue = GridView11.SelectedRow.Cells[3].Text.Trim();
                ddlRecPoD.Focus();
                if (GridView11.SelectedRow.Cells[3].Text == "Procesos")
                {
                    txtProcesoRec.Text = GridView11.SelectedDataKey[1].ToString().Trim();
                    lblIdProcesoRec.Text = GridView11.SelectedDataKey[4].ToString().Trim();
                    lblIdDependenciaRec2.Text = "";
                    txtDependenciaRec2.Text = "";
                    filaProcesoRec.Visible = true;
                    filaDependenciaRec.Visible = false;
                }
                else
                {
                    txtDependenciaRec2.Text = GridView11.SelectedDataKey[1].ToString().Trim();
                    lblIdDependenciaRec2.Text = GridView11.SelectedDataKey[0].ToString().Trim();
                    lblIdProcesoRec.Text = "";
                    txtProcesoRec.Text = "";
                    filaProcesoRec.Visible = false;
                    filaDependenciaRec.Visible = true;
                }

                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;
                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;

                txtCodForaneaGen.Text = GridView11.SelectedRow.Cells[0].Text.Trim();
                txtTipoForanea.Text = "RECOMENDACION";
                GridView4.DataBind();

                txtCodRecPlan.Text = GridView11.SelectedRow.Cells[0].Text.Trim();
                txtPlanRec.Text = GridView11.SelectedRow.Cells[10].Text.Trim();
                txtRecPlan.Text = GridView11.SelectedRow.Cells[10].Text.Trim();
                txtObjetivo.Text = txtNomObjetivoSel.Text;
                txtTipoPoD.Text = GridView11.SelectedRow.Cells[3].Text.Trim();
                txtNombrePoD.Text = GridView11.SelectedDataKey[1].ToString().Trim();
                txtRecomendacionPA.Text = GridView11.SelectedRow.Cells[10].Text.Trim();
                txtAvanceRec.Text = GridView11.SelectedRow.Cells[10].Text.Trim();
                txtCodRecPA.Text = GridView11.SelectedRow.Cells[0].Text.Trim();
                txtDescPlan.Text = "";
                txtCodRec.Text = GridView11.SelectedRow.Cells[0].Text.Trim();
                txtDependenciaRec1.Text = GridView11.SelectedDataKey[3].ToString().Trim();
                lblIdDependenciaRec1.Text = GridView11.SelectedDataKey[2].ToString().Trim();
                txtRecomendacion.Text = GridView11.SelectedRow.Cells[10].Text.Trim();
                txtUsuarioRec.Text = GridView11.SelectedDataKey[7].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = GridView11.SelectedRow.Cells[11].Text.Trim();
            }
        }

        protected void GridView13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView13.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodRiesgo.Text = GridView13.SelectedRow.Cells[0].Text.Trim();
                ddlTipoRiesgo.SelectedValue = GridView13.SelectedDataKey[0].ToString().Trim();
                ddlTipoRiesgo.Focus();
                txtNomRiesgo.Text = GridView13.SelectedRow.Cells[2].Text.Trim();
                ddlRiePoD.SelectedValue = GridView13.SelectedDataKey[1].ToString().Trim();

                if (GridView13.SelectedDataKey[1].ToString() == "Procesos")
                {
                    txtProcesoRie.Text = GridView13.SelectedDataKey[4].ToString().Trim();
                    lblIdProcesoRie.Text = GridView13.SelectedDataKey[3].ToString().Trim();
                    lblIdDependenciaRie.Text = "";
                    txtDependenciaRie.Text = "";
                    filaProcesoRie.Visible = true;
                    filaDependenciaRie.Visible = false;
                }
                else
                {
                    txtDependenciaRie.Text = GridView13.SelectedDataKey[4].ToString().Trim();
                    lblIdDependenciaRie.Text = GridView13.SelectedDataKey[2].ToString().Trim();
                    lblIdProcesoRie.Text = "";
                    txtProcesoRie.Text = "";
                    filaProcesoRie.Visible = false;
                    filaDependenciaRie.Visible = true;
                }

                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;

                txtCodForaneaGen.Text = GridView13.SelectedRow.Cells[0].Text.Trim();
                txtTipoForanea.Text = "RIESGO";
                GridView5.DataBind();

                txtCodRiesgoPlan.Text = GridView13.SelectedRow.Cells[0].Text.Trim();
                txtPlanRec.Text = GridView13.SelectedRow.Cells[2].Text.Trim();
                txtRiesgoPlan.Text = GridView13.SelectedRow.Cells[2].Text.Trim();

                txtObjetivo.Text = txtNomObjetivoSel.Text;
                txtTipoPoD.Text = GridView13.SelectedDataKey[1].ToString().Trim();
                txtNombrePoD.Text = GridView13.SelectedDataKey[4].ToString().Trim();
                txtRecomendacionPA.Text = GridView13.SelectedRow.Cells[2].Text.Trim();
                txtAvanceRec.Text = GridView13.SelectedRow.Cells[2].Text.Trim();
                txtCodRecPA.Text = GridView13.SelectedRow.Cells[0].Text.Trim();
                txtDescPlan.Text = "";

                txtDescRiesgo.Text = GridView13.SelectedDataKey[6].ToString().Trim();
                ddlProbabilidad.SelectedValue = GridView13.SelectedDataKey[7].ToString().Trim();
                ddlImpacto.SelectedValue = GridView13.SelectedDataKey[8].ToString().Trim();
                txtUsuarioRie.Text = GridView13.SelectedDataKey[10].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRie.Text = GridView13.SelectedRow.Cells[13].Text;
            }
        }

        protected void GridView100_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameFile = GridView100.SelectedRow.Cells[1].Text.Trim();
            mtdDescargarPdfSeguimiento(nameFile);
        }

        protected void GridViewSeguimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridViewSeguimiento.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                LabelIdSeguimientoAuditoria.Text = GridViewSeguimiento.SelectedRow.Cells[0].Text.Trim();
                TextBox1.Text = WebUtility.HtmlDecode(GridViewSeguimiento.SelectedRow.Cells[1].Text.Trim());
                TextBox2.Text = GridViewSeguimiento.SelectedRow.Cells[2].Text.Trim();
                TextBox3.Text = GridViewSeguimiento.SelectedRow.Cells[3].Text.Trim();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image img1 = (Image)e.Row.FindControl("Image1");

            // Check if row is data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // If el valor es "S" entonces cambia la imagen a apply.png
                if (((GridView)sender).DataKeys[e.Row.RowIndex].Values[6].ToString().Trim() == "S")
                    img1.ImageUrl = "~/Imagenes/Icons/apply.png";
                else
                    img1.ImageUrl = "~/Imagenes/Icons/aim_away.png";
            }
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GridView11_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image img1 = (Image)e.Row.FindControl("Image1");

            // Check if row is data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // If el valor es "S" entonces cambia la imagen a apply.png
                if (((GridView)sender).DataKeys[e.Row.RowIndex].Values[8].ToString().Trim() == "S")
                    img1.ImageUrl = "~/Imagenes/Icons/apply.png";
                else
                    img1.ImageUrl = "~/Imagenes/Icons/aim_away.png";
            }
        }

        protected void GridView13_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image img1 = (Image)e.Row.FindControl("Image1");

            // Check if row is data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // If el valor es "S" entonces cambia la imagen a apply.png
                if (((GridView)sender).DataKeys[e.Row.RowIndex].Values[11].ToString().Trim() == "S")
                    img1.ImageUrl = "~/Imagenes/Icons/apply.png";
                else
                    img1.ImageUrl = "~/Imagenes/Icons/aim_away.png";
            }
        }
        #endregion

        #region DDLs
        protected void ddlRecPoD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRecPoD.SelectedItem.Value == "Procesos")
            {
                filaDependenciaRec.Visible = false;
                filaProcesoRec.Visible = true;
            }
            else
            {
                filaDependenciaRec.Visible = true;
                filaProcesoRec.Visible = false;
            }
        }

        protected void ddlRiePoD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRiePoD.SelectedItem.Value == "Procesos")
            {
                filaDependenciaRie.Visible = false;
                filaProcesoRie.Visible = true;
            }
            else
            {
                filaDependenciaRie.Visible = true;
                filaProcesoRie.Visible = false;
            }
        }

        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
        {
            ddlMacroProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlProceso.DataBind();
            txtProcesoRec.Text = "";
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecomendacion.Visible == true)
            {
                txtProcesoRec.Text = ddlProceso.SelectedItem.Text;
                lblIdProcesoRec.Text = ddlProceso.SelectedValue;
            }
            else if (filaDetalleRiesgo.Visible == true)
            {
                txtProcesoRie.Text = ddlProceso.SelectedItem.Text;
                lblIdProcesoRie.Text = ddlProceso.SelectedValue;
            }
        }

        protected void ddlMacroProcesoRie_DataBound(object sender, EventArgs e)
        {
            ddlMacroProcesoRie.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProcesoRie_DataBound(object sender, EventArgs e)
        {
            ddlProcesoRie.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlMacroProcesoRie_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesoRie.Items.Clear();
            ddlProcesoRie.DataBind();
            txtProcesoRie.Text = "";
        }

        protected void ddlProcesoRie_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProcesoRie.Text = ddlProcesoRie.SelectedItem.Text;
            lblIdProcesoRie.Text = ddlProcesoRie.SelectedValue;
        }

        protected void ddlTipoHallazgo_DataBound(object sender, EventArgs e)
        {
            ddlTipoHallazgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEstadoHallazgo_DataBound(object sender, EventArgs e)
        {
            ddlEstadoHallazgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlTipoRiesgo_DataBound(object sender, EventArgs e)
        {
            ddlTipoRiesgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlProbabilidad_DataBound(object sender, EventArgs e)
        {
            ddlProbabilidad.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlImpacto_DataBound(object sender, EventArgs e)
        {
            ddlImpacto.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }
        #endregion

        #region TreeView
        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView()
        {
            TreeNodeCollection nodes = this.TreeView1.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData, 1);
            }

            nodes = this.TreeView2.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData2 = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData2, 2);
            }

            nodes = this.TreeView3.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData3 = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData3, 3);
            }
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        private DataTable GetTreeViewData()
        {
            // Get JerarquiaOrganizacional table
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        /// <summary>
        /// Filter the data to get only the rows that have a
        /// null ParentID (these are the top-level TreeView items)
        /// </summary>
        private void AddTopTreeViewNodes(DataTable treeViewData, int Arbol)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                if (Arbol == 1)
                    TreeView1.Nodes.Add(newNode);
                else if (Arbol == 2)
                    TreeView2.Nodes.Add(newNode);
                else if (Arbol == 3)
                    TreeView3.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecomendacion.Visible == true)
            {
                txtDependenciaRec1.Text = TreeView1.SelectedNode.Text;
                lblIdDependenciaRec1.Text = TreeView1.SelectedNode.Value;
            }
        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependenciaRec2.Text = TreeView2.SelectedNode.Text;
            lblIdDependenciaRec2.Text = TreeView2.SelectedNode.Value;
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependenciaRie.Text = TreeView3.SelectedNode.Text;
            lblIdDependenciaRie.Text = TreeView3.SelectedNode.Value;
        }
        #endregion

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalle.Visible = false;
            filaBtnTemas.Visible = false;
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
            bool err = false;

            mpeMsgBox.Hide();

            try
            {
                if (lblAccion.Text == "")
                {
                    if (TabContainer1.ActiveTabIndex.ToString() == "0")
                    {
                        SqlDataSource1.DeleteParameters["IdHallazgo"].DefaultValue = txtCodHallazgo.Text;
                        SqlDataSource1.Delete();
                    }
                    else if (TabContainer1.ActiveTabIndex.ToString() == "1")
                    {
                        SqlDataSource22.DeleteParameters["IdRecomendacion"].DefaultValue = txtCodRec.Text;
                        SqlDataSource22.Delete();
                    }
                    else if (TabContainer1.ActiveTabIndex.ToString() == "2")
                    {
                        SqlDataSource23.DeleteParameters["IdRiesgo"].DefaultValue = txtCodRiesgo.Text;
                        SqlDataSource23.Delete();
                    }
                }
                else if (lblAccion.Text == "CIERRE")
                {
                    SqlDataSource24.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource24.UpdateParameters["Estado"].DefaultValue = "CERRADA";
                    SqlDataSource24.Update();
                }
            }
            catch (Exception except)
            {
                if (lblAccion.Text == "CIERRE")
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                else
                    omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                if (lblAccion.Text == "CIERRE")
                {
                    filaAcciones.Visible = false;
                    filaTabGestion.Visible = false;
                    txtCodAuditoriaSel.Text = "";
                    txtCodObjetivoSel.Text = "";
                    txtCodEnfoqueSel.Text = "";
                    txtCodLiteralSel.Text = "";
                    txtNomAuditoriaSel.Text = "";
                    txtNomObjetivoSel.Text = "";
                    txtNomEnfoqueSel.Text = "";
                    txtNomLiteralSel.Text = "";
                    txtNomEnfoqueSel.Height = 18;
                    txtNomEnfoqueSel.Width = 402;
                    txtNomLiteralSel.Height = 18;
                    txtNomLiteralSel.Width = 402;
                    GridView6.DataBind();
                    imgBtnAuditoria.Focus();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                else
                {
                    omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");

                    if (TabContainer1.ActiveTabIndex.ToString() == "0")
                    {
                        TabContainer1.Tabs[1].Enabled = false;
                        TabContainer1.Tabs[2].Enabled = false;
                    }
                }
            }
        }

        protected void btnTemasAud_Click(object sender, EventArgs e)
        {
            filaDetalle.Visible = false;
            filaBtnTemas.Visible = false;
            filaAcciones.Visible = true;
            GridView1.DataBind();
        }

        protected void btnImgCancelarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleHallazgo.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;
        }

        protected void btnImgCancelarRec_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRecomendacion.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgCancelarRie_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRiesgo.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void imgBtnRiesgo_Click(object sender, EventArgs e)
        {
            TabContainer1.Tabs[1].Enabled = true;
            TabContainer1.Tabs[2].Enabled = true;
            TabContainer1.ActiveTabIndex = 2;

            btnVolverRec.Text = "Volver a Riesgos";
            lblRR.Text = "Riesgo:";
            lblAvanceRR.Text = "Riesgo:";
            lblPARR.Text = "Riesgo:";
            imgBtnInsertarArchivo.Visible = false;
        }

        protected void imgBtnRecomendacion_Click(object sender, EventArgs e)
        {
            TabContainer1.Tabs[1].Enabled = true;
            TabContainer1.Tabs[2].Enabled = true;
            TabContainer1.ActiveTabIndex = 1;

            btnVolverRec.Text = "Volver a Recomendaciones";
            lblRR.Text = "Recomendación:";
            lblAvanceRR.Text = "Recomendación:";
            lblPARR.Text = "Recomendación:";
            imgBtnInsertarArchivo.Visible = false;
        }

        protected void btnImgEliminarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void imgBtnInsertarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtCodHallazgo.Text = "";
                ddlTipoHallazgo.SelectedValue = null;
                ddlTipoHallazgo.Focus();
                txtHallazgo.Text = "";
                txtComentario.Text = "";
                ddlEstadoHallazgo.SelectedValue = null;
                txtFecCreacionHallazgo.Text = "";
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleHallazgo.Visible = true;
                txtUsuarioHallazgo.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionHallazgo.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarHallazgo.Visible = false;
                btnImgInsertarHallazgo.Visible = true;
            }
        }

        protected void btnImgEliminarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void imgBtnInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlRecPoD.SelectedValue = "Procesos";
                ddlRecPoD.Focus();
                filaProcesoRec.Visible = true;
                filaDependenciaRec.Visible = false;
                txtCodRec.Text = "";
                txtDependenciaRec1.Text = "";
                txtDependenciaRec2.Text = "";
                txtProcesoRec.Text = "";
                txtRecomendacion.Text = "";
                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;
                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarRec.Visible = false;
                btnImgInsertarRec.Visible = true;
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleRecomendacion.Visible = true;
            }
        }

        protected void btnImgEliminarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void imgBtnInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtNomRiesgo.Text = "";
                ddlTipoRiesgo.Focus();
                ddlTipoRiesgo.SelectedValue = null;
                ddlRiePoD.SelectedValue = "Procesos";
                filaProcesoRie.Visible = true;
                filaDependenciaRie.Visible = false;
                txtCodRiesgo.Text = "";
                txtDependenciaRie.Text = "";
                txtProcesoRie.Text = "";
                txtDescRiesgo.Text = "";
                txtUsuarioRie.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                ddlProbabilidad.SelectedValue = null;
                ddlImpacto.SelectedValue = null;
                txtFecCreacionRie.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarRie.Visible = false;
                btnImgInsertarRie.Visible = true;
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleRiesgo.Visible = true;
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgInsertarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource1.InsertParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource1.InsertParameters["IdDetalleEnfoque"].DefaultValue = txtCodLiteralSel.Text;
                    SqlDataSource1.InsertParameters["IdDetalleTipoHallazgo"].DefaultValue = ddlTipoHallazgo.SelectedValue;
                    SqlDataSource1.InsertParameters["IdEstado"].DefaultValue = ddlEstadoHallazgo.SelectedValue;
                    SqlDataSource1.InsertParameters["Hallazgo"].DefaultValue = txtHallazgo.Text;
                    SqlDataSource1.InsertParameters["ComentarioAuditado"].DefaultValue = txtComentario.Text;
                    SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource1.InsertParameters["Seguimiento"].DefaultValue = "S";
                    SqlDataSource1.Insert();
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaAcciones.Visible = true;
                    filaDetalleHallazgo.Visible = false;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource1.UpdateParameters["IdDetalleTipoHallazgo"].DefaultValue = ddlTipoHallazgo.SelectedValue;
                        SqlDataSource1.UpdateParameters["IdEstado"].DefaultValue = ddlEstadoHallazgo.SelectedValue;
                        SqlDataSource1.UpdateParameters["Hallazgo"].DefaultValue = txtHallazgo.Text;
                        SqlDataSource1.UpdateParameters["IdHallazgo"].DefaultValue = txtCodHallazgo.Text;
                        SqlDataSource1.UpdateParameters["ComentarioAuditado"].DefaultValue = txtComentario.Text;
                        SqlDataSource1.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaAcciones.Visible = true;
                        filaDetalleHallazgo.Visible = false;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource22.InsertParameters["IdHallazgo"].DefaultValue = txtCodHallazgoGen.Text;
                    SqlDataSource22.InsertParameters["Tipo"].DefaultValue = ddlRecPoD.SelectedValue;

                    if (ddlRecPoD.SelectedValue == "Dependencia")
                        SqlDataSource22.InsertParameters["IdDependenciaAuditada"].DefaultValue = lblIdDependenciaRec2.Text;
                    else
                        SqlDataSource22.InsertParameters["IdSubproceso"].DefaultValue = lblIdProcesoRec.Text;

                    SqlDataSource22.InsertParameters["IdDependenciaRespuesta"].DefaultValue = lblIdDependenciaRec1.Text;
                    SqlDataSource22.InsertParameters["Estado"].DefaultValue = "Formulado";
                    SqlDataSource22.InsertParameters["Observacion"].DefaultValue = txtRecomendacion.Text;
                    SqlDataSource22.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource22.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource22.InsertParameters["Seguimiento"].DefaultValue = "S";
                    SqlDataSource22.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaDetalleRecomendacion.Visible = false;
                    filaAcciones.Visible = true;
                }
                catch (Exception except)
                {
                    //Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {

                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource22.UpdateParameters["IdRecomendacion"].DefaultValue = txtCodRec.Text;
                        SqlDataSource22.UpdateParameters["Tipo"].DefaultValue = ddlRecPoD.SelectedValue;
                        if (ddlRecPoD.SelectedValue == "Dependencia")
                        {
                            SqlDataSource22.UpdateParameters["IdDependenciaAuditada"].DefaultValue = lblIdDependenciaRec2.Text;
                            SqlDataSource22.UpdateParameters["IdSubproceso"].DefaultValue = null;
                        }
                        else
                        {
                            SqlDataSource22.UpdateParameters["IdSubproceso"].DefaultValue = lblIdProcesoRec.Text;
                            SqlDataSource22.UpdateParameters["IdDependenciaAuditada"].DefaultValue = null;
                        }
                        SqlDataSource22.UpdateParameters["IdDependenciaRespuesta"].DefaultValue = lblIdDependenciaRec1.Text;
                        SqlDataSource22.UpdateParameters["Observacion"].DefaultValue = txtRecomendacion.Text;

                        SqlDataSource22.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaDetalleRecomendacion.Visible = false;
                        filaAcciones.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource23.InsertParameters["IdHallazgo"].DefaultValue = txtCodHallazgoGen.Text;
                    SqlDataSource23.InsertParameters["Nombre"].DefaultValue = txtNomRiesgo.Text;
                    SqlDataSource23.InsertParameters["IdDetalleTipoRiesgo"].DefaultValue = ddlTipoRiesgo.SelectedValue;
                    SqlDataSource23.InsertParameters["Tipo"].DefaultValue = ddlRiePoD.SelectedValue;

                    if (ddlRiePoD.SelectedValue == "Dependencia")
                    {
                        SqlDataSource23.InsertParameters["IdDependencia"].DefaultValue = lblIdDependenciaRie.Text;
                        SqlDataSource23.InsertParameters["IdSubproceso"].DefaultValue = null;
                    }
                    else
                    {
                        SqlDataSource23.InsertParameters["IdDependencia"].DefaultValue = null;
                        SqlDataSource23.InsertParameters["IdSubproceso"].DefaultValue = lblIdProcesoRie.Text;
                    }

                    SqlDataSource23.InsertParameters["Estado"].DefaultValue = "Formulado";
                    SqlDataSource23.InsertParameters["Observacion"].DefaultValue = txtDescRiesgo.Text;
                    SqlDataSource23.InsertParameters["IdDetalleTipoProbabilidad"].DefaultValue = ddlProbabilidad.SelectedValue;
                    SqlDataSource23.InsertParameters["IdDetalleTipoImpacto"].DefaultValue = ddlImpacto.SelectedValue;

                    SqlDataSource23.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource23.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource23.InsertParameters["Seguimiento"].DefaultValue = "S";

                    SqlDataSource23.Insert();
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaDetalleRiesgo.Visible = false;
                    filaAcciones.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource23.UpdateParameters["IdRiesgo"].DefaultValue = txtCodRiesgo.Text;
                        SqlDataSource23.UpdateParameters["Nombre"].DefaultValue = txtNomRiesgo.Text;
                        SqlDataSource23.UpdateParameters["IdDetalleTipoRiesgo"].DefaultValue = ddlTipoRiesgo.SelectedValue;
                        SqlDataSource23.UpdateParameters["Tipo"].DefaultValue = ddlRiePoD.SelectedValue;

                        if (ddlRiePoD.SelectedValue == "Dependencia")
                        {
                            SqlDataSource23.UpdateParameters["IdDependencia"].DefaultValue = lblIdDependenciaRie.Text;
                            SqlDataSource23.UpdateParameters["IdSubproceso"].DefaultValue = null;
                        }
                        else
                        {
                            SqlDataSource23.UpdateParameters["IdDependencia"].DefaultValue = null;
                            SqlDataSource23.UpdateParameters["IdSubproceso"].DefaultValue = lblIdProcesoRie.Text;
                        }

                        SqlDataSource23.UpdateParameters["Observacion"].DefaultValue = txtDescRiesgo.Text;
                        SqlDataSource23.UpdateParameters["IdDetalleTipoProbabilidad"].DefaultValue = ddlProbabilidad.SelectedValue;
                        SqlDataSource23.UpdateParameters["IdDetalleTipoImpacto"].DefaultValue = ddlImpacto.SelectedValue;

                        SqlDataSource23.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaDetalleRiesgo.Visible = false;
                        filaAcciones.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarMetodologia_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    SqlDataSource9.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource9.UpdateParameters["Metodologia"].DefaultValue = txtMetodologia.Text;
                    SqlDataSource9.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }

                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void imgBtnAdjuntar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                {
                    mtdCargarPdfSeguimiento();
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
                txtDescArchivo.Text = "";
            }
        }

        protected void btnImgCancelarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        protected void btnVolverArchivo_Click(object sender, EventArgs e)
        {
            filaAnexos.Visible = false;
            filaAcciones.Visible = true;

            if (TabContainer1.ActiveTabIndex == 0)
                filaTabGestion.Visible = true;
            else if (TabContainer1.ActiveTabIndex == 1)
                filaPlanAccion.Visible = true;
            else if (TabContainer1.ActiveTabIndex == 2)
                filaPlanAccionRiesgo.Visible = true;

            filaVolver.Visible = true;
        }

        protected void btnCierre_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "CIERRE";
                lblMsgBox.Text = "Desea realizar el Cierre de Seguimiento a la auditoría?";
                mpeMsgBox.Show();
            }
        }

        protected void btnVolverPlanes_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 1)
                filaPlanAccion.Visible = true;
            else
                if (TabContainer1.ActiveTabIndex == 2)
                    filaPlanAccionRiesgo.Visible = true;

            filaAvance.Visible = false;
        }

        protected void btnImgCancelarDetPlan_Click(object sender, ImageClickEventArgs e)
        {
            filaDetallePlan.Visible = false;
            if (TabContainer1.ActiveTabIndex == 1)
                filaPlanAccion.Visible = true;
            else if (TabContainer1.ActiveTabIndex == 2)
                filaPlanAccionRiesgo.Visible = true;

            filaVolver.Visible = true;
        }

        protected void btnImgCancelarRegAvances_Click(object sender, ImageClickEventArgs e)
        {
            filaRegistrarAvances.Visible = false;
            filaAvance.Visible = true;
        }

        protected void btnVolverRec_Click(object sender, EventArgs e)
        {
            filaPlanAccion.Visible = false;
            filaTabGestion.Visible = true;
            filaDetallePlan.Visible = false;
            filaVolver.Visible = false;
            filaAvance.Visible = false;
            filaPlanAccionRiesgo.Visible = false;
        }

        protected void btnImformeAud_Click(object sender, EventArgs e)
        {
            string str;
            str = "window.open('AudAdmReporteAuditoriaSeg1.aspx?Ca=" + txtCodAuditoriaSel.Text + "','Reporte','width=800,height=600,scrollbars=yes,resizable=yes')";
            Response.Write("<script languaje=javascript>" + str + "</script>");
        }

        protected void ImBtAdd_Click(object sender, ImageClickEventArgs e)
        {
            TextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            TextBox3.Text = Session["loginUsuario"].ToString().Trim();
            ImBtAdd.Visible = false;
            TbNuevoSeguimiento.Visible = true;
            FilaSeguimiento.Visible = false;
            ImBtGurdarSeguimiento.Visible = true;
            ImBtUpdateSeguimiento.Visible = false;
        }

        protected void ImBtGurdarSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SqlDataSourceSeguimiento.InsertParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                SqlDataSourceSeguimiento.InsertParameters["Seguimiento"].DefaultValue = TextBox1.Text.Trim();
                SqlDataSourceSeguimiento.InsertParameters["FechaSeguimiento"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSourceSeguimiento.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                SqlDataSourceSeguimiento.Insert();
                omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                TbNuevoSeguimiento.Visible = false;
                FilaSeguimiento.Visible = true;
                ImBtAdd.Visible = true;
                TextBox1.Text = "";
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
            }
        }

        protected void ImBtCancelSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            TbNuevoSeguimiento.Visible = false;
            FilaSeguimiento.Visible = true;
            TextBox1.Text = "";
            ImBtAdd.Visible = true;
        }

        protected void ImBtUpdateSeguimiento_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    SqlDataSourceSeguimiento.UpdateParameters["IdAuditoriaSeguimiento"].DefaultValue = LabelIdSeguimientoAuditoria.Text.Trim();
                    SqlDataSourceSeguimiento.UpdateParameters["Seguimiento"].DefaultValue = TextBox1.Text.Trim().Trim();
                    SqlDataSourceSeguimiento.UpdateParameters["FechaSeguimiento"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSourceSeguimiento.UpdateParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                    SqlDataSourceSeguimiento.Update();

                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    TbNuevoSeguimiento.Visible = false;
                    FilaSeguimiento.Visible = true;
                    TextBox1.Text = "";
                    ImBtAdd.Visible = true;
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }
        #endregion

        private string DetalleNodo(int tipoSelect, string idHijo)
        {
            string Detalle = "";
            string selectCommand = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            if (tipoSelect == 1)
                selectCommand = "SELECT NombreResponsable,CorreoResponsable FROM [Parametrizacion].[DetalleJerarquiaOrg] WHERE idHijo = " + idHijo;
            else
                selectCommand = "SELECT NombreResponsable,CorreoResponsable, NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] LEFT OUTER JOIN [Parametrizacion].[DetalleJerarquiaOrg] ON [DetalleJerarquiaOrg].idHijo = [JerarquiaOrganizacional].idHijo WHERE [JerarquiaOrganizacional].idHijo = " + idHijo;

            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                Detalle = "Responsable: " + row["NombreResponsable"].ToString() + "\r";
                Detalle = Detalle + "Correo: " + row["CorreoResponsable"].ToString().Trim();

                if (tipoSelect == 2)
                    Detalle = Detalle + "\r Nodo Jerarquía Org.: " + row["NombreHijo"].ToString().Trim();
            }

            if (Detalle == "")
                Detalle = "Responsable: \rCorreo:";

            return (Detalle);
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 3)
                filaMetodologia.Visible = true;
            else
                filaMetodologia.Visible = false;

            if (TabContainer1.ActiveTabIndex == 1)
            {
                btnVolverRec.Text = "Volver a Recomendaciones";
                lblRR.Text = "Recomendación:";
                lblAvanceRR.Text = "Recomendación:";
                lblPARR.Text = "Recomendación:";
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                btnVolverRec.Text = "Volver a Riesgos";
                lblRR.Text = "Riesgo:";
                lblAvanceRR.Text = "Riesgo:";
                lblPARR.Text = "Riesgo:";
            }
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (TabContainer1.ActiveTabIndex == 0)
            {
                if (ddlTipoHallazgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Tipo de Hallazgo.", 2, "Atención");
                    ddlTipoHallazgo.Focus();
                }
                else if (ValidarCadenaVacia(txtHallazgo.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Hallazgo.", 2, "Atención");
                    txtHallazgo.Focus();
                }
                else if (ddlEstadoHallazgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Estado del Hallazgo.", 2, "Atención");
                    ddlEstadoHallazgo.Focus();
                }
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                if (ddlRecPoD.SelectedValue == "Procesos" && ValidarCadenaVacia(txtProcesoRec.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar un Proceso.", 2, "Atención");
                    txtProcesoRec.Focus();
                }
                else if (ddlRecPoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(txtDependenciaRec2.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia.", 2, "Atención");
                    txtDependenciaRec2.Focus();
                }
                else if (ValidarCadenaVacia(txtDependenciaRec1.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia de Respuesta.", 2, "Atención");
                    txtDependenciaRec1.Focus();
                }
                else if (ValidarCadenaVacia(txtRecomendacion.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Recomendacion.", 2, "Atención");
                    txtRecomendacion.Focus();
                }
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                if (ddlTipoRiesgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Tipo de Riesgo.", 2, "Atención");
                    ddlTipoRiesgo.Focus();
                }
                else if (ValidarCadenaVacia(txtNomRiesgo.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Nombre del Riesgo.", 2, "Atención");
                    txtNomRiesgo.Focus();
                }
                else if (ValidarCadenaVacia(txtDescRiesgo.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Descripcion del Riesgo.", 2, "Atención");
                    txtDescRiesgo.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Procesos" && ValidarCadenaVacia(txtProcesoRie.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar un Proceso.", 2, "Atención");
                    txtProcesoRie.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(txtDependenciaRie.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia.", 2, "Atención");
                    txtDependenciaRie.Focus();
                }
                else if (ddlProbabilidad.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar la Probabilidad.", 2, "Atención");
                    ddlProbabilidad.Focus();
                }
                else if (ddlImpacto.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Impacto.", 2, "Atención");
                    ddlImpacto.Focus();
                }
            }
            return err;
        }

        protected Boolean ValidarCadenaVacia(string cadena)
        {
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

        #region PDFs
        private void descargarArchivo(string nameFile)
        {
            string filePath = Server.MapPath("~/Archivos/PDFsGestionVerificacion/" + nameFile);

            try
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nameFile + ";");
                Response.TransmitFile(filePath);
                Response.End();
            }
            catch (Exception ex)
            {
                omb.ShowMessage("El archivo no pudo ser descargado del servidor de datos." + "<br/>" + "Se presento el siguiente error:" + ex.Message, 1, "Atencion");
            }
        }

        private void loadFile()
        {
            #region
            bool err = false;
            string IdMaximo;
            string nameFile;
            #endregion

            //Calcula el siguiente codigo a asignar como id de la tabla Auditoria.Archivo
            DataView dvArchivo = (DataView)this.SqlDataSource101.Select(new DataSourceSelectArguments());
            IdMaximo = dvArchivo[0]["Maximo"].ToString().Trim();

            //En la formacion del nombre del archivo el segundo item, es decir -9-, corresponde al codigo de la tabla
            // Parametrizacion.ControlesUsuario , en este caso al control de usuario Gestión de Auditoría
            nameFile = IdMaximo + "-9-" + lblIdHallazgo.Text.Trim() + "-" + FileUpload1.FileName.ToString().Trim();

            #region
            try
            {
                #region
                try
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsGestionVerificacion/") + nameFile);
                }
                catch
                {
                    err = true;
                }
                #endregion

                if (!err)
                {
                    #region
                    SqlDataSource100.InsertParameters["IdControlUsuario"].DefaultValue = "9";
                    SqlDataSource100.InsertParameters["IdRegistro"].DefaultValue = lblIdHallazgo.Text.Trim();
                    SqlDataSource100.InsertParameters["UrlArchivo"].DefaultValue = nameFile;
                    SqlDataSource100.InsertParameters["Descripcion"].DefaultValue = txtDescArchivo.Text;
                    SqlDataSource100.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource100.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                    SqlDataSource100.Insert();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                #region
                omb.ShowMessage("El archivo no pudo ser cargado." + "<br/>" + "Se presento el siguiente error:" + ex.Message, 1, "Atencion");
                err = true;
                #endregion
            }
            #endregion

            #region
            if (!err)
            {
                omb.ShowMessage("El archivo se cargo con exito en el servidor de datos." + "<br/>" + "Tamaño del Archivo: " + FileUpload1.FileBytes.Length / 1024 + " Kb" + "<br/> Nombre del Archivo: " + nameFile, 3, "Atención");
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
            }
            #endregion
        }

        private void mtdCargarPdfSeguimiento()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "9";
            #endregion Vars

            dtInfo = cAu.loadCodigoArchivo();

            #region Nombre Archivo

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(), strIdControl,
                    lblIdHallazgo.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}-{2}",
                    strIdControl, lblIdHallazgo.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cAu.mtdAgregarArchivoPdf(lblIdHallazgo.Text.Trim(), strIdControl, txtDescArchivo.Text.Trim(),
                strNombreArchivo, bPdfData);

            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        private void mtdDescargarPdfSeguimiento(string strNombreArchivo)
        {
            #region Vars
            //string strNombreArchivo = InfoGridControles.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim();
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