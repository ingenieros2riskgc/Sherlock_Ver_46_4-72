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
using System.Runtime.InteropServices;
using System.Web.UI.DataVisualization.Charting;
using ListasSarlaft.Classes;
using System.Data.OleDb;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Gestion
{
    public partial class MenuCMI : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();

        String IdFormulario = "7012";
        String IdFormularioColores = "7013";
        String codigo;
        String cumplimiento;
        String cumplimientoHoy;
        String cumplimientoCon;
        String color;
        String colorHoy;
        String colorCon;
        String Mes;
        //String Periodo;
        String FechaMes;
        String FechaAno;
        DataTable dtInfoObj = new DataTable();

        private cGestionCMI cGestionCMI = new cGestionCMI();

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

        private DataTable infGridge;
        private DataTable InfoGridge
        {
            get
            {
                infGridge = (DataTable)ViewState["infGridge"];
                return infGridge;
            }
            set
            {
                infGridge = value;
                ViewState["infGridge"] = infGridge;
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

        private int idexRowge;
        private int IdexRowge
        {
            get
            {
                idexRowge = (int)ViewState["idexRowge"];
                return idexRowge;
            }
            set
            {
                idexRowge = value;
                ViewState["idexRowge"] = idexRowge;
            }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            //ver fecha CMI
            if (Image15.Visible == true)
            {
                LbFecha.Visible = false;
                TrFecha.Visible = false;
            }
            else
            {
                LbFecha.Visible = true;
                TrFecha.Visible = true;
            }
            //Cálculo de fecha
            if (DropDownListFechaMes.SelectedValue == "1")
            {
                FechaMes = "12";
                int NuevaFechaAno = Convert.ToInt16(DropDownListFechaAno.SelectedValue) - 1;
                FechaAno = Convert.ToString(NuevaFechaAno);
            }
            else
            {
                //FechaMes = DropDownListFechaMes.SelectedValue.ToString().Trim();
                //30-01-2014
                FechaMes = Convert.ToString(Convert.ToInt16(DropDownListFechaMes.SelectedValue) - 1);
                FechaAno = DropDownListFechaAno.SelectedValue.ToString().Trim();
            }
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!IsPostBack)
            {
                DateTime LaFecha = System.DateTime.Now;
                DropDownListFechaMes.SelectedValue = LaFecha.Month.ToString().Trim();
                DropDownListFechaAno.SelectedValue = LaFecha.Year.ToString().Trim();
                CargaPlanEstrattegico();
                //Ver las descripciones de Colores
                DataTable elcolor = new DataTable();
                elcolor = cGestionCMI.cmi_vercolor();
                Label2.Text = elcolor.Rows[0]["Descripcion"].ToString().Trim();
                Label3.Text = elcolor.Rows[1]["Descripcion"].ToString().Trim();
                Label4.Text = elcolor.Rows[2]["Descripcion"].ToString().Trim();
                //Ver las perspectivas
                DataTable DtPerspectivas = new DataTable();
                DtPerspectivas = cGestionCMI.VerPerspectivas();
                Label5.Text = DtPerspectivas.Rows[0]["NombreDetalle"].ToString().Trim();
                Label6.Text = DtPerspectivas.Rows[1]["NombreDetalle"].ToString().Trim();
                Label9.Text = DtPerspectivas.Rows[2]["NombreDetalle"].ToString().Trim();
                Label10.Text = DtPerspectivas.Rows[3]["NombreDetalle"].ToString().Trim();
                RadioButton1.Text = DtPerspectivas.Rows[0]["NombreDetalle"].ToString().Trim();
                RadioButton2.Text = DtPerspectivas.Rows[1]["NombreDetalle"].ToString().Trim();
                RadioButton3.Text = DtPerspectivas.Rows[2]["NombreDetalle"].ToString().Trim();
                RadioButton4.Text = DtPerspectivas.Rows[3]["NombreDetalle"].ToString().Trim();
            }
            else
            {
                if(DropDownListPE.SelectedValue != "---")
                {
                    CargarCuadrosObjetivosEstrategicos();
                }
            }
            if (Image9.Visible == true)
            {
                if (DropDownListOBJ.SelectedValue != "---")
                {
                    CargarCuadrosEstrategias();
                }
            }
            if (Image15.Visible == true)
            {
                if (DropDownListPE.SelectedValue != "---")
                {
                    CrearOverView();
                }
            }
            if (Image13.Visible == true)
            {
                if (DropDownListIndicador.SelectedValue != "---")
                {
                    VerIndicadoresDetalle();
                }
            }
            if (Image12.Visible == true)
            {
                CumplimientoIndicadorGlobal();
            }
            if (Image14.Visible == true)
            {
                VerCumplimientoResponsables();
            }
            
        }

        private void CargaPlanEstrattegico()
        {
            try
            {
                DataTable dtInfoPE = new DataTable();
                dtInfoPE = cGestionCMI.PlanEstrategico();
                for (int i = 0; i < dtInfoPE.Rows.Count; i++)
                {
                    DropDownListPE.Items.Insert(i + 1, new ListItem(dtInfoPE.Rows[i]["Nombre"].ToString().Trim(), dtInfoPE.Rows[i]["IdPlan"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar Plan Estratégico" + ex.Message);
            }
        }

        private void CargaObjEstrattegico()
        {
            try
            {
                DropDownListOBJ.Items.Clear();
                DropDownListOBJ.Items.Add("---");
                DropDownListOBJ.SelectedIndex = 0;
                dtInfoObj = cGestionCMI.ObjEstrategico(DropDownListPE.SelectedValue.ToString().Trim());
                for (int i = 0; i < dtInfoObj.Rows.Count; i++)
                {
                    DropDownListOBJ.Items.Insert(i + 1, new ListItem(dtInfoObj.Rows[i]["Descripcion"].ToString().Trim(), dtInfoObj.Rows[i]["IdObjetivo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar objetivos. " + ex.Message);
            }
        }

        private void CargaEstrategias()
        {
            try
            {
                TbPlanesdeAccion.Visible = false;
                DropDownListEstrategas.Items.Clear();
                DropDownListEstrategas.Items.Add("---");
                DropDownListEstrategas.SelectedIndex = 0;
                if (DropDownListOBJ.SelectedItem.Text != "---")
                {
                    DataTable dtEstrategia = new DataTable();
                    dtEstrategia = cGestionCMI.Estrategia(DropDownListOBJ.SelectedItem.Value.ToString());
                    int rrr = dtEstrategia.Rows.Count;
                    for (int i = 0; i < dtEstrategia.Rows.Count; i++)
                    {
                        DropDownListEstrategas.Items.Insert(i + 1, new ListItem(dtEstrategia.Rows[i]["Descripcion"].ToString().Trim(), dtEstrategia.Rows[i]["IdEstrategia"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar Estrategias. " + ex.Message);
            }
        }

        private void CargarResponsables()
        {
            try
            {
                DataTable dtResponsables = new DataTable();
                dtResponsables = cGestionCMI.cmiResponsables(DropDownListPE.SelectedItem.Value.ToString());
                for (int a = 0; a < dtResponsables.Rows.Count; a++)
                {
                    DropDownListResponsables.Items.Insert(a + 1, new ListItem(dtResponsables.Rows[a]["NombreResponsable"].ToString().Trim(), dtResponsables.Rows[a]["Responsable"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar responsables. " + ex.Message);
            }
        }

        private void CargarIndicadores()
        {
            try
            {
                DataTable dtIndicadores = new DataTable();
                dtIndicadores = cGestionCMI.cmiIndicadoresDetalle(DropDownListPE.SelectedItem.Value.ToString());
                for (int b = 0; b < dtIndicadores.Rows.Count; b++)
                {
                    DropDownListIndicador.Items.Insert(b + 1, new ListItem(dtIndicadores.Rows[b]["Descripcion"].ToString().Trim(), dtIndicadores.Rows[b]["IdIndicador"].ToString()));

                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar indicadores. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void Mensaje1(String Mensaje1)
        {
            lblMsgBox1.Text = Mensaje1;
            mpeMsgBox1.Show();
        }

        private void NuevoBtn_Click(object sender, EventArgs e)
        {
            String a = ((ImageButton)sender).CommandArgument;
            DropDownListOBJ.SelectedValue = a;
            ocultarformas();
            BtnOBJ.Enabled = false;

            DropDownListPE.Enabled = false;
            Trobj.Visible = true;
            CargarCuadrosEstrategias();
            Image9.Visible = true;
            Button3.Visible = true;
        }

        private void NuevoBtn_ClickPE(object sender, EventArgs e)
        {
            Image9.Visible = false;
            CargaEstrategias();
            String a = ((ImageButton)sender).CommandArgument;
            DropDownListEstrategas.SelectedValue = a;
            ocultarformas();
            BtnPA.Enabled = false;
            Image11.Visible = true;
            Trobj.Visible = true;
            TrEstrategias.Visible = true;
            loadGridPlan();
            cargarInfoGridPlan();
            TbPlanesdeAccion.Visible = true;
            TrTitulo0.Visible = false;
            TrTitulo1.Visible = false;
            TrTitulo2.Visible = false;
            Gestiones.Visible = false;
            Button3.Visible = true;
        }

        protected void DropDownListPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListPE.SelectedItem.Value != "---")
            {
                CargaObjEstrattegico();
                TbInfoReporte.Visible = true;
                codigo = DropDownListPE.SelectedValue.ToString().Trim();
                //NombrePlanEstrategico = DropDownListPE.SelectedItem.Text.ToString().Trim();
            }
            //if (Image15.Visible == true)
            //{
            //    CrearOverView();
            //}

        }
        private void CambiarLabel(int Contador, String IdObjetivo)
        {

            switch (Contador)
            {
                case 11:
                    LabelOverView11.Text = IdObjetivo;
                    break;
                case 12:
                    LabelOverView12.Text = IdObjetivo;
                    break;
                case 13:
                    LabelOverView13.Text = IdObjetivo;
                    break;
                case 14:
                    LabelOverView14.Text = IdObjetivo;
                    break;
                case 15:
                    LabelOverView15.Text = IdObjetivo;
                    break;
                case 16:
                    LabelOverView16.Text = IdObjetivo;
                    break;

                case 21:
                    LabelOverView21.Text = IdObjetivo;
                    break;
                case 22:
                    LabelOverView22.Text = IdObjetivo;
                    break;
                case 23:
                    LabelOverView23.Text = IdObjetivo;
                    break;
                case 24:
                    LabelOverView24.Text = IdObjetivo;
                    break;
                case 25:
                    LabelOverView25.Text = IdObjetivo;
                    break;
                case 26:
                    LabelOverView26.Text = IdObjetivo;
                    break;

                case 31:
                    LabelOverView31.Text = IdObjetivo;
                    break;
                case 32:
                    LabelOverView32.Text = IdObjetivo;
                    break;
                case 33:
                    LabelOverView33.Text = IdObjetivo;
                    break;
                case 34:
                    LabelOverView34.Text = IdObjetivo;
                    break;
                case 35:
                    LabelOverView35.Text = IdObjetivo;
                    break;
                case 36:
                    LabelOverView36.Text = IdObjetivo;
                    break;

                case 41:
                    LabelOverView41.Text = IdObjetivo;
                    break;
                case 42:
                    LabelOverView42.Text = IdObjetivo;
                    break;
                case 43:
                    LabelOverView43.Text = IdObjetivo;
                    break;
                case 44:
                    LabelOverView44.Text = IdObjetivo;
                    break;
                case 45:
                    LabelOverView45.Text = IdObjetivo;
                    break;
                case 46:
                    LabelOverView46.Text = IdObjetivo;
                    break;
            }
        }
        private void CrearOverView()
        {
            codigo = DropDownListPE.SelectedValue.ToString().Trim();
            DataTable dtInfo = new DataTable();
            DataTable dtInfoPE = new DataTable();
            verfecha();
            dtInfoPE = cGestionCMI.cmifechaspe(DropDownListPE.SelectedItem.Value);
            LabelIni.Text = dtInfoPE.Rows[0]["FechaInicio"].ToString().Trim();
            LabelFin.Text = dtInfoPE.Rows[0]["FechaFin"].ToString().Trim();
            //Perspectiva 1
            dtInfo = cGestionCMI.cmi_objetivos(codigo, "1");
            int Per1 = dtInfo.Rows.Count;
            for (int cont = 1; cont <= Per1; cont++)
            {
                CambiarLabel((cont+10), dtInfo.Rows[cont - 1]["IdObjetivo"].ToString().Trim());
                creargrafico(pnlMain1, dtInfo.Rows[cont - 1]["Descripcion"].ToString().Trim(), dtInfo.Rows[cont - 1]["IdObjetivo"].ToString().Trim(), "SqlOverView" + (Convert.ToString(cont+10)));
            }
            ////Perspectiva 2
            dtInfo = cGestionCMI.cmi_objetivos(codigo, "2");
            int Per2 = dtInfo.Rows.Count;
            for (int cont = 1; cont <= Per2; cont++)
            {
                CambiarLabel((cont+20), dtInfo.Rows[cont - 1]["IdObjetivo"].ToString().Trim());
                creargrafico(pnlMain2, dtInfo.Rows[cont - 1]["Descripcion"].ToString().Trim(), dtInfo.Rows[cont - 1]["IdObjetivo"].ToString().Trim(), "SqlOverView" + (Convert.ToString(cont+20)));
            }
            //Perspectiva 3
            dtInfo = cGestionCMI.cmi_objetivos(codigo, "3");
            int Per3 = dtInfo.Rows.Count;
            for (int cont = 1; cont <= Per3; cont++)
            {
                CambiarLabel((cont + 30), dtInfo.Rows[cont - 1]["IdObjetivo"].ToString().Trim());
                creargrafico(pnlMain3, dtInfo.Rows[cont - 1]["Descripcion"].ToString().Trim(), dtInfo.Rows[cont - 1]["IdObjetivo"].ToString().Trim(), "SqlOverView" + (Convert.ToString(cont + 30)));
            }
            //Perspectiva 23
            dtInfo = cGestionCMI.cmi_objetivos(codigo, "23");
            int Per4 = dtInfo.Rows.Count;
            for (int cont = 1; cont <= Per4; cont++)
            {
                CambiarLabel((cont+40),dtInfo.Rows[cont-1]["IdObjetivo"].ToString().Trim());
                creargrafico(pnlMain4, dtInfo.Rows[cont-1]["Descripcion"].ToString().Trim(), dtInfo.Rows[cont-1]["IdObjetivo"].ToString().Trim(), "SqlOverView"+(Convert.ToString(cont+40)));
            }
        }

        protected void BtnMapa_Click(object sender, EventArgs e)
        {
            ocultarformas();
            BtnMapa.Enabled = false;
            Image8.Visible = true;
            TbFiltroPE.Visible = true;
            DropDownListPE.Enabled = true;
            DropDownListPE.SelectedIndex = 0;
            ocultarfecha();
        }

        protected void BtnOBJ_Click(object sender, EventArgs e)
        {
            ocultarformas();
            BtnOBJ.Enabled = false;
            Image9.Visible = true;
            DropDownListPE.Enabled = false;
            Trobj.Visible = true;
            DropDownListOBJ.SelectedIndex = 0;
        }

        private void verfecha()
        {
            Label12.Visible = true;
            LabelIni.Visible = true;
            Label13.Visible = true;
            LabelFin.Visible = true;
        }

        private void ocultarfecha()
        {
            Label12.Visible = false;
            LabelIni.Visible = false;
            Label13.Visible = false;
            LabelFin.Visible = false;
        }

        protected void BtnEST_Click(object sender, EventArgs e)
        {
            if (DropDownListPE.SelectedIndex == 0)
            {
                Mensaje1("Primero debe seleccionar un Plan Estratégico en el menú Mapa Estratégco.");
            }
            else
            {
                ocultarformas();
                TbEstrategia.Visible = true;
                BtnEST.Enabled = false;
                Image10.Visible = true;
                DropDownListPE.Enabled = false;
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
                RadioButton4.Checked = false;
                BtnRES.Enabled = true;
            }
        }
        private void CrearObjetivosPerspectivas(String Perspectiva)
        {
            DataTable dtInfo = new DataTable();
            codigo = DropDownListPE.SelectedValue.ToString().Trim();
            dtInfo = cGestionCMI.cmi_objetivos(codigo, Perspectiva);
            int Objetivos = dtInfo.Rows.Count;
            for (int cont = 0; cont < Objetivos; cont++)
            {
                Table MyTable = new Table();
                TableCell aCell = new TableCell();
                TableCell bCell = new TableCell();
                TableCell cCell = new TableCell();
                TableCell dCell = new TableCell();
                TableCell eCell = new TableCell();
                TableCell fCell = new TableCell();
                TableRow aRow = new TableRow();
                DataTable dtInfoEstrategia = new DataTable();
                //MyTable.Width = 500;
                MyTable.Height = 30;
                MyTable.Rows.Add(aRow);
                aRow.Cells.Add(aCell);
                aRow.Cells.Add(bCell);
                aRow.Cells.Add(cCell);
                aRow.Cells.Add(dCell);
                aRow.Cells.Add(eCell);
                aRow.Cells.Add(fCell);
                aCell.Font.Name = "Calibri";
                aCell.Font.Size = 12;
                aCell.ForeColor = System.Drawing.Color.DarkBlue;
                aCell.Text = dtInfo.Rows[cont]["Descripcion"].ToString().Trim();
                //aCell.BorderWidth = 1;
                bCell.Font.Name = "Calibri";
                bCell.Font.Size = 12;
                bCell.ForeColor = System.Drawing.Color.DarkBlue;
                bCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                bCell.Text = dtInfo.Rows[cont]["Inicio"].ToString().Trim();
                //bCell.BorderWidth = 1;
                cCell.Font.Name = "Calibri";
                cCell.Font.Size = 12;
                cCell.ForeColor = System.Drawing.Color.DarkBlue;
                cCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                cCell.Text = dtInfo.Rows[cont]["Fin"].ToString().Trim();
                //cCell.BorderWidth = 1;
                dCell.Font.Name = "Calibri";
                dCell.Font.Size = 12;
                dCell.ForeColor = System.Drawing.Color.DarkBlue;
                //dCell.BorderWidth = 1;
                //dCell.BorderColor = System.Drawing.Color.Gold;
                aCell.Width = 330;
                bCell.Width = 90;
                cCell.Width = 90;
                //dCell.Width = 300;
                dtInfoEstrategia = cGestionCMI.Estrategia(dtInfo.Rows[cont]["IdObjetivo"].ToString().Trim());
                int Estartegias = dtInfoEstrategia.Rows.Count;
                for (int contest = 0; contest < Estartegias; contest++)
                {
                    Table MyTable1 = new Table();
                    TableCell straCell = new TableCell();
                    TableCell strbCell = new TableCell();
                    TableCell strcCell = new TableCell();
                    TableRow straRow = new TableRow();
                    Image img1 = new Image();
                    img1.Width = 25;
                    Image img1Hoy = new Image();
                    img1Hoy.Width = 25;
                    Image img1Con = new Image();
                    img1Con.Width = 25;
                    //Graficos de cumplimiento
                    DataTable dtInfo1 = new DataTable();
                    DataTable dtInfo2 = new DataTable();
                    DataTable dtInfo1Hoy = new DataTable();
                    DataTable dtInfo2Hoy = new DataTable();
                    DataTable dtInfo1Con = new DataTable();
                    DataTable dtInfo2Con = new DataTable();
                    cumplimiento = "";
                    cumplimientoHoy = "";
                    cumplimientoCon = "";
                    color = "";
                    colorHoy = "";
                    colorCon = "";
                    DataTable VerIndicadores = new DataTable();
                    VerIndicadores = cGestionCMI.cmi_VerIndicadoresObjetivos(dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim());
                    strbCell.Controls.Add(img1);
                    if (VerIndicadores.Rows[0]["CantidadIndicadores"].ToString() == "0")
                    {
                        img1.ToolTip = "Sin indicadores asociados";
                        img1.ImageUrl = "~/Imagenes/Aplicacion/SinInfo.png";
                    }
                    else
                    {
                        //Mes
                        dtInfo1 = cGestionCMI.PorcentjeCumplimiento("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim(), FechaMes, FechaAno);
                        if (dtInfo1.Rows.Count > 0)
                        {
                            cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                            dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                            color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                            img1.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                            if (color == "Green")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (color == "Orange")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (color == "Red")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }
                        else
                        {
                            img1.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                            img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }

                        //Hoy objetivos perspectivas
                        //dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                        //if (dtInfo1Hoy.Rows.Count > 0)
                        //{
                        //    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                        //    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                        //    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                        //    img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                        //    strbCell.Controls.Add(img1Hoy);
                        //    if (colorHoy == "Green")
                        //    {
                        //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        //    }
                        //    if (colorHoy == "Orange")
                        //    {
                        //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        //    }
                        //    if (colorHoy == "Red")
                        //    {
                        //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        //    }
                        //}
                        //else
                        //{
                        //    img1Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                        //    img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        //}
                        dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                        if (dtInfo1Hoy.Rows.Count > 0)
                        {
                            cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                            dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                            colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                            img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                            strbCell.Controls.Add(img1Hoy);
                            if (colorHoy == "Green")
                            {
                                img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (colorHoy == "Orange")
                            {
                                img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (colorHoy == "Red")
                            {
                                img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }
                        else
                        {
                            dtInfo1 = cGestionCMI.PorcentjeCumplimiento("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                            if (dtInfo1.Rows.Count > 0)
                            {
                                cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                                dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                strbCell.Controls.Add(img1Hoy);
                                if (colorHoy == "Green")
                                {
                                    img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                }
                                if (colorHoy == "Orange")
                                {
                                    img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                }
                                if (colorHoy == "Red")
                                {
                                    img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }
                                strbCell.Controls.Add(img1Hoy);
                            }
                            else
                            {
                                img1Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                                img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                strbCell.Controls.Add(img1Hoy);
                            }
                        }

                        //Consolidado
                        //if (System.DateTime.Now.Month == Convert.ToInt16(DropDownListFechaMes.SelectedValue.ToString()) && System.DateTime.Now.Year == Convert.ToInt16(DropDownListFechaAno.SelectedValue.ToString()))
                        //if (dtInfo1Hoy.Rows.Count > 0)
                        //{
                        //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoy("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                        //    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                        //}
                        //else
                        //{
                        //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim());
                        //    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                        //}
                        dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim());
                        cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                        if (cumplimientoCon != "")
                        {
                            cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                        }
                        else
                        {
                            dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("3", dtInfoEstrategia.Rows[contest]["IdEstrategia"].ToString().Trim());
                            cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                        }
                        //Fin

                        dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                        colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                        img1Con.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                        strbCell.Controls.Add(img1Con);
                        if (colorCon == "Green")
                        {
                            img1Con.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorCon == "Orange")
                        {
                            img1Con.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorCon == "Red")
                        {
                            img1Con.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                    }
                    MyTable1.Width = 420;
                    MyTable1.Height = 30;
                    MyTable1.Rows.Add(straRow);
                    straRow.Cells.Add(straCell);
                    straRow.Cells.Add(strbCell);
                    straRow.Cells.Add(strcCell);
                    straCell.Width = 350;
                    straCell.Font.Name = "Calibri";
                    straCell.Font.Size = 12;
                    straCell.ForeColor = System.Drawing.Color.DarkBlue;
                    straCell.Text = dtInfoEstrategia.Rows[contest]["Descripcion"].ToString().Trim();
                    strbCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    strcCell.Font.Name = "Calibri";
                    strcCell.Font.Size = 12;
                    strcCell.ForeColor = System.Drawing.Color.DarkBlue;
                    strcCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    dCell.Controls.Add(MyTable1);
                }
                PanelObjetivos.Controls.Add(MyTable);
                PanelObjetivos.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"));
            }
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CrearObjetivosPerspectivas("1");
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CrearObjetivosPerspectivas("2");
        }

        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CrearObjetivosPerspectivas("3");
        }

        protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CrearObjetivosPerspectivas("23");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormularioColores) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            else
            {
                DataTable elcolor = new DataTable();
                elcolor = cGestionCMI.cmi_vercolor();
                TextBox1.Text = elcolor.Rows[0]["ColorMinimo"].ToString().Trim();
                TextBox2.Text = elcolor.Rows[0]["ColorMaximo"].ToString().Trim();
                TextBox7.Text = elcolor.Rows[0]["Descripcion"].ToString().Trim();
                TextBox3.Text = elcolor.Rows[1]["ColorMinimo"].ToString().Trim();
                TextBox4.Text = elcolor.Rows[1]["ColorMaximo"].ToString().Trim();
                TextBox8.Text = elcolor.Rows[1]["Descripcion"].ToString().Trim();
                TextBox5.Text = elcolor.Rows[2]["ColorMinimo"].ToString().Trim();
                TextBox6.Text = elcolor.Rows[2]["ColorMaximo"].ToString().Trim();
                TextBox9.Text = elcolor.Rows[2]["Descripcion"].ToString().Trim();
                TextBox7.Visible = true;
                TextBox8.Visible = true;
                TextBox9.Visible = true;
                TbParColores.Visible = true;
            }
            Button2.Visible = false;
        }

        protected void BtnGuardarColor_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormularioColores) == "False")
            {
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            }
            else
            {
                cGestionCMI.cmi_parametrizacolor(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()), "Red");
                cGestionCMI.cmi_parametrizacolor(Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim()), "Orange");
                cGestionCMI.cmi_parametrizacolor(Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox9.Text.Trim()), "Green");
                TbParColores.Visible = false;
                Button2.Visible = true;
                TextBox7.Visible = false;
                TextBox8.Visible = false;
                TextBox9.Visible = false;
                Label2.Text = Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim());
                Label3.Text = Sanitizer.GetSafeHtmlFragment(TextBox8.Text.Trim());
                Label4.Text = Sanitizer.GetSafeHtmlFragment(TextBox9.Text.Trim());
                Mensaje("Parámetros de colores modificados correctamente");
                
            }
        }

        protected void BtnCancelaColor_Click(object sender, ImageClickEventArgs e)
        {
            TbParColores.Visible = false;
            Button2.Visible = true;
            TextBox7.Visible = false;
            TextBox8.Visible = false;
            TextBox9.Visible = false;
        }

        protected void BtnRES_Click(object sender, EventArgs e)
        {
            if (DropDownListPE.SelectedIndex == 0)
            {
                Mensaje1("Primero debe seleccionar un Plan Estratégico en el menú Mapa Estratégco.");
            }
            else
            {
                ocultarformas();
                DropDownListResponsables.Items.Clear();
                DropDownListResponsables.Items.Add("---");
                DropDownListResponsables.SelectedIndex = 0;
                CargarResponsables();
                TrResponsables.Visible = true;
                BtnRES.Enabled = false;
                Image14.Visible = true;
            }
        }

        protected void DropDownListResponsables_SelectedIndexChanged(object sender, EventArgs e)
        {
            TbResponsables.Visible = true;
        }

        public void VerCumplimientoResponsables()
        {
            if (DropDownListResponsables.SelectedValue != "---")
            {
                DataTable ResObj = new DataTable();
                ResObj = cGestionCMI.cmiResponsablesObjetivos(DropDownListPE.SelectedItem.Value.ToString().Trim(), DropDownListResponsables.SelectedItem.Value.ToString());
                int CountResobj = ResObj.Rows.Count;
                for (int cont = 0; cont < CountResobj; cont++)
                {
                    Table MyTable = new Table();
                    TableRow aRow = new TableRow();
                    TableCell aCell = new TableCell();
                    TableCell bCell = new TableCell();
                    DataTable ResPa = new DataTable();
                    MyTable.Height = 30;
                    MyTable.Rows.Add(aRow);
                    aRow.Cells.Add(aCell);
                    aRow.Cells.Add(bCell);
                    aCell.Font.Name = "Calibri";
                    aCell.Font.Size = 12;
                    aCell.ForeColor = System.Drawing.Color.DarkBlue;
                    aCell.Text = ResObj.Rows[cont]["Descripcion"].ToString().Trim();
                    aCell.Width = 345;
                    bCell.Font.Name = "Calibri";
                    bCell.Font.Size = 12;
                    bCell.ForeColor = System.Drawing.Color.DarkBlue;
                    ResPa = cGestionCMI.cmiResponsablesPlanesAccion(ResObj.Rows[cont]["IdObjetivo"].ToString().Trim(), DropDownListResponsables.SelectedItem.Value.ToString());

                    int CountPlanes = ResPa.Rows.Count;
                    for (int contpa = 0; contpa < CountPlanes; contpa++)
                    {
                        Table MyTablepa = new Table();
                        TableRow pRow = new TableRow();
                        TableCell pCell = new TableCell();
                        TableCell qCell = new TableCell();
                        TableCell rCell = new TableCell();
                        //TableCell sCell = new TableCell();
                        TableCell t1Cell = new TableCell();
                        TableCell t2Cell = new TableCell();
                        TableCell t3Cell = new TableCell();
                        TableCell uCell = new TableCell();
                        Image img1 = new Image();
                        img1.Width = 25;
                        Image img2 = new Image();
                        img2.Width = 25;
                        Image img3 = new Image();
                        img3.Width = 25;
                        pCell.Width = 345;
                        qCell.Width = 100;
                        qCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                        rCell.Width = 100;
                        rCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                        uCell.Width = 60;
                        DataTable dtInfo1 = new DataTable();
                        DataTable dtInfo2 = new DataTable();
                        DataTable dtInfo1Hoy = new DataTable();
                        DataTable dtInfo2Hoy = new DataTable();
                        DataTable dtInfo1Con = new DataTable();
                        DataTable dtInfo2Con = new DataTable();
                        cumplimiento = "";
                        color = "";
                        //Mes
                        dtInfo1 = cGestionCMI.PorcentjeCumplimiento("2", ResPa.Rows[contpa]["IdPlanAccion"].ToString().Trim(), FechaMes, FechaAno);
                        if (dtInfo1.Rows.Count > 0)
                        {
                            cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                            dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                            color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                            img1.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                            if (color == "Green")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (color == "Orange")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (color == "Red")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }
                        else
                        {
                            img1.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                            img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                        //Hoy Indicador Plan de Accion
                        dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("2", ResPa.Rows[contpa]["IdPlanAccion"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                        if (dtInfo1Hoy.Rows.Count > 0)
                        {
                            cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                            dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                            colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                            img2.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                            if (colorHoy == "Green")
                            {
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (colorHoy == "Orange")
                            {
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (colorHoy == "Red")
                            {
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }
                        else
                        {
                            dtInfo1 = cGestionCMI.PorcentjeCumplimiento("2", ResPa.Rows[contpa]["IdPlanAccion"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                            if (dtInfo1.Rows.Count > 0)
                            {
                                cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                                dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                img2.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                if (colorHoy == "Green")
                                {
                                    img2.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                }
                                if (colorHoy == "Orange")
                                {
                                    img2.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                }
                                if (colorHoy == "Red")
                                {
                                    img2.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }
                            }
                            else
                            {
                                img2.ToolTip = "Cumplimiento Hoy: 00" + " %";
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }
                        //Consolidado
                        //30-01-2014
                        dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("2", ResPa.Rows[contpa]["IdPlanAccion"].ToString().Trim());
                        cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                        if (cumplimientoCon != "")
                        {
                            cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                        }
                        else
                        {
                            dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("2", ResPa.Rows[contpa]["IdPlanAccion"].ToString().Trim());
                            cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                        }

                        //dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                        //colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                        img3.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                        if (colorCon == "Green")
                        {
                            img3.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorCon == "Orange")
                        {
                            img3.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorCon == "Red")
                        {
                            img3.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                        //fin
                        MyTablepa.Height = 30;
                        MyTablepa.Rows.Add(pRow);
                        pRow.Cells.Add(pCell);
                        pRow.Cells.Add(qCell);
                        pRow.Cells.Add(rCell);
                        pRow.Cells.Add(t1Cell);
                        pRow.Cells.Add(t2Cell);
                        pRow.Cells.Add(t3Cell);
                        pRow.Cells.Add(uCell);
                        pCell.Font.Name = "Calibri";
                        pCell.Font.Size = 12;
                        pCell.ForeColor = System.Drawing.Color.DarkBlue;
                        pCell.Text = ResPa.Rows[contpa]["Descripcion"].ToString().Trim();
                        qCell.Font.Name = "Calibri";
                        qCell.Font.Size = 12;
                        qCell.ForeColor = System.Drawing.Color.DarkBlue;
                        qCell.Text = ResPa.Rows[contpa]["Inicio"].ToString().Trim();
                        rCell.Font.Name = "Calibri";
                        rCell.Font.Size = 12;
                        rCell.ForeColor = System.Drawing.Color.DarkBlue;
                        rCell.Text = ResPa.Rows[contpa]["Fin"].ToString().Trim();
                        t1Cell.Controls.Add(img1);
                        t2Cell.Controls.Add(img2);
                        t3Cell.Controls.Add(img3);
                        uCell.Font.Name = "Calibri";
                        uCell.Font.Size = 12;
                        uCell.ForeColor = System.Drawing.Color.DarkBlue;
                        if (ResPa.Rows[contpa]["Abierto_SN"].ToString() == "S")
                        {
                            uCell.Text = "Abierto";
                        }
                        else
                        {
                            uCell.Text = "Cerrado";
                        }
                        bCell.Controls.Add(MyTablepa);
                    }
                    PanelResponsables.Controls.Add(MyTable);
                    PanelResponsables.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"));
                }
            }
        }

        protected void BtnINDG_Click(object sender, EventArgs e)
        {
            CumplimientoIndicadorGlobal();
        }

        public void CumplimientoIndicadorGlobal()
        {
            if (DropDownListPE.SelectedIndex == 0)
            {
                Mensaje1("Primero debe seleccionar un Plan Estratégico en el menú Mapa Estratégco.");
            }
            else
            {
                ocultarformas();
                TbIndicadorGlobal.Visible = true;
                BtnINDG.Enabled = false;
                Image12.Visible = true;
                DataTable PaIndicadorG = new DataTable();
                PaIndicadorG = cGestionCMI.cmi_PAIndicadoresGlobales(DropDownListPE.SelectedItem.Value.ToString());
                int CountIndiGlobal = PaIndicadorG.Rows.Count;
                for (int cont = 0; cont < CountIndiGlobal; cont++)
                {
                    Table MyTable = new Table();
                    TableRow aRow = new TableRow();
                    TableCell aCell = new TableCell();
                    TableCell bCell = new TableCell();
                    DataTable Inidicador = new DataTable();
                    MyTable.Height = 30;
                    MyTable.Rows.Add(aRow);
                    aRow.Cells.Add(aCell);
                    aRow.Cells.Add(bCell);
                    aCell.Font.Name = "Calibri";
                    aCell.Font.Size = 12;
                    aCell.ForeColor = System.Drawing.Color.DarkBlue;
                    aCell.Text = PaIndicadorG.Rows[cont]["PlanAccion"].ToString().Trim();
                    bCell.Font.Name = "Calibri";
                    bCell.Font.Size = 12;
                    bCell.ForeColor = System.Drawing.Color.DarkBlue;
                    Inidicador = cGestionCMI.cmiIndicadoresGlobales(PaIndicadorG.Rows[cont]["IdPlanAccion"].ToString().Trim());

                    int CountIndi = Inidicador.Rows.Count;
                    for (int contind = 0; contind < CountIndi; contind++)
                    {
                        Table MyTableIndicador = new Table();
                        TableRow rRow = new TableRow();
                        TableCell rCell = new TableCell();
                        TableCell sCell = new TableCell();
                        TableCell tCell = new TableCell();
                        TableCell uCell = new TableCell();
                        Image img1 = new Image();
                        img1.Width = 25;
                        Image img2 = new Image();
                        img2.Width = 25;
                        Image img3 = new Image();
                        img3.Width = 25;
                        aCell.Width = 310;
                        rCell.Width = 250;
                        DataTable dtInfo1 = new DataTable();
                        DataTable dtInfo2 = new DataTable();
                        DataTable dtInfo1Hoy = new DataTable();
                        DataTable dtInfo2Hoy = new DataTable();
                        DataTable dtInfo1Con = new DataTable();
                        DataTable dtInfo2Con = new DataTable();
                        cumplimiento = "";
                        color = "";

                        //Mes
                        dtInfo1 = cGestionCMI.PorcentjeCumplimiento("1", Inidicador.Rows[contind]["IdIndicador"].ToString().Trim(), FechaMes, FechaAno);
                        if (dtInfo1.Rows.Count > 0)
                        {
                            cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                            dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                            color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                            img1.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                            if (color == "Green")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (color == "Orange")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (color == "Red")
                            {
                                img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }
                        else
                        {
                            img1.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                            img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }

                        //Hoy Indicador Plan de Accion
                        dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("1", Inidicador.Rows[contind]["IdIndicador"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                        if (dtInfo1Hoy.Rows.Count > 0)
                        {
                            cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                            dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                            colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                            img2.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                            if (colorHoy == "Green")
                            {
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (colorHoy == "Orange")
                            {
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (colorHoy == "Red")
                            {
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }
                        else
                        {
                            dtInfo1 = cGestionCMI.PorcentjeCumplimiento("1", Inidicador.Rows[contind]["IdIndicador"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                            if (dtInfo1.Rows.Count > 0)
                            {
                                cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                                dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                img2.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                if (colorHoy == "Green")
                                {
                                    img2.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                }
                                if (colorHoy == "Orange")
                                {
                                    img2.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                }
                                if (colorHoy == "Red")
                                {
                                    img2.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }
                            }
                            else
                            {
                                img2.ToolTip = "Cumplimiento Hoy: 00" + " %";
                                img2.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }

                        //Consolidado
                        //30-01-2014
                        dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("1", Inidicador.Rows[contind]["IdIndicador"].ToString().Trim());
                        cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                        if (cumplimientoCon != "")
                        {
                            cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                        }
                        else
                        {
                            dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("1", Inidicador.Rows[contind]["IdIndicador"].ToString().Trim());
                            cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                        }

                        //dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                        //colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                        img3.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                        if (colorCon == "Green")
                        {
                            img3.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorCon == "Orange")
                        {
                            img3.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorCon == "Red")
                        {
                            img3.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }

                        MyTableIndicador.Height = 30;
                        MyTableIndicador.Rows.Add(rRow);
                        rRow.Cells.Add(rCell);
                        rRow.Cells.Add(sCell);
                        rRow.Cells.Add(uCell);
                        rRow.Cells.Add(tCell);
                        rCell.Font.Name = "Calibri";
                        rCell.Font.Size = 12;
                        rCell.ForeColor = System.Drawing.Color.DarkBlue;
                        rCell.Text = Inidicador.Rows[contind]["Indicador"].ToString().Trim();
                        sCell.Controls.Add(img1);
                        uCell.Controls.Add(img2);
                        tCell.Controls.Add(img3);
                        //Fin
                        bCell.Controls.Add(MyTableIndicador);
                    }
                    PanelIndicadorGlobal.Controls.Add(MyTable);
                    PanelIndicadorGlobal.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"));
                }
            }
        }

        private void ocultarformas()
        {
            //Botones
            BtnMapa.Enabled = true;
            Image8.Visible = false;
            BtnOBJ.Enabled = true;
            Image9.Visible = false;
            BtnEST.Enabled = true;
            Image10.Visible = false;
            BtnPA.Enabled = true;
            Image11.Visible = false;
            BtnINDD.Enabled = true;
            Image12.Visible = false;
            BtnINDG.Enabled = true;
            Image13.Visible = false;
            BtnRES.Enabled = true;
            Image14.Visible = false;
            BtnOV.Enabled = true;
            Image15.Visible = false;
            Image17.Visible = false;
            //Tr
            DropDownListPE.Enabled = false;
            Trobj.Visible = false;
            TrResponsables.Visible = false;
            TrIndicadorDetallado.Visible = false;
            TrEstrategias.Visible = false;
            //Tablas
            TbInfoReporte.Visible = false;
            TbObjetivos.Visible = false;
            TbEstrategia.Visible = false;
            TbIndicadorGlobal.Visible = false;
            TbResponsables.Visible = false;
            TbIndicadorDetalle.Visible = false;
            TbPlanesdeAccion.Visible = false;
        }

        protected void BtnPA_Click(object sender, EventArgs e)
        {
            //31-01-2014
            if (DropDownListPE.SelectedIndex == 0)
            {
                Mensaje1("Primero seleccionar un Plan Estratégico en el menú Mapa Estratégco.");
            }
            else
            {
                ocultarformas();
                BtnPA.Enabled = false;
                Image11.Visible = true;
                Trobj.Visible = true;
                TrEstrategias.Visible = true;
                DropDownListEstrategas.SelectedIndex = 0;
                DropDownListOBJ.SelectedIndex = 0;
            }
        }

        protected void BtnINDD_Click(object sender, EventArgs e)
        {
            if (DropDownListPE.SelectedIndex == 0)
            {
                Mensaje1("Primero debe seleccionar un Plan Estratégico en el menú Mapa Estratégco.");
            }
            else
            {
                ocultarformas();
                BtnINDD.Enabled = false;
                Image13.Visible = true;
                TrIndicadorDetallado.Visible = true;
                DropDownListIndicador.Items.Clear();
                DropDownListIndicador.Items.Add("---");
                DropDownListIndicador.SelectedIndex = 0;
                TbGrafico.Visible = true;
                CargarIndicadores();
            }
        }

        protected void BtnOV_Click(object sender, EventArgs e)
        {
            ocultarformas();
            BtnOV.Enabled = false;
            Image15.Visible = true;
            TbFiltroPE.Visible = true;
            DropDownListPE.Enabled = true;
            DropDownListPE.SelectedIndex = 0;
            ocultarfecha();
            //LbFecha.Visible = false;
            //TrFecha.Visible = false;
        }

        protected void DropDownListIndicador_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
            

        public void VerIndicadoresDetalle()
        {
            VerFormula();
            Mes = "";
            TbIndicadorDetalle.Visible = true;
            DataTable dtIndicadores = new DataTable();
            DataTable dtIndicadoresResultado = new DataTable();
            //DataTable dtInfo1 = new DataTable();
            //DataTable dtInfo2 = new DataTable();
            try
            {
                dtIndicadores = cGestionCMI.cmi_IndicadoreDetalle(DropDownListIndicador.SelectedItem.Value.ToString());
                Label49.Text = dtIndicadores.Rows[0]["Objetivo"].ToString().Trim();
                Label50.Text = dtIndicadores.Rows[0]["Perspectiva"].ToString().Trim();
                Label51.Text = dtIndicadores.Rows[0]["PlanAccion"].ToString().Trim();
                cumplimiento = "";
                color = "";
            }
            catch (Exception ex)
            {
                Mensaje1("Error dtIndicadores" + ex.Message);
            }
            try
            {
                //Graficos de cumplimiento
                DataTable dtInfo1 = new DataTable();
                DataTable dtInfo2 = new DataTable();
                DataTable dtInfo1Hoy = new DataTable();
                DataTable dtInfo2Hoy = new DataTable();
                DataTable dtInfo1Con = new DataTable();
                DataTable dtInfo2Con = new DataTable();
                cumplimiento = "";
                cumplimientoHoy = "";
                cumplimientoCon = "";
                color = "";
                colorHoy = "";
                colorCon = "";
                DataTable VerIndicadores = new DataTable();
                //Mes
                dtInfo1 = cGestionCMI.PorcentjeCumplimiento("1", DropDownListIndicador.SelectedItem.Value.ToString().Trim(), FechaMes, FechaAno);
                if (dtInfo1.Rows.Count > 0)
                {
                    cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                    color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                    Image16.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                    if (color == "Green")
                    {
                        Image16.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (color == "Orange")
                    {
                        Image16.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (color == "Red")
                    {
                        Image16.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                else
                {
                    Image16.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                    Image16.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                }

                //Hoy indicador Detalle
                //dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("1", DropDownListIndicador.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                //if (dtInfo1Hoy.Rows.Count > 0)
                //{
                //    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                //    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                //    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                //    Image16Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                //    Image16Hoy.Visible = true;
                //    if (colorHoy == "Green")
                //    {
                //        Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                //    }
                //    else if (colorHoy == "Orange")
                //    {
                //        Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                //    }
                //    else if (colorHoy == "Red")
                //    {
                //        Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //    }
                //    else
                //    {
                //        Image16Hoy.Visible = false;
                //    }
                //}
                //else
                //{
                //    Image16Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                //    Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //    Image16Hoy.Visible = false;
                //}
                dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("1", DropDownListIndicador.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                if (dtInfo1Hoy.Rows.Count > 0)
                {
                    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                    Image16Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                    if (colorHoy == "Green")
                    {
                        Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (colorHoy == "Orange")
                    {
                        Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (colorHoy == "Red")
                    {
                        Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                else
                {
                    dtInfo1 = cGestionCMI.PorcentjeCumplimiento("1", DropDownListIndicador.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                    if (dtInfo1.Rows.Count > 0)
                    {
                        cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                        dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                        colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                        Image16Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                        if (colorHoy == "Green")
                        {
                            Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorHoy == "Orange")
                        {
                            Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorHoy == "Red")
                        {
                            Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                    }
                    else
                    {
                        Image16Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                        Image16Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                //Consolidado
                //if (System.DateTime.Now.Month == Convert.ToInt16(DropDownListFechaMes.SelectedValue.ToString()) && System.DateTime.Now.Year == Convert.ToInt16(DropDownListFechaAno.SelectedValue.ToString()))
                //if (dtInfo1Hoy.Rows.Count > 0)
                //{
                //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoy("1", DropDownListIndicador.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                //    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                //}
                //else
                //{
                //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("1", DropDownListIndicador.SelectedItem.Value.ToString());
                //    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                //}
                dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("1", DropDownListIndicador.SelectedItem.Value.ToString());
                cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                if (cumplimientoCon != "")
                {
                    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                }
                else
                {
                    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("1", DropDownListIndicador.SelectedItem.Value.ToString());
                    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                }
                //Fin
                if (string.IsNullOrEmpty(cumplimientoCon))
                {
                    cumplimientoCon = "0";
                }

                dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                Image16Con.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                if (colorCon == "Green")
                {
                    Image16Con.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                }
                if (colorCon == "Orange")
                {
                    Image16Con.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                }
                if (colorCon == "Red")
                {
                    Image16Con.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                }
                //
            }
            catch (Exception ex)
            {
                Mensaje1("Error Cumplimiento" + ex.Message);
            }
            try
            {
                dtIndicadoresResultado = cGestionCMI.cmi_cumplimientoIndicadorResultado(DropDownListIndicador.SelectedItem.Value.ToString());
                int ContResultados = dtIndicadoresResultado.Rows.Count;
                for (int contind = 0; contind < ContResultados; contind++)
                {
                    cumplimiento = "";
                    DataTable dtInfo2 = new DataTable();
                    Table MyTableIndicador = new Table();
                    TableRow aRow = new TableRow();
                    TableCell aCell = new TableCell();
                    TableCell bCell = new TableCell();
                    TableCell cCell = new TableCell();
                    TableCell dCell = new TableCell();
                    Image img = new Image();
                    img.Width = 25;
                    Image imgHoy = new Image();
                    imgHoy.Width = 25;
                    Image imgCon = new Image();
                    imgCon.Width = 25;
                    MyTableIndicador.Rows.Add(aRow);
                    aRow.Cells.Add(aCell);
                    aRow.Cells.Add(bCell);
                    aRow.Cells.Add(cCell);
                    aRow.Cells.Add(dCell);
                    aCell.Width = 280;
                    bCell.Width = 80;
                    cCell.Width = 30;
                    dCell.Width = 40;
                    cCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    cCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    aCell.Font.Name = "Calibri";
                    aCell.Font.Size = 10;
                    aCell.ForeColor = System.Drawing.Color.DarkBlue;
                    bCell.Font.Name = "Calibri";
                    bCell.Font.Size = 10;
                    bCell.ForeColor = System.Drawing.Color.DarkBlue;
                    bCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    dCell.Font.Name = "Calibri";
                    dCell.Font.Size = 10;
                    dCell.ForeColor = System.Drawing.Color.DarkBlue;
                    dCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    aCell.Text = dtIndicadoresResultado.Rows[contind]["Periodo"].ToString().Trim();
                    bCell.Text = dtIndicadoresResultado.Rows[contind]["Resultado"].ToString().Trim();
                    cCell.Controls.Add(img);
                    dCell.Text = dtIndicadoresResultado.Rows[contind]["Cumplimiento"].ToString() + " %";
                    cumplimiento = dtIndicadoresResultado.Rows[contind]["Cumplimiento"].ToString().Trim();
                    if (cumplimiento != "")
                    {
                        dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                        color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                        if (color == "Green")
                        {
                            img.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (color == "Orange")
                        {
                            img.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (color == "Red")
                        {
                            img.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                    }
                    PanelTablaIndicadores.Controls.Add(MyTableIndicador);
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error cargando resultados" + ex.Message);
            }
        }

        protected void VerFormula()
        {
            DataTable dtFormula = new DataTable();
            String formula;
            try
            {
                Image17.Visible = true;
                dtFormula = cGestionCMI.IndicadorFormula(DropDownListIndicador.SelectedItem.Value.ToString());
                formula = "Periodicidad : " + dtFormula.Rows[0]["Periodicidad"].ToString() + "\n\n";
                formula = formula + "Fórmula :\n" + "Nominador :\r" + dtFormula.Rows[0]["Nominador"].ToString().Trim();
                if (dtFormula.Rows[0]["Denominador"].ToString() != "")
                {
                    formula = formula + "\r\rDenominador :\r" + dtFormula.Rows[0]["Denominador"].ToString().Trim();
                }
                Image17.ToolTip = formula;
            }
            catch (Exception ex)
            {
                Mensaje("Tooltip formula" + ex.Message);
            }
        }

        protected void GridViewPlanAccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Indicador":
                    Gestiones.Visible = false;
                    TrTitulo0.Visible = true;
                    TrTitulo1.Visible = true;
                    TrTitulo2.Visible = true;
                    TbIndicadoPlanAccion.Visible = true;
                    IndicadorPlanAccion();
                    break;
                case "Gestion":
                    TrTitulo0.Visible = true;
                    TrTitulo1.Visible = false;
                    TrTitulo2.Visible = false;
                    Gestiones.Visible = true;
                    TbIndicadoPlanAccion.Visible = true;
                    loadGridGestion();
                    cargarInfoGridGestion();
                    break;
            }
        }

        protected void IndicadorPlanAccion()
        {
            DataTable DtIndicadorPa = new DataTable();
            DtIndicadorPa = cGestionCMI.cmi_PlanesAccionIndicador(InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
            Label67.Text = "Plan de Acción: " + InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();

            //Graficos de cumplimiento
            DataTable dtInfo1 = new DataTable();
            DataTable dtInfo2 = new DataTable();
            DataTable dtInfo1Hoy = new DataTable();
            DataTable dtInfo2Hoy = new DataTable();
            DataTable dtInfo1Con = new DataTable();
            DataTable dtInfo2Con = new DataTable();
            cumplimiento = "";
            cumplimientoHoy = "";
            cumplimientoCon = "";
            color = "";
            colorHoy = "";
            colorCon = "";
            DataTable VerIndicadores = new DataTable();
            //VerIndicadores = cGestionCMI.cmi_VerIndicadoresObjetivos(InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
            VerIndicadores = cGestionCMI.cmi_VerIndicadoresPlaAccion(InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
            Image18Hoy.Visible = true;
            Image18Con.Visible = true;
            if (VerIndicadores.Rows[0]["CantidadIndicadores"].ToString() == "0")
            {
                Image18.ToolTip = "Sin indicadores asociados";
                Image18.ImageUrl = "~/Imagenes/Aplicacion/SinInfo.png";
                Image18Hoy.Visible = false;
                Image18Con.Visible = false;
            }
            else
            {
                //Mes
                dtInfo1 = cGestionCMI.PorcentjeCumplimiento("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim(), FechaMes, FechaAno);
                if (dtInfo1.Rows.Count > 0)
                {
                    cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                    color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                    Image18.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                    if (color == "Green")
                    {
                        Image18.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (color == "Orange")
                    {
                        Image18.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (color == "Red")
                    {
                        Image18.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                else
                {
                    Image18.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                    Image18.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                }

                //Hoy Indicador Plan de Accion
                //dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                //if (dtInfo1Hoy.Rows.Count > 0)
                //{
                //    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                //    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                //    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                //    Image18Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                //    if (colorHoy == "Green")
                //    {
                //        Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                //    }
                //    if (colorHoy == "Orange")
                //    {
                //        Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                //    }
                //    if (colorHoy == "Red")
                //    {
                //        Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //    }
                //}
                //else
                //{
                //    Image18Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                //    Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //}
                dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                if (dtInfo1Hoy.Rows.Count > 0)
                {
                    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                    Image18Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                    if (colorHoy == "Green")
                    {
                        Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (colorHoy == "Orange")
                    {
                        Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (colorHoy == "Red")
                    {
                        Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                else
                {
                    dtInfo1 = cGestionCMI.PorcentjeCumplimiento("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                    if (dtInfo1.Rows.Count > 0)
                    {
                        cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                        dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                        colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                        Image18Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                        if (colorHoy == "Green")
                        {
                            Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorHoy == "Orange")
                        {
                            Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorHoy == "Red")
                        {
                            Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                    }
                    else
                    {
                        Image18Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                        Image18Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }

                //Consolidado
                //30-01-2014
                //if (System.DateTime.Now.Month == Convert.ToInt16(DropDownListFechaMes.SelectedValue.ToString()) && System.DateTime.Now.Year == Convert.ToInt16(DropDownListFechaAno.SelectedValue.ToString()))
                //if (dtInfo1Hoy.Rows.Count > 0)
                //{
                //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoy("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                //    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                //}
                //else
                //{
                //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
                //    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                //}
                dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
                cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                if (cumplimientoCon != "")
                {
                    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                }
                else
                {
                    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
                    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                }
                //Fin

                dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                Image18Con.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                if (colorCon == "Green")
                {
                    Image18Con.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                }
                if (colorCon == "Orange")
                {
                    Image18Con.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                }
                if (colorCon == "Red")
                {
                    Image18Con.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                }
            }
            //Indicadores
            int ContIndicador = DtIndicadorPa.Rows.Count;
            for (int cont = 0; cont < ContIndicador; cont++)
            {
                Table MyTable = new Table();
                TableCell rCell = new TableCell();
                TableCell uCell = new TableCell();
                TableCell vCell = new TableCell();
                TableCell sCell = new TableCell();
                TableCell tCell = new TableCell();
                TableRow sRow = new TableRow();
                Image img = new Image();
                //DataTable dtInfo1 = new DataTable();
                //DataTable dtInfo2 = new DataTable();
                cumplimiento = "";
                Image imgHoy = new Image();
                //DataTable dtInfo1Hoy = new DataTable();
                //DataTable dtInfo2Hoy = new DataTable();
                cumplimientoHoy = "";
                color = "";
                dtInfo1 = cGestionCMI.PorcentjeCumplimiento("1", DtIndicadorPa.Rows[cont]["IdIndicador"].ToString().Trim(), FechaMes, FechaAno);
                if (dtInfo1.Rows.Count > 0)
                {
                    cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                    color = dtInfo2.Rows[0]["Color"].ToString().Trim();

                    if (color == "Green")
                    {
                        img.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (color == "Orange")
                    {
                        img.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (color == "Red")
                    {
                        img.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                    img.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + "%";
                }
                else
                {
                    img.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    img.ToolTip = "Cumplimiento Mes Anterior: 00";
                }

                //Hoy Indicadores Plan de Accion
                //dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("1", DtIndicadorPa.Rows[cont]["IdIndicador"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                //if (dtInfo1Hoy.Rows.Count > 0)
                //{
                //    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                //    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                //    color = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();

                //    if (color == "Green")
                //    {
                //        imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                //    }
                //    if (color == "Orange")
                //    {
                //        imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                //    }
                //    if (color == "Red")
                //    {
                //        imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //    }
                //    imgHoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + "%";
                //}
                //else
                //{
                //    imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //    imgHoy.ToolTip = "Cumplimiento Hoy: 00 %";
                //}
                dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("1", DtIndicadorPa.Rows[cont]["IdIndicador"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                if (dtInfo1Hoy.Rows.Count > 0)
                {
                    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                    imgHoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                    if (colorHoy == "Green")
                    {
                        imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (colorHoy == "Orange")
                    {
                        imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (colorHoy == "Red")
                    {
                        imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                else
                {
                    dtInfo1 = cGestionCMI.PorcentjeCumplimiento("1", DtIndicadorPa.Rows[cont]["IdIndicador"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                    if (dtInfo1.Rows.Count > 0)
                    {
                        cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                        dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                        colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                        imgHoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                        if (colorHoy == "Green")
                        {
                            imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorHoy == "Orange")
                        {
                            imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorHoy == "Red")
                        {
                            imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                    }
                    else
                    {
                        imgHoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                        imgHoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }


                MyTable.Height = 30;
                MyTable.Rows.Add(sRow);
                sRow.Cells.Add(rCell);
                sRow.Cells.Add(tCell);
                sRow.Cells.Add(sCell);
                sRow.Cells.Add(uCell);
                sRow.Cells.Add(vCell);
                rCell.Width = 310;
                tCell.Width = 150;
                sCell.Width = 40;
                vCell.Width = 50;
                rCell.Text = DtIndicadorPa.Rows[cont]["Descripcion"].ToString().Trim();
                rCell.Font.Name = "Calibri";
                rCell.Font.Size = 12;
                rCell.ForeColor = System.Drawing.Color.DarkBlue;
                tCell.Text = DtIndicadorPa.Rows[cont]["Periodicidad"].ToString().Trim();
                tCell.Font.Name = "Calibri";
                tCell.Font.Size = 12;
                tCell.ForeColor = System.Drawing.Color.DarkBlue;
                sCell.Text = DtIndicadorPa.Rows[cont]["Meta"].ToString().Trim();
                sCell.Font.Name = "Calibri";
                sCell.Font.Size = 12;
                sCell.ForeColor = System.Drawing.Color.DarkBlue;
                uCell.Controls.Add(img);
                uCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                if (System.DateTime.Now.Month == Convert.ToInt16(DropDownListFechaMes.SelectedValue.ToString()) && System.DateTime.Now.Year == Convert.ToInt16(DropDownListFechaAno.SelectedValue.ToString()))
                {
                    vCell.Controls.Add(imgHoy);
                    vCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                }
                img.Width = 25;
                imgHoy.Width = 25;
                PanelIndicadorPlanAccion.Controls.Add(MyTable);
            }
        }

        protected void DropDownListEstrategas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListEstrategas.SelectedValue != "---")
            {
                try
                {
                    loadGridPlan();
                    cargarInfoGridPlan();
                    TbPlanesdeAccion.Visible = true;
                    TrTitulo0.Visible = false;
                    TrTitulo1.Visible = false;
                    TrTitulo2.Visible = false;
                    Gestiones.Visible = false;
                }
                catch (Exception ex)
                {
                    Mensaje1("Error al cargar Estrategias." + ex.Message);
                }
            }
        }

        private void loadGridPlan()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlanAccion", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFin", typeof(string));
            grid.Columns.Add("Abierto_SN", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            GridViewPlanAccion.DataSource = grid;
            GridViewPlanAccion.DataBind();
            InfoGrid = grid;
        }

        private void cargarInfoGridPlan()
        {
            DataTable dtInfo = new DataTable();
            String elestado;
            dtInfo = cGestionCMI.cmi_PlanesAccion(DropDownListEstrategas.SelectedItem.Value.ToString());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    elestado = "";
                    if (dtInfo.Rows[rows]["Abierto_SN"].ToString().Trim() == "S")
                    {
                        elestado = "Abierto";
                    }
                    if (dtInfo.Rows[rows]["Abierto_SN"].ToString().Trim() == "N")
                    {
                        elestado = "Cerrado";
                    }
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["FechaFin"].ToString().Trim(),
                                                    elestado,
                                                    dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                    });
                }

                GridViewPlanAccion.DataSource = InfoGrid;
                GridViewPlanAccion.DataBind();
            }
        }

        private void loadGridGestion()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdPlanAccion", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("Fecha", typeof(string));
            GridViewGestion.DataSource = grid;
            GridViewGestion.DataBind();
            InfoGridge = grid;
        }
        private void CargarCuadrosObjetivosEstrategicos()
        {
            if (DropDownListPE.SelectedItem.Value != "---")
            {
                if (Image8.Visible == true)
                {
                    codigo = DropDownListPE.SelectedValue.ToString().Trim();
                    DataTable dtInfo = new DataTable();
                    DataTable dtInfoPE = new DataTable();
                    verfecha();
                    dtInfoPE = cGestionCMI.cmifechaspe(DropDownListPE.SelectedItem.Value);
                    LabelIni.Text = dtInfoPE.Rows[0]["FechaInicio"].ToString().Trim();
                    LabelFin.Text = dtInfoPE.Rows[0]["FechaFin"].ToString().Trim();
                    //Perspectiva 1
                    dtInfo = cGestionCMI.cmi_objetivos(codigo, "1");
                    int Per1 = dtInfo.Rows.Count;
                    for (int cont = 0; cont < Per1; cont++)
                    {
                        CrearObjetivoEstrategico(pnlMain1, "1", cont);
                    }
                    //Perspectiva 2
                    dtInfo = cGestionCMI.cmi_objetivos(codigo, "2");
                    int Per2 = dtInfo.Rows.Count;
                    for (int cont = 0; cont < Per2; cont++)
                    {
                        CrearObjetivoEstrategico(pnlMain2, "2", cont);
                    }
                    //Perspectiva 3
                    dtInfo = cGestionCMI.cmi_objetivos(codigo, "3");
                    int Per3 = dtInfo.Rows.Count;
                    for (int cont = 0; cont < Per3; cont++)
                    {
                        CrearObjetivoEstrategico(pnlMain3, "3", cont);
                    }

                    //Perspectiva 23
                    dtInfo = cGestionCMI.cmi_objetivos(codigo, "23");
                    int Per4 = dtInfo.Rows.Count;
                    for (int cont = 0; cont < Per4; cont++)
                    {
                        CrearObjetivoEstrategico(pnlMain4, "23", cont);
                    }
                }
            }
        }

        private void CargarCuadrosEstrategias()
        {
            if (Image11.Visible == true)
            {
                TbPlanesdeAccion.Visible = false;
                CargaEstrategias();
            }
            else
            {
                //DataTable dtInfo1 = new DataTable();
                //DataTable dtInfo1Hoy = new DataTable();
                //DataTable dtInfo2 = new DataTable();
                //DataTable dtInfo2Hoy = new DataTable();
                DataTable dtInfo3 = new DataTable();
                DataTable dtInfo4 = new DataTable();

                TrTitulo0.Visible = false;
                TrTitulo1.Visible = false;
                TrTitulo2.Visible = false;
                Gestiones.Visible = false;

                TbObjetivos.Visible = true;
                if (TbObjetivos.Visible == true)
                {
                    if (DropDownListOBJ.SelectedItem.Text != "---")
                    {
                        dtInfo3 = cGestionCMI.cmi_PerspectivaOjbr(DropDownListOBJ.SelectedItem.Value);
                        Label26.Text = DropDownListOBJ.SelectedItem.Text;
                        Label24.Text = dtInfo3.Rows[0]["Perspectiva"].ToString().Trim();
                        //Graficos de cumplimiento
                        DataTable dtInfo1 = new DataTable();
                        DataTable dtInfo2 = new DataTable();
                        DataTable dtInfo1Hoy = new DataTable();
                        DataTable dtInfo2Hoy = new DataTable();
                        DataTable dtInfo1Con = new DataTable();
                        DataTable dtInfo2Con = new DataTable();
                        cumplimiento = "";
                        cumplimientoHoy = "";
                        cumplimientoCon = "";
                        color = "";
                        colorHoy = "";
                        colorCon = "";
                        DataTable VerIndicadores = new DataTable();
                        VerIndicadores = cGestionCMI.cmi_VerIndicadoresObjetivos(DropDownListOBJ.SelectedItem.Value.ToString());
                        Image4Hoy.Visible = true;
                        Image4Con.Visible = true;
                        if (VerIndicadores.Rows[0]["CantidadIndicadores"].ToString() == "0")
                        {
                            Image4.ToolTip = "Sin indicadores asociados";
                            Image4.ImageUrl = "~/Imagenes/Aplicacion/SinInfo.png";
                            Image4Hoy.Visible = false;
                            Image4Con.Visible = false;
                        }
                        else
                        {
                            //Mes
                            dtInfo1 = cGestionCMI.PorcentjeCumplimiento("4", DropDownListOBJ.SelectedItem.Value.ToString().Trim(), FechaMes, FechaAno);
                            if (dtInfo1.Rows.Count > 0)
                            {
                                cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                                dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                                color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                                Image4.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                                if (color == "Green")
                                {
                                    Image4.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                }
                                if (color == "Orange")
                                {
                                    Image4.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                }
                                if (color == "Red")
                                {
                                    Image4.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }
                            }
                            else
                            {
                                Image4.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                                Image4.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }

                            //Hoy Estrategias
                            //Camiloaponte
                            //dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("4", DropDownListOBJ.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                            //if (dtInfo1Hoy.Rows.Count > 0)
                            //{
                            //    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                            //    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                            //    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                            //    Image4Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                            //    if (colorHoy == "Green")
                            //    {
                            //        Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            //    }
                            //    if (colorHoy == "Orange")
                            //    {
                            //        Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            //    }
                            //    if (colorHoy == "Red")
                            //    {
                            //        Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            //    }
                            //}
                            //else
                            //{
                            //    Image4Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                            //    Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            //}
                            dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("4", DropDownListOBJ.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                            if (dtInfo1Hoy.Rows.Count > 0)
                            {
                                cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                                dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                Image4Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                if (colorHoy == "Green")
                                {
                                    Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                }
                                if (colorHoy == "Orange")
                                {
                                    Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                }
                                if (colorHoy == "Red")
                                {
                                    Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }
                            }
                            else
                            {
                                dtInfo1 = cGestionCMI.PorcentjeCumplimiento("4", DropDownListOBJ.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                                if (dtInfo1.Rows.Count > 0)
                                {
                                    cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                                    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                    Image4Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                    if (colorHoy == "Green")
                                    {
                                        Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                    }
                                    if (colorHoy == "Orange")
                                    {
                                        Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                    }
                                    if (colorHoy == "Red")
                                    {
                                        Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                    }
                                }
                                else
                                {
                                    Image4Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                                    Image4Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }
                            }


                            //Consolidado
                            //30-01-2014
                            //if (System.DateTime.Now.Month == Convert.ToInt16(DropDownListFechaMes.SelectedValue.ToString()) && System.DateTime.Now.Year == Convert.ToInt16(DropDownListFechaAno.SelectedValue.ToString()))
                            //if (dtInfo1Hoy.Rows.Count > 0)
                            //{
                            //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoy("4", DropDownListOBJ.SelectedItem.Value.ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                            //    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                            //}
                            //else
                            //{
                            //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("4", DropDownListOBJ.SelectedItem.Value.ToString());
                            //    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                            //}
                            dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("4", DropDownListOBJ.SelectedItem.Value.ToString());
                            cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                            if (cumplimientoCon != "")
                            {
                                cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                            }
                            else
                            {
                                dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("4", DropDownListOBJ.SelectedItem.Value.ToString());
                                cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                            }
                            //Fin
                            if (!string.IsNullOrEmpty(cumplimientoCon))
                            {
                                dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                                colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                            }
                            Image4Con.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                            if (colorCon == "Green")
                            {
                                Image4Con.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (colorCon == "Orange")
                            {
                                Image4Con.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (colorCon == "Red")
                            {
                                Image4Con.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                        }

                        //carga las estrategias existentes
                        dtInfo4 = cGestionCMI.Estrategia(DropDownListOBJ.SelectedItem.Value);
                        int Estrategias = dtInfo4.Rows.Count;
                        for (int cont = 0; cont < Estrategias; cont++)
                        {
                            Table MyTable = new Table();
                            TableCell rCell = new TableCell();
                            TableCell uCell = new TableCell();
                            TableCell vCell = new TableCell();
                            TableCell zCell = new TableCell();
                            TableRow sRow = new TableRow();
                            Image img1 = new Image();
                            Image img1Hoy = new Image();
                            Image img1Con = new Image();
                            img1.Width = 25;
                            img1Hoy.Width = 25;
                            img1Con.Width = 25;
                            ImageButton img21 = new ImageButton();
                            img21.ImageUrl = "~/Imagenes/Icons/Lupa.png";
                            img21.Width = 20;
                            img21.ToolTip = "Ver detalle";
                            //Graficos de cumplimiento
                            cumplimiento = "";
                            cumplimientoHoy = "";
                            cumplimientoCon = "";
                            color = "";
                            colorHoy = "";
                            colorCon = "";
                            VerIndicadores = cGestionCMI.cmi_VerIndicadoresObjetivos(dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim());
                            uCell.Controls.Add(img1);
                            if (VerIndicadores.Rows[0]["CantidadIndicadores"].ToString() == "0")
                            {
                                img1.ToolTip = "Sin indicadores asociados";
                                img1.ImageUrl = "~/Imagenes/Aplicacion/SinInfo.png";
                            }
                            else
                            {
                                //Mes
                                dtInfo1 = cGestionCMI.PorcentjeCumplimiento("3", dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim(), FechaMes, FechaAno);
                                if (dtInfo1.Rows.Count > 0)
                                {
                                    cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                                    dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                                    color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                                    img1.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                                    if (color == "Green")
                                    {
                                        img1.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                    }
                                    if (color == "Orange")
                                    {
                                        img1.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                    }
                                    if (color == "Red")
                                    {
                                        img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                    }
                                }
                                else
                                {
                                    img1.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                                    img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }

                                //Hoy Estrategias
                                //dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("3", dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                                //if (dtInfo1Hoy.Rows.Count > 0)
                                //{
                                //    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                                //    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                //    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                //    img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                //    uCell.Controls.Add(img1Hoy);
                                //    if (colorHoy == "Green")
                                //    {
                                //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                //    }
                                //    if (colorHoy == "Orange")
                                //    {
                                //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                //    }
                                //    if (colorHoy == "Red")
                                //    {
                                //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                //    }
                                //}
                                //else
                                //{
                                //    img1Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                                //    img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                //}
                                dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("4", dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                                if (dtInfo1Hoy.Rows.Count > 0)
                                {
                                    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                                    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                    img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                    uCell.Controls.Add(img1Hoy);
                                    if (colorHoy == "Green")
                                    {
                                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                    }
                                    if (colorHoy == "Orange")
                                    {
                                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                    }
                                    if (colorHoy == "Red")
                                    {
                                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                    }
                                }
                                else
                                {
                                    dtInfo1 = cGestionCMI.PorcentjeCumplimiento("4", dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim(), DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                                    if (dtInfo1.Rows.Count > 0)
                                    {
                                        cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                                        dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                                        colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                                        img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                                        uCell.Controls.Add(img1Hoy);
                                        if (colorHoy == "Green")
                                        {
                                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                        }
                                        if (colorHoy == "Orange")
                                        {
                                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                        }
                                        if (colorHoy == "Red")
                                        {
                                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                        }
                                        uCell.Controls.Add(img1Hoy);
                                    }
                                    else
                                    {
                                        img1Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                        uCell.Controls.Add(img1Hoy);
                                    }
                                }

                                //Consolidado
                                //30-01-2014
                                dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("3", dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim());
                                cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                                if (cumplimientoCon != "")
                                {
                                    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                                }
                                else
                                {
                                    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("3", dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim());
                                    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                                }
                                //Fin

                               //dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                                //colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                                img1Con.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                                uCell.Controls.Add(img1Con);
                                if (colorCon == "Green")
                                {
                                    img1Con.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                                }
                                if (colorCon == "Orange")
                                {
                                    img1Con.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                                }
                                if (colorCon == "Red")
                                {
                                    img1Con.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                                }
                            }

                            MyTable.Height = 30;
                            MyTable.Rows.Add(sRow);
                            sRow.Cells.Add(rCell);
                            rCell.Text = dtInfo4.Rows[cont]["Descripcion"].ToString().Trim();
                            rCell.ToolTip = "Código Estrategia: " + dtInfo4.Rows[cont]["CodigoEstrategia"].ToString().Trim();
                            rCell.Font.Name = "Calibri";
                            rCell.Font.Size = 12;
                            rCell.ForeColor = System.Drawing.Color.DarkBlue;
                            rCell.Width = 330;
                            sRow.Cells.Add(uCell);
                            uCell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                            sRow.Cells.Add(vCell);
                            sRow.Cells.Add(zCell);
                            zCell.Controls.Add(img21);
                            img21.CommandArgument = dtInfo4.Rows[cont]["IdEstrategia"].ToString().Trim();
                            img21.Click += new System.Web.UI.ImageClickEventHandler(NuevoBtn_ClickPE);
                            PanelEstrategia.Controls.Add(MyTable);
                        }
                    }
                }
            }
        }

        private void cargarInfoGridGestion()
        {
            DataTable dtInfo = new DataTable();
            DataTable DtIndicadorPa = new DataTable();
            DtIndicadorPa = cGestionCMI.cmi_PlanesAccionIndicador(InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
            Label67.Text = "Plan de Acción: " + InfoGrid.Rows[IdexRow]["Descripcion"].ToString().Trim();
            DataTable dtInfoEst1 = new DataTable();
            DataTable dtInfoEst2 = new DataTable();
            cumplimiento = "";
            color = "";

            dtInfoEst1 = cGestionCMI.PorcentjeCumplimiento("2", InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim(),DropDownListFechaMes.SelectedValue.ToString().Trim(),DropDownListFechaAno.SelectedValue.ToString());
            if (dtInfoEst1.Rows.Count > 0)
            {
                cumplimiento = dtInfoEst1.Rows[0]["Cumplimiento"].ToString().Trim();
                dtInfoEst2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                color = dtInfoEst2.Rows[0]["Color"].ToString().Trim();
                if (color == "Green")
                {
                    Image18.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                }
                if (color == "Orange")
                {
                    Image18.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                }
                if (color == "Red")
                {
                    Image18.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                }
                //Label63.Text = cumplimiento + " %";
            }
            else
            {
                DataTable VerIndicadores = new DataTable();
                VerIndicadores = cGestionCMI.cmi_VerIndicadoresPlaAccion(InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
                if (VerIndicadores.Rows[0]["CantidadIndicadores"].ToString() == "0")
                {
                    Image18.ToolTip = "Sin indicadores asociados";
                    Image18.ImageUrl = "~/Imagenes/Aplicacion/SinInfo.png";
                    //Label63.Text = "";
                }
                else
                {
                    Image18.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    //Label63.Text = "00" + " %";
                }
            }

            dtInfo = cGestionCMI.cmi_Gestiones(InfoGrid.Rows[IdexRow]["IdPlanAccion"].ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridge.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdPlanAccion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Fecha"].ToString().Trim(),
                                                    });
                }
                GridViewGestion.DataSource = InfoGridge;
                GridViewGestion.DataBind();
            }
        }

        private void creargrafico(Panel pnlMainGrafico, String NombreObjetivo, String IdObjetivo,String Consulta)
        {
            Table MyTable1 = new Table();
            TableCell tCell0 = new TableCell();
            TableCell tCell1 = new TableCell();
            TableCell tCell2 = new TableCell();
            TableRow tRow0 = new TableRow();
            TableRow tRow1 = new TableRow();
            TableRow tRow2 = new TableRow();
            Image img1 = new Image();
            img1.Width = 20;
            Image img1Hoy = new Image();
            img1Hoy.Width = 20;
            Image img1Con = new Image();
            img1Con.Width = 20;
            
            //Graficos de cumplimiento
            DataTable dtInfo1 = new DataTable();
            DataTable dtInfo2 = new DataTable();
            DataTable dtInfo1Hoy = new DataTable();
            DataTable dtInfo2Hoy = new DataTable();
            DataTable dtInfo1Con = new DataTable();
            DataTable dtInfo2Con = new DataTable();
            cumplimiento = "";
            cumplimientoHoy = "";
            cumplimientoCon = "";
            color = "";
            colorHoy = "";
            colorCon = "";
            DataTable VerIndicadores = new DataTable();
            VerIndicadores = cGestionCMI.cmi_VerIndicadoresObjetivos(IdObjetivo);
            tCell2.Controls.Add(img1);
            if (VerIndicadores.Rows[0]["CantidadIndicadores"].ToString() == "0")
            {
                img1.ToolTip = "Sin indicadores asociados";
                img1.ImageUrl = "~/Imagenes/Aplicacion/SinInfo.png";
            }
            else
            {
                //Mes
                dtInfo1 = cGestionCMI.PorcentjeCumplimiento("4", IdObjetivo, FechaMes, FechaAno);
                if (dtInfo1.Rows.Count > 0)
                {
                    cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                    color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                    img1.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                    if (color == "Green")
                    {
                        img1.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (color == "Orange")
                    {
                        img1.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (color == "Red")
                    {
                        img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                else
                {
                    img1.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                    img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                }

                //Hoy Crear Grafico
                //dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("4", IdObjetivo, DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                //if (dtInfo1Hoy.Rows.Count > 0)
                //{
                //    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                //    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                //    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                //    img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                //    tCell2.Controls.Add(img1Hoy);
                //    if (colorHoy == "Green")
                //    {
                //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                //    }
                //    if (colorHoy == "Orange")
                //    {
                //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                //    }
                //    if (colorHoy == "Red")
                //    {
                //        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //    }
                //}
                //else
                //{
                //    img1Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                //    img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                //}
                dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("4", IdObjetivo, DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                if (dtInfo1Hoy.Rows.Count > 0)
                {
                    cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                    dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                    colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                    img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                    tCell2.Controls.Add(img1Hoy);
                    if (colorHoy == "Green")
                    {
                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (colorHoy == "Orange")
                    {
                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (colorHoy == "Red")
                    {
                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }
                else
                {
                    dtInfo1 = cGestionCMI.PorcentjeCumplimiento("4", IdObjetivo, DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                    if (dtInfo1.Rows.Count > 0)
                    {
                        cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                        dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                        colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                        img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                        tCell2.Controls.Add(img1Hoy);
                        if (colorHoy == "Green")
                        {
                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorHoy == "Orange")
                        {
                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorHoy == "Red")
                        {
                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                        tCell2.Controls.Add(img1Hoy);
                    }
                    else
                    {
                        img1Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        tCell2.Controls.Add(img1Hoy);
                    }
                }

                //Consolidado
                //30-01-2014
                //if (System.DateTime.Now.Month == Convert.ToInt16(DropDownListFechaMes.SelectedValue.ToString()) && System.DateTime.Now.Year == Convert.ToInt16(DropDownListFechaAno.SelectedValue.ToString()))
                //if (dtInfo1Hoy.Rows.Count > 0)
                //{
                //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoy("4", IdObjetivo, DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                //    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                //}
                //else
                //{
                //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("4", IdObjetivo);
                //    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                //}
                dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("4", IdObjetivo);
                cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                if (cumplimientoCon != "")
                {
                    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                }
                else
                {
                    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("4", IdObjetivo);
                    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                }
                //Fin

                //dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));
                //colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                img1Con.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                tCell2.Controls.Add(img1Con);
                if (colorCon == "Green")
                {
                    img1Con.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                }
                if (colorCon == "Orange")
                {
                    img1Con.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                }
                if (colorCon == "Red")
                {
                    img1Con.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                }
            }
            //
            
            Chart grafico1 = new Chart();
            Series serie1 = new Series();
            ChartArea char1 = new ChartArea();
            grafico1.Width = 180;
            grafico1.Height = 80;
            grafico1.Series.Add(serie1);
            grafico1.ChartAreas.Add(char1);
            serie1.ChartType = SeriesChartType.Spline;
            serie1.XValueMember = "Periodo";
            serie1.YValueMembers = "Cumplimiento";
            serie1.BorderWidth = 5;
            serie1.Color = System.Drawing.Color.Black;
            serie1.ToolTip = "Periodo: #VALX\nCumplimiento: #VAL" + " %";
            char1.BackColor = System.Drawing.Color.AliceBlue;
            char1.AxisY.LineColor = System.Drawing.Color.Transparent;
            char1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            char1.AxisY.LabelStyle.Enabled = false;
            char1.AxisX.LineColor = System.Drawing.Color.Transparent;
            char1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            char1.AxisX.LabelStyle.Enabled = false;
            grafico1.BackColor = System.Drawing.Color.AliceBlue;
            grafico1.DataSourceID = Consulta;
            MyTable1.BackColor = System.Drawing.Color.AliceBlue;
            MyTable1.ForeColor = System.Drawing.Color.SlateGray;
            MyTable1.BorderColor = System.Drawing.Color.Silver;
            MyTable1.Width = grafico1.Width;
            MyTable1.Height = grafico1.Height;
            MyTable1.BorderWidth = 2;
            MyTable1.Rows.Add(tRow0);
            MyTable1.Rows.Add(tRow1);
            MyTable1.Rows.Add(tRow2);
            tCell0.Text = NombreObjetivo;
            tCell0.Font.Name = "Calibri";
            tCell0.Font.Size = 10;
            tCell0.ForeColor = System.Drawing.Color.Black;
            tCell0.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            tCell1.Controls.Add(grafico1);
            tCell2.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            tRow0.Cells.Add(tCell0);
            tRow1.Cells.Add(tCell1);
            tRow2.Cells.Add(tCell2);
            pnlMainGrafico.Controls.Add(MyTable1);
            pnlMainGrafico.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"));
        }

        private void CrearObjetivoEstrategico(Panel Panel, String CodigoEstrategia, int cont)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cGestionCMI.cmi_objetivos(codigo, CodigoEstrategia);
                String codigoobj;
                Table MyTable1 = new Table();
                TableCell tCell1 = new TableCell();
                TableCell sCell1 = new TableCell();
                TableCell sCell1Hoy = new TableCell();
                TableCell sCell1Con = new TableCell();
                TableCell sCell2 = new TableCell();
                TableRow tRow1 = new TableRow();
                TableRow sRow1 = new TableRow();
                Image img1 = new Image();
                Image img1Hoy = new Image();
                Image img1Con = new Image();
                ImageButton img2 = new ImageButton();
                img2.ImageUrl = "~/Imagenes/Icons/Lupa.png";
                img2.Width = 25;
                img2.ToolTip = "Ver detalle";
                img1.Width = 25;
                img1Hoy.Width = 25;
                img1Con.Width = 25;
                DataTable dtInfo1 = new DataTable();
                DataTable dtInfo2 = new DataTable();
                DataTable dtInfo1Hoy = new DataTable();
                DataTable dtInfo2Hoy = new DataTable();
                DataTable dtInfo1Con = new DataTable();
                DataTable dtInfo2Con = new DataTable();
                cumplimiento = "";
                cumplimientoHoy = "";
                cumplimientoCon = "";
                color = "";
                colorHoy = "";
                colorCon = "";
                codigoobj = dtInfo.Rows[cont]["IdObjetivo"].ToString().Trim();
                DataTable VerIndicadores = new DataTable();
                VerIndicadores = cGestionCMI.cmi_VerIndicadoresObjetivos(codigoobj);
                sCell1.Controls.Add(img1);
                if (VerIndicadores.Rows[0]["CantidadIndicadores"].ToString() == "0")
                {
                    img1.ToolTip = "Sin indicadores asociados";
                    img1.ImageUrl = "~/Imagenes/Aplicacion/SinInfo.png";
                }
                else
                {
                    //Mes
                    dtInfo1 = cGestionCMI.PorcentjeCumplimiento("4", codigoobj, FechaMes, FechaAno);
                    if (dtInfo1.Rows.Count > 0)
                    {
                        cumplimiento = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                        dtInfo2 = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimiento));
                        color = dtInfo2.Rows[0]["Color"].ToString().Trim();
                        img1.ToolTip = "Cumplimiento Mes Anterior: " + cumplimiento + " %";
                        if (color == "Green")
                        {
                            img1.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (color == "Orange")
                        {
                            img1.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (color == "Red")
                        {
                            img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                    }
                    else
                    {
                        img1.ToolTip = "Cumplimiento Mes Anterior: 00" + " %";
                        img1.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }

                    //Hoy Mapa Estrategico
                    dtInfo1Hoy = cGestionCMI.PorcentjeCumplimientoHoy("4", codigoobj, DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                    if (dtInfo1Hoy.Rows.Count > 0)
                    {
                        cumplimientoHoy = dtInfo1Hoy.Rows[0]["Cumplimiento"].ToString().Trim();
                        dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                        colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                        img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                        sCell1.Controls.Add(img1Hoy);
                        if (colorHoy == "Green")
                        {
                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                        }
                        if (colorHoy == "Orange")
                        {
                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                        }
                        if (colorHoy == "Red")
                        {
                            img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        }
                    }
                    else
                    {
                        dtInfo1 = cGestionCMI.PorcentjeCumplimiento("4", codigoobj, DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                        if (dtInfo1.Rows.Count > 0)
                        {
                            cumplimientoHoy = dtInfo1.Rows[0]["Cumplimiento"].ToString().Trim();
                            dtInfo2Hoy = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoHoy));
                            colorHoy = dtInfo2Hoy.Rows[0]["Color"].ToString().Trim();
                            img1Hoy.ToolTip = "Cumplimiento Hoy: " + cumplimientoHoy + " %";
                            sCell1.Controls.Add(img1Hoy);
                            if (colorHoy == "Green")
                            {
                                img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                            }
                            if (colorHoy == "Orange")
                            {
                                img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                            }
                            if (colorHoy == "Red")
                            {
                                img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                            }
                            sCell1.Controls.Add(img1Hoy);
                        }
                        else
                        {
                        img1Hoy.ToolTip = "Cumplimiento Hoy: 00" + " %";
                        img1Hoy.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                        sCell1.Controls.Add(img1Hoy);
                        }
                    }

                    //Consolidado
                    //30-01-2014
                    //if (System.DateTime.Now.Month == Convert.ToInt16(DropDownListFechaMes.SelectedValue.ToString()) && System.DateTime.Now.Year == Convert.ToInt16(DropDownListFechaAno.SelectedValue.ToString()))
                    //if (dtInfo1Hoy.Rows.Count > 0)
                    //{
                    //    //dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoy("4", codigoobj, DropDownListFechaMes.SelectedValue.ToString().Trim(), DropDownListFechaAno.SelectedValue.ToString());
                    //    //cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                    //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("4", codigoobj);
                    //    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                    //}
                    //else
                    //{
                    //    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("4", codigoobj);
                    //    cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                    //}
                    dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidadoHoyNew("4", codigoobj);
                    cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                    if (cumplimientoCon != "")
                    {
                        cumplimientoCon = dtInfo1Con.Rows[0]["ConsolidadoHoy"].ToString().Trim();
                    }
                    else
                    {
                        dtInfo1Con = cGestionCMI.PorcentjeCumplimientoConsolidado("4", codigoobj);
                        cumplimientoCon = dtInfo1Con.Rows[0]["Consolidado"].ToString().Trim();
                    }
                    //Fin

                    if (string.IsNullOrEmpty(cumplimientoCon))
                        cumplimientoCon = "0";

                    dtInfo2Con = cGestionCMI.cmi_Color(Convert.ToInt32(cumplimientoCon));

                    colorCon = dtInfo2Con.Rows[0]["Color"].ToString().Trim();
                    img1Con.ToolTip = "Cumplimiento Consolidado: " + cumplimientoCon + " %";
                    sCell1.Controls.Add(img1Con);
                    if (colorCon == "Green")
                    {
                        img1Con.ImageUrl = "~/Imagenes/Aplicacion/Arriba.png";
                    }
                    if (colorCon == "Orange")
                    {
                        img1Con.ImageUrl = "~/Imagenes/Aplicacion/Igual.png";
                    }
                    if (colorCon == "Red")
                    {
                        img1Con.ImageUrl = "~/Imagenes/Aplicacion/Abajo.png";
                    }
                }

                MyTable1.BackColor = System.Drawing.Color.AliceBlue;
                MyTable1.ForeColor = System.Drawing.Color.SlateGray;
                MyTable1.BorderColor = System.Drawing.Color.Silver;
                MyTable1.Width = 150;
                MyTable1.Height = 80;
                MyTable1.BorderWidth = 2;
                MyTable1.ToolTip = "Código Objetivo : " + dtInfo.Rows[cont]["CodigoObjetivo"].ToString().Trim() + "\rInicio : " + dtInfo.Rows[cont]["Inicio"].ToString().Trim() + "\rFin : " + dtInfo.Rows[cont]["Fin"].ToString().Trim();
                MyTable1.Rows.Add(tRow1);
                tRow1.Cells.Add(tCell1);
                tCell1.Text = dtInfo.Rows[cont]["Descripcion"].ToString().Trim();
                tCell1.Font.Name = "Calibri";
                tCell1.Font.Size = 10;
                tCell1.ColumnSpan = 2;
                MyTable1.Rows.Add(sRow1);
                sRow1.Cells.Add(sCell1);
                sRow1.Cells.Add(sCell2);
                sCell1.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                //Lupa
                sCell2.Controls.Add(img2);
                sCell2.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                img2.CommandArgument = dtInfo.Rows[cont]["IdObjetivo"].ToString().Trim();
                img2.Click += new System.Web.UI.ImageClickEventHandler(NuevoBtn_Click);
                Panel.Controls.Add(MyTable1);
                Panel.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"));
            }
            catch (Exception )
            {
                Mensaje("Indicador(es) sin cumplimiento.");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ocultarformas();
            BtnMapa.Enabled = false;
            Image8.Visible = true;
            TbFiltroPE.Visible = true;
            DropDownListPE.Enabled = true;
            TbInfoReporte.Visible = true;
            ocultarfecha();
            CargarCuadrosObjetivosEstrategicos();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ocultarformas();
            BtnOBJ.Enabled = false;
            Image9.Visible = true;
            DropDownListPE.Enabled = false;
            Trobj.Visible = true;
            Button3.Visible = false;
            CargarCuadrosEstrategias();
        }

        protected void DropDownListOBJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Image11.Visible == true)
            {
                if (DropDownListOBJ.SelectedValue != "---")
                {
                    CargaEstrategias();
                }
            }
        }
    }
}
