<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuAdmin.ascx.cs" Inherits="ListasSarlaft.UserControls.MenuAdmin" %>
<style type="text/css">
    .MyMenuClass {
        table-layout: fixed;
    }

    .menu1 {
        border-style: solid;
        border-width: 2px;
        width: 150px;
    }

    .menu2 {
        border-style: solid;
        border-width: 2px;
        width: 150px;
    }

    .menu3 {
        border-style: solid;
        border-width: 2px;
        width: 150px;
    }

    .style2 {
        width: 667px;
    }
</style>
<table width="100%">
    <tr align="right" bgcolor="#333399">
        <td>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Icons/usuarios.png" ToolTip="Usuario" />&nbsp;&nbsp;
            <asp:Label ID="LblUsuarioEtiqueta0" runat="server" Font-Names="Calibri"
                Font-Size="Small" ForeColor="White" Text="Usuario:" Font-Bold="False"></asp:Label>&nbsp;
            <asp:Label ID="LblUsuarioDato" runat="server" Font-Names="Calibri"
                Font-Size="Small" ForeColor="White" Height="16px"></asp:Label>
            &nbsp;&nbsp;<asp:Label ID="LblNombreEtiqueta" runat="server" Font-Names="Calibri"
                Font-Size="Small" ForeColor="White" Font-Bold="False" Font-Italic="False">Nombre Usuario:</asp:Label>&nbsp;
            <asp:Label ID="LblNombreDato" runat="server" Font-Names="Calibri"
                Font-Size="Small" ForeColor="White"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Icons/reloj.png" ToolTip="Fecha y hora de ingreso" />&nbsp;&nbsp;
            <asp:Label ID="LblNombreTime" runat="server" Font-Names="Calibri"
                Font-Size="Small" ForeColor="White" ToolTip="Fecha y hora de ingreso"></asp:Label>
        </td>
    </tr>
