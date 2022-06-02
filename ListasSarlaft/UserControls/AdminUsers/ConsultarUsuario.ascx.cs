using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls
{
    public partial class ConsultarUsuario : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        DataTable CantidadUsuarios = new DataTable();
        String IdFormulario = "1002";
        //String CantidadUsuariosFull = "9999";
        //String CantidadUsuariosEventos = "9999";
        int CantidadUsuariosFull;
        int CantidadUsuariosEventos;
        DataTable dtInfoUsuarioProceso = new DataTable();
        clsDTOUsuarios Users = new clsDTOUsuarios();
        clsBLLUsuarios UsersBLL = new clsBLLUsuarios();
        string strVarTemporalMacro = string.Empty, strVarTemporalProc = string.Empty;
        bool booVarTemporal = false;

        #region Properties

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

        private int rowGrid;
        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["IdUsuario"].ToString().Trim()))
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
            else
            {
                dtInfoUsuarioProceso = cCuenta.ConsultarUsuario(Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim()));

                if (cCuenta.permisosConsulta(IdFormulario) == "False")
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadDDLRol();
                inicializarValores();
                //loadGrid();
                PopulateTreeView();
            }
        }

        private void CargarNoUsuarios()
        {
            int CantDigitosFull = System.Configuration.ConfigurationManager.AppSettings["System.Version.NuA"].Length - 18;
            CantidadUsuariosFull = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["System.Version.NuA"].Substring(18, CantDigitosFull));

            int CantDigitosEventos = System.Configuration.ConfigurationManager.AppSettings["System.Version.NuE"].Length - 18;
            CantidadUsuariosEventos = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["System.Version.NuE"].Substring(18, CantDigitosEventos));
        }

        #region Arbol (TreeView)
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView1.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {

            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView1.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

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
            TextBox34.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia1.Text = TreeView1.SelectedNode.Value;
            String nombre = cCuenta.NombreDetalleJerarquia(TreeView1.SelectedNode.Value.Trim());
            string[] strSeparator = new string[] { " " };
            string[] arrName = nombre.Split(strSeparator, StringSplitOptions.None);
            int i = arrName.Length;
            switch (i)
            {
                case 1:
                    TextBox5.Text = arrName[0].Trim();
                    break;
                case 2:
                    TextBox5.Text = arrName[0].Trim();
                    TextBox6.Text = arrName[1].Trim();
                    break;
                case 3:
                    TextBox5.Text = arrName[0].Trim();
                    TextBox6.Text = arrName[1].Trim() + " " + arrName[2].Trim();
                    break;
                case 4:
                    TextBox5.Text = arrName[0].Trim() + " " + arrName[1].Trim();
                    TextBox6.Text = arrName[2].Trim() + " " + arrName[3].Trim();
                    break;
                default:
                    TextBox5.Text = arrName[0].Trim() + " " + arrName[1].Trim();
                    TextBox6.Text = arrName[2].Trim() + " " + arrName[3].Trim();
                    break;
            }
        }
        #endregion Arbol (TreeView)

        #region Gridview
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            loadGrid();
            cargarInfoGrid();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    modificar();
                    break;
            }
        }
        #endregion Gridview

        #region Buttons
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                inicializarValores();
                loadGrid();
                cargarInfoGrid();
                //resetValues2();
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
                Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                //cCuenta.ResetContrasena(InfoGrid.Rows[RowGrid]["IdUsuario"].ToString().Trim());
                bool booResult = false;
                string strErrMsg = string.Empty;
                booResult = UsersBLL.mtRestPassUsuario(Convert.ToInt32(InfoGrid.Rows[RowGrid]["IdUsuario"].ToString().Trim()), ref strErrMsg);
                if (booResult == true)
                {
                    loadGrid();
                    cargarInfoGrid();
                    resetValues2();
                    Mensaje("Contraseña actualizada con exito.");
                }
                else
                {
                    Mensaje("Error en reiniciar la Contraseña. Descripción: " + strErrMsg);
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            resetValues();
        }

        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CargarNoUsuarios();
        //        if (cCuenta.permisosActualizar(IdFormulario) == "False")
        //            Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
        //        else
        //        {
        //            if (InfoGrid.Rows[RowGrid]["Usuario"].ToString().Trim().ToLower() != TextBox7.Text.Trim().ToLower())
        //            {
        //                validateUser();
        //            }
        //            else
        //            {
        //                if (DropDownList2.SelectedItem.Value == "0")
        //                    actualizarUsuario();
        //                else
        //                    actualizarUsuario();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("Error al modificar el usuario. " + ex.Message);
        //    }
        //}

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                CargarNoUsuarios();
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    Mensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                }
                else
                {
                    if (InfoGrid.Rows[RowGrid]["Usuario"].ToString().Trim().ToLower() != TextBox7.Text.Trim().ToLower())
                    {
                        validateUser();
                    }
                    else
                    {
                        if (DropDownList2.SelectedItem.Value == "0")
                        {
                            if (DropDownList1.SelectedItem.Value == "99")
                            {
                                CantidadUsuarios = cCuenta.ContarUsuariosEvento("99");
                                if (CantidadUsuariosEventos <= Convert.ToInt16(CantidadUsuarios.Rows[0]["Total"].ToString()))
                                {
                                    Mensaje1("No puede activar este usuario, ha llegado al límite de usuarios permitidos para este Rol");
                                }
                                else
                                {
                                    actualizarUsuario();
                                }
                            }
                            else
                            {
                                CantidadUsuarios = cCuenta.ContarUsuarios("99");
                                int a = Convert.ToInt16(CantidadUsuarios.Rows[0]["Total"].ToString());
                                if (CantidadUsuariosFull <= Convert.ToInt16(CantidadUsuarios.Rows[0]["Total"].ToString()))
                                {
                                    Mensaje1("No puede activar este usuario, ha llegado al límite de usuarios permitidos");
                                }
                                else
                                {
                                    actualizarUsuario();
                                }
                            }
                            //actualizarUsuario();
                        }
                        else
                        {
                            actualizarUsuario();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al modificar el usuario. " + ex.Message);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            resetValues2();
        }
        #endregion

        #region DropDownlists for MacroProceso and Proceso
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

            if (booVarTemporal)
            {
                DataTable dt = new DataTable();
                dt = cCuenta.LoadProceso(strVarTemporalMacro);
                int intIndex = 0;
                foreach (DataRow dr in dt.Select())
                {
                    intIndex++;
                    ddlProceso.Items.Insert(intIndex, new ListItem(dr["Nombre"].ToString().Trim(), dr["IdProceso"].ToString()));
                }

                if (strVarTemporalProc != "")
                    ddlProceso.SelectedValue = strVarTemporalProc;
            }
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                ddlProceso.Enabled = false;
                ddlMacroProceso.Enabled = false;
            }
            else
            {
                ddlProceso.Enabled = true;
                ddlMacroProceso.Enabled = true;
            }
        }

        #endregion DropDownlists for MacroProceso and Proceso

        private void modificar()
        {

            DataRow[] drInfo = dtInfoUsuarioProceso.Select("Usuario = '" + InfoGrid.Rows[RowGrid]["Usuario"].ToString().Trim() + "'");

            #region Ciclo DropdownList1
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.Trim() == InfoGrid.Rows[RowGrid]["NombreRol"].ToString().Trim())
                {
                    break;
                }
            }
            #endregion Ciclo DropdownList1

            TextBox4.Text = InfoGrid.Rows[RowGrid]["NumeroDocumento"].ToString().Trim();
            TextBox5.Text = InfoGrid.Rows[RowGrid]["Nombres"].ToString().Trim();
            TextBox6.Text = InfoGrid.Rows[RowGrid]["Apellidos"].ToString().Trim();
            TextBox7.Text = InfoGrid.Rows[RowGrid]["Usuario"].ToString().Trim();
            updateUser.Visible = true;
            string Bloqueado = "";

            switch (InfoGrid.Rows[RowGrid]["Bloqueado"].ToString().Trim())
            {
                case "0":
                    Bloqueado = "Activo";
                    break;
                case "1":
                    Bloqueado = "Bloqueado";
                    break;
            }

            #region Ciclo DropdownList2
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedItem.Text.Trim() == Bloqueado)
                {
                    break;
                }
            }
            #endregion Ciclo DropdownList2

            TextBox34.Text = InfoGrid.Rows[RowGrid]["NombreHijo"].ToString().Trim();
            lblIdDependencia1.Text = InfoGrid.Rows[RowGrid]["IdJerarquia"].ToString().Trim();

            #region Carga de Macroproceso y proceso
            try
            {
                if ((drInfo[0]["VerTodosProcesos"] == DBNull.Value))
                {
                    CheckBox1.Checked = false;
                    ddlMacroProceso.Enabled = true;
                    ddlProceso.Enabled = true;
                }
                else
                {
                    if (!Convert.ToBoolean(drInfo[0]["VerTodosProcesos"]))
                    {
                        CheckBox1.Checked = false;
                        ddlMacroProceso.Enabled = true;
                        ddlProceso.Enabled = true;
                    }
                    else
                    {
                        CheckBox1.Checked = true;
                        ddlMacroProceso.Enabled = false;
                        ddlProceso.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje1("Error al cargar Ver Todos Procesos. " + ex.Message);
            }

            try
            {
                if (drInfo[0]["IdMacroProcesoU"] != DBNull.Value)
                {
                    if (drInfo[0]["IdMacroProcesoU"].ToString() != "0")
                    {
                        ddlMacroProceso.SelectedValue = drInfo[0]["IdMacroProcesoU"].ToString().Trim();
                        strVarTemporalMacro = drInfo[0]["IdMacroProcesoU"].ToString().Trim();
                        strVarTemporalProc = drInfo[0]["IdProcesoU"].ToString().Trim();
                        booVarTemporal = true;
                        ddlMacroProceso_SelectedIndexChanged(ddlMacroProceso, EventArgs.Empty);
                    }
                }
                GridView1.Visible = false;
            }
            catch(Exception ex)
            {
                Mensaje1("Error al cargar MarcroProceso. " + ex.Message);
            }
            #endregion Carga de Macroproceso y proceso
        }
            

        private void validateUser()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cCuenta.validateUser(Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim()));

            if (dtInfo.Rows.Count == 0)
                actualizarUsuario();
            else
                Mensaje("El Nombre De Usuario Ya Se Encuentra Registrado.");
        }

        private void actualizarUsuario()
        {
            try
            {
                Users.intIdRol = Convert.ToInt32(DropDownList1.SelectedItem.Value.ToString().Trim());
                Users.strNumeroDocumento = Sanitizer.GetSafeHtmlFragment(TextBox4.Text.Trim());
                Users.strNombres = Sanitizer.GetSafeHtmlFragment(TextBox5.Text.Trim());
                Users.strApellidos = Sanitizer.GetSafeHtmlFragment(TextBox6.Text.Trim());
                Users.strUsuario = Sanitizer.GetSafeHtmlFragment(TextBox7.Text.Trim());
                Users.strContrasenaEncriptada = InfoGrid.Rows[RowGrid]["Contrasena"].ToString().Trim();
                Users.intBloqueado = Convert.ToInt32(DropDownList2.SelectedItem.Value.ToString().Trim());
                Users.intId = Convert.ToInt32(InfoGrid.Rows[RowGrid]["IdUsuario"].ToString().Trim());
                Users.intIdJerarquia = Convert.ToInt32(lblIdDependencia1.Text.Trim());
                if (ddlMacroProceso.SelectedItem.Value.ToString().Trim() != "0")
                    Users.intIdMacroprocesoU = Convert.ToInt32(ddlMacroProceso.SelectedItem.Value.ToString().Trim());
                else
                    Users.intIdMacroprocesoU = 0;
                if (ddlProceso.SelectedItem.Value.ToString().Trim() != "0")
                    Users.intIdProcesoU = Convert.ToInt32(ddlProceso.SelectedItem.Value.ToString().Trim());
                else
                    Users.intIdProcesoU = 0;
                Users.intVerTodosProcesos = Convert.ToInt32(CheckBox1.Checked);
                /*cCuenta.mtdActualizarUsuario(DropDownList1.SelectedItem.Value.ToString().Trim(), TextBox4.Text.Trim(), TextBox5.Text.Trim(),
                    TextBox6.Text.Trim(), TextBox7.Text.Trim(), InfoGrid.Rows[RowGrid]["Contrasena"].ToString().Trim(),
                    DropDownList2.SelectedItem.Value.ToString().Trim(), InfoGrid.Rows[RowGrid]["IdUsuario"].ToString().Trim(),
                    lblIdDependencia1.Text.Trim(), ddlMacroProceso.SelectedItem.Value.ToString().Trim(), ddlProceso.SelectedItem.Value.ToString().Trim(), CheckBox1.Checked);
                */
                bool boolResult = false;
                string strErrMsg = string.Empty;
                boolResult = UsersBLL.mtdActualizarUsuarios(Users, ref strErrMsg);
                if (boolResult == true)
                {
                    loadGrid();
                    cargarInfoGrid();
                    resetValues2();
                    ddlMacroProceso.SelectedIndex = 0;
                    ddlProceso.SelectedIndex = 0;
                    Mensaje("Usuario actualizado con éxito.");
                }
                else
                {
                    Mensaje("Error actualizando usuaro: Descripción: " + strErrMsg);
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error actualizando usuaro: Descripción: " + ex);
            }
        }

        private void loadDDLRol()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cCuenta.Roles();

                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i, new ListItem(dtInfo.Rows[i]["NombreRol"].ToString().Trim(), dtInfo.Rows[i]["IdRol"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar los roles. " + ex.Message);
            }
        }

        private void inicializarValores()
        {
            PagIndex = 0;
        }

        private void cargarInfoGrid()
        {
            DataTable dtInfo = new DataTable();
            //bool booResult = false;
            string strErrMsg = string.Empty;
            List<clsDTOUsuarios> lstUsuarios = new List<clsDTOUsuarios>();
            Users.strNumeroDocumento = Sanitizer.GetSafeHtmlFragment(TextBox1.Text.Trim());
            Users.strNombres = Sanitizer.GetSafeHtmlFragment(TextBox2.Text.Trim());
            Users.strApellidos = Sanitizer.GetSafeHtmlFragment(TextBox3.Text.Trim());
            //dtInfo = cCuenta.ConsultarUsuario(TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim());
            lstUsuarios = UsersBLL.mtdConsultarUsuarios(ref lstUsuarios, ref strErrMsg, Users);
            bool booResult = false;
            clsDALConUsuarios cDtRegistro = new clsDALConUsuarios();
            //booResult = cDtRegistro.mtdConsultarUsuarios(ref dtInfo, ref strErrMsg, Users);
            
            //dtInfoUsuarioProceso = dtInfo;
            //if (dtInfo.Rows.Count > 0)
            //{
            //    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
            //    {
            //        InfoGrid.Rows.Add(new Object[] {
            //            dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
            //            dtInfo.Rows[rows]["IdRol"].ToString().Trim(),
            //            dtInfo.Rows[rows]["NombreRol"].ToString().Trim(),
            //            dtInfo.Rows[rows]["IdJerarquia"].ToString().Trim(),
            //            dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
            //            dtInfo.Rows[rows]["NumeroDocumento"].ToString().Trim(),
            //            dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
            //            dtInfo.Rows[rows]["Apellidos"].ToString().Trim(),
            //            dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
            //            dtInfo.Rows[rows]["Bloqueado"].ToString().Trim(),
            //            dtInfo.Rows[rows]["Contrasena"].ToString().Trim()
            //        });
            //    }
            //}

            if (lstUsuarios != null)
            {
                mtdLoadInfoGridUsuarios(lstUsuarios);
                GridView1.PageIndex = PagIndex;
                GridView1.DataSource = InfoGrid;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("IdRol", typeof(string));
            grid.Columns.Add("NombreRol", typeof(string));
            grid.Columns.Add("IdJerarquia", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            grid.Columns.Add("NumeroDocumento", typeof(string));
            grid.Columns.Add("Nombres", typeof(string));
            grid.Columns.Add("Apellidos", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("Bloqueado", typeof(string));
            grid.Columns.Add("Contrasena", typeof(string));
            GridView1.DataSource = grid;
            GridView1.DataBind();
            GridView1.Visible = true;
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los Usuarios</param>
        private void mtdLoadInfoGridUsuarios(List<clsDTOUsuarios> lstUsuarios)
        {
            foreach (clsDTOUsuarios objUsuarios in lstUsuarios)
            {
                string text = string.Empty;
                if (objUsuarios.intBloqueado.ToString().Trim() == "0")
                    text = "Activo";
                else
                    text = "Bloqueado";
                InfoGrid.Rows.Add(new Object[] {
                    objUsuarios.intId.ToString().Trim(),
                            objUsuarios.intIdRol.ToString().Trim(),
                            objUsuarios.strNombreRol.ToString().Trim(),
                            objUsuarios.intIdJerarquia.ToString().Trim(),
                            objUsuarios.strCargo.ToString().Trim(),
                            objUsuarios.strNumeroDocumento.ToString().Trim(),
                            objUsuarios.strNombres.ToString().Trim(),
                            objUsuarios.strApellidos.ToString().Trim(),
                            objUsuarios.strUsuario.ToString().Trim(),
                            text,
                            objUsuarios.strContrasenaEncriptada.ToString().Trim()
                    });
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

        private void resetValues()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            resetValues2();
            /*loadGrid();
            cargarInfoGrid();*/
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        private void resetValues2()
        {
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            updateUser.Visible = false;
            TextBox34.Text = string.Empty;
            lblIdDependencia1.Text = string.Empty;

            ddlMacroProceso.ClearSelection();
            ddlProceso.ClearSelection();
            loadGrid();
            cargarInfoGrid();
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                inicializarValores();
                loadGrid();
                cargarInfoGrid();
                //resetValues2();
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la consulta. " + ex.Message);
            }
        }

        //protected void Button1_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        inicializarValores();
        //        loadGrid();
        //        cargarInfoGrid();
        //        resetValues2();
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje("Error al realizar la consulta. " + ex.Message);
        //    }
        //}

        //    protected void GridView1_PreRender(object sender, EventArgs e)
        //{
        //    for (int rowIndex = 0; rowIndex < GridView1.Rows.Count; rowIndex++)
        //    {
        //        GridViewRow row = GridView1.Rows[rowIndex];
        //        //GridViewRow previousRow = GridView1.Rows[rowIndex + 1];

        //        for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
        //        {
        //            //string text = ((Label)row.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;

        //            //string previousText = ((Label)previousRow.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;
        //            /*if (cellIndex == 0)
        //            {*/
        //            /*string text = ((Label)row.FindControl("DescripcionEntrada")).Text;
        //            string previousText = ((Label)previousRow.FindControl("DescripcionEntrada")).Text;*/
        //            if (cellIndex == 8)
        //            {
        //                string text = row.Cells[cellIndex].Text;
        //                //string previousText = previousRow.Cells[cellIndex].Text;
        //                if (text == "0")
        //                {
        //                    row.Cells[cellIndex].Text = "Activo";
        //                }
        //                else
        //                {
        //                    row.Cells[cellIndex].Text = "Bloqueado";
        //                }
        //            }
        //        }
        //    }
        //}




    }
}