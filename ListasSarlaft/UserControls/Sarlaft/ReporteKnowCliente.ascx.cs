using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls
{
    public partial class ReporteKnowCliente : System.Web.UI.UserControl
    {
        private cKnowClient cKnowClient = new cKnowClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadValue();
            }
        }

        private void loadValue()
        {
            try
            {
                int IdConocimientoCliente;
                IdConocimientoCliente = Convert.ToInt32(Request.QueryString["IdConocimientoCliente"]);
                reporte(IdConocimientoCliente);
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar la información. " + ex.Message);                
            }
        }

        private void reporte(int IdConocimientoCliente)
        {
            DataTable dtInfo = new DataTable();
            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[129];
            
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente);
            parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("FechaFormulario",dtInfo.Rows[0]["FechaFormulario"].ToString());
            parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("ClaseVinculacion", dtInfo.Rows[0]["ClaseVinculacion"].ToString());
            parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("OtraClaseVinculacion", dtInfo.Rows[0]["OtraClaseVinculacion"].ToString());
            parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("TomadorAsegurado", dtInfo.Rows[0]["TomadorAsegurado"].ToString());
            parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("OtraTomadorAsegurado", dtInfo.Rows[0]["OtraTomadorAsegurado"].ToString());
            parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("TomadorBeneficiario", dtInfo.Rows[0]["TomadorBeneficiario"].ToString());
            parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("OtraTomadorBeneficiario", dtInfo.Rows[0]["OtraTomadorBeneficiario"].ToString());
            parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("AseguradoBeneficiario", dtInfo.Rows[0]["AseguradoBeneficiario"].ToString());
            parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("OtraAseguradoBeneficiario", dtInfo.Rows[0]["OtraAseguradoBeneficiario"].ToString());

            dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);
            parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("PNPrimerApellido", dtInfo.Rows[0]["PNPrimerApellido"].ToString());
            parameters[10] = new Microsoft.Reporting.WebForms.ReportParameter("PNSegunApellido", dtInfo.Rows[0]["PNSegunApellido"].ToString());
            parameters[11] = new Microsoft.Reporting.WebForms.ReportParameter("PNNombres", dtInfo.Rows[0]["PNNombres"].ToString());
            parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PNTipoDocumento", dtInfo.Rows[0]["PNTipoDocumento"].ToString());
            parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PNNumeroDocumento", dtInfo.Rows[0]["PNNumeroDocumento"].ToString());
            parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaExpedicion", dtInfo.Rows[0]["PNFechaExpedicion"].ToString());
            parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNLugar", dtInfo.Rows[0]["PNLugar"].ToString());
            parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaNacimiento", dtInfo.Rows[0]["PNFechaNacimiento"].ToString());
            parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNNacionalidad", dtInfo.Rows[0]["PNNacionalidad"].ToString());
            parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNOcupacionOficio", dtInfo.Rows[0]["PNOcupacionOficio"].ToString());
            parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PNProfesion", dtInfo.Rows[0]["PNProfesion"].ToString());
            parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstadoOcupacion", dtInfo.Rows[0]["PNEstadoOcupacion"].ToString());
            parameters[21] = new Microsoft.Reporting.WebForms.ReportParameter("PNActividadEconomica", dtInfo.Rows[0]["PNActividadEconomica"].ToString());
            parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PNCIIU", dtInfo.Rows[0]["PNCIIU"].ToString());
            parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNEmpresaTrabajo", dtInfo.Rows[0]["PNEmpresaTrabajo"].ToString());
            parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNArea", dtInfo.Rows[0]["PNArea"].ToString());
            parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNCargo", dtInfo.Rows[0]["PNCargo"].ToString());
            parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudad1", dtInfo.Rows[0]["PNCiudad1"].ToString());
            parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDireccion", dtInfo.Rows[0]["PNDireccion"].ToString());
            parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelefono1", dtInfo.Rows[0]["PNTelefono1"].ToString());
            parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNFax", dtInfo.Rows[0]["PNFax"].ToString());
            parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNDireccionResidencia", dtInfo.Rows[0]["PNDireccionResidencia"].ToString());
            parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudad2", dtInfo.Rows[0]["PNCiudad2"].ToString());
            parameters[32] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelefono2", dtInfo.Rows[0]["PNTelefono2"].ToString());
            parameters[33] = new Microsoft.Reporting.WebForms.ReportParameter("PNCelular", dtInfo.Rows[0]["PNCelular"].ToString());
            parameters[34] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta1", dtInfo.Rows[0]["PNPregunta1"].ToString());
            parameters[35] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta2", dtInfo.Rows[0]["PNPregunta2"].ToString());
            parameters[36] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta3", dtInfo.Rows[0]["PNPregunta3"].ToString());
            parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNEspecificacionPreguntas", dtInfo.Rows[0]["PNEspecificacionPreguntas"].ToString());
            parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNIngresosMensuales", dtInfo.Rows[0]["PNIngresosMensuales"].ToString());
            parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNActivos", dtInfo.Rows[0]["PNActivos"].ToString());
            parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNEgresoMensuales", dtInfo.Rows[0]["PNEgresoMensuales"].ToString());
            parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPasivos", dtInfo.Rows[0]["PNPasivos"].ToString());
            parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtrosIngresos", dtInfo.Rows[0]["PNOtrosIngresos"].ToString());
            parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PNConceptoOtrosIngresos", dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString());
            parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexo", dtInfo.Rows[0]["PNSexo"].ToString());
            parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("PNCorreoElectronico", dtInfo.Rows[0]["PNCorreoElectronico"].ToString());

            dtInfo = cKnowClient.InfoFormPJ(IdConocimientoCliente);
            parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PJRazonDenominacion", dtInfo.Rows[0]["PJRazonDenominacion"].ToString());
            parameters[45] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT", dtInfo.Rows[0]["PJNIT"].ToString());
            parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJPrimerApellido", dtInfo.Rows[0]["PJPrimerApellido"].ToString());
            parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJSegundoApellido", dtInfo.Rows[0]["PJSegundoApellido"].ToString());
            parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombres", dtInfo.Rows[0]["PJNombres"].ToString());
            parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocumento", dtInfo.Rows[0]["PJTipoDocumento"].ToString());
            parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumento", dtInfo.Rows[0]["PJNumeroDocumento"].ToString());
            parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJLugarExpedicion", dtInfo.Rows[0]["PJLugarExpedicion"].ToString());
            parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJFechaExpedicion", "");
            parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionOficina", dtInfo.Rows[0]["PJDireccionOficina"].ToString());
            parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJCiudad1", dtInfo.Rows[0]["PJCiudad1"].ToString());
            parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefono1", dtInfo.Rows[0]["PJTelefono1"].ToString());
            parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PJFax1", dtInfo.Rows[0]["PJFax1"].ToString());
            parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionSucursal", dtInfo.Rows[0]["PJDireccionSucursal"].ToString());
            parameters[58] = new Microsoft.Reporting.WebForms.ReportParameter("PJCiudad2", dtInfo.Rows[0]["PJCiudad2"].ToString());
            parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefono2", dtInfo.Rows[0]["PJTelefono2"].ToString());
            parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PJFax2", dtInfo.Rows[0]["PJFax2"].ToString());
            parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoEmpresa", dtInfo.Rows[0]["PJTipoEmpresa"].ToString());
            parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PJActividadEconomica", dtInfo.Rows[0]["PJActividadEconomica"].ToString());
            parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PJOtraActividadEconomica", dtInfo.Rows[0]["PJOtraActividadEconomica"].ToString());
            parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIIU", dtInfo.Rows[0]["PJCIIU"].ToString());
            parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS1", dtInfo.Rows[0]["PJNombreAS1"].ToString());
            parameters[66] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS1", dtInfo.Rows[0]["PJTipoIdentificacionAS1"].ToString());
            parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS1", dtInfo.Rows[0]["PJNumeroDocumentoAS1"].ToString());
            parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS2", dtInfo.Rows[0]["PJNombreAS2"].ToString());
            parameters[69] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS2", dtInfo.Rows[0]["PJTipoIdentificacionAS2"].ToString());
            parameters[70] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS2", dtInfo.Rows[0]["PJNumeroDocumentoAS2"].ToString());
            parameters[71] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS3", dtInfo.Rows[0]["PJNombreAS3"].ToString());
            parameters[72] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS3", dtInfo.Rows[0]["PJTipoIdentificacionAS3"].ToString());
            parameters[73] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS3", dtInfo.Rows[0]["PJNumeroDocumentoAS3"].ToString());
            parameters[74] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS4", dtInfo.Rows[0]["PJNombreAS4"].ToString());
            parameters[75] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS4", dtInfo.Rows[0]["PJTipoIdentificacionAS4"].ToString());
            parameters[76] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS4", dtInfo.Rows[0]["PJNumeroDocumentoAS4"].ToString());
            parameters[77] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS5", dtInfo.Rows[0]["PJNombreAS5"].ToString());
            parameters[78] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS5", dtInfo.Rows[0]["PJTipoIdentificacionAS5"].ToString());
            parameters[79] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS5", dtInfo.Rows[0]["PJNumeroDocumentoAS5"].ToString());
            parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PJIngresosMensuales", dtInfo.Rows[0]["PJIngresosMensuales"].ToString());
            parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PJActivos", dtInfo.Rows[0]["PJActivos"].ToString());
            parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PJEgresoMensuales", dtInfo.Rows[0]["PJEgresoMensuales"].ToString());
            parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PJPasivos", dtInfo.Rows[0]["PJPasivos"].ToString());
            parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PJOtrosIngresos", dtInfo.Rows[0]["PJOtrosIngresos"].ToString());
            parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PJConceptoOtrosIngresos", dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString());
            parameters[126] = new Microsoft.Reporting.WebForms.ReportParameter("PJSexoRepLegal", dtInfo.Rows[0]["PJSexoRepLegal"].ToString());
            parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("PJCorreoPrincipal", dtInfo.Rows[0]["PJCorreoPrincipal"].ToString());
            parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("PJCorreoSucursal", dtInfo.Rows[0]["PJCorreoSucursal"].ToString());

            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);
            parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("TransacMonedaExtra", dtInfo.Rows[0]["TransacMonedaExtra"].ToString());
            parameters[87] = new Microsoft.Reporting.WebForms.ReportParameter("TipoTransaccion", dtInfo.Rows[0]["TipoTransaccion"].ToString());
            parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("OtroTipoTransaccion", dtInfo.Rows[0]["OtroTipoTransaccion"].ToString());
            parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto1", dtInfo.Rows[0]["PFTipoProducto1"].ToString());
            parameters[90] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto1", dtInfo.Rows[0]["PFNumeroProducto1"].ToString());
            parameters[91] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad1", dtInfo.Rows[0]["PFEntidad1"].ToString());
            parameters[92] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto1", dtInfo.Rows[0]["PFMonto1"].ToString());
            parameters[93] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad1", dtInfo.Rows[0]["PFCiudad1"].ToString());
            parameters[94] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais1", dtInfo.Rows[0]["PFPais1"].ToString());
            parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda1", dtInfo.Rows[0]["PFMoneda1"].ToString());
            parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto2", dtInfo.Rows[0]["PFTipoProducto2"].ToString());
            parameters[97] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto2", dtInfo.Rows[0]["PFNumeroProducto2"].ToString());
            parameters[98] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad2", dtInfo.Rows[0]["PFEntidad2"].ToString());
            parameters[99] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto2", dtInfo.Rows[0]["PFMonto2"].ToString());
            parameters[100] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad2", dtInfo.Rows[0]["PFCiudad2"].ToString());
            parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais2", dtInfo.Rows[0]["PFPais2"].ToString());
            parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda2", dtInfo.Rows[0]["PFMoneda2"].ToString());

            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);
            parameters[103] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno1", dtInfo.Rows[0]["SeguroAno1"].ToString());
            parameters[104] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo1", dtInfo.Rows[0]["SeguroRamo1"].ToString());
            parameters[105] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania1", dtInfo.Rows[0]["SeguroCompania1"].ToString());
            parameters[106] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor1", dtInfo.Rows[0]["SeguroValor1"].ToString());
            parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroTipo1", dtInfo.Rows[0]["SeguroTipo1"].ToString());
            parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno2", dtInfo.Rows[0]["SeguroAno2"].ToString());
            parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo2", dtInfo.Rows[0]["SeguroRamo2"].ToString());
            parameters[110] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania2", dtInfo.Rows[0]["SeguroCompania2"].ToString());
            parameters[111] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor2", dtInfo.Rows[0]["SeguroValor2"].ToString());
            parameters[112] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroTipo2", dtInfo.Rows[0]["SeguroTipo2"].ToString());
            parameters[113] = new Microsoft.Reporting.WebForms.ReportParameter("OrigenFondos", dtInfo.Rows[0]["OrigenFondos"].ToString());

            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);
            parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("LugarEntrevista", dtInfo.Rows[0]["LugarEntrevista"].ToString());
            parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("FechaEntrevista", dtInfo.Rows[0]["FechaEntrevista"].ToString());
            parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("HoraEntrevista", dtInfo.Rows[0]["HoraEntrevista"].ToString());
            parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("Resultado", dtInfo.Rows[0]["Resultado"].ToString());
            parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones1", dtInfo.Rows[0]["Observaciones1"].ToString());
            parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("NombreResponsable", dtInfo.Rows[0]["NombreResponsable"].ToString());
            parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("FechaVerificacion", dtInfo.Rows[0]["FechaVerificacion"].ToString());
            parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("HoraVerificacion", dtInfo.Rows[0]["HoraVerificacion"].ToString());
            parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("NombreVerifica", dtInfo.Rows[0]["NombreVerifica"].ToString());
            parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones2", dtInfo.Rows[0]["Observaciones2"].ToString());

            ReportViewer1.LocalReport.SetParameters(parameters);            
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
    }
}
