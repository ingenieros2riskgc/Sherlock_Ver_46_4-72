using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using System.Data;

namespace ListasSarlaft.Classes
{
    public class cKnowClient : cPropiedades
    {
        private cDataBase cDataBase = new cDataBase();
        private cError cError = new cError();
        private OleDbParameter[] parameters;
        private OleDbParameter parameter;

        #region Info Formulario

        //public DataTable InfoFormCliente(int IdConocimientoCliente)
        //{
        //    string strConsulta = string.Empty;
        //    DataTable dtInformacion = new DataTable();

        //    try
        //    {
        //        strConsulta = "SELECT LTRIM(RTRIM(FechaFormulario)) FechaFormulario, LTRIM(RTRIM(ClaseVinculacion)) ClaseVinculacion, " +
        //            "LTRIM(RTRIM(OtraClaseVinculacion)) OtraClaseVinculacion, LTRIM(RTRIM(TomadorAsegurado)) TomadorAsegurado, " +
        //            "LTRIM(RTRIM(OtraTomadorAsegurado)) OtraTomadorAsegurado, LTRIM(RTRIM(TomadorBeneficiario)) TomadorBeneficiario, " +
        //            "LTRIM(RTRIM(OtraTomadorBeneficiario))  OtraTomadorBeneficiario, LTRIM(RTRIM(AseguradoBeneficiario)) AseguradoBeneficiario, " +
        //            "LTRIM(RTRIM(OtraAseguradoBeneficiario)) AS OtraAseguradoBeneficiario " +
        //            "FROM Listas.InfoFormCliente WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")";

        //        cDataBase.conectar();
        //        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    return dtInformacion;
        //}
        public DataTable InfoFormCliente(int IdConocimientoCliente)
        {
            string strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                strConsulta = string.Format("SELECT LTRIM(RTRIM(FechaFormulario)) FechaFormulario, " +
                    "LTRIM(RTRIM(ClaseVinculacion)) ClaseVinculacion, LTRIM(RTRIM(OtraClaseVinculacion)) OtraClaseVinculacion, " +
                    "LTRIM(RTRIM(TomadorAsegurado)) TomadorAsegurado, LTRIM(RTRIM(OtraTomadorAsegurado)) OtraTomadorAsegurado, " +
                    "LTRIM(RTRIM(TomadorBeneficiario)) TomadorBeneficiario, LTRIM(RTRIM(OtraTomadorBeneficiario)) OtraTomadorBeneficiario, " +
                    "LTRIM(RTRIM(AseguradoBeneficiario)) AseguradoBeneficiario, LTRIM(RTRIM(OtraAseguradoBeneficiario)) OtraAseguradoBeneficiario, " +
                    "LTRIM(RTRIM(TipoCliente)) TipoCliente " +
                    "FROM Listas.InfoFormCliente WHERE (IdConocimientoCliente = {0})", IdConocimientoCliente);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        //public DataTable InfoFormPN(int IdConocimientoCliente)
        //{
        //    string strConsulta = string.Empty;
        //    DataTable dtInformacion = new DataTable();

        //    try
        //    {
        //        strConsulta = string.Format("SELECT LTRIM(RTRIM(PNPrimerApellido)) PNPrimerApellido, LTRIM(RTRIM(PNSegunApellido)) PNSegunApellido, LTRIM(RTRIM(PNNombres)) PNNombres, " +
        //            "LTRIM(RTRIM(PNTipoDocumento)) PNTipoDocumento, LTRIM(RTRIM(PNNumeroDocumento)) PNNumeroDocumento, LTRIM(RTRIM(PNFechaExpedicion)) PNFechaExpedicion, " +
        //            "LTRIM(RTRIM(PNLugar)) PNLugar, LTRIM(RTRIM(PNFechaNacimiento)) PNFechaNacimiento, LTRIM(RTRIM(PNNacionalidad)) PNNacionalidad, " +
        //            "LTRIM(RTRIM(PNOcupacionOficio)) PNOcupacionOficio, LTRIM(RTRIM(PNProfesion)) PNProfesion, LTRIM(RTRIM(PNEstadoOcupacion)) PNEstadoOcupacion, " +
        //            "LTRIM(RTRIM(PNActividadEconomica)) PNActividadEconomica, LTRIM(RTRIM(PNCIIU)) PNCIIU, LTRIM(RTRIM(PNEmpresaTrabajo)) PNEmpresaTrabajo, " +
        //            "LTRIM(RTRIM(PNArea)) PNArea, LTRIM(RTRIM(PNCargo)) PNCargo, LTRIM(RTRIM(PNCiudad1)) PNCiudad1, LTRIM(RTRIM(PNDireccion)) PNDireccion, " +
        //            "LTRIM(RTRIM(PNTelefono1)) PNTelefono1, LTRIM(RTRIM(PNFax)) PNFax, LTRIM(RTRIM(PNDireccionResidencia)) PNDireccionResidencia, " +
        //            "LTRIM(RTRIM(PNCiudad2)) PNCiudad2, LTRIM(RTRIM(PNTelefono2)) PNTelefono2, LTRIM(RTRIM(PNCelular)) PNCelular, LTRIM(RTRIM(PNPregunta1)) PNPregunta1, " +
        //            "LTRIM(RTRIM(PNEspecificacionPreguntas)) PNPregunta2, LTRIM(RTRIM(PNPregunta3)) PNPregunta3, LTRIM(RTRIM(PNEspecificacionPreguntas)) PNEspecificacionPreguntas, " +
        //            "LTRIM(RTRIM(PNIngresosMensuales)) PNIngresosMensuales, LTRIM(RTRIM(PNActivos)) PNActivos, LTRIM(RTRIM(PNEgresoMensuales)) PNEgresoMensuales, " +
        //            "LTRIM(RTRIM(PNPasivos)) PNPasivos, LTRIM(RTRIM(PNOtrosIngresos)) PNOtrosIngresos, LTRIM(RTRIM(PNConceptoOtrosIngresos)) PNConceptoOtrosIngresos, " +
        //            "PNSexo, PNCorreoElectronico, ISNULL(PNNacionalidad2,'') PNNacionalidad2, ISNULL(PNOtraActEconomica,'') PNOtraActEconomica, PNEstadoCivil " +
        //            "FROM Listas.InfoFormPN WHERE (IdConocimientoCliente = {0})", IdConocimientoCliente);

        //        cDataBase.conectar();
        //        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    return dtInformacion;
        //}
        public DataTable InfoFormPN(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;

                SqlCommand comando = new SqlCommand("SELECT [IdConocimientoCliente]      ,[PNPrimerApellido]      ,[PNSegunApellido]      ,[PNNombres]      ,[PNTipoDocumento]      ,[PNNumeroDocumento]      ,[PNFechaExpedicion]      ,[PNLugar]      ,[PNFechaNacimiento]      ,[PNNacionalidad]      ,[PNOcupacionOficio]      ,[PNProfesion]      ,[PNEstadoOcupacion]      ,[PNActividadEconomica]      ,[PNCIIU]      ,[PNEmpresaTrabajo]      ,[PNArea]      ,[PNCargo]      ,[PNCiudad1]      ,[PNDireccion]      ,[PNTelefono1]      ,[PNFax]      ,[PNDireccionResidencia]      ,[PNCiudad2]      ,[PNTelefono2]      ,[PNCelular]      ,[PNPregunta1]      ,[PNPregunta2]      ,[PNPregunta3]      ,[PNEspecificacionPreguntas]      ,[PNIngresosMensuales]      ,[PNActivos]      ,[PNEgresoMensuales]      ,[PNPasivos]      ,[PNOtrosIngresos]      ,[PNConceptoOtrosIngresos]      ,[PNSexo]      ,[PNCorreoElectronico]      ,[PNLugarNmto]      ,[PNEstadoCivil]      ,[PNDpto]      ,[PNBarrio]      ,[PNEstrato]      ,[PNTipoInmueble]      ,[PNTipoVivienda]      ,[PNOtroTipoInmueble]      ,[PNOtroTipoVivienda]      ,[PNViviendaPropia]      ,[PNCreditoHipo]      ,[PNVlrCuotaMen]      ,[PNEntidadFinanc]      ,[PNNacionalidad2]      ,[PNOtraActEconomica]      ,[PNPatrimonio]      ,[PNDireccionEmpresa]      ,[PNCiudadEmpresa]      ,[PNTelefonoEmpresa]      ,[PNOtraCIIU]      ,[PNPregunta4]      ,[PNPregunta5]      ,[PNEspecificacionPreguntas2]      ,[PNSector1]      ,[PNSector2]      ,[PNDptoEmpresa]      ,[PNDpto2]      ,[PNServicio]      ,[PNCIIUDescripcion]      ,TA.Nombre as PNTipoActividad      ,[PNTipoActividadOtra]      ,[PNpePrimerApellido]      ,[PNpeSegundoApellido]      ,[PNpeNombres]      ,[PNpeOcupacion]      ,[PNpeCargo] FROM Listas.vwInfoFormPN as a left outer join Parametrizacion.TipoActividad as TA on a.PNTipoActividad = TA.IdTipoActividad WHERE a.IdConocimientoCliente = @IdConocimientoCliente", sqlConnection);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);

                using (SqlDataAdapter adapater = new SqlDataAdapter())
                {
                    adapater.SelectCommand = comando;
                    adapater.Fill(dtInformacion);
                }
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return dtInformacion;
        }

        public string DescripcionCIIU_PN(string CodigoCIIUD)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT CIIU,Descripcion  FROM Parametrizacion.CIIU  where CIIU = '" + CodigoCIIUD + "'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion.Rows[0]["Descripcion"].ToString();
        }

        //public DataTable InfoFormPJ(int IdConocimientoCliente)
        //{
        //    string strConsulta = string.Empty;
        //    DataTable dtInformacion = new DataTable();

        //    try
        //    {
        //        strConsulta = string.Format("SELECT LTRIM(RTRIM(PJRazonDenominacion)) PJRazonDenominacion, LTRIM(RTRIM(PJNIT)) PJNIT, " +
        //            "LTRIM(RTRIM(PJPrimerApellido)) PJPrimerApellido, LTRIM(RTRIM(PJSegundoApellido)) PJSegundoApellido, LTRIM(RTRIM(PJNombres)) PJNombres, " +
        //            "LTRIM(RTRIM(PJTipoDocumento)) PJTipoDocumento, LTRIM(RTRIM(PJNumeroDocumento)) PJNumeroDocumento, LTRIM(RTRIM(PJLugarExpedicion)) PJLugarExpedicion, " +
        //            "LTRIM(RTRIM(PJDireccionOficina)) PJDireccionOficina, LTRIM(RTRIM(PJCiudad1)) PJCiudad1, LTRIM(RTRIM(PJTelefono1)) PJTelefono1, " +
        //            "LTRIM(RTRIM(PJFax1)) PJFax1, LTRIM(RTRIM(PJDireccionSucursal)) PJDireccionSucursal, LTRIM(RTRIM(PJCiudad2)) PJCiudad2, " +
        //            "LTRIM(RTRIM(PJTelefono2)) PJTelefono2, LTRIM(RTRIM(PJFax2)) PJFax2, LTRIM(RTRIM(PJTipoEmpresa)) PJTipoEmpresa, " +
        //            "LTRIM(RTRIM(PJActividadEconomica)) PJActividadEconomica, LTRIM(RTRIM(PJOtraActividadEconomica)) PJOtraActividadEconomica, " +
        //            "LTRIM(RTRIM(PJCIIU)) PJCIIU, " +
        //            "LTRIM(RTRIM(PJNombreAS1)) PJNombreAS1, LTRIM(RTRIM(PJTipoIdentificacionAS1)) PJTipoIdentificacionAS1, LTRIM(RTRIM(PJNumeroDocumentoAS1)) PJNumeroDocumentoAS1, " +
        //            "LTRIM(RTRIM(PJNombreAS2)) PJNombreAS2, LTRIM(RTRIM(PJTipoIdentificacionAS2)) PJTipoIdentificacionAS2, LTRIM(RTRIM(PJNumeroDocumentoAS2)) PJNumeroDocumentoAS2, " +
        //            "LTRIM(RTRIM(PJNombreAS3)) PJNombreAS3, LTRIM(RTRIM(PJTipoIdentificacionAS3)) PJTipoIdentificacionAS3, LTRIM(RTRIM(PJNumeroDocumentoAS3)) PJNumeroDocumentoAS3, " +
        //            "LTRIM(RTRIM(PJNombreAS4)) PJNombreAS4, LTRIM(RTRIM(PJTipoIdentificacionAS4)) PJTipoIdentificacionAS4, LTRIM(RTRIM(PJNumeroDocumentoAS4)) PJNumeroDocumentoAS4, " +
        //            "LTRIM(RTRIM(PJNombreAS5)) PJNombreAS5, LTRIM(RTRIM(PJTipoIdentificacionAS5)) PJTipoIdentificacionAS5, LTRIM(RTRIM(PJNumeroDocumentoAS5)) PJNumeroDocumentoAS5, " +
        //            "LTRIM(RTRIM(PJIngresosMensuales)) PJIngresosMensuales, LTRIM(RTRIM(PJActivos)) PJActivos, " +
        //            "LTRIM(RTRIM(PJEgresoMensuales)) PJEgresoMensuales, LTRIM(RTRIM(PJPasivos)) PJPasivos, " +
        //            "LTRIM(RTRIM(PJOtrosIngresos)) PJOtrosIngresos, LTRIM(RTRIM(PJConceptoOtrosIngresos)) PJConceptoOtrosIngresos, " +
        //            "PJSexoRepLegal, PJCorreoPrincipal, PJCorreoSucursal, PJTipoSociedad, PJOtroTipoSociedad, PJOtroTipoDoc, PJNacionalidad1, PJNacionalidad2, PJPagWeb, " +
        //            "PJDescObjSocial, PJConsecAS1, PJConsecAS2, PJConsecAS3, PJConsecAS4 " +
        //            "FROM Listas.InfoFormPJ WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");

        //        cDataBase.conectar();
        //        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    return dtInformacion;
        //}
        public DataTable InfoFormPJ(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;

                SqlCommand comando = new SqlCommand("SELECT * FROM Listas.vwInfoFormPJ WHERE IdConocimientoCliente = @IdConocimientoCliente", sqlConnection);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);

