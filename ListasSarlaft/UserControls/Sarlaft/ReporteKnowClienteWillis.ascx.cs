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
    public partial class ReporteKnowClienteWillis : System.Web.UI.UserControl
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
                mtdReporteWillis(IdConocimientoCliente, strIdForm);
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

        private void mtdReporteWillis(int IdConocimientoCliente, string strIdForm)
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
            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[125];
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/ReporteClienteWillisPN.rdlc");

            #region Cabecera
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente, false);
            parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("FechaFormulario", dtInfo.Rows[0]["FechaFormulario"].ToString().Trim());
            parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("ClaseVinculacion", dtInfo.Rows[0]["ClaseVinculacion"].ToString().Trim());
            parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("OtraClaseVinculacion", dtInfo.Rows[0]["OtraClaseVinculacion"].ToString().Trim());
            parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("TomadorAsegurado", dtInfo.Rows[0]["TomadorAsegurado"].ToString().Trim());
            parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("OtraTomadorAsegurado", dtInfo.Rows[0]["OtraTomadorAsegurado"].ToString().Trim());
            parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("TomadorBeneficiario", dtInfo.Rows[0]["TomadorBeneficiario"].ToString().Trim());
            parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("OtraTomadorBeneficiario", dtInfo.Rows[0]["OtraTomadorBeneficiario"].ToString().Trim());
            parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("AseguradoBeneficiario", dtInfo.Rows[0]["AseguradoBeneficiario"].ToString().Trim());
            parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("OtraAseguradoBeneficiario", dtInfo.Rows[0]["OtraAseguradoBeneficiario"].ToString().Trim());
            parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("TipoCliente", dtInfo.Rows[0]["TipoCliente"].ToString().Trim());
            parameters[10] = new Microsoft.Reporting.WebForms.ReportParameter("Ciudad", dtInfo.Rows[0]["Ciudad"].ToString().Trim());
            parameters[11] = new Microsoft.Reporting.WebForms.ReportParameter("Sucursal", dtInfo.Rows[0]["Sucursal"].ToString().Trim());
            parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("TipoSolicitud", dtInfo.Rows[0]["TipoSolicitud"].ToString().Trim());
            #endregion

            #region Persona Natural
            dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);
            parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PNPrimerApellido", dtInfo.Rows[0]["PNPrimerApellido"].ToString().Trim());
            parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNSegunApellido", dtInfo.Rows[0]["PNSegunApellido"].ToString().Trim());
            parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNNombres", dtInfo.Rows[0]["PNNombres"].ToString().Trim());
            parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNTipoDocumento", dtInfo.Rows[0]["PNTipoDocumento"].ToString().Trim());
            parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNNumeroDocumento", dtInfo.Rows[0]["PNNumeroDocumento"].ToString().Trim());
            parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaExpedicion", dtInfo.Rows[0]["PNFechaExpedicion"].ToString().Trim());
            parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PNLugar", dtInfo.Rows[0]["PNLugar"].ToString().Trim());
            parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaNacimiento", dtInfo.Rows[0]["PNFechaNacimiento"].ToString().Trim());
            parameters[21] = new Microsoft.Reporting.WebForms.ReportParameter("PNNacionalidad", dtInfo.Rows[0]["PNNacionalidad"].ToString().Trim());
            parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PNOcupacionOficio", dtInfo.Rows[0]["PNOcupacionOficio"].ToString().Trim());
            parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNActividadEconomica", dtInfo.Rows[0]["PNActividadEconomica"].ToString().Trim());
            parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCIIU", dtInfo.Rows[0]["PNCIIU"].ToString().Trim());
            parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNEmpresaTrabajo", dtInfo.Rows[0]["PNEmpresaTrabajo"].ToString().Trim());
            parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNCargo", dtInfo.Rows[0]["PNCargo"].ToString().Trim());
            parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudad1", dtInfo.Rows[0]["PNCiudad1"].ToString().Trim());
            parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNDireccion", dtInfo.Rows[0]["PNDireccion"].ToString().Trim());
            parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelefono1", dtInfo.Rows[0]["PNTelefono1"].ToString().Trim());
            parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNDireccionResidencia", dtInfo.Rows[0]["PNDireccionResidencia"].ToString().Trim());
            parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudad2", dtInfo.Rows[0]["PNCiudad2"].ToString().Trim());
            parameters[32] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelefono2", dtInfo.Rows[0]["PNTelefono2"].ToString().Trim());
            parameters[33] = new Microsoft.Reporting.WebForms.ReportParameter("PNCelular", dtInfo.Rows[0]["PNCelular"].ToString().Trim());
            parameters[34] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta1", dtInfo.Rows[0]["PNPregunta1"].ToString().Trim());
            parameters[35] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta2", dtInfo.Rows[0]["PNPregunta2"].ToString().Trim());
            parameters[36] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta3", dtInfo.Rows[0]["PNPregunta3"].ToString().Trim());
            parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNEspecificacionPreguntas", dtInfo.Rows[0]["PNEspecificacionPreguntas"].ToString().Trim());
            parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNIngresosMensuales", dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim());
            parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNActivos", dtInfo.Rows[0]["PNActivos"].ToString().Trim());
            parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNEgresoMensuales", dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim());
            parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPasivos", dtInfo.Rows[0]["PNPasivos"].ToString().Trim());
            parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtrosIngresos", dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim());
            parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PNConceptoOtrosIngresos", dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString().Trim());
            parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PNCorreoElectronico", dtInfo.Rows[0]["PNCorreoElectronico"].ToString().Trim());
            parameters[45] = new Microsoft.Reporting.WebForms.ReportParameter("PNLugarNmto", dtInfo.Rows[0]["PNLugarNmto"].ToString().Trim());
            parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PNDpto", dtInfo.Rows[0]["PNDpto"].ToString().Trim());
            parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtraActEconomica", dtInfo.Rows[0]["PNOtraActEconomica"].ToString().Trim());
            parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PNPatrimonio", dtInfo.Rows[0]["PNPatrimonio"].ToString().Trim());
            parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PNDireccionEmpresa", dtInfo.Rows[0]["PNDireccionEmpresa"].ToString().Trim());
            parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudadEmpresa", dtInfo.Rows[0]["PNCiudadEmpresa"].ToString().Trim());
            parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelefonoEmpresa", dtInfo.Rows[0]["PNTelefonoEmpresa"].ToString().Trim());
            parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtraCIIU", dtInfo.Rows[0]["PNOtraCIIU"].ToString().Trim());
            parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta4", dtInfo.Rows[0]["PNPregunta4"].ToString().Trim());
            parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta5", dtInfo.Rows[0]["PNPregunta5"].ToString().Trim());
            parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PNEspecificacionPreguntas2", dtInfo.Rows[0]["PNEspecificacionPreguntas2"].ToString().Trim());
            parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PNSector1", dtInfo.Rows[0]["PNSector1"].ToString().Trim());
            parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PNSector2", dtInfo.Rows[0]["PNSector2"].ToString().Trim());
            parameters[58] = new Microsoft.Reporting.WebForms.ReportParameter("PNDptoEmpresa", dtInfo.Rows[0]["PNDptoEmpresa"].ToString().Trim());
            parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PNDpto2", dtInfo.Rows[0]["PNDpto2"].ToString().Trim());
            parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PNServicio", dtInfo.Rows[0]["PNServicio"].ToString().Trim());
            //parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PNCIIUDescripcion", dtInfo.Rows[0]["PNCIIUDescripcion"].ToString().Trim());
            parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PNCIIUDescripcion", cKnowClient.DescripcionCIIU_PN(dtInfo.Rows[0]["PNCIIU"].ToString().Trim()));
            parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("PNTipoActividad", dtInfo.Rows[0]["PNTipoActividad"].ToString().Trim());
            parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("PNTipoActividadOtra", dtInfo.Rows[0]["PNTipoActividadOtra"].ToString().Trim());
            parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("PNpePrimerApellido", dtInfo.Rows[0]["PNpePrimerApellido"].ToString().Trim());
            parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("PNpeSegundoApellido", dtInfo.Rows[0]["PNpeSegundoApellido"].ToString().Trim());
            parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("PNpeNombres", dtInfo.Rows[0]["PNpeNombres"].ToString().Trim());
            parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("PNpeOcupacion", dtInfo.Rows[0]["PNpeOcupacion"].ToString().Trim());
            parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("PNpeCargo", dtInfo.Rows[0]["PNpeCargo"].ToString().Trim());
            #endregion

            #region Financiera
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);

            if (dtInfo.Rows.Count > 0)
            {
                parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("TransacMonedaExtra", dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim());
                parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("TipoTransaccion", dtInfo.Rows[0]["TipoTransaccion"].ToString().Trim());
                parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("OtroTipoTransaccion", dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim());
                parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto1", dtInfo.Rows[0]["PFTipoProducto1"].ToString().Trim());
                parameters[66] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto1", dtInfo.Rows[0]["PFNumeroProducto1"].ToString().Trim());
                parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad1", dtInfo.Rows[0]["PFEntidad1"].ToString().Trim());
                parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto1", dtInfo.Rows[0]["PFMonto1"].ToString().Trim());
                parameters[69] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad1", dtInfo.Rows[0]["PFCiudad1"].ToString().Trim());
                parameters[70] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais1", dtInfo.Rows[0]["PFPais1"].ToString().Trim());
                parameters[71] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda1", dtInfo.Rows[0]["PFMoneda1"].ToString().Trim());
                parameters[72] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto2", dtInfo.Rows[0]["PFTipoProducto2"].ToString().Trim());
                parameters[73] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto2", dtInfo.Rows[0]["PFNumeroProducto2"].ToString().Trim());
                parameters[74] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad2", dtInfo.Rows[0]["PFEntidad2"].ToString().Trim());
                parameters[75] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto2", dtInfo.Rows[0]["PFMonto2"].ToString().Trim());
                parameters[76] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad2", dtInfo.Rows[0]["PFCiudad2"].ToString().Trim());
                parameters[77] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais2", dtInfo.Rows[0]["PFPais2"].ToString().Trim());
                parameters[78] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda2", dtInfo.Rows[0]["PFMoneda2"].ToString().Trim());
                parameters[79] = new Microsoft.Reporting.WebForms.ReportParameter("PFCtaMonedaExtra", dtInfo.Rows[0]["PFCtaMonedaExtra"].ToString().Trim());
                parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto3", dtInfo.Rows[0]["PFTipoProducto3"].ToString().Trim());
                parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto3", dtInfo.Rows[0]["PFNumeroProducto3"].ToString().Trim());
                parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad3", dtInfo.Rows[0]["PFEntidad3"].ToString().Trim());
                parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto3", dtInfo.Rows[0]["PFMonto3"].ToString().Trim());
                parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad3", dtInfo.Rows[0]["PFCiudad3"].ToString().Trim());
                parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais3", dtInfo.Rows[0]["PFPais3"].ToString().Trim());
                parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda3", dtInfo.Rows[0]["PFMoneda3"].ToString().Trim());
                parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExterior", dtInfo.Rows[0]["PFProdExterior"].ToString().Trim());
            }
            else
            {
                parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("TransacMonedaExtra", "");
                parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("TipoTransaccion", "");
                parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("OtroTipoTransaccion", "");
                parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto1", "");
                parameters[66] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto1", "");
                parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad1", "");
                parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto1", "");
                parameters[69] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad1", "");
                parameters[70] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais1", "");
                parameters[71] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda1", "");
                parameters[72] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto2", "");
                parameters[73] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto2", "");
                parameters[74] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad2", "");
                parameters[75] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto2", "");
                parameters[76] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad2", "");
                parameters[77] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais2", "");
                parameters[78] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda2", "");
                parameters[79] = new Microsoft.Reporting.WebForms.ReportParameter("PFCtaMonedaExtra", "");
                parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto3", "");
                parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto3", "");
                parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad3", "");
                parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto3", "");
                parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad3", "");
                parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais3", "");
                parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda3", "");
                parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExterior", "");
            }
            #endregion

            #region Seguros
            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);
            parameters[87] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno1", dtInfo.Rows[0]["SeguroAno1"].ToString().Trim());
            parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo1", dtInfo.Rows[0]["SeguroRamo1"].ToString().Trim());
            parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania1", dtInfo.Rows[0]["SeguroCompania1"].ToString().Trim());
            parameters[90] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor1", dtInfo.Rows[0]["SeguroValor1"].ToString().Trim());
            parameters[91] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroTipo1", dtInfo.Rows[0]["SeguroTipo1"].ToString().Trim());
            parameters[92] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno2", dtInfo.Rows[0]["SeguroAno2"].ToString().Trim());
            parameters[93] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo2", dtInfo.Rows[0]["SeguroRamo2"].ToString().Trim());
            parameters[94] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania2", dtInfo.Rows[0]["SeguroCompania2"].ToString().Trim());
            parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor2", dtInfo.Rows[0]["SeguroValor2"].ToString().Trim());
            parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroTipo2", dtInfo.Rows[0]["SeguroTipo2"].ToString().Trim());
            parameters[97] = new Microsoft.Reporting.WebForms.ReportParameter("OrigenFondos", dtInfo.Rows[0]["OrigenFondos"].ToString().Trim());
            parameters[98] = new Microsoft.Reporting.WebForms.ReportParameter("Reclamaciones", dtInfo.Rows[0]["Reclamaciones"].ToString().Trim());


            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);
            parameters[99] = new Microsoft.Reporting.WebForms.ReportParameter("LugarEntrevista", dtInfo.Rows[0]["LugarEntrevista"].ToString().Trim());
            parameters[100] = new Microsoft.Reporting.WebForms.ReportParameter("FechaEntrevista", dtInfo.Rows[0]["FechaEntrevista"].ToString().Trim());
            parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("HoraEntrevista", dtInfo.Rows[0]["HoraEntrevista"].ToString().Trim());
            parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("Resultado", dtInfo.Rows[0]["Resultado"].ToString().Trim());
            parameters[103] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones1", dtInfo.Rows[0]["Observaciones1"].ToString().Trim());
            parameters[104] = new Microsoft.Reporting.WebForms.ReportParameter("NombreResponsable", dtInfo.Rows[0]["NombreResponsable"].ToString().Trim());
            parameters[105] = new Microsoft.Reporting.WebForms.ReportParameter("FechaVerificacion", dtInfo.Rows[0]["FechaVerificacion"].ToString().Trim());
            parameters[106] = new Microsoft.Reporting.WebForms.ReportParameter("HoraVerificacion", dtInfo.Rows[0]["HoraVerificacion"].ToString().Trim());
            parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("NombreVerifica", dtInfo.Rows[0]["NombreVerifica"].ToString().Trim());
            parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones2", dtInfo.Rows[0]["Observaciones2"].ToString().Trim());
            parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaFirma", dtInfo.Rows[0]["ValidaFirma"].ToString().Trim());
            parameters[110] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaHuella", dtInfo.Rows[0]["ValidaHuella"].ToString().Trim());
            parameters[111] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaEntrevista", dtInfo.Rows[0]["ValidaEntrevista"].ToString().Trim());
            parameters[112] = new Microsoft.Reporting.WebForms.ReportParameter("CCRespEntrevista", dtInfo.Rows[0]["CCRespEntrevista"].ToString().Trim());
            parameters[113] = new Microsoft.Reporting.WebForms.ReportParameter("CCVerificaEntrevista", dtInfo.Rows[0]["CCVerificaEntrevista"].ToString().Trim());
            parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("NombreIntermediario", dtInfo.Rows[0]["NombreIntermediario"].ToString().Trim());

            parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("Usuario", dtInfo.Rows[0]["Usuario"].ToString().Trim());
            parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("FechaRegistro", dtInfo.Rows[0]["FechaRegistro"].ToString().Trim());
            #endregion

            ReportViewer1.LocalReport.SetParameters(parameters);
        }

        private void mtdReporteJuridico(int IdConocimientoCliente)
        {
            DataTable dtInfo = new DataTable();
            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[178];
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reportes/ReporteClienteWillisPJ.rdlc");

            #region Cabecera
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente, false);
            parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("FechaFormulario", dtInfo.Rows[0]["FechaFormulario"].ToString().Trim());
            parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("ClaseVinculacion", dtInfo.Rows[0]["ClaseVinculacion"].ToString().Trim());
            parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("OtraClaseVinculacion", dtInfo.Rows[0]["OtraClaseVinculacion"].ToString().Trim());
            parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("TomadorAsegurado", dtInfo.Rows[0]["TomadorAsegurado"].ToString().Trim());
            parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("OtraTomadorAsegurado", dtInfo.Rows[0]["OtraTomadorAsegurado"].ToString().Trim());
            parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("TomadorBeneficiario", dtInfo.Rows[0]["TomadorBeneficiario"].ToString().Trim());
            parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("OtraTomadorBeneficiario", dtInfo.Rows[0]["OtraTomadorBeneficiario"].ToString().Trim());
            parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("AseguradoBeneficiario", dtInfo.Rows[0]["AseguradoBeneficiario"].ToString().Trim());
            parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("OtraAseguradoBeneficiario", dtInfo.Rows[0]["OtraAseguradoBeneficiario"].ToString().Trim());
            parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("TipoCliente", dtInfo.Rows[0]["TipoCliente"].ToString().Trim());
            parameters[10] = new Microsoft.Reporting.WebForms.ReportParameter("Ciudad", dtInfo.Rows[0]["Ciudad"].ToString().Trim());
            parameters[11] = new Microsoft.Reporting.WebForms.ReportParameter("Sucursal", dtInfo.Rows[0]["Sucursal"].ToString().Trim());
            parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("TipoSolicitud", dtInfo.Rows[0]["TipoSolicitud"].ToString().Trim());
            #endregion

            #region Juridica
            dtInfo = cKnowClient.InfoFormPJ(IdConocimientoCliente);
            parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PJRazonDenominacion", dtInfo.Rows[0]["PJRazonDenominacion"].ToString().Trim());
            parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT", dtInfo.Rows[0]["PJNIT"].ToString().Trim());
            parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PJPrimerApellido", dtInfo.Rows[0]["PJPrimerApellido"].ToString().Trim());
            parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PJSegundoApellido", dtInfo.Rows[0]["PJSegundoApellido"].ToString().Trim());
            parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombres", dtInfo.Rows[0]["PJNombres"].ToString().Trim());
            parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocumento", dtInfo.Rows[0]["PJTipoDocumento"].ToString().Trim());
            parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumento", dtInfo.Rows[0]["PJNumeroDocumento"].ToString().Trim());
            parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PJLugarExpedicion", dtInfo.Rows[0]["PJLugarExpedicion"].ToString().Trim());
            parameters[21] = new Microsoft.Reporting.WebForms.ReportParameter("PJFechaExpedicion", dtInfo.Rows[0]["PJFechaExpedicion"].ToString().Trim());
            parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionOficina", dtInfo.Rows[0]["PJDireccionOficina"].ToString().Trim());
            parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PJCiudad1", dtInfo.Rows[0]["PJCiudad1"].ToString().Trim());
            parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefono1", dtInfo.Rows[0]["PJTelefono1"].ToString().Trim());
            parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionSucursal", dtInfo.Rows[0]["PJDireccionSucursal"].ToString().Trim());
            parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PJCiudad2", dtInfo.Rows[0]["PJCiudad2"].ToString().Trim());
            parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefono2", dtInfo.Rows[0]["PJTelefono2"].ToString().Trim());
            if(dtInfo.Rows[0]["PJTipoEmpresa"].ToString().Trim() == "OFICINA")
            {
                parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoEmpresa", "OFICINA DE REPRESENTACION");
            }
            else
            {
            parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoEmpresa", dtInfo.Rows[0]["PJTipoEmpresa"].ToString().Trim());
            }
            
            parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PJActividadEconomica", dtInfo.Rows[0]["PJActividadEconomica"].ToString().Trim());
            parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIIU", dtInfo.Rows[0]["PJCIIU"].ToString().Trim());
            parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PJIngresosMensuales", dtInfo.Rows[0]["PJIngresosMensuales"].ToString().Trim());
            parameters[32] = new Microsoft.Reporting.WebForms.ReportParameter("PJActivos", dtInfo.Rows[0]["PJActivos"].ToString().Trim());
            parameters[33] = new Microsoft.Reporting.WebForms.ReportParameter("PJEgresoMensuales", dtInfo.Rows[0]["PJEgresoMensuales"].ToString().Trim());
            parameters[34] = new Microsoft.Reporting.WebForms.ReportParameter("PJPasivos", dtInfo.Rows[0]["PJPasivos"].ToString().Trim());
            parameters[35] = new Microsoft.Reporting.WebForms.ReportParameter("PJOtrosIngresos", dtInfo.Rows[0]["PJOtrosIngresos"].ToString().Trim());
            parameters[36] = new Microsoft.Reporting.WebForms.ReportParameter("PJConceptoOtrosIngresos", dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString().Trim());
            parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PJCorreoPrincipal", dtInfo.Rows[0]["PJCorreoPrincipal"].ToString().Trim());
            parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PJFechaConstitucion", dtInfo.Rows[0]["PJFechaConstitucion"].ToString().Trim());
            parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombresRepLegalPpal", dtInfo.Rows[0]["PJNombresRepLegalPpal"].ToString().Trim());
            parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegalPpal", dtInfo.Rows[0]["PJTipoDocRepLegalPpal"].ToString().Trim());
            parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocumentoRepLegalPpal", dtInfo.Rows[0]["PJDocumentoRepLegalPpal"].ToString().Trim());
            parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombresRepLegal1", dtInfo.Rows[0]["PJNombresRepLegal1"].ToString().Trim());
            parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal1", dtInfo.Rows[0]["PJTipoDocRepLegal1"].ToString().Trim());
            parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocumentoRepLegal1", dtInfo.Rows[0]["PJDocumentoRepLegal1"].ToString().Trim());
            parameters[45] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombresRepLegal2", dtInfo.Rows[0]["PJNombresRepLegal2"].ToString().Trim());
            parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal2", dtInfo.Rows[0]["PJTipoDocRepLegal2"].ToString().Trim());
            parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocumentoRepLegal2", dtInfo.Rows[0]["PJDocumentoRepLegal2"].ToString().Trim());
            parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombresRepLegal3", dtInfo.Rows[0]["PJNombresRepLegal3"].ToString().Trim());
            parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal3", dtInfo.Rows[0]["PJTipoDocRepLegal3"].ToString().Trim());
            parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocumentoRepLegal3", dtInfo.Rows[0]["PJDocumentoRepLegal3"].ToString().Trim());
            parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombresRepLegal4", dtInfo.Rows[0]["PJNombresRepLegal4"].ToString().Trim());
            parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal4", dtInfo.Rows[0]["PJTipoDocRepLegal4"].ToString().Trim());
            parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocumentoRepLegal4", dtInfo.Rows[0]["PJDocumentoRepLegal4"].ToString().Trim());
            parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJCodCIIU2", dtInfo.Rows[0]["PJCodCIIU2"].ToString().Trim());
            parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJNacionalidad1", dtInfo.Rows[0]["PJNacionalidad1"].ToString().Trim());
            parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PJDV", dtInfo.Rows[0]["PJDV"].ToString().Trim());
            parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PJDpto", dtInfo.Rows[0]["PJDpto"].ToString().Trim());
            parameters[58] = new Microsoft.Reporting.WebForms.ReportParameter("PJDpto2", dtInfo.Rows[0]["PJDpto2"].ToString().Trim());
            parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta1", dtInfo.Rows[0]["PJPregunta1"].ToString().Trim());
            parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta2", dtInfo.Rows[0]["PJPregunta2"].ToString().Trim());
            parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta3", dtInfo.Rows[0]["PJPregunta3"].ToString().Trim());
            parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PJEspecificacionPreguntas", dtInfo.Rows[0]["PJEspecificacionPreguntas"].ToString().Trim());
            parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta4", dtInfo.Rows[0]["PJPregunta4"].ToString().Trim());
            parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta5", dtInfo.Rows[0]["PJPregunta5"].ToString().Trim());
            parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta6", dtInfo.Rows[0]["PJPregunta6"].ToString().Trim());
            parameters[66] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta7", dtInfo.Rows[0]["PJPregunta7"].ToString().Trim());
            parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta8", dtInfo.Rows[0]["PJPregunta8"].ToString().Trim());
            parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta9", dtInfo.Rows[0]["PJPregunta9"].ToString().Trim());
            parameters[69] = new Microsoft.Reporting.WebForms.ReportParameter("PJPregunta10", dtInfo.Rows[0]["PJPregunta10"].ToString().Trim());
            parameters[70] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep1Legal1", dtInfo.Rows[0]["PJPreguntaRep1Legal1"].ToString().Trim());
            parameters[71] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep1Legal2", dtInfo.Rows[0]["PJPreguntaRep1Legal2"].ToString().Trim());
            parameters[72] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep1Legal3", dtInfo.Rows[0]["PJPreguntaRep1Legal3"].ToString().Trim());
            parameters[73] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep1Legal4", dtInfo.Rows[0]["PJPreguntaRep1Legal4"].ToString().Trim());
            parameters[74] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep2Legal1", dtInfo.Rows[0]["PJPreguntaRep2Legal1"].ToString().Trim());
            parameters[75] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep2Legal2", dtInfo.Rows[0]["PJPreguntaRep2Legal2"].ToString().Trim());
            parameters[76] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep2Legal3", dtInfo.Rows[0]["PJPreguntaRep2Legal3"].ToString().Trim());
            parameters[77] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep2Legal4", dtInfo.Rows[0]["PJPreguntaRep2Legal4"].ToString().Trim());
            parameters[78] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep3Legal1", dtInfo.Rows[0]["PJPreguntaRep3Legal1"].ToString().Trim());
            parameters[79] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep3Legal2", dtInfo.Rows[0]["PJPreguntaRep3Legal2"].ToString().Trim());
            parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep3Legal3", dtInfo.Rows[0]["PJPreguntaRep3Legal3"].ToString().Trim());
            parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep3Legal4", dtInfo.Rows[0]["PJPreguntaRep3Legal4"].ToString().Trim());
            parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep4Legal1", dtInfo.Rows[0]["PJPreguntaRep4Legal1"].ToString().Trim());
            parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep4Legal2", dtInfo.Rows[0]["PJPreguntaRep4Legal2"].ToString().Trim());
            parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep4Legal3", dtInfo.Rows[0]["PJPreguntaRep4Legal3"].ToString().Trim());
            parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRep4Legal4", dtInfo.Rows[0]["PJPreguntaRep4Legal4"].ToString().Trim());
            parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRepPpalLegal1", dtInfo.Rows[0]["PJPreguntaRepPpalLegal1"].ToString().Trim());
            parameters[87] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRepPpalLegal2", dtInfo.Rows[0]["PJPreguntaRepPpalLegal2"].ToString().Trim());
            parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRepPpalLegal3", dtInfo.Rows[0]["PJPreguntaRepPpalLegal3"].ToString().Trim());
            parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("PJPreguntaRepPpalLegal4", dtInfo.Rows[0]["PJPreguntaRepPpalLegal4"].ToString().Trim());
            parameters[90] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT1", dtInfo.Rows[0]["PJNIT1"].ToString().Trim());
            parameters[91] = new Microsoft.Reporting.WebForms.ReportParameter("PJPais1", dtInfo.Rows[0]["PJPais1"].ToString().Trim());
            parameters[92] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT2", dtInfo.Rows[0]["PJNIT2"].ToString().Trim());
            parameters[93] = new Microsoft.Reporting.WebForms.ReportParameter("PJPais2", dtInfo.Rows[0]["PJPais2"].ToString().Trim());
            parameters[94] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT3", dtInfo.Rows[0]["PJNIT3"].ToString().Trim());
            parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("PJPais3", dtInfo.Rows[0]["PJPais3"].ToString().Trim());
            parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT4", dtInfo.Rows[0]["PJNIT4"].ToString().Trim());
            parameters[97] = new Microsoft.Reporting.WebForms.ReportParameter("PJPais4", dtInfo.Rows[0]["PJPais4"].ToString().Trim());
            parameters[98] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionFiscal1", dtInfo.Rows[0]["PJDireccionFiscal1"].ToString().Trim());
            parameters[99] = new Microsoft.Reporting.WebForms.ReportParameter("PJDireccionFiscal2", dtInfo.Rows[0]["PJDireccionFiscal2"].ToString().Trim());
            parameters[100] = new Microsoft.Reporting.WebForms.ReportParameter("PJParticipacion1", dtInfo.Rows[0]["PJParticipacion1"].ToString().Trim());
            parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("PJParticipacion2", dtInfo.Rows[0]["PJParticipacion2"].ToString().Trim());
            parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("PJParticipacion3", dtInfo.Rows[0]["PJParticipacion3"].ToString().Trim());
            parameters[103] = new Microsoft.Reporting.WebForms.ReportParameter("PJParticipacion4", dtInfo.Rows[0]["PJParticipacion4"].ToString().Trim());
            parameters[104] = new Microsoft.Reporting.WebForms.ReportParameter("PJParticipacion5", dtInfo.Rows[0]["PJParticipacion5"].ToString().Trim());
            parameters[105] = new Microsoft.Reporting.WebForms.ReportParameter("PJCotizaBolsa", dtInfo.Rows[0]["PJCotizaBolsa"].ToString().Trim());
            parameters[106] = new Microsoft.Reporting.WebForms.ReportParameter("PJEstatal", dtInfo.Rows[0]["PJEstatal"].ToString().Trim());
            parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("PJSinAnimoLucro", dtInfo.Rows[0]["PJSinAnimoLucro"].ToString().Trim());
            parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("PJSubsidiaria", dtInfo.Rows[0]["PJSubsidiaria"].ToString().Trim());
            parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("PJSocMatriz", dtInfo.Rows[0]["PJSocMatriz"].ToString().Trim());
            parameters[110] = new Microsoft.Reporting.WebForms.ReportParameter("PJSocMatrizIdenTrib", dtInfo.Rows[0]["PJSocMatrizIdenTrib"].ToString().Trim());
            parameters[111] = new Microsoft.Reporting.WebForms.ReportParameter("PJSocMatrizJurisdiccion", dtInfo.Rows[0]["PJSocMatrizJurisdiccion"].ToString().Trim());
            parameters[112] = new Microsoft.Reporting.WebForms.ReportParameter("PJSocMatrizDireccion", dtInfo.Rows[0]["PJSocMatrizDireccion"].ToString().Trim());
            parameters[113] = new Microsoft.Reporting.WebForms.ReportParameter("PJSocMatrizCiudad", dtInfo.Rows[0]["PJSocMatrizCiudad"].ToString().Trim());
            parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("PJSocMatrizTelefono", dtInfo.Rows[0]["PJSocMatrizTelefono"].ToString().Trim());
            parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("PJLugarNmto", dtInfo.Rows[0]["PJLugarNmto"].ToString().Trim());
            parameters[170] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocumentoEmpresa", dtInfo.Rows[0]["PJTipoDocumentoEmpresa"].ToString().Trim());
            parameters[171] = new Microsoft.Reporting.WebForms.ReportParameter("PJpePrimerApellido", dtInfo.Rows[0]["PJpePrimerApellido"].ToString().Trim());
            parameters[172] = new Microsoft.Reporting.WebForms.ReportParameter("PJpeSegundoApellido", dtInfo.Rows[0]["PJpeSegundoApellido"].ToString().Trim());
            parameters[173] = new Microsoft.Reporting.WebForms.ReportParameter("PJpeNombres", dtInfo.Rows[0]["PJpeNombres"].ToString().Trim());
            parameters[174] = new Microsoft.Reporting.WebForms.ReportParameter("PJpeOcupacion", dtInfo.Rows[0]["PJpeOcupacion"].ToString().Trim());
            parameters[175] = new Microsoft.Reporting.WebForms.ReportParameter("PJpeCargo", dtInfo.Rows[0]["PJpeCargo"].ToString().Trim());
            #endregion

            #region Financiera
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);

            parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("TransacMonedaExtra", dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim());
            parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("TipoTransaccion", dtInfo.Rows[0]["TipoTransaccion"].ToString().Trim());
            parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("OtroTipoTransaccion", dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim());
            parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto1", dtInfo.Rows[0]["PFTipoProducto1"].ToString().Trim());
            parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto1", dtInfo.Rows[0]["PFNumeroProducto1"].ToString().Trim());
            parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad1", dtInfo.Rows[0]["PFEntidad1"].ToString().Trim());
            parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto1", dtInfo.Rows[0]["PFMonto1"].ToString().Trim());
            parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad1", dtInfo.Rows[0]["PFCiudad1"].ToString().Trim());
            parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais1", dtInfo.Rows[0]["PFPais1"].ToString().Trim());
            parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda1", dtInfo.Rows[0]["PFMoneda1"].ToString().Trim());
            parameters[126] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto2", dtInfo.Rows[0]["PFTipoProducto2"].ToString().Trim());
            parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto2", dtInfo.Rows[0]["PFNumeroProducto2"].ToString().Trim());
            parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad2", dtInfo.Rows[0]["PFEntidad2"].ToString().Trim());
            parameters[129] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto2", dtInfo.Rows[0]["PFMonto2"].ToString().Trim());
            parameters[130] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad2", dtInfo.Rows[0]["PFCiudad2"].ToString().Trim());
            parameters[131] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais2", dtInfo.Rows[0]["PFPais2"].ToString().Trim());
            parameters[132] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda2", dtInfo.Rows[0]["PFMoneda2"].ToString().Trim());
            parameters[133] = new Microsoft.Reporting.WebForms.ReportParameter("PFCtaMonedaExtra", dtInfo.Rows[0]["PFCtaMonedaExtra"].ToString().Trim());
            parameters[134] = new Microsoft.Reporting.WebForms.ReportParameter("PFTipoProducto3", dtInfo.Rows[0]["PFTipoProducto3"].ToString().Trim());
            parameters[135] = new Microsoft.Reporting.WebForms.ReportParameter("PFNumeroProducto3", dtInfo.Rows[0]["PFNumeroProducto3"].ToString().Trim());
            parameters[136] = new Microsoft.Reporting.WebForms.ReportParameter("PFEntidad3", dtInfo.Rows[0]["PFEntidad3"].ToString().Trim());
            parameters[137] = new Microsoft.Reporting.WebForms.ReportParameter("PFMonto3", dtInfo.Rows[0]["PFMonto3"].ToString().Trim());
            parameters[138] = new Microsoft.Reporting.WebForms.ReportParameter("PFCiudad3", dtInfo.Rows[0]["PFCiudad3"].ToString().Trim());
            parameters[139] = new Microsoft.Reporting.WebForms.ReportParameter("PFPais3", dtInfo.Rows[0]["PFPais3"].ToString().Trim());
            parameters[140] = new Microsoft.Reporting.WebForms.ReportParameter("PFMoneda3", dtInfo.Rows[0]["PFMoneda3"].ToString().Trim());
            parameters[169] = new Microsoft.Reporting.WebForms.ReportParameter("PFProdExterior", dtInfo.Rows[0]["PFProdExterior"].ToString().Trim());
            #endregion

            #region Seguros y Fondos
            dtInfo = cKnowClient.InfoFormSeguros(IdConocimientoCliente);
            parameters[141] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno1", dtInfo.Rows[0]["SeguroAno1"].ToString().Trim());
            parameters[142] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo1", dtInfo.Rows[0]["SeguroRamo1"].ToString().Trim());
            parameters[143] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania1", dtInfo.Rows[0]["SeguroCompania1"].ToString().Trim());
            parameters[144] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor1", dtInfo.Rows[0]["SeguroValor1"].ToString().Trim());
            parameters[145] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroTipo1", dtInfo.Rows[0]["SeguroTipo1"].ToString().Trim());
            parameters[146] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroAno2", dtInfo.Rows[0]["SeguroAno2"].ToString().Trim());
            parameters[147] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroRamo2", dtInfo.Rows[0]["SeguroRamo2"].ToString().Trim());
            parameters[148] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroCompania2", dtInfo.Rows[0]["SeguroCompania2"].ToString().Trim());
            parameters[149] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroValor2", dtInfo.Rows[0]["SeguroValor2"].ToString().Trim());
            parameters[150] = new Microsoft.Reporting.WebForms.ReportParameter("SeguroTipo2", dtInfo.Rows[0]["SeguroTipo2"].ToString().Trim());
            parameters[151] = new Microsoft.Reporting.WebForms.ReportParameter("OrigenFondos", dtInfo.Rows[0]["OrigenFondos"].ToString().Trim());
            parameters[152] = new Microsoft.Reporting.WebForms.ReportParameter("Reclamaciones", dtInfo.Rows[0]["Reclamaciones"].ToString().Trim());
            #endregion

            #region Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);
            parameters[153] = new Microsoft.Reporting.WebForms.ReportParameter("LugarEntrevista", dtInfo.Rows[0]["LugarEntrevista"].ToString().Trim());
            parameters[154] = new Microsoft.Reporting.WebForms.ReportParameter("FechaEntrevista", dtInfo.Rows[0]["FechaEntrevista"].ToString().Trim());
            parameters[155] = new Microsoft.Reporting.WebForms.ReportParameter("HoraEntrevista", dtInfo.Rows[0]["HoraEntrevista"].ToString().Trim());
            parameters[156] = new Microsoft.Reporting.WebForms.ReportParameter("Resultado", dtInfo.Rows[0]["Resultado"].ToString().Trim());
            parameters[157] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones1", dtInfo.Rows[0]["Observaciones1"].ToString().Trim());
            parameters[158] = new Microsoft.Reporting.WebForms.ReportParameter("NombreResponsable", dtInfo.Rows[0]["NombreResponsable"].ToString().Trim());
            parameters[159] = new Microsoft.Reporting.WebForms.ReportParameter("FechaVerificacion", dtInfo.Rows[0]["FechaVerificacion"].ToString().Trim());
            parameters[160] = new Microsoft.Reporting.WebForms.ReportParameter("HoraVerificacion", dtInfo.Rows[0]["HoraVerificacion"].ToString().Trim());
            parameters[161] = new Microsoft.Reporting.WebForms.ReportParameter("NombreVerifica", dtInfo.Rows[0]["NombreVerifica"].ToString().Trim());
            parameters[162] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones2", dtInfo.Rows[0]["Observaciones2"].ToString().Trim());
            parameters[163] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaFirma", dtInfo.Rows[0]["ValidaFirma"].ToString().Trim());
            parameters[164] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaHuella", dtInfo.Rows[0]["ValidaHuella"].ToString().Trim());
            parameters[165] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaEntrevista", dtInfo.Rows[0]["ValidaEntrevista"].ToString().Trim());
            parameters[166] = new Microsoft.Reporting.WebForms.ReportParameter("CCRespEntrevista", dtInfo.Rows[0]["CCRespEntrevista"].ToString().Trim());
            parameters[167] = new Microsoft.Reporting.WebForms.ReportParameter("CCVerificaEntrevista", dtInfo.Rows[0]["CCVerificaEntrevista"].ToString().Trim());
            parameters[168] = new Microsoft.Reporting.WebForms.ReportParameter("NombreIntermediario", dtInfo.Rows[0]["NombreIntermediario"].ToString().Trim());

            parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("Usuario", dtInfo.Rows[0]["Usuario"].ToString().Trim());
            parameters[177] = new Microsoft.Reporting.WebForms.ReportParameter("FechaRegistro", dtInfo.Rows[0]["FechaRegistro"].ToString().Trim());
            #endregion

            ReportViewer1.LocalReport.SetParameters(parameters);
            //ReportViewer1.LocalReport.Refresh();
        }

        /// <summary>
        /// Hidden the special SSRS rendering format in ReportViewer control
        /// </summary>
        /// <param name="ReportViewerID">The ID of the relevant ReportViewer control</param>
        /// <param name="strFormatName">Format Name</param>
        public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        {
            FieldInfo info;
            foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
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
