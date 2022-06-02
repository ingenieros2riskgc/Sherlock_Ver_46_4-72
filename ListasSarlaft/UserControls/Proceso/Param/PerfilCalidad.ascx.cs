using ListasSarlaft.Classes;
using ListasSarlaft.Classes.DTO.Calidad;
using ListasSarlaft.Classes.Utilidades;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class PerfilCalidad : System.Web.UI.UserControl
    {
        string IdFormulario = "4011";
        cCuenta cCuenta = new cCuenta();
        clsPerfilBLL perfilBLL = new clsPerfilBLL();
        cParametrizacion Parametrizacion = new cParametrizacion();
        private static int LastInsertIdCE;

        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;

        private DataTable infoGrid3;
        private int rowGrid3;
        private int pagIndex3;

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

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

        private DataTable InfoGrid3
        {
            get
            {
                infoGrid3 = (DataTable)ViewState["infoGrid3"];
                return infoGrid3;
            }
            set
            {
                infoGrid3 = value;
                ViewState["infoGrid3"] = infoGrid3;
            }
        }

        private int RowGrid3
        {
            get
            {
                rowGrid3 = (int)ViewState["rowGrid3"];
                return rowGrid3;
            }
            set
            {
                rowGrid3 = value;
                ViewState["rowGrid3"] = rowGrid3;
            }
        }

        private int PagIndex3
        {
            get
            {
                pagIndex3 = (int)ViewState["pagIndex3"];
                return pagIndex3;
            }
            set
            {
                pagIndex3 = value;
                ViewState["pagIndex3"] = pagIndex3;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);
            scrtManager.RegisterPostBackControl(btnAgregarPDF);
            scrtManager.RegisterPostBackControl(GridView3);

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    if (mtdLoadDDLMacroProceso(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    CargarMacroprocesos();
                    CargarEstadosPerfil();
                    PopulateTreeView();
                    PopulateTreeViewAprueba();
                    Dictionary<string, string> datosAnteriores = new Dictionary<string, string>();
                    Session["DatosAnterioresPerfil"] = datosAnteriores;
                    txtCargoFiltro.Attributes.Add("readonly", "readonly");
                }
            }
        }

        #region Gridviews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
                switch (e.CommandName)
                {
                    case "Modificar":
                        mtdModificar();
                        GridView3.Visible = true;
                        break;
                    case "Activar":
                        mtdActivar();
                        break;
                }
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error" + "<br/>" + "Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strErrMsg = string.Empty;

            RowGrid3 = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDescargarArchivo(ref strErrMsg);
                    if (!string.IsNullOrEmpty(strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");

                    break;
                case "Activar":
                    mtdActivarArchivo();
                    break;
            }
        }
        #endregion

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            divFiltro.Visible = true;
            filaDetalle.Visible = false;
            filaAdjuntos.Visible = false;
            mtdResetCampos();
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                CargarEstadosPerfil();
                mtdResetCampos();
                ddlMacroproceso.Focus();

                tbxId.Enabled = false;
                ChBEstado.Enabled = true;

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaGrid.Visible = false;
                divFiltro.Visible = false;
                filaDetalle.Visible = true;
                filaAdjuntos.Visible = false;
                imgDependencia4.Visible = true;
            }
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarPerfil(ref strErrMsg))
                {
                    omb.ShowMessage("Perfil registrado exitosamente.", 3, "Atención");
                    filaGrid.Visible = true;
                    divFiltro.Visible = true;
                    filaDetalle.Visible = false;
                    filaAdjuntos.Visible = false;
                    mtdCargarPerfil(DatosFiltro());
                    // Envia la notificación.
                    EnviarNotificaciones(tbxResponsable.Text.Trim(), txtCodigo.Text.ToUpper().Trim());
                    mtdResetCampos();
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar el perfil." + "<br/>" + "Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    // Obtiene el correo del usuario actual
                    string email = ObtenerCorreoJerarquia.ObtenerDireccionCorreo(Convert.ToInt32(Session["IdJerarquia"].ToString()));

                    if (mtdActualizarPerfil(ref strErrMsg))
                    {
                        omb.ShowMessage("Perfil modificado exitosamente.", 3, "Atención");
                        filaGrid.Visible = true;
                        divFiltro.Visible = true;
                        filaDetalle.Visible = false;
                        filaAdjuntos.Visible = false;

                        mtdResetCampos();
                        mtdCargarPerfil(DatosFiltro());
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al modificar el perfil. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnModificarEstado_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            mpeMsgBox.Hide();

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (Session["EstadoPerfil"].ToString() == "P")
                    {
                        if (mtdActualizarEstado(ref strErrMsg))
                        {
                            omb.ShowMessage("Perfil (in)activado exitosamente.", 3, "Atención");
                            filaGrid.Visible = true;
                            divFiltro.Visible = true;
                            filaDetalle.Visible = false;
                            filaAdjuntos.Visible = false;

                            mtdResetCampos();
                            mtdCargarPerfil(DatosFiltro());
                            //mtdCargarPdf(ref strErrMsg);
                        }
                        else
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                    }
                    else
                    {
                        if (Session["EstadoPerfil"].ToString() == "A")
                        {
                            if (mtdActualizarEstadoArchivo(ref strErrMsg))
                            {
                                omb.ShowMessage("Archivo (in)activado exitosamente.", 3, "Atención");
                                mtdCargarPdf(ref strErrMsg);
                            }
                            else
                                omb.ShowMessage(strErrMsg, 1, "Atención");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al inactivar el perfil. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnAgregarPDF_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosAgregar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (FileUpload2.HasFile)
                    {
                        string ext = Path.GetExtension(FileUpload2.FileName).ToLower().ToString().Trim();
                        if (!new[] { ".exe", ".dll", ".ocx", ".msi" }.Any(current => current == ext))
                        {
                            if (mtdGuardarArchivo(ref strErrMsg))
                            {
                                omb.ShowMessage("Archivo cargado exitósamente.", 3, "Atención");

                                if (mtdCargarPdf(ref strErrMsg))
                                    omb.ShowMessage(strErrMsg, 1, "Atención");
                                GridView3.Visible = true;
                            }
                            else
                                omb.ShowMessage(strErrMsg, 1, "Atención");
                        }
                        else
                            omb.ShowMessage("No se permite cargar archivos ejecutables.", 2, "Atención");
                    }
                    else
                        omb.ShowMessage("No hay archivos para cargar.", 2, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al agregar el archivo. " + ex.Message, 1, "Atención");
            }
        }

        #endregion

        #region Metodos

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdResetCampos()
        {
            //tbxId.Text = string.Empty;
            lblIdDependencia4.Text = string.Empty;
            txtJerarquiaAprueba.Text = string.Empty;
            lblIdDependencia1.Text = string.Empty;
            tbxResponsable.Text = string.Empty;
            tbxResumenCargo.Text = string.Empty;
            tbxNombrePerfil.Text = string.Empty;
            txtCodigo.Text = string.Empty;

            #region CheckBox
            ChBEstado.Checked = true;
            #endregion CheckBox

            tbxRoles.Text = string.Empty;
            tbxHabilidades.Text = string.Empty;
            tbxEducacion.Text = string.Empty;
            tbxFormacion.Text = string.Empty;
            tbxExperiencia.Text = string.Empty;
            tbxFunciones.Text = string.Empty;
            ddlEstadoPerfil.Enabled = true;
            ddlMacroproceso.SelectedValue = "0";

            //tbxUsuarioCreacion.Text = string.Empty;
            //tbxFecha.Text = string.Empty;

            trEstadoDocumento.Visible = false;

            // Filtro
            ddlMacroProcesoFiltro.ClearSelection();
            lJerarquiaFiltro.Text = string.Empty;
            rbtnEstadoInactivo.Checked = false;
            txtCargoFiltro.Text = string.Empty;
            if (((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Count > 0)
            {
                mtdLoadInfoGridPerfil(DatosFiltro());
                ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Clear();
            }
        }

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData, TreeView4);
            AddTopTreeViewNodes(treeViewData, TreeViewFiltro);
            TreeView4.ExpandAll();
            TreeViewFiltro.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT PJO.IdHijo, PJO.IdPadre, PJO.NombreHijo, PDJ.NombreResponsable, PDJ.CorreoResponsable " +
                "FROM Parametrizacion.JerarquiaOrganizacional PJO LEFT JOIN Parametrizacion.DetalleJerarquiaOrg PDJ ON PJO.idHijo = PDJ.idHijo";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData, TreeView treeView)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";

            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                treeView.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            tbxResponsable.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview

        #region Cargas
        #region Gridview
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarPerfil(clsPerfil perfil)
        {
            try
            {
                bool booResult = false;
                mtdLoadGridPerfil();
                booResult = mtdLoadInfoGridPerfil(perfil);
                return booResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridPerfil()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdJOrganizacional", typeof(string));
            grid.Columns.Add("strNombreJOrganizacional", typeof(string));
            grid.Columns.Add("strResumenCargo", typeof(string));
            grid.Columns.Add("strPerfil", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("strRol", typeof(string));
            grid.Columns.Add("strHabilidades", typeof(string));
            grid.Columns.Add("strEducacion", typeof(string));
            grid.Columns.Add("strFormacion", typeof(string));
            grid.Columns.Add("strExperiencia", typeof(string));
            grid.Columns.Add("strFunciones", typeof(string));
            grid.Columns.Add("intIdMacroproceso", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del perfil al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdLoadInfoGridPerfil(clsPerfil perfil)
        {
            try
            {
                bool booResult = false;
                List<clsPerfil> lstPerfil = new List<clsPerfil>();
                clsPerfilBLL cPerfil = new clsPerfilBLL();

                lstPerfil = cPerfil.mtdConsultarPerfil(perfil);
                Session["Perfiles"] = lstPerfil;

                if (lstPerfil != null)
                {
                    mtdLoadInfoGridPerfil(lstPerfil);
                    GridView1.DataSource = lstPerfil;
                    GridView1.PageIndex = PagIndex;
                    GridView1.DataBind();
                    booResult = true;
                    GridView1.Visible = true;
                }
                else
                {
                    GridView1.Visible = false;
                    omb.ShowMessage("No hay información para cargar", 2, "Atención");
                }
                return booResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstPerfil">Lista con los perfiles</param>
        private void mtdLoadInfoGridPerfil(List<clsPerfil> lstPerfil)
        {
            foreach (clsPerfil objPerfil in lstPerfil)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objPerfil.intId.ToString().Trim(),
                    objPerfil.intIdJOrganizacional.ToString().Trim(),
                    objPerfil.strNombreJOrganizacional.ToString().Trim(),
                    objPerfil.strResumenCargo.ToString().Trim(),
                    objPerfil.strPerfil.ToString().Trim(),
                    objPerfil.booEstado.ToString().Trim(),
                    objPerfil.strRol.ToString().Trim(),
                    objPerfil.strHabilidades.ToString().Trim(),
                    objPerfil.strEducacion.ToString().Trim(),
                    objPerfil.strFormacion.ToString().Trim(),
                    objPerfil.strExperiencia.ToString().Trim(),
                    objPerfil.strFunciones.ToString().Trim(),
                    objPerfil.intIdMacroproceso.ToString().Trim(),
                    objPerfil.intIdUsuario.ToString().Trim(),
                    objPerfil.strNombreUsuario.ToString().Trim(),
                    objPerfil.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Gridview PDF
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns></returns>
        private bool mtdCargarPdf(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridArchivos();
            mtdLoadInfoGridArchivos(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridArchivos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("intIdControl", typeof(string));
            grid.Columns.Add("intIdTipoControl", typeof(string));
            grid.Columns.Add("strNombreArchivo", typeof(string));
            grid.Columns.Add("bArchivoBinario", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGrid3 = grid;
        }

        /// <summary>
        /// Hace el llamado y la instancia de los campos del archivo al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridArchivos(ref string strErrMsg)
        {
            List<clsArchivosCalidad> lstArchivos = new List<clsArchivosCalidad>();
            clsArchivosCalidadBLL cArchivos = new clsArchivosCalidadBLL();
            clsArchivosCalidad objArchivo = new clsArchivosCalidad(0,
                Convert.ToInt32(tbxId.Text.Trim()), 2, string.Empty, null, true, 0, string.Empty);

            lstArchivos = cArchivos.mtdConsultarArchivos(objArchivo, ref strErrMsg);

            if (lstArchivos != null)
            {
                mtdLoadInfoGridArchivos(lstArchivos);
                GridView3.DataSource = lstArchivos;
                GridView3.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con los archivos</param>
        private void mtdLoadInfoGridArchivos(List<clsArchivosCalidad> lstArchivos)
        {
            foreach (clsArchivosCalidad objArchivo in lstArchivos)
            {
                InfoGrid3.Rows.Add(new Object[] {
                    objArchivo.intId.ToString().Trim(),
                    objArchivo.intIdControl.ToString().Trim(),
                    objArchivo.intIdTipoControl.ToString().Trim(),
                    objArchivo.strNombreArchivo.ToString().Trim(),
                    objArchivo.bArchivoBinario.ToString().Trim(),
                    objArchivo.booEstado.ToString().Trim(),
                    objArchivo.intIdUsuario.ToString().Trim(),
                    objArchivo.strNombreUsuario.ToString().Trim(),
                    objArchivo.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion

        #region Carga DropDownList
        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLMacroProceso(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                lstMacroproceso = cMacroproceso.mtdConsultarMacroproceso(true, ref strErrMsg);

                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new ListItem("", "0"));
                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstMacroproceso != null)
                    {
                        int intCounter = 1;

                        foreach (clsMacroproceso objMacroproceso in lstMacroproceso)
                        {
                            ddlMacroproceso.Items.Insert(intCounter, new ListItem(objMacroproceso.strNombreMacroproceso, objMacroproceso.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de macroprocesos. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        private void CargarMacroprocesos()
        {
            try
            {
                ddlMacroProcesoFiltro.Items.Clear();
                ddlMacroProcesoFiltro.DataSource = Macroprocesos();
                ddlMacroProcesoFiltro.DataTextField = "Nombre";
                ddlMacroProcesoFiltro.DataValueField = "IdMacroProceso";
                ddlMacroProcesoFiltro.DataBind();
                ddlMacroProcesoFiltro.Items.Insert(0, new ListItem("", "0"));
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los Macroprocesos. {ex.Message}", 1, "Error");
            }
        }

        private void CargarEstadosPerfil()
        {

            ddlEstadoPerfil.Items.Clear();
            ddlEstadoPerfil.DataSource = OpcionesEstadoPerfil();
            ddlEstadoPerfil.DataValueField = "IdEstadoDocumento";
            ddlEstadoPerfil.DataTextField = "NombreEstadoDocumento";
            ddlEstadoPerfil.DataBind();
            ddlEstadoPerfil.Items.Insert(0, new ListItem("", "0"));
            ListItem item = ddlEstadoPerfil.Items.FindByValue("0");
            item.Attributes.Add("style", "display:none");
        }

        #endregion



        /// <summary>
        /// Realiza la insercion del perfil
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no.</returns>
        private bool mtdInsertarPerfil(ref string strErrMsg)
        {
            trEstadoDocumento.Visible = false;
            bool booResult = false;
            clsPerfil objPerfil = new clsPerfil(0,
                Convert.ToInt32(lblIdDependencia4.Text.Trim()),
                Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()), 1, txtCodigo.Text, tbxResponsable.Text.Trim(),
                Convert.ToInt32(lblIdDependencia1.Text));

            clsPerfilBLL cPerfil = new clsPerfilBLL();

            booResult = cPerfil.mtdInsertarPerfil(objPerfil, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            // Estado Perfil
            trEstadoDocumento.Visible = true;
            CargarEstadosPerfil();

            string strErrMsg = string.Empty;

            ddlMacroproceso.Focus();
            tbxId.Enabled = false;
            ChBEstado.Enabled = false;
            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;
            filaGrid.Visible = false;
            divFiltro.Visible = false;
            filaDetalle.Visible = true;
            filaAdjuntos.Visible = true;

            // Carga el perfil actual
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            List<clsPerfil> lst = (List<clsPerfil>)Session["Perfiles"];
            clsPerfil doc = new clsPerfil();
            doc = lst.Find(item => item.intId == Convert.ToInt32(tbxId.Text));
            ddlEstadoPerfil.SelectedValue = doc.EstadoPerfil.ToString();

            lblIdDependencia4.Text = doc.intIdJOrganizacional.ToString();
            tbxResponsable.Text = doc.strNombreJOrganizacional;

            lblIdDependencia1.Text = doc.IdJerarquiaAprueba.ToString();
            txtJerarquiaAprueba.Text = doc.NombreJerarquiaAprueba;

            txtCodigo.Text = doc.Codigo;
            tbxResponsable.Enabled = false;
            //imgDependencia4.Visible = false;
            tbxResumenCargo.Text = InfoGrid.Rows[RowGrid][3].ToString().Trim();
            tbxNombrePerfil.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][5].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            tbxRoles.Text = InfoGrid.Rows[RowGrid][6].ToString().Trim();
            tbxHabilidades.Text = InfoGrid.Rows[RowGrid][7].ToString().Trim();
            tbxEducacion.Text = InfoGrid.Rows[RowGrid][8].ToString().Trim();
            tbxFormacion.Text = InfoGrid.Rows[RowGrid][9].ToString().Trim();
            tbxExperiencia.Text = InfoGrid.Rows[RowGrid][10].ToString().Trim();
            tbxFunciones.Text = InfoGrid.Rows[RowGrid][11].ToString().Trim();

            ddlMacroproceso.SelectedValue = InfoGrid.Rows[RowGrid][12].ToString().Trim();

            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][14].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][15].ToString().Trim();

            mtdCargarPdf(ref strErrMsg);

            // Bloquea el aprobado si no es el responsable
            BuscarNodos(TreeViewFiltro);
            string parentNode = (string)Session["ParentNode"];
            string correoResponsable = ObtenerCorreoJerarquia.ObtenerDireccionCorreo(Convert.ToInt32(parentNode));

            // Se guardan lo datos anteriores necesarios para una posterior validación
            if (((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Count == 0)
            {
                ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Add("CorreoResponsable", doc.IdJerarquiaAprueba.ToString());
                ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Add("EstadoAnterior", doc.EstadoPerfil.ToString());
                ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Add("CorreoUsuario", doc.IdJerarquiaUsuario.ToString());
            }

            #region Validación Estado Documento

            string jerarquiaActual = Session["IdJerarquia"].ToString();

            if (jerarquiaActual != doc.IdJerarquiaAprueba.ToString())
            {
                ListItem item = ddlEstadoPerfil.Items.FindByValue("2");
                item.Attributes.Add("style", "display:none");

                // Bloquea el estado devuelto si el estado es cargado
                if (doc.EstadoPerfil == 1)
                {
                    item = ddlEstadoPerfil.Items.FindByValue("3");
                    item.Attributes.Add("style", "display:none");
                }
                GridView3.Visible = false;
            }
            // Oculta la opción cargado a quien no haya tenido el último contacto con el documento
            else if(jerarquiaActual != doc.IdJerarquiaUsuario.ToString())
            {
                ListItem item = ddlEstadoPerfil.Items.FindByValue("1");
                item.Attributes.Add("style", "display:none");
            }
            #endregion
            // Si el documento es aprobado se inhabilita
            if (doc.EstadoPerfil == 2)
            {
                ddlEstadoPerfil.Enabled = false;
                GridView3.Visible = true;
            }
        }

        private void GuardaValorNodo(TreeNode treeNode)
        {
            if (treeNode.Value == lblIdDependencia4.Text)
                Session["ParentNode"] = treeNode.Parent.Value;
            foreach (TreeNode tn in treeNode.ChildNodes)
            {
                GuardaValorNodo(tn);
            }
        }

        private void BuscarNodos(TreeView treeView)
        {
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                GuardaValorNodo(n);
            }
        }

        private bool EnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
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
                        Asunto = row["Asunto"].ToString();
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
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
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
                    omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString(), 1, "Atención");
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

        private void EnviarNotificaciones(string perfil, string codigo)
        {
            try
            {

                string TextoAdicional = string.Empty;
                string direccionCorreo = string.Empty;
                string estadoANt = string.Empty;

                // Envia la notificación de acuerdo a la condicion especifica para cada caso
                switch (ddlEstadoPerfil.SelectedValue)
                {
                    case "0":
                        estadoANt = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value));
                        if (estadoANt == string.Empty)
                        {
                            TextoAdicional = "DOCUMENTO PERFIL CARGADO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento para el perfil {perfil.ToUpper().Trim()} ha sido cargado.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigo.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Perfil : " + Sanitizer.GetSafeHtmlFragment(perfil.Trim()) + "<br>";
                            EnviarNotificacion(36, 1, Convert.ToInt16(lblIdDependencia1.Text), DateTime.Now.ToString().Trim(), TextoAdicional);
                        }
                        break;

                    case "1":
                        estadoANt = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value));
                        if (ddlEstadoPerfil.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value))
                            && string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)) == "3")
                        {

                            TextoAdicional = "DOCUMENTO PERFIL CARGADO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento para el perfil {perfil.ToUpper().Trim()} que había sido devuelto ha sido cargado nuevamente.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigo.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Perfil : " + Sanitizer.GetSafeHtmlFragment(perfil.Trim()) + "<br>";
                            EnviarNotificacion(36, 1, Convert.ToInt16(lblIdDependencia1.Text), DateTime.Now.ToString().Trim(), TextoAdicional);
                        };
                        break;
                    case "2":
                        if (ddlEstadoPerfil.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)))
                        {

                            TextoAdicional = "DOCUMENTO PERFIL APROBADO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento para el perfil {perfil.ToUpper().Trim()} cargado por usted ha sido aprobado.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigo.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Perfil : " + Sanitizer.GetSafeHtmlFragment(perfil.Trim()) + "<br>";

                            direccionCorreo = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "CorreoUsuario").Select(o => o.Value));
                            if (direccionCorreo != string.Empty)
                                EnviarNotificacion(36, 1, Convert.ToInt16(direccionCorreo), DateTime.Now.ToString().Trim(), TextoAdicional);
                        };
                        break;
                    case "3":
                        estadoANt = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value));
                        if (ddlEstadoPerfil.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)))
                        {

                            TextoAdicional = "DOCUMENTO PERFIL DEVUELTO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento para el perfil {perfil.ToUpper().Trim()} cargado por usted ha sido devuelto.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigo.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Perfil : " + Sanitizer.GetSafeHtmlFragment(perfil.Trim()) + "<br>";

                            direccionCorreo = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresPerfil"]).Where(x => x.Key == "CorreoUsuario").Select(o => o.Value));
                            if (direccionCorreo != string.Empty)
                                EnviarNotificacion(36, 1, Convert.ToInt16(direccionCorreo), DateTime.Now.ToString().Trim(), TextoAdicional);
                        };
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarPerfil(ref string strErrMsg)
        {
            try
            {
                trEstadoDocumento.Visible = true;
                bool booResult = false;
                clsPerfil objPerfil = new clsPerfil(
                    Convert.ToInt32(tbxId.Text.Trim()),
                    Convert.ToInt32(lblIdDependencia4.Text.Trim()),
                    Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString().Trim()),
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                    ddlEstadoPerfil.SelectedValue.Equals("0") ? 1 : Convert.ToInt32(ddlEstadoPerfil.SelectedValue),
                    txtCodigo.Text,
                    tbxResponsable.Text.Trim(),
                    Convert.ToInt32(lblIdDependencia1.Text)
                    );

                clsPerfilBLL cPerfil = new clsPerfilBLL();

                booResult = cPerfil.mtdActualizarPerfil(objPerfil, ref strErrMsg);

                EnviarNotificaciones(tbxResponsable.Text.Trim(), txtCodigo.Text.ToUpper().Trim());

                return booResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza el proceso previo a actualizar el estado
        /// </summary>
        private void mtdActivar()
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string strEstado = string.Empty;
                bool booEstado = InfoGrid.Rows[RowGrid][5].ToString().Trim() == "True" ? true : false;
                Session["EstadoPerfil"] = "P";

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el Perfil?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar.
        /// </summary>
        private void mtdActivarArchivo()
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string strEstado = string.Empty;
                bool booEstado = InfoGrid3.Rows[RowGrid3][5].ToString().Trim() == "True" ? true : false;
                Session["EstadoPerfil"] = "A";

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} el archivo?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la actualizacion de estado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no </returns>
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;

            clsPerfil objPerfil = new clsPerfil(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                0, string.Empty, string.Empty,
                1,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty);

            clsPerfilBLL cPerfil = new clsPerfilBLL();

            booResult = cPerfil.mtdActualizarEstado(objPerfil, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarEstadoArchivo(ref string strErrMsg)
        {
            bool booResult = false;

            clsArchivosCalidad objArchivoIN = new clsArchivosCalidad(Convert.ToInt32(InfoGrid3.Rows[RowGrid3][0].ToString().Trim()),
                0, 0, string.Empty, null, !(InfoGrid3.Rows[RowGrid3][5].ToString().Trim() == "True" ? true : false), 0, string.Empty);
            clsArchivosCalidadBLL cArchivos = new clsArchivosCalidadBLL();

            booResult = cArchivos.mtdActualizarEstado(objArchivoIN, ref strErrMsg);

            return booResult;
        }

        private List<Classes.DTO.Calidad.Macroproceso> Macroprocesos() => perfilBLL.Macroprocesos();

        private List<EstadoDocumento> OpcionesEstadoPerfil() => perfilBLL.OpcionesEstadoPerfil();

        #region PDF
        private void mtdDescargarArchivo(ref string strErrMsg)
        {
            #region Vars
            clsArchivosCalidadBLL cArchivos = new clsArchivosCalidadBLL();
            clsArchivosCalidad objArchivoIN = new clsArchivosCalidad(Convert.ToInt32(InfoGrid3.Rows[RowGrid3][0].ToString().Trim()),
                0, 0, string.Empty, null, true, 0, string.Empty), objArchivoOUT = new clsArchivosCalidad();
            bool booEstado = InfoGrid3.Rows[RowGrid3][5].ToString().Trim() == "True" ? true : false;
            #endregion Vars

            if (booEstado)
            {
                objArchivoOUT = cArchivos.mtdDescargarArchivo(objArchivoIN, ref strErrMsg);

                if (objArchivoOUT.bArchivoBinario != null)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "Application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + objArchivoOUT.strNombreArchivo);
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(objArchivoOUT.bArchivoBinario);
                    Response.End();
                }
            }
            else
                strErrMsg = "No se puede descargar el archivo porque esta inactivo";
        }

        private bool mtdGuardarArchivo(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsArchivosCalidadBLL cArchivo = new clsArchivosCalidadBLL();
            int intConsecutivo = cArchivo.mtdConsecutivoArchivo(ref strErrMsg), intTipoControl = 2;
            string strNombreArchivo = string.Empty;
            clsArchivosCalidad objArchivo = new clsArchivosCalidad();
            #endregion

            if (intConsecutivo > 0)
            {
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}", intConsecutivo, intTipoControl, tbxId.Text.Trim(), FileUpload2.FileName.ToString().Trim());

                #region Vars
                Stream fs = FileUpload2.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
                #endregion

                objArchivo = new clsArchivosCalidad(0, Convert.ToInt32(tbxId.Text.Trim()), intTipoControl, strNombreArchivo, bPdfData, true,
                    Convert.ToInt32(Session["idUsuario"].ToString().Trim()), string.Empty);

                booResult = cArchivo.mtdInsertarArchivo(objArchivo, ref strErrMsg);
            }
            else
                booResult = false;

            return booResult;
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            mtdLoadInfoGridPerfil(DatosFiltro());
        }
        #endregion

        protected void TreeViewFiltro_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtCargoFiltro.Text = TreeViewFiltro.SelectedNode.Text;
            lJerarquiaFiltro.Text = TreeViewFiltro.SelectedNode.Value;
        }

        public clsPerfil DatosFiltro()
        {
            clsPerfil datosFiltro = new clsPerfil();
            datosFiltro.EstadoPerfil = rbtnEstadoActivo.Checked ? 1 : 0;
            datosFiltro.intIdJOrganizacional = lJerarquiaFiltro.Text.Equals("") ? 0 : Convert.ToInt32(lJerarquiaFiltro.Text);
            datosFiltro.intIdMacroproceso = Convert.ToInt32(ddlMacroProcesoFiltro.SelectedValue);
            return datosFiltro;
        }

        protected void btnBusquedaFiltro_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mtdCargarPerfil(DatosFiltro());
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los perfiles: {ex.Message}", 1, "Error");
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, ImageClickEventArgs e)
        {
            ddlMacroProcesoFiltro.ClearSelection();
            lJerarquiaFiltro.Text = string.Empty;
            rbtnEstadoInactivo.Checked = false;
            txtCargoFiltro.Text = string.Empty;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void SqlDataSource200_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        protected void TreeviewAprueba_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblIdDependencia1.Text = treeviewAprueba.SelectedNode.Value;
            txtJerarquiaAprueba.Text = treeviewAprueba.SelectedNode.Text;
        }

        private void PopulateTreeViewAprueba()
        {
            DataTable treeViewData = Parametrizacion.GetTreeViewData();
            Parametrizacion.AddTopTreeViewNodes(treeViewData, treeviewAprueba);
            treeviewAprueba.ExpandAll();
        }

        protected void BtnBorrarResponsables_Click(object sender, ImageClickEventArgs e)
        {
            txtJerarquiaAprueba.Text = string.Empty;
            lblIdDependencia1.Text = string.Empty;
        }

        protected void BtnBorrarPerfil_Click(object sender, ImageClickEventArgs e)
        {
            tbxResponsable.Text = string.Empty;
            lblIdDependencia4.Text = string.Empty;
        }
    }
}
#endregion