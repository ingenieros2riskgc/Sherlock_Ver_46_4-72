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
using System.Threading;
using Microsoft.Security.Application;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace ListasSarlaft.UserControls.Riesgos.Iframe
{
    public partial class IFcategorias : System.Web.UI.Page
    {
        cControl cControl = new cControl();
        cRiesgo Riesgo = new cRiesgo();
        cRegistroOperacion cRegistroOperacion = new cRegistroOperacion();
        cCuenta cCuenta = new cCuenta();
        cError error = new cError();
        string IdFormulario = "5009";
        private static int LastInsertIdCE;
        private static int NuevoControl = 0;
        private static string LastControl = string.Empty;
        List<string> dropDownListsVariables = new List<string>();
        // Diccionario que guarda los valores de acuerdo al DropdownList
        public static Dictionary<string, int> tempValores = new Dictionary<string, int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "parent", "parent.refresh();", true);
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            //scrtManager.RegisterPostBackControl(IBinsert);
            scrtManager.RegisterPostBackControl(this.btnAceptar);
            scrtManager.RegisterPostBackControl(GridView6);

            if (!IsPostBack)
            {
                lblActControlSuccess.Visible = false;
                lblActControlFailed.Visible = false;
                lblActRiesgos.Visible = false;
                // Variable de sesión que guarda la relación de los riesgos actualziados
                Session["RiesgosActualizar"] = string.Empty;
            }

            if (Request.QueryString["CodControl"] != null)
            {
                string CodControl = Request.QueryString["CodControl"].ToString();
                string NombreControl = Request.QueryString["NombreControl"].ToString();
                string DescripcionControl = Request.QueryString["DescripcionControl"].ToString();
                string ObjetivoControl = Request.QueryString["ObjetivoControl"].ToString();
                string ResponsableEjecucion = Request.QueryString["ResponsableEjecucion"].ToString();
                string IdResponsablesEjecucion = Request.QueryString["IdResponsablesEjecucion"].ToString();
                string IdResponsableCalificacion = Request.QueryString["IdResponsableCalificacion"].ToString();
                string Periodicidad = Request.QueryString["Periodicidad"].ToString();
                string test = Request.QueryString["test"].ToString();
                string Reduce = Request.QueryString["Reduce"].ToString();
                tbLimpiar.Visible = true;
                IBupdate.Visible = true;
                TextBox16.Visible = true;
                tJustificacionCambios.Visible = true;
                mtdLoadValorVariables(CodControl);
            }
            else
            {
                string NombreControl = Request.QueryString["NombreControl"].ToString();
                string DescripcionControl = Request.QueryString["DescripcionControl"].ToString();
                string ObjetivoControl = Request.QueryString["ObjetivoControl"].ToString();
                string ResponsableEjecucion = Request.QueryString["ResponsableEjecucion"].ToString();
                string IdResponsablesEjecucion = Request.QueryString["IdResponsablesEjecucion"].ToString();
                string IdResponsableCalificacion = Request.QueryString["IdResponsableCalificacion"].ToString();
                string Periodicidad = Request.QueryString["Periodicidad"].ToString();
                string test = Request.QueryString["test"].ToString();
                string Reduce = Request.QueryString["Reduce"].ToString();
                tbLimpiar.Visible = true;
                IBinsert.Visible = true;
                IBupdate.Visible = false;
                TabContainer2.Visible = false;
                mtdLoadVariables();
            }
        }

        private void mtdLoadValorVariables(string CodControl)
        {
            mtdLoadVariables();
            string strErrMsg = string.Empty;
            int IdControl = 0;
            List<clsDTOControlxVariable> lstControlxVariable = new List<clsDTOControlxVariable>();
            clsBLLControlxVariable cControlxVariable = new clsBLLControlxVariable();
            IdControl = cControlxVariable.mtdGetIdControl(ref strErrMsg, CodControl);
            lstControlxVariable = cControlxVariable.mtdConsultarVariablexContol(ref lstControlxVariable, ref strErrMsg, IdControl);

            //Se consulta en la tabla control el ID guardado para cada una de las variables
            DataTable dt = cControl.SeleccionarCategoriasControl(IdControl);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    DropDownList ddl = (DropDownList)FindControl("ddl" + Row["DescripcionVariable"].ToString());
                    // Valida si el value existe en los elementos
                    if (ddl.Items.FindByValue(Row["IdCategoriaVariableControl"].ToString()) != null)
                        ddl.SelectedValue = Row["IdCategoriaVariableControl"].ToString();
                    else
                        ddl.SelectedValue = "---";
                    string variable = Row["DescripcionVariable"].ToString();
                    // Se busca el peso de la variable y se guarda en la variable PorcentajeVariable
                    clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();
                    List<clsDTOCalificacionControl> lstCalificacion = new List<clsDTOCalificacionControl>();
                    lstCalificacion = cPorcentaje.mtdConsultarCalificacionControl(ref lstCalificacion, ref strErrMsg);

                    // Se trae el id guardado en la base de datos de calificación del control
                    int calificacion = cControl.SeleccionaCalificacion(IdControl);

                    foreach (clsDTOCalificacionControl objCalificacion in lstCalificacion)
                    {
                        if (objCalificacion.intIdCalificacionControl == calificacion)
                        {
                            Session["IdCalificacionControl"] = objCalificacion.intIdCalificacionControl;
                            Label14.Text = objCalificacion.strNombreEscala.ToString().Trim();
                            Panel1.BackColor = System.Drawing.Color.FromName(objCalificacion.strColor.ToString().Trim());
                        }
                    }
                }
                Session["total"] = 0;
            }
        }

        private void mtdLoadVariables()
        {
            string strErrMsg = string.Empty;
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);
            List<clsDTOCategoriasVariableControl> lstCategoria = new List<clsDTOCategoriasVariableControl>();
            clsDALCategoriaVariableControl cCategoria = new clsDALCategoriaVariableControl();
            DataTable dtInfo = new DataTable();
            if (lstVariables != null)
            {

                //string tablestring = "";
                //dt is datatable object which holds DB results.
                //tablestring = tablestring + "<table width='100 % ' border='1' cellspacing='0' cellpadding='2' bordercolor='White'><tr align='left'>";
                int i = 0;
                HtmlTableRow tRow = new HtmlTableRow();
                //tRow.BgColor = "#BBBBBB";
                foreach (clsDTOVariableCalificacionControl objVariable in lstVariables)
                {
                    //int index = pnlTextBoxes.Controls.OfType<DropDownList>().ToList().Count + 1;
                    bool flag = cCategoria.mtdConsultarCategoriaActivas(ref dtInfo, ref strErrMsg, objVariable.intIdCalificacionControl);
                    DropDownList ddlCategorias = this.CreateDropDown("ddl" + objVariable.strDescripcionVariable.ToString().Trim(), dtInfo);
                    ddlCategorias.Width = 500;
                    //Se agregan los nombres de los dropdownlist dinamicos para mas adelante guardar su SelectedValue
                    dropDownListsVariables.Add(ddlCategorias.ClientID);

                    //Si el peso de la variable es igual a 0 se oculta el control
                    //if (objVariable.FlPesoVariable == 0)
                    //    ddlCategorias.Visible = false;                        

                    Label lblVariable = new Label();
                    lblVariable.ID = "lbl" + objVariable.strDescripcionVariable.ToString().Trim();
                    lblVariable.CssClass = "LabelClass";
                    lblVariable.Text = objVariable.strDescripcionVariable.ToString().Trim();

                    if (objVariable.FlPesoVariable == 0)
                        tRow.Visible = false;
                    HtmlTableCell tCell = new HtmlTableCell();
                    tCell.Width = "200px";
                    tCell.Controls.Add(lblVariable);
                    tCell.BgColor = "#BBBBBB";
                    tRow.Cells.Add(tCell);
                    tCell = new HtmlTableCell();
                    tCell.Width = "200px";
                    tCell.Controls.Add(ddlCategorias);
                    tRow.Cells.Add(tCell);
                    tbVariableCategoriaControl.Rows.Add(tRow);
                    tRow = new HtmlTableRow();
                    i++;
                }

            }
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        private DropDownList CreateDropDown(string id, DataTable dtInfo)
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = id;
            ddl.Width = 200;
            ddl.Items.Insert(0, new ListItem("---", "---"));
            ddl.AutoPostBack = true;
            if (Request.QueryString["CodControl"] != null)
                ddl.SelectedIndexChanged += this.calcularEficacionUpd;
            else
                ddl.SelectedIndexChanged += this.calcularEficacia;
            for (int iteracion = 0; iteracion < dtInfo.Rows.Count; iteracion++)
            {
                ddl.Items.Insert(iteracion + 1, new ListItem(dtInfo.Rows[iteracion]["DescripcionCategoria"].ToString().Trim(), dtInfo.Rows[iteracion]["IdCategoriaVariableControl"].ToString()));
            }
            return ddl;
        }

        protected void calcularEficacionUpd(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string IdDDL = ddl.ID;
            string variable = IdDDL.Substring(3);
            string strErrMsg = string.Empty;
            string Categoria = ddl.SelectedItem.ToString().Trim();
            int IdCategoria = ddl.SelectedValue.Equals("---") ? 0 : Convert.ToInt32(ddl.SelectedValue);
            clsBLLControlxVariable cControlxVariable = new clsBLLControlxVariable();
            string CodControl = "";
            int IdControl = 0;
            List<clsDTOControlxVariable> lstControlxVariable = new List<clsDTOControlxVariable>();
            IdControl = cControlxVariable.mtdGetIdControl(ref strErrMsg, CodControl);

            lstControlxVariable = cControlxVariable.mtdConsultarVariablexContol(ref lstControlxVariable, ref strErrMsg, IdControl);
            //lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);

            // Se recorren las variables con peso
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            lstVariables = cControl.SeleccionarVariables();
            double total = 0;
            foreach (var _variable in lstVariables)
            {
                DropDownList ddlUp = (DropDownList)FindControl("ddl" + _variable.strDescripcionVariable);
                if (!Page.IsPostBack)
                {
                    var item = lstControlxVariable.FirstOrDefault(x => x.strNombreVariable == _variable.strDescripcionVariable);
                    if (true)
                    {
                        ddlUp.SelectedValue = item.intIdCategoria.ToString();
                    }
                }
                string variableUp = _variable.strDescripcionVariable;
                string CategoriaUp = ddlUp.SelectedItem.Text;
                int IdCategoriaUp = ddlUp.SelectedValue.Equals("---") ? 0 : Convert.ToInt32(ddlUp.SelectedValue);
                clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();
                double PorcentajeVariable = cPorcentaje.mtdConsultarPorcentajesxVariable(ref strErrMsg, ref variableUp);
                clsBLLCategoriaVariableControl cCategoria = new clsBLLCategoriaVariableControl();
                int PesoCategoria = cCategoria.mtdPesoCategoria(ref strErrMsg, IdCategoriaUp);
                double ValorCalificacion = 0;
                ValorCalificacion = ValorCalificacion + (PorcentajeVariable * PesoCategoria);
                ValorCalificacion = (ValorCalificacion / 100);
                total = total + ValorCalificacion;
                List<clsDTOCalificacionControl> lstCalificacion = new List<clsDTOCalificacionControl>();
                lstCalificacion = cPorcentaje.mtdConsultarCalificacionControl(ref lstCalificacion, ref strErrMsg);
                if (lstCalificacion != null)
                {
                    foreach (clsDTOCalificacionControl objCalificacion in lstCalificacion)
                    {
                        if (total >= objCalificacion.intLimiteInferior && total <= objCalificacion.intLimiteSuperior)
                        {
                            Session["IdCalificacionControl"] = objCalificacion.intIdCalificacionControl;
                            Label14.Text = objCalificacion.strNombreEscala.ToString().Trim();
                            Panel1.BackColor = System.Drawing.Color.FromName(objCalificacion.strColor.ToString().Trim());
                        }
                    }
                }
            }
            error.errorMessage("Cod Control: " + Request.QueryString["CodControl"].ToString() + " |Total calificación: " + total);
        }

        protected void calcularEficacia(object sender, EventArgs e)
        {
            try
            {
                //calcularCalificacionControl();
                DropDownList ddl = (DropDownList)sender;
                string IdDDL = ddl.ID;
                string variable = IdDDL.Substring(3);
                string strErrMsg = string.Empty;
                string Categoria = ddl.SelectedItem.ToString().Trim();
                int IdCategoria = ddl.SelectedValue.Equals("---") ? 0 : Convert.ToInt32(ddl.SelectedValue);

                //Se busca el peso de la variable y se guarda en la variable PorcentajeVariable
                clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();

                // Agrega al diccionario los DropdownList
                var _valor = tempValores.FirstOrDefault(x => x.Key == variable);
                if (_valor.Key is null)
                    tempValores.Add(variable, IdCategoria);
                else
                    tempValores[_valor.Key] = IdCategoria;

                double total = 0;

                // Se recorre el diccionario para hacer el calculo
                foreach (var tempValor in tempValores)
                {
                    string _tempVariable = tempValor.Key;
                    int tempCategoria = tempValor.Value;
                    double PorcentajeVariable = cPorcentaje.mtdConsultarPorcentajesxVariable(ref strErrMsg, ref _tempVariable);
                    //Se busca la calificacion de la categoria y se guarda en la variable IdCategoria
                    clsBLLCategoriaVariableControl cCategoria = new clsBLLCategoriaVariableControl();
                    int PesoCategoria = cCategoria.mtdPesoCategoria(ref strErrMsg, tempCategoria);
                    double ValorCalificacion = 0;
                    ValorCalificacion = ValorCalificacion + (PorcentajeVariable * PesoCategoria);
                    ValorCalificacion = (ValorCalificacion / 100);
                    total = total + ValorCalificacion;
                }
                List<clsDTOCalificacionControl> lstCalificacion = new List<clsDTOCalificacionControl>();
                lstCalificacion = cPorcentaje.mtdConsultarCalificacionControl(ref lstCalificacion, ref strErrMsg);
                if (lstCalificacion != null)
                {
                    foreach (clsDTOCalificacionControl objCalificacion in lstCalificacion)
                    {
                        Label14.Text = "";
                        if (total >= objCalificacion.intLimiteInferior && total <= objCalificacion.intLimiteSuperior)
                        {
                            Session["IdCalificacionControl"] = objCalificacion.intIdCalificacionControl;
                            Label14.Text = objCalificacion.strNombreEscala.ToString().Trim();
                            Panel1.BackColor = System.Drawing.Color.FromName(objCalificacion.strColor.ToString().Trim());
                            break;
                        }
                        else
                        {
                            Label14.Text = "No cumple con el rango de calificación";
                            Panel1.BackColor = System.Drawing.Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void mtdUpdateCategoriaValue(clsDTOControlxVariable objControlxVariable)
        {
            string strErrMsq = string.Empty;
            clsBLLControlxVariable cControlxVariable = new clsBLLControlxVariable();
            cControlxVariable.mtdUpdateCategoria(objControlxVariable, ref strErrMsq);
        }

        protected void IBinsert_Click(object sender, ImageClickEventArgs e)
        {
            mtdAgregarControl();
        }

        void mtdAgregarControl()
        {
            string strErrMsg = string.Empty;
            try
            {
                int scope = agregarControl(ref strErrMsg);
                if (scope > 0)
                {
                    clsDTOControlxVariable objControlxVariable = new clsDTOControlxVariable();
                    clsBLLControlxVariable cControlxVariable = new clsBLLControlxVariable();
                    List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
                    clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
                    bool booResult = false;
                    lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);
                    int LastIdControl = cControlxVariable.mtdLastIdControl(ref strErrMsg);
                    clsDALControlxVariable dalControl = new clsDALControlxVariable();
                    string NameControl = dalControl.mtdGetNameControl(ref strErrMsg, LastIdControl.ToString());
                    string DesControl = dalControl.mtdGetDesControl(ref strErrMsg, LastIdControl.ToString());
                    GetLastControl();
                    foreach (clsDTOVariableCalificacionControl objVariable in lstVariables)
                    {
                        objControlxVariable.intIdControl = LastIdControl;
                        objControlxVariable.strNombreVariable = objVariable.strDescripcionVariable.ToString().Trim();
                        DropDownList ddl = (DropDownList)FindControl("ddl" + objVariable.strDescripcionVariable.ToString().Trim());
                        objControlxVariable.intIdCategoria = ddl.SelectedValue.Equals("---") ? 0 : Convert.ToInt32(ddl.SelectedValue);
                        objControlxVariable.strNombreCategoria = ddl.SelectedItem.Text;
                        booResult = cControlxVariable.mtdInsertarControlxVariable(objControlxVariable, ref strErrMsg);
                    }
                    //Notificacion de creacion de Control
                    string IdResponsableCalificacion = Request.QueryString["IdResponsableCalificacion"].ToString();
                    string IdResponsablesEjecucion = Request.QueryString["IdResponsablesEjecucion"].ToString();
                    if (IdResponsableCalificacion != "")
                    {
                        boolEnviarNotificacion(7, Convert.ToInt16("0"), Convert.ToInt16(IdResponsableCalificacion), "",
                            "Ha sido asignado como responsable de calificación de un control, código del control: " + LastControl + " - Nombre: " + NameControl + " - Descripción: " + DesControl + ", en la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br /><br />");
                    }
                    if (IdResponsablesEjecucion != "")
                    {
                        boolEnviarNotificacionEjecucion(7, Convert.ToInt16("0"), IdResponsablesEjecucion, "",
                            "Ha sido asignado como responsable de ejecución de un control, código del control: " + LastControl + " - Nombre: " + NameControl + " - Descripción: " + DesControl + ", en la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br /><br />");
                    }
                    if (booResult == true)
                    {
                        //Session["LastControl"] = scope;
                        //Mensaje("El Control [" + LastControl + "] fue registrado con éxito");
                        Session["end"] = "1";
                    }
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "onClick", "onClick", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "parent", "parent.refresh();", true);
                }
                else
                {
                    Mensaje(strErrMsg);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error: " + ex);
            }


        }

        public cControlEntity ConsultarDropdownList()
        {
            cControlEntity controlEntity = new cControlEntity();
            int contadorVariable = 1;
            string IdCalificacionControl = Session["IdCalificacionControl"].ToString();

            // Se comienza a llenar el objeto control para hacer la inserción de datos

            controlEntity.NombreControl = Request.QueryString["NombreControl"].ToString();
            controlEntity.DescripcionControl = Request.QueryString["DescripcionControl"].ToString();
            controlEntity.ObjetivoControl = Request.QueryString["ObjetivoControl"].ToString();
            controlEntity.Responsable = Convert.ToInt32(Request.QueryString["IdResponsableCalificacion"].ToString());
            controlEntity.IdPeriodicidad = Convert.ToInt32(Request.QueryString["Periodicidad"].ToString());
            controlEntity.IdTest = Convert.ToInt32(Request.QueryString["test"].ToString());
            controlEntity.IdCalificacionControl = Convert.ToInt32(IdCalificacionControl);
            controlEntity.IdMitiga = Convert.ToInt32(Request.QueryString["Reduce"].ToString());
            controlEntity.IdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            controlEntity.ResponsableEjecucion = Request.QueryString["IdResponsablesEjecucion"].ToString();

            // Se guardan los SelectedValue de los Dropdownlist Dinámicos

            foreach (var dropDownListsVariable in dropDownListsVariables)
            {
                DropDownList dropDownList = (DropDownList)FindControl(dropDownListsVariable);
                switch (contadorVariable)
                {
                    case 1: controlEntity.IdClaseControl = ValidarNumero(dropDownList.SelectedValue); break;
                    case 2: controlEntity.IdTipoControl = ValidarNumero(dropDownList.SelectedValue); break;
                    case 3: controlEntity.IdResponsableExperiencia = ValidarNumero(dropDownList.SelectedValue); break;
                    case 4: controlEntity.IdDocumentacion = ValidarNumero(dropDownList.SelectedValue); break;
                    case 5: controlEntity.IdResponsabilidad = ValidarNumero(dropDownList.SelectedValue); break;
                    case 6: controlEntity.Variable6 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 7: controlEntity.Variable7 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 8: controlEntity.Variable8 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 9: controlEntity.Variable9 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 10: controlEntity.Variable10 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 11: controlEntity.Variable11 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 12: controlEntity.Variable12 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 13: controlEntity.Variable13 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 14: controlEntity.Variable14 = ValidarNumero(dropDownList.SelectedValue); break;
                    case 15: controlEntity.Variable15 = ValidarNumero(dropDownList.SelectedValue); break;
                    default: break;
                }
                contadorVariable++;
            };

            return controlEntity;
        }

        private int agregarControl(ref string strError)
        {
            int scope = 0;
            try
            {

                cControlEntity controlEntity = new cControlEntity();
                int contadorVariable = 1;
                string IdCalificacionControl = Session["IdCalificacionControl"].ToString();

                // Se comienza a llenar el objeto control para hacer la inserción de datos
                controlEntity.IdControl = 0;
                controlEntity.NombreControl = Request.QueryString["NombreControl"].ToString();
                controlEntity.DescripcionControl = Request.QueryString["DescripcionControl"].ToString();
                controlEntity.ObjetivoControl = Request.QueryString["ObjetivoControl"].ToString();
                controlEntity.Responsable = Convert.ToInt32(Request.QueryString["IdResponsableCalificacion"].ToString());
                controlEntity.IdPeriodicidad = Convert.ToInt32(Request.QueryString["Periodicidad"].ToString());
                controlEntity.IdTest = Convert.ToInt32(Request.QueryString["test"].ToString());
                controlEntity.IdCalificacionControl = Convert.ToInt32(IdCalificacionControl);
                controlEntity.IdMitiga = Convert.ToInt32(Request.QueryString["Reduce"].ToString());
                controlEntity.IdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                controlEntity.ResponsableEjecucion = Request.QueryString["IdResponsablesEjecucion"].ToString();

                // Se guardan los SelectedValue de los Dropdownlist Dinámicos

                foreach (var dropDownListsVariable in dropDownListsVariables)
                {
                    DropDownList dropDownList = (DropDownList)FindControl(dropDownListsVariable);
                    switch (contadorVariable)
                    {
                        case 1: controlEntity.IdClaseControl = ValidarNumero(dropDownList.SelectedValue); break;
                        case 2: controlEntity.IdTipoControl = ValidarNumero(dropDownList.SelectedValue); break;
                        case 3: controlEntity.IdResponsableExperiencia = ValidarNumero(dropDownList.SelectedValue); break;
                        case 4: controlEntity.IdDocumentacion = ValidarNumero(dropDownList.SelectedValue); break;
                        case 5: controlEntity.IdResponsabilidad = ValidarNumero(dropDownList.SelectedValue); break;
                        case 6: controlEntity.Variable6 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 7: controlEntity.Variable7 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 8: controlEntity.Variable8 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 9: controlEntity.Variable9 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 10: controlEntity.Variable10 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 11: controlEntity.Variable11 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 12: controlEntity.Variable12 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 13: controlEntity.Variable13 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 14: controlEntity.Variable14 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 15: controlEntity.Variable15 = ValidarNumero(dropDownList.SelectedValue); break;
                        default: break;
                    }
                    contadorVariable++;
                };

                scope = cControl.InsertControl(controlEntity);

                return scope;
            }
            catch (Exception ex)
            {
                strError = "El control no ha sido calificado";
            }

            return scope;
        }

        private bool ActualizarControl(ref string strError)
        {
            bool booResult = false;
            try
            {
                cControlEntity controlEntity = EntidadControl();
                cControl.InsertControl(controlEntity);
                booResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return booResult;
        }

        private bool InsertarJustificacionCambios()
        {
            bool booResult = false;
            try
            {
                cControlEntity controlEntity = EntidadControl();
                controlEntity.JustificacionCambios = TextBox16.Text;
                cControl.InsertJustificacion(controlEntity);
                booResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return booResult;
        }

        private bool InsertarComentario()
        {

            bool booResult = false;
            try
            {
                cControlEntity controlEntity = EntidadControl();
                controlEntity.JustificacionCambios = TextBox16.Text;
                controlEntity.NombreUsuario = (String)Session["nombreUsuario"];
                cControl.InsertComentario(controlEntity);
                booResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return booResult;
            try
            {
                int idControl = Convert.ToInt32(Request.QueryString["CodControl"].ToString().Replace('C', ' ').Trim());
                string nombreUsuario = (String)Session["nombreUsuario"];
            }
            catch (Exception)
            {

                throw;
            }
        }

        private cControlEntity EntidadControl()
        {
            try
            {
                cControlEntity controlEntity = new cControlEntity();
                int contadorVariable = 1;
                string IdCalificacionControl = Session["IdCalificacionControl"].ToString();
                // Se comienza a llenar el objeto control para hacer la inserción de datos

                controlEntity.IdControl = Convert.ToInt32(Request.QueryString["CodControl"].ToString().Replace('C', ' ').Trim());
                controlEntity.NombreControl = Request.QueryString["NombreControl"].ToString();
                controlEntity.DescripcionControl = Request.QueryString["DescripcionControl"].ToString();
                controlEntity.ObjetivoControl = Request.QueryString["ObjetivoControl"].ToString();
                controlEntity.Responsable = Convert.ToInt32(Request.QueryString["IdResponsableCalificacion"].ToString());
                controlEntity.IdPeriodicidad = Convert.ToInt32(Request.QueryString["Periodicidad"].ToString());
                controlEntity.IdTest = Convert.ToInt32(Request.QueryString["test"].ToString());
                controlEntity.IdCalificacionControl = Convert.ToInt32(IdCalificacionControl);
                controlEntity.IdMitiga = Convert.ToInt32(Request.QueryString["Reduce"].ToString());
                controlEntity.IdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                controlEntity.ResponsableEjecucion = Request.QueryString["IdResponsablesEjecucion"].ToString();

                // Se guardan los SelectedValue de los Dropdownlist Dinámicos el orden depende del Id Variable en orden ascendente

                foreach (var dropDownListsVariable in dropDownListsVariables)
                {
                    DropDownList dropDownList = (DropDownList)FindControl(dropDownListsVariable);
                    switch (contadorVariable)
                    {
                        case 1: controlEntity.IdClaseControl = ValidarNumero(dropDownList.SelectedValue); break;
                        case 2: controlEntity.IdTipoControl = ValidarNumero(dropDownList.SelectedValue); break;
                        case 3: controlEntity.IdResponsableExperiencia = ValidarNumero(dropDownList.SelectedValue); break;
                        case 4: controlEntity.IdDocumentacion = ValidarNumero(dropDownList.SelectedValue); break;
                        case 5: controlEntity.IdResponsabilidad = ValidarNumero(dropDownList.SelectedValue); break;
                        case 6: controlEntity.Variable6 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 7: controlEntity.Variable7 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 8: controlEntity.Variable8 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 9: controlEntity.Variable9 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 10: controlEntity.Variable10 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 11: controlEntity.Variable11 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 12: controlEntity.Variable12 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 13: controlEntity.Variable13 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 14: controlEntity.Variable14 = ValidarNumero(dropDownList.SelectedValue); break;
                        case 15: controlEntity.Variable15 = ValidarNumero(dropDownList.SelectedValue); break;
                        default: break;
                    }
                    contadorVariable++;
                };
                return controlEntity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int? ValidarNumero(string value)
        {
            int a = 0;
            if (int.TryParse(value, out a))
                return a;
            else
                return null;
        }

        private void GetLastControl()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cControl.GetLastControl();

                if (dtInfo.Rows.Count > 0)
                    LastControl =  dtInfo.Rows[0]["LastControl"].ToString().Trim();
                else
                    LastControl = "C1";

                Session["LastControl"] = LastControl;
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código control. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            try
            {
                #region informacion basica
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString();
                        Otros = row["Otros"].ToString();
                        Asunto = row["Asunto"].ToString() + " - Responsable Calificación";
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion

                #region Consulta el correo del Destinatario
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
                #endregion Consulta el correo del Destinatario

                #region Consulta el correo del Jefe Inmediato
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
                #endregion Consulta el correo del Jefe Inmediato

                #region Consulta el correo del Jefe Mediato
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
                #endregion Consulta el correo del Jefe Mediato

                //Insertar el Registro en la tabla de Correos Enviados
                #region Insertar el Registro en la tabla de Correos Enviados
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
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Insert();
                #endregion Insertar el Registro en la tabla de Correos Enviados
            }
            catch (Exception except)
            {
                // Handle the Exception.
                Mensaje("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim());
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                #region
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
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

                    #region
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region
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
                    #endregion Envio Correo
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    Mensaje("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString());
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    #region Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString();
                    SqlDataSource200.Update();
                    #endregion Actualiza el Estado del Correo Enviado
                }
            }

            return (err);
        }

        private Boolean boolEnviarNotificacionEjecucion(int idEvento, int idRegistro, string idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            bool err = false;
            string Destinatario = "", Cuerpo = "", selectCommand = "", Otros = "", Copia = "", Asunto = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            try
            {
                #region informacion basica
                //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString();
                        Otros = row["Otros"].ToString();
                        Asunto = row["Asunto"].ToString() + " - Responsable Ejecución";
                        Cuerpo = textoAdicional + "<br />***Nota: " + row["Cuerpo"].ToString();
                        //NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        //AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        //AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        //RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                        Destinatario = mtdBuscarCorreosRespEjecucion(idNodoJerarquia);
                    }
                }
                #endregion

                //Insertar el Registro en la tabla de Correos Enviados
                #region Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario;
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = "";
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = "";
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString();
                SqlDataSource200.Insert();
                #endregion Insertar el Registro en la tabla de Correos Enviados
            }
            catch (Exception except)
            {
                // Handle the Exception.
                Mensaje("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString());
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                    message.From = fromAddress;//here you can set address

                    #region
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region
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
                    #endregion Envio Correo
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    Mensaje("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim());
                    err = true;
                }

                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    #region Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                    #endregion Actualiza el Estado del Correo Enviado
                }
            }

            return (err);
        }

        private string mtdBuscarCorreosRespEjecucion(string IdResponsableEjecucion)
        {
            string CorreosResponsablesEjecucion = string.Empty;
            string[] srtSeparator = new string[] { "|" };
            string[] arrNombres = IdResponsableEjecucion.Split(srtSeparator, StringSplitOptions.None);
            string IdNombre = string.Empty;
            int i = arrNombres.Length;

            for (int j = 0; j < i; j++)
            {
                if (arrNombres[j].Contains("JO"))
                    CorreosResponsablesEjecucion += cControl.CorreoJerarquia(arrNombres[j].Remove(0, 3));
                else if (arrNombres[j].Contains("GT"))
                    CorreosResponsablesEjecucion += cControl.CorreoGrupoTrabajo(arrNombres[j].Remove(0, 3));
            }
            return CorreosResponsablesEjecucion;
        }

        protected void ImbClean_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            clsDTOControlxVariable objControlxVariable = new clsDTOControlxVariable();
            clsBLLControlxVariable cControlxVariable = new clsBLLControlxVariable();
            List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            bool booResult = false;
            lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);
            foreach (clsDTOVariableCalificacionControl objVariable in lstVariables)
            {
                DropDownList ddl = (DropDownList)FindControl("ddl" + objVariable.strDescripcionVariable.ToString().Trim());
                ddl.SelectedIndex = 0;
                Label14.Text = "";
                Panel1.BackColor = System.Drawing.Color.FromName(ConsoleColor.White.ToString());
                Session["variable"] = null;
                Session["valorVariable"] = null;
                Session["total"] = 0;
                Session["IdCalificacionControl"] = null;
            }
            lblActControlFailed.Visible = false;
            lblActControlSuccess.Visible = false;
        }

        protected void IBupdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string strErrMsg = string.Empty;
                
                // Actualiza la calificación de control e insertar los comentarios
                ActualizarControl(ref strErrMsg);
                InsertarJustificacionCambios();
                InsertarComentario();


                // Seleccionar los numeros de riesgo asosciados al control actual
                string codControl = Request.QueryString["CodControl"].ToString().Replace('C',' ').Trim();
                List<string>riesgosAsociados = cControl.SeleccionarRiesgosAsociadosControl(Convert.ToInt32(codControl));
                string riesgosActualizarIN = string.Join(",", riesgosAsociados);

                Session["RiesgosActualizar"] = riesgosActualizarIN;

                // Califica los riesgos asociados
                if (!string.IsNullOrEmpty(riesgosActualizarIN))
                {
                    DataTable dt = cControl.CalcularRiesgoResidual(riesgosActualizarIN);

                }

                lblActControlSuccess.Visible = true;
                lblActControlFailed.Visible = false;

                // Si se actualiza calificación residual riesgos se muestra el mensaje
                if (!string.IsNullOrEmpty((string)Session["RiesgosActualizar"]))
                {
                    lblActRiesgos.Visible = true;
                    lblActRiesgos.InnerText = $"Se actualizaron los siguientes riesgos asociados: {(string)Session["RiesgosActualizar"]}";
                }
            }
            catch (Exception ex)
            {
                lblActControlFailed.Visible = true;
                lblActControlSuccess.Visible = false;
                lblActRiesgos.Visible = false;
                lblActControlFailed.InnerText = $"Ocurrió un error al actualizar el control. {ex.Message}";
            }
        }

        #region Comentarios

        private DataTable infoGridControles;

        private DataTable InfoGridControles
        {
            get
            {
                infoGridControles = (DataTable)ViewState["infoGridControles"];
                return infoGridControles;
            }
            set
            {
                infoGridControles = value;
                ViewState["infoGridControles"] = infoGridControles;
            }
        }

        private DataTable infoGridArchivoControl;
        private DataTable InfoGridArchivoControl
        {
            get
            {
                infoGridArchivoControl = (DataTable)ViewState["infoGridArchivoControl"];
                return infoGridArchivoControl;
            }
            set
            {
                infoGridArchivoControl = value;
                ViewState["infoGridArchivoControl"] = infoGridArchivoControl;
            }
        }

        private DataTable infoGridComentarioControl;

        private DataTable InfoGridComentarioControl
        {
            get
            {
                infoGridComentarioControl = (DataTable)ViewState["infoGridComentarioControl"];
                return infoGridComentarioControl;
            }
            set
            {
                infoGridComentarioControl = value;
                ViewState["infoGridComentarioControl"] = infoGridComentarioControl;
            }
        }

        private int rowGridArchivoControl;
        private int RowGridArchivoControl
        {
            get
            {
                rowGridArchivoControl = (int)ViewState["rowGridArchivoControl"];
                return rowGridArchivoControl;
            }
            set
            {
                rowGridArchivoControl = value;
                ViewState["rowGridArchivoControl"] = rowGridArchivoControl;
            }
        }

        private int rowGridControles;
        private int RowGridControles
        {
            get
            {
                rowGridControles = (int)ViewState["rowGridControles"];
                return rowGridControles;
            }
            set
            {
                rowGridControles = value;
                ViewState["rowGridControles"] = rowGridControles;
            }
        }

        private int rowGridComentarioControl;
        private int RowGridComentarioControl
        {
            get
            {
                rowGridComentarioControl = (int)ViewState["rowGridComentarioControl"];
                return rowGridComentarioControl;
            }
            set
            {
                rowGridComentarioControl = value;
                ViewState["rowGridComentarioControl"] = rowGridComentarioControl;
            }
        }

        private void loadGridArchivoControl()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdArchivo", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("UrlArchivo", typeof(string));
            InfoGridArchivoControl = grid;
            GridView6.DataSource = InfoGridArchivoControl;
            GridView6.DataBind();
        }

        private void loadInfoArchivoControl()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoArchivoControl(Request.QueryString["CodControl"].ToString().Replace('C', ' ').Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridArchivoControl.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdArchivo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["UrlArchivo"].ToString().Trim()
                                                                 });
                }
                GridView6.DataSource = InfoGridArchivoControl;
                GridView6.DataBind();
            }
        }
        private void loadGridComentarioControl()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdComentario", typeof(string));
            grid.Columns.Add("NombreUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("ComentarioCorto", typeof(string));
            grid.Columns.Add("Comentario", typeof(string));
            InfoGridComentarioControl = grid;
            GridView5.DataSource = InfoGridComentarioControl;
            GridView5.DataBind();
        }

        private void loadInfoComentarioControl()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cControl.loadInfoComentarioControl(Request.QueryString["CodControl"].ToString().Replace('C', ' ').Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridComentarioControl.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdComentario"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["NombreUsuario"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["ComentarioCorto"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["Comentario"].ToString().Trim()
                                                                     });
                }
                GridView5.DataSource = InfoGridComentarioControl;
                GridView5.DataBind();
            }
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridArchivoControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarPdfControl();
                    break;
            }
        }

        private void verComentario()
        {
            TextBox16.Text = InfoGridComentarioControl.Rows[RowGridComentarioControl]["Comentario"].ToString().Trim();
            TextBox16.ReadOnly = true;
            //ImageButton9.Visible = true;
        }

        private void mtdCargarPdfControl()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty;
            #endregion Vars

            dtInfo = cControl.loadCodigoArchivoControl();

            #region Nombre Archivo
            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = dtInfo.Rows[0]["NumRegistros"].ToString().Trim() +
                    "-" + InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim() +
                    "-" + FileUpload2.FileName.ToString().Trim();
            else
                strNombreArchivo = "1-" + InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim() +
                    "-" + FileUpload2.FileName.ToString().Trim();
            #endregion Nombre Archivo

            Stream fs = FileUpload2.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);

            cControl.mtdAgregarPdf("1", InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim(), strNombreArchivo, bPdfData);
        }

        private void mtdDescargarPdfControl()
        {
            #region Vars
            string strNombreArchivo = InfoGridArchivoControl.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cControl.mtdDescargarPdf(strNombreArchivo);
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

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridComentarioControl = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    verComentario();
                    break;
            }
        }

        protected void btnAdjuntar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (FileUpload2.HasFile)
                    {
                        /*if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                        {*/
                        mtdCargarPdfControl();
                        loadGridArchivoControl();
                        loadInfoArchivoControl();
                        Mensaje("Archivo cargado exitósamente.");
                        /*}
                        else
                            Mensaje("El archivo a cargar debe ser en formato PDF.");*/
                    }
                    else
                        Mensaje("No hay archivos para cargar.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al agregar el archivo. " + ex.Message);
            }
        }

        #endregion
    }
}