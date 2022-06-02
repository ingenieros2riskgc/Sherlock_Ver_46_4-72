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
    public partial class ReporteKnowClienteCopidrogas : System.Web.UI.UserControl
    {
        private cKnowClient cKnowClient = new cKnowClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                loadValue();
        }

        private void loadValue()
        {
            try
            {
                int IdConocimientoCliente;
                IdConocimientoCliente = Convert.ToInt32(Request.QueryString["IdConocimientoCliente"]);
                mtdConsultaReporte(IdConocimientoCliente);
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar la información. " + ex.Message);
            }
        }

        private void mtdConsultaReporte(int IdConocimientoCliente)
        {
            DataTable dtInfo = new DataTable();
            Microsoft.Reporting.WebForms.ReportParameter[] parameters = new Microsoft.Reporting.WebForms.ReportParameter[177];

            #region Info Basica formulario
            dtInfo = cKnowClient.InfoFormCliente(IdConocimientoCliente);

            #region Tipo Vinculacion
            switch (dtInfo.Rows[0]["OtraClaseVinculacion"].ToString())
            {
                case "NUEVO":
                    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("Nuevo", "X");
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("Actualizacion", "");
                    break;
                case "ACTUALIZACION":
                    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("Nuevo", "");
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("Actualizacion", "X");
                    break;
                default:
                    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("Nuevo", "");
                    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("Actualizacion", "");
                    break;

                #region Modificacion
                //case "PERSONA NATURAL":
                //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaNatural", "X");
                //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Reingreso", "");
                //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Sucesion", "");
                //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaJuridica", "");
                //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Traslado", "");
                //    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("ActualizacionInfo", "");
                //    break;
                //case "REINGRESO":
                //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaNatural", "");
                //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Reingreso", "X");
                //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Sucesion", "");
                //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaJuridica", "");
                //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Traslado", "");
                //    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("ActualizacionInfo", "");
                //    break;
                //case "SUCESION":
                //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaNatural", "");
                //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Reingreso", "");
                //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Sucesion", "X");
                //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaJuridica", "");
                //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Traslado", "");
                //    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("ActualizacionInfo", "");
                //    break;
                //case "PERSONA JURIDICA":
                //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaNatural", "");
                //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Reingreso", "");
                //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Sucesion", "");
                //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaJuridica", "X");
                //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Traslado", "");
                //    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("ActualizacionInfo", "");
                //    break;
                //case "TRASLADO":
                //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaNatural", "");
                //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Reingreso", "");
                //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Sucesion", "");
                //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaJuridica", "");
                //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Traslado", "X");
                //    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("ActualizacionInfo", "");
                //    break;
                //case "ACTUALIZACION DE INFORMACION":
                //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaNatural", "");
                //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Reingreso", "");
                //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Sucesion", "");
                //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaJuridica", "");
                //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Traslado", "");
                //    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("ActualizacionInfo", "X");
                //    break;
                //default:
                //    parameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaNatural", "");
                //    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Reingreso", "");
                //    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("Sucesion", "");
                //    parameters[3] = new Microsoft.Reporting.WebForms.ReportParameter("PersonaJuridica", "");
                //    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Traslado", "");
                //    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("ActualizacionInfo", "");
                //    break;
                #endregion
            }
            #endregion

            #region Tipo Cliente
            switch (dtInfo.Rows[0]["TipoCliente"].ToString())
            {
                case "ASOCIADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Asociado", "X");
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("PreAsociado", "");
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Empleado", "");
                    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("Proveedor", "");
                    break;
                case "PRE-ASOCIADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Asociado", "");
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("PreAsociado", "X");
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Empleado", "");
                    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("Proveedor", "");
                    break;
                case "EMPLEADO":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Asociado", "");
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("PreAsociado", "");
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Empleado", "X");
                    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("Proveedor", "");
                    break;
                case "PROVEEDOR":
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Asociado", "");
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("PreAsociado", "");
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Empleado", "");
                    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("Proveedor", "X");
                    break;
                default:
                    parameters[1] = new Microsoft.Reporting.WebForms.ReportParameter("Asociado", "");
                    parameters[2] = new Microsoft.Reporting.WebForms.ReportParameter("PreAsociado", "");
                    parameters[4] = new Microsoft.Reporting.WebForms.ReportParameter("Empleado", "");
                    parameters[176] = new Microsoft.Reporting.WebForms.ReportParameter("Proveedor", "");                    
                    break;
            }
            #endregion

            #endregion

            #region Persona Natural
            dtInfo = cKnowClient.InfoFormPN(IdConocimientoCliente);
            #region TipoDocumento
            switch (dtInfo.Rows[0]["PNTipoDocumento"].ToString().Trim())
            {
                case "C.C.":
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", "X");
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNNUIP", "");
                    parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", "");
                    parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("PNRS", "");
                    parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", "");
                    break;
                case "NUIP":
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", "");
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNNUIP", "X");
                    parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", "");
                    parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("PNRS", "");
                    parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", "");
                    break;
                case "C.E.":
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", "");
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNNUIP", "");
                    parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", "X");
                    parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("PNRS", "");
                    parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", "");
                    break;
                case "R.S.":
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", "");
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNNUIP", "");
                    parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", "");
                    parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("PNRS", "X");
                    parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", "");
                    break;
                case "T.I.":
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", "");
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNNUIP", "");
                    parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", "");
                    parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("PNRS", "");
                    parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", "X");
                    break;
                default:
                    parameters[5] = new Microsoft.Reporting.WebForms.ReportParameter("PNCC", "");
                    parameters[6] = new Microsoft.Reporting.WebForms.ReportParameter("PNNUIP", "");
                    parameters[7] = new Microsoft.Reporting.WebForms.ReportParameter("PNCE", "");
                    parameters[8] = new Microsoft.Reporting.WebForms.ReportParameter("PNRS", "");
                    parameters[9] = new Microsoft.Reporting.WebForms.ReportParameter("PNTI", "");
                    break;
            }
            #endregion

            parameters[10] = new Microsoft.Reporting.WebForms.ReportParameter("PNPrimerApellido", dtInfo.Rows[0]["PNPrimerApellido"].ToString().Trim());
            parameters[11] = new Microsoft.Reporting.WebForms.ReportParameter("PNSegunApellido", dtInfo.Rows[0]["PNSegunApellido"].ToString().Trim());
            parameters[12] = new Microsoft.Reporting.WebForms.ReportParameter("PNNombres", dtInfo.Rows[0]["PNNombres"].ToString().Trim());
            parameters[13] = new Microsoft.Reporting.WebForms.ReportParameter("PNNumeroDocumento", dtInfo.Rows[0]["PNNumeroDocumento"].ToString().Trim());
            parameters[14] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaExpedicion", dtInfo.Rows[0]["PNFechaExpedicion"].ToString().Trim());
            parameters[15] = new Microsoft.Reporting.WebForms.ReportParameter("PNLugarExp", dtInfo.Rows[0]["PNLugar"].ToString().Trim());
            parameters[16] = new Microsoft.Reporting.WebForms.ReportParameter("PNFechaNacimiento", dtInfo.Rows[0]["PNFechaNacimiento"].ToString().Trim());
            parameters[17] = new Microsoft.Reporting.WebForms.ReportParameter("PNLugarNmto", dtInfo.Rows[0]["PNLugarNmto"].ToString().Trim());
            parameters[18] = new Microsoft.Reporting.WebForms.ReportParameter("PNNacionalidad", dtInfo.Rows[0]["PNNacionalidad"].ToString().Trim());

            #region Sexo
            switch (dtInfo.Rows[0]["PNSexo"].ToString().Trim())
            {
                case "Masculino":
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoM", "X");
                    parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoF", "");
                    break;
                case "Femenino":
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoM", "");
                    parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoF", "X");
                    break;
                default:
                    parameters[19] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoM", "");
                    parameters[20] = new Microsoft.Reporting.WebForms.ReportParameter("PNSexoF", "");
                    break;
            }
            #endregion

            parameters[21] = new Microsoft.Reporting.WebForms.ReportParameter("PNActividadEconomica", dtInfo.Rows[0]["PNActividadEconomica"].ToString().Trim());
            parameters[22] = new Microsoft.Reporting.WebForms.ReportParameter("PNCIIU", dtInfo.Rows[0]["PNCIIU"].ToString());

            #region Estado Civil
            switch (dtInfo.Rows[0]["PNEstadoCivil"].ToString().Trim())
            {
                case "Soltero":
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "X");
                    parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "");
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNUnionLibre", "");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDivorciado", "");
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "");
                    break;
                case "Casado":
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "");
                    parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "X");
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNUnionLibre", "");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDivorciado", "");
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "");
                    break;
                case "Union libre":
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "");
                    parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "");
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNUnionLibre", "X");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDivorciado", "");
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "");
                    break;
                case "Viudo":
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "");
                    parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "");
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNUnionLibre", "");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "X");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDivorciado", "");
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "");
                    break;
                case "Divorciado":
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "");
                    parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "");
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNUnionLibre", "");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDivorciado", "X");
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "");
                    break;
                case "Separado":
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "");
                    parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "");
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNUnionLibre", "");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDivorciado", "");
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "X");
                    break;
                default:
                    parameters[23] = new Microsoft.Reporting.WebForms.ReportParameter("PNSoltero", "");
                    parameters[24] = new Microsoft.Reporting.WebForms.ReportParameter("PNCasado", "");
                    parameters[25] = new Microsoft.Reporting.WebForms.ReportParameter("PNUnionLibre", "");
                    parameters[26] = new Microsoft.Reporting.WebForms.ReportParameter("PNViudo", "");
                    parameters[27] = new Microsoft.Reporting.WebForms.ReportParameter("PNDivorciado", "");
                    parameters[28] = new Microsoft.Reporting.WebForms.ReportParameter("PNSeparado", "");
                    break;
            }
            #endregion

            parameters[29] = new Microsoft.Reporting.WebForms.ReportParameter("PNDirDomPpal", dtInfo.Rows[0]["PNDireccionResidencia"].ToString().Trim());
            parameters[30] = new Microsoft.Reporting.WebForms.ReportParameter("PNDptoDomPpal", dtInfo.Rows[0]["PNDpto"].ToString().Trim());
            parameters[31] = new Microsoft.Reporting.WebForms.ReportParameter("PNCiudadDomPpal", dtInfo.Rows[0]["PNCiudad2"].ToString().Trim());
            parameters[32] = new Microsoft.Reporting.WebForms.ReportParameter("PNBarrioDomPpal", dtInfo.Rows[0]["PNBarrio"].ToString().Trim());
            parameters[33] = new Microsoft.Reporting.WebForms.ReportParameter("PNEstratoDomPpal", dtInfo.Rows[0]["PNEstrato"].ToString().Trim());
            parameters[34] = new Microsoft.Reporting.WebForms.ReportParameter("PNTelDomPpal", dtInfo.Rows[0]["PNTelefono2"].ToString().Trim());
            parameters[35] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta2", dtInfo.Rows[0]["PNPregunta2"].ToString().Trim());
            parameters[36] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta3", dtInfo.Rows[0]["PNPregunta3"].ToString().Trim());
            parameters[37] = new Microsoft.Reporting.WebForms.ReportParameter("PNPregunta1", dtInfo.Rows[0]["PNPregunta1"].ToString().Trim());
            parameters[38] = new Microsoft.Reporting.WebForms.ReportParameter("PNIngresosMensuales", dtInfo.Rows[0]["PNIngresosMensuales"].ToString().Trim());
            parameters[39] = new Microsoft.Reporting.WebForms.ReportParameter("PNActivos", dtInfo.Rows[0]["PNActivos"].ToString().Trim());
            parameters[40] = new Microsoft.Reporting.WebForms.ReportParameter("PNEgresoMensuales", dtInfo.Rows[0]["PNEgresoMensuales"].ToString().Trim());
            parameters[41] = new Microsoft.Reporting.WebForms.ReportParameter("PNPasivos", dtInfo.Rows[0]["PNPasivos"].ToString().Trim());
            parameters[42] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtrosIngresos", dtInfo.Rows[0]["PNOtrosIngresos"].ToString().Trim());
            parameters[43] = new Microsoft.Reporting.WebForms.ReportParameter("PNConceptoOtrosIngresos", dtInfo.Rows[0]["PNConceptoOtrosIngresos"].ToString().Trim());
            parameters[119] = new Microsoft.Reporting.WebForms.ReportParameter("PNNroCelular", dtInfo.Rows[0]["PNCelular"].ToString().Trim());
            parameters[120] = new Microsoft.Reporting.WebForms.ReportParameter("PNEmail", dtInfo.Rows[0]["PNCorreoElectronico"].ToString().Trim());
            parameters[121] = new Microsoft.Reporting.WebForms.ReportParameter("PNTipoInmueble", dtInfo.Rows[0]["PNTipoInmueble"].ToString().Trim());
            parameters[122] = new Microsoft.Reporting.WebForms.ReportParameter("PNTipoVivienda", dtInfo.Rows[0]["PNTipoVivienda"].ToString().Trim());
            parameters[123] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtroTipoInmueble", dtInfo.Rows[0]["PNOtroTipoInmueble"].ToString().Trim());
            parameters[124] = new Microsoft.Reporting.WebForms.ReportParameter("PNOtroTipoVivienda", dtInfo.Rows[0]["PNOtroTipoVivienda"].ToString().Trim());
            parameters[125] = new Microsoft.Reporting.WebForms.ReportParameter("PNViviendaPropia", dtInfo.Rows[0]["PNViviendaPropia"].ToString().Trim());
            parameters[126] = new Microsoft.Reporting.WebForms.ReportParameter("PNCreditoHipo", dtInfo.Rows[0]["PNCreditoHipo"].ToString().Trim());
            parameters[127] = new Microsoft.Reporting.WebForms.ReportParameter("PNVlrCuotaMen", dtInfo.Rows[0]["PNVlrCuotaMen"].ToString().Trim());
            parameters[128] = new Microsoft.Reporting.WebForms.ReportParameter("PNEntidadFin", dtInfo.Rows[0]["PNEntidadFinanc"].ToString().Trim());
            #endregion

            #region Info Juridica
            dtInfo = cKnowClient.InfoFormPJ(IdConocimientoCliente);
            parameters[44] = new Microsoft.Reporting.WebForms.ReportParameter("PJRazonDenominacion", dtInfo.Rows[0]["PJRazonDenominacion"].ToString().Trim());
            parameters[45] = new Microsoft.Reporting.WebForms.ReportParameter("PJNIT", dtInfo.Rows[0]["PJNIT"].ToString().Trim());
            parameters[46] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoPerJur", dtInfo.Rows[0]["PJTipoPersonaJur"].ToString().Trim());
            parameters[47] = new Microsoft.Reporting.WebForms.ReportParameter("PJSocCom", dtInfo.Rows[0]["PJSociedadComercial"].ToString().Trim());
            parameters[48] = new Microsoft.Reporting.WebForms.ReportParameter("PJActEconomica", dtInfo.Rows[0]["PJActividadEconomica"].ToString().Trim());
            parameters[49] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIIU", dtInfo.Rows[0]["PJCIIU"].ToString().Trim());
            parameters[50] = new Microsoft.Reporting.WebForms.ReportParameter("PJCapital", dtInfo.Rows[0]["PJCapitalSocial"].ToString().Trim());
            parameters[51] = new Microsoft.Reporting.WebForms.ReportParameter("PJFechaConst", dtInfo.Rows[0]["PJFechaConstitucion"].ToString().Trim());
            parameters[52] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocConst", dtInfo.Rows[0]["PJDocumentoConstitucion"].ToString().Trim());
            parameters[53] = new Microsoft.Reporting.WebForms.ReportParameter("PJFechaReg", dtInfo.Rows[0]["PJFechaRegistro"].ToString().Trim());
            parameters[54] = new Microsoft.Reporting.WebForms.ReportParameter("PJMatMercantil", dtInfo.Rows[0]["PJMatriculaMercantil"].ToString().Trim());
            parameters[55] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefono", dtInfo.Rows[0]["PJTelefono1"].ToString().Trim());
            parameters[56] = new Microsoft.Reporting.WebForms.ReportParameter("PJEmail", dtInfo.Rows[0]["PJCorreoPrincipal"].ToString().Trim());
            parameters[57] = new Microsoft.Reporting.WebForms.ReportParameter("PJRegSuperSol", dtInfo.Rows[0]["PJRegSuperSolidaria"].ToString().Trim());
            parameters[58] = new Microsoft.Reporting.WebForms.ReportParameter("PJRepLegalPpal", dtInfo.Rows[0]["PJNombresRepLegalPpal"].ToString().Trim());
            parameters[59] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocRepLegalPpal", dtInfo.Rows[0]["PJDocumentoRepLegalPpal"].ToString().Trim());
            parameters[60] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegalPpal", dtInfo.Rows[0]["PJTipoDocRepLegalPpal"].ToString().Trim());
            parameters[61] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS1", dtInfo.Rows[0]["PJNombreAS1"].ToString().Trim());
            parameters[62] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS1", dtInfo.Rows[0]["PJTipoIdentificacionAS1"].ToString().Trim());
            parameters[63] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS1", dtInfo.Rows[0]["PJNumeroDocumentoAS1"].ToString().Trim());
            parameters[64] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS2", dtInfo.Rows[0]["PJNombreAS2"].ToString().Trim());
            parameters[65] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS2", dtInfo.Rows[0]["PJTipoIdentificacionAS2"].ToString().Trim());
            parameters[66] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS2", dtInfo.Rows[0]["PJNumeroDocumentoAS2"].ToString().Trim());
            parameters[67] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS3", dtInfo.Rows[0]["PJNombreAS3"].ToString().Trim());
            parameters[68] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS3", dtInfo.Rows[0]["PJTipoIdentificacionAS3"].ToString().Trim());
            parameters[69] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS3", dtInfo.Rows[0]["PJNumeroDocumentoAS3"].ToString().Trim());
            parameters[70] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS4", dtInfo.Rows[0]["PJNombreAS4"].ToString().Trim());
            parameters[71] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS4", dtInfo.Rows[0]["PJTipoIdentificacionAS4"].ToString().Trim());
            parameters[72] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS4", dtInfo.Rows[0]["PJNumeroDocumentoAS4"].ToString().Trim());
            parameters[73] = new Microsoft.Reporting.WebForms.ReportParameter("PJNombreAS5", dtInfo.Rows[0]["PJNombreAS5"].ToString().Trim());
            parameters[74] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoIdentificacionAS5", dtInfo.Rows[0]["PJTipoIdentificacionAS5"].ToString().Trim());
            parameters[75] = new Microsoft.Reporting.WebForms.ReportParameter("PJNumeroDocumentoAS5", dtInfo.Rows[0]["PJNumeroDocumentoAS5"].ToString().Trim());
            parameters[76] = new Microsoft.Reporting.WebForms.ReportParameter("PJIngresosMensuales", dtInfo.Rows[0]["PJIngresosMensuales"].ToString().Trim());
            parameters[77] = new Microsoft.Reporting.WebForms.ReportParameter("PJActivos", dtInfo.Rows[0]["PJActivos"].ToString().Trim());
            parameters[78] = new Microsoft.Reporting.WebForms.ReportParameter("PJEgresoMensuales", dtInfo.Rows[0]["PJEgresoMensuales"].ToString().Trim());
            parameters[79] = new Microsoft.Reporting.WebForms.ReportParameter("PJPasivos", dtInfo.Rows[0]["PJPasivos"].ToString().Trim());
            parameters[80] = new Microsoft.Reporting.WebForms.ReportParameter("PJOtrosIngresos", dtInfo.Rows[0]["PJOtrosIngresos"].ToString().Trim());
            parameters[81] = new Microsoft.Reporting.WebForms.ReportParameter("PJConceptoOtrosIngresos", dtInfo.Rows[0]["PJConceptoOtrosIngresos"].ToString().Trim());
            parameters[129] = new Microsoft.Reporting.WebForms.ReportParameter("PJRepLegal1", dtInfo.Rows[0]["PJNombresRepLegal1"].ToString().Trim());
            parameters[130] = new Microsoft.Reporting.WebForms.ReportParameter("PJRepLegal2", dtInfo.Rows[0]["PJNombresRepLegal2"].ToString().Trim());
            parameters[131] = new Microsoft.Reporting.WebForms.ReportParameter("PJRepLegal3", dtInfo.Rows[0]["PJNombresRepLegal3"].ToString().Trim());
            parameters[132] = new Microsoft.Reporting.WebForms.ReportParameter("PJRepLegal4", dtInfo.Rows[0]["PJNombresRepLegal4"].ToString().Trim());
            parameters[133] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal1", dtInfo.Rows[0]["PJTipoDocRepLegal1"].ToString().Trim());
            parameters[134] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal2", dtInfo.Rows[0]["PJTipoDocRepLegal2"].ToString().Trim());
            parameters[135] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal3", dtInfo.Rows[0]["PJTipoDocRepLegal3"].ToString().Trim());
            parameters[136] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegal4", dtInfo.Rows[0]["PJTipoDocRepLegal4"].ToString().Trim());
            parameters[137] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocRepLegal1", dtInfo.Rows[0]["PJDocumentoRepLegal1"].ToString().Trim());
            parameters[138] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocRepLegal2", dtInfo.Rows[0]["PJDocumentoRepLegal2"].ToString().Trim());
            parameters[139] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocRepLegal3", dtInfo.Rows[0]["PJDocumentoRepLegal3"].ToString().Trim());
            parameters[140] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocRepLegal4", dtInfo.Rows[0]["PJDocumentoRepLegal4"].ToString().Trim());
            parameters[141] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefonoRepLegal", dtInfo.Rows[0]["PJTelefonoRepLegal"].ToString().Trim());
            parameters[142] = new Microsoft.Reporting.WebForms.ReportParameter("PJLugarRepLegal", dtInfo.Rows[0]["PJDptoCiudadRepLegal"].ToString().Trim());
            parameters[143] = new Microsoft.Reporting.WebForms.ReportParameter("PJRepLegalSupl", dtInfo.Rows[0]["PJNombresRepLegalSupl"].ToString().Trim());
            parameters[144] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoDocRepLegalSupl", dtInfo.Rows[0]["PJTipoDocRepLegalSupl"].ToString().Trim());
            parameters[145] = new Microsoft.Reporting.WebForms.ReportParameter("PJDocRepLegalSupl", dtInfo.Rows[0]["PJDocumentoRepLegalSupl"].ToString().Trim());
            parameters[146] = new Microsoft.Reporting.WebForms.ReportParameter("PJTelefonoRepLegalSupl", dtInfo.Rows[0]["PJTelefonoRepLegalSupl"].ToString().Trim());
            parameters[147] = new Microsoft.Reporting.WebForms.ReportParameter("PJLugarRepLegalSupl", dtInfo.Rows[0]["PJDptoCiudadRepLegalSupl"].ToString().Trim());
            parameters[148] = new Microsoft.Reporting.WebForms.ReportParameter("PJTipoEmpresa", dtInfo.Rows[0]["PJTipoEmpresa"].ToString().Trim());
            parameters[149] = new Microsoft.Reporting.WebForms.ReportParameter("PJDescActEcoPpal", dtInfo.Rows[0]["PJDescActEcoPpal"].ToString().Trim());
            parameters[150] = new Microsoft.Reporting.WebForms.ReportParameter("PJCIIUActEcoPpal", dtInfo.Rows[0]["PJCodCIIU2"].ToString().Trim());
            #endregion

            #region Info Fiscal
            dtInfo = cKnowClient.InfoFormPF(IdConocimientoCliente);
            parameters[82] = new Microsoft.Reporting.WebForms.ReportParameter("TransacMonedaExtra", dtInfo.Rows[0]["TransacMonedaExtra"].ToString().Trim());
            parameters[83] = new Microsoft.Reporting.WebForms.ReportParameter("TipoTransaccion", dtInfo.Rows[0]["OtroTipoTransaccion"].ToString().Trim());
            parameters[84] = new Microsoft.Reporting.WebForms.ReportParameter("PJCtaMonExtra", dtInfo.Rows[0]["PFCtaMonedaExtra"].ToString().Trim());
            parameters[85] = new Microsoft.Reporting.WebForms.ReportParameter("PJNoCta", dtInfo.Rows[0]["PFNroCtaMonedaExtra"].ToString().Trim());
            parameters[86] = new Microsoft.Reporting.WebForms.ReportParameter("PJBancoMonExtra", dtInfo.Rows[0]["PFBancoCtaMonedaExtra"].ToString().Trim());
            parameters[87] = new Microsoft.Reporting.WebForms.ReportParameter("PJCiudadMonExtra", dtInfo.Rows[0]["PFCiudadCtaMonedaExtra"].ToString().Trim());
            parameters[88] = new Microsoft.Reporting.WebForms.ReportParameter("PJPaisMonExtra", dtInfo.Rows[0]["PFPaisCtaMonedaExtra"].ToString().Trim());
            parameters[89] = new Microsoft.Reporting.WebForms.ReportParameter("PJMoneda", dtInfo.Rows[0]["PFMonedaCtaMonedaExtra"].ToString().Trim());
            #endregion

            #region Info Drogueria
            dtInfo = cKnowClient.InfoFormDrogueria(IdConocimientoCliente);
            parameters[90] = new Microsoft.Reporting.WebForms.ReportParameter("NombreDrogueriaPpal", dtInfo.Rows[0]["NombreDrogueriaPpal"].ToString());
            parameters[91] = new Microsoft.Reporting.WebForms.ReportParameter("NITDrogueriaPpal", dtInfo.Rows[0]["NITDrogueriaPpal"].ToString());
            parameters[92] = new Microsoft.Reporting.WebForms.ReportParameter("DirDrogueriaPpal", dtInfo.Rows[0]["DirDrogueriaPpal"].ToString());
            parameters[93] = new Microsoft.Reporting.WebForms.ReportParameter("DptoDrogueriaPpal", dtInfo.Rows[0]["DptoDrogueriaPpal"].ToString());
            parameters[94] = new Microsoft.Reporting.WebForms.ReportParameter("CiudadDrogueriaPpal", dtInfo.Rows[0]["CiudadDrogueriaPpal"].ToString());
            parameters[95] = new Microsoft.Reporting.WebForms.ReportParameter("BarrioDrogueriaPpal", dtInfo.Rows[0]["BarrioDrogueriaPpal"].ToString());
            parameters[96] = new Microsoft.Reporting.WebForms.ReportParameter("TelefonoDrogueriaPpal", dtInfo.Rows[0]["TelDrogueriaPpal"].ToString());
            parameters[97] = new Microsoft.Reporting.WebForms.ReportParameter("NombreDrogueria2", dtInfo.Rows[0]["NombreDrogueria1"].ToString());
            parameters[98] = new Microsoft.Reporting.WebForms.ReportParameter("DirDrogueria2", dtInfo.Rows[0]["DirDrogueria1"].ToString());
            parameters[99] = new Microsoft.Reporting.WebForms.ReportParameter("DptoDrogueria2", dtInfo.Rows[0]["DptoDrogueria1"].ToString());
            parameters[100] = new Microsoft.Reporting.WebForms.ReportParameter("CiudadDrogueria2", dtInfo.Rows[0]["CiudadDrogueria1"].ToString());
            parameters[101] = new Microsoft.Reporting.WebForms.ReportParameter("BarrioDrogueria2", dtInfo.Rows[0]["BarrioDrogueria1"].ToString().Trim());
            parameters[102] = new Microsoft.Reporting.WebForms.ReportParameter("TelefonoDrogueria2", dtInfo.Rows[0]["TelDrogueria1"].ToString().Trim());
            parameters[103] = new Microsoft.Reporting.WebForms.ReportParameter("NombreDrogueria3", dtInfo.Rows[0]["NombreDrogueria2"].ToString().Trim());
            parameters[104] = new Microsoft.Reporting.WebForms.ReportParameter("DirDrogueria3", dtInfo.Rows[0]["DirDrogueria2"].ToString().Trim());
            parameters[105] = new Microsoft.Reporting.WebForms.ReportParameter("DptoDrogueria3", dtInfo.Rows[0]["DptoDrogueria2"].ToString().Trim());
            parameters[106] = new Microsoft.Reporting.WebForms.ReportParameter("CiudadDrogueria3", dtInfo.Rows[0]["CiudadDrogueria2"].ToString().Trim());
            parameters[107] = new Microsoft.Reporting.WebForms.ReportParameter("BarrioDrogueria3", dtInfo.Rows[0]["BarrioDrogueria2"].ToString().Trim());
            parameters[108] = new Microsoft.Reporting.WebForms.ReportParameter("TelefonoDrogueria3", dtInfo.Rows[0]["TelDrogueria2"].ToString().Trim());
            #endregion

            #region Info Sometimiento 1
            dtInfo = cKnowClient.InfoFormSometimiento(IdConocimientoCliente);
            parameters[151] = new Microsoft.Reporting.WebForms.ReportParameter("AportesXPagar", dtInfo.Rows[0]["AportesSoc"].ToString().Trim());
            parameters[152] = new Microsoft.Reporting.WebForms.ReportParameter("VlrCtaCopicredito", dtInfo.Rows[0]["VlrAperturaCopicredito"].ToString().Trim());
            parameters[153] = new Microsoft.Reporting.WebForms.ReportParameter("CompromisoMensual", dtInfo.Rows[0]["CompromisoAhorroMen"].ToString().Trim());
            parameters[154] = new Microsoft.Reporting.WebForms.ReportParameter("CuotaAdmision", dtInfo.Rows[0]["CuotaAdm"].ToString().Trim());
            parameters[155] = new Microsoft.Reporting.WebForms.ReportParameter("CoutaAsocodro", dtInfo.Rows[0]["CuotaAfilAsocoldro"].ToString().Trim());
            parameters[156] = new Microsoft.Reporting.WebForms.ReportParameter("Total", dtInfo.Rows[0]["Total"].ToString().Trim());
            parameters[157] = new Microsoft.Reporting.WebForms.ReportParameter("FechaEntregaForm", dtInfo.Rows[0]["FechaEntregaForm"].ToString().Trim());
            #endregion

            #region Info Sometimiento 2
            dtInfo = cKnowClient.InfoFormSometimiento(IdConocimientoCliente);
            parameters[151] = new Microsoft.Reporting.WebForms.ReportParameter("AportesXPagar", dtInfo.Rows[0]["AportesSoc"].ToString().Trim());
            parameters[152] = new Microsoft.Reporting.WebForms.ReportParameter("VlrCtaCopicredito", dtInfo.Rows[0]["VlrAperturaCopicredito"].ToString().Trim());
            parameters[153] = new Microsoft.Reporting.WebForms.ReportParameter("CompromisoMensual", dtInfo.Rows[0]["CompromisoAhorroMen"].ToString().Trim());
            parameters[154] = new Microsoft.Reporting.WebForms.ReportParameter("CuotaAdmision", dtInfo.Rows[0]["CuotaAdm"].ToString().Trim());
            parameters[155] = new Microsoft.Reporting.WebForms.ReportParameter("CoutaAsocodro", dtInfo.Rows[0]["CuotaAfilAsocoldro"].ToString().Trim());
            parameters[156] = new Microsoft.Reporting.WebForms.ReportParameter("Total", dtInfo.Rows[0]["Total"].ToString().Trim());
            parameters[157] = new Microsoft.Reporting.WebForms.ReportParameter("FechaEntregaForm", dtInfo.Rows[0]["FechaEntregaForm"].ToString().Trim());
            #endregion

            #region Info Copidrogas
            dtInfo = cKnowClient.InfoFormCopidrogas(IdConocimientoCliente);
            parameters[158] = new Microsoft.Reporting.WebForms.ReportParameter("ActaNoAdmision", dtInfo.Rows[0]["ActaNoAdmision"].ToString().Trim());
            parameters[159] = new Microsoft.Reporting.WebForms.ReportParameter("FechaAdmision", dtInfo.Rows[0]["FechaAdmision"].ToString().Trim());
            parameters[160] = new Microsoft.Reporting.WebForms.ReportParameter("FavorableAdmision", dtInfo.Rows[0]["FavorableAdmision"].ToString().Trim());
            parameters[161] = new Microsoft.Reporting.WebForms.ReportParameter("DesfavorableAdmision", dtInfo.Rows[0]["DesfavorableAdmision"].ToString().Trim());
            parameters[162] = new Microsoft.Reporting.WebForms.ReportParameter("AplazadoAdmision", dtInfo.Rows[0]["AplazadoAdmision"].ToString().Trim());
            parameters[163] = new Microsoft.Reporting.WebForms.ReportParameter("VisitaAdmision", dtInfo.Rows[0]["VisitaEfectuadaAdmision"].ToString().Trim());
            parameters[164] = new Microsoft.Reporting.WebForms.ReportParameter("ObsAdmision", dtInfo.Rows[0]["ObservacionesAdmision"].ToString().Trim());
            parameters[165] = new Microsoft.Reporting.WebForms.ReportParameter("ActaNoAdmon", dtInfo.Rows[0]["ActaNoAdmon"].ToString().Trim());
            parameters[166] = new Microsoft.Reporting.WebForms.ReportParameter("FavorableAdmon", dtInfo.Rows[0]["FavorableAdmon"].ToString().Trim());
            parameters[167] = new Microsoft.Reporting.WebForms.ReportParameter("AplazadoAdmon", dtInfo.Rows[0]["AplazadoAdmon"].ToString().Trim());
            parameters[168] = new Microsoft.Reporting.WebForms.ReportParameter("DesfavorableAdmon", dtInfo.Rows[0]["DesfavorableAdmon"].ToString().Trim());
            parameters[169] = new Microsoft.Reporting.WebForms.ReportParameter("VisitaAdmon", dtInfo.Rows[0]["VisitaEfectuadaAdmon"].ToString().Trim());
            parameters[170] = new Microsoft.Reporting.WebForms.ReportParameter("ObsAdmon", dtInfo.Rows[0]["ObservacionesAdmon"].ToString().Trim());
            parameters[171] = new Microsoft.Reporting.WebForms.ReportParameter("FechaAdmon", dtInfo.Rows[0]["FechaAdmon"].ToString().Trim());
            parameters[172] = new Microsoft.Reporting.WebForms.ReportParameter("NombreCoordAdmision", dtInfo.Rows[0]["NombreCoordinadorAdmision"].ToString().Trim());
            parameters[173] = new Microsoft.Reporting.WebForms.ReportParameter("NombreSecretarioAdmision", dtInfo.Rows[0]["NombreSecretarioAdmision"].ToString().Trim());
            parameters[174] = new Microsoft.Reporting.WebForms.ReportParameter("NombreCoordAdmon", dtInfo.Rows[0]["NombreCoordinadorAdmon"].ToString().Trim());
            parameters[175] = new Microsoft.Reporting.WebForms.ReportParameter("NombreSecretarioAdmon", dtInfo.Rows[0]["NombreSecretarioAdmon"].ToString().Trim());
            #endregion

            #region Info Entrevista
            dtInfo = cKnowClient.InfoFormEntrevista(IdConocimientoCliente);
            parameters[109] = new Microsoft.Reporting.WebForms.ReportParameter("FechaVerificacion1", dtInfo.Rows[0]["FechaEntrevista"].ToString());
            parameters[110] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones1", dtInfo.Rows[0]["Observaciones1"].ToString());
            parameters[111] = new Microsoft.Reporting.WebForms.ReportParameter("NombreCargo1", dtInfo.Rows[0]["NombreResponsable"].ToString());
            parameters[112] = new Microsoft.Reporting.WebForms.ReportParameter("FechaVerificacion2", dtInfo.Rows[0]["FechaVerificacion"].ToString());
            parameters[113] = new Microsoft.Reporting.WebForms.ReportParameter("NombreCargo2", dtInfo.Rows[0]["NombreVerifica"].ToString());
            parameters[114] = new Microsoft.Reporting.WebForms.ReportParameter("Observaciones2", dtInfo.Rows[0]["Observaciones2"].ToString());
            parameters[115] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaFirma", dtInfo.Rows[0]["ValidaFirma"].ToString().Trim());
            parameters[116] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaHuella", dtInfo.Rows[0]["ValidaHuella"].ToString().Trim());
            parameters[117] = new Microsoft.Reporting.WebForms.ReportParameter("ValidaEntrevista", dtInfo.Rows[0]["ValidaEntrevista"].ToString().Trim());
            #endregion

            #region Por usar
            //****por usar****//
            //parameters[] = new Microsoft.Reporting.WebForms.ReportParameter("", dtInfo.Rows[0][""].ToString().Trim()); 
            parameters[118] = new Microsoft.Reporting.WebForms.ReportParameter("PJFechaExpedicion", "");
            //********//
            #endregion

            ReportViewer1.LocalReport.SetParameters(parameters);
            ReportViewer1.LocalReport.Refresh();
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }
    }
}
