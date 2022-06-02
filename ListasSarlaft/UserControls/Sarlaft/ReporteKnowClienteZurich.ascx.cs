using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Microsoft.Reporting.WebForms;
using System.Reflection;

namespace ListasSarlaft.UserControls
{
    public partial class ReporteKnowClienteZurich : System.Web.UI.UserControl
    {
        private cKnowClient cKnowClient = new cKnowClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                loadValue();
        }

        private void loadValue()
        {
            int IdConocimientoCliente = Convert.ToInt32(Request.QueryString["IdConocimientoCliente"]);
            string strIdForm = Request.QueryString["IdTipoForm"];

            try
            {
                mtdReporteZurich(IdConocimientoCliente, strIdForm);
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar la información. " + ex.Message);
            }
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdReporteZurich(int IdConocimientoCliente, string strIdForm)
        {
            switch (strIdForm)
            {
                case "0":
                    mtdReporteNatural(IdConocimientoCliente);
                    break;
                case "1":
                    mtdReporteJuridico(IdConocimientoCliente);
                    break;
            }
        }

        private void mtdReporteNatural(int IdConocimientoCliente)
        {
            DataTable dtInfo = new DataTable();
            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[126];
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/ReporteClienteZurich.rdlc");

            #region Cabecera
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente, false);
            parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("FechaFormulario", dtInfo.Rows[0]["FechaFormulario"].ToString());

            #region Clase Vinculacion
            switch (dtInfo.Rows[0]["ClaseVinculacion"].ToString())
            {
                case "TOMADOR":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", "X");
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "ASEGURADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", "X");
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "BENEFICIARIO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", "X");
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "APODERADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", "X");
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "AFIANZADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", "X");
                    break;
                default:
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
            }
            #endregion

            #region Tomador-Asegurado-Afianzado
            switch (dtInfo.Rows[0]["TomadorAsegurado"].ToString())
            {
                case "FAMILIAR":
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", "X");
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
                case "COMERCIAL":
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", "X");
                    parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
                case "LABORAL":
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", "X");
                    parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
                case "PERSONAL":
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", "X");
                    break;
                default:
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
            }
            #endregion

            #region Tomador-Beneficiario
            switch (dtInfo.Rows[0]["TomadorBeneficiario"].ToString())
            {
                case "FAMILIAR":
                    parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", "X");
                    parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
                case "COMERCIAL":
                    parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", "X");
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
                case "LABORAL":
                    parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", "X");
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
                case "PERSONAL":
                    parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", "X");
                    break;
                default:
                    parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
            }
            #endregion

            #region Asegurado-Beneficiario
            switch (dtInfo.Rows[0]["AseguradoBeneficiario"].ToString())
            {
                case "FAMILIAR":
                    parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", "X");
                    parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
                case "COMERCIAL":
                    parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", "X");
                    parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
                case "LABORAL":
                    parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", "X");
                    parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
                case "PERSONAL":
                    parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", "X");
                    break;
                default:
                    parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
            }
            #endregion

            #region Sucursal
            parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("Sucursal", dtInfo.Rows[0]["Sucursal"].ToString());
            #endregion
            #endregion

            #region Natural
            dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);

            #region Apellidos
            parameters[10] = new Microsoft.Reporting.WebForms.ReportParameter("PNPrimerApellido", dtInfo.Rows[0]["PNPrimerApellido"].ToString());
            parameters[11] = new Microsoft.Reporting.WebForms.ReportParameter("PNSegunApellido", dtInfo.Rows[0]["PNSegunApellido"].ToString());
            #endregion

            #region Nombres
            string[] strPartes = dtInfo.Rows[0]["PNNombres"].ToString().Split(' ');
            string strNombre1 = string.Empty, strNombre2 = string.Empty;
            strNombre1 = strPartes[0];
            strNombre2 = strPartes.Length == 1 ? string.Empty : strPartes[1];

            parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PNNombre1", strNombre1);
            parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PNNombre2", strNombre2);
            #endregion

            #region Documento
            #region Tipo Doc
            switch (dtInfo.Rows[0]["PNTipoDocumento"].ToString())
            {
                case "C.C.":
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", "X");
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNRC", string.Empty);
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNPASAPORTE", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOTRO", string.Empty);
                    break;
                case "C.E.":
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", "X");
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNRC", string.Empty);
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNPASAPORTE", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOTRO", string.Empty);
                    break;
                case "T.I.":
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", "X");
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNRC", string.Empty);
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNPASAPORTE", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOTRO", string.Empty);
                    break;
                case "R.C.":
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNRC", "X");
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNPASAPORTE", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOTRO", string.Empty);
                    break;
                case "PASAPORTE":
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNRC", string.Empty);
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNPASAPORTE", "X");
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOTRO", string.Empty);
                    break;
                case "OTRO":
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNRC", string.Empty);
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNPASAPORTE", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOTRO", "X");
                    break;
                default:
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNRC", string.Empty);
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNPASAPORTE", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOTRO", string.Empty);
                    break;
            }
            #endregion

            parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PNNumeroDocumento", dtInfo.Rows[0]["PNNumeroDocumento"].ToString());
            parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PNLugar", dtInfo.Rows[0]["PNLugar"].ToString());
            parameters[21] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaExpedicion", dtInfo.Rows[0]["PNFechaExpedicion"].ToString());
            #endregion

            #region Sexo
            switch (dtInfo.Rows[0]["PNSexo"].ToString())
            {
                case "MASCULINO":
                    parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoM", "X");
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoF", string.Empty);
                    break;
                case "FEMENINO":
                    parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoM", string.Empty);
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoF", "X");
                    break;
                default:
                    parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoM", string.Empty);
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoF", string.Empty);
                    break;
            }
            #endregion

            #region Nacionalidad
            parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaNacimiento", dtInfo.Rows[0]["PNFechaNacimiento"].ToString());
            parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNNacionalidad1", dtInfo.Rows[0]["PNNacionalidad"].ToString());
            parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNNacionalidad2", dtInfo.Rows[0]["PNNacionalidad2"].ToString());
            parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNOcupacionOficio", dtInfo.Rows[0]["PNOcupacionOficio"].ToString());
            #endregion

            #region Estado Civil
            switch (dtInfo.Rows[0]["PNEstadoCivil"].ToString())
            {
                case "CASADO":
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "X");
                    parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", string.Empty);
                    parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", string.Empty);
                    parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", string.Empty);
                    break;
                case "SOLTERO":
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", string.Empty);
                    parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "X");
                    parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", string.Empty);
                    parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", string.Empty);
                    break;
                case "SEPARADO":
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", string.Empty);
                    parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", string.Empty);
                    parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "X");
                    parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", string.Empty);
                    break;
                case "VIUDO":
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", string.Empty);
                    parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", string.Empty);
                    parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", string.Empty);
                    parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "X");
                    break;
                default:
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", string.Empty);
                    parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", string.Empty);
                    parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", string.Empty);
                    parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", string.Empty);
                    break;
            }
            #endregion

            #region Residencia
            parameters[32] = new Microsoft.Reporting.WebForms.ReportParameter("PNDireccionResidencia", dtInfo.Rows[0]["PNDireccionResidencia"].ToString());
            parameters[33] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudad1", dtInfo.Rows[0]["PNCiudad1"].ToString());
            parameters[34] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelefono1", dtInfo.Rows[0]["PNTelefono1"].ToString());
            parameters[35] = new Microsoft.Reporting.WebForms.ReportParameter("PNCelular", dtInfo.Rows[0]["PNCelular"].ToString());
            parameters[36] = new Microsoft.Reporting.WebForms.ReportParameter("PNCorreoElectronico", dtInfo.Rows[0]["PNCorreoElectronico"].ToString());
            #endregion

            #region Act. Econ.
            #region Tipo Act. Econ.
            switch (dtInfo.Rows[0]["PNActividadEconomica"].ToString())
            {
                case "ASALARIADO":
                    parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNAsalariado", "X");
                    parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstudiante", string.Empty);
                    parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNAma", string.Empty);
                    parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNRentista", string.Empty);
                    parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPensionado", string.Empty);
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNIndependiente", string.Empty);
                    break;
                case "ESTUDIANTE":
                    parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNAsalariado", string.Empty);
                    parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstudiante", "X");
                    parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNAma", string.Empty);
                    parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNRentista", string.Empty);
                    parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPensionado", string.Empty);
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNIndependiente", string.Empty);
                    break;
                case "AMA DE CASA":
                    parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNAsalariado", string.Empty);
                    parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstudiante", string.Empty);
                    parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNAma", "X");
                    parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNRentista", string.Empty);
                    parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPensionado", string.Empty);
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNIndependiente", string.Empty);
                    break;
                case "RENTISTA":
                    parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNAsalariado", string.Empty);
                    parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstudiante", string.Empty);
                    parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNAma", string.Empty);
                    parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNRentista", "X");
                    parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPensionado", string.Empty);
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNIndependiente", string.Empty);
                    break;
                case "PENSIONADO":
                    parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNAsalariado", string.Empty);
                    parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstudiante", string.Empty);
                    parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNAma", string.Empty);
                    parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNRentista", string.Empty);
                    parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPensionado", "X");
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNIndependiente", string.Empty);
                    break;
                case "INDEPENDIENTE":
                    parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNAsalariado", string.Empty);
                    parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstudiante", string.Empty);
                    parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNAma", string.Empty);
                    parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNRentista", string.Empty);
                    parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPensionado", string.Empty);
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNIndependiente", "X");
                    break;
                default:
                    parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNAsalariado", string.Empty);
                    parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstudiante", string.Empty);
                    parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNAma", string.Empty);
                    parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNRentista", string.Empty);
                    parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPensionado", string.Empty);
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNIndependiente", string.Empty);
                    break;
            }
            #endregion

            parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtraActEconomica", dtInfo.Rows[0]["PNOtraActEconomica"].ToString());
            #endregion

            #region Empresa
            parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PNEmpresaTrabajo", dtInfo.Rows[0]["PNEmpresaTrabajo"].ToString());
            parameters[45] = new Microsoft.Reporting.WebForms.ReportParameter("PNCargo", dtInfo.Rows[0]["PNCargo"].ToString());
            parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudad2", dtInfo.Rows[0]["PNCiudad2"].ToString());
            parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PNDireccion", dtInfo.Rows[0]["PNDireccion"].ToString());
            parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelefono2", dtInfo.Rows[0]["PNTelefono2"].ToString());
            parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PNFax", dtInfo.Rows[0]["PNFax"].ToString());
            #endregion

            #region Ingresos
            parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PNIngresosMensuales", dtInfo.Rows[0]["PNIngresosMensuales"].ToString());
            parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PNActivos", dtInfo.Rows[0]["PNActivos"].ToString());
            parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PNEgresoMensuales", dtInfo.Rows[0]["PNEgresoMensuales"].ToString());
            parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PNPasivos", dtInfo.Rows[0]["PNPasivos"].ToString());
            parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtrosIngresos", dtInfo.Rows[0]["PNOtrosIngresos"].ToString());
            parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PNConceptoOtrosIngresos", dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString());
            #endregion

            #region Obligacion tributaria otros paises
            #region SI/NO
            switch (dtInfo.Rows[0]["PNPregunta1"].ToString())
            {
                case "SI":
                    parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PNObliSI", "X");
                    parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PNObliNO", string.Empty);
                    break;
                case "NO":
                    parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PNObliSI", string.Empty);
                    parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PNObliNO", "X");
                    break;
                default:
                    parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PNObliSI", string.Empty);
                    parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PNObliNO", string.Empty);
                    break;
            }
            #endregion

            parameters[58] = new Microsoft.Reporting.WebForms.ReportParameter("PNEspecificacionPreguntas", dtInfo.Rows[0]["PNEspecificacionPreguntas"].ToString());
            #endregion
            #endregion

            #region Financiera
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);

            #region SI/NO
            switch (dtInfo.Rows[0]["TransacMonedaExtra"].ToString())
            {
                case "SI":
                    parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranSI", "X");
                    parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranNO", string.Empty);
                    break;
                case "NO":
                    parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranSI", string.Empty);
                    parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranNO", "X");
                    break;
                default:
                    parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranSI", string.Empty);
                    parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranNO", string.Empty);
                    break;
            }
            #endregion

            #region Tipo Transaccion
            switch (dtInfo.Rows[0]["TipoTransaccion"].ToString())
            {
                case "IMPORTACIONES":
                    parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", "X");
                    parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "EXPORTACIONES":
                    parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", "X");
                    parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "INVERSIONES":
                    parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", "X");
                    parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "TRANSFERENCIAS":
                    parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", "X");
                    parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "OTRA":
                    parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", "X");
                    break;
                default:
                    parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
            }

            parameters[66] = new Microsoft.Reporting.WebForms.ReportParameter("OtroTipoTransaccion", dtInfo.Rows[0]["OtroTipoTransaccion"].ToString());
            #endregion

            #region Prod. Ext
            #region SI/NO
            switch (dtInfo.Rows[0]["PFProdExterior"].ToString())
            {
                case "SI":
                    parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorSI", "X");
                    parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorNO", string.Empty);
                    break;
                case "NO":
                    parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorSI", string.Empty);
                    parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorNO", "X");
                    break;
                default:
                    parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorSI", string.Empty);
                    parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorNO", string.Empty);
                    break;
            }
            #endregion

            #region Prod 1
            parameters[69] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto1", dtInfo.Rows[0]["PFTipoProducto1"].ToString());
            parameters[70] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto1", dtInfo.Rows[0]["PFNumeroProducto1"].ToString());
            parameters[71] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad1", dtInfo.Rows[0]["PFEntidad1"].ToString());
            parameters[72] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto1", dtInfo.Rows[0]["PFMonto1"].ToString());
            parameters[73] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad1", dtInfo.Rows[0]["PFCiudad1"].ToString());
            parameters[74] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais1", dtInfo.Rows[0]["PFPais1"].ToString());
            parameters[75] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda1", dtInfo.Rows[0]["PFMoneda1"].ToString());
            #endregion

            #region Prod 2
            parameters[76] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto2", dtInfo.Rows[0]["PFTipoProducto2"].ToString());
            parameters[77] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto2", dtInfo.Rows[0]["PFNumeroProducto2"].ToString());
            parameters[78] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad2", dtInfo.Rows[0]["PFEntidad2"].ToString());
            parameters[79] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto2", dtInfo.Rows[0]["PFMonto2"].ToString());
            parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad2", dtInfo.Rows[0]["PFCiudad2"].ToString());
            parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais2", dtInfo.Rows[0]["PFPais2"].ToString());
            parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda2", dtInfo.Rows[0]["PFMoneda2"].ToString());
            #endregion

            #region Prod 3
            parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto3", dtInfo.Rows[0]["PFTipoProducto3"].ToString());
            parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto3", dtInfo.Rows[0]["PFNumeroProducto3"].ToString());
            parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad3", dtInfo.Rows[0]["PFEntidad3"].ToString());
            parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto3", dtInfo.Rows[0]["PFMonto3"].ToString());
            parameters[87] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad3", dtInfo.Rows[0]["PFCiudad3"].ToString());
            parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais3", dtInfo.Rows[0]["PFPais3"].ToString());
            parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda3", dtInfo.Rows[0]["PFMoneda3"].ToString());
            #endregion
            #endregion
            #endregion

            #region Seguros y Fondos
            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);

            parameters[90] = new Microsoft.Reporting.WebForms.ReportParameter("OrigenFondos", dtInfo.Rows[0]["OrigenFondos"].ToString());

            #region Seguro 1
            parameters[91] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno1", dtInfo.Rows[0]["SeguroAno1"].ToString());
            parameters[92] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo1", dtInfo.Rows[0]["SeguroRamo1"].ToString());
            parameters[93] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania1", dtInfo.Rows[0]["SeguroCompania1"].ToString());
            parameters[94] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor1", dtInfo.Rows[0]["SeguroValor1"].ToString());

            #region Tipo Seguro
            switch (dtInfo.Rows[0]["SeguroTipo1"].ToString())
            {
                case "RECLAM.":
                    parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA1", "X");
                    parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM1", string.Empty);
                    break;
                case "INDEMNIZ.":
                    parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA1", string.Empty);
                    parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM1", "X");
                    break;
                default:
                    parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA1", string.Empty);
                    parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM1", string.Empty);
                    break;
            }
            #endregion
            #endregion

            #region Seguro 2
            parameters[97] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno2", dtInfo.Rows[0]["SeguroAno2"].ToString());
            parameters[98] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo2", dtInfo.Rows[0]["SeguroRamo2"].ToString());
            parameters[99] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania2", dtInfo.Rows[0]["SeguroCompania2"].ToString());
            parameters[100] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor2", dtInfo.Rows[0]["SeguroValor2"].ToString());

            #region Tipo Seguro
            switch (dtInfo.Rows[0]["SeguroTipo2"].ToString())
            {
                case "RECLAM.":
                    parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA2", "X");
                    parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM2", string.Empty);
                    break;
                case "INDEMNIZ.":
                    parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA2", string.Empty);
                    parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM2", "X");
                    break;
                default:
                    parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA2", string.Empty);
                    parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM2", string.Empty);
                    break;
            }
            #endregion
            #endregion
            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);

            #region Info Entrevista
            parameters[103] = new Microsoft.Reporting.WebForms.ReportParameter("LugarEntrevista", dtInfo.Rows[0]["LugarEntrevista"].ToString());
            parameters[104] = new Microsoft.Reporting.WebForms.ReportParameter("FechaEntrevista", dtInfo.Rows[0]["FechaEntrevista"].ToString());
            parameters[105] = new Microsoft.Reporting.WebForms.ReportParameter("HoraEntrevista", dtInfo.Rows[0]["HoraEntrevista"].ToString());
            parameters[106] = new Microsoft.Reporting.WebForms.ReportParameter("NombreResponsable", dtInfo.Rows[0]["NombreResponsable"].ToString());

            #region Tipo Resultado
            switch (dtInfo.Rows[0]["Resultado"].ToString())
            {
                case "Aceptado.":
                    parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("RESAceptado", "X");
                    parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("ResRechazado", string.Empty);
                    break;
                case "Rechazado.":
                    parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("RESAceptado", string.Empty);
                    parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("ResRechazado", "X");
                    break;
                default:
                    parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("RESAceptado", string.Empty);
                    parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("ResRechazado", string.Empty);
                    break;
            }
            #endregion

            parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("CCRespEntrevista", dtInfo.Rows[0]["CCRespEntrevista"].ToString());
            parameters[110] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones1", dtInfo.Rows[0]["Observaciones1"].ToString());
            #endregion

            #region Verificacion Entrevista
            parameters[111] = new Microsoft.Reporting.WebForms.ReportParameter("NombreVerifica", dtInfo.Rows[0]["NombreVerifica"].ToString());
            parameters[112] = new Microsoft.Reporting.WebForms.ReportParameter("CCVerificaEntrevista", dtInfo.Rows[0]["CCVerificaEntrevista"].ToString());
            parameters[113] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones2", dtInfo.Rows[0]["Observaciones2"].ToString());
            #endregion
            #endregion

            #region Campos Disponibles
            parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("y", "");
            parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("z", "");
            #endregion

            ReportViewer1.LocalReport.SetParameters(parameters);
        }

        private void mtdReporteJuridico(int IdConocimientoCliente)
        {
            DataTable dtInfo = new DataTable();
            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[139];
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/ReporteClienteJurZurich.rdlc");

            #region Cabecera
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente, false);

            parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("FechaFormulario", dtInfo.Rows[0]["FechaFormulario"].ToString());

            #region Clase Vinculacion
            switch (dtInfo.Rows[0]["ClaseVinculacion"].ToString())
            {
                case "TOMADOR":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", "X");
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "ASEGURADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", "X");
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "BENEFICIARIO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", "X");
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "APODERADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", "X");
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
                case "AFIANZADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", "X");
                    break;
                default:
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("EncTomador", string.Empty);
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("EncAsegurado", string.Empty);
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("EncBeneficiario", string.Empty);
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("EncApoderado", string.Empty);
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("EncAfianzado", string.Empty);
                    break;
            }
            #endregion

            #region Tomador-Asegurado-Afianzado
            switch (dtInfo.Rows[0]["TomadorAsegurado"].ToString())
            {
                case "FAMILIAR":
                    parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", "X");
                    parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[129] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[130] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
                case "COMERCIAL":
                    parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", "X");
                    parameters[129] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[130] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
                case "LABORAL":
                    parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[129] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", "X");
                    parameters[130] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
                case "PERSONAL":
                    parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[129] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[130] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", "X");
                    break;
                default:
                    parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar1", string.Empty);
                    parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer1", string.Empty);
                    parameters[129] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor1", string.Empty);
                    parameters[130] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso1", string.Empty);
                    break;
            }
            #endregion

            #region Tomador-Beneficiario
            switch (dtInfo.Rows[0]["TomadorBeneficiario"].ToString())
            {
                case "FAMILIAR":
                    parameters[131] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", "X");
                    parameters[132] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[133] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[134] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
                case "COMERCIAL":
                    parameters[131] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[132] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", "X");
                    parameters[133] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[134] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
                case "LABORAL":
                    parameters[131] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[132] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[133] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", "X");
                    parameters[134] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
                case "PERSONAL":
                    parameters[131] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[132] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[133] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[134] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", "X");
                    break;
                default:
                    parameters[131] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar2", string.Empty);
                    parameters[132] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer2", string.Empty);
                    parameters[133] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor2", string.Empty);
                    parameters[134] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso2", string.Empty);
                    break;
            }
            #endregion

            #region Asegurado-Beneficiario
            switch (dtInfo.Rows[0]["AseguradoBeneficiario"].ToString())
            {
                case "FAMILIAR":
                    parameters[135] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", "X");
                    parameters[136] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[137] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[138] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
                case "COMERCIAL":
                    parameters[135] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[136] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", "X");
                    parameters[137] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[138] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
                case "LABORAL":
                    parameters[135] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[136] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[137] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", "X");
                    parameters[138] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
                case "PERSONAL":
                    parameters[135] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[136] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[137] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[138] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", "X");
                    break;
                default:
                    parameters[135] = new Microsoft.Reporting.WebForms.ReportParameter("VincFliar3", string.Empty);
                    parameters[136] = new Microsoft.Reporting.WebForms.ReportParameter("VincComer3", string.Empty);
                    parameters[137] = new Microsoft.Reporting.WebForms.ReportParameter("VincLabor3", string.Empty);
                    parameters[138] = new Microsoft.Reporting.WebForms.ReportParameter("VincPerso3", string.Empty);
                    break;
            }
            #endregion

            #region Sucursal
            parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("Sucursal", dtInfo.Rows[0]["Sucursal"].ToString());
            #endregion
            #endregion

            #region Juridica
            dtInfo = cKnowClient.InfoFormPJ(IdConocimientoCliente);

            parameters[10] = new Microsoft.Reporting.WebForms.ReportParameter("PJRazonDenominacion", dtInfo.Rows[0]["PJRazonDenominacion"].ToString());
            parameters[11] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT", dtInfo.Rows[0]["PJNIT"].ToString());

            #region Tipo Sociedad
            switch (dtInfo.Rows[0]["PJTipoSociedad"].ToString())
            {
                case "LTDA":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", "X");
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
                case "S.A.":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", "X");
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PFSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
                case "S.A.S":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", "X");
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
                case "SC":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", "X");
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
                case "COOP":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", "X");
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
                case "E.U.":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", "X");
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
                case "E.S.":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", "X");
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
                case "OTRA":
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", "X");
                    break;
                default:
                    parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PJLTDA", string.Empty);
                    parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJSA", string.Empty);
                    parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJSAS", string.Empty);
                    parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJSC", string.Empty);
                    parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOOP", string.Empty);
                    parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJEU", string.Empty);
                    parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJES", string.Empty);
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRA", string.Empty);
                    break;
            }
            #endregion
            parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PJOtroTipoSociedad", dtInfo.Rows[0]["PJOtroTipoSociedad"].ToString());

            #region Rep. Legal
            parameters[21] = new Microsoft.Reporting.WebForms.ReportParameter("PJPrimerApellido", dtInfo.Rows[0]["PJPrimerApellido"].ToString());
            parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PJSegundoApellido", dtInfo.Rows[0]["PJSegundoApellido"].ToString());

            #region Nombres Rep. Legal
            string[] strPartes = dtInfo.Rows[0]["PJNombres"].ToString().Split(' ');
            string strNombre1 = string.Empty, strNombre2 = string.Empty;
            strNombre1 = strPartes[0];
            strNombre2 = strPartes.Length == 1 ? string.Empty : strPartes[1];

            parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombre1", strNombre1);
            parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombre2", strNombre2);
            #endregion
            #endregion

            #region Documento
            #region Tipo Doc
            switch (dtInfo.Rows[0]["PJTipoDocumento"].ToString())
            {
                case "C.C.":
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PJCC", "X");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PJCE", string.Empty);
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRO", string.Empty);
                    break;
                case "C.E.":
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PJCC", string.Empty);
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PJCE", "X");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRO", string.Empty);
                    break;
                case "OTRO":
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PJCC", string.Empty);
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PJCE", string.Empty);
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRO", "X");
                    break;
                default:
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PJCC", string.Empty);
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PJCE", string.Empty);
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTRO", string.Empty);
                    break;
            }
            #endregion

            parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PJOTROTIPODOC", dtInfo.Rows[0]["PJOtroTipoDoc"].ToString());
            parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumento", dtInfo.Rows[0]["PJNumeroDocumento"].ToString());
            parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PJLugarExpedicion", dtInfo.Rows[0]["PJLugarExpedicion"].ToString());
            #endregion

            #region Nacionalidad
            parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PJNacionalidad1", dtInfo.Rows[0]["PJNacionalidad1"].ToString());
            parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PJNacionalidad2", dtInfo.Rows[0]["PJNacionalidad2"].ToString());
            #endregion

            #region Of. Ppal
            parameters[32] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionOficina", dtInfo.Rows[0]["PJDireccionOficina"].ToString());
            parameters[33] = new Microsoft.Reporting.WebForms.ReportParameter("PJCiudad1", dtInfo.Rows[0]["PJCiudad1"].ToString());
            parameters[34] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefono1", dtInfo.Rows[0]["PJTelefono1"].ToString());
            parameters[35] = new Microsoft.Reporting.WebForms.ReportParameter("PJFax1", dtInfo.Rows[0]["PJFax1"].ToString());
            parameters[36] = new Microsoft.Reporting.WebForms.ReportParameter("PJPagWeb", dtInfo.Rows[0]["PJPagWeb"].ToString());
            parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PJCorreoPrincipal", dtInfo.Rows[0]["PJCorreoPrincipal"].ToString());
            #endregion

            #region Sucursal
            parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionSucursal", dtInfo.Rows[0]["PJDireccionSucursal"].ToString());
            parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PJCiudad2", dtInfo.Rows[0]["PJCiudad2"].ToString());
            parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefono2", dtInfo.Rows[0]["PJTelefono2"].ToString());
            parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PJFax2", dtInfo.Rows[0]["PJFax2"].ToString());
            #endregion

            #region Empresa
            #region Tipo Empresa
            switch (dtInfo.Rows[0]["PJTipoEmpresa"].ToString())
            {
                case "PÚBLICA":
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PJPUB", "X");
                    parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PJPRI", string.Empty);
                    parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PJMIX", string.Empty);
                    break;
                case "PRIVADA":
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PJPUB", string.Empty);
                    parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PJPRI", "X");
                    parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PJMIX", string.Empty);
                    break;
                case "MIXTA":
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PJPUB", string.Empty);
                    parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PJPRI", string.Empty);
                    parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PJMIX", "X");
                    break;
                default:
                    parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PJPUB", string.Empty);
                    parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PJPRI", string.Empty);
                    parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PJMIX", string.Empty);
                    break;
            }
            #endregion

            parameters[45] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIIU", dtInfo.Rows[0]["PJCIIU"].ToString());

            #region Actividad Economica
            switch (dtInfo.Rows[0]["PJActividadEconomica"].ToString())
            {
                case "INDUSTRIAL":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", "X");
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "COMERCIAL":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", "X");
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "TRANSPORTE":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", "X");
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "CONSTRUCCIÓN":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", "X");
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "AGROINDUSTRIA":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", "X");
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "CIVIL":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", "X");
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "FINANCIERO":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", "X");
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "SERVICIOS":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", "X");
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "SALUD":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", "X");
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
                case "OTRA":
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", "X");
                    break;
                default:
                    parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJINDUST", string.Empty);
                    parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJCOMERC", string.Empty);
                    parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJTRANSP", string.Empty);
                    parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCONSTR", string.Empty);
                    parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJAGROIN", string.Empty);
                    parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIVIL", string.Empty);
                    parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFINANC", string.Empty);
                    parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJSERVIC", string.Empty);
                    parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJSALUD", string.Empty);
                    parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJAEOTRA", string.Empty);
                    break;
            }
            #endregion
            parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PJOtraActividadEconomica", dtInfo.Rows[0]["PJOtraActividadEconomica"].ToString());
            parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PJDescObjSocial", dtInfo.Rows[0]["PJDescObjSocial"].ToString());
            #endregion

            #region Accionistas
            parameters[58] = new Microsoft.Reporting.WebForms.ReportParameter("PJConsecAS1", dtInfo.Rows[0]["PJConsecAS1"].ToString());
            parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS1", dtInfo.Rows[0]["PJTipoIdentificacionAS1"].ToString());
            parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS1", dtInfo.Rows[0]["PJNumeroDocumentoAS1"].ToString());
            parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS1", dtInfo.Rows[0]["PJNombreAS1"].ToString());

            parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PJConsecAS2", dtInfo.Rows[0]["PJConsecAS2"].ToString());
            parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS2", dtInfo.Rows[0]["PJTipoIdentificacionAS2"].ToString());
            parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS2", dtInfo.Rows[0]["PJNumeroDocumentoAS2"].ToString());
            parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS2", dtInfo.Rows[0]["PJNombreAS2"].ToString());

            parameters[66] = new Microsoft.Reporting.WebForms.ReportParameter("PJConsecAS3", dtInfo.Rows[0]["PJConsecAS3"].ToString());
            parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS3", dtInfo.Rows[0]["PJTipoIdentificacionAS3"].ToString());
            parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS3", dtInfo.Rows[0]["PJNumeroDocumentoAS3"].ToString());
            parameters[69] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS3", dtInfo.Rows[0]["PJNombreAS3"].ToString());

            parameters[70] = new Microsoft.Reporting.WebForms.ReportParameter("PJConsecAS4", dtInfo.Rows[0]["PJConsecAS4"].ToString());
            parameters[71] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS4", dtInfo.Rows[0]["PJTipoIdentificacionAS4"].ToString());
            parameters[72] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS4", dtInfo.Rows[0]["PJNumeroDocumentoAS4"].ToString());
            parameters[73] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS4", dtInfo.Rows[0]["PJNombreAS4"].ToString());
            #endregion

            #region Ingresos
            parameters[74] = new Microsoft.Reporting.WebForms.ReportParameter("PJIngresosMensuales", dtInfo.Rows[0]["PJIngresosMensuales"].ToString());
            parameters[75] = new Microsoft.Reporting.WebForms.ReportParameter("PJActivos", dtInfo.Rows[0]["PJActivos"].ToString());
            parameters[76] = new Microsoft.Reporting.WebForms.ReportParameter("PJEgresoMensuales", dtInfo.Rows[0]["PJEgresoMensuales"].ToString());
            parameters[77] = new Microsoft.Reporting.WebForms.ReportParameter("PJPasivos", dtInfo.Rows[0]["PJPasivos"].ToString());
            parameters[78] = new Microsoft.Reporting.WebForms.ReportParameter("PJOtrosIngresos", dtInfo.Rows[0]["PJOtrosIngresos"].ToString());
            parameters[79] = new Microsoft.Reporting.WebForms.ReportParameter("PJConceptoOtrosIngresos", dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString());
            #endregion

            #endregion

            #region Financiera
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);

            #region SI/NO
            switch (dtInfo.Rows[0]["TransacMonedaExtra"].ToString())
            {
                case "SI":
                    parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranSI", "X");
                    parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranNO", string.Empty);
                    break;
                case "NO":
                    parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranSI", string.Empty);
                    parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranNO", "X");
                    break;
                default:
                    parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranSI", string.Empty);
                    parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PFTranNO", string.Empty);
                    break;
            }
            #endregion

            #region Tipo Transaccion
            switch (dtInfo.Rows[0]["TipoTransaccion"].ToString())
            {
                case "IMPORTACIONES":
                    parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", "X");
                    parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "EXPORTACIONES":
                    parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", "X");
                    parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "INVERSIONES":
                    parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", "X");
                    parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "TRANSFERENCIAS":
                    parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", "X");
                    parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
                case "OTRA":
                    parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", "X");
                    break;
                default:
                    parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFIMPOR", string.Empty);
                    parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFEXPOR", string.Empty);
                    parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFINVER", string.Empty);
                    parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFTRANSFER", string.Empty);
                    parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFOTRA", string.Empty);
                    break;
            }

            parameters[87] = new Microsoft.Reporting.WebForms.ReportParameter("OtroTipoTransaccion", dtInfo.Rows[0]["OtroTipoTransaccion"].ToString());
            #endregion

            #region Prod. Ext
            #region SI/NO
            switch (dtInfo.Rows[0]["PFProdExterior"].ToString())
            {
                case "SI":
                    parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorSI", "X");
                    parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorNO", string.Empty);
                    break;
                case "NO":
                    parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorSI", string.Empty);
                    parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorNO", "X");
                    break;
                default:
                    parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorSI", string.Empty);
                    parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExteriorNO", string.Empty);
                    break;
            }
            #endregion

            #region Prod 1
            parameters[90] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto1", dtInfo.Rows[0]["PFTipoProducto1"].ToString());
            parameters[91] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto1", dtInfo.Rows[0]["PFNumeroProducto1"].ToString());
            parameters[92] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad1", dtInfo.Rows[0]["PFEntidad1"].ToString());
            parameters[93] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto1", dtInfo.Rows[0]["PFMonto1"].ToString());
            parameters[94] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad1", dtInfo.Rows[0]["PFCiudad1"].ToString());
            parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais1", dtInfo.Rows[0]["PFPais1"].ToString());
            parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda1", dtInfo.Rows[0]["PFMoneda1"].ToString());
            #endregion

            #region Prod 2
            parameters[97] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto2", dtInfo.Rows[0]["PFTipoProducto2"].ToString());
            parameters[98] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto2", dtInfo.Rows[0]["PFNumeroProducto2"].ToString());
            parameters[99] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad2", dtInfo.Rows[0]["PFEntidad2"].ToString());
            parameters[100] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto2", dtInfo.Rows[0]["PFMonto2"].ToString());
            parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad2", dtInfo.Rows[0]["PFCiudad2"].ToString());
            parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais2", dtInfo.Rows[0]["PFPais2"].ToString());
            parameters[103] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda2", dtInfo.Rows[0]["PFMoneda2"].ToString());
            #endregion
            #endregion
            #endregion

            #region Seguros y Fondos
            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);

            #region Seguro 1
            parameters[104] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno1", dtInfo.Rows[0]["SeguroAno1"].ToString());
            parameters[105] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo1", dtInfo.Rows[0]["SeguroRamo1"].ToString());
            parameters[106] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania1", dtInfo.Rows[0]["SeguroCompania1"].ToString());
            parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor1", dtInfo.Rows[0]["SeguroValor1"].ToString());

            #region Tipo Seguro
            switch (dtInfo.Rows[0]["SeguroTipo1"].ToString())
            {
                case "RECLAM.":
                    parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA1", "X");
                    parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM1", string.Empty);
                    break;
                case "INDEMNIZ.":
                    parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA1", string.Empty);
                    parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM1", "X");
                    break;
                default:
                    parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA1", string.Empty);
                    parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM1", string.Empty);
                    break;
            }
            #endregion
            #endregion

            #region Seguro 2
            parameters[110] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno2", dtInfo.Rows[0]["SeguroAno2"].ToString());
            parameters[111] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo2", dtInfo.Rows[0]["SeguroRamo2"].ToString());
            parameters[112] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania2", dtInfo.Rows[0]["SeguroCompania2"].ToString());
            parameters[113] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor2", dtInfo.Rows[0]["SeguroValor2"].ToString());

            #region Tipo Seguro
            switch (dtInfo.Rows[0]["SeguroTipo2"].ToString())
            {
                case "RECLAM.":
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA2", "X");
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM2", string.Empty);
                    break;
                case "INDEMNIZ.":
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA2", string.Empty);
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM2", "X");
                    break;
                default:
                    parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("RECLA2", string.Empty);
                    parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("INDEM2", string.Empty);
                    break;
            }
            #endregion
            #endregion
            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);

            #region Info Entrevista
            parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("LugarEntrevista", dtInfo.Rows[0]["LugarEntrevista"].ToString());
            parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("FechaEntrevista", dtInfo.Rows[0]["FechaEntrevista"].ToString());
            parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("HoraEntrevista", dtInfo.Rows[0]["HoraEntrevista"].ToString());
            parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("NombreResponsable", dtInfo.Rows[0]["NombreResponsable"].ToString());

            #region Tipo Resultado
            switch (dtInfo.Rows[0]["Resultado"].ToString())
            {
                case "Aceptado.":
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("RESAceptado", "X");
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("ResRechazado", string.Empty);
                    break;
                case "Rechazado.":
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("RESAceptado", string.Empty);
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("ResRechazado", "X");
                    break;
                default:
                    parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("RESAceptado", string.Empty);
                    parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("ResRechazado", string.Empty);
                    break;
            }
            #endregion

            parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("CCRespEntrevista", dtInfo.Rows[0]["CCRespEntrevista"].ToString());
            parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones1", dtInfo.Rows[0]["Observaciones1"].ToString());
            #endregion

            #region Verificacion Entrevista
            parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("NombreVerifica", dtInfo.Rows[0]["NombreVerifica"].ToString());
            parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("CCVerificaEntrevista", dtInfo.Rows[0]["CCVerificaEntrevista"].ToString());
            parameters[126] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones2", dtInfo.Rows[0]["Observaciones2"].ToString());
            #endregion
            #endregion


            #region Campos Disponibles
           
            parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("y", "");
            parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("z", "");
            #endregion

            ReportViewer1.LocalReport.SetParameters(parameters);
        }

        /// <summary>
        /// Hidden the special SSRS rendering format in ReportViewer control
        /// </summary>
        /// <param name="ReportViewerID">The ID of the relevant ReportViewer control</param>
        /// <param name="strFormatName">Format Name</param>
        public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        {
            FieldInfo info;
            foreach (RenderingExtension extension in ReportViewerID.ServerReport.ListRenderingExtensions())
            {
                if (extension.Name == strFormatName)
                {
                    info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }

            }
        }

        protected void ReportViewer1_PreRender(object sender, EventArgs e)
        {
            DisableUnwantedExportFormat((ReportViewer)sender, "Excel");
            DisableUnwantedExportFormat((ReportViewer)sender, "Word");
        }
    }
}