                using (SqlDataAdapter adapater = new SqlDataAdapter())
                {
                    adapater.SelectCommand = comando;
                    adapater.Fill(dtInformacion);
                }
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return dtInformacion;
        }

        public void InfoFormPJ(int IdConocimientoCliente,
            string PJRazonDenominacion, string PJNIT, string PJPrimerApellido, string PJSegundoApellido, string PJNombres, string PJTipoDocumento, string PJNumeroDocumento, string PJLugarExpedicion, string PJFechaExpedicion,
            string PJDireccionOficina, string PJCiudad1, string PJTelefono1, string PJDireccionSucursal, string PJCiudad2, string PJTelefono2, string PJTipoEmpresa, string PJActividadEconomica, string PJCIIU,
            string PJIngresosMensuales, string PJActivos, string PJEgresoMensuales, string PJPasivos, string PJOtrosIngresos, string PJConceptoOtrosIngresos, string PJCorreoPrincipal, string PJFechaConstitucion,
            string PJNombresRepLegalPpal, string PJTipoDocRepLegalPpal, string PJDocumentoRepLegalPpal, string PJNombresRepLegal1, string PJTipoDocRepLegal1, string PJDocumentoRepLegal1, string PJNombresRepLegal2,
            string PJTipoDocRepLegal2, string PJDocumentoRepLegal2, string PJNombresRepLegal3, string PJTipoDocRepLegal3, string PJDocumentoRepLegal3, string PJNombresRepLegal4, string PJTipoDocRepLegal4, string PJDocumentoRepLegal4,
            string PJCodCIIU2, string PJNacionalidad1, string PJDV, string PJDpto, string PJDpto2, string PJPregunta1, string PJPregunta2, string PJPregunta3, string PJEspecificacionPreguntas, string PJPregunta4, string PJPregunta5,
            string PJPregunta6, string PJPregunta7, string PJPregunta8, string PJPregunta9, string PJPregunta10, string PJPreguntaRep1Legal1, string PJPreguntaRep1Legal2, string PJPreguntaRep1Legal3,
            string PJPreguntaRep1Legal4, string PJPreguntaRep2Legal1, string PJPreguntaRep2Legal2, string PJPreguntaRep2Legal3, string PJPreguntaRep2Legal4, string PJPreguntaRep3Legal1, string PJPreguntaRep3Legal2,
            string PJPreguntaRep3Legal3, string PJPreguntaRep3Legal4, string PJPreguntaRep4Legal1, string PJPreguntaRep4Legal2, string PJPreguntaRep4Legal3, string PJPreguntaRep4Legal4,
            string PJPreguntaRepPpalLegal1, string PJPreguntaRepPpalLegal2, string PJPreguntaRepPpalLegal3, string PJPreguntaRepPpalLegal4, string PJNIT1,string PJPais1,string PJNIT2,string PJPais2,
            string PJNIT3,string PJPais3,string PJNIT4,string PJPais4,string PJDireccionFiscal1,string PJDireccionFiscal2,
            string PJParticipacion1,string PJParticipacion2,string PJParticipacion3,string PJParticipacion4,string PJParticipacion5,
            string PJCotizaBolsa,string PJEstatal,string PJSinAnimoLucro,string PJSubsidiaria,string PJSocMatriz,string PJSocMatrizIdenTrib,string PJSocMatrizJurisdiccion,string PJSocMatrizDireccion,string PJSocMatrizCiudad,string PJSocMatrizTelefono,
            string PJLugarNmto, string PJTipoDocumentoEmpresa, string PJpePrimerApellido, string PJpeSegundoApellido, string PJpeNombres, string PJpeOcupacion, string PJpeCargo
            )
        {
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormPJ", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@PJRazonDenominacion", PJRazonDenominacion);
                comando.Parameters.AddWithValue("@PJNIT", PJNIT);
                comando.Parameters.AddWithValue("@PJPrimerApellido", PJPrimerApellido);
                comando.Parameters.AddWithValue("@PJSegundoApellido", PJSegundoApellido);
                comando.Parameters.AddWithValue("@PJNombres", PJNombres);
                comando.Parameters.AddWithValue("@PJTipoDocumento", PJTipoDocumento);
                comando.Parameters.AddWithValue("@PJNumeroDocumento", PJNumeroDocumento);
                comando.Parameters.AddWithValue("@PJLugarExpedicion", PJLugarExpedicion);
                comando.Parameters.AddWithValue("@PJFechaExpedicion", PJFechaExpedicion);
                comando.Parameters.AddWithValue("@PJDireccionOficina", PJDireccionOficina);
                comando.Parameters.AddWithValue("@PJCiudad1", PJCiudad1);
                comando.Parameters.AddWithValue("@PJTelefono1", PJTelefono1);
                comando.Parameters.AddWithValue("@PJDireccionSucursal", PJDireccionSucursal);
                comando.Parameters.AddWithValue("@PJCiudad2", PJCiudad2);
                comando.Parameters.AddWithValue("@PJTelefono2", PJTelefono2);
                comando.Parameters.AddWithValue("@PJTipoEmpresa", PJTipoEmpresa);
                comando.Parameters.AddWithValue("@PJActividadEconomica", PJActividadEconomica);
                comando.Parameters.AddWithValue("@PJCIIU", PJCIIU);
                comando.Parameters.AddWithValue("@PJIngresosMensuales", PJIngresosMensuales);
                comando.Parameters.AddWithValue("@PJActivos", PJActivos);
                comando.Parameters.AddWithValue("@PJEgresoMensuales", PJEgresoMensuales);
                comando.Parameters.AddWithValue("@PJPasivos", PJPasivos);
                comando.Parameters.AddWithValue("@PJOtrosIngresos", PJOtrosIngresos);
                comando.Parameters.AddWithValue("@PJConceptoOtrosIngresos", PJConceptoOtrosIngresos);
                comando.Parameters.AddWithValue("@PJCorreoPrincipal", PJCorreoPrincipal);
                comando.Parameters.AddWithValue("@PJFechaConstitucion", PJFechaConstitucion);
                comando.Parameters.AddWithValue("@PJNombresRepLegalPpal", PJNombresRepLegalPpal);
                comando.Parameters.AddWithValue("@PJTipoDocRepLegalPpal", PJTipoDocRepLegalPpal);
                comando.Parameters.AddWithValue("@PJDocumentoRepLegalPpal", PJDocumentoRepLegalPpal);
                comando.Parameters.AddWithValue("@PJNombresRepLegal1", PJNombresRepLegal1);
                comando.Parameters.AddWithValue("@PJTipoDocRepLegal1", PJTipoDocRepLegal1);
                comando.Parameters.AddWithValue("@PJDocumentoRepLegal1", PJDocumentoRepLegal1);
                comando.Parameters.AddWithValue("@PJNombresRepLegal2", PJNombresRepLegal2);
                comando.Parameters.AddWithValue("@PJTipoDocRepLegal2", PJTipoDocRepLegal2);
                comando.Parameters.AddWithValue("@PJDocumentoRepLegal2", PJDocumentoRepLegal2);
                comando.Parameters.AddWithValue("@PJNombresRepLegal3", PJNombresRepLegal3);
                comando.Parameters.AddWithValue("@PJTipoDocRepLegal3", PJTipoDocRepLegal3);
                comando.Parameters.AddWithValue("@PJDocumentoRepLegal3", PJDocumentoRepLegal3);
                comando.Parameters.AddWithValue("@PJNombresRepLegal4", PJNombresRepLegal4);
                comando.Parameters.AddWithValue("@PJTipoDocRepLegal4", PJTipoDocRepLegal4);
                comando.Parameters.AddWithValue("@PJDocumentoRepLegal4", PJDocumentoRepLegal4);
                comando.Parameters.AddWithValue("@PJCodCIIU2", PJCodCIIU2);
                comando.Parameters.AddWithValue("@PJNacionalidad1", PJNacionalidad1);
                comando.Parameters.AddWithValue("@PJDV", PJDV);
                comando.Parameters.AddWithValue("@PJDpto", PJDpto);
                comando.Parameters.AddWithValue("@PJDpto2", PJDpto2);
                comando.Parameters.AddWithValue("@PJPregunta1", PJPregunta1);
                comando.Parameters.AddWithValue("@PJPregunta2", PJPregunta2);
                comando.Parameters.AddWithValue("@PJPregunta3", PJPregunta3);
                comando.Parameters.AddWithValue("@PJEspecificacionPreguntas", PJEspecificacionPreguntas);
                comando.Parameters.AddWithValue("@PJPregunta4", PJPregunta4);
                comando.Parameters.AddWithValue("@PJPregunta5", PJPregunta5);
                comando.Parameters.AddWithValue("@PJPregunta6", PJPregunta6);
                comando.Parameters.AddWithValue("@PJPregunta7", PJPregunta7);
                comando.Parameters.AddWithValue("@PJPregunta8", PJPregunta8);
                comando.Parameters.AddWithValue("@PJPregunta9", PJPregunta9);
                comando.Parameters.AddWithValue("@PJPregunta10", PJPregunta10);
                comando.Parameters.AddWithValue("@PJPreguntaRep1Legal1", PJPreguntaRep1Legal1);
                comando.Parameters.AddWithValue("@PJPreguntaRep1Legal2", PJPreguntaRep1Legal2);
                comando.Parameters.AddWithValue("@PJPreguntaRep1Legal3", PJPreguntaRep1Legal3);
                comando.Parameters.AddWithValue("@PJPreguntaRep1Legal4", PJPreguntaRep1Legal4);
                comando.Parameters.AddWithValue("@PJPreguntaRep2Legal1", PJPreguntaRep2Legal1);
                comando.Parameters.AddWithValue("@PJPreguntaRep2Legal2", PJPreguntaRep2Legal2);
                comando.Parameters.AddWithValue("@PJPreguntaRep2Legal3", PJPreguntaRep2Legal3);
                comando.Parameters.AddWithValue("@PJPreguntaRep2Legal4", PJPreguntaRep2Legal4);
                comando.Parameters.AddWithValue("@PJPreguntaRep3Legal1", PJPreguntaRep3Legal1);
                comando.Parameters.AddWithValue("@PJPreguntaRep3Legal2", PJPreguntaRep3Legal2);
                comando.Parameters.AddWithValue("@PJPreguntaRep3Legal3", PJPreguntaRep3Legal3);
                comando.Parameters.AddWithValue("@PJPreguntaRep3Legal4", PJPreguntaRep3Legal4);
                comando.Parameters.AddWithValue("@PJPreguntaRep4Legal1", PJPreguntaRep4Legal1);
                comando.Parameters.AddWithValue("@PJPreguntaRep4Legal2", PJPreguntaRep4Legal2);
                comando.Parameters.AddWithValue("@PJPreguntaRep4Legal3", PJPreguntaRep4Legal3);
                comando.Parameters.AddWithValue("@PJPreguntaRep4Legal4", PJPreguntaRep4Legal4);
                comando.Parameters.AddWithValue("@PJPreguntaRepPpalLegal1", PJPreguntaRepPpalLegal1);
                comando.Parameters.AddWithValue("@PJPreguntaRepPpalLegal2", PJPreguntaRepPpalLegal2);
                comando.Parameters.AddWithValue("@PJPreguntaRepPpalLegal3", PJPreguntaRepPpalLegal3);
                comando.Parameters.AddWithValue("@PJPreguntaRepPpalLegal4", PJPreguntaRepPpalLegal4);
                comando.Parameters.AddWithValue("@PJNIT1", PJNIT1);
                comando.Parameters.AddWithValue("@PJPais1", PJPais1);
                comando.Parameters.AddWithValue("@PJNIT2", PJNIT2);
                comando.Parameters.AddWithValue("@PJPais2", PJPais2);
                comando.Parameters.AddWithValue("@PJNIT3", PJNIT3);
                comando.Parameters.AddWithValue("@PJPais3", PJPais3);
                comando.Parameters.AddWithValue("@PJNIT4", PJNIT4);
                comando.Parameters.AddWithValue("@PJPais4", PJPais4);
                comando.Parameters.AddWithValue("@PJDireccionFiscal1", PJDireccionFiscal1);
                comando.Parameters.AddWithValue("@PJDireccionFiscal2", PJDireccionFiscal2);
                comando.Parameters.AddWithValue("@PJParticipacion1", PJParticipacion1);
                comando.Parameters.AddWithValue("@PJParticipacion2", PJParticipacion2);
                comando.Parameters.AddWithValue("@PJParticipacion3", PJParticipacion3);
                comando.Parameters.AddWithValue("@PJParticipacion4", PJParticipacion4);
                comando.Parameters.AddWithValue("@PJParticipacion5", PJParticipacion5);
                comando.Parameters.AddWithValue("@PJCotizaBolsa", PJCotizaBolsa);
                comando.Parameters.AddWithValue("@PJEstatal", PJEstatal);
                comando.Parameters.AddWithValue("@PJSinAnimoLucro", PJSinAnimoLucro);
                comando.Parameters.AddWithValue("@PJSubsidiaria", PJSubsidiaria);
                comando.Parameters.AddWithValue("@PJSocMatriz", PJSocMatriz);
                comando.Parameters.AddWithValue("@PJSocMatrizIdenTrib", PJSocMatrizIdenTrib);
                comando.Parameters.AddWithValue("@PJSocMatrizJurisdiccion", PJSocMatrizJurisdiccion);
                comando.Parameters.AddWithValue("@PJSocMatrizDireccion", PJSocMatrizDireccion);
                comando.Parameters.AddWithValue("@PJSocMatrizCiudad", PJSocMatrizCiudad);
                comando.Parameters.AddWithValue("@PJSocMatrizTelefono", PJSocMatrizTelefono);
                comando.Parameters.AddWithValue("@PJLugarNmto", PJLugarNmto);
                comando.Parameters.AddWithValue("@PJTipoDocumentoEmpresa", PJTipoDocumentoEmpresa);
                comando.Parameters.AddWithValue("@PJpePrimerApellido", PJpePrimerApellido);
                comando.Parameters.AddWithValue("@PJpeSegundoApellido", PJpeSegundoApellido);
                comando.Parameters.AddWithValue("@PJpeNombres", PJpeNombres);
                comando.Parameters.AddWithValue("@PJpeOcupacion", PJpeOcupacion);
                comando.Parameters.AddWithValue("@PJpeCargo", PJpeCargo);

                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //public DataTable InfoFormPF(int IdConocimientoCliente)
        //{
        //    string strConsulta = string.Empty;
        //    DataTable dtInformacion = new DataTable();

        //    try
        //    {
        //        strConsulta = string.Format("SELECT LTRIM(RTRIM(TransacMonedaExtra)) TransacMonedaExtra, LTRIM(RTRIM(TipoTransaccion)) TipoTransaccion, " +
        //            "LTRIM(RTRIM(OtroTipoTransaccion)) OtroTipoTransaccion, LTRIM(RTRIM(PFTipoProducto1)) PFTipoProducto1, LTRIM(RTRIM(PFNumeroProducto1)) PFNumeroProducto1, " +
        //            "LTRIM(RTRIM(PFEntidad1)) PFEntidad1, LTRIM(RTRIM(PFMonto1)) PFMonto1, LTRIM(RTRIM(PFCiudad1)) PFCiudad1, LTRIM(RTRIM(PFPais1)) PFPais1, " +
        //            "LTRIM(RTRIM(PFMoneda1)) PFMoneda1, LTRIM(RTRIM(PFTipoProducto2)) PFTipoProducto2, LTRIM(RTRIM(PFNumeroProducto2)) PFNumeroProducto2, " +
        //            "LTRIM(RTRIM(PFEntidad2)) PFEntidad2, LTRIM(RTRIM(PFMonto2)) PFMonto2, LTRIM(RTRIM(PFCiudad2)) PFCiudad2, LTRIM(RTRIM(PFPais2)) PFPais2, " +
        //            "LTRIM(RTRIM(PFMoneda2)) PFMoneda2, PFProdExterior, " +
        //            "LTRIM(RTRIM(PFTipoProducto3)) PFTipoProducto3, LTRIM(RTRIM(PFNumeroProducto3)) PFNumeroProducto3, LTRIM(RTRIM(PFEntidad3)) PFEntidad3, " +
        //            "LTRIM(RTRIM(PFMonto3)) PFMonto3, LTRIM(RTRIM(PFCiudad3)) PFCiudad3, LTRIM(RTRIM(PFPais3)) PFPais3, LTRIM(RTRIM(PFMoneda3)) PFMoneda3 " +
        //            "FROM Listas.InfoFormPF WHERE (IdConocimientoCliente = {0})", IdConocimientoCliente);

        //        cDataBase.conectar();
        //        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    return dtInformacion;
        //}
        public DataTable InfoFormPF(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;

                SqlCommand comando = new SqlCommand("SELECT * FROM Listas.vwInfoFormPF WHERE IdConocimientoCliente = @IdConocimientoCliente", sqlConnection);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);

                using (SqlDataAdapter adapater = new SqlDataAdapter())
                {
                    adapater.SelectCommand = comando;
                    adapater.Fill(dtInformacion);
                }
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return dtInformacion;
        }

        public DataTable InfoFormSeguros(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;

                SqlCommand comando = new SqlCommand("SELECT * FROM Listas.vwInfoFormSeguros WHERE IdConocimientoCliente = @IdConocimientoCliente", sqlConnection);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);

                using (SqlDataAdapter adapater = new SqlDataAdapter())
                {
                    adapater.SelectCommand = comando;
                    adapater.Fill(dtInformacion);
                }
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return dtInformacion;
        }

        //public DataTable InfoFormEntrevista(int IdConocimientoCliente)
        //{
        //    string strConsulta = string.Empty;
        //    DataTable dtInformacion = new DataTable();

        //    try
        //    {
        //        strConsulta = string.Format("SELECT LTRIM(RTRIM(LugarEntrevista)) LugarEntrevista, LTRIM(RTRIM(FechaEntrevista)) FechaEntrevista, " +
        //            "LTRIM(RTRIM(HoraEntrevista)) HoraEntrevista, LTRIM(RTRIM(Resultado)) Resultado, LTRIM(RTRIM(Observaciones1)) Observaciones1, " +
        //            "LTRIM(RTRIM(NombreResponsable)) NombreResponsable, LTRIM(RTRIM(FechaVerificacion)) FechaVerificacion, " +
        //            "LTRIM(RTRIM(HoraVerificacion)) HoraVerificacion, LTRIM(RTRIM(NombreVerifica)) NombreVerifica, LTRIM(RTRIM(Observaciones2)) Observaciones2, " +
        //            "LTRIM(RTRIM(CCRespEntrevista)) CCRespEntrevista, LTRIM(RTRIM(CCVerificaEntrevista)) CCVerificaEntrevista " +
        //            "FROM Listas.InfoFormEntrevista WHERE (IdConocimientoCliente = {0})", IdConocimientoCliente);

        //        cDataBase.conectar();
        //        dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    return dtInformacion;
        //}
        public DataTable InfoFormEntrevista(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;

                SqlCommand comando = new SqlCommand("SELECT * FROM Listas.vwInfoFormEntrevista WHERE IdConocimientoCliente = @IdConocimientoCliente", sqlConnection);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);

                using (SqlDataAdapter adapater = new SqlDataAdapter())
                {
                    adapater.SelectCommand = comando;
                    adapater.Fill(dtInformacion);
                }
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return dtInformacion;
        }

        public DataTable InfoFormDocsInu(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Doc1, LTRIM(RTRIM(Doc2)) AS Doc2, LTRIM(RTRIM(Doc3)) AS Doc3, LTRIM(RTRIM(Doc4)) AS Doc4, LTRIM(RTRIM(Doc5)) AS Doc5, LTRIM(RTRIM(Doc6)) AS Doc6, LTRIM(RTRIM(Doc7)) AS Doc7, LTRIM(RTRIM(Doc8)) AS Doc8, LTRIM(RTRIM(UrlDocPdf)) AS UrlDocPdf, LTRIM(RTRIM(Conve1)) AS Conve1, LTRIM(RTRIM(Conve2)) AS Conve2, LTRIM(RTRIM(Conve3)) AS Conve3, LTRIM(RTRIM(Conve4)) AS Conve4, LTRIM(RTRIM(Conve5)) AS Conve5, LTRIM(RTRIM(Conve6)) AS Conve6, LTRIM(RTRIM(Conve7)) AS Conve7, LTRIM(RTRIM(Conve8)) AS Conve8, LTRIM(RTRIM(Doc9)) AS Doc9, LTRIM(RTRIM(Doc10)) AS Doc10, LTRIM(RTRIM(Doc11)) AS Doc11, LTRIM(RTRIM(Doc12)) AS Doc12, LTRIM(RTRIM(Doc13)) AS Doc13, LTRIM(RTRIM(Doc14)) AS Doc14 FROM Listas.InfoFormDocsInu WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        #region Zurich
        public DataTable InfoFormCliente(int IdConocimientoCliente, bool booFoo)
        {
            DataTable dtInformacion = new DataTable();
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;

                SqlCommand comando = new SqlCommand("SELECT * FROM Listas.vwInfoFormCliente WHERE IdConocimientoCliente = @IdConocimientoCliente", sqlConnection);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);

                using (SqlDataAdapter adapater = new SqlDataAdapter())
                {
                    adapater.SelectCommand = comando;
                    adapater.Fill(dtInformacion);
                }
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return dtInformacion;
        }
        #endregion

        public DataTable InfoFormDrogueria(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdInfoFormDrogueria], [IdConocimientoCliente], " +
                    "[NombreDrogueriaPpal], [NitDrogueriaPpal], [DirDrogueriaPpal], [DptoDrogueriaPpal], [CiudadDrogueriaPpal], [BarrioDrogueriaPpal], [TelDrogueriaPpal]," +
                    "[NombreDrogueria1], [NitDrogueria1], [DirDrogueria1], [DptoDrogueria1], [CiudadDrogueria1], [BarrioDrogueria1], [TelDrogueria1]," +
                    "[NombreDrogueria2], [NitDrogueria2], [DirDrogueria2], [DptoDrogueria2], [CiudadDrogueria2], [BarrioDrogueria2], [TelDrogueria2] " +
                    "FROM [Listas].[InfoFormDrogueria] WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable InfoFormSometimiento(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdInfoFormSometimiento], [IdConocimientoCliente], " +
                    "[AportesSoc], [VlrAperturaCopicredito], [CompromisoAhorroMen], [CuotaAdm], [CuotaAfilAsocoldro], [Total], [FechaEntregaForm] " +
                    "FROM [Listas].[InfoFormSometimiento] WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable InfoFormCopidrogas(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdInfoFormCopidrogas],[IdConocimientoCliente]," +
                    "[ActaNoAdmision],[FechaAdmision],[FavorableAdmision],[AplazadoAdmision],[DesfavorableAdmision]," +
                    "[VisitaEfectuadaAdmision],[ObservacionesAdmision],[NombreCoordinadorAdmision],[NombreSecretarioAdmision]," +
                    "[ActaNoAdmon],[FechaAdmon],[FavorableAdmon],[AplazadoAdmon],[DesfavorableAdmon],[VisitaEfectuadaAdmon]," +
                    "[ObservacionesAdmon],[NombreCoordinadorAdmon],[NombreSecretarioAdmon] " +
                    "FROM [Listas].[InfoFormCopidrogas] WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
         #endregion




        #region Info Formulario
        public int agregarConocimientoCliente(String NumeroFecha, String Ano)
        {
            try
            {
                parameters = new OleDbParameter[4];
                parameter = new OleDbParameter("@IdConocimientoCliente", OleDbType.Integer, 18);
                parameter.Direction = ParameterDirection.Output;
                parameters[0] = parameter;
                parameter = new OleDbParameter("@IdUsuario", OleDbType.Integer);
                parameter.Value = IdUsuario;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@NumeroFecha", OleDbType.Integer);
                parameter.Value = NumeroFecha;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@Ano", OleDbType.Integer);
                parameter.Value = Ano;
                parameters[3] = parameter;
                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("agregarConocimientoCliente", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return Convert.ToInt32(parameters[0].Value);
        }

        public void InfoFormCliente(int IdConocimientoCliente, string FechaFormulario,
            string ClaseVinculacion, string OtraClaseVinculacion,
            string TomadorAsegurado, string OtraTomadorAsegurado,
            string TomadorBeneficiario, string OtraTomadorBeneficiario,
            string AseguradoBeneficiario, string OtraAseguradoBeneficiario)
        {
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormCliente", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@FechaFormulario", FechaFormulario);
                comando.Parameters.AddWithValue("@ClaseVinculacion", ClaseVinculacion);
                comando.Parameters.AddWithValue("@OtraClaseVinculacion", OtraClaseVinculacion);
                comando.Parameters.AddWithValue("@TomadorAsegurado", TomadorAsegurado);
                comando.Parameters.AddWithValue("@OtraTomadorAsegurado", OtraTomadorAsegurado);
                comando.Parameters.AddWithValue("@TomadorBeneficiario", TomadorBeneficiario);
                comando.Parameters.AddWithValue("@OtraTomadorBeneficiario", OtraTomadorBeneficiario);
                comando.Parameters.AddWithValue("@AseguradoBeneficiario", AseguradoBeneficiario);
                comando.Parameters.AddWithValue("@OtraAseguradoBeneficiario", OtraAseguradoBeneficiario);
                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void InfoFormCliente(int IdConocimientoCliente, string FechaFormulario, string ClaseVinculacion, string OtraClaseVinculacion,
            string TomadorAsegurado, string OtraTomadorAsegurado, string TomadorBeneficiario, string OtraTomadorBeneficiario, string AseguradoBeneficiario, string OtraAseguradoBeneficiario, string TipoCliente,
            string Ciudad, string Sucursal, string TipoSolicitud
         )
        {
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormCliente", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@FechaFormulario", FechaFormulario);
                comando.Parameters.AddWithValue("@ClaseVinculacion", ClaseVinculacion);
                comando.Parameters.AddWithValue("@OtraClaseVinculacion", OtraClaseVinculacion);
                comando.Parameters.AddWithValue("@TomadorAsegurado", TomadorAsegurado);
                comando.Parameters.AddWithValue("@OtraTomadorAsegurado", OtraTomadorAsegurado);
                comando.Parameters.AddWithValue("@TomadorBeneficiario", TomadorBeneficiario);
                comando.Parameters.AddWithValue("@OtraTomadorBeneficiario", OtraTomadorBeneficiario);
                comando.Parameters.AddWithValue("@AseguradoBeneficiario", AseguradoBeneficiario);
                comando.Parameters.AddWithValue("@OtraAseguradoBeneficiario", OtraAseguradoBeneficiario);
                comando.Parameters.AddWithValue("@TipoCliente", TipoCliente);
                comando.Parameters.AddWithValue("@Ciudad", Ciudad);
                comando.Parameters.AddWithValue("@Sucursal", Sucursal);
                comando.Parameters.AddWithValue("@TipoSolicitud", TipoSolicitud);

                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void InfoFormPN(int IdConocimientoCliente, string PNPrimerApellido, string PNSegunApellido, string PNNombres,
            string PNTipoDocumento, string PNNumeroDocumento, string PNFechaExpedicion, string PNLugar,
            string PNFechaNacimiento, string PNNacionalidad, string PNOcupacionOficio,
            string PNActividadEconomica, string PNEstadoOcupacion, string PNCIIU, string PNEmpresaTrabajo,
            string PNArea, string PNCargo, string PNCiudad1, string PNDireccion, string PNTelefono1, string PNFax,
            string PNDireccionResidencia, string PNCiudad2, string PNTelefono2, string PNCelular,
            string PNPregunta1, string PNPregunta2, string PNPregunta3, string PNEspecificacionPreguntas, string PNIngresosMensuales,
            string PNActivos, string PNEgresoMensuales, string PNPasivos, string PNOtrosIngresos, string PNConceptoOtrosIngresos, string PNSexo, string PNCorreoElectronico
            )
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT Listas.InfoFormPN (IdConocimientoCliente, PNPrimerApellido, PNSegunApellido, PNNombres, PNTipoDocumento, PNNumeroDocumento, PNFechaExpedicion, PNLugar, PNFechaNacimiento, PNNacionalidad, PNOcupacionOficio, PNProfesion,  PNActividadEconomica, PNCIIU, PNEmpresaTrabajo, PNArea, PNCargo, PNCiudad1, PNDireccion, PNTelefono1, PNFax, PNDireccionResidencia, PNCiudad2, PNTelefono2, PNCelular, PNPregunta1, PNPregunta2, PNPregunta3, PNEspecificacionPreguntas, PNIngresosMensuales, PNActivos, PNEgresoMensuales, PNPasivos, PNOtrosIngresos, PNConceptoOtrosIngresos, PNSexo, PNCorreoElectronico) VALUES (" + IdConocimientoCliente + ", N'" + PNPrimerApellido + "', N'" + PNSegunApellido + "', N'" + PNNombres + "', N'" + PNTipoDocumento + "', N'" + PNNumeroDocumento + "', N'" + PNFechaExpedicion + "', N'" + PNLugar + "', N'" + PNFechaNacimiento + "', N'" + PNNacionalidad + "', N'" + PNOcupacionOficio + "', N'" + PNActividadEconomica + "', N'" + PNEstadoOcupacion + "', N'" + PNCIIU + "', N'" + PNEmpresaTrabajo + "', N'" + PNArea + "', N'" + PNCargo + "', N'" + PNCiudad1 + "', N'" + PNDireccion + "', N'" + PNTelefono1 + "', N'" + PNFax + "', N'" + PNDireccionResidencia + "', N'" + PNCiudad2 + "', N'" + PNTelefono2 + "', N'" + PNCelular + "', N'" + PNPregunta1 + "', N'" + PNPregunta2 + "', N'" + PNPregunta3 + "', N'" + PNEspecificacionPreguntas + "', N'" + PNIngresosMensuales + "', N'" + PNActivos + "', N'" + PNEgresoMensuales + "', N'" + PNPasivos + "', N'" + PNOtrosIngresos + "', N'" + PNConceptoOtrosIngresos + "', N'" + PNSexo + "', N'" + PNCorreoElectronico + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InfoFormPJ(int IdConocimientoCliente, String PJRazonDenominacion, String PJNIT, String PJPrimerApellido, String PJSegundoApellido, String PJNombres, String PJTipoDocumento, String PJNumeroDocumento, String PJLugarExpedicion,
                               String PJFechaExpedicion, String PJDireccionOficina, String PJCiudad1, String PJTelefono1, String PJFax1, String PJDireccionSucursal, String PJCiudad2, String PJTelefono2, String PJFax2, String PJTipoEmpresa, String PJActividadEconomica,
                               String PJOtraActividadEconomica, String PJCIIU, String PJNombreAS1, String PJTipoIdentificacionAS1, String PJNumeroDocumentoAS1, String PJNombreAS2, String PJTipoIdentificacionAS2, String PJNumeroDocumentoAS2, String PJNombreAS3,
                               String PJTipoIdentificacionAS3, String PJNumeroDocumentoAS3, String PJNombreAS4, String PJTipoIdentificacionAS4, String PJNumeroDocumentoAS4, String PJNombreAS5, String PJTipoIdentificacionAS5, String PJNumeroDocumentoAS5,
                               String PJIngresosMensuales, String PJActivos, String PJEgresoMensuales, String PJPasivos, String PJOtrosIngresos, String PJConceptoOtrosIngresos, String PJSexoRepLegal, string PJCorreoPrincipal, string PJCorreoSucursal)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT Listas.InfoFormPJ (IdConocimientoCliente, PJRazonDenominacion, PJNIT, PJPrimerApellido, PJSegundoApellido, PJNombres, PJTipoDocumento, PJNumeroDocumento, PJLugarExpedicion, PJFechaExpedicion, PJDireccionOficina, PJCiudad1, PJTelefono1, PJFax1, PJDireccionSucursal, PJCiudad2, PJTelefono2, PJFax2, PJTipoEmpresa, PJActividadEconomica, PJOtraActividadEconomica, PJCIIU, PJNombreAS1, PJTipoIdentificacionAS1, PJNumeroDocumentoAS1, PJNombreAS2, PJTipoIdentificacionAS2, PJNumeroDocumentoAS2, PJNombreAS3, PJTipoIdentificacionAS3, PJNumeroDocumentoAS3, PJNombreAS4, PJTipoIdentificacionAS4, PJNumeroDocumentoAS4, PJNombreAS5, PJTipoIdentificacionAS5, PJNumeroDocumentoAS5, PJIngresosMensuales, PJActivos, PJEgresoMensuales, PJPasivos, PJOtrosIngresos, PJConceptoOtrosIngresos, PJSexoRepLegal, PJCorreoPrincipal, PJCorreoSucursal) VALUES (" + IdConocimientoCliente + ", N'" + PJRazonDenominacion + "', N'" + PJNIT + "', N'" + PJPrimerApellido + "', N'" + PJSegundoApellido + "', N'" + PJNombres + "', N'" + PJTipoDocumento + "', N'" + PJNumeroDocumento + "', N'" + PJLugarExpedicion + "', N'" + PJFechaExpedicion + "', N'" + PJDireccionOficina + "', N'" + PJCiudad1 + "', N'" + PJTelefono1 + "', N'" + PJFax1 + "', N'" + PJDireccionSucursal + "', N'" + PJCiudad2 + "', N'" + PJTelefono2 + "', N'" + PJFax2 + "', N'" + PJTipoEmpresa + "', N'" + PJActividadEconomica + "', N'" + PJOtraActividadEconomica + "', N'" + PJCIIU + "', N'" + PJNombreAS1 + "', N'" + PJTipoIdentificacionAS1 + "', N'" + PJNumeroDocumentoAS1 + "', N'" + PJNombreAS2 + "', N'" + PJTipoIdentificacionAS2 + "', N'" + PJNumeroDocumentoAS2 + "', N'" + PJNombreAS3 + "', N'" + PJTipoIdentificacionAS3 + "', N'" + PJNumeroDocumentoAS3 + "', N'" + PJNombreAS4 + "', N'" + PJTipoIdentificacionAS4 + "', N'" + PJNumeroDocumentoAS4 + "', N'" + PJNombreAS5 + "', N'" + PJTipoIdentificacionAS5 + "', N'" + PJNumeroDocumentoAS5 + "', N'" + PJIngresosMensuales + "', N'" + PJActivos + "', N'" + PJEgresoMensuales + "', N'" + PJPasivos + "', N'" + PJOtrosIngresos + "', N'" + PJConceptoOtrosIngresos + "',N'" + PJSexoRepLegal + "',N'" + PJCorreoPrincipal + "',N'" + PJCorreoSucursal + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InfoFormPF(int IdConocimientoCliente, String TransacMonedaExtra, String TipoTransaccion, String OtroTipoTransaccion, String PFTipoProducto1, String PFNumeroProducto1, String PFEntidad1, String PFMonto1, String PFCiudad1,
                               String PFPais1, String PFMoneda1, String PFTipoProducto2, String PFNumeroProducto2, String PFEntidad2, String PFMonto2, String PFCiudad2, String PFPais2, String PFMoneda2)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT Listas.InfoFormPF (IdConocimientoCliente, TransacMonedaExtra, TipoTransaccion, OtroTipoTransaccion, PFTipoProducto1, PFNumeroProducto1, PFEntidad1, PFMonto1, PFCiudad1, PFPais1, PFMoneda1, PFTipoProducto2, PFNumeroProducto2, PFEntidad2, PFMonto2, PFCiudad2, PFPais2, PFMoneda2) VALUES (" + IdConocimientoCliente + ", N'" + TransacMonedaExtra + "', N'" + TipoTransaccion + "', N'" + OtroTipoTransaccion + "', N'" + PFTipoProducto1 + "', N'" + PFNumeroProducto1 + "', N'" + PFEntidad1 + "', N'" + PFMonto1 + "', N'" + PFCiudad1 + "', N'" + PFPais1 + "', N'" + PFMoneda1 + "', N'" + PFTipoProducto2 + "', N'" + PFNumeroProducto2 + "', N'" + PFEntidad2 + "', N'" + PFMonto2 + "', N'" + PFCiudad2 + "', N'" + PFPais2 + "', N'" + PFMoneda2 + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InfoFormSeguros(int IdConocimientoCliente,
           string SeguroAno1, string SeguroRamo1, string SeguroCompania1, string SeguroValor1, string SeguroTipo1,
           string SeguroAno2, string SeguroRamo2, string SeguroCompania2, string SeguroValor2, string SeguroTipo2, string OrigenFondos)
        {
            string strConsulta = string.Empty;
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormSeguros", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@SeguroAno1", SeguroAno1);
                comando.Parameters.AddWithValue("@SeguroRamo1", SeguroRamo1);
                comando.Parameters.AddWithValue("@SeguroCompania1", SeguroCompania1);
                comando.Parameters.AddWithValue("@SeguroValor1", SeguroValor1);
                comando.Parameters.AddWithValue("@SeguroTipo1", SeguroTipo1);
                comando.Parameters.AddWithValue("@SeguroAno2", SeguroAno2);
                comando.Parameters.AddWithValue("@SeguroRamo2", SeguroRamo2);
                comando.Parameters.AddWithValue("@SeguroCompania2", SeguroCompania2);
                comando.Parameters.AddWithValue("@SeguroValor2", SeguroValor2);
                comando.Parameters.AddWithValue("@SeguroTipo2", SeguroTipo2);
                comando.Parameters.AddWithValue("@OrigenFondos", OrigenFondos);
                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void InfoFormSeguros(int IdConocimientoCliente,
            string SeguroAno1, string SeguroRamo1, string SeguroCompania1, string SeguroValor1, string SeguroTipo1,
            string SeguroAno2, string SeguroRamo2, string SeguroCompania2, string SeguroValor2, string SeguroTipo2, 
            string OrigenFondos, string Reclamaciones
            )
        {
            string strConsulta = string.Empty;
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormSeguros", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@SeguroAno1", SeguroAno1);
                comando.Parameters.AddWithValue("@SeguroRamo1", SeguroRamo1);
                comando.Parameters.AddWithValue("@SeguroCompania1", SeguroCompania1);
                comando.Parameters.AddWithValue("@SeguroValor1", SeguroValor1);
                comando.Parameters.AddWithValue("@SeguroTipo1", SeguroTipo1);
                comando.Parameters.AddWithValue("@SeguroAno2", SeguroAno2);
                comando.Parameters.AddWithValue("@SeguroRamo2", SeguroRamo2);
                comando.Parameters.AddWithValue("@SeguroCompania2", SeguroCompania2);
                comando.Parameters.AddWithValue("@SeguroValor2", SeguroValor2);
                comando.Parameters.AddWithValue("@SeguroTipo2", SeguroTipo2);
                comando.Parameters.AddWithValue("@OrigenFondos", OrigenFondos);
                comando.Parameters.AddWithValue("@Reclamaciones", Reclamaciones);

                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void InfoFormEntrevista(int IdConocimientoCliente, String LugarEntrevista,
            String FechaEntrevista, String HoraEntrevista, String Resultado, String Observaciones1, String NombreResponsable,
            String FechaVerificacion, String HoraVerificacion, String NombreVerifica, String Observaciones2)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT Listas.InfoFormEntrevista (IdConocimientoCliente, LugarEntrevista, FechaEntrevista, HoraEntrevista, Resultado, Observaciones1, NombreResponsable, FechaVerificacion, HoraVerificacion, NombreVerifica, Observaciones2) VALUES (" + IdConocimientoCliente + ", N'" + LugarEntrevista + "', N'" + FechaEntrevista + "', N'" + HoraEntrevista + "', N'" + Resultado + "', N'" + Observaciones1 + "', N'" + NombreResponsable + "', N'" + FechaVerificacion + "', N'" + HoraVerificacion + "', N'" + NombreVerifica + "', N'" + Observaciones2 + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InfoFormDocsInu(int IdConocimientoCliente, String Doc1, String Doc2, String Doc3, String Doc4, String Doc5, String Doc6, String Doc7, String Doc8, String UrlDocPdf, String Conve1, String Conve2, String Conve3, String Conve4, String Conve5, String Conve6, String Conve7, String Conve8, String Doc9, String Doc10, String Doc11, String Doc12, String Doc13, String Doc14)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT Listas.InfoFormDocsInu (IdConocimientoCliente, Doc1, Doc2, Doc3, Doc4, Doc5, Doc6, Doc7, Doc8, UrlDocPdf, Conve1, Conve2, Conve3, Conve4, Conve5, Conve6, Conve7, Conve8, Doc9, Doc10, Doc11, Doc12, Doc13, Doc14) VALUES (" + IdConocimientoCliente + ", N'" + Doc1 + "', N'" + Doc2 + "', N'" + Doc3 + "', N'" + Doc4 + "', N'" + Doc5 + "', N'" + Doc6 + "', N'" + Doc7 + "', N'" + Doc8 + "', N'" + UrlDocPdf + "', N'" + Conve1 + "', N'" + Conve2 + "', N'" + Conve3 + "', N'" + Conve4 + "', N'" + Conve5 + "', N'" + Conve6 + "', N'" + Conve7 + "', N'" + Conve8 + "', N'" + Doc9 + "', N'" + Doc10 + "', N'" + Doc11 + "', N'" + Doc12 + "', N'" + Doc13 + "', N'" + Doc14 + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #region Zurich
        public int agregarConocimientoCliente(string NumeroFecha, string Ano, string strSucursal, string strTipoPersona)
        {
            try
            {
                #region Parametros
                parameters = new OleDbParameter[6];
                parameter = new OleDbParameter("@IdConocimientoCliente", OleDbType.Integer, 18);
                parameter.Direction = ParameterDirection.Output;
                parameters[0] = parameter;

                parameter = new OleDbParameter("@IdUsuario", OleDbType.Integer);
                parameter.Value = IdUsuario;
                parameters[1] = parameter;
                parameter = new OleDbParameter("@NumeroFecha", OleDbType.Integer);
                parameter.Value = NumeroFecha;
                parameters[2] = parameter;
                parameter = new OleDbParameter("@Ano", OleDbType.Integer);
                parameter.Value = Ano;
                parameters[3] = parameter;
                parameter = new OleDbParameter("@Sucursal", OleDbType.VarChar);
                parameter.Value = strSucursal;
                parameters[4] = parameter;

                parameter = new OleDbParameter("@TipoPersona", OleDbType.VarChar);
                parameter.Value = strTipoPersona;
                parameters[5] = parameter;
                #endregion

                cDataBase.conectar();
                cDataBase.ejecutarSPParametros("agregarConocimientoCliente", parameters);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return Convert.ToInt32(parameters[0].Value);
        }

        public void InfoFormPN(int IdConocimientoCliente, string PNPrimerApellido, string PNSegunApellido, string PNNombre1, string PNNombre2,
            string PNTipoDocumento, string PNNumeroDocumento, string PNLugar, string PNFechaExpedicion, string PNSexo,
            string PNFechaNacimiento, string PNNacionalidad1, string PNNacionalidad2, string PNOcupacionOficio, string PNEstadoCivil,
            string PNDireccionResidencia, string PNCiudadResidencia, string PNTelefonoResidencia, string PNCelular, string PNCorreoElectronico,
            string PNActividadEconomica, string PNOtraActividadEconomica,
            string PNEmpresaTrabajo, string PNCargo, string PNCiudadEmpresa, string PNDirEmpresa, string PNTelefonoEmpresa, string PNFax,
            string PNIngresosMensuales, string PNActivos, string PNEgresosMensuales, string PNPasivos, string PNOtrosIngresos, string PNConceptoOtrosIngresos,
            string PNPregunta1, string PNEspecifiquePregunta1)
        {
            string strConsulta = string.Empty, strPNNombres = string.Empty;

            try
            {
                strPNNombres = PNNombre1 + " " + PNNombre2;
                strConsulta = string.Format("INSERT Listas.InfoFormPN (IdConocimientoCliente, PNPrimerApellido, PNSegunApellido, PNNombres, " +
                    "PNTipoDocumento, PNNumeroDocumento, PNLugar, PNFechaExpedicion, PNSexo, " +
                    "PNFechaNacimiento, PNNacionalidad, PNNacionalidad2, PNOcupacionOficio, PNEstadoCivil, " +
                    "PNDireccionResidencia, PNCiudad1, PNTelefono1, PNCelular, PNCorreoElectronico, " +
                    "PNActividadEconomica, PNOtraActEconomica, " +
                    "PNEmpresaTrabajo, PNCargo, PNCiudad2, PNDireccion, PNTelefono2, PNFax, " +
                    "PNIngresosMensuales, PNActivos, PNEgresoMensuales, PNPasivos, PNOtrosIngresos, PNConceptoOtrosIngresos, " +
                    "PNPregunta1, PNEspecificacionPreguntas) " +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', " +
                    "N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', " +
                    "N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', " +
                    "N'{14}', N'{15}',N'{16}', N'{17}', N'{18}', " +
                    "N'{19}', N'{20}', " +
                    "N'{21}', N'{22}', N'{23}', N'{24}', N'{25}', N'{26}', " +
                    "N'{27}', N'{28}', N'{29}', N'{30}', N'{31}', N'{32}', " +
                    "N'{33}', N'{34}')",
                    IdConocimientoCliente, PNPrimerApellido, PNSegunApellido, strPNNombres,
                    PNTipoDocumento, PNNumeroDocumento, PNLugar, PNFechaExpedicion, PNSexo,
                    PNFechaNacimiento, PNNacionalidad1, PNNacionalidad2, PNOcupacionOficio, PNEstadoCivil,
                    PNDireccionResidencia, PNCiudadResidencia, PNTelefonoResidencia, PNCelular, PNCorreoElectronico,
                    PNActividadEconomica, PNOtraActividadEconomica,
                    PNEmpresaTrabajo, PNCargo, PNCiudadEmpresa, PNDirEmpresa, PNTelefonoEmpresa, PNFax,
                    PNIngresosMensuales, PNActivos, PNEgresosMensuales, PNPasivos, PNOtrosIngresos, PNConceptoOtrosIngresos,
                    PNPregunta1, PNEspecifiquePregunta1);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InfoFormPJ(int IdConocimientoCliente, string PJRazonDenominacion, string PJNIT,
            string PJTipoSociedad, string PJOtroTipoSociedad,
            string PJPrimerApellido, string PJSegundoApellido, string PJNombre1, string PJNombre2,
            string PJTipoDocumento, string PJOtroTipoDoc, string PJNumeroDocumento, string PJLugarExpedicion, string PJNacionalidad1, string PJNacionalidad2,
            string PJDireccionOficina, string PJCiudad1, string PJTelefono1, string PJFax1, string PJPagWeb, string PJCorreoPrincipal,
            string PJDireccionSucursal, string PJCiudad2, string PJTelefono2, string PJFax2,
            string PJTipoEmpresa, string PJActividadEconomica, string PJOtraActividadEconomica, string PJCIIU, string PJDescObjSocial,
            string PJConsecAS1, string PJNombreAS1, string PJTipoIdentificacionAS1, string PJNumeroDocumentoAS1,
            string PJConsecAS2, string PJNombreAS2, string PJTipoIdentificacionAS2, string PJNumeroDocumentoAS2,
            string PJConsecAS3, string PJNombreAS3, string PJTipoIdentificacionAS3, string PJNumeroDocumentoAS3,
            string PJConsecAS4, string PJNombreAS4, string PJTipoIdentificacionAS4, string PJNumeroDocumentoAS4,
            string PJIngresosMensuales, string PJActivos, string PJEgresoMensuales, string PJPasivos, string PJOtrosIngresos, string PJConceptoOtrosIngresos)
        {
            string strConsulta = string.Empty, strPJNombres = string.Empty;

            try
            {
                strPJNombres = PJNombre1 + " " + PJNombre2;

                strConsulta = string.Format("INSERT Listas.InfoFormPJ (IdConocimientoCliente, PJRazonDenominacion, PJNIT, PJTipoSociedad, PJOtroTipoSociedad, " +
                    "PJPrimerApellido, PJSegundoApellido, PJNombres, " +
                    "PJTipoDocumento, PJOtroTipoDoc, PJNumeroDocumento, PJLugarExpedicion, PJNacionalidad1, PJNacionalidad2, " +
                    "PJDireccionOficina, PJCiudad1, PJTelefono1, PJFax1, PJPagWeb , PJCorreoPrincipal, " +
                    "PJDireccionSucursal, PJCiudad2, PJTelefono2, PJFax2, " +
                    "PJTipoEmpresa, PJActividadEconomica, PJOtraActividadEconomica, PJCIIU, PJDescObjSocial, " +
                    "PJConsecAS1, PJNombreAS1, PJTipoIdentificacionAS1, PJNumeroDocumentoAS1, " +
                    "PJConsecAS2, PJNombreAS2, PJTipoIdentificacionAS2, PJNumeroDocumentoAS2, " +
                    "PJConsecAS3, PJNombreAS3, PJTipoIdentificacionAS3, PJNumeroDocumentoAS3, " +
                    "PJConsecAS4, PJNombreAS4, PJTipoIdentificacionAS4, PJNumeroDocumentoAS4, " +
                    "PJIngresosMensuales, PJActivos, PJEgresoMensuales, PJPasivos, PJOtrosIngresos, PJConceptoOtrosIngresos) " +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', " +
                    "N'{5}', N'{6}', N'{7}', " +
                    "N'{8}', N'{9}', N'{10}', N'{11}', N'{12}',N'{13}', " +
                    "N'{14}', N'{15}', N'{16}', N'{17}', N'{18}', N'{19}', " +
                    "N'{20}', N'{21}', N'{22}', N'{23}', " +
                    "N'{24}', N'{25}', N'{26}', N'{27}', N'{28}', " +
                    "N'{29}', N'{30}', N'{31}', N'{32}', " +
                    "N'{33}', N'{34}', N'{35}', N'{36}', " +
                    "N'{37}', N'{38}', N'{39}', N'{40}', " +
                    "N'{41}', N'{42}', N'{43}', N'{44}', " +
                    "N'{45}', N'{46}', N'{47}', N'{48}', N'{49}', N'{50}')",
                    IdConocimientoCliente, PJRazonDenominacion, PJNIT, PJTipoSociedad, PJOtroTipoSociedad,
                    PJPrimerApellido, PJSegundoApellido, strPJNombres,
                    PJTipoDocumento, PJOtroTipoDoc, PJNumeroDocumento, PJLugarExpedicion, PJNacionalidad1, PJNacionalidad2,
                    PJDireccionOficina, PJCiudad1, PJTelefono1, PJFax1, PJPagWeb, PJCorreoPrincipal,
                    PJDireccionSucursal, PJCiudad2, PJTelefono2, PJFax2,
                    PJTipoEmpresa, PJActividadEconomica, PJOtraActividadEconomica, PJCIIU, PJDescObjSocial,
                    PJConsecAS1, PJNombreAS1, PJTipoIdentificacionAS1, PJNumeroDocumentoAS1,
                    PJConsecAS2, PJNombreAS2, PJTipoIdentificacionAS2, PJNumeroDocumentoAS2,
                    PJConsecAS3, PJNombreAS3, PJTipoIdentificacionAS3, PJNumeroDocumentoAS3,
                    PJConsecAS4, PJNombreAS4, PJTipoIdentificacionAS4, PJNumeroDocumentoAS4,
                    PJIngresosMensuales, PJActivos, PJEgresoMensuales, PJPasivos, PJOtrosIngresos, PJConceptoOtrosIngresos);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InfoFormPF(int IdConocimientoCliente,
            string TransacMonedaExtra, string TipoTransaccion, string OtroTipoTransaccion, string PFProdExterior,
            string PFTipoProducto1, string PFNumeroProducto1, string PFEntidad1, string PFMonto1, string PFCiudad1, string PFPais1, string PFMoneda1,
            string PFTipoProducto2, string PFNumeroProducto2, string PFEntidad2, string PFMonto2, string PFCiudad2, string PFPais2, string PFMoneda2,
            string PFTipoProducto3, string PFNumeroProducto3, string PFEntidad3, string PFMonto3, string PFCiudad3, string PFPais3, string PFMoneda3)
        {
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormPF", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@TransacMonedaExtra", TransacMonedaExtra);
                comando.Parameters.AddWithValue("@TipoTransaccion", TipoTransaccion);
                comando.Parameters.AddWithValue("@OtroTipoTransaccion", OtroTipoTransaccion);
                comando.Parameters.AddWithValue("@PFTipoProducto1", PFTipoProducto1);
                comando.Parameters.AddWithValue("@PFNumeroProducto1", PFNumeroProducto1);
                comando.Parameters.AddWithValue("@PFEntidad1", PFEntidad1);
                comando.Parameters.AddWithValue("@PFMonto1", PFMonto1);
                comando.Parameters.AddWithValue("@PFCiudad1", PFCiudad1);
                comando.Parameters.AddWithValue("@PFPais1", PFPais1);
                comando.Parameters.AddWithValue("@PFMoneda1", PFMoneda1);
                comando.Parameters.AddWithValue("@PFTipoProducto2", PFTipoProducto2);
                comando.Parameters.AddWithValue("@PFNumeroProducto2", PFNumeroProducto2);
                comando.Parameters.AddWithValue("@PFEntidad2", PFEntidad2);
                comando.Parameters.AddWithValue("@PFMonto2", PFMonto2);
                comando.Parameters.AddWithValue("@PFCiudad2", PFCiudad2);
                comando.Parameters.AddWithValue("@PFPais2", PFPais2);
                comando.Parameters.AddWithValue("@PFMoneda2", PFMoneda2);
                comando.Parameters.AddWithValue("@PFCtaMonedaExtra", string.Empty);
                comando.Parameters.AddWithValue("@PFNroCtaMonedaExtra", string.Empty);
                comando.Parameters.AddWithValue("@PFBancoCtaMonedaExtra", string.Empty);
                comando.Parameters.AddWithValue("@PFCiudadCtaMonedaExtra", string.Empty);
                comando.Parameters.AddWithValue("@PFPaisCtaMonedaExtra", string.Empty);
                comando.Parameters.AddWithValue("@PFMonedaCtaMonedaExtra", string.Empty);
                comando.Parameters.AddWithValue("@PFProdExterior", PFProdExterior);
                comando.Parameters.AddWithValue("@PFTipoProducto3", PFTipoProducto3);
                comando.Parameters.AddWithValue("@PFNumeroProducto3", PFNumeroProducto3);
                comando.Parameters.AddWithValue("@PFEntidad3", PFEntidad3);
                comando.Parameters.AddWithValue("@PFMonto3", PFMonto3);
                comando.Parameters.AddWithValue("@PFCiudad3", PFCiudad3);
                comando.Parameters.AddWithValue("@PFPais3", PFPais3);
                comando.Parameters.AddWithValue("@PFMoneda3", PFMoneda3);

                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void InfoFormPF(int IdConocimientoCliente,
            string TransacMonedaExtra, string TipoTransaccion, string OtroTipoTransaccion, string PFProdExterior,
            string PFTipoProducto1, string PFNumeroProducto1, string PFEntidad1, string PFMonto1, string PFCiudad1, string PFPais1, string PFMoneda1,
            string PFTipoProducto2, string PFNumeroProducto2, string PFEntidad2, string PFMonto2, string PFCiudad2, string PFPais2, string PFMoneda2,
            string PFTipoProducto3, string PFNumeroProducto3, string PFEntidad3, string PFMonto3, string PFCiudad3, string PFPais3, string PFMoneda3,
            string PFCtaMonedaExtra, string PFNroCtaMonedaExtra, string PFBancoCtaMonedaExtra, string PFCiudadCtaMonedaExtra, string PFPaisCtaMonedaExtra, string PFMonedaCtaMonedaExtra)
        {
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormPF", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@TransacMonedaExtra", TransacMonedaExtra);
                comando.Parameters.AddWithValue("@TipoTransaccion", TipoTransaccion);
                comando.Parameters.AddWithValue("@OtroTipoTransaccion", OtroTipoTransaccion);
                comando.Parameters.AddWithValue("@PFTipoProducto1", PFTipoProducto1);
                comando.Parameters.AddWithValue("@PFNumeroProducto1", PFNumeroProducto1);
                comando.Parameters.AddWithValue("@PFEntidad1", PFEntidad1);
                comando.Parameters.AddWithValue("@PFMonto1", PFMonto1);
                comando.Parameters.AddWithValue("@PFCiudad1", PFCiudad1);
                comando.Parameters.AddWithValue("@PFPais1", PFPais1);
                comando.Parameters.AddWithValue("@PFMoneda1", PFMoneda1);
                comando.Parameters.AddWithValue("@PFTipoProducto2", PFTipoProducto2);
                comando.Parameters.AddWithValue("@PFNumeroProducto2", PFNumeroProducto2);
                comando.Parameters.AddWithValue("@PFEntidad2", PFEntidad2);
                comando.Parameters.AddWithValue("@PFMonto2", PFMonto2);
                comando.Parameters.AddWithValue("@PFCiudad2", PFCiudad2);
                comando.Parameters.AddWithValue("@PFPais2", PFPais2);
                comando.Parameters.AddWithValue("@PFMoneda2", PFMoneda2);
                comando.Parameters.AddWithValue("@PFCtaMonedaExtra", PFCtaMonedaExtra);
                comando.Parameters.AddWithValue("@PFNroCtaMonedaExtra", PFNroCtaMonedaExtra);
                comando.Parameters.AddWithValue("@PFBancoCtaMonedaExtra", PFBancoCtaMonedaExtra);
                comando.Parameters.AddWithValue("@PFCiudadCtaMonedaExtra", PFCiudadCtaMonedaExtra);
                comando.Parameters.AddWithValue("@PFPaisCtaMonedaExtra", PFPaisCtaMonedaExtra);
                comando.Parameters.AddWithValue("@PFMonedaCtaMonedaExtra", PFMonedaCtaMonedaExtra);
                comando.Parameters.AddWithValue("@PFProdExterior", PFProdExterior);
                comando.Parameters.AddWithValue("@PFTipoProducto3", PFTipoProducto3);
                comando.Parameters.AddWithValue("@PFNumeroProducto3", PFNumeroProducto3);
                comando.Parameters.AddWithValue("@PFEntidad3", PFEntidad3);
                comando.Parameters.AddWithValue("@PFMonto3", PFMonto3);
                comando.Parameters.AddWithValue("@PFCiudad3", PFCiudad3);
                comando.Parameters.AddWithValue("@PFPais3", PFPais3);
                comando.Parameters.AddWithValue("@PFMoneda3", PFMoneda3);

                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void InfoFormEntrevista(int IdConocimientoCliente,
            string LugarEntrevista, string FechaEntrevista, string HoraEntrevista,
            string NombreResponsable, string Resultado, string CCRespEntrevista, string Observaciones1,
            string NombreResponsableVerifica, string CCVerificaEntrevista, string Observaciones2, bool foo)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT Listas.InfoFormEntrevista (IdConocimientoCliente, " +
                    "LugarEntrevista, FechaEntrevista, HoraEntrevista, NombreResponsable, Resultado, CCRespEntrevista, Observaciones1, " +
                    "NombreVerifica, CCVerificaEntrevista, Observaciones2) " +
                    "VALUES ({0},  N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}' ," +
                    "N'{8}', N'{9}', N'{10}')",
                    IdConocimientoCliente, LugarEntrevista, FechaEntrevista, HoraEntrevista, NombreResponsable, Resultado, CCRespEntrevista, Observaciones1,
                    NombreResponsableVerifica, CCVerificaEntrevista, Observaciones2);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void InfoFormEntrevista(int IdConocimientoCliente, string LugarEntrevista,string FechaEntrevista,string HoraEntrevista,string Resultado,
            string Observaciones1,string NombreResponsable,string FechaVerificacion,string HoraVerificacion,string NombreVerifica,string Observaciones2,
            string ValidaFirma,string ValidaHuella,string ValidaEntrevista,string CCRespEntrevista,string CCVerificaEntrevista,string NombreIntermediario
        )
        {
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormEntrevista", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@LugarEntrevista", LugarEntrevista);
                comando.Parameters.AddWithValue("@FechaEntrevista", FechaEntrevista);
                comando.Parameters.AddWithValue("@HoraEntrevista", HoraEntrevista);
                comando.Parameters.AddWithValue("@Resultado", Resultado);
                comando.Parameters.AddWithValue("@Observaciones1", Observaciones1);
                comando.Parameters.AddWithValue("@NombreResponsable", NombreResponsable);
                comando.Parameters.AddWithValue("@FechaVerificacion", FechaVerificacion);
                comando.Parameters.AddWithValue("@HoraVerificacion", HoraVerificacion);
                comando.Parameters.AddWithValue("@NombreVerifica", NombreVerifica);
                comando.Parameters.AddWithValue("@Observaciones2", Observaciones2);
                comando.Parameters.AddWithValue("@ValidaFirma", ValidaFirma);
                comando.Parameters.AddWithValue("@ValidaHuella", ValidaHuella);
                comando.Parameters.AddWithValue("@ValidaEntrevista", ValidaEntrevista);
                comando.Parameters.AddWithValue("@CCRespEntrevista", CCRespEntrevista);
                comando.Parameters.AddWithValue("@CCVerificaEntrevista", CCVerificaEntrevista);
                comando.Parameters.AddWithValue("@NombreIntermediario", NombreIntermediario);

                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        #endregion

        #endregion

        //----------------------------------------------------------------------------------------------------------------------
        #region Buscar Cliente
        public DataTable buscarClientFormPN(String PrimerApellido, String SegundoApellido, String Nombre,
            String NumeroDocumento, String Ano, String FechaDesde, String FechaHasta, String SINOConve, String Conve, String Estado)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty;
            try
            {
                if (PrimerApellido != string.Empty)
                {
                    condicion = "WHERE	(Listas.InfoFormPN.PNPrimerApellido LIKE N'%" + PrimerApellido + "%') ";
                }
                if (SegundoApellido != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormPN.PNSegunApellido LIKE N'%" + SegundoApellido + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormPN.PNSegunApellido LIKE N'%" + SegundoApellido + "%') ";
                    }
                }
                if (Nombre != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormPN.PNNombres LIKE N'%" + Nombre + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormPN.PNNombres LIKE N'%" + Nombre + "%') ";
                    }
                }
                if (NumeroDocumento != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormPN.PNNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormPN.PNNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    }
                }
                if (Ano != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.ConocimientoCliente.Ano = " + Ano + ") ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.ConocimientoCliente.Ano = " + Ano + ") ";
                    }
                }
                if (FechaDesde != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.ConocimientoCliente.NumeroFecha >= " + FechaDesde + ")";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.ConocimientoCliente.NumeroFecha >= " + FechaDesde + ")";
                    }
                }
                if (FechaHasta != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.ConocimientoCliente.NumeroFecha <= " + FechaHasta + ")";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.ConocimientoCliente.NumeroFecha <= " + FechaHasta + ")";
                    }
                }
                if (SINOConve == "SI")
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormDocsInu." + Conve.Trim() + " = N'" + Estado.Trim() + "') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormDocsInu." + Conve.Trim() + " = N'" + Estado.Trim() + "') ";
                    }
                }
                if (condicion.ToString().Trim() == "")
                {
                    condicion = "WHERE ((LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNNombres, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNNumeroDocumento, ''))) != '')) ";
                }
                else
                {
                    condicion = condicion + "AND ((LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNNombres, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPN.PNNumeroDocumento, ''))) != '')) ";
                }
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT	Listas.ConocimientoCliente.IdConocimientoCliente, LTRIM(RTRIM(Listas.InfoFormPN.PNPrimerApellido)) AS PrimerApellido, LTRIM(RTRIM(Listas.InfoFormPN.PNSegunApellido)) AS SegundoApellido, LTRIM(RTRIM(Listas.InfoFormPN.PNNombres)) AS Nombres, LTRIM(RTRIM(Listas.InfoFormPN.PNTipoDocumento)) AS TipoDocumento, LTRIM(RTRIM(Listas.InfoFormPN.PNNumeroDocumento)) AS NumeroDocumento, Listas.ConocimientoCliente.Ano, CONVERT(varchar, Listas.ConocimientoCliente.FechaRegistro, 109) AS FechaRegistro, '' AS RazonDenominacion, '' AS NIT FROM Listas.ConocimientoCliente INNER JOIN Listas.InfoFormPN ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormPN.IdConocimientoCliente INNER JOIN Listas.InfoFormDocsInu ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormDocsInu.IdConocimientoCliente " + condicion + " ORDER BY NumeroDocumento, FechaRegistro");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable buscarClientFormPJ(String PrimerApellido, String SegundoApellido, String Nombre, String NumeroDocumento,
            String Ano, String FechaDesde, String FechaHasta, String RazonSosial, String NIT, String SINOConve, String Conve, String Estado)
        {
            DataTable dtInformacion = new DataTable();
            String condicion = string.Empty;
            try
            {
                if (PrimerApellido != string.Empty)
                    condicion = "WHERE (Listas.InfoFormPJ.PJPrimerApellido LIKE N'%" + PrimerApellido + "%') ";

                if (SegundoApellido != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (Listas.InfoFormPJ.PJSegundoApellido LIKE N'%" + SegundoApellido + "%') ";
                    else
                        condicion = condicion + "AND (Listas.InfoFormPJ.PJSegundoApellido LIKE N'%" + SegundoApellido + "%') ";
                }

                if (Nombre != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormPJ.PJNombres LIKE N'%" + Nombre + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormPJ.PJNombres LIKE N'%" + Nombre + "%') ";
                    }
                }
                if (NumeroDocumento != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormPJ.PJNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormPJ.PJNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    }
                }
                if (Ano != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.ConocimientoCliente.Ano = " + Ano + ") ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.ConocimientoCliente.Ano = " + Ano + ") ";
                    }
                }
                if (FechaDesde != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.ConocimientoCliente.NumeroFecha >= " + FechaDesde + ") ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.ConocimientoCliente.NumeroFecha >= " + FechaDesde + ") ";
                    }
                }
                if (FechaHasta != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.ConocimientoCliente.NumeroFecha <= " + FechaHasta + ") ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.ConocimientoCliente.NumeroFecha <= " + FechaHasta + ") ";
                    }
                }
                if (RazonSosial != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormPJ.PJRazonDenominacion LIKE N'%" + RazonSosial + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormPJ.PJRazonDenominacion LIKE N'%" + RazonSosial + "%') ";
                    }
                }
                if (NIT != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormPJ.PJNIT LIKE N'%" + NIT + "%') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormPJ.PJNIT LIKE N'%" + NIT + "%') ";
                    }
                }
                if (SINOConve == "SI")
                {
                    if (condicion.ToString().Trim() == "")
                    {
                        condicion = "WHERE (Listas.InfoFormDocsInu." + Conve.Trim() + " = N'" + Estado.Trim() + "') ";
                    }
                    else
                    {
                        condicion = condicion + "AND (Listas.InfoFormDocsInu." + Conve.Trim() + " = N'" + Estado.Trim() + "') ";
                    }
                }
                if (condicion.ToString().Trim() == "")
                {
                    condicion = "WHERE ((LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJNombres, ''))) !='') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJNumeroDocumento, ''))) != '')) ";
                }
                else
                {
                    condicion = condicion + "AND ((LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJNombres, ''))) !='') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(Listas.InfoFormPJ.PJNumeroDocumento, ''))) != '')) ";
                }
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT Listas.ConocimientoCliente.IdConocimientoCliente, LTRIM(RTRIM(Listas.InfoFormPJ.PJPrimerApellido)) AS PrimerApellido, LTRIM(RTRIM(Listas.InfoFormPJ.PJSegundoApellido)) AS SegundoApellido, LTRIM(RTRIM(Listas.InfoFormPJ.PJNombres)) AS Nombres, LTRIM(RTRIM(Listas.InfoFormPJ.PJTipoDocumento)) AS TipoDocumento, LTRIM(RTRIM(Listas.InfoFormPJ.PJNumeroDocumento)) AS NumeroDocumento, Listas.ConocimientoCliente.Ano, CONVERT(varchar, Listas.ConocimientoCliente.FechaRegistro, 109) AS FechaRegistro, LTRIM(RTRIM(Listas.InfoFormPJ.PJRazonDenominacion)) AS RazonDenominacion, LTRIM(RTRIM(Listas.InfoFormPJ.PJNIT)) AS NIT FROM Listas.ConocimientoCliente INNER JOIN Listas.InfoFormPJ ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormPJ.IdConocimientoCliente INNER JOIN Listas.InfoFormDocsInu ON Listas.ConocimientoCliente.IdConocimientoCliente = Listas.InfoFormDocsInu.IdConocimientoCliente " + condicion + " ORDER BY NumeroDocumento, FechaRegistro");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        #region Zurich
        public DataTable buscarClientFormPN(string PrimerApellido, string SegundoApellido, string Nombre, string NumeroDocumento)
        {
            string condicion = string.Empty, strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                if (PrimerApellido != string.Empty)
                    condicion = "WHERE (LIPN.PNPrimerApellido LIKE N'%" + PrimerApellido + "%') ";

                if (SegundoApellido != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPN.PNSegunApellido LIKE N'%" + SegundoApellido + "%') ";
                    else
                        condicion = condicion + "AND (LIPN.PNSegunApellido LIKE N'%" + SegundoApellido + "%') ";
                }

                if (Nombre != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPN.PNNombres LIKE N'%" + Nombre + "%') ";
                    else
                        condicion = condicion + "AND (LIPN.PNNombres LIKE N'%" + Nombre + "%') ";
                }

                if (NumeroDocumento != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPN.PNNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    else
                        condicion = condicion + "AND (LIPN.PNNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                }

                /*
                if (condicion.ToString().Trim() == "")
                    condicion = "WHERE ((LTRIM(RTRIM(ISNULL(LIPN.PNPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIPN.PNNombres, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIPN.PNTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIPN.PNNumeroDocumento, ''))) != '')) ";
                else
                    condicion = condicion + "AND ((LTRIM(RTRIM(ISNULL(LIPN.PNPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIPN.PNNombres, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIPN.PNTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIPN.PNNumeroDocumento, ''))) != '')) ";
                */

                strConsulta = string.Format("SELECT LCC.IdConocimientoCliente, LTRIM(RTRIM(LIPN.PNPrimerApellido)) PrimerApellido, " +
                    "LTRIM(RTRIM(LIPN.PNSegunApellido)) SegundoApellido, LTRIM(RTRIM(LIPN.PNNombres)) Nombres, " +
                    "LTRIM(RTRIM(LIPN.PNTipoDocumento)) TipoDocumento, LTRIM(RTRIM(LIPN.PNNumeroDocumento)) NumeroDocumento, " +
                    "LCC.Ano, CONVERT(VARCHAR, LCC.FechaRegistro, 109) FechaRegistro, '' RazonDenominacion, '' NIT " +
                    "FROM Listas.ConocimientoCliente LCC " +
                    "INNER JOIN Listas.InfoFormPN LIPN ON LCC.IdConocimientoCliente = LIPN.IdConocimientoCliente " +
                    "{0} ORDER BY NumeroDocumento, FechaRegistro", condicion);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return dtInformacion;
        }

        public DataTable InfoAprobadoFCC(int IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            string strConsulta = string.Empty;
            try
            {
                strConsulta = string.Format("SELECT b.Estado,c.Usuario,a.[FechaAprobado] " +
                    "FROM [Listas].[FormularioConocimientoCliente] as a " +
                    "inner join Listas.EstadosFCC as b on b.IdEstado = a.Estado "+
                    "inner join Listas.Usuarios as c on c.IdUsuario = a.IdUsuario " +
                    "where IdConocimientoCliente = {0} and a.Aprobado = 1", IdConocimientoCliente);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable buscarClientFormPJ(string PrimerApellido, string SegundoApellido, string Nombre, string NumeroDocumento,
            string RazonSocial, string NIT)
        {
            string condicion = string.Empty, strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                if (PrimerApellido != string.Empty)
                    condicion = "WHERE (LIPJ.PJPrimerApellido LIKE N'%" + PrimerApellido + "%') ";

                if (SegundoApellido != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPJ.PJSegundoApellido LIKE N'%" + SegundoApellido + "%') ";
                    else
                        condicion = condicion + "AND (LIPJ.PJSegundoApellido LIKE N'%" + SegundoApellido + "%') ";
                }

                if (Nombre != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPJ.PJNombres LIKE N'%" + Nombre + "%') ";
                    else
                        condicion = condicion + "AND (LIPJ.PJNombres LIKE N'%" + Nombre + "%') ";
                }

                if (NumeroDocumento != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPJ.PJNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    else
                        condicion = condicion + "AND (LIPJ.PJNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                }

                if (RazonSocial != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPJ.PJRazonDenominacion LIKE N'%" + RazonSocial + "%') ";
                    else
                        condicion = condicion + "AND (LIPJ.PJRazonDenominacion LIKE N'%" + RazonSocial + "%') ";
                }

                if (NIT != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (LIPJ.PJNIT LIKE N'%" + NIT + "%') ";
                    else
                        condicion = condicion + "AND (LIPJ.PJNIT LIKE N'%" + NIT + "%') ";
                }

                /*
                if (condicion.ToString().Trim() == "")
                    condicion = "WHERE ((LTRIM(RTRIM(ISNULL(LIPJ.PJPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIPJ.PJNombres, ''))) !='') AND (LTRIM(RTRIM(ISNULL(LIPJ.PJTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIPJ.PJNumeroDocumento, ''))) != '')) ";
                else
                    condicion = condicion + "AND ((LTRIM(RTRIM(ISNULL(LIPJ.PJPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIPJ.PJNombres, ''))) !='') AND (LTRIM(RTRIM(ISNULL(LIPJ.PJTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIPJ.PJNumeroDocumento, ''))) != '')) ";
                */
                strConsulta = string.Format("SELECT LCC.IdConocimientoCliente, " +
                    "LTRIM(RTRIM(LIPJ.PJPrimerApellido)) PrimerApellido, LTRIM(RTRIM(LIPJ.PJSegundoApellido)) SegundoApellido, " +
                    "LTRIM(RTRIM(LIPJ.PJNombres)) Nombres, LTRIM(RTRIM(LIPJ.PJTipoDocumento)) TipoDocumento, " +
                    "LTRIM(RTRIM(LIPJ.PJNumeroDocumento)) NumeroDocumento, LCC.Ano, " +
                    "CONVERT(varchar, LCC.FechaRegistro, 109) FechaRegistro, LTRIM(RTRIM(LIPJ.PJRazonDenominacion)) AS RazonDenominacion, " +
                    "LTRIM(RTRIM(LIPJ.PJNIT)) NIT " +
                    "FROM Listas.ConocimientoCliente LCC " +
                    "INNER JOIN Listas.InfoFormPJ LIPJ ON LCC.IdConocimientoCliente = LIPJ.IdConocimientoCliente " +
                    "{0} ORDER BY NumeroDocumento, FechaRegistro", condicion);

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        #endregion

        #endregion

        #region Consulta Formulario
        public DataTable buscarClientFormPN(string PrimerApellido, string SegundoApellido, string Nombre,
            string NumeroDocumento, string Ano, string FechaDesde, string FechaHasta)
        {
            string strCondicion = string.Empty, strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                #region Primer Apellido
                if (!string.IsNullOrEmpty(PrimerApellido))
                    strCondicion = "WHERE (LIF.PNPrimerApellido LIKE N'%" + PrimerApellido + "%') ";
                #endregion

                #region Segundo Apellido
                if (!string.IsNullOrEmpty(SegundoApellido))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PNSegunApellido LIKE N'%" + SegundoApellido + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PNSegunApellido LIKE N'%" + SegundoApellido + "%') ";
                }
                #endregion

                #region Nombre
                if (!string.IsNullOrEmpty(Nombre))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PNNombres LIKE N'%" + Nombre + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PNNombres LIKE N'%" + Nombre + "%') ";
                }
                #endregion

                #region Numero Documento
                if (!string.IsNullOrEmpty(NumeroDocumento))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PNNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PNNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                }
                #endregion

                #region Ano
                if (!string.IsNullOrEmpty(Ano))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LCC.Ano = " + Ano + ") ";
                    else
                        strCondicion = strCondicion + "AND (LCC.Ano = " + Ano + ") ";
                }
                #endregion

                #region Fecha Desde
                if (!string.IsNullOrEmpty(FechaDesde))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LCC.NumeroFecha >= " + FechaDesde + ")";
                    else
                        strCondicion = strCondicion + "AND (LCC.NumeroFecha >= " + FechaDesde + ")";
                }
                #endregion

                #region Fecha Hasta
                if (!string.IsNullOrEmpty(FechaHasta))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LCC.NumeroFecha <= " + FechaHasta + ")";
                    else
                        strCondicion = strCondicion + "AND (LCC.NumeroFecha <= " + FechaHasta + ")";
                }
                #endregion

                #region Complemento Condicion
                if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                    strCondicion = "WHERE ((LTRIM(RTRIM(ISNULL(LIF.PNPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIF.PNNombres, ''))) != '') " +
                        "AND (LTRIM(RTRIM(ISNULL(LIF.PNTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIF.PNNumeroDocumento, ''))) != '')) ";
                else
                    strCondicion = strCondicion + "AND ((LTRIM(RTRIM(ISNULL(LIF.PNPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIF.PNNombres, ''))) != '') " +
                        "AND (LTRIM(RTRIM(ISNULL(LIF.PNTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIF.PNNumeroDocumento, ''))) != '')) ";
                #endregion

                strConsulta = "SELECT LCC.IdConocimientoCliente, LTRIM(RTRIM(LIF.PNPrimerApellido)) PrimerApellido, " +
                    "LTRIM(RTRIM(LIF.PNSegunApellido)) SegundoApellido, LTRIM(RTRIM(LIF.PNNombres)) Nombres, " +
                    "LTRIM(RTRIM(LIF.PNTipoDocumento)) TipoDocumento, LTRIM(RTRIM(LIF.PNNumeroDocumento)) NumeroDocumento, " +
                    "LCC.Ano, CONVERT(varchar, LCC.FechaRegistro, 109) FechaRegistro, '' RazonDenominacion, '' NIT " +
                    "FROM Listas.ConocimientoCliente LCC " +
                    "INNER JOIN Listas.InfoFormPN LIF ON LCC.IdConocimientoCliente = LIF.IdConocimientoCliente " +
                    strCondicion + " ORDER BY NumeroDocumento, FechaRegistro";

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return dtInformacion;
        }

        public DataTable buscarClientFormPJ(string PrimerApellido, string SegundoApellido, string Nombre, string NumeroDocumento,
            string Ano, string FechaDesde, string FechaHasta, string RazonSocial, string NIT)
        {
            string strCondicion = string.Empty, strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                #region Primer Apellido
                if (!string.IsNullOrEmpty(PrimerApellido))
                    strCondicion = "WHERE (LIF.PJPrimerApellido LIKE N'%" + PrimerApellido + "%') ";
                #endregion

                #region Segundo Apellido
                if (!string.IsNullOrEmpty(SegundoApellido))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PJSegundoApellido LIKE N'%" + SegundoApellido + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PJSegundoApellido LIKE N'%" + SegundoApellido + "%') ";
                }
                #endregion

                #region Nombre
                if (!string.IsNullOrEmpty(Nombre))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PJNombres LIKE N'%" + Nombre + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PJNombres LIKE N'%" + Nombre + "%') ";
                }
                #endregion

                #region Numero Documento
                if (!string.IsNullOrEmpty(NumeroDocumento))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PJNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PJNumeroDocumento LIKE N'%" + NumeroDocumento + "%') ";
                }
                #endregion

                #region Ano
                if (!string.IsNullOrEmpty(Ano))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LCC.Ano = " + Ano + ") ";
                    else
                        strCondicion = strCondicion + "AND (LCC.Ano = " + Ano + ") ";
                }
                #endregion

                #region Fecha Desde
                if (!string.IsNullOrEmpty(FechaDesde))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LCC.NumeroFecha >= " + FechaDesde + ") ";
                    else
                        strCondicion = strCondicion + "AND (LCC.NumeroFecha >= " + FechaDesde + ") ";
                }
                #endregion

                #region Fecha Hasta
                if (!string.IsNullOrEmpty(FechaHasta))
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LCC.NumeroFecha <= " + FechaHasta + ") ";
                    else
                        strCondicion = strCondicion + "AND (LCC.NumeroFecha <= " + FechaHasta + ") ";
                }
                #endregion

                #region RazonSocial
                if (RazonSocial != string.Empty)
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PJRazonDenominacion LIKE N'%" + RazonSocial + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PJRazonDenominacion LIKE N'%" + RazonSocial + "%') ";
                }
                #endregion

                #region NIT
                if (NIT != string.Empty)
                {
                    if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                        strCondicion = "WHERE (LIF.PJNIT LIKE N'%" + NIT + "%') ";
                    else
                        strCondicion = strCondicion + "AND (LIF.PJNIT LIKE N'%" + NIT + "%') ";
                }
                #endregion

                #region Complemento Condicion
                if (string.IsNullOrEmpty(strCondicion.ToString().Trim()))
                    strCondicion = "WHERE ((LTRIM(RTRIM(ISNULL(LIF.PJPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIF.PJNombres, ''))) !='') " +
                        "AND (LTRIM(RTRIM(ISNULL(LIF.PJTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIF.PJNumeroDocumento, ''))) != '')) ";
                else
                    strCondicion = strCondicion + "AND ((LTRIM(RTRIM(ISNULL(LIF.PJPrimerApellido, ''))) != '') AND (LTRIM(RTRIM(ISNULL(LIF.PJNombres, ''))) !='') " +
                        "AND (LTRIM(RTRIM(ISNULL(LIF.PJTipoDocumento, '---'))) != '---') AND (LTRIM(RTRIM(ISNULL(LIF.PJNumeroDocumento, ''))) != '')) ";
                #endregion

                strConsulta = "SELECT LCC.IdConocimientoCliente, LTRIM(RTRIM(LIF.PJPrimerApellido)) AS PrimerApellido, " +
                    "LTRIM(RTRIM(LIF.PJSegundoApellido)) AS SegundoApellido, LTRIM(RTRIM(LIF.PJNombres)) AS Nombres, " +
                    "LTRIM(RTRIM(LIF.PJTipoDocumento)) AS TipoDocumento, LTRIM(RTRIM(LIF.PJNumeroDocumento)) AS NumeroDocumento, " +
                    "LCC.Ano, CONVERT(varchar, LCC.FechaRegistro, 109) AS FechaRegistro, " +
                    "LTRIM(RTRIM(LIF.PJRazonDenominacion)) AS RazonDenominacion, LTRIM(RTRIM(LIF.PJNIT)) AS NIT " +
                    "FROM Listas.ConocimientoCliente LCC" +
                    "INNER JOIN Listas.InfoFormPJ LIF ON LCC.IdConocimientoCliente = LIF.IdConocimientoCliente " +
                    strCondicion + " ORDER BY NumeroDocumento, FechaRegistro";

                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------
        #region Actualizar
        public void UpdateInfoFormCliente(int IdConocimientoCliente, String FechaFormulario, String ClaseVinculacion, String OtraClaseVinculacion, String TomadorAsegurado,
                                          String OtraTomadorAsegurado, String TomadorBeneficiario, String OtraTomadorBeneficiario, String AseguradoBeneficiario, String OtraAseguradoBeneficiario)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.InfoFormCliente SET FechaFormulario = N'" + FechaFormulario + "', ClaseVinculacion = N'" + ClaseVinculacion + "', OtraClaseVinculacion = N'" + OtraClaseVinculacion + "', TomadorAsegurado = N'" + TomadorAsegurado + "', OtraTomadorAsegurado = N'" + OtraTomadorAsegurado + "', TomadorBeneficiario = N'" + TomadorBeneficiario + "', OtraTomadorBeneficiario = N'" + OtraTomadorBeneficiario + "', AseguradoBeneficiario = N'" + AseguradoBeneficiario + "', OtraAseguradoBeneficiario = N'" + OtraAseguradoBeneficiario + "' WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInfoFormPN(int IdConocimientoCliente, String PNPrimerApellido, String PNSegunApellido, String PNNombres, String PNTipoDocumento, String PNNumeroDocumento, String PNFechaExpedicion, String PNLugar,
                                     String PNFechaNacimiento, String PNNacionalidad, String PNOcupacionOficio, String PNProfesion, String PNEstadoOcupacion, String PNActividadEconomica, String PNCIIU, String PNEmpresaTrabajo,
                                     String PNArea, String PNCargo, String PNCiudad1, String PNDireccion, String PNTelefono1, String PNFax, String PNDireccionResidencia, String PNCiudad2, String PNTelefono2, String PNCelular,
                                     String PNPregunta1, String PNPregunta2, String PNPregunta3, String PNEspecificacionPreguntas, String PNIngresosMensuales, String PNActivos, String PNEgresoMensuales, String PNPasivos,
                                     String PNOtrosIngresos, String PNConceptoOtrosIngresos)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.InfoFormPN SET PNPrimerApellido = N'" + PNPrimerApellido + "', PNSegunApellido = N'" + PNSegunApellido + "', PNNombres = N'" + PNNombres + "', PNTipoDocumento = N'" + PNTipoDocumento + "', PNNumeroDocumento = N'" + PNNumeroDocumento + "', PNFechaExpedicion = N'" + PNFechaExpedicion + "', PNLugar = N'" + PNLugar + "', PNFechaNacimiento = N'" + PNFechaNacimiento + "', PNNacionalidad = N'" + PNNacionalidad + "', PNOcupacionOficio = N'" + PNOcupacionOficio + "', PNProfesion = N'" + PNProfesion + "', PNEstadoOcupacion = N'" + PNEstadoOcupacion + "', PNActividadEconomica = N'" + PNActividadEconomica + "', PNCIIU = N'" + PNCIIU + "', PNEmpresaTrabajo = N'" + PNEmpresaTrabajo + "', PNArea = N'" + PNArea + "', PNCargo = N'" + PNCargo + "', PNCiudad1 = N'" + PNCiudad1 + "', PNDireccion = N'" + PNDireccion + "', PNTelefono1 = N'" + PNTelefono1 + "', PNFax = N'" + PNFax + "', PNDireccionResidencia = N'" + PNDireccionResidencia + "', PNCiudad2 = N'" + PNCiudad2 + "', PNTelefono2 = N'" + PNTelefono2 + "', PNCelular = N'" + PNCelular + "', PNPregunta1 = N'" + PNPregunta1 + "', PNPregunta2 = N'" + PNPregunta2 + "', PNPregunta3 = N'" + PNPregunta3 + "', PNEspecificacionPreguntas = N'" + PNEspecificacionPreguntas + "', PNIngresosMensuales = N'" + PNIngresosMensuales + "', PNActivos = N'" + PNActivos + "', PNEgresoMensuales = N'" + PNEgresoMensuales + "', PNPasivos = N'" + PNPasivos + "', PNOtrosIngresos = N'" + PNOtrosIngresos + "', PNConceptoOtrosIngresos = N'" + PNConceptoOtrosIngresos + "' WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInfoFormPJ(int IdConocimientoCliente, String PJRazonDenominacion, String PJNIT, String PJPrimerApellido, String PJSegundoApellido, String PJNombres, String PJTipoDocumento, String PJNumeroDocumento, String PJLugarExpedicion,
                                     String PJFechaExpedicion, String PJDireccionOficina, String PJCiudad1, String PJTelefono1, String PJFax1, String PJDireccionSucursal, String PJCiudad2, String PJTelefono2, String PJFax2, String PJTipoEmpresa, String PJActividadEconomica,
                                     String PJOtraActividadEconomica, String PJCIIU, String PJNombreAS1, String PJTipoIdentificacionAS1, String PJNumeroDocumentoAS1, String PJNombreAS2, String PJTipoIdentificacionAS2, String PJNumeroDocumentoAS2, String PJNombreAS3,
                                     String PJTipoIdentificacionAS3, String PJNumeroDocumentoAS3, String PJNombreAS4, String PJTipoIdentificacionAS4, String PJNumeroDocumentoAS4, String PJNombreAS5, String PJTipoIdentificacionAS5, String PJNumeroDocumentoAS5,
                                     String PJIngresosMensuales, String PJActivos, String PJEgresoMensuales, String PJPasivos, String PJOtrosIngresos, String PJConceptoOtrosIngresos)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.InfoFormPJ SET PJRazonDenominacion = N'" + PJRazonDenominacion + "', PJNIT = N'" + PJNIT + "', PJPrimerApellido = N'" + PJPrimerApellido + "', PJSegundoApellido = N'" + PJSegundoApellido + "', PJNombres = N'" + PJNombres + "', PJTipoDocumento = N'" + PJTipoDocumento + "', PJNumeroDocumento = N'" + PJNumeroDocumento + "', PJLugarExpedicion = N'" + PJLugarExpedicion + "', PJFechaExpedicion = N'" + PJFechaExpedicion + "', PJDireccionOficina = N'" + PJDireccionOficina + "', PJCiudad1 = N'" + PJCiudad1 + "', PJTelefono1 = N'" + PJTelefono1 + "', PJFax1 = N'" + PJFax1 + "', PJDireccionSucursal = N'" + PJDireccionSucursal + "', PJCiudad2 = N'" + PJCiudad2 + "', PJTelefono2 = N'" + PJTelefono2 + "', PJFax2 = N'" + PJFax2 + "', PJTipoEmpresa = N'" + PJTipoEmpresa + "', PJActividadEconomica = N'" + PJActividadEconomica + "', PJOtraActividadEconomica = N'" + PJOtraActividadEconomica + "', PJCIIU = N'" + PJCIIU + "', PJNombreAS1 = N'" + PJNombreAS1 + "', PJTipoIdentificacionAS1 = N'" + PJTipoIdentificacionAS1 + "', PJNumeroDocumentoAS1 = N'" + PJNumeroDocumentoAS1 + "', PJNombreAS2 = N'" + PJNombreAS2 + "', PJTipoIdentificacionAS2 = N'" + PJTipoIdentificacionAS2 + "', PJNumeroDocumentoAS2 = N'" + PJNumeroDocumentoAS2 + "', PJNombreAS3 = N'" + PJNombreAS3 + "', PJTipoIdentificacionAS3 = N'" + PJTipoIdentificacionAS3 + "', PJNumeroDocumentoAS3 = N'" + PJNumeroDocumentoAS3 + "', PJNombreAS4 = N'" + PJNombreAS4 + "', PJTipoIdentificacionAS4 = N'" + PJTipoIdentificacionAS4 + "', PJNumeroDocumentoAS4 = N'" + PJNumeroDocumentoAS4 + "', PJNombreAS5 = N'" + PJNombreAS5 + "', PJTipoIdentificacionAS5 = N'" + PJTipoIdentificacionAS5 + "', PJNumeroDocumentoAS5 = N'" + PJNumeroDocumentoAS5 + "', PJIngresosMensuales = N'" + PJIngresosMensuales + "', PJActivos = N'" + PJActivos + "', PJEgresoMensuales = N'" + PJEgresoMensuales + "', PJPasivos = N'" + PJPasivos + "', PJOtrosIngresos = N'" + PJOtrosIngresos + "', PJConceptoOtrosIngresos = N'" + PJConceptoOtrosIngresos + "' WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInfoFormPF(int IdConocimientoCliente, String TransacMonedaExtra, String TipoTransaccion, String OtroTipoTransaccion, String PFTipoProducto1, String PFNumeroProducto1, String PFEntidad1, String PFMonto1, String PFCiudad1,
                                     String PFPais1, String PFMoneda1, String PFTipoProducto2, String PFNumeroProducto2, String PFEntidad2, String PFMonto2, String PFCiudad2, String PFPais2, String PFMoneda2)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.InfoFormPF SET TransacMonedaExtra = N'" + TransacMonedaExtra + "', TipoTransaccion = N'" + TipoTransaccion + "', OtroTipoTransaccion = N'" + OtroTipoTransaccion + "', PFTipoProducto1 = N'" + PFTipoProducto1 + "', PFNumeroProducto1 = N'" + PFNumeroProducto1 + "', PFEntidad1 = N'" + PFEntidad1 + "', PFMonto1 = N'" + PFMonto1 + "', PFCiudad1 = N'" + PFCiudad1 + "', PFPais1 = N'" + PFPais1 + "', PFMoneda1 = N'" + PFMoneda1 + "', PFTipoProducto2 = N'" + PFTipoProducto2 + "', PFNumeroProducto2 = N'" + PFNumeroProducto2 + "', PFEntidad2 = N'" + PFEntidad2 + "', PFMonto2 = N'" + PFMonto2 + "', PFCiudad2 = N'" + PFCiudad2 + "', PFPais2 = N'" + PFPais2 + "', PFMoneda2 = N'" + PFMoneda2 + "' WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInfoFormSeguros(int IdConocimientoCliente, String SeguroAno1, String SeguroRamo1, String SeguroCompania1, String SeguroValor1, String SeguroTipo1, String SeguroAno2, String SeguroRamo2, String SeguroCompania2,
                                          String SeguroValor2, String SeguroTipo2, String OrigenFondos)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.InfoFormSeguros SET SeguroAno1 = N'" + SeguroAno1 + "', SeguroRamo1 = N'" + SeguroRamo1 + "', SeguroCompania1 = N'" + SeguroCompania1 + "', SeguroValor1 = N'" + SeguroValor1 + "', SeguroTipo1 = N'" + SeguroTipo1 + "', SeguroAno2 = N'" + SeguroAno2 + "', SeguroRamo2 = N'" + SeguroRamo2 + "', SeguroCompania2 = N'" + SeguroCompania2 + "', SeguroValor2 = N'" + SeguroValor2 + "', SeguroTipo2 = N'" + SeguroTipo2 + "', OrigenFondos = N'" + OrigenFondos + "' WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInfoFormEntrevista(int IdConocimientoCliente, String LugarEntrevista, String FechaEntrevista, String HoraEntrevista, String Resultado, String Observaciones1, String NombreResponsable, String FechaVerificacion, String HoraVerificacion,
                                             String NombreVerifica, String Observaciones2)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.InfoFormEntrevista SET LugarEntrevista = N'" + LugarEntrevista + "', FechaEntrevista = N'" + FechaEntrevista + "', HoraEntrevista = N'" + HoraEntrevista + "', Resultado = N'" + Resultado + "', Observaciones1 = N'" + Observaciones1 + "', NombreResponsable = N'" + NombreResponsable + "', FechaVerificacion = N'" + FechaVerificacion + "', HoraVerificacion = N'" + HoraVerificacion + "', NombreVerifica = N'" + NombreVerifica + "', Observaciones2 = N'" + Observaciones2 + "' WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInfoFormDocsInu(int IdConocimientoCliente, String Doc1, String Doc2, String Doc3, String Doc4, String Doc5, String Doc6, String Doc7, String Doc8, String Conve1, String Conve2, String Conve3, String Conve4, String Conve5, String Conve6, String Conve7, String Conve8, String Doc9, String Doc10, String Doc11, String Doc12, String Doc13, String Doc14)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("UPDATE Listas.InfoFormDocsInu SET Doc1 = N'" + Doc1 + "', Doc2 = N'" + Doc2 + "', Doc3 = N'" + Doc3 + "', Doc4 = N'" + Doc4 + "', Doc5 = N'" + Doc5 + "', Doc6 = N'" + Doc6 + "', Doc7 = N'" + Doc7 + "', Doc8 = N'" + Doc8 + "', Conve1 = N'" + Conve1 + "', Conve2 = N'" + Conve2 + "', Conve3 = N'" + Conve3 + "', Conve4 = N'" + Conve4 + "', Conve5 = N'" + Conve5 + "', Conve6 = N'" + Conve6 + "', Conve7 = N'" + Conve7 + "', Conve8 = N'" + Conve8 + "', Doc9 = N'" + Doc9 + "', Doc10 = N'" + Doc10 + "', Doc11 = N'" + Doc11 + "', Doc12 = N'" + Doc12 + "', Doc13 = N'" + Doc13 + "', Doc14 = N'" + Doc14 + "' WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------------
        #region Cargar Info
        public DataTable loadInfoGridArchivos(String IdConocimientoCliente)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT IdArchivo, NombreUsuario, FechaRegistro, UrlArchivo FROM Listas.InfoFormArchivo WHERE (IdConocimientoCliente = " + IdConocimientoCliente + ")");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        public DataTable loadCodigoInfoFormArchivo()
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT TOP (1) IdArchivo+1 AS NumRegistros FROM Listas.InfoFormArchivo ORDER BY IdArchivo DESC");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        #endregion


        #region Ingreso Formulario Coopidrogas




        //public int agregarConocimientoCliente(string NumeroFecha, string Ano)
        //{
        //    try
        //    {
        //        parameters = new OleDbParameter[4];
        //        parameter = new OleDbParameter("@IdConocimientoCliente", OleDbType.Integer, 18);
        //        parameter.Direction = ParameterDirection.Output;
        //        parameters[0] = parameter;
        //        parameter = new OleDbParameter("@IdUsuario", OleDbType.Integer);
        //        parameter.Value = IdUsuario;
        //        parameters[1] = parameter;
        //        parameter = new OleDbParameter("@NumeroFecha", OleDbType.Integer);
        //        parameter.Value = NumeroFecha;
        //        parameters[2] = parameter;
        //        parameter = new OleDbParameter("@Ano", OleDbType.Integer);
        //        parameter.Value = Ano;
        //        parameters[3] = parameter;
        //        cDataBase.conectar();
        //        cDataBase.ejecutarSPParametros("agregarConocimientoCliente", parameters);
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //    return Convert.ToInt32(parameters[0].Value);
        //}



        public void InfoFormCliente(int IdConocimientoCliente, string FechaFormulario, string ClaseVinculacion, string OtraClaseVinculacion,
            string TomadorAsegurado, string OtraTomadorAsegurado, string TomadorBeneficiario, string OtraTomadorBeneficiario,
            string AseguradoBeneficiario, string OtraAseguradoBeneficiario, string strTipoCliente)
        {
            string strConsulta = string.Empty;
            try
            {
                strConsulta = string.Format("INSERT Listas.InfoFormCliente (IdConocimientoCliente, FechaFormulario, ClaseVinculacion, " +
                    "OtraClaseVinculacion, TomadorAsegurado, OtraTomadorAsegurado, TomadorBeneficiario, OtraTomadorBeneficiario, " +
                    "AseguradoBeneficiario, OtraAseguradoBeneficiario, [TipoCliente]) VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', " +
                    "N'{6}', N'{7}', N'{8}', N'{9}', N'{10}')",
                    IdConocimientoCliente, FechaFormulario, ClaseVinculacion, OtraClaseVinculacion, TomadorAsegurado, OtraTomadorAsegurado,
                    TomadorBeneficiario, OtraTomadorBeneficiario, AseguradoBeneficiario, OtraAseguradoBeneficiario, strTipoCliente);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        #region Persona Natural
        #region Viejo
        //public void InfoFormPN(int IdConocimientoCliente, string PNPrimerApellido, string PNSegunApellido, string PNNombres, string PNTipoDocumento, string PNNumeroDocumento, string PNFechaExpedicion, string PNLugar,
        //    string PNFechaNacimiento, string PNNacionalidad, string PNOcupacionOficio, string PNActividadEconomica, string PNEstadoOcupacion, string PNCIIU, string PNEmpresaTrabajo,
        //    string PNArea, string PNCargo, string PNCiudad1, string PNDireccion, string PNTelefono1, string PNFax, string PNDireccionResidencia, string PNCiudad2, string PNTelefono2, string PNCelular,
        //    string PNPregunta1, string PNPregunta2, string PNPregunta3, string PNEspecificacionPreguntas, string PNIngresosMensuales, string PNActivos, string PNEgresoMensuales, string PNPasivos,
        //    string PNOtrosIngresos, string PNConceptoOtrosIngresos, string PNSexo, string PNCorreoElectronico)
        //{
        //    string strConsulta = string.Empty;

        //    try
        //    {
        //        strConsulta = "INSERT Listas.InfoFormPN (IdConocimientoCliente, PNPrimerApellido, PNSegunApellido, PNNombres, PNTipoDocumento, " +
        //            "PNNumeroDocumento, PNFechaExpedicion, PNLugar, PNFechaNacimiento, PNNacionalidad, PNOcupacionOficio, PNProfesion, " +
        //            "PNActividadEconomica, PNCIIU, PNEmpresaTrabajo, PNArea, PNCargo, PNCiudad1, PNDireccion, PNTelefono1, PNFax, " +
        //            "PNDireccionResidencia, PNCiudad2, PNTelefono2, PNCelular, PNPregunta1, PNPregunta2, PNPregunta3, PNEspecificacionPreguntas, " +
        //            "PNIngresosMensuales, PNActivos, PNEgresoMensuales, PNPasivos, PNOtrosIngresos, PNConceptoOtrosIngresos, PNSexo, PNCorreoElectronico) " +
        //            "VALUES (" +
        //            IdConocimientoCliente + ", N'" + PNPrimerApellido + "', N'" + PNSegunApellido + "', N'" + PNNombres + "', N'" + PNTipoDocumento +
        //            "', N'" + PNNumeroDocumento + "', N'" + PNFechaExpedicion + "', N'" + PNLugar + "', N'" + PNFechaNacimiento +
        //            "', N'" + PNNacionalidad + "', N'" + PNOcupacionOficio + "', N'" + PNActividadEconomica + "', N'" + PNEstadoOcupacion +
        //            "', N'" + PNCIIU + "', N'" + PNEmpresaTrabajo + "', N'" + PNArea + "', N'" + PNCargo + "', N'" + PNCiudad1 + "', N'" + PNDireccion +
        //            "', N'" + PNTelefono1 + "', N'" + PNFax + "', N'" + PNDireccionResidencia + "', N'" + PNCiudad2 + "', N'" + PNTelefono2 +
        //            "', N'" + PNCelular + "', N'" + PNPregunta1 + "', N'" + PNPregunta2 + "', N'" + PNPregunta3 + "', N'" + PNEspecificacionPreguntas +
        //            "', N'" + PNIngresosMensuales + "', N'" + PNActivos + "', N'" + PNEgresoMensuales + "', N'" + PNPasivos + "', N'" + PNOtrosIngresos +
        //            "', N'" + PNConceptoOtrosIngresos + "', N'" + PNSexo + "', N'" + PNCorreoElectronico + "')";

        //        cDataBase.conectar();
        //        cDataBase.ejecutarQuery(strConsulta);
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //}
        #endregion

        #region Copidrogas
        public void InfoFormPN(int IdConocimientoCliente, string PNPrimerApellido, string PNSegunApellido, string PNNombres,
            string PNTipoDocumento, string PNNumeroDocumento, string PNFechaExpedicion, string PNLugarExp,
            string PNFechaNacimiento, string PNNacionalidad, string PNOcupacionOficio,
            string PNActividadEconomica, string PNEstadoOcupacion, string PNCIIU, string PNEmpresaTrabajo,
            string PNArea, string PNCargo, string PNCiudad1, string PNDireccion, string PNTelefono1, string PNFax,
            string PNDirDomicilioPpal, string PNCiudadMun, string PNTelefono2, string PNNroCelular,
            string PNPregunta1, string PNPregunta2, string PNPregunta3, string PNEspecificacionPreguntas,
            string PNIngresosMensuales, string PNActivos, string PNEgresoMensuales, string PNPasivos,
            string PNOtrosIngresos, string PNConceptoOtrosIngresos, string PNSexo, string PNCorreoElectronico,
            string PNLugarNmto, string PNEstadoCivil, string PNDpto, string PNBarrio, string PNEstrato,
            string PNTipoInmueble, string PNTipoVivienda, string PNViviendaPropia, string PNCreditoHipo, string PNVlrCuotaMen, string PNEntidadFinanc,
            string PNOtroTipoInmueble, string PNOtroTipoVivienda)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT Listas.InfoFormPN (IdConocimientoCliente, PNPrimerApellido, " +
                    "PNSegunApellido, PNNombres, PNTipoDocumento, PNNumeroDocumento, PNFechaExpedicion, PNLugar, " +
                    "PNFechaNacimiento, PNNacionalidad, PNOcupacionOficio, PNProfesion,  PNActividadEconomica, " +
                    "PNCIIU, PNEmpresaTrabajo, PNArea, PNCargo, PNCiudad1, PNDireccion, PNTelefono1, PNFax, " +
                    "PNDireccionResidencia, PNCiudad2, PNTelefono2, PNCelular, PNPregunta1, PNPregunta2, PNPregunta3, " +
                    "PNEspecificacionPreguntas, PNIngresosMensuales, PNActivos, PNEgresoMensuales, PNPasivos, PNOtrosIngresos, " +
                    "PNConceptoOtrosIngresos, PNSexo, PNCorreoElectronico, PNLugarNmto, PNEstadoCivil, PNDpto, PNBarrio, " +
                    "PNEstrato, PNTipoInmueble, PNTipoVivienda, PNViviendaPropia, PNCreditoHipo, " +
                    "PNVlrCuotaMen, PNEntidadFinanc, PNOtroTipoInmueble, PNOtroTipoVivienda) " +
                    "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}'," +
                    " '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}'," +
                    " '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}'," +
                    " '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', '{40}'," +
                    " '{41}', '{42}', '{43}', '{44}', '{45}', '{46}', '{47}', '{48}', '{49}')", IdConocimientoCliente,
                    PNPrimerApellido, PNSegunApellido, PNNombres, PNTipoDocumento, PNNumeroDocumento,
                    PNFechaExpedicion, PNLugarExp, PNFechaNacimiento, PNNacionalidad, PNOcupacionOficio,
                    PNActividadEconomica, PNEstadoOcupacion, PNCIIU, PNEmpresaTrabajo, PNArea,
                    PNCargo, PNCiudad1, PNDireccion, PNTelefono1, PNFax,
                    PNDirDomicilioPpal, PNCiudadMun, PNTelefono2, PNNroCelular, PNPregunta1,
                    PNPregunta2, PNPregunta3, PNEspecificacionPreguntas, PNIngresosMensuales, PNActivos,
                    PNEgresoMensuales, PNPasivos, PNOtrosIngresos, PNConceptoOtrosIngresos, PNSexo,
                    PNCorreoElectronico, PNLugarNmto, PNEstadoCivil, PNDpto, PNBarrio,
                    PNEstrato, PNTipoInmueble, PNTipoVivienda, PNViviendaPropia, PNCreditoHipo,
                    PNVlrCuotaMen, PNEntidadFinanc, PNOtroTipoInmueble, PNOtroTipoVivienda);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Willis
        public void InfoFormPN(
            int IdConocimientoCliente, string PNPrimerApellido, string PNSegunApellido, string PNNombres, string PNTipoDocumento, string PNNumeroDocumento, string PNFechaExpedicion, string PNLugar,
            string PNFechaNacimiento, string PNNacionalidad, string PNOcupacionOficio, string PNProfesion, string PNActividadEconomica, string PNCIIU, string PNEmpresaTrabajo, string PNCargo, string PNCiudad1, string PNTelefono1,
            string PNDireccionResidencia, string PNCiudad2, string PNTelefono2, string PNCelular, string PNIngresosMensuales, string PNActivos, string PNEgresoMensuales, string PNPasivos, string PNOtrosIngresos,
            string PNConceptoOtrosIngresos, string PNCorreoElectronico, string PNLugarNmto, string PNDpto, string PNOtraActEconomica, string PNPatrimonio, string PNDireccionEmpresa, string PNCiudadEmpresa, string PNTelefonoEmpresa,
            string PNOtraCIIU, string PNPregunta1, string PNPregunta2, string PNPregunta3, string PNEspecificacionPreguntas, string PNPregunta4, string PNPregunta5, string PNEspecificacionPreguntas2, string PNSector1, string PNSector2,
            string PNDptoEmpresa, string PNDpto2, string PNDireccion, string PNServicio, string PNCIIUDescripcion, string PNTipoActividad, string PNTipoActividadOtra, string PNpePrimerApellido, string PNpeSegundoApellido, string PNpeNombres, string PNpeOcupacion, string PNpeCargo
            )
        {
            string strConsulta = string.Empty;
            string strConnection = string.Empty;
            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    System.Configuration.ConnectionStringSettings connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Sherlock_TestConnectionString"];
                    strConnection = connString.ToString().Trim();
                }
                sqlConnection.ConnectionString = strConnection;
                SqlCommand comando = new SqlCommand("Listas.spInfoFormPN", sqlConnection);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdConocimientoCliente", IdConocimientoCliente);
                comando.Parameters.AddWithValue("@PNPrimerApellido", PNPrimerApellido);
                comando.Parameters.AddWithValue("@PNSegunApellido", PNSegunApellido);
                comando.Parameters.AddWithValue("@PNNombres", PNNombres);
                comando.Parameters.AddWithValue("@PNTipoDocumento", PNTipoDocumento);
                comando.Parameters.AddWithValue("@PNNumeroDocumento", PNNumeroDocumento);
                comando.Parameters.AddWithValue("@PNFechaExpedicion", PNFechaExpedicion);
                comando.Parameters.AddWithValue("@PNLugar", PNLugar);
                comando.Parameters.AddWithValue("@PNFechaNacimiento", PNFechaNacimiento);
                comando.Parameters.AddWithValue("@PNNacionalidad", PNNacionalidad);
                comando.Parameters.AddWithValue("@PNOcupacionOficio", PNOcupacionOficio);
                comando.Parameters.AddWithValue("@PNProfesion", PNProfesion);
                comando.Parameters.AddWithValue("@PNActividadEconomica", PNActividadEconomica);
                comando.Parameters.AddWithValue("@PNCIIU", PNCIIU);
                comando.Parameters.AddWithValue("@PNEmpresaTrabajo", PNEmpresaTrabajo);
                comando.Parameters.AddWithValue("@PNCargo", PNCargo);
                comando.Parameters.AddWithValue("@PNCiudad1", PNCiudad1);
                comando.Parameters.AddWithValue("@PNDireccion", PNDireccion);
                comando.Parameters.AddWithValue("@PNTelefono1", PNTelefono1);
                comando.Parameters.AddWithValue("@PNDireccionResidencia", PNDireccionResidencia);
                comando.Parameters.AddWithValue("@PNCiudad2", PNCiudad2);
                comando.Parameters.AddWithValue("@PNTelefono2", PNTelefono2);
                comando.Parameters.AddWithValue("@PNCelular", PNCelular);
                comando.Parameters.AddWithValue("@PNPregunta1", PNPregunta1);
                comando.Parameters.AddWithValue("@PNPregunta2", PNPregunta2);
                comando.Parameters.AddWithValue("@PNPregunta3", PNPregunta3);
                comando.Parameters.AddWithValue("@PNEspecificacionPreguntas", PNEspecificacionPreguntas);
                comando.Parameters.AddWithValue("@PNIngresosMensuales", PNIngresosMensuales);
                comando.Parameters.AddWithValue("@PNActivos", PNActivos);
                comando.Parameters.AddWithValue("@PNEgresoMensuales", PNEgresoMensuales);
                comando.Parameters.AddWithValue("@PNPasivos", PNPasivos);
                comando.Parameters.AddWithValue("@PNOtrosIngresos", PNOtrosIngresos);
                comando.Parameters.AddWithValue("@PNConceptoOtrosIngresos", PNConceptoOtrosIngresos);
                comando.Parameters.AddWithValue("@PNCorreoElectronico", PNCorreoElectronico);
                comando.Parameters.AddWithValue("@PNLugarNmto", PNLugarNmto);
                comando.Parameters.AddWithValue("@PNDpto", PNDpto);
                comando.Parameters.AddWithValue("@PNOtraActEconomica", PNOtraActEconomica);
                comando.Parameters.AddWithValue("@PNPatrimonio", PNPatrimonio);
                comando.Parameters.AddWithValue("@PNDireccionEmpresa", PNDireccionEmpresa);
                comando.Parameters.AddWithValue("@PNCiudadEmpresa", PNCiudadEmpresa);
                comando.Parameters.AddWithValue("@PNTelefonoEmpresa", PNTelefonoEmpresa);
                comando.Parameters.AddWithValue("@PNOtraCIIU", PNOtraCIIU);
                comando.Parameters.AddWithValue("@PNPregunta4", PNPregunta4);
                comando.Parameters.AddWithValue("@PNPregunta5", PNPregunta5);
                comando.Parameters.AddWithValue("@PNEspecificacionPreguntas2", PNEspecificacionPreguntas2);
                comando.Parameters.AddWithValue("@PNSector1", PNSector1);
                comando.Parameters.AddWithValue("@PNSector2", PNSector2);
                comando.Parameters.AddWithValue("@PNDptoEmpresa", PNDptoEmpresa);
                comando.Parameters.AddWithValue("@PNDpto2", PNDpto2);
                comando.Parameters.AddWithValue("@PNServicio", PNServicio);
                comando.Parameters.AddWithValue("@PNCIIUDescripcion", PNCIIUDescripcion);
                comando.Parameters.AddWithValue("@PNTipoActividad", PNTipoActividad);
                comando.Parameters.AddWithValue("@PNTipoActividadOtra", PNTipoActividadOtra);
                comando.Parameters.AddWithValue("@PNpePrimerApellido", PNpePrimerApellido);
                comando.Parameters.AddWithValue("@PNpeSegundoApellido", PNpeSegundoApellido);
                comando.Parameters.AddWithValue("@PNpeNombres", PNpeNombres);
                comando.Parameters.AddWithValue("@PNpeOcupacion", PNpeOcupacion);
                comando.Parameters.AddWithValue("@PNpeCargo", PNpeCargo);
                sqlConnection.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        #endregion
        #endregion

        #region Persona Juridica
        //public void InfoFormPJ(int IdConocimientoCliente, string PJRazonDenominacion, string PJNIT, string PJPrimerApellido, string PJSegundoApellido,
        //    string PJNombres, string PJTipoDocumento, string PJNumeroDocumento, string PJLugarExpedicion, string PJFechaExpedicion, string PJDireccionOficina,
        //    string PJCiudad1, string PJTelefono1, string PJFax1, string PJDireccionSucursal, string PJCiudad2, string PJTelefono2, string PJFax2,
        //    string PJTipoEmpresa, string PJActividadEconomica, string PJOtraActividadEconomica, string PJCIIU, string PJNombreAS1, string PJTipoIdentificacionAS1,
        //    string PJNumeroDocumentoAS1, string PJNombreAS2, string PJTipoIdentificacionAS2, string PJNumeroDocumentoAS2, string PJNombreAS3,
        //    string PJTipoIdentificacionAS3, string PJNumeroDocumentoAS3, string PJNombreAS4, string PJTipoIdentificacionAS4, string PJNumeroDocumentoAS4,
        //    string PJNombreAS5, string PJTipoIdentificacionAS5, string PJNumeroDocumentoAS5, string PJIngresosMensuales, string PJActivos, string PJEgresoMensuales,
        //    string PJPasivos, string PJOtrosIngresos, string PJConceptoOtrosIngresos, string PJSexoRepLegal, string PJCorreoPrincipal, string PJCorreoSucursal)
        //{
        //    try
        //    {
        //        cDataBase.conectar();
        //        cDataBase.ejecutarQuery("INSERT Listas.InfoFormPJ (IdConocimientoCliente, PJRazonDenominacion, PJNIT, PJPrimerApellido, PJSegundoApellido, PJNombres, PJTipoDocumento, PJNumeroDocumento, PJLugarExpedicion, PJFechaExpedicion, PJDireccionOficina, PJCiudad1, PJTelefono1, PJFax1, PJDireccionSucursal, PJCiudad2, PJTelefono2, PJFax2, PJTipoEmpresa, PJActividadEconomica, PJOtraActividadEconomica, PJCIIU, PJNombreAS1, PJTipoIdentificacionAS1, PJNumeroDocumentoAS1, PJNombreAS2, PJTipoIdentificacionAS2, PJNumeroDocumentoAS2, PJNombreAS3, PJTipoIdentificacionAS3, PJNumeroDocumentoAS3, PJNombreAS4, PJTipoIdentificacionAS4, PJNumeroDocumentoAS4, PJNombreAS5, PJTipoIdentificacionAS5, PJNumeroDocumentoAS5, PJIngresosMensuales, PJActivos, PJEgresoMensuales, PJPasivos, PJOtrosIngresos, PJConceptoOtrosIngresos, PJSexoRepLegal, PJCorreoPrincipal, PJCorreoSucursal) VALUES (" + IdConocimientoCliente + ", N'" + PJRazonDenominacion + "', N'" + PJNIT + "', N'" + PJPrimerApellido + "', N'" + PJSegundoApellido + "', N'" + PJNombres + "', N'" + PJTipoDocumento + "', N'" + PJNumeroDocumento + "', N'" + PJLugarExpedicion + "', N'" + PJFechaExpedicion + "', N'" + PJDireccionOficina + "', N'" + PJCiudad1 + "', N'" + PJTelefono1 + "', N'" + PJFax1 + "', N'" + PJDireccionSucursal + "', N'" + PJCiudad2 + "', N'" + PJTelefono2 + "', N'" + PJFax2 + "', N'" + PJTipoEmpresa + "', N'" + PJActividadEconomica + "', N'" + PJOtraActividadEconomica + "', N'" + PJCIIU + "', N'" + PJNombreAS1 + "', N'" + PJTipoIdentificacionAS1 + "', N'" + PJNumeroDocumentoAS1 + "', N'" + PJNombreAS2 + "', N'" + PJTipoIdentificacionAS2 + "', N'" + PJNumeroDocumentoAS2 + "', N'" + PJNombreAS3 + "', N'" + PJTipoIdentificacionAS3 + "', N'" + PJNumeroDocumentoAS3 + "', N'" + PJNombreAS4 + "', N'" + PJTipoIdentificacionAS4 + "', N'" + PJNumeroDocumentoAS4 + "', N'" + PJNombreAS5 + "', N'" + PJTipoIdentificacionAS5 + "', N'" + PJNumeroDocumentoAS5 + "', N'" + PJIngresosMensuales + "', N'" + PJActivos + "', N'" + PJEgresoMensuales + "', N'" + PJPasivos + "', N'" + PJOtrosIngresos + "', N'" + PJConceptoOtrosIngresos + "',N'" + PJSexoRepLegal + "',N'" + PJCorreoPrincipal + "',N'" + PJCorreoSucursal + "')");
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //}

        public void InfoFormPJ(int IdConocimientoCliente, string PJRazonDenominacion, string PJNIT, string PJPrimerApellido, string PJSegundoApellido,
            string PJNombres, string PJTipoDocumento, string PJNumeroDocumento, string PJLugarExpedicion, string PJFechaExpedicion, string PJDireccionOficina,
            string PJCiudad1, string PJTelefono1, string PJFax1, string PJDireccionSucursal, string PJCiudad2, string PJTelefono2, string PJFax2,
            string PJTipoEmpresa, string PJActividadEconomica, string PJOtraActividadEconomica, string PJCIIU, string PJNombreAS1, string PJTipoIdentificacionAS1,
            string PJNumeroDocumentoAS1, string PJNombreAS2, string PJTipoIdentificacionAS2, string PJNumeroDocumentoAS2, string PJNombreAS3,
            string PJTipoIdentificacionAS3, string PJNumeroDocumentoAS3, string PJNombreAS4, string PJTipoIdentificacionAS4, string PJNumeroDocumentoAS4,
            string PJNombreAS5, string PJTipoIdentificacionAS5, string PJNumeroDocumentoAS5, string PJIngresosMensuales, string PJActivos, string PJEgresoMensuales,
            string PJPasivos, string PJOtrosIngresos, string PJConceptoOtrosIngresos, string PJSexoRepLegal, string PJCorreoPrincipal, string PJCorreoSucursal,
            string PJTipoPersonaJur, string PJSociedadComercial, string PJCapitalSocial, string PJFechaConstitucion,
            string PJDocumentoConstitucion, string PJFechaRegistro, string PJMatriculaMercantil, string PJRegSuperSolidaria,
            string PJNombresRepLegalPpal, string PJTipoDocRepLegalPpal, string PJDocumentoRepLegalPpal, string PJNombresRepLegal1, string PJTipoDocRepLegal1,
            string PJDocumentoRepLegal1, string PJNombresRepLegal2, string PJTipoDocRepLegal2, string PJDocumentoRepLegal2,
            string PJNombresRepLegal3, string PJTipoDocRepLegal3, string PJDocumentoRepLegal3, string PJNombresRepLegal4,
            string PJTipoDocRepLegal4, string PJDocumentoRepLegal4, string PJTelefonoRepLegal, string PJDptoCiudadRepLegal,
            string PJNombresRepLegalSupl, string PJTipoDocRepLegalSupl, string PJDocumentoRepLegalSupl, string PJTelefonoRepLegalSupl,
            string PJDptoCiudadRepLegalSupl, string PJDescActEcoPpal, string PJCodCIIU2)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Listas].[InfoFormPJ]([IdConocimientoCliente], " +
                    "[PJRazonDenominacion], [PJNIT], [PJPrimerApellido], [PJSegundoApellido], [PJNombres], " +
                    "[PJTipoDocumento], [PJNumeroDocumento], [PJLugarExpedicion], [PJFechaExpedicion], [PJDireccionOficina], [PJCiudad1]," +
                    "[PJTelefono1],[PJFax1], [PJDireccionSucursal], [PJCiudad2], [PJTelefono2], [PJFax2], [PJTipoEmpresa]," +
                    "[PJActividadEconomica], [PJOtraActividadEconomica], [PJCIIU], [PJNombreAS1], [PJTipoIdentificacionAS1]," +
                    "[PJNumeroDocumentoAS1], [PJNombreAS2], [PJTipoIdentificacionAS2], [PJNumeroDocumentoAS2], [PJNombreAS3]," +
                    "[PJTipoIdentificacionAS3], [PJNumeroDocumentoAS3], [PJNombreAS4], [PJTipoIdentificacionAS4], [PJNumeroDocumentoAS4]," +
                    "[PJNombreAS5], [PJTipoIdentificacionAS5], [PJNumeroDocumentoAS5], [PJIngresosMensuales], [PJActivos]," +
                    "[PJEgresoMensuales], [PJPasivos], [PJOtrosIngresos], [PJConceptoOtrosIngresos], [PJSexoRepLegal]," +
                    "[PJCorreoPrincipal], [PJCorreoSucursal], [PJTipoPersonaJur], [PJSociedadComercial], [PJCapitalSocial]," +
                    "[PJFechaConstitucion], [PJDocumentoConstitucion], [PJFechaRegistro], [PJMatriculaMercantil], [PJRegSuperSolidaria]," +
                    "[PJNombresRepLegalPpal], [PJDocumentoRepLegalPpal], [PJNombresRepLegal1], [PJTipoDocRepLegal1], [PJDocumentoRepLegal1]," +
                    "[PJNombresRepLegal2], [PJTipoDocRepLegal2], [PJDocumentoRepLegal2], [PJNombresRepLegal3], [PJTipoDocRepLegal3]," +
                    "[PJDocumentoRepLegal3], [PJNombresRepLegal4], [PJTipoDocRepLegal4], [PJDocumentoRepLegal4], [PJTelefonoRepLegal]," +
                    "[PJDptoCiudadRepLegal], [PJNombresRepLegalSupl], [PJTipoDocRepLegalSupl], [PJDocumentoRepLegalSupl]," +
                    "[PJTelefonoRepLegalSupl], [PJDptoCiudadRepLegalSupl], [PJDescActEcoPpal], [PJCodCIIU2], [PJTipoDocRepLegalPpal]) " +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', " +
                    "N'{8}', N'{9}', N'{10}', N'{11}', N'{12}', N'{13}', N'{14}', N'{15}', N'{16}', " +
                    "N'{17}', N'{18}', N'{19}', N'{20}', N'{21}', N'{22}', N'{23}', N'{24}', N'{25}', " +
                    "N'{26}', N'{27}', N'{28}', N'{29}', N'{30}', N'{31}', N'{32}', N'{33}', " +
                    "N'{34}', N'{35}', N'{36}', N'{37}', N'{38}', N'{39}', N'{40}', N'{41}', " +
                    "N'{42}', N'{43}', N'{44}', N'{45}',  " +
                    "N'{46}', N'{47}', N'{48}', N'{49}', N'{50}', N'{51}', N'{52}', " +
                    "N'{53}', N'{54}', N'{55}', N'{56}', N'{57}', " +
                    "N'{58}', N'{59}', N'{60}', N'{61}', N'{62}', N'{63}', " +
                    "N'{64}', N'{65}', N'{66}', N'{67}', N'{68}', N'{69}', " +
                    "N'{70}', N'{71}', N'{72}', N'{73}', N'{74}', N'{75}', N'{76}', N'{77}' )", IdConocimientoCliente,
                    PJRazonDenominacion, PJNIT, PJPrimerApellido, PJSegundoApellido, PJNombres, PJTipoDocumento, PJNumeroDocumento,
                    PJLugarExpedicion, PJFechaExpedicion, PJDireccionOficina, PJCiudad1, PJTelefono1, PJFax1, PJDireccionSucursal, PJCiudad2, PJTelefono2,
                    PJFax2, PJTipoEmpresa, PJActividadEconomica, PJOtraActividadEconomica, PJCIIU, PJNombreAS1, PJTipoIdentificacionAS1, PJNumeroDocumentoAS1, PJNombreAS2,
                    PJTipoIdentificacionAS2, PJNumeroDocumentoAS2, PJNombreAS3, PJTipoIdentificacionAS3, PJNumeroDocumentoAS3, PJNombreAS4, PJTipoIdentificacionAS4,
                    PJNumeroDocumentoAS4, PJNombreAS5, PJTipoIdentificacionAS5, PJNumeroDocumentoAS5, PJIngresosMensuales, PJActivos, PJEgresoMensuales, PJPasivos,
                    PJOtrosIngresos, PJConceptoOtrosIngresos, PJSexoRepLegal, PJCorreoPrincipal, PJCorreoSucursal,
                    PJTipoPersonaJur, PJSociedadComercial, PJCapitalSocial, PJFechaConstitucion, PJDocumentoConstitucion, PJFechaRegistro, PJMatriculaMercantil,
                    PJRegSuperSolidaria, PJNombresRepLegalPpal, PJDocumentoRepLegalPpal, PJNombresRepLegal1, PJTipoDocRepLegal1,
                    PJDocumentoRepLegal1, PJNombresRepLegal2, PJTipoDocRepLegal2, PJDocumentoRepLegal2, PJNombresRepLegal3, PJTipoDocRepLegal3,
                    PJDocumentoRepLegal3, PJNombresRepLegal4, PJTipoDocRepLegal4, PJDocumentoRepLegal4, PJTelefonoRepLegal, PJDptoCiudadRepLegal,
                    PJNombresRepLegalSupl, PJTipoDocRepLegalSupl, PJDocumentoRepLegalSupl, PJTelefonoRepLegalSupl, PJDptoCiudadRepLegalSupl, PJDescActEcoPpal,
                    PJCodCIIU2, PJTipoDocRepLegalPpal);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Info Financiera
        //public void InfoFormPF(int IdConocimientoCliente, String TransacMonedaExtra, String TipoTransaccion, String OtroTipoTransaccion,
        //    String PFTipoProducto1, String PFNumeroProducto1, String PFEntidad1, String PFMonto1, String PFCiudad1,
        //    String PFPais1, String PFMoneda1, String PFTipoProducto2, String PFNumeroProducto2, String PFEntidad2,
        //    String PFMonto2, String PFCiudad2, String PFPais2, String PFMoneda2)
        //{
        //    try
        //    {
        //        cDataBase.conectar();
        //        cDataBase.ejecutarQuery("INSERT Listas.InfoFormPF (IdConocimientoCliente, TransacMonedaExtra, TipoTransaccion, OtroTipoTransaccion, PFTipoProducto1, PFNumeroProducto1, PFEntidad1, PFMonto1, PFCiudad1, PFPais1, PFMoneda1, PFTipoProducto2, PFNumeroProducto2, PFEntidad2, PFMonto2, PFCiudad2, PFPais2, PFMoneda2) VALUES (" + IdConocimientoCliente + ", N'" + TransacMonedaExtra + "', N'" + TipoTransaccion + "', N'" + OtroTipoTransaccion + "', N'" + PFTipoProducto1 + "', N'" + PFNumeroProducto1 + "', N'" + PFEntidad1 + "', N'" + PFMonto1 + "', N'" + PFCiudad1 + "', N'" + PFPais1 + "', N'" + PFMoneda1 + "', N'" + PFTipoProducto2 + "', N'" + PFNumeroProducto2 + "', N'" + PFEntidad2 + "', N'" + PFMonto2 + "', N'" + PFCiudad2 + "', N'" + PFPais2 + "', N'" + PFMoneda2 + "')");
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //}

        public void InfoFormPF(int IdConocimientoCliente, string TransacMonedaExtra, string TipoTransaccion, string OtroTipoTransaccion,
            string PFTipoProducto1, string PFNumeroProducto1, string PFEntidad1, string PFMonto1, string PFCiudad1,
            string PFPais1, string PFMoneda1, string PFTipoProducto2, string PFNumeroProducto2, string PFEntidad2,
            string PFMonto2, string PFCiudad2, string PFPais2, string PFMoneda2,
            string PFCtaMonedaExtra, string PFNroCtaMonedaExtra, string PFBancoCtaMonedaExtra, string PFCiudadCtaMonedaExtra,
            string PFPaisCtaMonedaExtra, string PFMonedaCtaMonedaExtra)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Listas].[InfoFormPF]([IdConocimientoCliente], [TransacMonedaExtra], [TipoTransaccion], [OtroTipoTransaccion], " +
                    "[PFTipoProducto1],[PFNumeroProducto1],[PFEntidad1],[PFMonto1],[PFCiudad1],[PFPais1],[PFMoneda1],[PFTipoProducto2], " +
                    "[PFNumeroProducto2],[PFEntidad2],[PFMonto2],[PFCiudad2],[PFPais2],[PFMoneda2],[PFCtaMonedaExtra],[PFNroCtaMonedaExtra], " +
                    "[PFBancoCtaMonedaExtra],[PFCiudadCtaMonedaExtra],[PFPaisCtaMonedaExtra],[PFMonedaCtaMonedaExtra]) " +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', " +
                    "N'{7}', N'{8}', N'{9}', N'{10}' , N'{11}', N'{12}', N'{13}', " +
                    "N'{14}', N'{15}', N'{16}', N'{17}', N'{18}', N'{19}', N'{20}', " +
                    "N'{21}', N'{22}', N'{23}')", IdConocimientoCliente,
                    TransacMonedaExtra, TipoTransaccion, OtroTipoTransaccion, PFTipoProducto1, PFNumeroProducto1, PFEntidad1,
                    PFMonto1, PFCiudad1, PFPais1, PFMoneda1, PFTipoProducto2, PFNumeroProducto2, PFEntidad2,
                    PFMonto2, PFCiudad2, PFPais2, PFMoneda2, PFCtaMonedaExtra, PFNroCtaMonedaExtra, PFBancoCtaMonedaExtra,
                    PFCiudadCtaMonedaExtra, PFPaisCtaMonedaExtra, PFMonedaCtaMonedaExtra);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region
        //public void InfoFormSeguros(int IdConocimientoCliente, String SeguroAno1, String SeguroRamo1, String SeguroCompania1, String SeguroValor1, String SeguroTipo1, String SeguroAno2, String SeguroRamo2, String SeguroCompania2,
        //                            String SeguroValor2, String SeguroTipo2, String OrigenFondos)
        //{
        //    try
        //    {
        //        cDataBase.conectar();
        //        cDataBase.ejecutarQuery("INSERT Listas.InfoFormSeguros (IdConocimientoCliente, SeguroAno1, SeguroRamo1, SeguroCompania1, SeguroValor1, SeguroTipo1, SeguroAno2, SeguroRamo2, SeguroCompania2, SeguroValor2, SeguroTipo2, OrigenFondos) VALUES (" + IdConocimientoCliente + ", N'" + SeguroAno1 + "', N'" + SeguroRamo1 + "', N'" + SeguroCompania1 + "', N'" + SeguroValor1 + "', N'" + SeguroTipo1 + "', N'" + SeguroAno2 + "', N'" + SeguroRamo2 + "', N'" + SeguroCompania2 + "', N'" + SeguroValor2 + "', N'" + SeguroTipo2 + "', N'" + OrigenFondos + "')");
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //}
        #endregion

        #region
        //public void InfoFormDocsInu(int IdConocimientoCliente, String Doc1, String Doc2, String Doc3, String Doc4, String Doc5, String Doc6, String Doc7, String Doc8, String UrlDocPdf, String Conve1, String Conve2, String Conve3, String Conve4, String Conve5, String Conve6, String Conve7, String Conve8, String Doc9, String Doc10, String Doc11, String Doc12, String Doc13, String Doc14)
        //{
        //    try
        //    {
        //        cDataBase.conectar();
        //        cDataBase.ejecutarQuery("INSERT Listas.InfoFormDocsInu (IdConocimientoCliente, Doc1, Doc2, Doc3, Doc4, Doc5, Doc6, Doc7, Doc8, UrlDocPdf, Conve1, Conve2, Conve3, Conve4, Conve5, Conve6, Conve7, Conve8, Doc9, Doc10, Doc11, Doc12, Doc13, Doc14) VALUES (" + IdConocimientoCliente + ", N'" + Doc1 + "', N'" + Doc2 + "', N'" + Doc3 + "', N'" + Doc4 + "', N'" + Doc5 + "', N'" + Doc6 + "', N'" + Doc7 + "', N'" + Doc8 + "', N'" + UrlDocPdf + "', N'" + Conve1 + "', N'" + Conve2 + "', N'" + Conve3 + "', N'" + Conve4 + "', N'" + Conve5 + "', N'" + Conve6 + "', N'" + Conve7 + "', N'" + Conve8 + "', N'" + Doc9 + "', N'" + Doc10 + "', N'" + Doc11 + "', N'" + Doc12 + "', N'" + Doc13 + "', N'" + Doc14 + "')");
        //        cDataBase.desconectar();
        //    }
        //    catch (Exception ex)
        //    {
        //        cDataBase.desconectar();
        //        cError.errorMessage(ex.Message + ", " + ex.StackTrace);
        //        throw new Exception(ex.Message);
        //    }
        //}
        #endregion

        #region Entrevista
        public void InfoFormEntrevista(int IdConocimientoCliente,
            string LugarEntrevista, string FechaEntrevista, string HoraEntrevista, string Resultado, string Observaciones1,
            string NombreResponsable,
            string FechaVerificacion, string HoraVerificacion, string NombreVerifica, string Observaciones2,
            string strValidaFirma, string strValidaHuella, string strValidaEntrevista)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT Listas.InfoFormEntrevista (IdConocimientoCliente, " +
                    "LugarEntrevista, FechaEntrevista, HoraEntrevista, Resultado, Observaciones1, " +
                    "NombreResponsable, FechaVerificacion, HoraVerificacion, NombreVerifica, Observaciones2, [ValidaFirma], [ValidaHuella], [ValidaEntrevista]) " +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', " +
                    "N'{7}', N'{8}', N'{9}', N'{10}', N'{11}', N'{12}', N'{13}')",
                    IdConocimientoCliente, LugarEntrevista, FechaEntrevista, HoraEntrevista, Resultado, Observaciones1, NombreResponsable,
                    FechaVerificacion, HoraVerificacion, NombreVerifica, Observaciones2, strValidaFirma, strValidaHuella, strValidaEntrevista);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Droguerias Copidrogas
        public void InfoFormDroguerias(int IdConocimientoCliente, string NombreDrogPpal, string NitDrogPpal, string DirDrogPpal,
            string DptoDrogPpal, string CiudadDrogPpal, string BarrioDrogPpal, string TelDrogPpal,
            string NombreDrog2, string DirDrog2, string DptoDrog2, string CiudadDrog2, string BarrioDrog2, string TelDrog2,
            string NombreDrog3, string DirDrog3, string DptoDrog3, string CiudadDrog3, string BarrioDrog3, string TelDrog3)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Listas].[InfoFormDrogueria]([IdConocimientoCliente], [NombreDrogueriaPpal], " +
                    "[NitDrogueriaPpal],[DirDrogueriaPpal],[DptoDrogueriaPpal],[CiudadDrogueriaPpal]," +
                    "[BarrioDrogueriaPpal],[TelDrogueriaPpal],[NombreDrogueria1],[DirDrogueria1]," +
                    "[DptoDrogueria1],[CiudadDrogueria1],[BarrioDrogueria1],[TelDrogueria1],[NombreDrogueria2]," +
                    "[DirDrogueria2],[DptoDrogueria2],[CiudadDrogueria2],[BarrioDrogueria2],[TelDrogueria2])" +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', N'{10}', N'{11}', N'{12}', " +
                    "N'{13}', N'{14}', N'{15}', N'{16}', N'{17}', N'{18}', N'{19}')", IdConocimientoCliente,
                    NombreDrogPpal, NitDrogPpal, DirDrogPpal, DptoDrogPpal, CiudadDrogPpal, BarrioDrogPpal, TelDrogPpal,
                    NombreDrog2, DirDrog2, DptoDrog2, CiudadDrog2, BarrioDrog2, TelDrog2,
                    NombreDrog3, DirDrog3, DptoDrog3, CiudadDrog3, BarrioDrog3, TelDrog3);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Sometimiento Copidrogas
        public void InfoFormSometimiento(int IdConocimientoCliente, string AportesSoc, string VlrAperturaCopicredito, string CompromisoAhorroMen,
           string CuotaAdm, string CuotaAfilAsocoldro, string Total, string FechaEntregaForm)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Listas].[InfoFormSometimiento]([IdConocimientoCliente],[AportesSoc], " +
                    "[VlrAperturaCopicredito], [CompromisoAhorroMen], [CuotaAdm], [CuotaAfilAsocoldro], [Total], [FechaEntregaForm] )" +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}')", IdConocimientoCliente,
                    AportesSoc, VlrAperturaCopicredito, CompromisoAhorroMen, CuotaAdm, CuotaAfilAsocoldro, Total, FechaEntregaForm);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Info Copidrogas
        public void InfoFormCopidrogas(int IdConocimientoCliente, string ActaNoAdmision, string FechaAdmision, string FavorableAdmision,
           string AplazadoAdmision, string DesfavorableAdmision, string VisitaEfecAdmision, string ObsAdmision, string NombreCoordinadorAdmision,
            string NombreSecretarioAdmision, string ActaNoAdmon, string FechaAdmon, string FavorableAdmon,
           string AplazadoAdmon, string DesfavorableAdmon, string VisitaEfecAdmon, string ObsAdmon, string NombreCoordinadorAdmon,
            string NombreSecretarioAdmon)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Listas].[InfoFormCopidrogas] ([IdConocimientoCliente], [ActaNoAdmision], " +
                    "[FechaAdmision], [FavorableAdmision], [AplazadoAdmision], [DesfavorableAdmision], [VisitaEfectuadaAdmision], " +
                    "[ObservacionesAdmision], [NombreCoordinadorAdmision], [NombreSecretarioAdmision], " +
                    "[ActaNoAdmon], [FechaAdmon], [FavorableAdmon], [AplazadoAdmon], [DesfavorableAdmon], [VisitaEfectuadaAdmon], [ObservacionesAdmon], " +
                    "[NombreCoordinadorAdmon], [NombreSecretarioAdmon])" +
                    "VALUES ({0}, N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}', N'{10}', N'{11}', N'{12}', " +
                    "N'{13}', N'{14}', N'{15}', N'{16}', N'{17}', N'{18}')", IdConocimientoCliente, ActaNoAdmision,
                    FechaAdmision, FavorableAdmision, AplazadoAdmision, DesfavorableAdmision, VisitaEfecAdmision,
                    ObsAdmision, NombreCoordinadorAdmision, NombreSecretarioAdmision,
                    ActaNoAdmon, FechaAdmon, FavorableAdmon, AplazadoAdmon, DesfavorableAdmon, VisitaEfecAdmon, ObsAdmon,
                    NombreCoordinadorAdmon, NombreSecretarioAdmon);

                cDataBase.conectar();
                cDataBase.ejecutarQuery(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #endregion



        #region PDFs

        public void agregarArchivo(String IdConocimientoCliente, String UrlArchivo)
        {
            try
            {
                cDataBase.conectar();
                cDataBase.ejecutarQuery("INSERT INTO Listas.InfoFormArchivo (IdConocimientoCliente, NombreUsuario, FechaRegistro, UrlArchivo) VALUES (" +
                    IdConocimientoCliente + ", '" + NombreUsuario + "', GETDATE(), N'" + UrlArchivo + "')");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public void mtdAgregarArchivoPdf(string strIdConocimientoCliente,
            string strUrlArchivo, byte[] bPdfData)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("INSERT INTO [Listas].[InfoFormArchivo] ([IdConocimientoCliente], [NombreUsuario], [FechaRegistro], [UrlArchivo], [ArchivoPDF]) VALUES ({0}, N'{1}', GETDATE(), N'{2}', @PdfData)",
                    strIdConocimientoCliente, NombreUsuario, strUrlArchivo);

                cDataBase.mtdConectarSql();
                cDataBase.mtdEjecutarConsultaSQL(strConsulta, bPdfData);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }

        public byte[] mtdDescargarArchivoPdf(string strNombreArchivo)
        {
            #region Vars
            byte[] bInfo = null;
            string strConsulta = string.Empty;
            #endregion Vars

            try
            {
                strConsulta = string.Format("SELECT [UrlArchivo], [ArchivoPDF] FROM [Listas].[InfoFormArchivo] WHERE [UrlArchivo] = N'{0}'", strNombreArchivo);

                cDataBase.mtdConectarSql();
                bInfo = cDataBase.mtdEjecutarConsultaSqlPdf(strConsulta);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return bInfo;
        }

        #endregion PDFs

        #region LoadFormularioCliente
        public DataTable loadInfoGridCliente(string DocumentoIdentidad)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdRegistro],[NumeroDocumento],[IdConocimientoCliente],[PrimerApellido],[SegundoApellido],[Nombres],[Estado],[FechaRegistro],[IdUsuario],[Usuario]"
                + " FROM [Listas].[vwClienteFormulariosEnviados]"
                + " where Estado = 'Enviado' and NumeroDocumento =  '" + DocumentoIdentidad+"'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }
        public DataTable loadInfoGridJudicial(string DocumentoIdentidad)
        {
            DataTable dtInformacion = new DataTable();
            try
            {
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta("SELECT [IdRegistro],[NumeroDocumento],[IdConocimientoCliente],[PrimerApellido],[SegundoApellido],[Nombres],[Estado],[FechaRegistro],[IdUsuario],[Usuario]"
                + " FROM [Listas].[vwJuricoFormulariosEnviados]"
                + " where Estado = 'Enviado' and NumeroDocumento = '" + DocumentoIdentidad+"'");
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return dtInformacion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdConocimientoCliente"></param>
        public void mtdAprobarFormulario(string strIdConocimientoCliente)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("UPDATE [Listas].[FormularioConocimientoCliente] SET [Estado] = 2, [Aprobado] =1, [FechaAprobado] = GETDATE()"
                    + " where [IdConocimientoCliente] = {0}",
                    strIdConocimientoCliente);

                cDataBase.mtdConectarSql();
                cDataBase.mtdEjecutarConsultaSQL(strConsulta);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdConocimientoCliente"></param>
        public void mtdRechazarFormulario(string strIdConocimientoCliente)
        {
            string strConsulta = string.Empty;

            try
            {
                strConsulta = string.Format("UPDATE [Listas].[FormularioConocimientoCliente] SET [Estado] = 3, [Rechazado] =1, [FechaRechazado] = GETDATE()"
                    + " where [IdConocimientoCliente] = {0}",
                    strIdConocimientoCliente);

                cDataBase.mtdConectarSql();
                cDataBase.mtdEjecutarConsultaSQL(strConsulta);
                cDataBase.mtdDesconectarSql();
            }
            catch (Exception ex)
            {
                cDataBase.mtdDesconectarSql();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public DataTable buscarClienteFCC(string TipoPersona, string PApellido, string SApellido, string Nombre, string NumeIden, string RazonSocial, string Nit, string FechaIni, string FechaFin, string Estado)
        {
            string condicion = string.Empty, strConsulta = string.Empty;
            DataTable dtInformacion = new DataTable();

            try
            {
                if (TipoPersona != string.Empty && TipoPersona != "-1")
                    condicion = "WHERE (a.IdTipoPersona = '" + TipoPersona + "') ";

                if (PApellido != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.PrimerApellido LIKE N'%" + PApellido + "%') ";
                    else
                        condicion = condicion + "AND (a.PrimerApellido LIKE N'%" + PApellido + "%') ";
                }

                if (SApellido != string.Empty)
                    condicion = "WHERE (a.SegundoApellido LIKE N'%" + SApellido + "%') ";

                if (SApellido != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.SegundoApellido LIKE N'%" + SApellido + "%') ";
                    else
                        condicion = condicion + "AND (a.SegundoApellido LIKE N'%" + SApellido + "%') ";
                }

                if (Nombre != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.NombrePerNatural LIKE N'%" + Nombre + "%') ";
                    else
                        condicion = condicion + "AND (a.NombrePerNatural LIKE N'%" + Nombre + "%') ";
                }

                if (NumeIden != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.Documento LIKE N'%" + NumeIden + "%') ";
                    else
                        condicion = condicion + "AND (a.Documento LIKE N'%" + NumeIden + "%') ";
                }

                if (Nit != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.Documento LIKE N'%" + Nit + "%') ";
                    else
                        condicion = condicion + "AND (a.Documento LIKE N'%" + Nit + "%') ";
                }

                if (FechaIni != string.Empty && FechaFin != string.Empty)
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.FechaDiligenciamiento BETWEEN '" + FechaIni + " 00:00:00' AND '" + FechaFin + " 23:59:59')";
                    else
                        condicion = condicion + "AND (a.FechaDiligenciamiento BETWEEN '" + FechaIni + " 00:00:00' AND '" + FechaFin + " 23:59:59')";
                }

                if (Estado != string.Empty && Estado != "0")
                {
                    if (condicion.ToString().Trim() == "")
                        condicion = "WHERE (a.IdEstado = " + Estado + ") ";
                    else
                        condicion = condicion + "AND (a.IdEstado = " + Estado + ") ";
                }


                strConsulta = string.Format("SELECT  a.Id,a.IdTipoPersona,d.Descripcion TipoPersona, c.Descripcion TipoDocumento, a.Documento, " +
                        "CASE WHEN RazonSocial IS NULL THEN NombrePerNatural + ' ' + PrimerApellido + ' ' + SegundoApellido ELSE RazonSocial END AS nombreCliente, a.IdEstado, " +
                        "b.Descipcion AS estadoFormulario, CONVERT(VARCHAR, a.FechaDiligenciamiento, 120) AS fechaActualizacion " +
                        "FROM dbo.FormularioDatosBasicos AS a " +
                        "INNER JOIN dbo.Estado AS b ON a.IdEstado = b.IdEstado " +
                        "INNER JOIN dbo.TipoDocumento c ON a.IdTipoDocumento = c.IdTipoDoc " +
                        "INNER JOIN dbo.TipoPersona d on a.IdTipoPersona = d.IdTipoPersona " +
                        "{0} order by FechaDiligenciamiento,Documento", condicion);
                cDataBase.conectar();
                dtInformacion = cDataBase.ejecutarConsulta(strConsulta);
                cDataBase.desconectar();
            }
            catch (Exception ex)
            {
                cDataBase.desconectar();
                cError.errorMessage(ex.Message + ", " + ex.StackTrace);
                throw new Exception(ex.Message);
            }

            return dtInformacion;
        }

    }
}