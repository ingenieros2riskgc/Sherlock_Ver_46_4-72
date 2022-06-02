using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;
using System.Text.RegularExpressions;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class ConsolidadoRiesgos : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "5016";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                loadDDLCadenaValor();
                loadDDLClasificacion();
                mtdCargarAreas();
                inicializarValores();
                mtdLoadDDLEmpresa();
                loadDDLPlanes();
            }
        }
        #region Propierties
        private int rowGridRiesgos;
        private int RowGridRiesgos
        {
            get
            {
                rowGridRiesgos = (int)ViewState["rowGridRiesgos"];
                return rowGridRiesgos;
            }
            set
            {
                rowGridRiesgos = value;
                ViewState["rowGridRiesgos"] = rowGridRiesgos;
            }
        }

        private DataTable infoGridRiesgos;
        private DataTable InfoGridRiesgos
        {
            get
            {
                infoGridRiesgos = (DataTable)ViewState["infoGridRiesgos"];
                return infoGridRiesgos;
            }
            set
            {
                infoGridRiesgos = value;
                ViewState["infoGridRiesgos"] = infoGridRiesgos;
            }
        }

        private DataTable infoGridControlesRiesgo;
        private DataTable InfoGridControlesRiesgo
        {
            get
            {
                infoGridControlesRiesgo = (DataTable)ViewState["infoGridControlesRiesgo"];
                return infoGridControlesRiesgo;
            }
            set
            {
                infoGridControlesRiesgo = value;
                ViewState["infoGridControlesRiesgo"] = infoGridControlesRiesgo;
            }
        }

        private DataTable infoGridObjetivoRiesgo;
        private DataTable InfoGridObjetivoRiesgo
        {
            get
            {
                infoGridObjetivoRiesgo = (DataTable)ViewState["infoGridObjetivoRiesgo"];
                return infoGridObjetivoRiesgo;
            }
            set
            {
                infoGridObjetivoRiesgo = value;
                ViewState["infoGridObjetivoRiesgo"] = infoGridObjetivoRiesgo;
            }
        }

        private DataTable infoGridPlanAccionRiesgo;
        private DataTable InfoGridPlanAccionRiesgo
        {
            get
            {
                infoGridPlanAccionRiesgo = (DataTable)ViewState["infoGridPlanAccionRiesgo"];
                return infoGridPlanAccionRiesgo;
            }
            set
            {
                infoGridPlanAccionRiesgo = value;
                ViewState["infoGridPlanAccionRiesgo"] = infoGridPlanAccionRiesgo;
            }
        }

        private DataTable infoGridResponsableRiesgo;
        private DataTable InfoGridResponsableRiesgo
        {
            get
            {
                infoGridResponsableRiesgo = (DataTable)ViewState["infoGridResponsableRiesgo"];
                return infoGridResponsableRiesgo;
            }
            set
            {
                infoGridResponsableRiesgo = value;
                ViewState["infoGridResponsableRiesgo"] = infoGridResponsableRiesgo;
            }
        }

        private int pagIndexInfoGridRiesgos;
        private int PagIndexInfoGridRiesgos
        {
            get
            {
                pagIndexInfoGridRiesgos = (int)ViewState["pagIndexInfoGridRiesgos"];
                return pagIndexInfoGridRiesgos;
            }
            set
            {
                pagIndexInfoGridRiesgos = value;
                ViewState["pagIndexInfoGridRiesgos"] = pagIndexInfoGridRiesgos;
            }
        }

        private DataTable infoGridRiesgoResidualvsSistema;
        private DataTable InfoGridRiesgoResidualvsSistema
        {
            get
            {
                infoGridRiesgoResidualvsSistema = (DataTable)ViewState["infoGridRiesgoResidualvsSistema"];
                return infoGridRiesgoResidualvsSistema;
            }
            set
            {
                infoGridRiesgoResidualvsSistema = value;
                ViewState["infoGridRiesgoResidualvsSistema"] = infoGridRiesgoResidualvsSistema;
            }
        }
        #endregion
        #region Loads
        private void mtdLoadDDLEmpresa()
        {
            DataTable dtInfo = new DataTable();
            cRiesgo cRiesgo = new cRiesgo();

            try
            {
                dtInfo = cRiesgo.mtdLoadEmpresa(true);
                ddlEmpresa.Items.Insert(0, new ListItem("---", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlEmpresa.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Empresas. " + ex.Message);
            }
        }
        #endregion

        private void inicializarValores()
        {
            PagIndexInfoGridRiesgos = 0;
        }

        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList52.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cadena valor. " + ex.Message);
            }
        }

        private void loadDDLClasificacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList56.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación riesgo. " + ex.Message);
            }
        }

        protected void DropDownList52_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "---"));
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList52.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList52.SelectedValue.ToString().Trim(), 2);
            }
        }

        private void loadDDLMacroproceso(String IdCadenaValor, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLMacroproceso(IdCadenaValor);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList53.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
            }
        }

        protected void DropDownList53_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList53.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList53.SelectedValue.ToString().Trim(), 2);
            }
        }

        private void loadDDLProceso(String IdMacroproceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLProceso(IdMacroproceso);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList54.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
            }
        }

        protected void DropDownList54_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList54.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList54.SelectedValue.ToString().Trim(), 2);
            }
        }

        private void loadDDLSubProceso(String IdProceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubProceso(IdProceso);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList7.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar subproceso. " + ex.Message);
            }
        }

        protected void DropDownList56_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "---"));
            DropDownList58.Items.Clear();
            DropDownList58.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList56.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionGeneral(DropDownList56.SelectedValue.ToString().Trim(), 2);
            }

            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));

            if (DropDownList56.SelectedValue.ToString().Trim() == "2")
            {
                mtdLoadFactorRiesgo(2);
            }

        }

        private void loadDDLClasificacionGeneral(String IdClasificacionRiesgo, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionGeneral(IdClasificacionRiesgo);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList57.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionGeneralRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación general. " + ex.Message);
            }
        }

        protected void DropDownList57_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList58.Items.Clear();
            DropDownList58.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList57.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionParticular(DropDownList57.SelectedValue.ToString().Trim(), 2);
            }
        }

        private void loadDDLClasificacionParticular(String IdClasificacionGeneralRiesgo, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionParticular(IdClasificacionGeneralRiesgo);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList58.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionParticularRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionParticularRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación particular. " + ex.Message);
            }
        }
        private void loadDDLPlanes()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLPlanes();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLPlanEstrategico.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Nombre"].ToString().Trim(), dtInfo.Rows[i]["IdPlan"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Planes Estratégicos. " + ex.Message);
            }
        }
        private void loadDDLObjetivos()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                //Camilo 12/02/2014
                //dtInfo = cRiesgo.loadDDLObjetivos();
                dtInfo = cRiesgo.loadDDLObjetivos(DDLPlanEstrategico.SelectedValue.ToString());
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLObjetivoEstrategico.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreObjetivos"].ToString().Trim(), dtInfo.Rows[i]["IdObjetivos"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar objetivos. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        

        private void loadGridRiesgos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("IdProbabilidad", typeof(string));
            grid.Columns.Add("IdImpacto", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("NombreClasificacionRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("NombreRiesgoInherente", typeof(string));
            grid.Columns.Add("Color", typeof(string));
            grid.Columns.Add("NombreProbabilidad", typeof(string));
            grid.Columns.Add("NombreImpacto", typeof(string));
            InfoGridRiesgos = grid;
            GridView1.DataSource = InfoGridRiesgos;
            GridView1.DataBind();
        }

        private void loadInfoRiesgos(String IdProbabilidad, String IdImpacto)
        {
            #region Variables
            DataTable dtInfo = new DataTable();
            string strInfoAreas = string.Empty, strComma = string.Empty;
            #endregion Variables

            #region Captura de Areas
            if (ListBox2.Items.Count > 0)
            {
                foreach (ListItem liArea in ListBox2.Items)
                {
                    strInfoAreas += strComma + liArea.Value.ToString().Trim();
                    strComma = ",";
                }
            }
            #endregion Captura de Areas

            switch (DropDownList1.SelectedValue.ToString().Trim())
            {
                case "1":
                    dtInfo = cRiesgo.loadInfoRiesgosMapaInherente(IdProbabilidad, IdImpacto,
                        DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                        DropDownList54.SelectedValue.ToString().Trim(), DropDownList7.SelectedValue.ToString().Trim(),
                        DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                        DropDownList58.SelectedValue.ToString().Trim(), strInfoAreas, Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()),
                        DropDownList2.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedValue.ToString().Trim(),
                        ddlEmpresa.SelectedItem.Text.ToString().Trim(), DDLObjetivoEstrategico.SelectedValue.ToString().Trim());
                    break;
                case "2":
                    dtInfo = cRiesgo.loadInfoRiesgosMapaResidual(IdProbabilidad, IdImpacto,
                        DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(), 
                        DropDownList54.SelectedValue.ToString().Trim(), DropDownList7.SelectedValue.ToString().Trim(), 
                        DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(), 
                        DropDownList58.SelectedValue.ToString().Trim(), strInfoAreas, Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), 
                        DropDownList2.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedValue.ToString().Trim(), 
                        ddlEmpresa.SelectedItem.Text.ToString().Trim(), DDLObjetivoEstrategico.SelectedValue.ToString().Trim());
                    break;
            }
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para Cargue de info
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdImpacto"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreRiesgoInherente"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Color"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreProbabilidad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreImpacto"].ToString().Trim()                                                           
                                                          });
                }
                #endregion Recorrido para Cargue de info

                GridView1.PageIndex = PagIndexInfoGridRiesgos;
                GridView1.DataSource = InfoGridRiesgos;
                GridView1.DataBind();
                trRiesgos.Visible = true;
                trRiesgovsSistemaTitulo.Visible = false;
                trRiesgovsSistema.Visible = false;
                Label14.Text = InfoGridRiesgos.Rows[0]["NombreRiesgoInherente"].ToString().Trim();
                Panel1.BackColor = System.Drawing.Color.FromName(InfoGridRiesgos.Rows[0]["Color"].ToString().Trim());
                Label6.Text = InfoGridRiesgos.Rows[0]["NombreProbabilidad"].ToString().Trim();
                Label4.Text = InfoGridRiesgos.Rows[0]["NombreImpacto"].ToString().Trim();
            }
        }
        private void loadInfoRiesgosResidualvsSistema(string C1, string C2)
        {
            #region Variables
            DataTable dtInfo = new DataTable();
            dtInfo.Columns.Add("IdClasificacionRiesgo");
            dtInfo.Columns.Add("NombreClasificacionRiesgo");
            string strInfoAreas = string.Empty, strComma = string.Empty;
            String promedioProbabilidad = "";
            String promedioImpacto = "";
            
            LinkButton coordenadaAnterior = new LinkButton();
            coordenadaAnterior = (LinkButton)FindControl("LBt" + C1 + C2);
            Panel scoordenadaPanel = new Panel();
            int iteracion = 0;
            Regex rex = new Regex("^[0-9]*$");
            #endregion Variables



            if (InfoGridRiesgoResidualvsSistema.Rows.Count > 0)
            {
                for (int rows = 0; rows < InfoGridRiesgoResidualvsSistema.Rows.Count; rows++)
                {
                    LinkButton coordenada = new LinkButton();
                    promedioProbabilidad = Math.Round((Convert.ToDouble(InfoGridRiesgoResidualvsSistema.Rows[rows]["SumatoriaProbabilidadResidual"].ToString().Trim())) / (Convert.ToDouble(InfoGridRiesgoResidualvsSistema.Rows[rows]["NumeroRegistros"]))).ToString().Trim();
                    promedioImpacto = Math.Round((Convert.ToDouble(InfoGridRiesgoResidualvsSistema.Rows[rows]["SumatoriaImpactoResidual"].ToString().Trim())) / (Convert.ToDouble(InfoGridRiesgoResidualvsSistema.Rows[rows]["NumeroRegistros"]))).ToString().Trim();
                    if(cRiesgo.IdProbabilidad(promedioProbabilidad) == C1 && cRiesgo.IdImpacto(promedioImpacto) == C2)
                        coordenada = (LinkButton)FindControl("LBt" + cRiesgo.IdProbabilidad(promedioProbabilidad) + cRiesgo.IdImpacto(promedioImpacto));
                    
                    if (coordenada != null)
                    {
                        if (rex.IsMatch(coordenada.Text) && coordenada.Text != "")
                        {
                            DataRow rowInfo = dtInfo.NewRow();
                            rowInfo["IdClasificacionRiesgo"] = InfoGridRiesgoResidualvsSistema.Rows[rows]["IdClasificacionRiesgo"].ToString().Trim();
                            rowInfo["NombreClasificacionRiesgo"] = InfoGridRiesgoResidualvsSistema.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim();
                            dtInfo.Rows.Add(rowInfo);
                        }
                        
                    }
                }
                if(dtInfo.Rows.Count > 0)
                {
                    GVriesgovsSistema.DataSource = dtInfo;
                    GVriesgovsSistema.DataBind();
                    trRiesgovsSistema.Visible = true;
                    trRiesgovsSistemaTitulo.Visible = true;
                }else
                {
                    trRiesgovsSistema.Visible = false;
                    trRiesgovsSistemaTitulo.Visible = false;
                }
            }
        }
        protected void coordenadaRiesgo(object sender, CommandEventArgs e)
        {
            if (DropDownList1.SelectedValue.ToString().Trim() != "3" && DropDownList1.SelectedValue.ToString().Trim() != "4" && DropDownList1.SelectedValue.ToString().Trim() != "5")
            {
                #region Variables
                char[] delimiter = { ',' };
                String[] coordenada = e.CommandArgument.ToString().Trim().Split(delimiter);
                #endregion Variables

                resetRiesgos();
                resetDetalleRiesgo();
                
                loadGridRiesgos();
                loadInfoRiesgos(coordenada[0].ToString().Trim(), coordenada[1].ToString().Trim());
            }
            if(DropDownList1.SelectedValue.ToString().Trim() == "5")
            {
                #region Variables
                char[] delimiter = { ',' };
                String[] coordenada = e.CommandArgument.ToString().Trim().Split(delimiter);
                #endregion Variables
                loadGridRiesgos();
                loadInfoRiesgosResidualvsSistema(coordenada[0].ToString().Trim(), coordenada[1].ToString().Trim());
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                inicializarValores();
                resetValuesMatriz();
                resetRiesgos();
                resetDetalleRiesgo();
                consolidadoRiesgos();
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la busqueda. " + ex.Message);
            }
        }

        private void resetValuesConsulta()
        {
            DropDownList1.SelectedIndex = 0;
            DropDownList52.SelectedIndex = 0;
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            DropDownList7.Items.Clear();
            DropDownList7.Items.Insert(0, new ListItem("---", "---"));
            DropDownList56.SelectedIndex = 0;
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "---"));
            DropDownList58.Items.Clear();
            DropDownList58.Items.Insert(0, new ListItem("---", "---"));
            ddlEmpresa.SelectedIndex = 0;
            DDLPlanEstrategico.Items.Clear();
            DDLPlanEstrategico.Items.Insert(0, new ListItem("---", "---"));
            DDLObjetivoEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Insert(0, new ListItem("---", "---"));
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
        }

        private void resetValuesMatriz()
        {
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    LinkButton coordenada = (LinkButton)FindControl("LBt" + i.ToString() + j.ToString());
                    coordenada.Text = "";
                }
            }
            trMapaRiesgos.Visible = false;
        }

        private void resetRiesgos()
        {
            Label14.Text = "";
            Panel1.BackColor = System.Drawing.Color.FromName("Transparent");
            Label6.Text = "";
            Label4.Text = "";
            trRiesgos.Visible = false;
            trRiesgovsSistema.Visible = false;
            trRiesgovsSistemaTitulo.Visible = false;
        }

        private void resetDetalleRiesgo()
        {
            trDetalleRiesgo.Visible = false;
        }

        private void consolidadoRiesgos()
        {
            #region Variables
            DataTable dtInfo = new DataTable();
            DataTable dtColor = new DataTable();
            String promedioProbabilidad = "";
            String promedioImpacto = "";
            LinkButton coordenada = new LinkButton();
            Panel scoordenadaPanel = new Panel();
            string strInfoAreas = string.Empty, strComma = string.Empty;
            #endregion Variables

            #region Pintar Color Panel

            dtColor = cRiesgo.mtdGetColoresRiesgoInherente();
            //Pintar color de panel
            if (dtColor.Rows.Count > 0)
            {
                for (int intRows = 0; intRows < dtColor.Rows.Count; intRows++)
                {
                    scoordenadaPanel = (Panel)FindControl("Panel" + dtColor.Rows[intRows]["IdProbabilidad"].ToString().Trim() + dtColor.Rows[intRows]["IdImpacto"].ToString().Trim());
                    string strColor = dtColor.Rows[intRows]["Color"].ToString().Trim();
                    scoordenadaPanel.BackColor = System.Drawing.Color.FromName(strColor);
                }
            }
            #endregion

            #region Captura de Areas
            if (ListBox2.Items.Count > 0)
            {
                foreach (ListItem liArea in ListBox2.Items)
                {
                    strInfoAreas += strComma + liArea.Value.ToString().Trim();
                    strComma = ",";
                }
            }
            #endregion Captura de Areas

            switch (DropDownList1.SelectedValue.ToString().Trim())
            {
                case "1":
                    #region Riesgo Inherente
                    dtInfo = cRiesgo.consultaRiesgoInherente(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                        DropDownList54.SelectedValue.ToString().Trim(), DropDownList7.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(),
                        DropDownList57.SelectedValue.ToString().Trim(), DropDownList58.SelectedValue.ToString().Trim(), strInfoAreas, Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()),
                        DropDownList2.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedItem.Text.ToString().Trim(),
                        DDLObjetivoEstrategico.SelectedValue.ToString().Trim());

                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                        {
                            coordenada = (LinkButton)FindControl("LBt" + dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim() + dtInfo.Rows[rows]["IdImpacto"].ToString().Trim());

                            if (coordenada != null)
                            {
                                coordenada.Text = dtInfo.Rows[rows]["NumeroRegistros"].ToString().Trim();
                                coordenada.CssClass = "buttonClassNumber";
                            }
                        }
                    }
                    #endregion Riesgo Inherente

                    break;
                case "2":
                    #region Riesgo Residual
                    dtInfo = cRiesgo.consultaRiesgoResidual(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                        DropDownList54.SelectedValue.ToString().Trim(), DropDownList7.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(),
                        DropDownList57.SelectedValue.ToString().Trim(), DropDownList58.SelectedValue.ToString().Trim(), strInfoAreas, Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()),
                        DropDownList2.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedItem.Text.ToString().Trim(),
                        DDLObjetivoEstrategico.SelectedValue.ToString().Trim());

                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                        {
                            coordenada = (LinkButton)FindControl("LBt" + dtInfo.Rows[rows]["IdProbabilidadResidual"].ToString().Trim() + dtInfo.Rows[rows]["IdImpactoResidual"].ToString().Trim());

                            if (coordenada != null)
                            {
                                coordenada.Text = dtInfo.Rows[rows]["NumeroRegistros"].ToString().Trim();
                                coordenada.CssClass = "buttonClassNumber";
                            }
                            }
                    }
                    #endregion Riesgo Residual

                    break;
                case "3":
                    #region Perfil Riesgo Inherente
                    dtInfo = cRiesgo.perfilRiesgoInherente(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                        DropDownList54.SelectedValue.ToString().Trim(), DropDownList7.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(),
                        DropDownList57.SelectedValue.ToString().Trim(), DropDownList58.SelectedValue.ToString().Trim(), strInfoAreas, Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()),
                        DropDownList2.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedItem.Text.ToString().Trim(),
                        DDLObjetivoEstrategico.SelectedValue.ToString().Trim());

                    if (Convert.ToInt16(dtInfo.Rows[0]["NumeroRegistros"].ToString().Trim()) != 0)
                    {
                        promedioProbabilidad = Math.Round((Convert.ToDouble(dtInfo.Rows[0]["SumatoriaProbabilidad"].ToString().Trim())) / (Convert.ToDouble(dtInfo.Rows[0]["NumeroRegistros"]))).ToString().Trim();
                        promedioImpacto = Math.Round((Convert.ToDouble(dtInfo.Rows[0]["SumatoriaImpacto"].ToString().Trim())) / (Convert.ToDouble(dtInfo.Rows[0]["NumeroRegistros"]))).ToString().Trim();
                        coordenada = (LinkButton)FindControl("LBt" + cRiesgo.IdProbabilidad(promedioProbabilidad) + cRiesgo.IdImpacto(promedioImpacto));
                        
                        if (coordenada != null)
                        { coordenada.CssClass = "buttonClassNumber";
                            coordenada.Text = "PRI";
                        }
                            
                    }
                    #endregion Riesgo Inherente

                    break;
                case "4":
                    #region Perfil Riesgo Residual
                    dtInfo = cRiesgo.perfilRiesgoResidual(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                        DropDownList54.SelectedValue.ToString().Trim(), DropDownList7.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(),
                        DropDownList57.SelectedValue.ToString().Trim(), DropDownList58.SelectedValue.ToString().Trim(), strInfoAreas, Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()),
                        DropDownList2.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedItem.Text.ToString().Trim(),
                        DDLObjetivoEstrategico.SelectedValue.ToString().Trim());

                    if (Convert.ToInt16(dtInfo.Rows[0]["NumeroRegistros"].ToString().Trim()) != 0)
                    {
                        promedioProbabilidad = Math.Round((Convert.ToDouble(dtInfo.Rows[0]["SumatoriaProbabilidadResidual"].ToString().Trim())) / (Convert.ToDouble(dtInfo.Rows[0]["NumeroRegistros"]))).ToString().Trim();
                        promedioImpacto = Math.Round((Convert.ToDouble(dtInfo.Rows[0]["SumatoriaImpactoResidual"].ToString().Trim())) / (Convert.ToDouble(dtInfo.Rows[0]["NumeroRegistros"]))).ToString().Trim();
                        coordenada = (LinkButton)FindControl("LBt" + cRiesgo.IdProbabilidad(promedioProbabilidad) + cRiesgo.IdImpacto(promedioImpacto));
                        
                        if (coordenada != null)
                        { coordenada.CssClass = "buttonClassNumber";
                            coordenada.Text = "PRR";
                        }
                            
                    }
                    #endregion Riesgo Residual

                    break;
                case "5":
                    #region Perfil de Riesgo Residual por Sistema de Riesgo
                    dtInfo = cRiesgo.perfilRiesgoResidualvsSistemaRiesgo(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                        DropDownList54.SelectedValue.ToString().Trim(), DropDownList7.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(),
                        DropDownList57.SelectedValue.ToString().Trim(), DropDownList58.SelectedValue.ToString().Trim(), strInfoAreas, Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()),
                        DropDownList2.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedValue.ToString().Trim(), ddlEmpresa.SelectedItem.Text.ToString().Trim(),
                        DDLObjetivoEstrategico.SelectedValue.ToString().Trim());
                    if(dtInfo.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(dtInfo.Rows[0]["NumeroRegistros"].ToString().Trim()) != 0)
                        {
                            int iteracion = 1;
                            InfoGridRiesgoResidualvsSistema = dtInfo;
                            Regex rex = new Regex("^[0-9]*$");
                            for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                            {
                                promedioProbabilidad = Math.Round((Convert.ToDouble(dtInfo.Rows[rows]["SumatoriaProbabilidadResidual"].ToString().Trim())) / (Convert.ToDouble(dtInfo.Rows[rows]["NumeroRegistros"]))).ToString().Trim();
                                promedioImpacto = Math.Round((Convert.ToDouble(dtInfo.Rows[rows]["SumatoriaImpactoResidual"].ToString().Trim())) / (Convert.ToDouble(dtInfo.Rows[rows]["NumeroRegistros"]))).ToString().Trim();
                                coordenada = (LinkButton)FindControl("LBt" + cRiesgo.IdProbabilidad(promedioProbabilidad) + cRiesgo.IdImpacto(promedioImpacto));
                                if (coordenada != null)
                                {
                                    if (coordenada.Text == "")
                                    {
                                        iteracion = 1;
                                        coordenada.Text = dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim();
                                        coordenada.CssClass = "buttonClass";
                                    }
                                    else
                                    {
                                        if (rex.IsMatch(coordenada.Text) == true)
                                        {
                                            int NumCoordenada = Convert.ToInt32(coordenada.Text);
                                            NumCoordenada++;
                                            coordenada.Text = NumCoordenada.ToString();
                                        }
                                        else
                                        {
                                            iteracion++;
                                            coordenada.Text = iteracion.ToString();
                                        }


                                    }
                                }
                            }

                        }
                        #endregion Perfil de Riesgo Residual por Sistema de Riesgo
                    }


                    break;
            }
            trMapaRiesgos.Visible = true;
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            resetValuesMatriz();
            resetRiesgos();
            resetDetalleRiesgo();
            inicializarValores();
            loadDDLPlanes();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            resetDetalleRiesgo();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridRiesgos = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridRiesgos) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Ver":
                    trDetalleRiesgo.Visible = true;
                    loadGridControlesRiesgo();
                    loadInfoControlesRiesgo();
                    loadGridObjetivoRiesgo();
                    loadInfoObjetivoRiesgo();
                    loadGridPlanAccionRiesgo();
                    loadInfoPlanAccionRiesgo();
                    loadGridResponsableRiesgo();
                    loadInfoResponsableRiesgo();
                    break;
            }
        }

        private void loadGridControlesRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdControlesRiesgo", typeof(string));
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("NombreTest", typeof(string));
            grid.Columns.Add("CalificacionControl", typeof(string));
            grid.Columns.Add("DesviacionProbabilidad", typeof(string));
            grid.Columns.Add("DesviacionImpacto", typeof(string));
            InfoGridControlesRiesgo = grid;
            GridView2.DataSource = InfoGridControlesRiesgo;
            GridView2.DataBind();
        }

        private void loadInfoControlesRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoControlesRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControlesRiesgo.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["IdControlesRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),                                                                   
                        dtInfo.Rows[rows]["CalificacionControl"].ToString().Trim(),
                        dtInfo.Rows[rows]["DesviacionProbabilidad"].ToString().Trim(),
                        dtInfo.Rows[rows]["DesviacionImpacto"].ToString().Trim()
                        });
                }
                GridView2.DataSource = InfoGridControlesRiesgo;
                GridView2.DataBind();
            }
        }

        private void loadInfoObjetivoRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoObjetivoRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridObjetivoRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdObjetivosRiesgo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdObjetivos"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["NombreObjetivos"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                                                                  dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim()
                                                                 });
                }
                GridView7.DataSource = InfoGridObjetivoRiesgo;
                GridView7.DataBind();
            }
        }

        private void loadGridObjetivoRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdObjetivosRiesgo", typeof(string));
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("IdObjetivos", typeof(string));
            grid.Columns.Add("NombreObjetivos", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            InfoGridObjetivoRiesgo = grid;
            GridView7.DataSource = InfoGridObjetivoRiesgo;
            GridView7.DataBind();
        }

        private void loadInfoPlanAccionRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoPlanAccionRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridPlanAccionRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["DescripcionAccion"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdTipoRecursoPlanAccion"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["ValorRecurso"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["IdEstadoPlanAccion"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["NombreEstadoPlanAccion"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["FechaCompromiso"].ToString().Trim(),
                                                                    dtInfo.Rows[rows]["NombreHijo"].ToString().Trim()
                                                                   });
                }
                GridView3.DataSource = InfoGridPlanAccionRiesgo;
                GridView3.DataBind();
            }
        }

        private void loadGridPlanAccionRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlanAccion", typeof(string));
            grid.Columns.Add("DescripcionAccion", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            grid.Columns.Add("IdTipoRecursoPlanAccion", typeof(string));
            grid.Columns.Add("ValorRecurso", typeof(string));
            grid.Columns.Add("IdEstadoPlanAccion", typeof(string));
            grid.Columns.Add("NombreEstadoPlanAccion", typeof(string));
            grid.Columns.Add("FechaCompromiso", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            InfoGridPlanAccionRiesgo = grid;
            GridView3.DataSource = InfoGridPlanAccionRiesgo;
            GridView3.DataBind();
        }

        private void loadInfoResponsableRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoResponsableRiesgo(InfoGridRiesgos.Rows[RowGridRiesgos]["IdRiesgo"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridResponsableRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdResponsableRiesgo"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["Responsable"].ToString().Trim()                                                                    
                                                                    });
                }
                GridView8.DataSource = InfoGridResponsableRiesgo;
                GridView8.DataBind();
            }
        }

        private void loadGridResponsableRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdResponsableRiesgo", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            InfoGridResponsableRiesgo = grid;
            GridView8.DataSource = InfoGridResponsableRiesgo;
            GridView8.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridRiesgos = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridRiesgos;
            GridView1.DataSource = InfoGridRiesgos;
            GridView1.DataBind();
        }

        #region Eventos de Botones Area
        protected void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                ListBox2.Items.Insert(i, new ListItem(ListBox1.Items[i].Text, ListBox1.Items[i].Value));
            }
            ListBox1.Items.Clear();
            ListBox1.Visible = false;
            ListBox2.Visible = true;
        }

        protected void BtnSelectOne_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                int Treg = ListBox2.Items.Count;
                if (ListBox1.SelectedIndex != -1)
                {
                    if (Treg == 0)
                        ListBox2.Items.Insert(0, new ListItem(ListBox1.SelectedItem.Text, ListBox1.SelectedItem.Value));
                    else
                        ListBox2.Items.Insert(Treg, new ListItem(ListBox1.SelectedItem.Text, ListBox1.SelectedItem.Value));

                    ListBox1.Items.Remove(ListBox1.SelectedItem);
                }
                ListBox2.ClearSelection();
                ListBox2.Visible = true;
            }
            if (ListBox1.Items.Count == 0)
                ListBox1.Visible = false;
        }

        protected void BtnUnSelectOne_Click(object sender, EventArgs e)
        {
            ListBox1.Visible = true;
            if (ListBox2.SelectedItem != null)
            {
                if (ListBox2.SelectedIndex != -1)
                {
                    int Treg = ListBox1.Items.Count;
                    if (Treg == 0)
                        ListBox1.Items.Insert(0, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));
                    else
                        ListBox1.Items.Insert(Treg, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));

                    ListBox2.Items.Remove(ListBox2.SelectedItem);
                }
                ListBox1.ClearSelection();
            }
            if (ListBox2.Items.Count == 0)
                ListBox2.Visible = false;
        }

        protected void BtnUnSelectAll_Click(object sender, EventArgs e)
        {
            ListBox1.Visible = true;
            ListBox2.Visible = false;
            if (ListBox2.Items.Count > 0)
            {
                for (int i = 0; i < ListBox2.Items.Count; i++)
                {
                    ListBox2.SelectedIndex = i;
                    ListBox1.Items.Insert(i, new ListItem(ListBox2.SelectedItem.Text, ListBox2.SelectedItem.Value));
                }
                ListBox2.Items.Clear();
            }
        }
        #endregion Eventos de Botones Area

        /// <summary>
        /// Procedimiento para cargar las areas asociadas
        /// </summary>
        private void mtdCargarAreas()
        {
            DataTable DtInfo = new DataTable();
            cRiesgo clsRiesgo = new cRiesgo();

            try
            {
                DtInfo = clsRiesgo.mtdCargarInfoAreas();
                for (int i = 0; i < DtInfo.Rows.Count; i++)
                {
                    ListBox1.Items.Insert(i, new ListItem(DtInfo.Rows[i]["NombreArea"].ToString().Trim(), DtInfo.Rows[i]["IdArea"].ToString()));
                    ListBox1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar Areas." + ex.Message);
            }
        }

        private void mtdLoadFactorRiesgo(int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.mtdCargarDDLFactorRiesgo(Tipo);

                switch (Tipo)
                {
                    case 1://FactorRiesgo
                        break;

                    case 2://FactorRiesgoLAFT
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreFactorRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdFactorRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el Factor de Riesgo. " + ex.Message);
            }
        }

        protected void DDLPlanEstrategico_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLObjetivoEstrategico.Items.Clear();
            DDLObjetivoEstrategico.Items.Insert(0, new ListItem("---", "0"));
            if (DDLPlanEstrategico.SelectedValue.ToString().Trim() != "---")
                loadDDLObjetivos();
        }
    }
}