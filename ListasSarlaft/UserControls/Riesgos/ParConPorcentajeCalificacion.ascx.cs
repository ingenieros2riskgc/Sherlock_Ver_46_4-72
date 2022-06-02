using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class ParConPorcentajeCalificacion : System.Web.UI.UserControl
    {
		string IdFormulario = "5001";
        cParametrizacionRiesgos cParametrizacionRiesgos = new cParametrizacionRiesgos();
        cCuenta cCuenta = new cCuenta();
        clsDTOParaPorcentajeCalificacion PorcentajeCalificacion = new clsDTOParaPorcentajeCalificacion();
        clsBLLParaPorcentajeCalificacion PorcentajeBLL = new clsBLLParaPorcentajeCalificacion();
        #region Properties
        private DataTable infoGridTratamientos;
        private int rowGridTratamientos;
        private int pagIndexTratamientos;

        private DataTable InfoGridTratamientos
        {
            get
            {
                infoGridTratamientos = (DataTable)ViewState["infoGridTratamientos"];
                return infoGridTratamientos;
            }
            set
            {
                infoGridTratamientos = value;
                ViewState["infoGridTratamientos"] = infoGridTratamientos;
            }
        }

        private int RowGridTratamientos
        {
            get
            {
                rowGridTratamientos = (int)ViewState["rowGridTratamientos"];
                return rowGridTratamientos;
            }
            set
            {
                rowGridTratamientos = value;
                ViewState["rowGridTratamientos"] = rowGridTratamientos;
            }
        }

        private int PagIndexTratamientos
        {
            get
            {
                pagIndexTratamientos = (int)ViewState["pagIndexTratamientos"];
                return pagIndexTratamientos;
            }
            set
            {
                pagIndexTratamientos = value;
                ViewState["pagIndex"] = pagIndexTratamientos;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
                loadInfo();
        }

        private void loadInfo()
        {
            mtdStard();
        }
        protected void mtdStard()
        {
            pagIndexTratamientos = 0;
            string strErrMsg = String.Empty;
            if (!mtdLoadPorcentajesCalificacion(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");

        }
        protected void mtdResetFields()
        {
            BodyFormPC.Visible = false;
            BodyGridPC.Visible = true;

            txtId.Text = string.Empty;
            ddlVariables.Items.Clear();
            txtValorPorcentaje.Text = string.Empty;
            ddlVariables.Items.Clear();
        }
        private bool mtdLoadPorcentajesCalificacion(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOPorcentajeCalificacion objPorcentajes = new clsDTOPorcentajeCalificacion();
            List<clsDTOPorcentajeCalificacion> lstPorcentajes = new List<clsDTOPorcentajeCalificacion>();
            clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();
            #endregion Vars
            lstPorcentajes = cPorcentaje.mtdConsultarPorcentajes(ref lstPorcentajes, ref strErrMsg);

            if (lstPorcentajes != null)
            {
                mtdLoadPorcentajesCalificacion();
                GVporcentajeCalificacion.DataSource = lstPorcentajes;
                GVporcentajeCalificacion.PageIndex = pagIndexTratamientos;
                GVporcentajeCalificacion.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "No hay variables registradas";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadPorcentajesCalificacion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdPorcentajeCalificarControl", typeof(string));
            grid.Columns.Add("strNombrePorcentajeCalificarControl", typeof(string));
            grid.Columns.Add("intValorPorcentajeCalificarControl", typeof(string));

            GVporcentajeCalificacion.DataSource = grid;
            GVporcentajeCalificacion.DataBind();
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            clsDTOPorcentajeCalificacion objPorcentaje = new clsDTOPorcentajeCalificacion();
            clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();
            string strErrMsg = string.Empty;
            int valorTotalPorcentaje = 0;
            int cantVariable = 0;
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    valorTotalPorcentaje = cPorcentaje.mtdCantidadTotalPeso(ref strErrMsg);
                    valorTotalPorcentaje = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtValorPorcentaje.Text)) + valorTotalPorcentaje;
                    cantVariable = cPorcentaje.mtdValidarValorPorcentaje(ref strErrMsg, Sanitizer.GetSafeHtmlFragment(ddlVariables.SelectedItem.Text));
                    if (cantVariable == 0)
                    {
                        if (valorTotalPorcentaje <= 100)
                        {
                            //cParametrizacionRiesgos.modificarRegistroPorcentajeCalificacion(TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox4.Text.Trim(), TextBox5.Text.Trim());
                            bool booResult = false;
                            objPorcentaje.strNombrePorcentajeCalificarControl = Sanitizer.GetSafeHtmlFragment(ddlVariables.SelectedItem.Text);
                            objPorcentaje.intValorPorcentajeCalificarControl = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtValorPorcentaje.Text));


                            booResult = cPorcentaje.mtdInsertarPorcentajeCalificacion(objPorcentaje, ref strErrMsg);
                            if (booResult == true)
                            {
                                loadInfo();
                                strErrMsg = "Porcentaje de Calificaciín registrado exitosamente";
                                omb.ShowMessage(strErrMsg, 3, "Atención");
                                mtdResetFields();
                            }
                            else
                            {
                                strErrMsg = "Error al registrar el Porcentaje de Calificación del Control";
                                omb.ShowMessage(strErrMsg, 2, "Atención");
                            }
                        }
                        else
                        {
                            strErrMsg = "Error: los valores de los porcentajes son superiores a 100";
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                        }
                    }
                    else
                    {
                        strErrMsg = "Error: la variable ya fue registrada";
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    }
                }
                   
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar la información, los valores deben ser números enteros. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridPC.Visible = false;
            BodyFormPC.Visible = true;
            ImageButton1.Visible = false;
            ImageButton2.Visible = true;
            LoadVariables();
            rfvVariabl.Enabled = true;
            rfvVariablUp.Enabled = false;
        }

        private void LoadVariables()
        {
            bool booResult = false;
            string strErrMsg = string.Empty;
            //List<clsDTOVariableCalificacionControl> lstVariables = new List<clsDTOVariableCalificacionControl>();
            //clsBLLVariableCalificacionControl cVariable = new clsBLLVariableCalificacionControl();
            clsDALVariableCalificacionControl var = new clsDALVariableCalificacionControl();
            //lstVariables = cVariable.mtdConsultarVariablesActivas(ref lstVariables, ref strErrMsg);
            DataTable dtInfo = new DataTable();
            booResult = var.mtdConsultarVariablesActivas(ref dtInfo, ref strErrMsg);
            if (booResult == true)
            {
                ddlVariables.Items.Insert(0, new ListItem("---", "---"));
                for (int iteracion = 0; iteracion < dtInfo.Rows.Count; iteracion++)
                {
                    ddlVariables.Items.Insert(iteracion + 1, new ListItem(dtInfo.Rows[iteracion]["DescripcionVariable"].ToString().Trim(), dtInfo.Rows[iteracion]["IdVariableCalificacionControl"].ToString()));
                }
            }
            else
            {
                omb.ShowMessage("No hay variables registradas", 2, "Atención");
            }
        }
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
        }

        protected void GVporcentajeCalificacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGridPC = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGridPC);
                    ImageButton2.Visible = false;
                    ImageButton1.Visible = true;
                    BodyFormPC.Visible = true;
                    BodyGridPC.Visible = false;
                    rfvVariabl.Enabled = false;
                    rfvVariablUp.Enabled = true;
                    break;
            }
        }
        protected void mtdShowUpdate(int Rowgrid)
        {
            GridViewRow row = GVporcentajeCalificacion.Rows[Rowgrid];
            txtId.Text = row.Cells[0].Text;
            LoadVariables();
            ddlVariables.SelectedItem.Text = ((Label)row.FindControl("strNombrePorcentajeCalificarControl")).Text;
            txtValorPorcentaje.Text = row.Cells[2].Text;
            

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = String.Empty;
            if (mtdUpdatePorcentaje(ref strErrMsg) == true)
            {
                omb.ShowMessage(strErrMsg, 3, "Atención");
                mtdResetFields();
                mtdStard();
            }
            else
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        private bool mtdUpdatePorcentaje(ref string strErrMsg)
        {
            bool booResult = false;
            clsDTOPorcentajeCalificacion objPorcentaje = new clsDTOPorcentajeCalificacion();
            objPorcentaje.intIdPorcentajeCalificarControl = Convert.ToInt32(txtId.Text);
            objPorcentaje.strNombrePorcentajeCalificarControl = Sanitizer.GetSafeHtmlFragment(ddlVariables.SelectedItem.Text);
            objPorcentaje.intValorPorcentajeCalificarControl = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtValorPorcentaje.Text));
            clsBLLPorcentajeCalificacion cPorcentaje = new clsBLLPorcentajeCalificacion();
            int cantVariable = 0;
            int valorTotalPorcentaje = 0;
            valorTotalPorcentaje = cPorcentaje.mtdCantidadTotalPesoUp(ref strErrMsg, Convert.ToInt32(txtId.Text));
            valorTotalPorcentaje = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtValorPorcentaje.Text)) + valorTotalPorcentaje;
            /*cantVariable = cPorcentaje.mtdValidarValorPorcentaje(ref strErrMsg, Sanitizer.GetSafeHtmlFragment(ddlVariables.SelectedItem.Text));
            if (cantVariable == 0)
            {*/
                if (valorTotalPorcentaje <= 100)
                {
                    
                        booResult = cPorcentaje.mtdUpdatePorcentaje(objPorcentaje, ref strErrMsg);
                        if (booResult == true)
                            strErrMsg = "Porcentaje de Calificación actualizado  exitosamente";
                        else
                            strErrMsg = "Error al actualizar el Porcentaje de Calificación";
                    }else
                        {
                        strErrMsg = "Error: los valores de los porcentajes son superiores a 100";
                        //omb.ShowMessage(strErrMsg, 1, "Atención");
                    }
                /*}
                else
                {
                    strErrMsg = "Error: la variable ya fue registrada";
                    //omb.ShowMessage(strErrMsg, 1, "Atención");
                }*/
            
                return booResult;
        }

        protected void GVporcentajeCalificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndexTratamientos = e.NewPageIndex;
            /*GVporcentajeCalificacion.PageIndex = pagIndexTratamientos;
            GVporcentajeCalificacion.DataBind();*/
            string strErrMsg = "";
            if (!mtdLoadPorcentajesCalificacion(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
    }
}