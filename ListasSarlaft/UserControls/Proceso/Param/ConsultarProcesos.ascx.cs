using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class ConsultarProcesos : System.Web.UI.UserControl
    {
        string IdFormulario = "0000";
        cCuenta cCuenta = new cCuenta();

        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;

        private DataTable infoGrid2;
        private int rowGrid2;
        private int pagIndex2;

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

        private DataTable InfoGrid2
        {
            get
            {
                infoGrid2 = (DataTable)ViewState["infoGrid2"];
                return infoGrid2;
            }
            set
            {
                infoGrid2 = value;
                ViewState["infoGrid2"] = infoGrid2;
            }
        }

        private int RowGrid2
        {
            get
            {
                rowGrid2 = (int)ViewState["rowGrid2"];
                return rowGrid2;
            }
            set
            {
                rowGrid2 = value;
                ViewState["rowGrid2"] = rowGrid2;
            }
        }

        private int PagIndex2
        {
            get
            {
                pagIndex2 = (int)ViewState["pagIndex2"];
                return pagIndex2;
            }
            set
            {
                pagIndex2 = value;
                ViewState["pagIndex2"] = pagIndex2;
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
            string strErrMsg = string.Empty, strProceso = string.Empty, strIdProceso;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    strProceso = Request.QueryString["Proceso"];
                    strIdProceso = Request.QueryString["Id"];

                    if (!mtdValidarProceso(strProceso, strIdProceso))
                    {
                        mtdInicializarValores();

                        if (mtdCargarProceso(strProceso, strIdProceso, ref strErrMsg))
                            omb.ShowMessage(strErrMsg, 3, "Atención");
                    }
                }

            }
        }

        #region Metodos
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            PagIndex2 = 0;
            PagIndex3 = 0;
        }        

        #region Validar
        private bool mtdValidarProceso(string strProceso, string strIdProceso)
        {
            bool booResult = false;

            if (!clsUtilidades.mtdEsNumero(strIdProceso))
                booResult = true;

            if (!booResult)
                if (!mtdValidarTipoProceso(strProceso))
                    booResult = true;

            return booResult;
        }

        private bool mtdValidarTipoProceso(string strProceso)
        {
            bool booResult = false;

            switch (strProceso)
            {
                case "MP":
                case "P":
                case "SP":
                    booResult = true;
                    break;
            }

            return booResult;
        }

        private int mtdObtenerTipoProceso(string strProceso)
        {
            int intResult = 0;

            switch (strProceso)
            {
                case "MP":
                    intResult = 1;
                    break;
                case "P":
                    intResult = 2;
                    break;
                case "SP":
                    intResult = 3;
                    break;
            }

            return intResult;
        }
        #endregion

        #region Cargas
        private bool mtdCargarProceso(string strProceso, string strIdProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsCaracterizacion objCaracter = new clsCaracterizacion(0,
                mtdObtenerTipoProceso(strProceso), Convert.ToInt32(strIdProceso), 0, string.Empty);

            mtdBuscarProceso(strProceso, strIdProceso, ref strErrMsg);

            if (mtdCargarEntradas(objCaracter, ref strErrMsg))
                booResult = true;

            if (!booResult)
                if (mtdCargarActividades(objCaracter, ref strErrMsg))
                    booResult = true;

            if (!booResult)
                if (mtdCargarSalidas(objCaracter, ref strErrMsg))
                    booResult = true;

            return booResult;
        }

        #region Gridview Entrada
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarEntradas(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridEntrada();
            mtdLoadInfoGridEntrada(objCaracter, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridEntrada()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("strProveedor", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la cadena de valor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridEntrada(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            List<clsEntrada> lstEntrada = new List<clsEntrada>();
            clsEntradaBLL cEntrada = new clsEntradaBLL();

            lstEntrada = cEntrada.mtdConsultarEntrada(objCaracter, ref strErrMsg);

            if (lstEntrada != null)
            {
                GridView1.DataSource = lstEntrada;
                GridView1.DataBind();
            }
        }

        #endregion

        #region Gridview Actividades
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarActividades(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridActividades();
            mtdLoadInfoGridActividades(objCaracter, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridActividades()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intCargoResponsable", typeof(string));
            grid.Columns.Add("strNombreCargoResponsable", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView2.DataSource = grid;
            GridView2.DataBind();
            InfoGrid2 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la actividad al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridActividades(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsActividadBLL cActividad = new clsActividadBLL();

            lstActividad = cActividad.mtdConsultarActividad(objCaracter, ref strErrMsg);

            if (lstActividad != null)
            {
                GridView2.DataSource = lstActividad;
                GridView2.DataBind();
            }
        }

        #endregion

        #region Gridview Salidas
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarSalidas(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridSalida();
            mtdLoadInfoGridSalida(objCaracter, ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridSalida()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("strCliente", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView3.DataSource = grid;
            GridView3.DataBind();
            InfoGrid3 = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la cadena de valor al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridSalida(clsCaracterizacion objCaracter, ref string strErrMsg)
        {
            List<clsSalida> lstSalida = new List<clsSalida>();
            clsSalidaBLL cSalida = new clsSalidaBLL();

            lstSalida = cSalida.mtdConsultarSalida(objCaracter, ref strErrMsg);

            if (lstSalida != null)
            {
                GridView3.DataSource = lstSalida;
                GridView3.DataBind();
            }
        }

        #endregion

        #endregion

        private bool mtdBuscarProceso(string strProceso, string strIdProceso, ref string strErrMsg)
        {
            bool booResult = false;
            switch (strProceso)
            {
                case "MP":
                    clsMacroproceso objMPIn = new clsMacroproceso(Convert.ToInt32(strIdProceso),
                        string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty, string.Empty),
                        objMPOut;
                    clsMacroProcesoBLL cMacroP = new clsMacroProcesoBLL();

                    objMPOut = cMacroP.mtdConsultarMacroproceso(objMPIn, ref strErrMsg);

                    if (!string.IsNullOrEmpty(strErrMsg))
                        booResult = true;
                    else
                        mtdLlenarCamposMP(objMPOut);

                    break;
                case "P": //Proceso
                    clsProceso objProcIN  = new clsProceso(Convert.ToInt32(strIdProceso),
                        0, string.Empty, string.Empty, string.Empty,string.Empty, 0, 0, true, 0, string.Empty),
                        objProcOut;
                    clsProcesoBLL cProceso = new clsProcesoBLL();
                    objProcOut = cProceso.mtdConsultarProceso(objProcIN, ref strErrMsg);

                    if (!string.IsNullOrEmpty(strErrMsg))
                        booResult = true;
                    else
                        mtdLlenarCamposProc(objProcOut);
                    break;
                case "SP":
                    clsSubproceso objSubpIN = new clsSubproceso(Convert.ToInt32(strIdProceso),
                        string.Empty, string.Empty, string.Empty, true, 0, 0, 0, string.Empty),
                        ObjSubpOut;
                    clsSubprocesoBLL cSubp = new clsSubprocesoBLL();
                    ObjSubpOut = cSubp.mtdConsultarSubProceso(objSubpIN, ref strErrMsg);

                    if (!string.IsNullOrEmpty(strErrMsg))
                        booResult = true;
                    else
                        mtdLlenarCamposSubProc(ObjSubpOut);

                    break;
            }

            return booResult;
        }

        #region Lllenar Campos
        private void mtdLlenarCamposMP(clsMacroproceso objMPIn)
        {
            tbxCadenaValor.Text = objMPIn.strNombreCadenaValor;
            tbxMacroproceso.Text = objMPIn.strNombreMacroproceso;
            tbxDescripcion.Text = objMPIn.strDescripcion;
            tbxObjetivo.Text = objMPIn.strObjetivo;
            tbxCargo.Text = objMPIn.strNombreResponsable;

            mtdHabilitarCampos(1, true);
        }

        private void mtdLlenarCamposProc(clsProceso objProcIn)
        {
            tbxCadenaValor.Text = objProcIn.strNombreCadenaValor;
            tbxMacroproceso.Text = objProcIn.strNombreMacroProceso;
            tbxProceso.Text = objProcIn.strNombreProceso;
            tbxDescripcion.Text = objProcIn.strDescripcion;
            tbxObjetivo.Text = objProcIn.strObjetivo;
            tbxCargo.Text = objProcIn.strCargoResponsable;

            mtdHabilitarCampos(2, true);
        }

        private void mtdLlenarCamposSubProc(clsSubproceso objSubpIn)
        {
            tbxCadenaValor.Text = objSubpIn.strNombreCadenaValor;
            tbxMacroproceso.Text = objSubpIn.strNombreMacroProceso;
            tbxProceso.Text = objSubpIn.strNombreProceso;
            tbxSubProceso.Text = objSubpIn.strNombreSubproceso;
            tbxDescripcion.Text = objSubpIn.strDescripcion;
            tbxObjetivo.Text = objSubpIn.strObjetivo;
            tbxCargo.Text = objSubpIn.strCargoResponsable;

            mtdHabilitarCampos(3, true);
        }

        void mtdHabilitarCampos(int intTipo, bool booEstado)
        {
            switch (intTipo)
            {
                case 1: //Macroproceso
                    tbxProceso.Visible = !booEstado;
                    tbxSubProceso.Visible = !booEstado;
                    break;
                case 2: //Proceso
                    tbxProceso.Visible = booEstado;
                    tbxSubProceso.Visible = !booEstado;
                    break;
                case 3: //Proceso
                    tbxProceso.Visible = booEstado;
                    tbxSubProceso.Visible = booEstado;
                    break;
            }
        }
        #endregion

        #endregion
    }
}