using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Alertas
{
    public partial class SAlertas : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "6004";
        private cAlertas cAlertas = new cAlertas();

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
            }
        }

        private void inicializarValores()
        {
            IdexRow = 0;
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdSenal", typeof(string));
            grid.Columns.Add("NombreSenal", typeof(string));
            grid.Columns.Add("TipoCliente", typeof(string));
            grid.Columns.Add("Frecuencia", typeof(string));
            grid.Columns.Add("CantidadGiros", typeof(string));
            grid.Columns.Add("SumaGiros", typeof(string));
            grid.Columns.Add("ValorMinGiro", typeof(string));
            grid.Columns.Add("ValorMaxGiro", typeof(string));
            grid.Columns.Add("CantidadOficinas", typeof(string));
            grid.Columns.Add("TipoIdentificacion", typeof(string));
            GridViewPlan.DataSource = grid;
            GridViewPlan.DataBind();
            InfoGrid = grid;
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAlertas.VerAlertas();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdSenal"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["NombreSenal"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["TipoCliente"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["Frecuencia"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CantidadGiros"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["SumaGiros"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["ValorMinGiro"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["ValorMaxGiro"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["CantidadOficinas"].ToString().Trim(),
                                                    dtInfo.Rows[rows]["TipoIdentificacion"].ToString().Trim(),
                                                    });
                }
                GridViewPlan.DataSource = InfoGrid;
                GridViewPlan.DataBind();
            }
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

        private void agregarPlanEstrategico()
        {
            //cGestion.agregarPlan(TextBox11.Text.Trim(), TextBox1.Text.Trim(), TextBox3.Text.Trim(), TextBox4.Text.Trim());
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox7.Text = "";
        }
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

        private DataTable infGridvision;
        private DataTable InfoGridVision
        {
            get
            {
                infGridvision = (DataTable)ViewState["infGridvision"];
                return infGridvision;
            }
            set
            {
                infGridvision = value;
                ViewState["infGridvision"] = infGridvision;
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

        #endregion

        private void modificarLista()
        {
            agregarPlanEstrategico();
        }

        private void verModificar()
        {
            TbModificarPlan.Visible = true;
            Label1.Visible = true;
            Label2.Visible = true;
            BtnModificaPlan.Visible = true;
            BtnGuardarPla.Visible = false;
            //TbAdicionaPlan.Visible = false;
        }

        protected void GridViewPlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IdexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    verModificar();
                    modificar();
                    break;
                case "Analizar":
                    modificar();
                    analizar();
                    break;
            }
        }

        private void modificar()
        {
            resetValues();
            Label2.Text = InfoGrid.Rows[IdexRow]["IdSenal"].ToString().Trim();
            TextBox1.Text = InfoGrid.Rows[IdexRow]["NombreSenal"].ToString().Trim();

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                 if (DropDownList1.SelectedItem.Text == InfoGrid.Rows[IdexRow]["TipoCliente"].ToString().Trim())
                {
                    break;
                }
            }
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                 if (DropDownList2.SelectedItem.Text == InfoGrid.Rows[IdexRow]["Frecuencia"].ToString().Trim())
                {
                    break;
                }
            }
            TextBox2.Text = InfoGrid.Rows[IdexRow]["CantidadGiros"].ToString().Trim();
            TextBox3.Text = InfoGrid.Rows[IdexRow]["SumaGiros"].ToString().Trim();
            TextBox4.Text = InfoGrid.Rows[IdexRow]["ValorMinGiro"].ToString().Trim();
            TextBox5.Text = InfoGrid.Rows[IdexRow]["ValorMaxGiro"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[IdexRow]["TipoIdentificacion"].ToString().Trim();
            TextBox1.Focus();
        }

        private void analizar()
        {
            DataTable DtSenales = new DataTable();
            try
            {
                String condicion = "";
                if (Label2.Text == "16")
                {
                    condicion = "select a.PuntoVentaGiro,a.PuntoVentaGiro,b.NombrePunto,b.CategoriaCaptacion,sum(a.ValorGiro),c.Captacion_Envio_Max from Proceso.ArchivoGiros a, Proceso.ArchivoOficinas b,Proceso.ArchivoOficinasRangos c where a.PuntoVentaGiro = b.CodigoPunto and b.CategoriaCaptacion = c.Categoria group by a.PuntoVentaGiro,b.NombrePunto,b.CategoriaCaptacion,Captacion_Envio_Max having sum(a.ValorGiro) > c.Captacion_Envio_Max";
                }
                else if (DropDownList1.SelectedItem.Text == "Remitentes")
                {
                    condicion += "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having COUNT(*) > 0";
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "")
                    {
                        condicion = "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro > " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion = "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro > " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " and ValorGiro < " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text) + " group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) == "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion = "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro > " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text) + " group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox2.Text) != "")
                    {
                        condicion += " and  COUNT(*) > " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox3.Text) != "")
                    {

                        if (Label2.Text == "5")
                        {
                            condicion += " or sum(ValorGiro) > " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
                        }
                        else
                        {
                            condicion += " and sum(ValorGiro) > " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
                        }

                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox7.Text) != "")
                    {
                        condicion += " and TipoIdenRemitente in (" + Sanitizer.GetSafeHtmlFragment(TextBox7.Text) + ")";
                    }
                    //************************************************************************************
                }
                else if (DropDownList1.SelectedItem.Text == "Beneficiarios")
                {
                    condicion += "select TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros group by TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario having COUNT(*) > 0";
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "")
                    {
                        condicion = "select TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro > " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " group by TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion = "select TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro > " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " and ValorGiro < " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text) + " group by TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) == "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion = "select TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro > " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text) + " group by TipoIdenBeneficiario,NumeIdenBeneficiario,NombreBeneficiario having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox2.Text) != "")
                    {
                        condicion += " and  COUNT(*) > " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox3.Text) != "")
                    {
                        condicion += " and sum(ValorGiro) > " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox7.Text) != "")
                    {
                        condicion += " and TipoIdenBeneficiario in (" + Sanitizer.GetSafeHtmlFragment(TextBox7.Text) + ")";
                    }
                    //*********************************************************
                }
                else if (DropDownList1.SelectedItem.Text == "Remitentes y Beneficiarios")
                {
                    condicion += "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having NumeIdenRemitente in (select b.NumeIdenBeneficiario from Proceso.ArchivoGiros b) and  COUNT(*) > 0";

                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "")
                    {
                        condicion = "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro  > " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having COUNT(*) > 0";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion = "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro  > " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " and ValorGiro  < " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text) + " group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having COUNT(*) > 0";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) == "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion = "select TipoIdenRemitente,NumeIdenRemitente,NombreRemitente,COUNT(*),SUM(ValorGiro) from Proceso.ArchivoGiros where ValorGiro  < " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " group by TipoIdenRemitente,NumeIdenRemitente,NombreRemitente having COUNT(*) > 0";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox2.Text) != "")
                    {
                        condicion += " and  COUNT(*) > " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox3.Text) != "")
                    {
                        condicion += " and sum(ValorGiro) > " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox7.Text) != "")
                    {
                        condicion += " and TipoIdenRemitente in (" + Sanitizer.GetSafeHtmlFragment(TextBox7.Text) + ")";
                    }
                    //***************************************************************************************
                }
                else
                {
                    condicion += "select TipoIdenGanador,NumeIdenGanador,NombreGanador,COUNT(*),SUM(ValorPremio) from Proceso.ArchivoPremios group by TipoIdenGanador,NumeIdenGanador,NombreGanador having COUNT(*) > 0";

                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "")
                    {
                        condicion = "select TipoIdenGanador,NumeIdenGanador,NombreGanador,COUNT(*),SUM(ValorPremio) from Proceso.ArchivoPremios where ValorPremio > " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text) + " group by TipoIdenGanador,NumeIdenGanador,NombreGanador having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) == "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion = "select TipoIdenGanador,NumeIdenGanador,NombreGanador,COUNT(*),SUM(ValorPremio) from Proceso.ArchivoPremios where ValorPremio > " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text) + " group by TipoIdenGanador,NumeIdenGanador,NombreGanador having COUNT(*) > 0 ";
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "" & Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
                    {
                        condicion += " and ValorPremio < " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox2.Text) != "")
                    {
                        condicion += " and  COUNT(*) > " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox3.Text) != "")
                    {
                        condicion += " and sum(ValorPremio) > " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
                    }
                    if (Sanitizer.GetSafeHtmlFragment(TextBox7.Text) != "")
                    {
                        condicion += " and TipoIdenGanador in (" + Sanitizer.GetSafeHtmlFragment(TextBox7.Text) + ")";
                    }
                    if (Label2.Text == "12")
                    {
                        condicion = "SELECT TipoIdenGanador,NumeIdenGanador,NombreGanador,COUNT(*),SUM(ValorPremio) FROM Proceso.ArchivoPremios group by TipoIdenGanador,NumeIdenGanador,NombreGanador having TipoIdenGanador is null AND NumeIdenGanador is null or (TipoIdenGanador = 0 and NumeIdenGanador = 0)";
                    }
                }


                DtSenales=cAlertas.AnalizarAlertas(condicion);
                
                if (DtSenales.Rows.Count > 0)
                {
                    cAlertas.InsertConteoRegistro(DtSenales.Rows.Count);
                    DataTable c = new DataTable();
                    int Conteo = 0;
                    c = cAlertas.CantidadConteos();
                    if (c.Rows[0][0].ToString() == "0")
                    {
                        Conteo = 1;
                    }
                    else
                    {
                        Conteo = Convert.ToInt16(c.Rows[0][0].ToString());
                    }
                    for (int a = 0; a < DtSenales.Rows.Count; a++)
                    {
                        cAlertas.InsertRegistroOperacion(Session["idUsuario"].ToString().Trim(), DtSenales.Rows[a][1].ToString().Trim(), DtSenales.Rows[a][2].ToString().Trim(), Conteo, DtSenales.Rows[a][3].ToString().Trim(), DtSenales.Rows[a][4].ToString().Trim(), DropDownList2.SelectedItem.Text, DropDownList1.SelectedItem.Text,Label2.Text, Sanitizer.GetSafeHtmlFragment(TextBox1.Text));
                    }

                }
                Mensaje("Señal de alerta analizada correctamente." + "\rResultado: " + DtSenales.Rows.Count + " registro(s) detectado(s)");
            }
            catch (Exception ex)
            {
                Mensaje("Error analizar señales de alerta." + ex.Message);
            }
        }
 
        private void modificarPlanEstrategico()
        {
            string condicion = "UPDATE Proceso.SenalesAlerta SET ";

            if (Sanitizer.GetSafeHtmlFragment(TextBox1.Text) != "")
            {
                condicion += "NombreSenal = '" + Sanitizer.GetSafeHtmlFragment(TextBox1.Text) + "'";
            }
            else
            {
                condicion += "NombreSenal = NULL";
            }

            if (DropDownList1.SelectedItem.Text != "")
            {
                condicion += ", TipoCliente = '" + DropDownList1.SelectedItem.Text + "'";
            }
            else
            {
                condicion += ", TipoCliente = NULL";
            }

            if (DropDownList2.SelectedItem.Text != "")
            {
                condicion += ", Frecuencia = '" + DropDownList2.SelectedItem.Text + "'";
            }
            else
            {
                condicion += ", Frecuencia = NULL";
            }


            if (Sanitizer.GetSafeHtmlFragment(TextBox2.Text) != "")
            {
                condicion += ", CantidadGiros = " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text);
            }
            else
            {
                condicion += ", CantidadGiros = NULL";
            }
            if (Sanitizer.GetSafeHtmlFragment(TextBox3.Text) != "")
            {
                condicion += ", SumaGiros = " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text);
            }
            else
            {
                condicion += ", SumaGiros = NULL";
            }
            if (Sanitizer.GetSafeHtmlFragment(TextBox4.Text) != "")
            {
                condicion += ", ValorMinGiro = " + Sanitizer.GetSafeHtmlFragment(TextBox4.Text);
            }
            else
            {
                condicion += ", ValorMinGiro = NULL";
            }
            if (Sanitizer.GetSafeHtmlFragment(TextBox5.Text) != "")
            {
                condicion += ", ValorMaxGiro = " + Sanitizer.GetSafeHtmlFragment(TextBox5.Text);
            }
            else
            {
                condicion += ", ValorMaxGiro = NULL";
            }
            if (Sanitizer.GetSafeHtmlFragment(TextBox7.Text) != "")
            {
                condicion += ", TipoIdentificacion = '" + Sanitizer.GetSafeHtmlFragment(TextBox7.Text) + "'";
            }
            else
            {
                condicion += ", TipoIdentificacion = NULL";
            }
            condicion += " WHERE IdSenal = " + Label2.Text;

            cAlertas.modificarAlerta(condicion);
            
        }

        protected void BtnModificaPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    modificarPlanEstrategico();
                    loadGrid();
                    cargarInfoGrid();
                    TbModificarPlan.Visible = false;
                    BtnModificaPlan.Visible = false;
                    Mensaje("Señal de alerta modificada correctamente.");
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar Señal de alerta." + ex.Message);
            }

        }

        protected void BtnAdicionaPlan_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
            TbModificarPlan.Visible = true;
            BtnGuardarPla.Visible = true;
            BtnModificaPlan.Visible = false;
            Label1.Visible = false;
            Label2.Visible = false;
            TextBox1.Focus();
        }

        protected void BtnCalcelModPlan_Click(object sender, ImageClickEventArgs e)
        {
            TbModificarPlan.Visible = false;
            BtnGuardarPla.Visible = false;
            BtnModificaPlan.Visible = false;

        }

        protected void BtnGuardarPlan_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (TextBox2.Text == "")
                {
                    TextBox2.Text = "NULL";
                }
                if (TextBox3.Text == "")
                {
                    TextBox3.Text = "NULL";
                }
                if (TextBox4.Text == "")
                {
                    TextBox4.Text = "NULL";
                }
                if (TextBox5.Text == "")
                {
                    TextBox5.Text = "NULL";
                }
                cAlertas.insertarrAlerta(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), DropDownList1.SelectedItem.Text, DropDownList2.SelectedItem.Text.Trim(), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()));
                Mensaje("Señal de alerta creada correctamete");
                loadGrid();
                cargarInfoGrid();
                TbModificarPlan.Visible = false;
                BtnGuardarPla.Visible = false;
            }
            catch (Exception ex)
            {
                Mensaje1("Error al guardar señal de alerta" + ex.Message);
            }
        }
    }
}