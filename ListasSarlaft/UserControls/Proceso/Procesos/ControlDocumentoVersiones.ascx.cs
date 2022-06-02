using ListasSarlaft.Classes;
using ListasSarlaft.Classes.DTO.Calidad;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class ControlDocumentoVersiones : System.Web.UI.UserControl
    {
        string IdFormulario = "4029";
        string IdFormularioDownloadFile = "4043";
        string IdFormularioActivarFile = "4044";
        
        cCuenta cCuenta = new cCuenta();
        cProcesos cProcesos = new cProcesos();
        clsVersionDocumentoBLL clsVersion = new clsVersionDocumentoBLL();
        cError error = new cError();
        private static int LastInsertIdCE;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(GVversiones);
            scriptManager.RegisterPostBackControl(this.GVcontrolDocumento);
            //scriptManager.RegisterPostBackControl(this.fuArchivoPerfil);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdCargarDDLs();
                    mtdInicializarValores();
                    Session["FiltroActual"] = "";
                    Dictionary<string, string> datosAnteriores = new Dictionary<string, string>();
                    Session["DatosAnterioresDocumento"] = datosAnteriores;
                }
            }
        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridVersiones;
        private int rowGridVersiones;
        private int pagIndexVersiones;
        private DataTable infoGridFile;
        private int rowGridFile;
        private int pagIndexFile;

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
        private DataTable InfoGridVersiones
        {
            get
            {
                infoGridVersiones = (DataTable)ViewState["infoGridVersiones"];
                return infoGridVersiones;
            }
            set
            {
                infoGridVersiones = value;
                ViewState["infoGridVersiones"] = infoGridVersiones;
            }
        }

        private int RowGridVersiones
        {
            get
            {
                rowGridVersiones = (int)ViewState["rowGridVersiones"];
                return rowGridVersiones;
            }
            set
            {
                rowGridVersiones = value;
                ViewState["rowGridVersiones"] = rowGridVersiones;
            }
        }

        private int PagIndexVersiones
        {
            get
            {
                pagIndexVersiones = (int)ViewState["pagIndexVersiones"];
                return pagIndexVersiones;
            }
            set
            {
                pagIndexVersiones = value;
                ViewState["pagIndexVersiones"] = pagIndexVersiones;
            }
        }
        private DataTable InfoGridFile
        {
            get
            {
                infoGridFile = (DataTable)ViewState["infoGridFile"];
                return infoGridFile;
            }
            set
            {
                infoGridFile = value;
                ViewState["infoGridFile"] = infoGridFile;
            }
        }

        private int RowGridFile
        {
            get
            {
                rowGridFile = (int)ViewState["rowGridFile"];
                return rowGridFile;
            }
            set
            {
                rowGridFile = value;
                ViewState["rowGridFile"] = rowGridFile;
            }
        }

        private int PagIndexFile
        {
            get
            {
                pagIndexFile = (int)ViewState["pagIndexFile"];
                return pagIndexFile;
            }
            set
            {
                pagIndexFile = value;
                ViewState["pagIndexFile"] = pagIndexFile;
            }
        }
        #endregion
        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView4.ExpandAll();
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

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";

            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeView4.Nodes.Add(newNode);
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
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            PopulateTreeView();
            //if (!mtdLoadControlVersion(ref strErrMsg))
            //    omb.ShowMessage(strErrMsg, 2, "Atención");

        }
        protected void mtdResetFields()
        {
            string strErrMsg = string.Empty;
            BodyFormCDV.Visible = false;
            BodyGridRNC.Visible = true;
            divFiltrosBusqueda.Visible = true;
            DVVersiones.Visible = false;
            ddlCadenaValor.Enabled = true;
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.Enabled = true;
            ddlMacroproceso.ClearSelection();
            ddlProceso.Enabled = true;
            ddlProceso.ClearSelection();
            ddlSubproceso.Enabled = true;
            ddlSubproceso.ClearSelection();
            txtId.Text = "";
            TXname.Text = "";
            TXname.Enabled = true;
            TXcodigoDoc.Text = "";
            TXcodigoDoc.Enabled = true;
            TXversion.Text = "";
            TXfechaimplementacion.Text = "";
            TXfechaimplementacion.Enabled = true;
            TXfechaActual.Text = "";
            TXfechaActual.Enabled = true;
            TXfechaUp.Text = "";
            TXfechaDel.Text = "";
            tbxResponsable.Text = "";
            tbxResponsable.Enabled = true;
            imgDependencia4.Visible = true;
            TXalmace.Text = "";
            TXalmace.Enabled = true;
            TXrollback.Text = "";
            TXrollback.Enabled = true;
            TXtiempoactivo.Text = "";
            TXtiempoactivo.Enabled = true;
            TXtiempoinactivo.Text = "";
            TXtiempoinactivo.Enabled = true;
            Txdisposicion.Text = "";
            Txdisposicion.Enabled = true;
            TXmedio.Text = "";
            TXmedio.Enabled = true;
            TXformato.Text = "";
            TXformato.Enabled = true;
            txtFecha.Text = "";
            txtFechaMod.Text = "";
            tbxUsuarioCreacion.Text = "";
            TXobservaciones.Text = "";
            txtJustificacion.Text = string.Empty;
            ddlEstadoDocumento.Enabled = true;
            ddlEstadoDocumento.ClearSelection();
            LblDocumentoActivo.Text = string.Empty;

            // Filtros
            ddlCadenaValorFiltro.ClearSelection();
            ddlMacroprocesoFiltro.ClearSelection();
            ddlProcesoFiltro.ClearSelection();
            txtNombreFiltro.Text = string.Empty;
            txtCodigoFiltro.Text = string.Empty;
            txtFechaImplementacionFiltro.Text = string.Empty;
            ddlTipoDocumentoFiltro.ClearSelection();
            if (((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Count > 0)
            {
                mtdLoadControlVersion(ref strErrMsg, ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]));
                ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Clear();
            }
            trJustificacion.Visible = true;
            trJustificacionAprobacion.Visible = false;
        }
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            trEstadoDocumento.Visible = false;
            trJustificacion.Visible = false;
            BodyGridRNC.Visible = false;
            divFiltrosBusqueda.Visible = false;
            BodyFormCDV.Visible = true;
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            Session["IdControl"] = null;
        }
        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(Convert.ToInt32(ddlCadenaValor.SelectedValue)))
            {
                ddlMacroproceso.ClearSelection();
                ddlProceso.Items.Clear();
                ddlSubproceso.Items.Clear();
            };

        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(Convert.ToInt32(ddlMacroproceso.SelectedValue)))
            {
                ddlProceso.ClearSelection();
                ddlSubproceso.Items.Clear();
            }
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (ddlProceso.SelectedValue == "0")
                rfvProceso.Enabled = false;
            else
                rfvProceso.Enabled = true;

            ddlSubproceso.Items.Clear();

            if (mtdLoadDDLSubproceso(Convert.ToInt32(ddlProceso.SelectedValue)))
            {
                ddlSubproceso.ClearSelection();
                if (ddlSubproceso.Items.Count == 1)
                {
                    omb.ShowMessage("No hay información de Subprocesos", 2, "Atención");
                }
            }
        }

        protected void ddlSubproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProceso.SelectedValue == "0")
                rfvSubproceso.Enabled = false;
            else
                rfvSubproceso.Enabled = true;
        }
        #endregion
        #region DDLs
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;
            mtdLoadDDLCadenaValor(ref strErrMsg);
            mtdLoadDDLTipo();
            mtdLoadDDLTipoDocumentoFiltro();
            mtdLoadDDLEstadoDocumento();


        }

        public List<EstadoDocumento> loadDdlEstadoDocumento() => clsVersion.opcionesEstadoDocumento();

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLCadenaValor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();
            #endregion Vars

            try
            {
                lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(true, ref strErrMsg);

                ddlCadenaValor.Items.Clear();
                ddlCadenaValor.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlCadenaValorFiltro.Items.Clear();
                ddlCadenaValorFiltro.Items.Insert(0, new ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            ddlCadenaValorFiltro.Items.Insert(intCounter, new ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
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
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLMacroProceso(int IdCadenaValor)
        {
            #region Vars
            bool booResult = false;
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cMacroproceso.ConsultarMacroProcesos(IdCadenaValor);
                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlMacroprocesoFiltro.Items.Clear();
                ddlMacroprocesoFiltro.Items.Insert(0, new ListItem("", "0"));


                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        int intCounter = 1;
                        ddlMacroproceso.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdMacroProceso"].ToString()));
                        ddlMacroprocesoFiltro.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdMacroProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLProceso(int IdMacroproceso)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {

                DataTable dt = cProceso.ConsultarProcesos(IdMacroproceso);
                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlProcesoFiltro.Items.Clear();
                ddlProcesoFiltro.Items.Insert(0, new ListItem("", "0"));


                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlProceso.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdProceso"].ToString()));
                        ddlProcesoFiltro.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = true;
                throw ex;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los subprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLSubproceso(int? idProceso)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cSubproceso.ConsultarSubprocesos(idProceso);

                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlSubprocesoFiltro.Items.Clear();
                ddlSubprocesoFiltro.Items.Insert(0, new ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlSubproceso.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdSubproceso"].ToString()));
                        ddlSubprocesoFiltro.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdSubproceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }
        public void mtdLoadDDLTipo()
        {
            DDLtipo.Items.Clear();
            DDLtipo.Items.Insert(0, new ListItem("Documento", "1"));
            DDLtipo.Items.Insert(0, new ListItem("Registro", "2"));
            DDLtipo.Items.Insert(0, new ListItem("Eliminación", "3"));
            DDLtipo.Items.Insert(0, new ListItem("Procedimiento", "4"));
            DDLtipo.Items.Insert(0, new ListItem("Politica", "5"));
            DDLtipo.Items.Insert(0, new ListItem("Manual", "6"));
            DDLtipo.Items.Insert(0, new ListItem("Instructivo", "7"));
            DDLtipo.Items.Insert(0, new ListItem("Reglamento", "8"));
            DDLtipo.Items.Insert(0, new ListItem("Formato", "9"));
            DDLtipo.Items.Insert(0, new ListItem("Circular", "10"));
            DDLtipo.Items.Insert(0, new ListItem("", "0"));
        }

        public void mtdLoadDDLEstadoDocumento()
        {
            ddlEstadoDocumento.Items.Clear();
            ddlEstadoDocumento.DataSource = loadDdlEstadoDocumento();
            ddlEstadoDocumento.DataValueField = "IdEstadoDocumento";
            ddlEstadoDocumento.DataTextField = "NombreEstadoDocumento";
            ddlEstadoDocumento.DataBind();
            ddlEstadoDocumento.Items.Insert(0, new ListItem("", "0"));
            ListItem item = ddlEstadoDocumento.Items.FindByValue("0");
            item.Attributes.Add("style", "display:none");
        }

        public void mtdLoadDDLTipoDocumentoFiltro()
        {
            ddlTipoDocumentoFiltro.Items.Clear();
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Documento", "1"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Registro", "2"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Eliminación", "3"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Procedimiento", "4"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Politica", "5"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Manual", "6"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Instructivo", "7"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Reglamento", "8"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Formato", "9"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Circular", "10"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("", "0"));
        }

        #endregion

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            string strErrMsg = string.Empty;
            bool booResult = new bool();
            if (fuArchivoPerfil.HasFile && (Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".pdf" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".docx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".doc" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xlsx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xls"))
            {
                omb.ShowMessage("Solo archivos en formato pdf,word,excel", 2, "Atención");
                return;
            }
            if (DDLtipo.SelectedValue == "1")
            {
                if (fuArchivoPerfil.HasFile)
                {

                    if (System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".pdf" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".docx" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".doc" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".xlsx")
                    {
                        booResult = mtdInsertarActualizarControlDoc(ref strErrMsg);
                        if (booResult == false)
                        {
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                            mtdResetFields();
                            mtdStard();
                        }
                        else
                        {
                            omb.ShowMessage(strErrMsg, 3, "Atención");
                            mtdResetFields();
                            mtdStard();
                        }
                    }
                    else
                    {
                        omb.ShowMessage("Soló se permiten archivos PDF, Word, Excel", 2, "Atención");
                    }
                }
                else
                {
                    omb.ShowMessage("Cargar el archivo es Obligatorio", 2, "Atención");
                }
            }
            else
            {
                booResult = mtdInsertarActualizarControlDoc(ref strErrMsg);
                if (booResult == false)
                {
                    omb.ShowMessage(strErrMsg, 1, "Atención");
                    mtdResetFields();
                    mtdStard();
                }
                else
                {
                    omb.ShowMessage(strErrMsg, 3, "Atención");
                    mtdResetFields();
                    mtdStard();
                }
            }
            mtdLoadControlVersion(ref strErrMsg, ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]));
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                return;
            }
            trEstadoDocumento.Visible = true;
            trJustificacion.Visible = true;
            string strErrMsg = String.Empty;
            string version = TXversion.Text;

            // Se valida para insertar una justificacion
            if (int.TryParse((string)Session["IdControl"], out int outResult))
            {
                if (string.IsNullOrEmpty(txtJustificacion.Text) && string.IsNullOrEmpty(txtJustificacionAprobacion.Text))
                {
                    omb.ShowMessage("Debe indicar una justificación para el cambio", 2, "Atención");
                    return;
                }
            }

            if (version != "")
            {
                if (fuArchivoPerfil.HasFile && (Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".pdf" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".docx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".doc" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xlsx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xls"))
                {
                    omb.ShowMessage("Solo archivos en formato pdf,word,excel", 2, "Atención");
                    return;
                }
                if (DDLtipo.SelectedValue == "1")
                {
                    if (ddlEstadoDocumento.SelectedValue == "2")
                    {
                        if ((mtdInsertarActualizarControlDoc(ref strErrMsg)))
                        {
                            omb.ShowMessage(strErrMsg, 3, "Atención");
                            mtdStard();
                            mtdResetFields();
                        }
                    }
                    else
                    {
                        if (fuArchivoPerfil.HasFile)
                        {
                            if ((mtdInsertarActualizarControlDoc(ref strErrMsg)))
                            {
                                omb.ShowMessage(strErrMsg, 3, "Atención");
                                mtdStard();
                                mtdResetFields();
                            }
                        }
                        else
                        {
                            omb.ShowMessage("Cargar el archivo es Obligatorio", 2, "Atención");
                        }
                    }
                }
                else
                {
                    if (mtdInsertarActualizarControlDoc(ref strErrMsg))
                    {
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                        mtdStard();
                        mtdResetFields();
                    }
                }
            }
            else
            {
                omb.ShowMessage("La version no puede ir vacia", 2, "Atención");
            }
        }
        private bool mtdUpdateControlVersion(ref string strErrMsg)
        {
            txtFechaMod.Text = "" + DateTime.Now;
            bool booResult = false;
            clsControlVersion objCrlInfra = new clsControlVersion();
            objCrlInfra.intIdVersionDocumento = Convert.ToInt32(Session["IdControl"].ToString());
            objCrlInfra.strVersion = TXversion.Text;
            objCrlInfra.strObservaciones = TXobservaciones.Text;
            objCrlInfra.dtFechaModificacion = TXfechaUp.Text;
            objCrlInfra.dtFechaEliminacion = TXfechaDel.Text;
            objCrlInfra.dtFechaRegistro = Convert.ToDateTime(txtFechaMod.Text);
            objCrlInfra.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            string pathFile = string.Empty;
            int IdTipo = Convert.ToInt32(DDLtipo.SelectedValue);
            objCrlInfra.intIdTipoDocumento = IdTipo;
            clsVersionDocumentoBLL cCrlVersion = new clsVersionDocumentoBLL();
            if (fuArchivoPerfil.HasFile)
            {
                if (System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".pdf" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".docx" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".doc" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".xlsx")
                {
                    pathFile = "Ver." + TXversion.Text + fuArchivoPerfil.FileName;
                    Byte[] archivo = fuArchivoPerfil.FileBytes;
                    int length = Convert.ToInt32(fuArchivoPerfil.FileContent.Length);
                    string extension = System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim();
                    saveFile(pathFile, length, archivo, Convert.ToInt32(txtId.Text), extension);
                    //mtdCargarArchivo();
                    objCrlInfra.strPathFIle = pathFile;

                    booResult = cCrlVersion.mtdUpdateVersion(objCrlInfra, ref strErrMsg, IdTipo);
                }
                else
                    omb.ShowMessage("Archivo sin guardar. Solo archivos en formato pdf,word,excel", 2, "Atención");
            }
            else
            {
                booResult = cCrlVersion.mtdUpdateVersion(objCrlInfra, ref strErrMsg, IdTipo);
            }


            if (booResult == true)
                strErrMsg = "Control del documento  actualizado exitosamente";
            else
                strErrMsg = "Error al actualizar control del Documento";
            return booResult;
        }

        private void saveFile(string NombreArchivo, int Length, byte[] archivo, int IdRegistro, string extension)
        {
            string strErrMsg = string.Empty;
            clsVersionDocumentoBLL cVersion = new clsVersionDocumentoBLL();

            if (!cVersion.mtdInsertarArchivo(NombreArchivo, Length, archivo, IdRegistro, ref strErrMsg, extension))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        private bool mtdInsertarActualizarControlDoc(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                int outResult = 0;
                clsVersionDocumento objVersion = new clsVersionDocumento();
                if (ddlSubproceso.SelectedValue != "" && ddlSubproceso.SelectedValue != "0")
                {
                    objVersion.intIdMacroProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                    objVersion.intIdTipoProceso = 3;
                }
                else
                {
                    if (ddlProceso.SelectedValue != "" && ddlProceso.SelectedValue != "0")
                    {
                        objVersion.intIdMacroProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                        objVersion.intIdTipoProceso = 2;
                    }
                    else
                    {
                        if (ddlMacroproceso.SelectedValue != "" && ddlMacroproceso.SelectedValue != "0")
                        {
                            objVersion.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                            objVersion.intIdTipoProceso = 1;
                        }
                    }
                }

                // Se valida si el documento se va insertar o a actualizar
                if (int.TryParse((string)Session["IdControl"], out outResult))
                    objVersion.intId = Convert.ToInt32(Session["IdControl"].ToString());

                objVersion.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                objVersion.IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                objVersion.IdSubproceso = ddlSubproceso.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                objVersion.strCodigoDocumento = TXcodigoDoc.Text;
                objVersion.dtFechaImplementacion = TXfechaimplementacion.Text;
                objVersion.intIdTipoDocumento = Convert.ToInt32(DDLtipo.SelectedValue.ToString());
                objVersion.intidCargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
                objVersion.strUbicacionAlmacenamiento = TXalmace.Text;
                objVersion.strRecuperacion = TXrollback.Text;
                objVersion.strTiempoRetencionActivo = TXtiempoactivo.Text;
                objVersion.strTiempoRetencionInactivo = TXtiempoinactivo.Text;
                objVersion.strDisposicionFinal = Txdisposicion.Text;
                objVersion.strMedioSoporte = TXmedio.Text;
                objVersion.strFormato = TXformato.Text;
                objVersion.dtFechaRegistro = DateTime.Now;
                objVersion.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                objVersion.strNombreDocumento = TXname.Text;
                objVersion.dtFechaModificacion = TXfechaUp.Text;
                objVersion.dtFechaEliminacion = TXfechaDel.Text;
                objVersion.IdEstadoDocumento = Convert.ToInt32(ddlEstadoDocumento.SelectedValue);
                string pathFile = string.Empty;
                clsVersionDocumentoBLL cCrlVersion = new clsVersionDocumentoBLL();
                booResult = cCrlVersion.mtdInsertarActualizarControlDocumento(objVersion, ref strErrMsg);

                // Recupera el último Id de la tabla
                int LastId = cCrlVersion.mtdLastIdControlNoConformidad(ref strErrMsg);

                clsControlVersion version = new clsControlVersion();

                // Determina el documento al que va a adjuntar la versión
                if (int.TryParse((string)Session["IdControl"], out outResult))
                    version.intIdVersionDocumento = Convert.ToInt32(Session["IdControl"].ToString());
                else
                {
                    version.intIdVersionDocumento = LastId;
                }

                if (Session["idControlVersion"] != null)
                    version.intId = Convert.ToInt32(Session["idControlVersion"].ToString());
                //--> Inicio Nuevos campos
                version.IdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                version.IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                version.IdSubproceso = ddlSubproceso.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                version.CodigoDocumento = TXcodigoDoc.Text;
                version.CargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
                version.UbicacionAlmacemiento = TXalmace.Text;
                version.Recuperacion = TXrollback.Text;
                version.TiempoRetencionActivo = TXtiempoactivo.Text;
                version.TiempoRetencionInactivo = TXtiempoinactivo.Text;
                version.DisposionFinal = Txdisposicion.Text;
                version.MedioSoporte = TXmedio.Text;
                version.Formato = TXformato.Text;
                version.NombreDocumento = TXname.Text;
                version.IdTipoProceso = objVersion.intIdTipoProceso;
                version.FechaImplementacion = TXfechaimplementacion.Text;
                if (ddlEstadoDocumento.SelectedValue == "2")
                    version.JustificacionAprobacion = txtJustificacionAprobacion.Text;
                else
                    version.JustificacionCambios = txtJustificacion.Text;
                //Fin Nuevos campos <--
                
                version.strVersion = TXversion.Text;
                version.dtFechaModificacion = TXfechaUp.Text;
                version.dtFechaEliminacion = TXfechaDel.Text;
                version.strObservaciones = TXobservaciones.Text;
                version.strPathFIle = pathFile;
                version.dtFechaRegistro = DateTime.Now;
                if (ddlEstadoDocumento.SelectedValue == "2")
                    version.UsuarioAprobacion = Convert.ToInt32(Session["IdUsuario"]);
                else
                    version.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                version.intIdTipoDocumento = Convert.ToInt32(DDLtipo.SelectedValue.ToString());
                version.IdEstadoDocumento = Convert.ToInt32(ddlEstadoDocumento.SelectedValue);

                //Subir archivo
                if (fuArchivoPerfil.HasFile)
                {
                    pathFile = "Ver." + TXversion.Text + fuArchivoPerfil.FileName;
                    Byte[] archivo = fuArchivoPerfil.FileBytes;
                    int length = Convert.ToInt32(fuArchivoPerfil.FileContent.Length);
                    string extension = Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim();
                    saveFile(pathFile, length, archivo, txtId.Text.Equals("") ? LastId : Convert.ToInt32(txtId.Text), extension);
                    version.strPathFIle = pathFile;
                }
                if(Session["VerActual"] != null)
                {
                    if (version.strVersion != Session["VerActual"].ToString())
                        booResult = cCrlVersion.mtdInsertarControlVersion(version, ref strErrMsg);
                    else
                        booResult = cCrlVersion.mtdUpdateControlVersion(version, ref strErrMsg);
                }else
                    booResult = cCrlVersion.mtdInsertarControlVersion(version, ref strErrMsg);


                if (booResult == true)
                {
                    if (int.TryParse((string)Session["IdControl"], out outResult))
                        strErrMsg = "Versión registrada exitosamente";
                    else
                        strErrMsg = "Documento registrado exitosamente";
                }
                else
                {
                    strErrMsg = "Error al registrar el control";
                    booResult = false;
                }
                string Justificacion = string.Empty;
                if (string.IsNullOrEmpty(version.JustificacionCambios) == false)
                    Justificacion = version.JustificacionCambios;
                else
                    Justificacion = version.JustificacionAprobacion;
                //mtdEnviarNotificaciones(version.NombreDocumento, version.CodigoDocumento, version.JustificacionCambios);
                if (string.IsNullOrEmpty(Justificacion) == true)
                    Justificacion = " ";
                mtdEnviarNotificaciones(version.NombreDocumento, version.CodigoDocumento, Justificacion);

                return booResult;
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error {ex.Message}", 1, "Error");
                booResult = false;
                return booResult;
            }
        }
        private bool mtdLoadControlVersion(ref string strErrMsg, Dictionary<string, string> filtrosBusqueda)
        {
            #region Vars
            bool booResult = false;
            clsVersionDocumento objCrlVersion = new clsVersionDocumento();
            List<clsVersionDocumento> lstCrlVersion = new List<clsVersionDocumento>();
            clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
            #endregion Vars
            lstCrlVersion = cCrtVersion.mtdConsultarControlVersion(ref strErrMsg, ref lstCrlVersion, filtrosBusqueda);

            // Guarda la variable en sesión
            Session["lstCrlVersion"] = lstCrlVersion;

            if (lstCrlVersion != null)
            {
                mtdLoadControlVersion();
                mtdLoadControlVersion(lstCrlVersion);
                GVcontrolDocumento.DataSource = lstCrlVersion;
                GVcontrolDocumento.PageIndex = pagIndex;
                GVcontrolDocumento.DataBind();
                GVcontrolDocumento.Visible = true;
                booResult = true;
            }
            else
            {
                GVcontrolDocumento.DataSource = null;
                GVcontrolDocumento.DataBind();
                GVcontrolDocumento.Visible = false;
                omb.ShowMessage("No hay información para cargar", 2, "Atención");
                strErrMsg = "No hay información para cargar";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadControlVersion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("strNombreDocumento", typeof(string));
            grid.Columns.Add("dtFechaImplementacion", typeof(string));
            grid.Columns.Add("strCodigoDocumento", typeof(string));


            GVcontrolDocumento.DataSource = grid;
            GVcontrolDocumento.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadControlVersion(List<clsVersionDocumento> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsVersionDocumento objEvaComp in lstControl)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strNombreProceso.ToString().Trim(),
                    objEvaComp.strNombreDocumento.ToString().Trim(),
                    objEvaComp.dtFechaImplementacion.ToString().Trim(),
                    objEvaComp.strCodigoDocumento.ToString().Trim()
                    });
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

        private void mtdEnviarNotificaciones(string nombreDoc, string codigoDoc, string justificacion)
        {
            try
            {

                string TextoAdicional = string.Empty;
                string direccionCorreo = string.Empty;

                // Envia la notificación de acuerdo a la condicion especifica para cada caso
                switch (ddlEstadoDocumento.SelectedValue)
                {
                    case "0":
                        if (string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)) == string.Empty)
                        {
                            TextoAdicional = "DOCUMENTO CARGADO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento {nombreDoc.ToUpper().Trim()} ha sido cargado.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigoDoc.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Observaciones : " + Sanitizer.GetSafeHtmlFragment(justificacion.Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Nombre Documento: " + Sanitizer.GetSafeHtmlFragment(nombreDoc.Trim()) + "<br>";
                            EnviarNotificacion(35, 1, Convert.ToInt16(lblIdDependencia4.Text), DateTime.Now.ToString().Trim(), TextoAdicional);
                        }
                        break;

                    case "1":
                        if (string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)) != string.Empty)/*ddlEstadoDocumento.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)) && string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)) == "3"*/
                        {
                            TextoAdicional = "DOCUMENTO CARGADO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento {nombreDoc.ToUpper().Trim()} que había sido devuelto ha sido cargado nuevamente.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigoDoc.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Observaciones : " + Sanitizer.GetSafeHtmlFragment(justificacion.Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Nombre Documento: " + Sanitizer.GetSafeHtmlFragment(nombreDoc.Trim()) + "<br>";
                            EnviarNotificacion(35, 1, Convert.ToInt16(lblIdDependencia4.Text), DateTime.Now.ToString().Trim(), TextoAdicional);
                        };
                        break;
                    case "2":
                        if (ddlEstadoDocumento.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)))
                        {
                            TextoAdicional = "DOCUMENTO APROBADO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento {nombreDoc.ToUpper().Trim()} cargado por usted ha sido aprobado.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigoDoc.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Observaciones : " + Sanitizer.GetSafeHtmlFragment(justificacion.Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Nombre Documento: " + Sanitizer.GetSafeHtmlFragment(nombreDoc.Trim()) + "<br>";

                            direccionCorreo = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "CorreoUsuario").Select(o => o.Value));
                            if (direccionCorreo != string.Empty)
                                //HelperCorreo.EnviarCorreo(direccionCorreo, $"<div><br />El documento {nombreDoc.ToUpper()} con código [{codigoDoc}] cargado por usted ha sido aprobado. <br /><br />Observaciones: {justificacion}</div>", "Documento Aprobado");
                                EnviarNotificacion(35, 1, Convert.ToInt16(direccionCorreo), DateTime.Now.ToString().Trim(), TextoAdicional);
                        };
                        break;
                    case "3":
                        if (ddlEstadoDocumento.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)))
                        {

                            TextoAdicional = "DOCUMENTO DEVUELTO" + "<br><br>";
                            TextoAdicional = TextoAdicional + $"El documento {nombreDoc.ToUpper().Trim()} cargado por usted ha sido devuelto.<br>";
                            TextoAdicional = TextoAdicional + "<br>";
                            TextoAdicional = TextoAdicional + " Código : " + Sanitizer.GetSafeHtmlFragment(codigoDoc.ToUpper().Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Nombre Documento: " + Sanitizer.GetSafeHtmlFragment(nombreDoc.Trim()) + "<br>";
                            TextoAdicional = TextoAdicional + " Observaciones : " + Sanitizer.GetSafeHtmlFragment(justificacion.Trim()) + "<br>";

                            direccionCorreo = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "CorreoUsuario").Select(o => o.Value));
                            if (direccionCorreo != string.Empty)
                                EnviarNotificacion(35, 1, Convert.ToInt16(direccionCorreo), DateTime.Now.ToString().Trim(), TextoAdicional);
                            //HelperCorreo.EnviarCorreo(direccionCorreo, $"<div><br />El documento {nombreDoc.ToUpper()} con código [{codigoDoc}] cargado por usted ha sido devuelto. <br /><br />Observaciones: {justificacion} <br /><br />Por favor modifiquélo y carguélo nuevamente.</div>", "Documento Devuelto");
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

        protected void GVcontrolDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    //Si el estado es aprobado no se permite guardar
                    if (ddlEstadoDocumento.SelectedValue == "1")//|| ddlEstadoDocumento.SelectedValue == "2"
                    {
                        //IBupdateGVC.Visible = false;
                        IBupdateGVC.Visible = true;
                        mtdBloquearCamposEstadoAprobado();
                    }
                    else
                    {
                        IBupdateGVC.Visible = true;
                        mtdDesBloquearCamposEstadoAprobado();
                    }
                    //fin
                    break;
            }
        }

        public void mtdBloquearCamposEstadoAprobado()
        {
            

                ddlCadenaValor.Enabled = false;
                ddlMacroproceso.Enabled = false;
                ddlProceso.Enabled = false;
                ddlSubproceso.Enabled = false;
                TXname.Enabled = false;
                TXcodigoDoc.Enabled = false;
                TXversion.Enabled = false;
                TXfechaimplementacion.Enabled = false;
                TXfechaActual.Enabled = false;
                TXfechaUp.Enabled = false;
                TXfechaDel.Enabled = false;
                DDLtipo.Enabled = false;
                tbxResponsable.Enabled = false;
                imgDependencia4.Visible = false;
                TXalmace.Enabled = false;
                TXrollback.Enabled = false;
                //ddlEstadoDocumento.Enabled = false;
                TXtiempoactivo.Enabled = false;
                TXtiempoinactivo.Enabled = false;
                TXmedio.Enabled = false;
                TXformato.Enabled = false;
                Txdisposicion.Enabled = false;
                fuArchivoPerfil.Enabled = false;
                TXobservaciones.Enabled = false;
                //txtJustificacion.Enabled = false;
        }

        public void mtdDesBloquearCamposEstadoAprobado()
        {
            ddlCadenaValor.Enabled = true;
            ddlMacroproceso.Enabled = true;
            ddlProceso.Enabled = true;
            ddlSubproceso.Enabled = true;
            TXname.Enabled = true;
            TXcodigoDoc.Enabled = true;
            TXversion.Enabled = true;
            TXfechaimplementacion.Enabled = true;
            TXfechaActual.Enabled = true;
            TXfechaUp.Enabled = true;
            TXfechaDel.Enabled = true;
            DDLtipo.Enabled = true;
            tbxResponsable.Enabled = true;
            imgDependencia4.Visible = true;
            TXalmace.Enabled = true;
            TXrollback.Enabled = true;
            ddlEstadoDocumento.Enabled = true;
            TXtiempoactivo.Enabled = true;
            TXtiempoinactivo.Enabled = true;
            TXmedio.Enabled = true;
            TXformato.Enabled = true;
            Txdisposicion.Enabled = true;
            fuArchivoPerfil.Enabled = true;
            TXobservaciones.Enabled = true;
            txtJustificacion.Enabled = true;
        }

        private void mtdShowUpdate(int RowGrid)
        {
            try
            {
                string strErrMsg = string.Empty;
                mtdCargarDDLs();
                GridViewRow row = GVcontrolDocumento.Rows[RowGrid];
                var colsNoVisible = GVcontrolDocumento.DataKeys[RowGrid].Values;
                Session["VerActual"] = colsNoVisible[3].ToString();
                BodyGridRNC.Visible = false;
                divFiltrosBusqueda.Visible = false;
                BodyFormCDV.Visible = true;
                #region DatosControl
                txtId.Text = row.Cells[0].Text;
                Session["IdControl"] = row.Cells[0].Text;

                // Se obtiene el documento actual
                List<clsVersionDocumento> lst = (List<clsVersionDocumento>)Session["lstCrlVersion"];
                clsVersionDocumento doc = new clsVersionDocumento();
                doc = lst.Find(item => item.intId == Convert.ToInt32(row.Cells[0].Text));

                /***************************************************************
                 * Control de version
                 * **************************************************************/
                 

                // Se guardan lo datos anteriores necesarios para una posterior validación
                //if (((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Count == 0)
                //{
                //    ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("CorreoResponsable", doc.intidCargoResponsable.ToString());
                //    ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("CorreoUsuario", doc.IdJerarquiaUsuario.ToString());
                //    ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("EstadoAnterior", doc.IdEstadoDocumento.ToString());
                //}
                mtdLoadDDLMacroProceso(doc.IdCadenaValor);
                mtdLoadDDLProceso(doc.intIdMacroProceso);
                mtdLoadDDLSubproceso(doc.IdProceso);

                // llena el formulario
                if (ddlCadenaValor.Items.OfType<ListItem>().Any(x => x.Value == doc.IdCadenaValor.ToString()))
                    ddlCadenaValor.SelectedValue = doc.IdCadenaValor.ToString();
                if (ddlMacroproceso.Items.OfType<ListItem>().Any(x => x.Value == doc.intIdMacroProceso.ToString()))
                    ddlMacroproceso.SelectedValue = doc.intIdMacroProceso.ToString();
                if (ddlProceso.Items.OfType<ListItem>().Any(x => x.Value == doc.IdProceso.ToString()))
                    ddlProceso.SelectedValue = doc.IdProceso.ToString();
                if (ddlSubproceso.Items.OfType<ListItem>().Any(x => x.Value == doc.IdSubproceso.ToString()))
                    ddlSubproceso.SelectedValue = doc.IdSubproceso.ToString();
                
                    TXname.Text = doc.strNombreDocumento;
                    TXfechaimplementacion.Text = doc.dtFechaImplementacion;
                    TXcodigoDoc.Text = doc.strCodigoDocumento;
                    tbxUsuarioCreacion.Text = doc.strUsuario;
                    txtFecha.Text = doc.dtFechaRegistro.ToString();
                    tbxProcIndica.Text = doc.intIdMacroProceso.ToString();
                    DDLtipo.SelectedValue = doc.intIdTipoDocumento.ToString();
                    lblIdDependencia4.Text = doc.intidCargoResponsable.ToString();
                    tbxResponsable.Text = doc.strNombreCargo;
                    tbxResponsable.Enabled = true;
                    imgDependencia4.Visible = true;
                    TXalmace.Text = doc.strUbicacionAlmacenamiento;
                    TXrollback.Text = doc.strRecuperacion;
                    TXtiempoactivo.Text = doc.strTiempoRetencionActivo;
                    TXtiempoinactivo.Text = doc.strTiempoRetencionInactivo;
                    Txdisposicion.Text = doc.strDisposicionFinal;
                    TXmedio.Text = doc.strMedioSoporte;
                    TXformato.Text = doc.strFormato;
                    trEstadoDocumento.Visible = true;
                    trJustificacion.Visible = true;
                    ddlEstadoDocumento.SelectedValue = doc.IdEstadoDocumento.ToString();


                

                #endregion
                #region Version Documento
                #region Vars

                //string strErrMsg = string.Empty;
                clsControlVersion objCrlVersion = new clsControlVersion();
                List<clsControlVersion> lstCrlVersion = new List<clsControlVersion>();
                clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
                #endregion Vars
                #region Validación Estado Documento
                // Bloquea el aprobado si no es el responsable
                string jerarquiaActual = Session["IdJerarquia"].ToString();

                if (jerarquiaActual != lblIdDependencia4.Text)
                {
                    ListItem item = ddlEstadoDocumento.Items.FindByValue("2");
                    item.Attributes.Add("style", "display:none");

                    // Bloquea el estado devuelto si el estado es cargado
                    if (doc.IdEstadoDocumento == 1)
                    {
                        item = ddlEstadoDocumento.Items.FindByValue("3");
                        item.Attributes.Add("style", "display:none");
                    }
                    //DVVersiones.Visible = false;
                }
                else if (jerarquiaActual != doc.IdJerarquiaUsuario.ToString())
                {
                    // Oculta la opción cargado a quien no haya tenido el último contacto con el documento

                    ListItem item = ddlEstadoDocumento.Items.FindByValue("1");
                    item.Attributes.Add("style", "display:none");
                }
                #endregion
                int IdVersionDocumento = Convert.ToInt32(row.Cells[0].Text);
                lstCrlVersion = cCrtVersion.mtdConsultarVersion(ref strErrMsg, ref lstCrlVersion, ref IdVersionDocumento);
                if (lstCrlVersion != null)
                {
                    mtdLoadVersion();
                    mtdLoadVersion(lstCrlVersion);
                    GVversiones.DataSource = lstCrlVersion;
                    GVversiones.PageIndex = pagIndexVersiones;
                    GVversiones.DataBind();
                    if (jerarquiaActual == lblIdDependencia4.Text)
                        DVVersiones.Visible = true;
                    else
                        DVVersiones.Visible = false;
                }
                lstCrlVersion = lstCrlVersion.Where(x => x.intbitActivo == 1).ToList();
                int idControlVersion = 0;
                //string strJustificaciones = string.Empty;
                foreach (clsControlVersion objControl in lstCrlVersion)
                {

                    /*Daniel Velasquez 10/09/2019 --> Se realiza modificacion de restricciones con la finalidad de que el usuario pueda vizualizar 
                     los elementos que necesita segun requerimiento*/
                    idControlVersion = objControl.intId;
                    if (string.IsNullOrEmpty(objControl.JustificacionCambios) == false)
                    {
                        txtJustificacion.Text = objControl.JustificacionCambios;
                        trJustificacion.Visible = true;
                        trJustificacionAprobacion.Visible = true;
                    }
                    else
                    {
                        txtJustificacionAprobacion.Text = objControl.JustificacionAprobacion;
                        trJustificacion.Visible = true;
                        trJustificacionAprobacion.Visible = false;
                    }
                    TXversion.Text = objControl.strVersion;
                    // Se guardan lo datos anteriores necesarios para una posterior validación
                    if (((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Count == 0)
                    {
                        ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("CorreoResponsable", objControl.CargoResponsable.ToString());
                        ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("CorreoUsuario", objControl.intIdUsuario.ToString());
                        ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("EstadoAnterior", doc.IdEstadoDocumento.ToString());
                    }
                }
                
                Session["idControlVersion"] = idControlVersion;


                #endregion
                // Si el documento es aprobado se inhabilita
                if(doc.IdEstadoDocumento == 2)
                    DVVersiones.Visible = true;
                if (doc.IdEstadoDocumento == 1)
                {
                    //ddlEstadoDocumento.Enabled = false;
                    GridViewRow Vrow = GVversiones.Rows[0];
                    string version = ((Label)Vrow.FindControl("version")).Text;
                    TXversion.Text = version;
                    Session["VerActual"] = version;
                    string dtFechaModificacion = ((Label)Vrow.FindControl("dtFechaModificacion")).Text;
                    TXfechaUp.Text = dtFechaModificacion;
                    //DVVersiones.Visible = true;
                }
                if (TXfechaUp.Text == string.Empty)
                {
                    TXfechaUp.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al modificar el perfil. {ex.Message}", 1);
            }

        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadVersion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strVersion", typeof(string));
            grid.Columns.Add("strTipoDocumento", typeof(string));
            grid.Columns.Add("dtFechaModificacion", typeof(string));
            grid.Columns.Add("dtFechaEliminacion", typeof(string));
            grid.Columns.Add("strObservaciones", typeof(string));
            grid.Columns.Add("strPathFile", typeof(string));
            grid.Columns.Add("intBitActivo", typeof(string));
            GVversiones.DataSource = grid;
            GVversiones.DataBind();
            InfoGridVersiones = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadVersion(List<clsControlVersion> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsControlVersion objEvaComp in lstControl)
            {

                InfoGridVersiones.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strVersion.ToString().Trim(),
                    objEvaComp.strTipoDocumento.ToString().Trim(),
                    objEvaComp.dtFechaModificacion.ToString().Trim(),
                    objEvaComp.dtFechaEliminacion.ToString().Trim(),
                    objEvaComp.strObservaciones.ToString().Trim(),
                    objEvaComp.strPathFIle.ToString().Trim(),
                    objEvaComp.intbitActivo.ToString().Trim()
                    });
            }
        }

        protected void GVversiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    if (cCuenta.permisosAgregar(IdFormularioDownloadFile) == "False")
                    {
                        omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(LblDocumentoActivo.Text))
                            mtdDownloadFile(RowGrid);
                        else
                            mtdReDownloadFile(RowGrid);

                    }
                    break;
                case "Activar":
                    if (cCuenta.permisosAgregar(IdFormularioActivarFile) == "False")
                    {
                        omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                        return;
                    }
                    else
                    {
                        mtdActivarFile(RowGrid);
                    }
                    break;
            }
        }

        public void mtdActivarFile(int RowGrid)
        {
            string strEstado = string.Empty;
            GridViewRow row = GVversiones.Rows[RowGrid];
            var colsNoVisible = GVversiones.DataKeys[RowGrid].Values;
            int IdRegistro = Convert.ToInt32(txtId.Text);
            int bitActivo = Convert.ToInt32(colsNoVisible[0].ToString());
            //string IdArControlVersion = GVversiones.Rows[RowGrid].Cells[0].Text;
            string IdArControlVersion = colsNoVisible[1].ToString();
            string strErrMsg = string.Empty;
            if (string.IsNullOrEmpty(LblDocumentoActivo.Text))
                LblDocumentoActivo.Text = Convert.ToString(bitActivo.ToString());

            if (LblDocumentoActivo.Text == "1")
            {
                strEstado = "inactivado";
                cProcesos.ActivarControlVersion(IdArControlVersion.ToString(), 0);
                LblDocumentoActivo.Text = "0";
            }
            else
            {
                strEstado = "activado";
                cProcesos.ActivarControlVersion(IdArControlVersion.ToString(), 1);
                LblDocumentoActivo.Text = "1";
            }
            omb.ShowMessage("Documento |codigo:" + IdRegistro + "| " + strEstado + " correctamente", 3, "Atención");
            //int IdVersionDocumento = Convert.ToInt32(row.Cells[0].Text);
            clsControlVersion objCrlVersion = new clsControlVersion();
            List<clsControlVersion> lstCrlVersion = new List<clsControlVersion>();
            clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
            lstCrlVersion = cCrtVersion.mtdConsultarVersion(ref strErrMsg, ref lstCrlVersion, ref IdRegistro);
            if (lstCrlVersion != null)
            {
                mtdLoadVersion();
                mtdLoadVersion(lstCrlVersion);
                GVversiones.DataSource = lstCrlVersion;
                GVversiones.PageIndex = pagIndexVersiones;
                GVversiones.DataBind();
            }
            lstCrlVersion = lstCrlVersion.Where(x => x.intbitActivo == 1).ToList();
            int idControlVersion = 0;
            foreach (clsControlVersion objControl in lstCrlVersion)
            {
                idControlVersion = objControl.intId;
            }
        }

        public void mtdReDownloadFile(int RowGrid)
        {
            GridViewRow row = GVversiones.Rows[RowGrid];
            var colsNoVisible = GVversiones.DataKeys[RowGrid].Values;
            int IdRegistro = Convert.ToInt32(txtId.Text);
            int bitActivo = Convert.ToInt32(colsNoVisible[0].ToString());
            string archivo = ((Label)row.FindControl("archivo")).Text;
            clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
            string strErrMsg = string.Empty;
            if (LblDocumentoActivo.Text == "1")
            {
                string extension = cCrtVersion.mtdDownLoadFileData(ref strErrMsg, ref IdRegistro, archivo);
                if (extension != "")
                {
                    Byte[] file = cCrtVersion.mtdDownLoadFile(ref strErrMsg, ref IdRegistro, archivo);
                    extension = extension.Remove(0, 1);
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + archivo);
                    Response.ContentType = extension;
                    this.Response.BinaryWrite(file);
                    this.Response.End();
                }
                else
                {
                    omb.ShowMessage("No hay documentos para descargar", 2, "Atención");
                }
            }
            else
            {
                omb.ShowMessage("Documento Inactivo", 2, "Atención");
            }
        }

        public void mtdDownloadFile(int RowGrid)
        {
            GridViewRow row = GVversiones.Rows[RowGrid];
            var colsNoVisible = GVversiones.DataKeys[RowGrid].Values;
            int IdRegistro = Convert.ToInt32(txtId.Text);
            int bitActivo = Convert.ToInt32(colsNoVisible[0].ToString());
            string archivo = ((Label)row.FindControl("archivo")).Text;
            clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
            string strErrMsg = string.Empty;
            if (bitActivo == 1)
            {
                /*if (DDLtipo.SelectedValue == "1")
                {*/
                string extension = cCrtVersion.mtdDownLoadFileData(ref strErrMsg, ref IdRegistro, archivo);
                if (extension != "")
                {
                    Byte[] file = cCrtVersion.mtdDownLoadFile(ref strErrMsg, ref IdRegistro, archivo);
                    extension = extension.Remove(0, 1);
                    //System.IO.File.WriteAllBytes(archivo, file);
                    /*Response.Clear();
                    Response.AddHeader("content-disposition", "at‌​tachment;filename=" + archivo);
                    if(extension == "pdf")
                        Response.ContentType = "application/pdf";
                    if(extension == "doc" || extension == "docx")
                        Response.ContentType = "application/ms-word";
                    if (extension == "xlsx" || extension == "xls")
                        Response.ContentType = "application/vnd.xls";
                    Response.BinaryWrite(file);
                    Response.End();*/
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + archivo);
                    //Set the content type as file extension type
                    Response.ContentType = extension;
                    //Write the file content
                    this.Response.BinaryWrite(file);
                    this.Response.End();
                }
                else
                {
                    omb.ShowMessage("No hay documentos para descargar", 2, "Atención");
                }
                /*}else
                {
                    omb.ShowMessage("Solo el tipo 'Documento' puede realizar descarga", 2, "Atención");
                }*/
            }
            else
            {
                omb.ShowMessage("Archivo no habilitado usa la ultima versión", 2, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
            mtdStard();
        }

        protected void GVcontrolDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVversiones.PageIndex = PagIndex;
            GVversiones.DataBind();
            //mtdLoadControlVersion(ref strErrMsg);
        }

        protected void DDLtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLtipo.SelectedValue.ToString() == "3")
            {
                RFVfechaDel.Enabled = true;
                //RFVfechaDel2.Enabled = true;
            }
            else
            {
                RFVfechaDel.Enabled = false;
                //RFVfechaDel2.Enabled = false;
            }
        }

        protected void ddlCadenaValorFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMacroprocesoFiltro.Items.Clear();

            if (mtdLoadDDLMacroProceso(Convert.ToInt32(ddlCadenaValorFiltro.SelectedValue)))
            {
                ddlMacroprocesoFiltro.ClearSelection();
                ddlProcesoFiltro.Items.Clear();

            };
        }

        protected void ddlMacroprocesoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesoFiltro.Items.Clear();

            if (mtdLoadDDLProceso(Convert.ToInt32(ddlMacroprocesoFiltro.SelectedValue)))
            {
                ddlProcesoFiltro.ClearSelection();
            }
        }

        protected void ddlProcesoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubprocesoFiltro.Items.Clear();

            if (mtdLoadDDLSubproceso(Convert.ToInt32(ddlProcesoFiltro.SelectedValue)))
            {
                ddlSubprocesoFiltro.ClearSelection();
                if (ddlSubproceso.Items.Count == 1)
                {
                    omb.ShowMessage("No hay información de Subprocesos", 2, "Atención");
                }
            }
        }

        protected void btnBusquedaFiltro_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                Dictionary<string, string> filtrosBusqueda = new Dictionary<string, string>
            {
                { "IdCadenaValor", ddlCadenaValorFiltro.SelectedValue },
                { "IdMacroProceso", ddlMacroprocesoFiltro.SelectedValue },
                { "IdProceso", ddlProcesoFiltro.SelectedValue },
                { "IdSubproceso", ddlSubprocesoFiltro.SelectedValue},
                { "NombreDocumento", txtNombreFiltro.Text },
                { "CodigoDocumento", txtCodigoFiltro.Text },
                { "FechaImplementacion", txtFechaImplementacionFiltro.Text },
                { "IdTipoDocumento", ddlTipoDocumentoFiltro.SelectedValue }
            };

                string where = "WHERE " + string.Join(" AND ", filtrosBusqueda.Where(o => o.Value != string.Empty && o.Value != "0").Select(o => $"cast(UPPER({ o.Key }) as varchar(32)) = '{ o.Value }'"));

                if (where == "WHERE ")
                    where = string.Empty;

                Session["FiltroActual"] = where;

                mtdLoadControlVersion(ref strErrMsg, filtrosBusqueda);
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los documentos: {ex.Message}", 1, "Error");
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, ImageClickEventArgs e)
        {
            ddlCadenaValorFiltro.ClearSelection();
            ddlMacroprocesoFiltro.ClearSelection();
            ddlProcesoFiltro.ClearSelection();
            txtNombreFiltro.Text = string.Empty;
            txtCodigoFiltro.Text = string.Empty;
            txtFechaImplementacionFiltro.Text = string.Empty;
            ddlTipoDocumentoFiltro.ClearSelection();
            GVcontrolDocumento.DataSource = null;
            GVcontrolDocumento.DataBind();
        }

        protected void SqlDataSource200_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        protected void GVversiones_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < GVversiones.Rows.Count; rowIndex++)
            {
                GridViewRow row = GVversiones.Rows[rowIndex];
                int booActivo = 0;
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if (cellIndex == 11)
                    {
                        booActivo = Convert.ToInt32(((Label)row.FindControl("booActivo")).Text);
                        ImageButton ImgBnt = ((ImageButton)row.FindControl("ImgBtnInact"));
                        if (booActivo == 0)
                            ImgBnt.ImageUrl = "~/Imagenes/Icons/switch-off-icon.png";
                    }
                }
            }
        }

        protected void ddlEstadoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlEstadoDocumento.SelectedValue == "2")
            {
                trJustificacion.Visible = false;
                trJustificacionAprobacion.Visible = true;
            }
            else
            {
                trJustificacion.Visible = true;
                trJustificacionAprobacion.Visible = false;
            }
        }
    }
}
