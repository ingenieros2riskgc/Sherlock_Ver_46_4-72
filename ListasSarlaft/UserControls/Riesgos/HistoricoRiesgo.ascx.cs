using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;
using System.IO;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class HistoricoRiesgo : System.Web.UI.UserControl
    {
        cHistoricoRiesgo cHistoricoRiesgo = new cHistoricoRiesgo();
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "5018";
        //Modificación Realizada Heber Jessid Correal 04/04/2018 Se crea propiedad para guardar el valor de InfoGridHistoricoRiesgo dado que se debe borrar su valor
        public static DataTable dtInfoGridHistoricoRiesgoCopy { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                inicializarValores();
                loadDDLCadenaValor();
                LoadDropdownsDependientes();
                loadDDLRiesgoGlobal();
                loadDDLClasGeneral();
                mtdLoadDDLEmpresa();
            }
        }

        #region Loads
        private void mtdLoadDDLEmpresa()
        {
            DataTable dtInfo = new DataTable();
            cRiesgo cRiesgo = new cRiesgo();

            try
            {
                dtInfo = cRiesgo.mtdLoadEmpresa(true);
                DropDownList1.Items.Insert(0, new ListItem("---", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Empresas. " + ex.Message);
            }
        }
        #endregion

        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList52.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["CadenaValor"].ToString().Trim(), dtInfo.Rows[i]["CadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cadena de valor. " + ex.Message);
            }
        }

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se sobrecarga metodo loadDDLMacroproceso
        private void loadDDLMacroproceso(string CadenaValor)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLMacroproceso(CadenaValor);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList53.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Macroproceso"].ToString().Trim(), dtInfo.Rows[i]["Macroproceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
            }
        }

        private void loadDDLMacroproceso()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLMacroproceso();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList53.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Macroproceso"].ToString().Trim(), dtInfo.Rows[i]["Macroproceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
            }
        }

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se sobrecarga metodo loadDDLProceso
        private void loadDDLProceso(string MacroProceso)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLProceso(MacroProceso);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList54.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Proceso"].ToString().Trim(), dtInfo.Rows[i]["Proceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
            }
        }

        private void loadDDLProceso()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLProceso();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList54.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Proceso"].ToString().Trim(), dtInfo.Rows[i]["Proceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
            }
        }

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se sobrecarga metodo loadDDLProceso
        private void loadDDLSubproceso(string Proceso)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLSubproceso(Proceso);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList22.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Subproceso"].ToString().Trim(), dtInfo.Rows[i]["Subproceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar subproceso. " + ex.Message);
            }
        }

        private void loadDDLSubproceso()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLSubproceso();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList22.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Subproceso"].ToString().Trim(), dtInfo.Rows[i]["Subproceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar subproceso. " + ex.Message);
            }
        }

        private void loadDDLRiesgoGlobal()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLRiesgoGlobal();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList56.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["ClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["ClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar riesgo global. " + ex.Message);
            }
        }

        private void loadDDLClasGeneral()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadDDLClasGeneral();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList57.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["ClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["ClasificacionGeneralRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación general. " + ex.Message);
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridHistoricoRiesgo = 0;
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
            LoadDropdownsDependientes();
        }

        private void resetValues()
        {
            TextBox11.Text = string.Empty;
            TextBox17.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox10.Text = string.Empty;
            DropDownList52.SelectedIndex = 0;
            DropDownList56.SelectedIndex = 0;
            DropDownList57.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            ReporteRiesgosControles.Visible = false;
            ////Modificación Realizada Heber Jessid Correal 04/04/2018 Se limpia la propiedad InfoGridHistoricoRiesgo para evitar excepciones.
            InfoGridHistoricoRiesgo.Clear();
            infoGridHistoricoRiesgo.AcceptChanges();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim())) &&
                    string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim())) &&
                    string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim())) &&
                    string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim())) &&
                    DropDownList52.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList53.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList54.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList22.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList56.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList57.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList2.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList3.SelectedValue.ToString().Trim() == "---" &&
                    DropDownList1.SelectedValue.ToString().Trim() == "---")
                {
                    Mensaje("Debe ingresar por lo menos un parámetro de consulta.");
                }
                else
                {
                    inicializarValores();
                    loadGridHistoricoRiesgo();
                    loadInfoHistoricoRiesgo();
                    resetValues();
                    LoadDropdownsDependientes();
                    ReporteRiesgosControles.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        private void loadInfoHistoricoRiesgo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cHistoricoRiesgo.loadInfoHistoricoRiesgo(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox10.Text.Trim()),
                    DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                    DropDownList22.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                    DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList1.SelectedItem.ToString().Trim());
                if (dtInfo.Rows.Count > 0)
                {
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        InfoGridHistoricoRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["FechaHistorico"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["TipoRiesgoOperativo"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Actividad"].ToString().Trim(),

                                                                   dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),

                                                                   dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["ResponsableControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["FechaRegistroControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombrePeriodicidad"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreClaseControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreTipoControl"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreResponsableExperiencia"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreDocumentacion"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreResponsabilidad"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreEscala"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreMitiga"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Empresa"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["Justificacion"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreVariable"].ToString().Trim(),
                                                                   dtInfo.Rows[rows]["NombreCategoria"].ToString().Trim()
                                                                   });
                    }
                    GridView2.PageIndex = PagIndexInfoGridHistoricoRiesgo;
                    GridView2.DataSource = InfoGridHistoricoRiesgo;
                    GridView2.DataBind();
                    //Modificación Realizada Heber Jessid Correal 04/04/2018 Se copia en dtInfoGridHistoricoRiesgoCopy la estructura y los datos de InfoGridHistoricoRiesgo ya que se borra.
                    dtInfoGridHistoricoRiesgoCopy = infoGridHistoricoRiesgo.Copy();
                }
                else
                {
                    loadGridHistoricoRiesgo();
                    Mensaje("No existen registros asociados a los parámetros de consulta.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }

        }

        private void loadGridHistoricoRiesgo()
        {
            try
            {
                DataTable grid = new DataTable();
                grid.Columns.Add("FechaHistorico", typeof(string));
                grid.Columns.Add("CodigoRiesgo", typeof(string));
                grid.Columns.Add("NombreRiesgo", typeof(string));
                grid.Columns.Add("ResponsableRiesgo", typeof(string));
                grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
                grid.Columns.Add("ClasificacionRiesgo", typeof(string));
                grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
                grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
                grid.Columns.Add("TipoRiesgoOperativo", typeof(string));
                grid.Columns.Add("CadenaValor", typeof(string));
                grid.Columns.Add("Macroproceso", typeof(string));
                grid.Columns.Add("Proceso", typeof(string));
                grid.Columns.Add("Subproceso", typeof(string));
                grid.Columns.Add("Actividad", typeof(string));

                grid.Columns.Add("FrecuenciaInherente", typeof(string));
                grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
                grid.Columns.Add("ImpactoInherente", typeof(string));
                grid.Columns.Add("CodigoImpactoInherente", typeof(string));
                grid.Columns.Add("RiesgoInherente", typeof(string));
                grid.Columns.Add("CodigoRiesgoInherente", typeof(string));
                grid.Columns.Add("FrecuenciaResidual", typeof(string));
                grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
                grid.Columns.Add("ImpactoResidual", typeof(string));
                grid.Columns.Add("CodigoImpactoResidual", typeof(string));
                grid.Columns.Add("RiesgoResidual", typeof(string));
                grid.Columns.Add("CodigoRiesgoResidual", typeof(string));

                grid.Columns.Add("CodigoControl", typeof(string));
                grid.Columns.Add("NombreControl", typeof(string));
                grid.Columns.Add("ResponsableControl", typeof(string));
                grid.Columns.Add("FechaRegistroControl", typeof(string));
                grid.Columns.Add("NombrePeriodicidad", typeof(string));
                grid.Columns.Add("NombreTest", typeof(string));
                grid.Columns.Add("NombreClaseControl", typeof(string));
                grid.Columns.Add("NombreTipoControl", typeof(string));
                grid.Columns.Add("NombreResponsableExperiencia", typeof(string));
                grid.Columns.Add("NombreDocumentacion", typeof(string));
                grid.Columns.Add("NombreResponsabilidad", typeof(string));
                grid.Columns.Add("NombreEscala", typeof(string));
                grid.Columns.Add("NombreMitiga", typeof(string));
                grid.Columns.Add("Empresa", typeof(string));
                grid.Columns.Add("Justificacion", typeof(string));
                grid.Columns.Add("NombreVariable", typeof(string));
                grid.Columns.Add("NombreCategoria", typeof(string));
                InfoGridHistoricoRiesgo = grid;
                GridView2.DataSource = InfoGridHistoricoRiesgo;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }

        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Propierties
        private DataTable infoGridHistoricoRiesgo;
        private DataTable InfoGridHistoricoRiesgo
        {
            get
            {
                infoGridHistoricoRiesgo = (DataTable)ViewState["infoGridHistoricoRiesgo"];
                return infoGridHistoricoRiesgo;
            }
            set
            {
                infoGridHistoricoRiesgo = value;
                ViewState["infoGridHistoricoRiesgo"] = infoGridHistoricoRiesgo;
            }
        }

        private int rowGridHistoricoRiesgo;
        private int RowGridHistoricoRiesgo
        {
            get
            {
                rowGridHistoricoRiesgo = (int)ViewState["rowGridHistoricoRiesgo"];
                return rowGridHistoricoRiesgo;
            }
            set
            {
                rowGridHistoricoRiesgo = value;
                ViewState["rowGridHistoricoRiesgo"] = rowGridHistoricoRiesgo;
            }
        }

        private int pagIndexInfoGridHistoricoRiesgo;
        private int PagIndexInfoGridHistoricoRiesgo
        {
            get
            {
                pagIndexInfoGridHistoricoRiesgo = (int)ViewState["pagIndexInfoGridHistoricoRiesgo"];
                return pagIndexInfoGridHistoricoRiesgo;
            }
            set
            {
                pagIndexInfoGridHistoricoRiesgo = value;
                ViewState["pagIndexInfoGridHistoricoRiesgo"] = pagIndexInfoGridHistoricoRiesgo;
            }
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Modificación Realizada Heber Jessid Correal 04/04/2018 se cambia el parametro Datatable por dtInfoGridHistoricoRiesgoCopy ya que a la propiedad original se le debe modificar su valor 
            exportExcel(dtInfoGridHistoricoRiesgoCopy, Response, "Reporte Historico de riesgos");
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename)
        {
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(dt, "Data");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (System.IO.MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridHistoricoRiesgo = e.NewPageIndex;
            GridView2.PageIndex = PagIndexInfoGridHistoricoRiesgo;
            //Modificación Realizada Heber Jessid Correal 04/04/2018 se cambia el datasource por dtInfoGridHistoricoRiesgoCopy ya que a la propiedad original se le debe modificar su valor 
            GridView2.DataSource = dtInfoGridHistoricoRiesgoCopy;
            GridView2.DataBind();
        }

        //Modificación Realizada Heber Jessid Correal 02/04/2018 Se genera evento SelectedIndexChanged para filtrar las opciones del DropDownList MacroProceso
        protected void DropDownList52_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList52.SelectedItem.Value != "---")
            {
                LimpiarDropDown(DropDownList53);
                LimpiarDropDown(DropDownList54);
                LimpiarDropDown(DropDownList22);
                loadDDLMacroproceso(DropDownList52.SelectedItem.Value);
            }
            else
            {
                LimpiarDropDown(DropDownList53);
                LimpiarDropDown(DropDownList54);
                LimpiarDropDown(DropDownList22);
                loadDDLMacroproceso();
            }
        }

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se genera evento SelectedIndexChanged para filtrar las opciones del DropDownList Proceso
        protected void DropDownList53_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList53.SelectedItem.Value != "---")
            {
                LimpiarDropDown(DropDownList54);
                LimpiarDropDown(DropDownList22);
                loadDDLProceso(DropDownList53.SelectedItem.Value);
            }
            else
            {
                LimpiarDropDown(DropDownList54);
                LimpiarDropDown(DropDownList22);
                loadDDLProceso();
            }

        }

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Se genera evento SelectedIndexChanged para filtrar las opciones del DropDownList Subproceso
        protected void DropDownList54_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList54.SelectedItem.Value != "---")
            {
                LimpiarDropDown(DropDownList22);
                loadDDLSubproceso(DropDownList54.SelectedItem.Value);
            }
            else
            {
                LimpiarDropDown(DropDownList22);
                loadDDLSubproceso();
            }
        }

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Metodo creado para limpiar la opciones cuando se cambie el(los) DropDownList del que hay dependencia 
        public void LimpiarDropDown(DropDownList DropDown)
        {
            DropDown.Items.Clear();
            DropDown.Items.Add("---");
        }

        //Modificación Realizada Heber Jessid Correal 03/04/2018 Metodo creado para cargar todas las opciones de los DropdownList despues de ejecutar el informe.
        public void LoadDropdownsDependientes()
        {
            LimpiarDropDown(DropDownList53);
            LimpiarDropDown(DropDownList54);
            LimpiarDropDown(DropDownList22);
            loadDDLMacroproceso();
            loadDDLProceso();
            loadDDLSubproceso();
        }
    }
}