</table>
<table bgcolor="#E8E9EA" width="100%">
    <tr>
        <td align="left">
            <table id="TBmenu" runat="server" visible="false">
                <tr>
                    <td class="style2">
                        <asp:Menu ID="Menu3" runat="server" DynamicHorizontalOffset="2" Font-Names="Franklin Gothic Medium"
                            Font-Size="Small" Orientation="Horizontal" ForeColor="DarkBlue" BackColor="#E8E9EA"
                            StaticSubMenuIndent="15px" RenderingMode="Table" MaximumDynamicDisplayLevels="4">
                            <DynamicHoverStyle BackColor="DarkBlue" ForeColor="White" />
                            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <DynamicMenuStyle BackColor="#E8E9EA" />
                            <DynamicSelectedStyle BackColor="#5D7B9D" />
                            <Items>
                                <asp:MenuItem Text="Mi cuenta" Value="Mi cuenta">
                                    <asp:MenuItem NavigateUrl="~/Formularios/AdminUsers/CambioContrasena.aspx" Text="Cambio contraseña"
                                        Value="Cambio contraseña"></asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Seguridad" Value="Seguridad">
                                    <asp:MenuItem Text="Roles" Value="Roles">
                                        <asp:MenuItem NavigateUrl="~/Formularios/AdminUsers/Roles.aspx" Text="Roles" Value="Roles"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Agregar usuario" Value="Agregar usuario">
                                        <asp:MenuItem NavigateUrl="~/Formularios/AdminUsers/AgregarUsuario.aspx" Text="Agregar usuario"
                                            Value="Agregar usuario"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Consultar usuario" Value="Consultar usuario">
                                        <asp:MenuItem NavigateUrl="~/Formularios/AdminUsers/ConsultarUsuario.aspx" Text="Consultar usuario"
                                            Value="Consultar usuario"></asp:MenuItem>
                                    </asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Parametrización General" Value="Parametrización General">
                                    <asp:MenuItem Text="Cargos" Value="Cargos" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmCargos.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Notificaciones" Value="Notificaciones">
                                        <asp:MenuItem Text="Correos Destino" Value="Correos Destino" NavigateUrl="~/Formularios/Notificaciones/Admin/NtfAdmCorreosDestino.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Correos Enviados" Value="Correos Enviados" NavigateUrl="~/Formularios/Notificaciones/Admin/NtfAdmCorreosEnviados.aspx"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Notificaciones/Admin/NotificacionesTest.aspx" Text="Validar notificaciones" Value="Validar notificaciones"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Otros Tipos" Value="OtrosTipos" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmOtrosTipos.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Jerarquía Organizacional" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmJerarquiaOrg.aspx"
                                        Value="Jerarquía Organizacional"></asp:MenuItem>
                                    <asp:MenuItem Text="Ubicación Geográfica" Value="Ubicación Geográfica">
                                        <asp:MenuItem Text="Regiones" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmRegiones.aspx"
                                            Value="Regiones"></asp:MenuItem>
                                        <asp:MenuItem Text="Países" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmPaises.aspx"
                                            Value="Paises"></asp:MenuItem>
                                        <asp:MenuItem Text="Departamentos" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmDepartamentos.aspx"
                                            Value="Departamentos"></asp:MenuItem>
                                        <asp:MenuItem Text="Ciudades" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmCiudades.aspx"
                                            Value="Ciudades"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmOficinas.aspx"
                                            Text="Oficinas / Sucursales" Value="Oficinas / Sucursales"></asp:MenuItem>
                                    </asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Auditoría" Value="Auditoría">
                                    <asp:MenuItem Text="Parametrización" Value="Parametrización">
                                        <asp:MenuItem Text="Días No Laborables" Value="DiasNoLaborables" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmDiasNoLaborables.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Horas Laborables" Value="HorasLaborables" NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmHorasLaborables.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Tipo de Estándar" Value="TipoEstandar" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmTipoEstandar.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Estándar" Value="Estandar" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmEstandar.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Objetivo" Value="Objetivo" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmObjetivo.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Enfoque" Value="Enfoque" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmEnfoque.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Grupos de Auditoría" Value="GruposAuditoria" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmGruposAuditoria.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Universo Auditable" NavigateUrl="" Value="UnivAudit">
                                            <asp:MenuItem Text="Ciclo" Value="Ciclo" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmCiclo.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Calificación de Control" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmCalificacionControl.aspx"
                                                Value="CalControl"></asp:MenuItem>
                                            <asp:MenuItem Text="Riesgo Inherente" Value="RiesgoInherente" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmRiesgoInherente.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Otros Factores" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmOtrosFactores.aspx"
                                                Value="OtrosFac"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Parametrizacion/Admin/PrmAdmParamColores.aspx"
                                            Text="Colorimetría" Value="Colorimetría"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Planeación" Value="Planeación" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmPlaneacion.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Temas a Auditar" Value="Temas a Auditar" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmAuditoria.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Gestion de Auditoria" Value="Gestion de Auditoria 2" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmGestionAuditoria.aspx"></asp:MenuItem>
                                    
                                    <asp:MenuItem Text="Verificación" Value="Verificacion" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmVerificacion.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Recomendaciones" Value="Recomendaciones" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmRecomendaciones.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Plan de Acción" Value="Plan de Acción" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmPlanesAccion.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Seguimiento" Value="Seguimiento" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmSeguimiento.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Plan de Auditoría" Value="Plan de Auditoría" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmPlanAuditoria.aspx"></asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmReporteAuditoriaAnulada.aspx"
                                        Text="Auditorías Anuladas" Value="Auditorías Anuladas"></asp:MenuItem>
                                    <asp:MenuItem Text="Historico de Auditoria" Value="Historico" NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmHistorial.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Reportes" Value="Reportes">
                                        <asp:MenuItem NavigateUrl="~/Formularios/Auditoria/Reportes/ReporteHallazgos.aspx"
                                            Text="Reporte Hallazgos" Value="Reporte Hallazgos"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Auditoria/Reportes/ReporteRecomendaciones.aspx"
                                            Text="Reporte Recomendaciones" Value="Reporte Recomendaciones"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Auditoria/Reportes/GestionAuditoria.aspx"
                                            Text="Gestion Auditoría" Value="Gestion Auditoría"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Auditoria/Admin/AudAdmReporteHallazgoVsPlanAccion.aspx"
                                            Text="Reporte Hallazgo VS Plan de acción" Value="HallazgoVsPlanAccion"></asp:MenuItem>
                                    </asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Calidad" Value="Calidad">
                                    <asp:MenuItem Text="Parametrización" Value="Parametrizacion">
                                        <asp:MenuItem Text="Cadena de Valor" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmCadenaValor.aspx"
                                            Value="CadenaValor"></asp:MenuItem>
                                        <asp:MenuItem Text="Macroproceso" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmMacroproceso.aspx"
                                            Value="Macroproceso"></asp:MenuItem>
                                        <asp:MenuItem Text="Proceso" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmProceso.aspx"
                                            Value="Proceso"></asp:MenuItem>
                                        <asp:MenuItem Text="Subproceso" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmSubproceso.aspx"
                                            Value="Subproceso"></asp:MenuItem>
                                        <asp:MenuItem Text="Política de Calidad" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmPoliticaCalidad.aspx"
                                            Value="PoliticaCalidad"></asp:MenuItem>
                                        <asp:MenuItem Text="Objetivo de Calidad" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmObjetivoCalidad.aspx"
                                            Value="ObjetivoCalidad"></asp:MenuItem>
                                        <asp:MenuItem Text="Perfil" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmPerfilCalidad.aspx"
                                            Value="PerfilCalidad"></asp:MenuItem>
                                        <asp:MenuItem Text="Requisitos" Value="Requisitos">
                                            <asp:MenuItem Text="Entrada" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmEntrada.aspx"
                                                Value="Entrada"></asp:MenuItem>
                                            <asp:MenuItem Text="Salida" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmSalida.aspx"
                                                Value="Salida"></asp:MenuItem>
                                            <asp:MenuItem Text="Actividad" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmActividad.aspx"
                                                Value="Actividad"></asp:MenuItem>
                                            <asp:MenuItem Text="Procedimientos" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmProcedimiento.aspx"
                                                Value="Procedimientos"></asp:MenuItem>
                                            <asp:MenuItem Text="Indicadores" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmIndicador.aspx"
                                                Value="Indicador"></asp:MenuItem>
                                            <asp:MenuItem Text="Gestión de Valor de Variables" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmValorVariable.aspx"
                                                Value="ValorVariable"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Parámetros ACM" Value="AcmParametrizacion">
                                            <asp:MenuItem Text="Origen no Conformidad" NavigateUrl="~/Formularios/Proceso/Admin/OrigenNoConformidad.aspx"
                                                Value="OrigenNoConformidad"></asp:MenuItem>
                                        </asp:MenuItem>
                                    <asp:MenuItem Text="Asociación Caracterización" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmAsocCaracterizacion.aspx"
                                        Value="Caracterizacion"></asp:MenuItem>
                                    <asp:MenuItem Text="Criterios de Evaluación" Value="Criterios">
                                        <asp:MenuItem Text="Gestionar Calificación de Evaluaciones" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmCalificacionEvaluaciones.aspx"
                                            Value="Calificacion"></asp:MenuItem>
                                        <asp:MenuItem Text="Gestionar criterios evaluación de competencia" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmCriterioCompetencias.aspx"
                                            Value="competencia"></asp:MenuItem>
                                        <asp:MenuItem Text="Gestionar criterios evaluación de desempeño" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmCriteriosDesempeno.aspx"
                                            Value="desempeño"></asp:MenuItem>
                                        <asp:MenuItem Text="Gestionar criterios evaluación de proveedores" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmCriterioProveedor.aspx"
                                            Value="proveedores"></asp:MenuItem>
                                        <asp:MenuItem Text="Gestionar criterios evaluación de Servicio" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmCriterioServicio.aspx"
                                            Value="servicio"></asp:MenuItem>
                                        <asp:MenuItem Text="Encuestas" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmEncuestas.aspx"
                                            Value="Encuestas"></asp:MenuItem>
                                    </asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Procesos" Value="Procesos">
                                    <asp:MenuItem Text="Ver Mapa de Procesos" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmMapaProceso.aspx"
                                        Value="MapaProceso"></asp:MenuItem>
                                    <asp:MenuItem Text="Ver Caracterización" NavigateUrl="~/Formularios/Proceso/Admin/PrcVerCaracterizacion.aspx"
                                        Value="Caracterizacion"></asp:MenuItem>
                                    <asp:MenuItem Text="Seguimiento Indicadores" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmSeguimientoIndicador.aspx"
                                        Value="SeguimientoIndicadores"></asp:MenuItem>
                                    <asp:MenuItem Text="Matriz de Indicadores" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmMatrizIndicadores.aspx"
                                        Value="MatrizIndicadores"></asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Administrativo" Value="Administrativo">
                                    <asp:MenuItem Text="Control Infraestructura" NavigateUrl="~/Formularios/Proceso/Admin/PrcCrlInfraestructura.aspx"
                                        Value="ControlInfraestructura"></asp:MenuItem>
                                    <asp:MenuItem Text="Plan de Formación" Value="PlanFormacion">
                                        <asp:MenuItem Text="Gestión Evaluación Competencias" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmGestionEvaluacionCompetencia.aspx"
                                            Value="GestionEvaluacionCompetencias"></asp:MenuItem>
                                        <asp:MenuItem Text="Gestión Evaluación Desempeño" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmGestionEvaluacionDesempeño.aspx"
                                            Value="GestionEvaluacionDesempeño"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Gestión Evaluación Proveedor" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmGestionEvaluacionProveedor.aspx"
                                        Value="GestionEvaluacionProveedor"></asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Gestión Calidad" Value="Calidad">
                                    <asp:MenuItem NavigateUrl="~/Formularios/Proceso/Acm/GestionAcm.aspx" Text="Gestión de ACM" Value="Acm"></asp:MenuItem>
                                    <asp:MenuItem Text="Auditoría" Value="PromragaAuditoria">
                                        <asp:MenuItem Text="Control no conformidad" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdminRegistroNoConformidad.aspx"
                                            Value="RegistroNoConformidad"></asp:MenuItem>
                                        <asp:MenuItem Text="Programación de Capacitaciones" NavigateUrl="~/Formularios/Proceso/Admin/PrcProgramaCapacitacion.aspx"
                                            Value="ProgramaCapacitacion"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Control Documentos y Versiones" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmCrlDocument.aspx"
                                        Value="ControlInfraestructura"></asp:MenuItem>
                                    <asp:MenuItem Text="Gestión Evaluación Servicio" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdmGestionEvaluacionServicio.aspx"
                                        Value="GestionEvaluacionServicio"></asp:MenuItem>
                                    <asp:MenuItem Text="Control Producto no conforme" NavigateUrl="~/Formularios/Proceso/Admin/PrcCrlProductoNoConforme.aspx"
                                        Value="ControlProductoNoConforme"></asp:MenuItem>
                                    <asp:MenuItem Text="Control Propiedad del Cliente o Proveedor" NavigateUrl="~/Formularios/Proceso/Admin/PrcCrlPropiedadClienteProveedor.aspx"
                                        Value="ControlPropiedadClienteProveedor"></asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Gerencia" Value="Gerencia">
                                    <asp:MenuItem Text="Plan de mejoramiento" NavigateUrl="~/Formularios/Proceso/Admin/PrcAdminPlanMejoramiento.aspx"
                                        Value="PlanMejoramiento"></asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Reportes" Value="Reportes">
                                    <asp:MenuItem Text="Reporte Documentos y Registros Vigentes" NavigateUrl="~/Formularios/Reportes/RPTreporteDocumentosVigentes.aspx"
                                        Value="PlanMejoramiento"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte No Conformidades" NavigateUrl="~/Formularios/Proceso/Admin/RPTreporteNoConformidades.aspx"
                                        Value="NoConformidades"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte Indicadores Metas Cumplidas" NavigateUrl="~/Formularios/Proceso/Reportes/RPTreporteIndicadoresMetasCumplidas.aspx"
                                        Value="ReporteIndicadoresMetasCumplidas"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte Indicadores Metas Incumplidas" NavigateUrl="~/Formularios/Proceso/Reportes/RPTreporteIndicadoresMetasIncumplidas.aspx"
                                        Value="ReporteIndicadoresMetasInCumplidas"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte indicadores por proceso" NavigateUrl="~/Formularios/Proceso/Reportes/RPTreporteIndicadoresPorProceso.aspx"
                                        Value="ReporteIndicadoresPorProceso"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte Seguimiento plan de formación" NavigateUrl="~/Formularios/Proceso/Reportes/RPTreporteSeguimientoPlanFormacion.aspx"
                                        Value="ReporteSeguimientoPlanFormacion"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte Seguimiento Evaluación de desempeño" NavigateUrl="~/Formularios/Proceso/Reportes/RPTreporteSeguimientoEvaluacionDesempeño.aspx"
                                        Value="ReporteSeguimientoEvaluacionDesempeño"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte Información Gerencial" NavigateUrl="~/Formularios/Proceso/Reportes/RPTreporteInformacionGeneral.aspx"
                                        Value="ReporteInformacionGeneral"></asp:MenuItem>
                                    <asp:MenuItem Text="Reporte Control Infraestructura" NavigateUrl="~/Formularios/Proceso/Reportes/RPTreporteControlInfraestructura.aspx"
                                        Value="ReporteControlInfraestructura"></asp:MenuItem>
                                </asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Riesgos" Value="Riesgos">
                                    <asp:MenuItem Text="Parametrización" Value="Parametrización">
                                        <asp:MenuItem Text="Eventos" Value="Eventos">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/Servicios.aspx" Text="Producto o Servicio Afectado"
                                                Value="Producto o Servicio Afectado"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/SubServicios.aspx" Text="SubProducto o SubServicio Afectado"
                                                Value="SubProducto o SubServicio Afectado"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/Canales.aspx" Text="Canales" Value="Canales"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/GeneradorEvento.aspx" Text="Generador Evento"
                                                Value="Generador Evento"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/Estado.aspx" Text="Estados" Value="Estados"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/Clases.aspx" Text="Clase Riesgo"
                                                Value="Clase Riesgo"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/SubClases.aspx" Text="SubClase Riesgo"
                                                Value="SubClase Riesgo"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParEveTipoPerdida.aspx" Text="Tipo perdida"
                                                Value="Tipo perdida"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/LNegocio.aspx" Text="Línea Operativa"
                                                Value="Línea Operativa"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Eventos/SubLNegocio.aspx" Text="SubLínea Operativa"
                                                Value="SubLínea Operativa"></asp:MenuItem>

                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Controles" Value="Controles">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParConPeriodicidad.aspx" Text="Periodicidad"
                                                Value="Periodicidad"></asp:MenuItem>
                                            <%--<asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParConPorcentajeCalificacion.aspx"
                                                Text="Porcentaje calificación control" Value="Porcentaje calificación control"></asp:MenuItem>--%>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParConDesviaciones.aspx" Text="Desviaciones"
                                                Value="Desviaciones"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParConLimites.aspx" Text="Configurar Limites"
                                                Value="Limites"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParGruposTrabajo.aspx" Text="Grupos de Trabajo" Value="Grupos de Trabajo"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParGruposTrabajoParam.aspx" Text="Recursos Externos" Value="Recursos Externos"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/VariablesCalificacionControl.aspx" Text="Variables de Calificación Control"
                                                Value="VariablesCalificacionControl"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/CategoriaVariablesControl.aspx" Text="Categoría de las Variables de Calificación"
                                                Value="CategoriaVariablesControl"></asp:MenuItem>
                                            <%--<asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/VariablesxCategoriaControl.aspx" Text="Asociación Categoria a Variables"
                                            Value="CategoriaVariablesControl"></asp:MenuItem>--%>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Legislación" Value="Legislación">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParLegTipoLegislacion.aspx"
                                                Text="Tipo de legislación" Value="Tipo de legislación"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParLegEstado.aspx" Text="Estado legislación"
                                                Value="Estado legislación"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Planes de evaluación" Value="Planes de evaluación">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParPlaEvaTipoPrueba.aspx"
                                                Text="Tipo de prueba" Value="Tipo de prueba"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParPlaEvaEstado.aspx" Text="Estado plan evaluación"
                                                Value="Estado plan evaluación"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Planes de acción" Value="Planes de acción">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParPlaAccTipoRecurso.aspx"
                                                Text="Tipo de recurso" Value="Tipo de recurso"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParPlaAccEstado.aspx" Text="Estado plan de acción"
                                                Value="Estado plan de acción"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Riesgos" Value="Riesgos">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieCausas.aspx" Text="Causas"
                                                Value="Causas"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieConsecuencias.aspx"
                                                Text="Consecuencias" Value="Consecuencias"></asp:MenuItem>
                                            <asp:MenuItem Text="SARO" Value="SARO">
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieTipoEventoSaro.aspx"
                                                    Text="Tipo de evento" Value="Tipo de evento"></asp:MenuItem>
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieRiesgoAsociadoSaro.aspx"
                                                    Text="Riesgo asociado" Value="Riesgo asociado"></asp:MenuItem>
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieFactorRiesgoOperativo.aspx"
                                                    Text="Factor riesgo operativo" Value="Factor riesgo operativo"></asp:MenuItem>
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieSubFactorRiesgoOperativo.aspx"
                                                    Text="Sub factor riesgo operativo" Value="Sub factor riesgo operativo"></asp:MenuItem>
                                            </asp:MenuItem>
                                            <asp:MenuItem Text="SARLAFT" Value="SARLAFT">
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieFactorRiesgoLaft.aspx"
                                                    Text="Factor riesgo LAFT" Value="Factor riesgo LAFT"></asp:MenuItem>
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieRiesgoAsociadoSarlaft.aspx"
                                                    Text="Riesgo asociado" Value="Riesgo asociado"></asp:MenuItem>
                                            </asp:MenuItem>
                                            <asp:MenuItem Text="Clasificación" Value="Clasificación">
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieClasificacionRiesgo.aspx"
                                                    Text="Clasificación global" Value="Clasificación global"></asp:MenuItem>
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieClasificacionGeneralRiesgo.aspx"
                                                    Text="Clasificación general" Value="Clasificación general"></asp:MenuItem>
                                                <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieClasificacionParticularRiesgo.aspx"
                                                    Text="Clasificación particular" Value="Clasificación particular"></asp:MenuItem>
                                            </asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieProbabilidad.aspx" Text="Frecuencia"
                                                Value="Frecuencia"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParRieImpacto.aspx" Text="Impacto"
                                                Value="Impacto"></asp:MenuItem>

                                        </asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminMapaRiesgos.aspx" Text="Mapa de Riesgos"
                                            Value="Mapa de Riesgos"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminAreas.aspx" Text="Areas"
                                            Value="Areas"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/FrecuenciavsEventos.aspx" Text="Frecuencia vs Eventos"
                                            Value="FrecuenciavsEventos"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/Tratamientos.aspx" Text="Tratamientos"
                                            Value="Tratamientos"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminCumplimiento.aspx" Text="Legislación"
                                        Value="Legislación"></asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminRiesgos.aspx" Text="Riesgos"
                                        Value="Riesgos"></asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminAnulacionRiesgo.aspx"
                                        Text="Anulación de riesgo" Value="Anulación de riesgo"></asp:MenuItem>
                                    <asp:MenuItem Text="Controles" Value="Controles">
                                        <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminControles.aspx" Text="Registro de Controles"
                                            Value="Controles"></asp:MenuItem>

                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Eventos" Value="Eventos">
                                        <asp:MenuItem NavigateUrl="~/Formularios/Eventos/AdminEventos.aspx" Text="Registro de Eventos"
                                            Value="Registro de Eventos"></asp:MenuItem>
                                        <asp:MenuItem Text="Reportes" Value="Reportes" NavigateUrl="~/Formularios/Eventos/ReporteEventos.aspx"></asp:MenuItem>

                                    </asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminConsolidadoRiesgos.aspx"
                                        Text="Mapa riesgos" Value="Mapa riesgos"></asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/AdminReporte.aspx" Text="Reporte"
                                        Value="Reporte"></asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/HistoricoRiesgo.aspx" Text="Histórico riesgos"
                                        Value="Histórico riesgos"></asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Admin/ParCargaMasiva.aspx" Text="Carga Masiva"
                                        Value="CargaMasiva"></asp:MenuItem>
                                    <asp:MenuItem Text="Indicadores" Value="Indicadores">
                                        <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/Indicadores/RiesgoIndicador.aspx" Text="Creación Indicadores"
                                            Value="Registro de Indicador"></asp:MenuItem>
                                        <asp:MenuItem Text="Gestión Indicadores" Value="Gestion" NavigateUrl="~/Formularios/Riesgos/Gestion/GestionRiesgoIndicador.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Monitoreo" Value="Seguimiento" NavigateUrl="~/Formularios/Riesgos/Seguimiento/SeguimientoRiesgoIndicador.aspx"></asp:MenuItem>

                                    </asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Riesgos/CuadroMando/CuadroMando.aspx" Text="Cuadro de Mando"
                                        Value="CuadroMando"></asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Sarlaft" Value="Sarlaft">
                                    <asp:MenuItem Text="Perfilamiento" Value="Perfilamiento">
                                        <asp:MenuItem Text="Parametrización General" Value="Parametrización General">
                                            <asp:MenuItem Text="Configuración Variables" Value="Variables" NavigateUrl="~/Formularios/ConfigEstructura/PrmVariables.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Configuración Categorias" Value="Categorias" NavigateUrl="~/Formularios/ConfigEstructura/PrmCategorias.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Configuración Perfiles" Value="ConfPerfiles" NavigateUrl="~/Formularios/Perfilamiento/PrmPerfiles.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Configuración Estructura Archivo" Value="EstrucArchivo" NavigateUrl="~/Formularios/ConfigEstructura/PrmEstructuraArchivo.aspx"></asp:MenuItem>
                                            <%--<asp:MenuItem Text="Configuración Relación Perfil - Variable" Value="PerfilVariable"
                                                NavigateUrl="~/Formularios/ConfigEstructura/ConfPerfilVariable.aspx"></asp:MenuItem>
                                            --%>
                                        </asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Señales de Alerta" Value="SenalesAlerta">
                                        <asp:MenuItem Text="Configuración Señales de Alerta" Value="Senales" NavigateUrl="~/Formularios/ConfigEstructura/PrmSenales.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Señales de alerta manuales" Value="SenalManual" NavigateUrl="~/Formularios/Perfilamiento/SenalAlertaManual.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Señales de alerta comparativas" Value="SenalComparativa" NavigateUrl="~/Formularios/Perfilamiento/SenalAlertaComparativa.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Cargue de archivos" Value="CargueArchivo" NavigateUrl="~/Formularios/Perfilamiento/CargueArchivo.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Configuración Factor de Riesgo" Value="FactoresRiesgo" NavigateUrl="~/Formularios/ConfigEstructura/ConfFactorRiesgo.aspx"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Instrumentos SARLAFT" Value="Instrumentos SARLAFT">
                                        <asp:MenuItem Text="Segmentación" Value="Segmentación">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/AdminSegmentacion.aspx" Text="Segmentación"
                                                Value="Segmentación"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/AdminVerSegmentacion.aspx"
                                                Text="Ver segmentación" Value="Ver segmentación"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Consultar registro operaciones" Value="Consultar registro operaciones">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/AdminRegistroOperacion.aspx"
                                                Text="Consultar registro operaciones" Value="Consultar registro operaciones"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/AdminROIManual.aspx" Text="Ingresar ROI"
                                                Value="Ingresar ROI"></asp:MenuItem>
                                        </asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Conocimiento de cliente" Value="Conocimiento de cliente">
                                        <asp:MenuItem Text="Formulario" Value="Formulario">
                                            <%--<asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/AdminFormCliente.aspx" Text="Conocimiento cliente"
                                                Value="Conocimiento cliente"></asp:MenuItem>--%>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/AdminFormClienteWillisFCC.aspx" Text="Conocimiento cliente"
                                                Value="Conocimiento cliente"></asp:MenuItem>
                                            <%--<asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/AdminFormClienteZurich.aspx" Text="Conocimiento cliente"
                                                Value="Conocimiento cliente"></asp:MenuItem>
                                            <asp:MenuItem Text="Aprobar / Rechazar FCC" Value="FormularioConocimientoCliente" NavigateUrl="~/Formularios/Sarlaft/Admin/FormularioConocimientoCliente.aspx"></asp:MenuItem>--%>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Consultar cliente" Value="Consultar cliente">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/ConsultarFormClienteWillis.aspx"
                                                Text="Consultar cliente" Value="Consultar cliente"></asp:MenuItem>
                                            <%--<asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/ConsultarFormClienteZurich.aspx"
                                                Text="Consultar cliente" Value="Consultar cliente"></asp:MenuItem>--%>
                                            <%--<asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/ConsultarFormClienteCopidrogas.aspx"
                                                Text="Consultar cliente" Value="Consultar cliente"></asp:MenuItem>--%>
                                        </asp:MenuItem>
                                        <%--<asp:MenuItem Text="Aprobar / Rechazar FCC" Value="FormularioConocimientoCliente" NavigateUrl="~/Formularios/Sarlaft/Admin/FormularioConocimientoCliente.aspx" >
                                            </asp:MenuItem>--%>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Reportes" Value="Reportes">
                                        <asp:MenuItem Text="Reportes de Perfilamiento" Value="Reporte Perfilamiento">
                                            <asp:MenuItem Text="Reporte Perfiles" Value="RptPerfiles" NavigateUrl="~/Formularios/Perfilamiento/ReportePerfiles.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Reporte Perfiles Detallado" Value="RptPerfiles" NavigateUrl="../../Formularios/Perfilamiento/ReportePerfilesDetallado.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Reporte Histórico Inspektor" Value="RptHistInspektor" NavigateUrl="~/Formularios/Perfilamiento/ReporteHistInspektor.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Reporte relación Factor Riesgo - Señal Alerta" Value="RptFactorSenal"
                                                NavigateUrl="~/Formularios/Perfilamiento/ReporteFactorSenal.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Reporte Detalle Calificación" Value="RptDetalleCalificacion" NavigateUrl="~/Formularios/Perfilamiento/ReporteDetalleCalificacion.aspx"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Reportes Operaciones Inusuales" Value="Reportes Operaciones Inusuales">
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/RpteConsolidadoSarlaft.aspx"
                                                Text="Reporte Consolidado Operaciones Inusuales" Value="Reporte Consolidado Operaciones Inusuales"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/RpteDetalleSarlaft.aspx" Text="Reporte Detallado Operaciones Inusuales"
                                                Value="Reporte Detallado Operaciones Inusuales"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/RpteRegistroSarlaft.aspx"
                                                Text="Reporte Por Registro" Value="Reporte Por Registro"></asp:MenuItem>
                                            <asp:MenuItem NavigateUrl="~/Formularios/Sarlaft/Admin/RpteTrazaSarlaft.aspx" Text="Reporte Trazabilidad Consolidado"
                                                Value="Reporte Trazabilidad Consolidado"></asp:MenuItem>
                                        </asp:MenuItem>
                                    </asp:MenuItem>
                                </asp:MenuItem>
                                <asp:MenuItem Text="Gestión Estratégica" Value="Gestión Estratégica">
                                    <asp:MenuItem NavigateUrl="~/Formularios/Gestion/Parametrizacion.aspx" Text="Parametrización"
                                        Value="Parametrización"></asp:MenuItem>
                                    <asp:MenuItem Text="Visión de la Empresa" Value="Visión de la Empresa" NavigateUrl="~/Formularios/Gestion/VisionEmpresa.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Plan Estratégico" Value="Plan Estratégico">
                                        <asp:MenuItem Text="Definición Plan Estratégico" Value="Definición Plan Estratégico"
                                            NavigateUrl="~/Formularios/Gestion/PlanEstrategico.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Objetivos Estratégicos" Value="Objetivos Estratégicos">
                                            <asp:MenuItem Text="Definición Objetivos Estratégicos" Value="Definición Objetivos Estratégicos"
                                                NavigateUrl="~/Formularios/Gestion/ObjetivoEstrategico.aspx"></asp:MenuItem>
                                            <asp:MenuItem Text="Causa y Efecto" Value="Causa y Efecto" NavigateUrl="~/Formularios/Gestion/CausaEfecto.aspx"></asp:MenuItem>
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Estrategias" Value="Estrategias" NavigateUrl="~/Formularios/Gestion/Estrategia.aspx"></asp:MenuItem>
                                        <asp:MenuItem Text="Planes de Acción" Value="Planes de Acción" NavigateUrl="~/Formularios/Gestion/PlanAccion.aspx"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Indicadores" Value="Indicadores" NavigateUrl="~/Formularios/Gestion/Indicadores.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Seguimiento" Value="Seguimiento" NavigateUrl="~/Formularios/Gestion/Seguimiento.aspx"></asp:MenuItem>
                                    <asp:MenuItem Text="Gestión" Value="Gestión">
                                        <asp:MenuItem NavigateUrl="~/Formularios/Gestion/Gestion.aspx" Text="Gestión" Value="Gestión"></asp:MenuItem>
                                        <asp:MenuItem Text="Cerrar Periodos" Value="Cerrar Periodos" NavigateUrl="~/Formularios/Gestion/CerrarPeriodo.aspx"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem NavigateUrl="~/Formularios/Gestion/MenuCMI.aspx" Text="Cuadro de Mando Integral"
                                        Value="Cuadro de Mando Integral"></asp:MenuItem>
                                    <asp:MenuItem Text="Reportes" Value="Reportes">
                                        <asp:MenuItem NavigateUrl="~/Formularios/Gestion/RptObjetivos.aspx" Text="Objetivos x Perspectivas"
                                            Value="Objetivos x Perspectivas"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Gestion/RptObjetivosEstrategicos.aspx" Text="Objetivos Estratégicos"
                                            Value="Objetivos Estratégicos"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Gestion/RptObjetivosVrsRiesgos.aspx" Text="Objetivos vrs Riesgos"
                                            Value="Objetivos vrs Riesgos"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Gestion/RptCuadorMandoIntegral.aspx" Text="Reporte Indicadores"
                                            Value="Reporte Indicadores"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Gestion/RptIndicadoresSinResultado.aspx"
                                            Text="Indicadores sin Resultado" Value="Indicadores sin Resultado"></asp:MenuItem>
                                        <asp:MenuItem NavigateUrl="~/Formularios/Gestion/RptPlanesResponsales.aspx" Text="Planes de Acción por Responsable"
                                            Value="Planes de Acción por Responsable"></asp:MenuItem>
                                    </asp:MenuItem>
                                </asp:MenuItem>
                            </Items>
                            <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <StaticSelectedStyle BackColor="#507CD1" />
                        </asp:Menu>
                    </td>
                </tr>
            </table>
        </td>
        <td align="right">
            <table style="background-color: #E8E9EA">
                <tr>
                    <td>
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/exit.png"
                            OnClick="ImageButton8_Click" Width="20px" />
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton8" runat="server" Font-Underline="False" OnClick="LinkButton8_Click"
                            Font-Bold="True" Font-Names="Calibri" Font-Overline="False" Font-Strikeout="False"
                            ForeColor="DarkBlue" Font-Size="Small">Exit</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
