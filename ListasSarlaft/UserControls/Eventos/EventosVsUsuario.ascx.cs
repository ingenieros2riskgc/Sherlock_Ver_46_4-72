using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using ClosedXML.Excel;

namespace ListasSarlaft.UserControls.Eventos
{
    public partial class EventosVsUsuario : System.Web.UI.UserControl
    {
        string IdFormulario = "5022";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdInicializarValores();

                }
            }
        }
        #region Properties
        private DataTable infoGridEVU;
        private int rowGridEVU;
        private int pagIndexEVU;

        private DataTable InfoGridEVU
        {
            get
            {
                infoGridEVU = (DataTable)ViewState["infoGridEVU"];
                return infoGridEVU;
            }
            set
            {
                infoGridEVU = value;
                ViewState["infoGrid1"] = infoGridEVU;
            }
        }

        private int RowGridEVU
        {
            get
            {
                rowGridEVU = (int)ViewState["rowGridEVU"];
                return rowGridEVU;
            }
            set
            {
                rowGridEVU = value;
                ViewState["rowGridEVU"] = rowGridEVU;
            }
        }

        private int PagIndexEVU
        {
            get
            {
                pagIndexEVU = (int)ViewState["pagIndexEVU"];
                return pagIndexEVU;
            }
            set
            {
                pagIndexEVU = value;
                ViewState["pagIndexEVU"] = pagIndexEVU;
            }
        }
        #endregion
        private void mtdInicializarValores()
        {
            PagIndexEVU = 0;
            
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            if (!mtdLoadEventosVsUsuario(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2, "Atención");
            /*mtdCargarDDLs();
            PopulateTreeView();*/
        }
        private bool mtdLoadEventosVsUsuario(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            clsDTOEventosVsUsuario objEventvsUser = new clsDTOEventosVsUsuario();
            List<clsDTOEventosVsUsuario> lstEventvsUser = new List<clsDTOEventosVsUsuario>();
            clsBLLEventosVsUsuario cRegistroEVU = new clsBLLEventosVsUsuario();
            #endregion Vars
            int IdUsuarioJerarquia = Convert.ToInt32(Session["IdJerarquia"].ToString());
            lstEventvsUser = cRegistroEVU.mtdConsultarSerivios(ref lstEventvsUser, ref strErrMsg, ref IdUsuarioJerarquia);

            if (lstEventvsUser != null)
            {
                mtdLoadEventosVsUsuario();
                mtdLoadEventosVsUsuario(lstEventvsUser);
                GVeventosUsuario.DataSource = lstEventvsUser;
                GVeventosUsuario.PageIndex = pagIndexEVU;
                GVeventosUsuario.DataBind();
                booResult = true;
            }
            else
            {
                strErrMsg = "El usuario no tiene eventos reportados";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadEventosVsUsuario()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdEvento", typeof(string));
            grid.Columns.Add("strCodigoEvento", typeof(string));
            grid.Columns.Add("strDescripcionEvento", typeof(string));
            grid.Columns.Add("decCuantiaPerdida", typeof(string));
            grid.Columns.Add("intIdGeneraEvento", typeof(string));
            grid.Columns.Add("intGeneraEvento", typeof(string));
            grid.Columns.Add("strNombreGenerador", typeof(string));
            grid.Columns.Add("intIdResponsableEvento", typeof(string));
            grid.Columns.Add("strNombreResponsable", typeof(string));
            grid.Columns.Add("intIdClase", typeof(string));
            grid.Columns.Add("strNombreClaseEvento", typeof(string));

            GVeventosUsuario.DataSource = grid;
            GVeventosUsuario.DataBind();
            infoGridEVU = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="clsDTOEventosVsUsuario">Lista con los Eventos vs Usuario</param>
        private void mtdLoadEventosVsUsuario(List<clsDTOEventosVsUsuario> lstEventvsUser)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOEventosVsUsuario objEventVsUser in lstEventvsUser)
            {

                infoGridEVU.Rows.Add(new Object[] {
                    objEventVsUser.intIdEvento.ToString().Trim(),
                            objEventVsUser.strCodigoEvento.ToString().Trim(),
                            objEventVsUser.strDescripcionEvento.ToString().Trim(),
                            objEventVsUser.decCuantiaPerdida.ToString().Trim(),
                            objEventVsUser.intIdGeneraEvento.ToString().Trim(),
                            objEventVsUser.intGeneraEvento.ToString().Trim(),
                            objEventVsUser.strNombreGenerador.ToString().Trim(),
                            objEventVsUser.intIdResponsableEvento.ToString().Trim(),
                            objEventVsUser.strNombreResponsable.ToString().Trim(),
                            objEventVsUser.intIdClase.ToString().Trim(),
                            objEventVsUser.strNombreClaseEvento.ToString().Trim(),
                    });
            }
        }
    }
}