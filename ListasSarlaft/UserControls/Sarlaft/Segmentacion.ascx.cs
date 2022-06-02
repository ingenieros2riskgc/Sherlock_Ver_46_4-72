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
    public partial class Segmentacion : System.Web.UI.UserControl
    {
        string IdFormulario = "6001";
        cSegmentacion cSegmentacion = new cSegmentacion();
        cAtributos cAtributos = new cAtributos();
        cCuenta cCuenta = new cCuenta();

        #region Properties

        private DataTable infoGridFactorRiesgo;
        private DataTable InfoGridFactorRiesgo
        {
            get
            {
                infoGridFactorRiesgo = (DataTable)ViewState["infoGridFactorRiesgo"];
                return infoGridFactorRiesgo;
            }
            set
            {
                infoGridFactorRiesgo = value;
                ViewState["infoGridFactorRiesgo"] = infoGridFactorRiesgo;
            }
        }

        private DataTable infoGridSegmento;
        private DataTable InfoGridSegmento
        {
            get
            {
                infoGridSegmento = (DataTable)ViewState["infoGridSegmento"];
                return infoGridSegmento;
            }
            set
            {
                infoGridSegmento = value;
                ViewState["infoGridSegmento"] = infoGridSegmento;
            }
        }

        private DataTable infoGridTipoSegmento;
        private DataTable InfoGridTipoSegmento
        {
            get
            {
                infoGridTipoSegmento = (DataTable)ViewState["infoGridTipoSegmento"];
                return infoGridTipoSegmento;
            }
            set
            {
                infoGridTipoSegmento = value;
                ViewState["infoGridTipoSegmento"] = infoGridTipoSegmento;
            }
        }

        private DataTable infoGridAtributo;
        private DataTable InfoGridAtributo
        {
            get
            {
                infoGridAtributo = (DataTable)ViewState["infoGridAtributo"];
                return infoGridAtributo;
            }
            set
            {
                infoGridAtributo = value;
                ViewState["infoGridAtributo"] = infoGridAtributo;
            }
        }

        private DataTable infoGridPerfilSegmento;
        private DataTable InfoGridPerfilSegmento
        {
            get
            {
                infoGridPerfilSegmento = (DataTable)ViewState["infoGridPerfilSegmento"];
                return infoGridPerfilSegmento;
            }
            set
            {
                infoGridPerfilSegmento = value;
                ViewState["infoGridPerfilSegmento"] = infoGridPerfilSegmento;
            }
        }

        private DataTable infoGridIndicador;
        private DataTable InfoGridIndicador
        {
            get
            {
                infoGridIndicador = (DataTable)ViewState["infoGridIndicador"];
                return infoGridIndicador;
            }
            set
            {
                infoGridIndicador = value;
                ViewState["infoGridIndicador"] = infoGridIndicador;
            }
        }

        private int rowGridFactorRiesgo;
        private int RowGridFactorRiesgo
        {
            get
            {
                rowGridFactorRiesgo = (int)ViewState["rowGridFactorRiesgo"];
                return rowGridFactorRiesgo;
            }
            set
            {
                rowGridFactorRiesgo = value;
                ViewState["rowGridFactorRiesgo"] = rowGridFactorRiesgo;
            }
        }

        private int rowGridSegmento;
        private int RowGridSegmento
        {
            get
            {
                rowGridSegmento = (int)ViewState["rowGridSegmento"];
                return rowGridSegmento;
            }
            set
            {
                rowGridSegmento = value;
                ViewState["rowGridSegmento"] = rowGridSegmento;
            }
        }

        private int rowGridTipoSegmento;
        private int RowGridTipoSegmento
        {
            get
            {
                rowGridTipoSegmento = (int)ViewState["rowGridTipoSegmento"];
                return rowGridTipoSegmento;
            }
            set
            {
                rowGridTipoSegmento = value;
                ViewState["rowGridTipoSegmento"] = rowGridTipoSegmento;
            }
        }

        private int rowGridAtributo;
        private int RowGridAtributo
        {
            get
            {
                rowGridAtributo = (int)ViewState["rowGridAtributo"];
                return rowGridAtributo;
            }
            set
            {
                rowGridAtributo = value;
                ViewState["rowGridAtributo"] = rowGridAtributo;
            }
        }

        private int rowGridPerfilSegmento;
        private int RowGridPerfilSegmento
        {
            get
            {
                rowGridPerfilSegmento = (int)ViewState["rowGridPerfilSegmento"];
                return rowGridPerfilSegmento;
            }
            set
            {
                rowGridPerfilSegmento = value;
                ViewState["rowGridPerfilSegmento"] = rowGridPerfilSegmento;
            }
        }

        private int rowGridIndicador;
        private int RowGridIndicador
        {
            get
            {
                rowGridIndicador = (int)ViewState["rowGridIndicador"];
                return rowGridIndicador;
            }
            set
            {
                rowGridIndicador = value;
                ViewState["rowGridIndicador"] = rowGridIndicador;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                loadGridFactorRiesgo();
                loadInfoFactorRiesgo();
                loadCodigoFactorRiesgo();
                loadDDListAtributo();
            }
        }

        #region Buttons
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    addFactorRiesgo();
                    loadGridFactorRiesgo();
                    loadInfoFactorRiesgo();
                    resetValuesFactorRiesgo();
                    loadCodigoFactorRiesgo();
                    Mensaje("Factor de riesgo agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar el factor de riesgo. " + ex.Message);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateFactorRiesgo();
                    loadGridFactorRiesgo();
                    loadInfoFactorRiesgo();
                    resetValuesFactorRiesgo();
                    loadCodigoFactorRiesgo();
                    Mensaje("Factor de riesgo actualizado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el factor de riesgo. " + ex.Message);
            }
        }

        protected void ImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    addSegmento();
                    loadGridSegmento();
                    loadInfoSegmento();
                    resetValuesSegmentos();
                    tbSegmentos.Visible = true;
                    loadCodigoSegmento();
                    Mensaje("Segmento agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar el segmento. " + ex.Message);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateSegmento();
                    loadGridSegmento();
                    loadInfoSegmento();
                    resetValuesSegmentos();
                    tbSegmentos.Visible = true;
                    loadCodigoSegmento();
                    Mensaje("Segmento actualizado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el segmento. " + ex.Message);
            }
        }

        protected void ImageButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    addTipoSegmento();
                    loadGridTipoSegmento();
                    loadInfoTipoSegmento();
                    resetValuesTipoSegmento();
                    tbTiposSegmento.Visible = true;
                    loadCodigoTipoSegmento();
                    Mensaje("Tipo segmento agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar el tipo de segmento. " + ex.Message);
            }
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateTipoSegmento();
                    loadGridTipoSegmento();
                    loadInfoTipoSegmento();
                    resetValuesTipoSegmento();
                    tbTiposSegmento.Visible = true;
                    loadCodigoTipoSegmento();
                    Mensaje("Tipo segmento actualizado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el tipo de segmento. " + ex.Message);
            }
        }

        protected void ImageButton10_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    addAtributo();
                    loadGridAtributo();
                    loadInfoAtributo();
                    resetValuesAtributo();
                    tbAtributos.Visible = true;
                    loadCodigoAtributo();
                    Mensaje("Atributo agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar el atributo. " + ex.Message);
            }
        }

        protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateAtributo();
                    loadGridAtributo();
                    loadInfoAtributo();
                    resetValuesAtributo();
                    tbAtributos.Visible = true;
                    loadCodigoAtributo();
                    Mensaje("Atributo actualizado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el atributo. " + ex.Message);
            }
        }

        protected void ImageButton13_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    addPerfilSegmento();
                    loadGridPerfilSegmento();
                    loadInfoPerfilSegmento();
                    resetValuesPerfilSegmento();
                    tbPerfilSegmento.Visible = true;
                    loadCodigoPerfilSegmento();
                    Mensaje("Perfil de segmento agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar el perfil de segmento. " + ex.Message);
            }
        }

        protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updatePerfilSegmento();
                    loadGridPerfilSegmento();
                    loadInfoPerfilSegmento();
                    resetValuesPerfilSegmento();
                    tbPerfilSegmento.Visible = true;
                    loadCodigoPerfilSegmento();
                    Mensaje("Perfil de segmento actualizado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el perfil de segmento. " + ex.Message);
            }
        }

        protected void ImageButton16_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    addIndicador();
                    loadGridIndicador();
                    loadInfoIndicador();
                    resetValuesIndicador();
                    tbIndicador.Visible = true;
                    loadCodigoIndicador();
                    Mensaje("Indicador agregado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al ingresar el indicador. " + ex.Message);
            }
        }

        protected void ImageButton17_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    updateIndicador();
                    loadGridIndicador();
                    loadInfoIndicador();
                    resetValuesIndicador();
                    tbIndicador.Visible = true;
                    loadCodigoIndicador();
                    Mensaje("Indicador actualizado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar el indicador. " + ex.Message);
            }
        }

        protected void ImageButton18_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesIndicador();
            loadGridIndicador();
            loadInfoIndicador();
            tbIndicador.Visible = true;
            loadCodigoIndicador();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            loadGridPerfilSegmento();
            loadInfoPerfilSegmento();
            tbPerfilSegmento.Visible = true;
            loadCodigoPerfilSegmento();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValuesFactorRiesgo();
            resetValuesSegmentos();
            resetValuesTipoSegmento();
            resetValuesAtributo();
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            loadGridFactorRiesgo();
            loadInfoFactorRiesgo();
            loadCodigoFactorRiesgo();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValuesSegmentos();
            resetValuesTipoSegmento();
            resetValuesAtributo();
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            loadGridSegmento();
            loadInfoSegmento();
            tbSegmentos.Visible = true;
            loadCodigoSegmento();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            resetValuesTipoSegmento();
            resetValuesAtributo();
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            loadGridTipoSegmento();
            loadInfoTipoSegmento();
            tbTiposSegmento.Visible = true;
            loadCodigoTipoSegmento();
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            resetValuesAtributo();
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            loadGridAtributo();
            loadInfoAtributo();
            tbAtributos.Visible = true;
            loadCodigoAtributo();
        }

        protected void btnAceptarOkNo_Click(object sender, EventArgs e)
        {
            switch (lbldummyOkNo.Text.Trim())
            {
                case "Eliminar Factor":
                    borrarFactorRiesgo();
                    break;
                case "Eliminar Segmento":
                    borrarSegmento();
                    break;
                case "Eliminar Tipo Segmento":
                    borrarTipoSegmento();
                    break;
                case "Eliminar Atributo":
                    borrarAtributo();
                    break;
                case "Eliminar Perfil Segmento":
                    borrarPerfilSegmento();
                    break;
                case "Eliminar Indicador":
                    borrarIndicador();
                    break;
            }
        }
        #endregion

        #region Gridviews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridFactorRiesgo = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    seleccionarFactorRiesgo();
                    break;
                case "Modificar":
                    modificarFactorRiesgo();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Eliminar Factor";
                    }
                    break;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridSegmento = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    seleccionarSegmento();
                    break;
                case "Modificar":
                    modificarSegmento();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Eliminar Segmento";
                    }
                    break;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridTipoSegmento = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    seleccionarTipoSegmento();
                    break;
                case "Modificar":
                    modificarTipoSegmento();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Eliminar Tipo Segmento";
                    }
                    break;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridAtributo = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    seleccionarAtributo();
                    break;
                case "Modificar":
                    modificarAtributo();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Eliminar Atributo";
                    }
                    break;
            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridPerfilSegmento = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    seleccionarPerfilSegmento();
                    break;
                case "Modificar":
                    modificarPerfilSegmento();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Eliminar Perfil Segmento";
                    }
                    break;
            }
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridIndicador = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    modificarIndicador();
                    break;
                case "Borrar":
                    if (cCuenta.permisosBorrar(IdFormulario) == "False")
                        Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                    else
                    {
                        lblMsgBoxOkNo.Text = "Desea eliminar la información de la Base de Datos?";
                        mpeMsgBoxOkNo.Show();
                        lbldummyOkNo.Text = "Eliminar Indicador";
                    }
                    break;
            }
        }
        #endregion

        #region DDLs
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList1.SelectedValue.ToString().Trim() != "---")
                loadDDListRango(1, DropDownList1.SelectedValue.ToString().Trim());
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList3.SelectedValue.ToString().Trim() != "---")
                loadDDListRango(2, DropDownList3.SelectedValue.ToString().Trim());
        }
        #endregion

        #region Actualizar
        private void updateFactorRiesgo()
        {
            cSegmentacion.updateFactorRiesgo(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["IdFactorRiesgo"].ToString().Trim());
        }

        private void updateSegmento()
        {
            cSegmentacion.updateSegmento(Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), InfoGridSegmento.Rows[RowGridSegmento]["IdSegmento"].ToString().Trim());
        }

        private void updateTipoSegmento()
        {
            cSegmentacion.updateTipoSegmento(Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox9.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["IdTipoSegmento"].ToString().Trim());
        }

        private void updateAtributo()
        {
            cSegmentacion.updateAtributo(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox12.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()), InfoGridAtributo.Rows[RowGridAtributo]["IdAtributo"].ToString().Trim());
        }

        private void updatePerfilSegmento()
        {
            cSegmentacion.updatePerfilSegmento(Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox15.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()), InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["IdPerfil"].ToString().Trim());
        }

        private void updateIndicador()
        {
            String ValorInferior1 = "0", ValorSuperior1 = "0", ValorInferior2 = "0", ValorSuperior2 = "0";
            if (DropDownList2.SelectedValue.ToString().Trim() != "---")
            {
                DataTable dtInformacion = new DataTable();
                dtInformacion = cAtributos.cargarValoresRangosAtributos(DropDownList2.SelectedValue.ToString().Trim());
                ValorInferior1 = dtInformacion.Rows[0]["ValorInferior"].ToString().Trim();
                ValorSuperior1 = dtInformacion.Rows[0]["ValorSuperior"].ToString().Trim();
            }

            if (DropDownList4.SelectedValue.ToString().Trim() != "---")
            {
                DataTable dtInformacion = new DataTable();
                dtInformacion = cAtributos.cargarValoresRangosAtributos(DropDownList4.SelectedValue.ToString().Trim());
                ValorInferior2 = dtInformacion.Rows[0]["ValorInferior"].ToString().Trim();
                ValorSuperior2 = dtInformacion.Rows[0]["ValorSuperior"].ToString().Trim();
            }
            cSegmentacion.updateIndicador(Sanitizer.GetSafeHtmlFragment(TextBox18.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox19.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox20.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox21.Text.Trim()), DropDownList1.SelectedItem.ToString().Trim(), DropDownList2.SelectedItem.ToString().Trim(), DropDownList3.SelectedItem.ToString().Trim(), DropDownList4.SelectedItem.ToString().Trim(), ValorInferior1, ValorSuperior1, ValorInferior2, ValorSuperior2, InfoGridIndicador.Rows[RowGridIndicador]["IdIndicador"].ToString().Trim());
        }
        #endregion

        #region Insertar
        private void addFactorRiesgo()
        {
            cSegmentacion.addFactorRiesgo(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()));
        }

        private void addSegmento()
        {
            cSegmentacion.addSegmento(InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["IdFactorRiesgo"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()));
        }

        private void addTipoSegmento()
        {
            cSegmentacion.addTipoSegmento(InfoGridSegmento.Rows[RowGridSegmento]["IdSegmento"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox9.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()));
        }

        private void addAtributo()
        {
            cSegmentacion.addAtributo(InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["IdTipoSegmento"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox12.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox13.Text.Trim()));
        }

        private void addPerfilSegmento()
        {
            cSegmentacion.addPerfilSegmento(InfoGridAtributo.Rows[RowGridAtributo]["IdAtributo"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox14.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox15.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox16.Text.Trim()));
        }

        private void addIndicador()
        {
            String ValorInferior1 = "0", ValorSuperior1 = "0", ValorInferior2 = "0", ValorSuperior2 = "0";
            if (DropDownList2.SelectedValue.ToString().Trim() != "---")
            {
                DataTable dtInformacion = new DataTable();
                dtInformacion = cAtributos.cargarValoresRangosAtributos(DropDownList2.SelectedValue.ToString().Trim());
                ValorInferior1 = dtInformacion.Rows[0]["ValorInferior"].ToString().Trim();
                ValorSuperior1 = dtInformacion.Rows[0]["ValorSuperior"].ToString().Trim();
            }
            if (DropDownList4.SelectedValue.ToString().Trim() != "---")
            {
                DataTable dtInformacion = new DataTable();
                dtInformacion = cAtributos.cargarValoresRangosAtributos(DropDownList4.SelectedValue.ToString().Trim());
                ValorInferior2 = dtInformacion.Rows[0]["ValorInferior"].ToString().Trim();
                ValorSuperior2 = dtInformacion.Rows[0]["ValorSuperior"].ToString().Trim();
            }
            cSegmentacion.addIndicador(InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["IdPerfil"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox18.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox19.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox20.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox21.Text.Trim()), DropDownList1.SelectedItem.ToString().Trim(), DropDownList2.SelectedItem.ToString().Trim(), DropDownList3.SelectedItem.ToString().Trim(), DropDownList4.SelectedItem.ToString().Trim(), ValorInferior1, ValorSuperior1, ValorInferior2, ValorSuperior2);
        }
        #endregion

        #region Loads
        private void loadCodigoIndicador()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cSegmentacion.loadCodigoIndicador();
                if (dtInfo.Rows.Count > 0)
                    TextBox18.Text = "I" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox18.Text = "I1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código indicador. " + ex.Message);
            }
        }

        private void loadCodigoPerfilSegmento()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cSegmentacion.loadCodigoPerfilSegmento();
                if (dtInfo.Rows.Count > 0)
                    TextBox14.Text = "PS" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox14.Text = "PS1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código del perfil de segmento. " + ex.Message);
            }
        }

        private void loadCodigoAtributo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cSegmentacion.loadCodigoAtributo();
                if (dtInfo.Rows.Count > 0)
                    TextBox11.Text = "A" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox11.Text = "A1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código del atributo. " + ex.Message);
            }
        }

        private void loadCodigoTipoSegmento()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cSegmentacion.loadCodigoTipoSegmento();
                if (dtInfo.Rows.Count > 0)
                    TextBox8.Text = "TS" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox8.Text = "TS1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código tipo segmento. " + ex.Message);
            }
        }

        private void loadCodigoSegmento()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cSegmentacion.loadCodigoSegmento();
                if (dtInfo.Rows.Count > 0)
                    TextBox5.Text = "S" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox5.Text = "S1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código segmento. " + ex.Message);
            }
        }

        private void loadCodigoFactorRiesgo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cSegmentacion.loadCodigoFactorRiesgo();
                if (dtInfo.Rows.Count > 0)
                    TextBox1.Text = "FR" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                else
                    TextBox1.Text = "FR1";
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código factor de riesgo. " + ex.Message);
            }
        }

        private void loadGridFactorRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdFactorRiesgo", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Indicador", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridFactorRiesgo = grid;
            GridView1.DataSource = InfoGridFactorRiesgo;
            GridView1.DataBind();
        }

        private void loadGridSegmento()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdSegmento", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridSegmento = grid;
            GridView2.DataSource = InfoGridSegmento;
            GridView2.DataBind();
        }

        private void loadGridTipoSegmento()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdTipoSegmento", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridTipoSegmento = grid;
            GridView3.DataSource = InfoGridTipoSegmento;
            GridView3.DataBind();
        }

        private void loadGridAtributo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdAtributo", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridAtributo = grid;
            GridView4.DataSource = InfoGridAtributo;
            GridView4.DataBind();
        }

        private void loadGridPerfilSegmento()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPerfil", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            InfoGridPerfilSegmento = grid;
            GridView5.DataSource = InfoGridPerfilSegmento;
            GridView5.DataBind();
        }

        private void loadGridIndicador()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdIndicador", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Indicador", typeof(string));
            grid.Columns.Add("MensajeSenalAlerta", typeof(string));
            grid.Columns.Add("NombreAtributo1", typeof(string));
            grid.Columns.Add("NombreRango1", typeof(string));
            grid.Columns.Add("NombreAtributo2", typeof(string));
            grid.Columns.Add("NombreRango2", typeof(string));
            InfoGridIndicador = grid;
            GridView6.DataSource = InfoGridIndicador;
            GridView6.DataBind();
        }

        private void loadInfoFactorRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoFactorRiesgo();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridFactorRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdFactorRiesgo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Indicador"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                                });
                }
                GridView1.DataSource = InfoGridFactorRiesgo;
                GridView1.DataBind();
            }
        }

        private void loadInfoSegmento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoSegmento(InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["IdFactorRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridSegmento.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdSegmento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Nombre"].ToString().Trim(),                                                            
                                                            dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                            });
                }
                GridView2.DataSource = InfoGridSegmento;
                GridView2.DataBind();
            }
        }

        private void loadInfoTipoSegmento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoTipoSegmento(InfoGridSegmento.Rows[RowGridSegmento]["IdSegmento"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridTipoSegmento.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdTipoSegmento"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                dtInfo.Rows[rows]["Nombre"].ToString().Trim(),                                                            
                                                                dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                               });
                }
                GridView3.DataSource = InfoGridTipoSegmento;
                GridView3.DataBind();
            }
        }

        private void loadInfoAtributo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoAtributo(InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["IdTipoSegmento"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAtributo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdAtributo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Nombre"].ToString().Trim(),                                                            
                                                            dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                           });
                }
                GridView4.DataSource = InfoGridAtributo;
                GridView4.DataBind();
            }
        }

        private void loadInfoPerfilSegmento()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoPerfilSegmento(InfoGridAtributo.Rows[RowGridAtributo]["IdAtributo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPerfilSegmento.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPerfil"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["Nombre"].ToString().Trim(),                                                            
                                                                  dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                                 });
                }
                GridView5.DataSource = InfoGridPerfilSegmento;
                GridView5.DataBind();
            }
        }

        private void loadInfoIndicador()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cSegmentacion.loadInfoIndicador(InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["IdPerfil"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridIndicador.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdIndicador"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["Nombre"].ToString().Trim(),                                                            
                                                             dtInfo.Rows[rows]["Indicador"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["MensajeSenalAlerta"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreAtributo1"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreRango1"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreAtributo2"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreRango2"].ToString().Trim()                                                                 
                                                            });
                }
                GridView6.DataSource = InfoGridIndicador;
                GridView6.DataBind();
            }
        }

        private void loadDDListAtributo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cAtributos.cargarAtributos();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreAtributo"].ToString().Trim(), dtInfo.Rows[i]["IdAtributos"].ToString()));
                    DropDownList3.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreAtributo"].ToString().Trim(), dtInfo.Rows[i]["IdAtributos"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los atributos. " + ex.Message);
            }
        }

        private void loadDDListRango(int tipoDDLRango, String IdAtributos)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cAtributos.cargarRangosAtributo(IdAtributos);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    switch (tipoDDLRango)
                    {
                        case 1:
                            DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRango"].ToString().Trim(), dtInfo.Rows[i]["IdRangosAtributo"].ToString()));
                            break;
                        case 2:
                            DropDownList4.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRango"].ToString().Trim(), dtInfo.Rows[i]["IdRangosAtributo"].ToString()));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los rangos de atributos. " + ex.Message);
            }
        }
        #endregion

        #region Borrar
        private void borrarFactorRiesgo()
        {
            try
            {
                cSegmentacion.borrarFactorRiesgo(InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["IdFactorRiesgo"].ToString().Trim());
                resetValuesFactorRiesgo();
                tbFactorRiesgo.Visible = true;
                loadGridFactorRiesgo();
                loadInfoFactorRiesgo();
                loadCodigoFactorRiesgo();
                Mensaje("Factor de riesgo borrado con éxito.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el factor de riesgo. " + ex.Message);
            }
        }

        private void borrarSegmento()
        {
            try
            {
                cSegmentacion.borrarSegmento(InfoGridSegmento.Rows[RowGridSegmento]["IdSegmento"].ToString().Trim());
                resetValuesSegmentos();
                tbSegmentos.Visible = true;
                loadGridSegmento();
                loadInfoSegmento();
                loadCodigoSegmento();
                Mensaje("Segmento borrado con éxito.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el segmento. " + ex.Message);
            }
        }

        private void borrarTipoSegmento()
        {
            try
            {
                cSegmentacion.borrarTipoSegmento(InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["IdTipoSegmento"].ToString().Trim());
                resetValuesTipoSegmento();
                tbTiposSegmento.Visible = true;
                loadGridTipoSegmento();
                loadInfoTipoSegmento();
                loadCodigoTipoSegmento();
                Mensaje("Tipo segmento borrado con éxito");
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el tipo segmento. " + ex.Message);
            }
        }

        private void borrarAtributo()
        {
            try
            {
                cSegmentacion.borrarAtributo(InfoGridAtributo.Rows[RowGridAtributo]["IdAtributo"].ToString().Trim());
                resetValuesAtributo();
                tbAtributos.Visible = true;
                loadGridAtributo();
                loadInfoAtributo();
                loadCodigoAtributo();
                Mensaje("Atributo borrado con éxito");
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el atributo. " + ex.Message);
            }
        }

        private void borrarPerfilSegmento()
        {
            try
            {
                cSegmentacion.borrarPerfilSegmento(InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["IdPerfil"].ToString().Trim());
                resetValuesPerfilSegmento();
                tbPerfilSegmento.Visible = true;
                loadGridPerfilSegmento();
                loadInfoPerfilSegmento();
                loadCodigoPerfilSegmento();
                Mensaje("Perfil de segmento borrado con éxito.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el perfil de segmento. " + ex.Message);
            }
        }

        private void borrarIndicador()
        {
            try
            {
                cSegmentacion.borrarIndicador(InfoGridIndicador.Rows[RowGridIndicador]["IdIndicador"].ToString().Trim());
                resetValuesIndicador();
                tbIndicador.Visible = true;
                loadGridIndicador();
                loadInfoIndicador();
                loadCodigoIndicador();
                Mensaje("Indicador borrado con éxito.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar el indicador. " + ex.Message);
            }
        }
        #endregion

        #region Modificar
        private void modificarFactorRiesgo()
        {
            resetValuesFactorRiesgo();
            ImageButton1.Visible = false;
            ImageButton2.Visible = true;
            ImageButton3.Visible = true;
            TextBox1.Text = InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["Codigo"].ToString().Trim();
            TextBox2.Text = InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["Nombre"].ToString().Trim();
            TextBox3.Text = InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["Indicador"].ToString().Trim();
            TextBox4.Text = InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["Descripcion"].ToString().Trim();
            tbFactorRiesgo.Visible = true;
        }

        private void modificarSegmento()
        {
            resetValuesSegmentos();
            ImageButton4.Visible = false;
            ImageButton5.Visible = true;
            ImageButton6.Visible = true;
            TextBox5.Text = InfoGridSegmento.Rows[RowGridSegmento]["Codigo"].ToString().Trim();
            TextBox6.Text = InfoGridSegmento.Rows[RowGridSegmento]["Nombre"].ToString().Trim();
            TextBox7.Text = InfoGridSegmento.Rows[RowGridSegmento]["Descripcion"].ToString().Trim();
            tbSegmentos.Visible = true;
        }

        private void modificarTipoSegmento()
        {
            resetValuesTipoSegmento();
            ImageButton7.Visible = false;
            ImageButton8.Visible = true;
            ImageButton9.Visible = true;
            TextBox8.Text = InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["Codigo"].ToString().Trim();
            TextBox9.Text = InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["Nombre"].ToString().Trim();
            TextBox10.Text = InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["Descripcion"].ToString().Trim();
            tbTiposSegmento.Visible = true;
        }

        private void modificarAtributo()
        {
            resetValuesAtributo();
            ImageButton10.Visible = false;
            ImageButton11.Visible = true;
            ImageButton12.Visible = true;
            TextBox11.Text = InfoGridAtributo.Rows[RowGridAtributo]["Codigo"].ToString().Trim();
            TextBox12.Text = InfoGridAtributo.Rows[RowGridAtributo]["Nombre"].ToString().Trim();
            TextBox13.Text = InfoGridAtributo.Rows[RowGridAtributo]["Descripcion"].ToString().Trim();
            tbAtributos.Visible = true;
        }

        private void modificarPerfilSegmento()
        {
            resetValuesPerfilSegmento();
            ImageButton13.Visible = false;
            ImageButton14.Visible = true;
            ImageButton15.Visible = true;
            TextBox14.Text = InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["Codigo"].ToString().Trim();
            TextBox15.Text = InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["Nombre"].ToString().Trim();
            TextBox16.Text = InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["Descripcion"].ToString().Trim();
            tbPerfilSegmento.Visible = true;
        }

        private void modificarIndicador()
        {
            resetValuesIndicador();
            ImageButton16.Visible = false;
            ImageButton17.Visible = true;
            ImageButton18.Visible = true;
            TextBox18.Text = InfoGridIndicador.Rows[RowGridIndicador]["Codigo"].ToString().Trim();
            TextBox19.Text = InfoGridIndicador.Rows[RowGridIndicador]["Nombre"].ToString().Trim();
            TextBox20.Text = InfoGridIndicador.Rows[RowGridIndicador]["Indicador"].ToString().Trim();
            TextBox21.Text = InfoGridIndicador.Rows[RowGridIndicador]["MensajeSenalAlerta"].ToString().Trim();
            tbIndicador.Visible = true;
        }
        #endregion

        #region Seleccionar
        private void seleccionarFactorRiesgo()
        {
            resetValuesSegmentos();
            resetValuesTipoSegmento();
            resetValuesAtributo();
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            Label9.Text = InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["Codigo"].ToString().Trim();
            Label11.Text = InfoGridFactorRiesgo.Rows[RowGridFactorRiesgo]["Nombre"].ToString().Trim();
            tbFactorRiesgo.Visible = false;
            trFactorRiesgo.Visible = true;
            tbSegmentos.Visible = true;
            trSegmentos.Visible = false;
            tbTiposSegmento.Visible = false;
            trTiposSegmento.Visible = false;
            tbAtributos.Visible = false;
            trAtributos.Visible = false;
            tbPerfilSegmento.Visible = false;
            trPerfilSegmento.Visible = false;
            tbIndicador.Visible = false;
            loadGridSegmento();
            loadInfoSegmento();
            loadCodigoSegmento();
        }

        private void seleccionarSegmento()
        {
            resetValuesTipoSegmento();
            resetValuesAtributo();
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            Label19.Text = InfoGridSegmento.Rows[RowGridSegmento]["Codigo"].ToString().Trim();
            Label21.Text = InfoGridSegmento.Rows[RowGridSegmento]["Nombre"].ToString().Trim();
            tbSegmentos.Visible = false;
            trSegmentos.Visible = true;
            tbTiposSegmento.Visible = true;
            trTiposSegmento.Visible = false;
            tbAtributos.Visible = false;
            trAtributos.Visible = false;
            tbPerfilSegmento.Visible = false;
            trPerfilSegmento.Visible = false;
            tbIndicador.Visible = false;
            loadGridTipoSegmento();
            loadInfoTipoSegmento();
            loadCodigoTipoSegmento();
        }

        private void seleccionarTipoSegmento()
        {
            resetValuesAtributo();
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            Label29.Text = InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["Codigo"].ToString().Trim();
            Label31.Text = InfoGridTipoSegmento.Rows[RowGridTipoSegmento]["Nombre"].ToString().Trim();
            tbTiposSegmento.Visible = false;
            trTiposSegmento.Visible = true;
            tbAtributos.Visible = true;
            trAtributos.Visible = false;
            tbPerfilSegmento.Visible = false;
            trPerfilSegmento.Visible = false;
            tbIndicador.Visible = false;
            loadGridAtributo();
            loadInfoAtributo();
            loadCodigoAtributo();
        }

        private void seleccionarAtributo()
        {
            resetValuesPerfilSegmento();
            resetValuesIndicador();
            Label39.Text = InfoGridAtributo.Rows[RowGridAtributo]["Codigo"].ToString().Trim();
            Label41.Text = InfoGridAtributo.Rows[RowGridAtributo]["Nombre"].ToString().Trim();
            tbAtributos.Visible = false;
            trAtributos.Visible = true;
            tbPerfilSegmento.Visible = true;
            trPerfilSegmento.Visible = false;
            tbIndicador.Visible = false;
            loadGridPerfilSegmento();
            loadInfoPerfilSegmento();
            loadCodigoPerfilSegmento();
        }

        private void seleccionarPerfilSegmento()
        {
            resetValuesIndicador();
            Label49.Text = InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["Codigo"].ToString().Trim();
            Label51.Text = InfoGridPerfilSegmento.Rows[RowGridPerfilSegmento]["Nombre"].ToString().Trim();
            tbPerfilSegmento.Visible = false;
            trPerfilSegmento.Visible = true;
            tbIndicador.Visible = true;
            loadGridIndicador();
            loadInfoIndicador();
            loadCodigoIndicador();
        }
        #endregion

        #region Resets
        private void resetValuesFactorRiesgo()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            Label9.Text = "";
            Label11.Text = "";
            tbFactorRiesgo.Visible = true;
            trFactorRiesgo.Visible = false;
            ImageButton1.Visible = true;
            ImageButton2.Visible = false;
            ImageButton3.Visible = false;
        }

        private void resetValuesSegmentos()
        {
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            Label19.Text = "";
            Label21.Text = "";
            tbSegmentos.Visible = false;
            trSegmentos.Visible = false;
            ImageButton4.Visible = true;
            ImageButton5.Visible = false;
            ImageButton6.Visible = false;
        }

        private void resetValuesTipoSegmento()
        {
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            Label29.Text = "";
            Label31.Text = "";
            tbTiposSegmento.Visible = false;
            trTiposSegmento.Visible = false;
            ImageButton7.Visible = true;
            ImageButton8.Visible = false;
            ImageButton9.Visible = false;
        }

        private void resetValuesAtributo()
        {
            TextBox11.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            Label39.Text = "";
            Label41.Text = "";
            tbAtributos.Visible = false;
            trAtributos.Visible = false;
            ImageButton10.Visible = true;
            ImageButton11.Visible = false;
            ImageButton12.Visible = false;
        }

        private void resetValuesPerfilSegmento()
        {
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox16.Text = "";
            Label49.Text = "";
            Label51.Text = "";
            tbPerfilSegmento.Visible = false;
            trPerfilSegmento.Visible = false;
            ImageButton13.Visible = true;
            ImageButton14.Visible = false;
            ImageButton15.Visible = false;
        }

        private void resetValuesIndicador()
        {
            TextBox18.Text = "";
            TextBox19.Text = "";
            TextBox20.Text = "";
            TextBox21.Text = "";
            DropDownList1.SelectedIndex = 0;
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
            DropDownList3.SelectedIndex = 0;
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            tbIndicador.Visible = false;
            ImageButton16.Visible = true;
            ImageButton17.Visible = false;
            ImageButton18.Visible = false;
        }
        #endregion

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
    }
}