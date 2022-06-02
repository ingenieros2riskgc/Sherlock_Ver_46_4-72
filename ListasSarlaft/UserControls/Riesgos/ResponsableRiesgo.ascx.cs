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
    public partial class ResponsableRiesgo : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();

        #region Properties
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

        private int rowGridResponsableRiesgo;
        private int RowGridResponsableRiesgo
        {
            get
            {
                rowGridResponsableRiesgo = (int)ViewState["rowGridResponsableRiesgo"];
                return rowGridResponsableRiesgo;
            }
            set
            {
                rowGridResponsableRiesgo = value;
                ViewState["rowGridResponsableRiesgo"] = rowGridResponsableRiesgo;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadDDLNivelResponsable();
                loadGridResponsableRiesgo();
                loadInfoResponsableRiesgo();
            }
        }

        private void loadInfoResponsableRiesgo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoResponsableRiesgo();
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridResponsableRiesgo.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdResponsableRiesgo"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["CodigoResponsableRiesgo"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["NombreResponsableRiesgo"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["IdNivelResponsable"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["NombreNivelResponsable"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["Email"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["PerteneceURS"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                                     dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim()
                                                                     });
                }
                GridView1.DataSource = InfoGridResponsableRiesgo;
                GridView1.DataBind();
            }
        }

        private void loadGridResponsableRiesgo()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdResponsableRiesgo", typeof(string));
            grid.Columns.Add("CodigoResponsableRiesgo", typeof(string));
            grid.Columns.Add("NombreResponsableRiesgo", typeof(string));
            grid.Columns.Add("IdNivelResponsable", typeof(string));
            grid.Columns.Add("NombreNivelResponsable", typeof(string));
            grid.Columns.Add("Email", typeof(string));
            grid.Columns.Add("PerteneceURS", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            InfoGridResponsableRiesgo = grid;
            GridView1.DataSource = InfoGridResponsableRiesgo;
            GridView1.DataBind();
        }

        private void loadCodigoResponsableRiesgo()
        {
            DataTable dtInfo = new DataTable();
            try
            {
                dtInfo = cRiesgo.loadCodigoResponsableRiesgo();
                if (dtInfo.Rows.Count > 0)
                {
                    TextBox1.Text = "RR" + dtInfo.Rows[0]["NumRegistros"].ToString().Trim();
                }
                else
                {
                    TextBox1.Text = "RR1";
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar el código responsable riesgo. " + ex.Message);
            }
        }

        private void loadDDLNivelResponsable()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLNivelResponsable();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreNivelResponsable"].ToString().Trim(), dtInfo.Rows[i]["IdNivelResponsable"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar nivel responsable. " + ex.Message);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
            loadCodigoResponsableRiesgo();
            tbCampos.Visible = true;
            ImageButton4.Visible = true;
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            resetValues();
            loadGridResponsableRiesgo();
            loadInfoResponsableRiesgo();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                String PerteneceURS = "0";
                if (CheckBox1.Checked)
                {
                    PerteneceURS = "1";
                }
                cRiesgo.registrarResponsable(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), PerteneceURS);
                resetValues();
                loadGridResponsableRiesgo();
                loadInfoResponsableRiesgo();
            }
            catch (Exception ex)
            {
                Mensaje("Error al registrar responsable riesgo. " + ex.Message);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                String PerteneceURS = "0";
                if (CheckBox1.Checked)
                {
                    PerteneceURS = "1";
                }
                cRiesgo.actualizarResponsable(InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["IdResponsableRiesgo"].ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()), PerteneceURS);
                resetValues();
                loadGridResponsableRiesgo();
                loadInfoResponsableRiesgo();
            }
            catch (Exception ex)
            {
                Mensaje("Error al actualizar responsable riesgo. " + ex.Message);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridResponsableRiesgo = Convert.ToUInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    detalleResponsable();
                    break;
                case "Borrar":
                    borrarResponsable();
                    break;
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            DropDownList1.SelectedIndex = 0;
            TextBox3.Text = "";
            CheckBox1.Checked = false;
            trUsuario.Visible = false;
            trFecha.Visible = false;
            ImageButton4.Visible = false;
            ImageButton5.Visible = false;
            tbCampos.Visible = false;
            tbResponsableRiesgo.Visible = true;
            Label9.Text = "";
            Label12.Text = "";
        }

        private void borrarResponsable()
        {
            try
            {
                cRiesgo.borrarResponsable(InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["IdResponsableRiesgo"].ToString().Trim());
                resetValues();
                loadGridResponsableRiesgo();
                loadInfoResponsableRiesgo();
            }
            catch (Exception ex)
            {
                Mensaje("Error al borrar responsable riesgo. " + ex.Message);
            }
        }

        private void detalleResponsable()
        {
            resetValues();
            tbCampos.Visible = true;
            ImageButton5.Visible = true;
            trUsuario.Visible = true;
            trFecha.Visible = true;
            TextBox1.Text = InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["CodigoResponsableRiesgo"].ToString().Trim();
            TextBox2.Text = InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["NombreResponsableRiesgo"].ToString().Trim();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedValue.ToString().Trim() == InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["IdNivelResponsable"].ToString().Trim())
                {
                    break;
                }
            }
            TextBox3.Text = InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["Email"].ToString().Trim();
            CheckBox1.Checked = false;
            if (InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["PerteneceURS"].ToString().Trim() == "True")
            {
                CheckBox1.Checked = true;
            }
            Label9.Text = InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["Usuario"].ToString().Trim();
            Label12.Text = InfoGridResponsableRiesgo.Rows[RowGridResponsableRiesgo]["FechaRegistro"].ToString().Trim();
        }
    }
}