using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class Indicadores : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "7008";
        private cGestion cGestion = new cGestion();

        #region Propierties

        private DataTable infGrid;
        private DataTable InfoGrid
        {
            get
            {
                infGrid = (DataTable)ViewState["infGrid"];
                return infGrid;
            }
            set
            {
                infGrid = value;
                ViewState["infGrid"] = infGrid;
            }
        }

        private DataTable infGridVariables;
        private DataTable InfoGridVariables
        {
            get
            {
                infGridVariables = (DataTable)ViewState["infGridVariables"];
                return infGridVariables;
            }
            set
            {
                infGridVariables = value;
                ViewState["infGridVariables"] = infGridVariables;
            }
        }

        private DataTable infGridMetas;
        private DataTable InfoGridMetas
        {
            get
            {
                infGridMetas = (DataTable)ViewState["infGridMetas"];
                return infGridMetas;
            }
            set
            {
                infGridMetas = value;
                ViewState["infGridMetas"] = infGridMetas;
            }
        }

        private int idexRowM;
        private int IdexRowM
        {
            get
            {
                idexRowM = (int)ViewState["idexRowM"];
                return idexRowM;
            }
            set
            {
                idexRowM = value;
                ViewState["idexRowM"] = idexRowM;
            }
        }

        private int idexRow;
        private int IdexRow
        {
            get
            {
                idexRow = (int)ViewState["idexRow"];
                return idexRow;
            }
            set
            {
                idexRow = value;
                ViewState["idexRow"] = idexRow;
            }
        }
        private int idexRowV;
        private int IdexRowV
        {
            get
            {
                idexRowV = (int)ViewState["idexRowV"];
                return idexRowV;
            }
            set
            {
                idexRowV = value;
                ViewState["idexRowV"] = idexRowV;
            }
        }
        private int idexRowVS;
        private int IdexRowVS
        {
            get
            {
                idexRowVS = (int)ViewState["idexRowVS"];
                return idexRowVS;
            }
            set
            {
                idexRowVS = value;
                ViewState["idexRowVS"] = idexRowVS;
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

        private int pagIndexM;
        private int PagIndexM
        {
            get
            {
                pagIndexM = (int)ViewState["pagIndexM"];
                return pagIndexM;
            }
            set
            {
                pagIndexM = value;
                ViewState["pagIndexM"] = pagIndexM;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!IsPostBack)
            {
                loadGrid();
                cargarInfoGrid();
                inicializarValores();
                loadDDLRolAdd();
                loadDDLFormatoAdd();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataSource = InfoGrid;
            GridView1.DataBind();
        }

        protected void GridViewMetas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexM = e.NewPageIndex;
            GridViewMetas.PageIndex = PagIndexM;
            GridViewMetas.DataSource = InfoGridMetas;
            GridViewMetas.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = (Convert.ToInt16(GridView1.PageSize) * Convert.ToInt16(GridView1.PageIndex)) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ModificarIndicador":
                    verModificar();
                    modificarIndicador();
                    break;
                case "EliminarIndicador":
                    EliminarIndicador();
                    break;
            }
        }

        protected void GridViewMetas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowM = (Convert.ToInt16(GridViewMetas.PageSize) * Convert.ToInt16(GridViewMetas.PageIndex)) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ModificarMeta":
                    verModificarMeta();

                    break;
                case "EliminarMeta":
                    EliminarMeta();
                    break;
            }
        }

        protected void GridViewVariables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowV = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "EliminarVariable":
                    EliminarVariable();
                    break;
                case "ModificarVariable":
                    ModificarVariable();
                    break;
            }
        }

        protected void GridViewSeleccVariables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRowVS = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecVariable":
                    SelecVariable();
                    break;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void GridViewSeleccVariables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValues();
        }

        protected void BtnGuardaVision_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                agregarLista();
                loadGrid();
                cargarInfoGrid();
                resetValues();
                Mensaje("Visión de la Empresa almacenada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al guardar la visión de la empresa. " + ex.Message);
            }
        }

        protected void BtnCancelaVision_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
        }

        protected void BtnModificaVision_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                modificarLista();
                loadGrid();
                cargarInfoGrid();
                resetValues();
                Mensaje("Visión de la Empresa modificada correctamente.");
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar la visión de la empresa. " + ex.Message);
            }
        }

        protected void BtnCancelaMod_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void BtnAdIndicador_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                TbIndicadorSeleccionado.Visible = false;
                resetValues();
                resetValues1();
                TbIndicadores.Visible = true;
                TbCrearVariables.Visible = false;
                BtnIndicador.Visible = true;
                TbBotones.Visible = true;
                loadCodigo();
                TabContainerIndicadores.ActiveTabIndex = 0;
                loadGridMetas();
                cargarInfoGridMetas();
            }
        }

        protected void BtnAdicionaVariable_Click(object sender, ImageClickEventArgs e)
        {
            TbCrearVariables.Visible = false;
            TbAddVaiables.Visible = true;
        }

        protected void BtnGuardarIndicador_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    agregarIndicadorTotal();
                    TbIndicadores.Visible = false;
                    TbBotones.Visible = false;
                    TbMostrarIndicadores.Visible = true;
                    TbIndicadorSeleccionado.Visible = false;
                    TbSeleccionarVariables.Visible = false;
                    Mensaje("Indicador almacenado correctamente.");
                    loadGrid();
                    cargarInfoGrid();
                    resetValues();
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar Indicador." + ex.Message);
            }
        }

        protected void BtnCancelaIndicador_Click(object sender, ImageClickEventArgs e)
        {
            TbIndicadores.Visible = false;
            TbMostrarIndicadores.Visible = true;
            loadGrid();
            cargarInfoGrid();
            resetValues();
            TbBotones.Visible = false;
            TbIndicadorSeleccionado.Visible = false;
            TbSeleccionarVariables.Visible = false;
        }

        protected void BtnIndicador_Click(object sender, EventArgs e)
        {
            try
            {
                agregarIndicadorParcial();
                Label9.Text = Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + "=";
                Mensaje("Indicador almacenado correctamente.");
                TbCrearVariables.Visible = true;
                TbSeleccionarVariables.Visible = true;
                BtnIndicador.Visible = false;
                loadGridVariables();
                cargarInfoGridVariables();
                loadCodigo_After();
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar Indicador." + ex.Message);
            }
        }

        protected void BtnGuardarVariable_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                cGestion.agregarVariables(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), DropDownListFormato.SelectedItem.Value.ToString().Trim());
                Mensaje("Variable almacenada correctamente.");
                TbAddVaiables.Visible = false;
                resetValues1();
                TbCrearVariables.Visible = true;
                loadGridVariables();
                cargarInfoGridVariables();
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar Variable." + ex.Message);
            }
        }

        protected void BtnCancelaVariable_Click(object sender, ImageClickEventArgs e)
        {
            TbAddVaiables.Visible = false;
            TbCrearVariables.Visible = true;
            resetValues1();
        }

        protected void BtnNominador_Click(object sender, EventArgs e)
        {
            Label26.Text = "Nominador";
            ImageNominadr.Visible = true;
            BtnDenominador.Visible = true;
            BtnNominador.Visible = false;
            ImageDenominador.Visible = false;
            TbIndicadorSeleccionado.Visible = false;
        }

        protected void BtnDenominador_Click(object sender, EventArgs e)
        {
            Label26.Text = "Denominador";
            ImageDenominador.Visible = true;
            BtnNominador.Visible = true;
            ImageNominadr.Visible = false;
            BtnDenominador.Visible = false;
        }

        protected void ButtonMas_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + HttpUtility.HtmlDecode(" + ");
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + HttpUtility.HtmlDecode(" + ");
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + HttpUtility.HtmlDecode(" + ");
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + HttpUtility.HtmlDecode(" + ");
            }
        }

        protected void ButtonPor_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " * ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " * ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " * ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " * ";
            }
        }

        protected void ButtonAbreP_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " ( ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " ( ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " ( ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " ( ";
            }
        }

        protected void ButtonCierraP_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " ) ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " ) ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " ) ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " ) ";
            }
        }

        protected void ButtonDel_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = "";
                TextBox7.Text = "";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = "";
                TextBox8.Text = "";
            }
        }

        protected void ButtonMenos_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " - ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " - ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " - ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " - ";
            }
        }

        protected void ButtonDivide_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " / ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " / ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " / ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " / ";
            }
        }

        protected void ButtonPorc_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " % ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " % ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " % ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " % ";
            }
        }

        protected void ButtonCero_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " 0 ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " 0 ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " 0 ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " 0 ";
            }
        }

        protected void ButtonCien_Click(object sender, EventArgs e)
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + " 100 ";
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + " 100 ";
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + " 100 ";
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + " 100 ";
            }
        }

        protected void BtnCancelaAUpVariable_Click(object sender, ImageClickEventArgs e)
        {
            TbModificaVariable.Visible = false;
            TbCrearVariables.Visible = true;
        }

        protected void BtnModificaVariable_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                cGestion.ModificarVariables(Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedItem.Value.ToString().Trim(), Label17.Text.Trim());
                Mensaje("Variable modificada correctamente.");
                TbModificaVariable.Visible = false;
                TbCrearVariables.Visible = true;
                loadGridVariables();
                cargarInfoGridVariables();
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar Variable." + ex.Message);
            }
        }

        protected void BtnAdicionaMeta_Click(object sender, ImageClickEventArgs e)
        {
            TbMetas.Visible = false;
            TbAddMetas.Visible = true;
        }

        protected void BtnAdiciona_Click(object sender, ImageClickEventArgs e)
        {
            OcultarPeridicidad();
            TbMetas.Visible = false;
            TbAddMetas.Visible = true;
            BtnGuardarMeta.Visible = true;
            BtnUpdaterMeta.Visible = false;
            VerPeriodicidad();
        }

        protected void BtnCancelaMeta_Click(object sender, ImageClickEventArgs e)
        {
            OcultarPeridicidad();
            TbAddMetas.Visible = false;
            TbMetas.Visible = true;
            BtnGuardarMeta.Visible = false;
            BtnUpdaterMeta.Visible = false;
            TextBox10.Text = "";
        }

        protected void BtnUpdaterMeta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (DropDownListPeriodicidad.SelectedItem.Value == "14")//diaria
                {
                    cGestion.ModifficarMetaDia(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox12.Text.Trim()));
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "15")//Semanal
                {
                    cGestion.ModifficarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text, Sanitizer.GetSafeHtmlFragment(TextBox10.Text), DropDownListSemana.SelectedItem.Value.Trim(), DropDownListMes.SelectedItem.Value.ToString().Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "16")//Quincenal
                {
                    cGestion.ModifficarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text, Sanitizer.GetSafeHtmlFragment(TextBox10.Text), DropDownListQuincena.SelectedItem.Value.Trim(), DropDownListMes.SelectedItem.Value.ToString().Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "18")//Mensual
                {
                    cGestion.ModifficarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text, Sanitizer.GetSafeHtmlFragment(TextBox10.Text), "1", DropDownListMes.SelectedItem.Value.ToString().Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "19")//Bimestral
                {
                    cGestion.ModifficarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text, Sanitizer.GetSafeHtmlFragment(TextBox10.Text), "1", DropDownListBimestre.SelectedItem.Value.ToString().Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "20")//Trimestral
                {
                    cGestion.ModifficarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text, Sanitizer.GetSafeHtmlFragment(TextBox10.Text), "1", DropDownListTrimestre.SelectedItem.Value.ToString().Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "21")//Semestral
                {
                    cGestion.ModifficarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text, Sanitizer.GetSafeHtmlFragment(TextBox10.Text), "1", DropDownListSemestre.SelectedItem.Value.ToString().Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "22")//Anual
                {
                    cGestion.ModifficarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim(), Label6.Text, Sanitizer.GetSafeHtmlFragment(TextBox10.Text), "1", "1", DropDownListAno.SelectedItem.Value.Trim());
                }
                loadGridMetas();
                cargarInfoGridMetas();
                TbAddMetas.Visible = false;
                TbMetas.Visible = true;
                Mensaje("Meta modificada correctamente para el periodo seleccionado");
            }
            catch (Exception)
            {
                Mensaje2("Ya existe meta para el periodo seleccionado");
            }
        }

        protected void BtnGuardarMeta_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (DropDownListPeriodicidad.SelectedItem.Value == "14")//diaria
                {
                    cGestion.InsertarMetaDia(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox12.Text.Trim()));
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "15")//Semanal
                {
                    cGestion.InsertarMeta(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), DropDownListSemana.SelectedItem.Value.Trim(), DropDownListMes.SelectedItem.Value.Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "16")//Quincenal
                {
                    cGestion.InsertarMeta(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), DropDownListQuincena.SelectedItem.Value.Trim(), DropDownListMes.SelectedItem.Value.Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "18")//Mensual
                {
                    cGestion.InsertarMeta(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), "1", DropDownListMes.SelectedItem.Value.Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "19")//Bimestral
                {
                    cGestion.InsertarMeta(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), "1", DropDownListBimestre.SelectedItem.Value.Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "20")//Trimestral
                {
                    cGestion.InsertarMeta(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), "1", DropDownListTrimestre.SelectedItem.Value.Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "21")//Semestral
                {
                    cGestion.InsertarMeta(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), "1", DropDownListSemestre.SelectedItem.Value.Trim(), DropDownListAno.SelectedItem.Value.Trim());
                }
                else if (DropDownListPeriodicidad.SelectedItem.Value == "22")//Anual
                {
                    cGestion.InsertarMeta(Label6.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()), "1", "1", DropDownListAno.SelectedItem.Value.Trim());
                }
                loadGridMetas();
                cargarInfoGridMetas();
                TbAddMetas.Visible = false;
                TbMetas.Visible = true;
                Mensaje("Meta guardada correctamente para el periodo seleccionado");
            }
            catch (Exception)
            {
                Mensaje2("Ya existe meta para el periodo seleccionado");
            }
        }

        protected void DropDownListPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarPeridicidad();
        }

        protected void DropDownListFormato_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CodigoCon()
        {
            Label6.Text = cGestion.CodigoConsecutivo("IdIndicador", "Gestion.Indicadores");
            if (Label6.Text == "")
            {
                TextBox3.Text = "IND1" + cGestion.CodigoConsecutivo("IdIndicador", "Gestion.Indicadores");
                Label6.Text = "1";
            }
            else
            {
                TextBox3.Text = "IND" + cGestion.CodigoConsecutivo("IdIndicador", "Gestion.Indicadores");
            }
        }

        private void agregarIndicadorParcial()
        {
            cGestion.agregarIndicadorParcial(Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), DropDownListPeriodicidad.SelectedItem.Value.ToString().Trim());
        }

        private void agregarIndicadorTotal()
        {
            cGestion.agregarIndicadorTotal(HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim())), DropDownListPeriodicidad.SelectedItem.Value.ToString().Trim(), HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim())), HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim())), Label6.Text.Trim(), DropDownListActivo.SelectedItem.Value.ToString().Trim());
        }

        private void loadDDLRolAdd()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LPeriodicidad();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownListPeriodicidad.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Erro al cargar Perspectivas. " + ex.Message);
            }
        }

        private void loadDDLFormatoAdd()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestion.LFormatos();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownListFormato.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreDetalle"].ToString().Trim(), dtInfo.Rows[i]["IdDetalleTipo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Erro al cargar Formatos. " + ex.Message);
            }
        }

        private void loadCodigo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo("IdIndicador", "Gestion.Indicadores");
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox3.Text = "IN" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                    Label6.Text = dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox3.Text = "IN1";
                    Label6.Text = "1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código control. " + ex.Message);
            }
        }

        private void loadCodigo_After()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cGestion.loadCodigo_After("IdIndicador", "Gestion.Indicadores");
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox3.Text = "IN" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                    Label6.Text = dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox3.Text = "IN1";
                    Label6.Text = "1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código control. " + ex.Message);
            }
        }

        private void loadGridVariables()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdIndicador", typeof(string));
            grid.Columns.Add("IdVariable", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Formato", typeof(string));
            GridViewVariables.DataSource = grid;
            GridViewSeleccVariables.DataSource = grid;
            GridViewVariables.DataBind();
            GridViewSeleccVariables.DataBind();
            InfoGridVariables = grid;
        }

        private void cargarInfoGridVariables()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.Variables(Label6.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridVariables.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["IdVariable"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Formato"].ToString().Trim(),
                                                    });
                }
                GridViewVariables.DataSource = InfoGridVariables;
                GridViewVariables.DataBind();
                GridViewSeleccVariables.DataSource = InfoGridVariables;
                GridViewSeleccVariables.DataBind();
            }
        }

        private void loadGridMetas()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdIndicador", typeof(string));
            grid.Columns.Add("IdMetaValor", typeof(string));
            grid.Columns.Add("Dia", typeof(string));
            grid.Columns.Add("Mes", typeof(string));
            grid.Columns.Add("Ano", typeof(string));
            grid.Columns.Add("Periodo", typeof(string));
            grid.Columns.Add("Meta", typeof(string));
            GridViewMetas.DataSource = grid;
            GridViewMetas.DataBind();
            InfoGridMetas = grid;
        }

        private void cargarInfoGridMetas()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cGestion.Metas(Label6.Text.Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridMetas.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["IdMetaValor"].ToString().Trim(),                            
                                                    dtInfo.Rows[rows]["Dia"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Mes"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Ano"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Periodo"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Meta"].ToString().Trim(),
                                                    });
                }
                GridViewMetas.DataSource = InfoGridMetas;
                GridViewMetas.DataBind();
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdIndicador", typeof(string));
            grid.Columns.Add("CodigoIndicador", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Periodicidad", typeof(string));
            grid.Columns.Add("Meta", typeof(string));
            grid.Columns.Add("Nominador", typeof(string));
            grid.Columns.Add("Denominador", typeof(string));
            grid.Columns.Add("Activo_SN", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cGestion.Indicadores();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CodigoIndicador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Periodicidad"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Meta"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Nominador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Denominador"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Activo_SN"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                    });
                }
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
        }

        private void verModificarMeta()
        {
            OcultarPeridicidad();
            TbMetas.Visible = false;
            TbAddMetas.Visible = true;
            BtnGuardarMeta.Visible = false;
            BtnUpdaterMeta.Visible = true;
            VerPeriodicidad();
            if (DropDownListPeriodicidad.SelectedItem.Value == "14")//diaria
            {
                TextBox12.Text = InfoGridMetas.Rows[IdexRowM]["Periodo"].ToString().Trim();
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "15")//Semanal
            {
                for (int i = 0; i < DropDownListSemana.Items.Count; i++)
                {
                    DropDownListSemana.SelectedIndex = i;
                    if (DropDownListSemana.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Dia"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListSemana.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListMes.Items.Count; i++)
                {
                    DropDownListMes.SelectedIndex = i;
                    if (DropDownListMes.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListMes.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "16")//Quincenal
            {
                for (int i = 0; i < DropDownListQuincena.Items.Count; i++)
                {
                    DropDownListQuincena.SelectedIndex = i;
                    if (DropDownListQuincena.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Dia"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListQuincena.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListMes.Items.Count; i++)
                {
                    DropDownListMes.SelectedIndex = i;
                    if (DropDownListMes.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListMes.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "18")//Mensual
            {
                for (int i = 0; i < DropDownListMes.Items.Count; i++)
                {
                    DropDownListMes.SelectedIndex = i;
                    if (DropDownListMes.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListMes.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "19")//Bimestral
            {
                for (int i = 0; i < DropDownListBimestre.Items.Count; i++)
                {
                    DropDownListBimestre.SelectedIndex = i;
                    if (DropDownListBimestre.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListBimestre.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "20")//Trimestral
            {
                for (int i = 0; i < DropDownListTrimestre.Items.Count; i++)
                {
                    DropDownListTrimestre.SelectedIndex = i;
                    if (DropDownListTrimestre.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListTrimestre.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "21")//Semestral
            {
                for (int i = 0; i < DropDownListSemestre.Items.Count; i++)
                {
                    DropDownListSemestre.SelectedIndex = i;
                    if (DropDownListSemestre.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Mes"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListSemestre.SelectedIndex = (0);
                }
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "22")//Anual
            {
                for (int i = 0; i < DropDownListAno.Items.Count; i++)
                {
                    DropDownListAno.SelectedIndex = i;
                    if (DropDownListAno.SelectedItem.Value == InfoGridMetas.Rows[IdexRowM]["Ano"].ToString().Trim())
                    {
                        break;
                    }
                    DropDownListAno.SelectedIndex = (0);
                }
            }


            TextBox10.Text = InfoGridMetas.Rows[IdexRowM]["Meta"].ToString().Trim();
        }

        private void EliminarMeta()
        {
            try
            {
                if (cCuenta.permisosBorrar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cGestion.eliminarMeta(InfoGridMetas.Rows[IdexRowM]["IdMetaValor"].ToString().Trim());
                    loadGridMetas();
                    cargarInfoGridMetas();
                    Mensaje("Meta eliminada correctamente.");
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al eliminar Meta." + ex.Message);
            }
        }

        private void VerPeriodicidad()
        {
            if (DropDownListPeriodicidad.SelectedItem.Value == "14")//diaria
            {
                TextBox12.Visible = true;
                RequiredFieldValidatorDia.ValidationGroup = "AddMeta";
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "15")//Semanal
            {
                DropDownListSemana.Visible = true;
                CompareValidatorSemana.ValidationGroup = "AddMeta";
                DropDownListMes.Visible = true;
                CompareValidatorMes.ValidationGroup = "AddMeta";
                DropDownListAno.Visible = true;
                CompareValidatorAno.ValidationGroup = "AddMeta";
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "16")//Quincenal
            {
                DropDownListQuincena.Visible = true;
                CompareValidatorQuincena.ValidationGroup = "AddMeta";
                DropDownListMes.Visible = true;
                CompareValidatorMes.ValidationGroup = "AddMeta";
                DropDownListAno.Visible = true;
                CompareValidatorAno.ValidationGroup = "AddMeta";
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "18")//Mensual
            {
                DropDownListMes.Visible = true;
                CompareValidatorMes.ValidationGroup = "AddMeta";
                DropDownListAno.Visible = true;
                CompareValidatorAno.ValidationGroup = "AddMeta";
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "19")//Bimestral
            {
                DropDownListBimestre.Visible = true;
                CompareValidatorBimestre.ValidationGroup = "AddMeta";
                DropDownListAno.Visible = true;
                CompareValidatorAno.ValidationGroup = "AddMeta";
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "20")//Trimestral
            {
                DropDownListTrimestre.Visible = true;
                CompareValidatorTrimestre.ValidationGroup = "AddMeta";
                DropDownListAno.Visible = true;
                CompareValidatorAno.ValidationGroup = "AddMeta";
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "21")//Semestral
            {
                DropDownListSemestre.Visible = true;
                CompareValidatorSemestre.ValidationGroup = "AddMeta";
                DropDownListAno.Visible = true;
                CompareValidatorAno.ValidationGroup = "AddMeta";
            }
            else if (DropDownListPeriodicidad.SelectedItem.Value == "22")//Anual
            {
                DropDownListAno.Visible = true;
                CompareValidatorAno.ValidationGroup = "AddMeta";
            }
        }

        private void OcultarPeridicidad()
        {
            TextBox12.Visible = false;
            RequiredFieldValidatorDia.ValidationGroup = "";

            DropDownListSemana.Visible = false;
            CompareValidatorSemana.ValidationGroup = "";
            DropDownListSemana.SelectedIndex = 0;

            DropDownListQuincena.Visible = false;
            CompareValidatorQuincena.ValidationGroup = "";
            DropDownListQuincena.SelectedIndex = 0;

            DropDownListMes.Visible = false;
            CompareValidatorMes.ValidationGroup = "";
            DropDownListMes.SelectedIndex = 0;

            DropDownListBimestre.Visible = false;
            CompareValidatorBimestre.ValidationGroup = "";
            DropDownListBimestre.SelectedIndex = 0;

            DropDownListTrimestre.Visible = false;
            CompareValidatorTrimestre.ValidationGroup = "";
            DropDownListTrimestre.SelectedIndex = 0;

            DropDownListSemestre.Visible = false;
            CompareValidatorSemestre.ValidationGroup = "";
            DropDownListSemestre.SelectedIndex = 0;

            DropDownListAno.Visible = false;
            CompareValidatorAno.ValidationGroup = "";
            DropDownListAno.SelectedIndex = 0;

            TextBox10.Text = "";
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void Mensaje1(String Mensaje)
        {
            lblMsgBox1.Text = Mensaje;
            mpeMsgBox1.Show();
        }

        private void Mensaje2(String Mensaje)
        {
            lblMsgBox2.Text = Mensaje;
            mpeMsgBox2.Show();
        }

        private void agregarLista()
        {
            //cGestion.agregarVision(TextBox5.Text.Trim(), TextBox2.Text.Trim());
        }

        private void resetValues()
        {
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            Label26.Text = "";
            Label6.Text = "";
            Label9.Text = "";
            int contar = Convert.ToInt32(DropDownListPeriodicidad.Items.Count.ToString());
            DropDownListPeriodicidad.SelectedIndex = (contar - 1);
            DropDownListActivo.SelectedIndex = (0);
            ImageNominadr.Visible = false;
            ImageDenominador.Visible = false;
            BtnNominador.Visible = true;
            BtnDenominador.Visible = true;
        }

        private void resetValues1()
        {
            TextBox1.Text = "";
            int contar1 = Convert.ToInt32(DropDownListFormato.Items.Count.ToString());
            DropDownListFormato.SelectedIndex = (contar1 - 1);
            //TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";

        }

        private void inicializarValores()
        {
            IdexRow = 0;
            IdexRowV = 0;
            IdexRowVS = 0;
        }

        private void modificarLista()
        {
            agregarLista();
        }

        private void verModificar()
        {
            TabContainerIndicadores.ActiveTabIndex = 0;
            TbIndicadorSeleccionado.Visible = true;
            TbSeleccionarVariables.Visible = true;
            Label20.Text = InfoGrid.Rows[IdexRow]["CodigoIndicador"].ToString().Trim();
            Label21.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            TbIndicadores.Visible = true;
            TbCrearVariables.Visible = true;
            TbBotones.Visible = true;
            TbAddVaiables.Visible = false;
            TbModificaVariable.Visible = false;
            resetValues1();
        }

        private void EliminarIndicador()
        {
            try
            {
                if (cCuenta.permisosBorrar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    cGestion.eliminarIndicador(InfoGrid.Rows[IdexRow]["IdIndicador"].ToString().Trim());
                    loadGrid();
                    cargarInfoGrid();
                    Mensaje("Indicador eliminado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al eliminar Indicador." + ex.Message);
            }
        }

        private void EliminarVariable()
        {
            try
            {
                cGestion.eliminarVariables(InfoGridVariables.Rows[IdexRowV]["IdVariable"].ToString().Trim());
                loadGridVariables();
                cargarInfoGridVariables();
                Mensaje("Variable eliminada correctamente.");
                TbAddVaiables.Visible = false;
                resetValues1();
            }
            catch (Exception ex)
            {
                Mensaje1("Error al eliminar Variable." + ex.Message);
            }
        }

        private void ModificarVariable()
        {
            TbCrearVariables.Visible = false;
            TbModificaVariable.Visible = true;
            Label17.Text = InfoGridVariables.Rows[IdexRowV]["IdVariable"].ToString().Trim();
            TextBox2.Text = InfoGridVariables.Rows[IdexRowV]["Nombre"].ToString().Trim();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.Trim() == InfoGridVariables.Rows[IdexRowV]["Formato"].ToString().Trim())
                {
                    break;
                }
            }
        }

        private void modificarIndicador()
        {
            Label6.Text = InfoGrid.Rows[IdexRow]["IdIndicador"].ToString().Trim();
            TextBox3.Text = InfoGrid.Rows[IdexRow]["CodigoIndicador"].ToString().Trim();
            TextBox4.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            TextBox5.Text = InfoGrid.Rows[IdexRow]["Nominador"].ToString().Trim();
            TextBox6.Text = InfoGrid.Rows[IdexRow]["Denominador"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[IdexRow]["Nominador"].ToString().Trim();
            TextBox8.Text = InfoGrid.Rows[IdexRow]["Denominador"].ToString().Trim();
            Label9.Text = InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim() + "=";
            BtnIndicador.Visible = false;
            for (int i = 0; i < DropDownListPeriodicidad.Items.Count; i++)
            {
                DropDownListPeriodicidad.SelectedIndex = i;
                if (DropDownListPeriodicidad.SelectedItem.Text.Trim() == InfoGrid.Rows[IdexRow]["Periodicidad"].ToString().Trim())
                {
                    break;
                }
            }
            //string activosn;
            for (int i = 0; i < DropDownListActivo.Items.Count; i++)
            {
                DropDownListActivo.SelectedIndex = i;
                //activosn = DropDownListActivo.SelectedItem.Value.Trim();
                if (DropDownListActivo.SelectedItem.Value == InfoGrid.Rows[IdexRow]["Activo_SN"].ToString().Trim())
                {
                    break;
                }
            }
            loadGridVariables();
            cargarInfoGridVariables();
            loadGridMetas();
            cargarInfoGridMetas();
        }

        private void SelecVariable()
        {
            if (Label26.Text == "Nominador")
            {
                TextBox5.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox5.Text)) + InfoGridVariables.Rows[IdexRowVS]["Nombre"].ToString().Trim();
                TextBox7.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox7.Text)) + InfoGridVariables.Rows[IdexRowVS]["Nombre"].ToString().Trim();
            }
            if (Label26.Text == "Denominador")
            {
                TextBox6.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox6.Text)) + InfoGridVariables.Rows[IdexRowVS]["Nombre"].ToString().Trim();
                TextBox8.Text = HttpUtility.HtmlDecode(Sanitizer.GetSafeHtmlFragment(TextBox8.Text)) + InfoGridVariables.Rows[IdexRowVS]["Nombre"].ToString().Trim();
            }
        }

    }
}