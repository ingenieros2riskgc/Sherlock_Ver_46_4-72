<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Auuditoriaa.ascx.cs"
    Inherits="ListasSarlaft.UserControls.MAuditoria.Auuditoriaa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .ajax__tab_header
    {
        color: #909090;
        background-color: #EEEEEE;
    }

    .ajax__tab_xp .ajax__tab_body
    {
        background-color: #EEEEEE;
        border: 1px solid #EAEAEA;
        padding: 1px;
        list-style-type: none;
        font-size: 20px;
    }

    .ajax__tab_default .ajax__tab_inner
    {
        height: 100%;
    }

    .ajax__tab_default .ajax__tab_tab
    {
        height: 100%;
        font-size: 12px;
        background-color: #EEEEEE;
    }

    .ajax__tab_xp .ajax__tab_hover .ajax__tab_tab
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_active .ajax__tab_tab
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_inner
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_tab
    {
        height: 100%;
    }

    .ajax__tab_xp .ajax__tab_hover .ajax__tab_inner
    {
        height: 100%;
        color: #365F91;
    }

    .ajax__tab_xp .ajax__tab_active .ajax__tab_inner
    {
        height: 100%;
        color: #365F91;
    }

    .backgroundTest
    {
        background-color: aqua;
    }

    .tablas
    {
        background: #EEEEEE;
        border-color: White;
        border-bottom-color: White;
        border-left-color: White;
        border-right-color: White;
        border-collapse: collapse;
        padding: 2px;
        border: 1px solid;
        border-spacing: 0px;
    }

        .tablas tbody td
        {
            border-color: White;
            padding: 2px;
            border: 2px solid White;
            border-spacing: 0px;
        }
</style>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[Auditoria] WHERE [IdAuditoria] = @IdAuditoria"
    InsertCommand="INSERT INTO [Auditoria].[Auditoria] ([Tema], [IdEstandar], [Tipo], [FechaRegistro], [IdPlaneacion], [IdDependencia], [IdProceso], [IdUsuario], [IdEmpresa], [Estado], [Recursos], [Objetivo], [Alcance], [NivelImportancia], [IdDetalleTipo_TipoNaturaleza], [FechaInicio], [FechaCierre]) VALUES (@Tema, @IdEstandar, @Tipo, @FechaRegistro, @IdPlaneacion, @IdDependencia, @IdProceso, @IdUsuario, @IdEmpresa, @Estado, @Recursos, @Objetivo, @Alcance, @NivelImportancia, @IdDetalleTipo_TipoNaturaleza, @FechaInicio, @FechaCierre)  SET @NewParameter=SCOPE_IDENTITY();"
    SelectCommand="SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], [IdDependencia], [IdProceso], [NombreHijo] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa],  CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10),[FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre
                    FROM   [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Parametrizacion].[JerarquiaOrganizacional]
                    WHERE  [Auditoria].IdEstandar = [Estandar].IdEstandar AND
	                       [Auditoria].IdUsuario = [Usuarios].IdUsuario AND
	                       [Auditoria].IdDependencia = [JerarquiaOrganizacional].IdHijo AND
                           [Auditoria].IdPlaneacion = @IdPlaneacion and IdDependencia <> 0
                    UNION
                    SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], [IdDependencia], [Auditoria].[IdProceso], [Proceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre
                    FROM   [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[Proceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso
                    WHERE  [Auditoria].IdEstandar = [Estandar].IdEstandar AND
	                       [Auditoria].IdUsuario = [Usuarios].IdUsuario AND
	                       [Auditoria].IdProceso = [Proceso].IdProceso AND
    [Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and
                           [Auditoria].IdPlaneacion = @IdPlaneacion
    and AuditoriaProceso.IdTipoProceso = 2
                    UNION
                    SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], [IdDependencia], [Auditoria].[IdProceso], [MacroProceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre
                    FROM   [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[MacroProceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso
                    WHERE  [Auditoria].IdEstandar = [Estandar].IdEstandar AND
	                       [Auditoria].IdUsuario = [Usuarios].IdUsuario AND
	                       [Auditoria].IdProceso = [MacroProceso].IdMacroProceso AND
    [Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and
                           [Auditoria].IdPlaneacion = @IdPlaneacion
    and AuditoriaProceso.IdTipoProceso = 1"
    
    UpdateCommand="UPDATE [Auditoria].[Auditoria] SET [Tema] = @Tema, [IdEstandar] = @IdEstandar, [Tipo] = @Tipo, [IdPlaneacion] = @IdPlaneacion, [IdDependencia] = @IdDependencia, [IdProceso] = @IdProceso, [IdEmpresa] = @IdEmpresa, [Recursos] = @Recursos, [Objetivo] = @Objetivo, [Alcance] = @Alcance, [NivelImportancia] = @NivelImportancia, [IdDetalleTipo_TipoNaturaleza] = @IdDetalleTipo_TipoNaturaleza, [FechaInicio] = @FechaInicio, [FechaCierre] = @FechaCierre WHERE [IdAuditoria] = @IdAuditoria"
    OnInserted="SqlDataSource1_On_Inserted">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Tema" Type="String" />
        <asp:Parameter Name="IdEstandar" Type="Int64" />
        <asp:Parameter Name="Tipo" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdPlaneacion" Type="Int64" />
        <asp:Parameter Name="IdDependencia" Type="Int64" />
        <asp:Parameter Name="IdProceso" Type="Int32" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
        <asp:Parameter Name="IdEmpresa" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Recursos" Type="String" />
        <asp:Parameter Name="Objetivo" Type="String" />
        <asp:Parameter Name="Alcance" Type="String" />
        <asp:Parameter Name="NivelImportancia" Type="String" />
        <asp:Parameter Name="IdDetalleTipo_TipoNaturaleza" Type="Int64" />
        <asp:Parameter Name="FechaInicio" Type="DateTime" />
        <asp:Parameter Name="FechaCierre" Type="DateTime" />
        <asp:Parameter Direction="Output" Name="NewParameter" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Tema" Type="String" />
        <asp:Parameter Name="IdEstandar" Type="Int64" />
        <asp:Parameter Name="Tipo" Type="String" />
        <asp:Parameter Name="IdPlaneacion" Type="Int64" />
        <asp:Parameter Name="IdDependencia" Type="Int64" />
        <asp:Parameter Name="IdProceso" Type="Int64" />
        <asp:Parameter Name="IdEmpresa" Type="Int64" />
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="Recursos" Type="String" />
        <asp:Parameter Name="Objetivo" Type="String" />
        <asp:Parameter Name="Alcance" Type="String" />
        <asp:Parameter Name="NivelImportancia" Type="String" />
        <asp:Parameter Name="IdDetalleTipo_TipoNaturaleza" Type="Int64" />
        <asp:Parameter Name="FechaInicio" Type="DateTime" />
        <asp:Parameter Name="FechaCierre" Type="DateTime" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource20" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], [IdDependencia], [IdProceso], [NombreHijo] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa],  CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10),[FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre
                    FROM   [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Parametrizacion].[JerarquiaOrganizacional]
                    WHERE  [Auditoria].IdEstandar = [Estandar].IdEstandar AND
	                       [Auditoria].IdUsuario = [Usuarios].IdUsuario AND
	                       [Auditoria].IdDependencia = [JerarquiaOrganizacional].IdHijo AND
                           [Auditoria].IdPlaneacion = @IdPlaneacion and [Auditoria].IdAuditoria = @IdAuditoria and IdDependencia <> 0
                    UNION
                    SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], [IdDependencia], [Auditoria].[IdProceso], [Proceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre
                    FROM   [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[Proceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso
                    WHERE  [Auditoria].IdEstandar = [Estandar].IdEstandar AND
	                       [Auditoria].IdUsuario = [Usuarios].IdUsuario AND
	                       [Auditoria].IdProceso = [Proceso].IdProceso AND
                           [Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria AND
                           [Auditoria].IdPlaneacion = @IdPlaneacion and [Auditoria].IdAuditoria = @IdAuditoria
                            AND [AuditoriaProceso].IdTipoProceso  = 2
                    UNION
                    SELECT [Auditoria].[Auditoria].[IdAuditoria], [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], [IdDependencia], [Auditoria].[IdProceso], [MacroProceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre
                    FROM   [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[MacroProceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso
                    WHERE  [Auditoria].IdEstandar = [Estandar].IdEstandar AND
	                       [Auditoria].IdUsuario = [Usuarios].IdUsuario AND
	                       [Auditoria].IdProceso = [MacroProceso].IdMacroProceso AND
                           [Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria AND
                           [Auditoria].IdPlaneacion = @IdPlaneacion and [Auditoria].IdAuditoria = @IdAuditoria
                            AND [AuditoriaProceso].IdTipoProceso = 1"
    >
    <SelectParameters>
        <asp:ControlParameter ControlID="TXIdPlaneacionCNC" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="txtCodAuditoriaGen" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [AuditoriaObjetivo].[IdObjetivo], [Objetivo].[Nombre], [Alcance],  CONVERT(VARCHAR(10),[AuditoriaObjetivo].[FechaInicial],120) AS FechaInicial, CONVERT(VARCHAR(10),[AuditoriaObjetivo].[FechaFinal],120) AS FechaFinal, CONVERT(VARCHAR(10),[AuditoriaObjetivo].[FechaRegistro],120) AS FechaRegistro, [AuditoriaObjetivo].[IdUsuario], [Usuarios].[Usuario], [AuditoriaObjetivo].[IdGrupoAuditoria]  
                   FROM   [Auditoria].[AuditoriaObjetivo], [Listas].[Usuarios], [Auditoria].[Objetivo] 
                   WHERE  [IdAuditoria] = @IdAuditoria AND
                          [AuditoriaObjetivo].IdUsuario = [Usuarios].IdUsuario AND
                          [Objetivo].IdObjetivo = [AuditoriaObjetivo].IdObjetivo"
    DeleteCommand="DELETE FROM [Auditoria].[AuditoriaObjetivo] WHERE [IdAuditoria] = @IdAuditoria AND [IdObjetivo] = @IdObjetivo"
    InsertCommand="INSERT INTO [Auditoria].[AuditoriaObjetivo] ([IdAuditoria], [IdObjetivo], [Alcance], [IdGrupoAuditoria], [FechaInicial], [FechaFinal], [FechaRegistro], [IdUsuario]) VALUES (@IdAuditoria, @IdObjetivo, @Alcance, @IdGrupoAuditoria, @FechaInicial, @FechaFinal, @FechaRegistro, @IdUsuario)"
    UpdateCommand="UPDATE [Auditoria].[AuditoriaObjetivo] 
        SET [Alcance] = @Alcance, [IdGrupoAuditoria] = @IdGrupoAuditoria, [FechaInicial] = @FechaInicial, [FechaFinal] = @FechaFinal, [IdObjetivo] = @IdObjetivoNuevo 
        WHERE [IdAuditoria] = @IdAuditoria AND [IdObjetivo] = @IdObjetivo">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaGen" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="Alcance" Type="String" />
        <asp:Parameter Name="IdGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="FechaInicial" Type="DateTime" />
        <asp:Parameter Name="FechaFinal" Type="DateTime" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Alcance" Type="String" />
        <asp:Parameter Name="IdGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="FechaInicial" Type="DateTime" />
        <asp:Parameter Name="FechaFinal" Type="DateTime" />
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdObjetivoNuevo" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2A" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    UpdateCommand="UPDATE AUDITORIA.AudObjEnfoque SET [IdObjetivo] = @IdObjetivoNuevo WHERE [IdAuditoria] = @IdAuditoria AND [IdObjetivo] = @IdObjetivo">
    <UpdateParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdObjetivoNuevo" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdObjetivo], [Nombre] FROM [Auditoria].[Objetivo] WHERE ([IdEstandar] = @IdEstandar)">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodEstandarObj" Name="IdEstandar" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[AuditoriaRICC] WHERE [IdAuditoria] = @IdAuditoria AND [IdRiesgoInherente] = @IdRiesgoInherente AND [IdCalificacionControl] = @IdCalificacionControl AND [IdCiclo] = @IdCiclo"
    InsertCommand="INSERT INTO [Auditoria].[AuditoriaRICC] ([IdAuditoria], [IdCiclo], [IdRiesgoInherente], [IdCalificacionControl], [FechaRegistro], [IdUsuario]) VALUES (@IdAuditoria, @IdCiclo, @IdRiesgoInherente, @IdCalificacionControl, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdAuditoria], [AuditoriaRICC].[IdCiclo], [Ciclo].[NombreCiclo], [AuditoriaRICC].[IdRiesgoInherente], [RiesgoInherente].[Descripcion] AS NombreRI, [AuditoriaRICC].[IdCalificacionControl], [CalificacionControl].[Descripcion] AS NombreCC, CONVERT(VARCHAR(10),[AuditoriaRICC].[FechaRegistro],120) AS FechaRegistro, [AuditoriaRICC].[IdUsuario], [Usuarios].[Usuario]
                   FROM [Auditoria].[AuditoriaRICC], [Auditoria].[Ciclo], [Auditoria].[CalificacionControl], [Auditoria].[RiesgoInherente], [Listas].[Usuarios]
                   WHERE [AuditoriaRICC].IdAuditoria = @IdAuditoria AND
                         [AuditoriaRICC].IdCiclo = [Ciclo].IdCiclo AND
                         [AuditoriaRICC].IdCalificacionControl = [CalificacionControl].IdCalificacionControl AND
                         [AuditoriaRICC].IdRiesgoInherente = [RiesgoInherente].IdRiesgoInherente AND
                         [AuditoriaRICC].IdUsuario = [Usuarios].IdUsuario"
    UpdateCommand="UPDATE [Auditoria].[AuditoriaRICC] SET [IdCiclo] = @IdCiclo, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario WHERE [IdAuditoria] = @IdAuditoria AND [IdRiesgoInherente] = @IdRiesgoInherente AND [IdCalificacionControl] = @IdCalificacionControl">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaGen" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdCiclo" Type="Int64" />
        <asp:Parameter Name="IdRiesgoInherente" Type="Int64" />
        <asp:Parameter Name="IdCalificacionControl" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdCiclo" Type="Int64" />
        <asp:Parameter Name="IdRiesgoInherente" Type="Int64" />
        <asp:Parameter Name="IdCalificacionControl" Type="Int64" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdCiclo" Type="Int64" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdRiesgoInherente" Type="Int64" />
        <asp:Parameter Name="IdCalificacionControl" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdCiclo], [NombreCiclo] FROM [Auditoria].[Ciclo] ORDER BY NombreCiclo"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdCalificacionControl], [Descripcion] FROM [Auditoria].[CalificacionControl] ORDER BY Descripcion"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdRiesgoInherente], [Descripcion] FROM [Auditoria].[RiesgoInherente] ORDER BY Descripcion"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdPlaneacion], [Nombre] FROM [Auditoria].[Planeacion]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdEstandar], [Nombre] FROM [Auditoria].[Estandar]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdMacroProceso], [Nombre] 
FROM [Procesos].[Macroproceso] 
inner join Procesos.CadenaValor as CV on Macroproceso.IdCadenaValor = CV.IdCadenaValor
where Macroproceso.Estado = 1 and CV.Estado = 1"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdProceso], [Nombre] FROM [Procesos].[Proceso] WHERE [IdMacroProceso] = @IdMacroProceso and Estado = 1">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlMacroProceso" Name="IdMacroProceso" PropertyName="SelectedValue"
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[AuditoriaOtrosFactores] WHERE [IdAuditoria] = @IdAuditoria AND [IdOtrosFactores] = @IdOtrosFactores"
    InsertCommand="INSERT INTO [Auditoria].[AuditoriaOtrosFactores] ([IdAuditoria], [IdCiclo], [IdOtrosFactores], [FechaRegistro], [IdUsuario]) VALUES (@IdAuditoria, @IdCiclo, @IdOtrosFactores, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdAuditoria], [AuditoriaOtrosFactores].[IdCiclo], [Ciclo].NombreCiclo, [AuditoriaOtrosFactores].[IdOtrosFactores], [OtrosFactores].[Descripcion], CONVERT(VARCHAR(10),[AuditoriaOtrosFactores].[FechaRegistro],120) AS FechaRegistro, [AuditoriaOtrosFactores].[IdUsuario], [Usuarios].[Usuario]
                   FROM   [Auditoria].[AuditoriaOtrosFactores], [Listas].[Usuarios], [Auditoria].[OtrosFactores], [Auditoria].[Ciclo]
                   WHERE  [AuditoriaOtrosFactores].IdUsuario = [Usuarios].IdUsuario AND
                          [OtrosFactores].IdOtrosFactores = [AuditoriaOtrosFactores].IdOtrosFactores AND
                          [AuditoriaOtrosFactores].IdCiclo = [Ciclo].IdCiclo AND
                          [AuditoriaOtrosFactores].IdAuditoria = @IdAuditoria"
    UpdateCommand="UPDATE [Auditoria].[AuditoriaOtrosFactores] SET [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario WHERE [IdAuditoria] = @IdAuditoria AND [IdOtrosFactores] = @IdOtrosFactores">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaGen" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdOtrosFactores" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdCiclo" Type="Int64" />
        <asp:Parameter Name="IdOtrosFactores" Type="Int64" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdOtrosFactores" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource13" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdOtrosFactores], [Descripcion] FROM [Auditoria].[OtrosFactores]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource14" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdEnfoque], [Descripcion] FROM [Auditoria].[Enfoque] WHERE ([IdObjetivo] = @IdObjetivo)">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodObjetivo" DefaultValue="" Name="IdObjetivo"
            PropertyName="Text" Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource15" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[AudObjEnfoque] WHERE [IdAuditoria] = @IdAuditoria AND [IdObjetivo] = @IdObjetivo AND [IdEnfoque] = @IdEnfoque"
    InsertCommand="INSERT INTO [Auditoria].[AudObjEnfoque] ([IdAuditoria], [IdObjetivo], [IdEnfoque], [FechaRegistro], [IdUsuario]) VALUES (@IdAuditoria, @IdObjetivo, @IdEnfoque, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdAuditoria], [AudObjEnfoque].[IdObjetivo], [Objetivo].[Nombre], [AudObjEnfoque].IdEnfoque, [Enfoque].Descripcion, CONVERT(VARCHAR(10),[AudObjEnfoque].[FechaRegistro],120) AS FechaRegistro, [AudObjEnfoque].[IdUsuario], [Usuarios].Usuario
                   FROM [Auditoria].[AudObjEnfoque], [Listas].[Usuarios], [Auditoria].Objetivo, [Auditoria].Enfoque
                   WHERE [AudObjEnfoque].[IdAuditoria] = @IdAuditoria AND [AudObjEnfoque].[IdObjetivo] = @IdObjetivo AND
						[AudObjEnfoque].IdUsuario = [Usuarios].IdUsuario AND
						[AudObjEnfoque].IdObjetivo =  [Objetivo].IdObjetivo AND
						[AudObjEnfoque].IdEnfoque = [Enfoque].IdEnfoque"
    UpdateCommand="UPDATE [Auditoria].[AudObjEnfoque] SET [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario WHERE [IdAuditoria] = @IdAuditoria AND [IdObjetivo] = @IdObjetivo AND [IdEnfoque] = @IdEnfoque">
    <DeleteParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdEnfoque" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdEnfoque" Type="Int64" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaGen" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="txtCodObjetivo" Name="IdObjetivo" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdEnfoque" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource16" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdGrupoAuditoria], [Nombre] FROM [Auditoria].[GrupoAuditoria]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource17" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[AudObjRecurso] WHERE [IdAuditoria] = @IdAuditoria AND [IdObjetivo] = @IdObjetivo AND [IdGrupoAuditoria] = @IdGrupoAuditoria AND [IdHijo] = @IdHijo AND [Etapa] = @Etapa"
    InsertCommand="INSERT INTO [Auditoria].[AudObjRecurso] ([IdAuditoria], [IdObjetivo], [IdGrupoAuditoria], [IdHijo], [Etapa], [FechaFinal], [FechaInicial], [HorasPlaneadas], [FechaRegistro], [IdUsuario]) VALUES (@IdAuditoria, @IdObjetivo, @IdGrupoAuditoria, @IdHijo, @Etapa, @FechaFinal, @FechaInicial, @HorasPlaneadas, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT A.[IdAuditoria], A.[IdObjetivo], A.ObjNombre, A.[IdGrupoAuditoria], A.[IdHijo], A.Nodo_Jerarquia, DT.NombreResponsable,  A.[Etapa], A.FechaInicial, A.FechaFinal, A.[HorasPlaneadas], A.FechaRegistro, A.[IdUsuario], A.Usuario
                    FROM (
	                    SELECT [IdAuditoria], [AudObjRecurso].[IdObjetivo], [Objetivo].[Nombre] as ObjNombre, [IdGrupoAuditoria], [AudObjRecurso].[IdHijo], [JerarquiaOrganizacional].[NombreHijo] AS Nodo_Jerarquia, [Etapa], CONVERT(VARCHAR(10),[AudObjRecurso].[FechaInicial],120) AS FechaInicial, CONVERT(VARCHAR(10),[AudObjRecurso].[FechaFinal],120) AS FechaFinal, [HorasPlaneadas], CONVERT(VARCHAR(10),[AudObjRecurso].[FechaRegistro],120) AS FechaRegistro, [AudObjRecurso].[IdUsuario], [Usuarios].Usuario
	                    FROM   [Auditoria].[AudObjRecurso], [Auditoria].[Objetivo], [Parametrizacion].[JerarquiaOrganizacional], [Listas].[Usuarios]
	                    WHERE  
		                      [AudObjRecurso].[IdAuditoria] = @IdAuditoria AND [AudObjRecurso].[IdObjetivo] = @IdObjetivo AND
		                      [AudObjRecurso].[IdGrupoAuditoria] = @IdGrupoAuditoria AND
		                      [Objetivo].IdObjetivo = [AudObjRecurso].IdObjetivo AND
		                      [JerarquiaOrganizacional].idHijo = [AudObjRecurso].idHijo AND
		                      [AudObjRecurso].IdUsuario = [Usuarios].IdUsuario) AS A 
                    LEFT JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DT
                    ON A.IdHijo = DT.idHijo"
    UpdateCommand="UPDATE [Auditoria].[AudObjRecurso] SET [FechaFinal] = @FechaFinal, [FechaInicial] = @FechaInicial, [HorasPlaneadas] = @HorasPlaneadas WHERE [IdAuditoria] = @IdAuditoria AND [IdObjetivo] = @IdObjetivo AND [IdGrupoAuditoria] = @IdGrupoAuditoria AND [IdHijo] = @IdHijo AND [Etapa] = @Etapa">
    <DeleteParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="IdHijo" Type="Int64" />
        <asp:Parameter Name="Etapa" Type="String" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="IdHijo" Type="Int64" />
        <asp:Parameter Name="Etapa" Type="String" />
        <asp:Parameter Name="FechaFinal" Type="DateTime" />
        <asp:Parameter Name="FechaInicial" Type="DateTime" />
        <asp:Parameter Name="HorasPlaneadas" Type="Int32" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Etapa" Type="String" />
        <asp:Parameter Name="FechaFinal" Type="DateTime" />
        <asp:Parameter Name="FechaInicial" Type="DateTime" />
        <asp:Parameter Name="HorasPlaneadas" Type="Int32" />
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
        <asp:Parameter Name="IdObjetivo" Type="Int64" />
        <asp:Parameter Name="IdGrupoAuditoria" Type="Int64" />
        <asp:Parameter Name="IdHijo" Type="Int64" />
    </UpdateParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaGen" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="txtCodObjetivo" Name="IdObjetivo" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="lblCodGA" Name="IdGrupoAuditoria" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource18" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT Etapa, SUM(HorasPlaneadas) as Suma
                    FROM Auditoria.AudObjRecurso
                    WHERE IdHijo = @IdHijo  
                    GROUP BY Etapa">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblCodNodoGA" Name="IdHijo" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource19" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdDetalleTipo], [NombreDetalle] FROM [Parametrizacion].[DetalleTipos] WHERE IdTipo = 1"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource181" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT Etapa, SUM(HorasPlaneadas) as Suma
                    FROM Auditoria.AudObjRecurso
                    WHERE IdHijo = @IdHijo  
                    GROUP BY Etapa">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblCodNodoGA1" Name="IdHijo" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource200" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) VALUES (@IdEvento, @Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario, @Tipo) SET @NewParameter2=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdCorreosEnviados], [IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosEnviados]"
    UpdateCommand="UPDATE [Notificaciones].[CorreosEnviados] SET [FechaEnvio] = @FechaEnvio, [Estado] = @Estado WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    OnInserted="SqlDataSource200_On_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdEvento" Type="Decimal" />
        <asp:Parameter Name="Destinatario" Type="String" />
        <asp:Parameter Name="Copia" Type="String" />
        <asp:Parameter Name="Otros" Type="String" />
        <asp:Parameter Name="Asunto" Type="String" />
        <asp:Parameter Name="Cuerpo" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Tipo" Type="String" />
        <asp:Parameter Name="IdRegistro" Type="Decimal" />
        <asp:Parameter Name="FechaEnvio" Type="DateTime" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Direction="Output" Name="NewParameter2" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="FechaEnvio" Type="DateTime" />
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource201" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosRecordatorio] WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosRecordatorio] ([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) VALUES (@IdCorreosEnviados, @NroDiasRecordatorio, CONVERT(datetime, @FechaFinal, 120), @Estado, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdCorreosRecordatorio], [IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosRecordatorio]"
    UpdateCommand="UPDATE [Estado] = @Estado WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
        <asp:Parameter Name="NroDiasRecordatorio" Type="Int32" />
        <asp:Parameter Name="FechaFinal" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgokEliminar" runat="server" Text="Ok" OnClick="btnImgokEliminar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox"
            BehaviorID="mypopup" Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>
        <div id="TreeDIV" runat="server">
            <asp:Panel ID="pnlDependencia" runat="server" CssClass="popup" Width="400px" Style="display: none">
                <table width="100%" class="tablaSinBordes">
                    <tr align="right" bgcolor="#5D7B9D">
                        <td>
                            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                OnClientClick="$find('popup').hidePopup(); return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                Style="overflow: auto" Font-Size="Small" LineImagesFolder="~/TreeLineImages"
                                ForeColor="Black" ShowLines="True" Target="_self" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                                Font-Bold="False" Height="400px">
                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                            </asp:TreeView>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Button ID="BtnOk" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup').hidePopup(); return false;" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <asp:TextBox ID="txtCodAuditoriaGen" runat="server" Visible="false">0</asp:TextBox>
            <asp:TextBox runat="server" ID="txtCodEstandarObj" Visible="false" OnTextChanged="txtCodEstandarObj_TextChanged">0</asp:TextBox>
            <asp:TextBox runat="server" ID="txtCodObjetivo" Visible="false"></asp:TextBox>
            <asp:Label ID="lblTipoUA" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="OF" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblCodGA" runat="server" Text="Label" Visible="False"></asp:Label>
        </div>
        <asp:Panel ID="pnlImpacto" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="btnClosepp1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" align="left" class="tabla">
                            <tr>
                                <td>
                                    <asp:Label ID="Label60" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Poco Probable:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label61" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Es remoto y poco probable de que suceda."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label62" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Eventual:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label63" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Esta sujeto a la conjución de circunstancias."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label64" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Probable:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label65" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Puede ocurrir en algún momento."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlProbabilidad" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tabla">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup1').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table align="left" class="tabla">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label56" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Insignificante:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label57" runat="server" Font-Names="Calibri" Font-Size="Small" Text="De poca importancia, no repercute negativamente en el Good Will de la empresa y puede acompañarse de perdidas financieras mínimas."></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label58" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Mínimo:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label59" runat="server" Font-Names="Calibri" Font-Size="Small" Text="De poca importancia, con leve impacto y puede acompañarse de perdidas financieras bajas."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlPlaneacion" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupPlaneacion').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView8" runat="server" DataSourceID="SqlDataSource8" Font-Names="Calibri"
                            Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                            BorderStyle="Solid" DataKeyNames="IdPlaneacion" AllowPaging="True" AllowSorting="True"
                            ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" GridLines="Vertical"
                            CellPadding="4" OnSelectedIndexChanged="GridView8_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="IdPlaneacion" HeaderText="Código" InsertVisible="False"
                                    ReadOnly="True" SortExpression="IdPlaneacion">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HtmlEncode="False"
                                    HtmlEncodeFormatString="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/edit.png"
                                    ShowSelectButton="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupPlaneacion').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlProceso" runat="server" CssClass="popup" Width="350px" Style="display: none">
            <table width="100%" class="tabla">
                <tr align="right" bgcolor="#5D7B9D">
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupProceso').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#BBBBBB">
                        <asp:Label ID="Label7" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td align="center" bgcolor="#EEEEEE">
                        <asp:DropDownList ID="ddlMacroProceso" runat="server" Width="250px" CssClass="Apariencia"
                            OnDataBound="ddlMacroProceso_DataBound" DataSourceID="SqlDataSource10" DataTextField="Nombre"
                            DataValueField="IdMacroProceso" AutoPostBack="True" OnSelectedIndexChanged="ddlMacroProceso_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#BBBBBB">
                        <asp:Label ID="Label25" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td align="center" bgcolor="#EEEEEE">
                        <asp:DropDownList ID="ddlProceso" runat="server" Width="250px" CssClass="Apariencia"
                            OnDataBound="ddlProceso_DataBound" AutoPostBack="True" DataSourceID="SqlDataSource11"
                            DataTextField="Nombre" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                            DataValueField="IdProceso">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:Button ID="Button3" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupProceso').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlGrupoAud" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupGA').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TreeView ID="TVGrupoAud" ExpandDepth="3" runat="server" Font-Names="Calibri"
                            Font-Bold="False" Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black"
                            ShowLines="True" AutoGenerateDataBindings="False" OnSelectedNodeChanged="TVGrupoAud_SelectedNodeChanged">
                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                        </asp:TreeView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupGA').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMatrizRecursos" runat="server" CssClass="popup" Width="220px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupMR').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label51" runat="server" Text="Fecha Inicial:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFecIniMR" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label52" runat="server" Text="Fecha Final:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFecFinMR" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label53" runat="server" Text="Horas Laborables:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHorasLab" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label55" runat="server" Text="Horas Utilizadas:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHorasUtil" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label54" runat="server" Text="Horas Disponibles:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHorasDisp" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="GridView6" runat="server" DataSourceID="SqlDataSource18" Font-Names="Calibri"
                                        Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                                        BorderStyle="Solid" AllowSorting="True" ShowHeaderWhenEmpty="True" ForeColor="#333333"
                                        Font-Bold="False" GridLines="Vertical" CellPadding="4">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="Etapa" HeaderText="Etapa" SortExpression="Etapa" />
                                            <asp:BoundField DataField="Suma" HeaderText="Horas Asignadas" ReadOnly="True" SortExpression="Suma">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button4" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupMR').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMatrizRecursos1" runat="server" CssClass="popup" Width="220px"
            Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton51" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupMR1').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label511" runat="server" Text="Fecha Inicial:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFecIniMR1" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label521" runat="server" Text="Fecha Final:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFecFinMR1" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label531" runat="server" Text="Horas Laborables:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHorasLab1" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label551" runat="server" Text="Horas Utilizadas:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHorasUtil1" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#BBBBBB">
                                    <asp:Label ID="Label541" runat="server" Text="Horas Disponibles:" CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHorasDisp1" runat="server" CssClass="Apariencia" Enabled="False"
                                        Width="60px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="GridView61" runat="server" DataSourceID="SqlDataSource181" Font-Names="Calibri"
                                        Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                                        BorderStyle="Solid" AllowSorting="True" ShowHeaderWhenEmpty="True" ForeColor="#333333"
                                        Font-Bold="False" GridLines="Vertical" CellPadding="4">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="Etapa" HeaderText="Etapa" SortExpression="Etapa" />
                                            <asp:BoundField DataField="Suma" HeaderText="Horas Asignadas" ReadOnly="True" SortExpression="Suma">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button41" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupMR1').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="70%" bgcolor="#EEEEEE" align="center">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Temas a Auditar" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                    <asp:Label ID="LidAuditoria" runat="server" ForeColor="White" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr id="filaAuditoria" runat="server" visible="true">
                <td>
                    <table align="center" width="100%" class="tabla">
                        <tr align="center">
                            <td>
                                <table class="tabla">
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label67" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodPlaneacion" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="70px"></asp:TextBox>
                                            <asp:TextBox ID="TXIdPlaneacionCNC" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="70px" Visible="false">0</asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label66" runat="server" CssClass="Apariencia" Text="Planeación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomPlaneacion" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="350px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnPlaneacion" runat="server" ImageUrl="~/Imagenes/Icons/DateTime.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupPlanea" runat="server" BehaviorID="popupPlaneacion"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlPlaneacion" Position="Bottom"
                                                TargetControlID="imgBtnPlaneacion">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td bgcolor="#EEEEEE">
                                <br />
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource1"
                                                Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                Font-Bold="False" OnRowCommand="GridView1_RowCommand" ShowHeaderWhenEmpty="True"
                                                DataKeyNames="Usuario,IdEstandar,IdDependencia,IdProceso,NombreDP, Recursos, Objetivo, Alcance, NivelImportancia, IdDetalleTipo_TipoNaturaleza, FechaInicio, FechaCierre, IdEmpresa">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdAuditoria" HeaderText="Código" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="IdAuditoria">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tema" HeaderText="Tema" SortExpression="Tema" HtmlEncode="False"
                                                        HtmlEncodeFormatString="False" Visible="true">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdEstandar" HeaderText="IdEstandar" SortExpression="IdEstandar"
                                                        InsertVisible="False" ReadOnly="True" Visible="False" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Estándar" SortExpression="Nombre"
                                                        HtmlEncode="False" HtmlEncodeFormatString="False" />
                                                    <asp:BoundField DataField="IdPlaneacion" HeaderText="IdPlaneacion" SortExpression="IdPlaneacion"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                                    <asp:BoundField DataField="IdDependencia" HeaderText="IdDependencia" SortExpression="IdDependencia"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="IdProceso" HeaderText="IdProceso" SortExpression="IdProceso"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="NombreDP" HeaderText="NombreDP" SortExpression="NombreDP"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdEmpresa" HeaderText="IdEmpresa" SortExpression="IdEmpresa"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Recursos" HeaderText="Recursos" SortExpression="Recursos"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Objetivo" HeaderText="Objetivo" SortExpression="Objetivo"
                                                        Visible="true" />
                                                    <asp:BoundField DataField="Alcance" HeaderText="Alcance" SortExpression="Alcance"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="NivelImportancia" HeaderText="NivelImportancia" SortExpression="NivelImportancia"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="IdDetalleTipo_TipoNaturaleza" HeaderText="IdDetalleTipo_TipoNaturaleza"
                                                        SortExpression="IdDetalleTipo_TipoNaturaleza" Visible="False" />
                                                    <asp:BoundField DataField="FechaInicio" HeaderText="FechaInicio" SortExpression="FechaInicio"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="FechaCierre" HeaderText="FechaCierre" SortExpression="FechaCierre"
                                                        Visible="False" />
                                                    <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                CommandArgument="Editar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar"
                                                                ToolTip="Editar" />
                                                            <asp:ImageButton ID="btnImgEliminarObjetivo" runat="server" CausesValidation="False"
                                                                OnClick="btnImgEliminarObjetivo_Click" CommandArgument="<%# Container.DataItemIndex %>"
                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <asp:GridView ID="GridView7" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource20"
                                                Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnSelectedIndexChanged="GridView7_SelectedIndexChanged"
                                                Font-Bold="False" OnRowCommand="GridView7_RowCommand" ShowHeaderWhenEmpty="True" Visible="false"
                                                DataKeyNames="Usuario,IdEstandar,IdDependencia,IdProceso,NombreDP, Recursos, Objetivo, Alcance, NivelImportancia, IdDetalleTipo_TipoNaturaleza, FechaInicio, FechaCierre, IdEmpresa">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdAuditoria" HeaderText="Código" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="IdAuditoria">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tema" HeaderText="Tema" SortExpression="Tema" HtmlEncode="False"
                                                        HtmlEncodeFormatString="False"  Visible="true">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdEstandar" HeaderText="IdEstandar" SortExpression="IdEstandar"
                                                        InsertVisible="False" ReadOnly="True" Visible="False" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Estándar" SortExpression="Nombre"
                                                        HtmlEncode="False" HtmlEncodeFormatString="False" />
                                                    <asp:BoundField DataField="IdPlaneacion" HeaderText="IdPlaneacion" SortExpression="IdPlaneacion"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                                    <asp:BoundField DataField="IdDependencia" HeaderText="IdDependencia" SortExpression="IdDependencia"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="IdProceso" HeaderText="IdProceso" SortExpression="IdProceso"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="NombreDP" HeaderText="NombreDP" SortExpression="NombreDP"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdEmpresa" HeaderText="IdEmpresa" SortExpression="IdEmpresa"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Recursos" HeaderText="Recursos" SortExpression="Recursos"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Objetivo" HeaderText="Objetivo" SortExpression="Objetivo"
                                                        Visible="true" />
                                                    <asp:BoundField DataField="Alcance" HeaderText="Alcance" SortExpression="Alcance"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="NivelImportancia" HeaderText="NivelImportancia" SortExpression="NivelImportancia"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="IdDetalleTipo_TipoNaturaleza" HeaderText="IdDetalleTipo_TipoNaturaleza"
                                                        SortExpression="IdDetalleTipo_TipoNaturaleza" Visible="False" />
                                                    <asp:BoundField DataField="FechaInicio" HeaderText="FechaInicio" SortExpression="FechaInicio"
                                                        Visible="False" />
                                                    <asp:BoundField DataField="FechaCierre" HeaderText="FechaCierre" SortExpression="FechaCierre"
                                                        Visible="False" />
                                                    <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                CommandArgument="Editar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar"
                                                                ToolTip="Editar" />
                                                            <asp:ImageButton ID="btnImgEliminarObjetivo" runat="server" CausesValidation="False"
                                                                OnClick="btnImgEliminarObjetivo_Click" CommandArgument="<%# Container.DataItemIndex %>"
                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                                                ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertar_Click" Text="Insert"
                                                ToolTip="Insertar" />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaDetalle" runat="server" align="left" visible="false" bgcolor="#EEEEEE">
                <td bgcolor="#EEEEEE">
                    <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="2" AutoPostBack="True"
                        OnActiveTabChanged="TabContainer2_ActiveTabChanged">
                        <asp:TabPanel ID="TabPanel4" runat="server" Font-Underline="True" HeaderText="Información Básica"
                            Style="padding: 1,1,1,1">
                            <ContentTemplate>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel5" runat="server" Font-Underline="True" HeaderText="Universo Auditable"
                            BackColor="#EEEEEE">
                            <ContentTemplate>
                                <table width="100%" bgcolor="#EEEEEE">
                                    <tr id="filaGridRICC" align="center" runat="server">
                                        <td id="Td5" bgcolor="#EEEEEE" runat="server">
                                            <table width="100%" class="tabla">
                                                <tr align="center">
                                                    <td>
                                                        <table class="tabla">
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label21" runat="server" CssClass="Apariencia" Text="Planeación:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPlaneacionRICC" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label24" runat="server" CssClass="Apariencia" Text="Auditoria:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAuditoriaRICC" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label26" runat="server" CssClass="Apariencia" Text="Estandar:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEstandarRICC" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <table width="100%">
                                                            <tr align="center" bgcolor="#5D7B9D">
                                                                <td>
                                                                    <asp:Label ID="Label27" runat="server" ForeColor="White" Text="Riesgo Inherente - Calificación Control"
                                                                        Font-Bold="False" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                                <br />
                                                                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource4"
                                                                                    Font-Bold="False" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center"
                                                                                    OnRowCommand="GridView2_RowCommand" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                                                                                    CssClass="Apariencia" ShowHeaderWhenEmpty="True" DataKeyNames="IdAuditoria,IdCiclo,IdRiesgoInherente,IdCalificacionControl,Usuario">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" ReadOnly="True"
                                                                                            SortExpression="IdAuditoria" Visible="False" />
                                                                                        <asp:BoundField DataField="IdCiclo" HeaderText="IdCiclo" ReadOnly="True" SortExpression="IdCiclo"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="NombreCiclo" HeaderText="Ciclo" SortExpression="NombreCiclo">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="IdRiesgoInherente" HeaderText="IdRiesgoInherente" ReadOnly="True"
                                                                                            SortExpression="IdRiesgoInherente" Visible="False" />
                                                                                        <asp:BoundField DataField="NombreRI" HeaderText="Riesgo Inherente" SortExpression="NombreRI">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="IdCalificacionControl" HeaderText="IdCalificacionControl"
                                                                                            ReadOnly="True" SortExpression="IdCalificacionControl" Visible="False" />
                                                                                        <asp:BoundField DataField="NombreCC" HeaderText="Calificación de Control" SortExpression="NombreCC">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                                                            Visible="False" />
                                                                                        <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                                                    CommandArgument="Editar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" ToolTip="Editar" /><asp:ImageButton
                                                                                                        ID="btnImgEliminarRICC" runat="server" CausesValidation="False" OnClick="btnImgEliminarRICC_Click"
                                                                                                        CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                                        Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:ImageButton ID="imgBtnInsertarRICC" runat="server" CausesValidation="False"
                                                                                    CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarRICC_Click"
                                                                                    Text="Insert" ToolTip="Insertar" /><br />
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="filaDetalleRICC" runat="server" x align="left" visible="False">
                                        <td id="Td6" runat="server">
                                            <table bgcolor="#EEEEEE" class="tabla" width="100%">
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label18" runat="server" CssClass="Apariencia" Text="Ciclo:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="Apariencia" Width="300px"
                                                            DataSourceID="SqlDataSource5" DataTextField="NombreCiclo" OnDataBound="ddlCiclo_DataBound"
                                                            DataValueField="IdCiclo">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label19" runat="server" CssClass="Apariencia" Text="Riesgo Inherente:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlRI" runat="server" CssClass="Apariencia" Width="300px" OnDataBound="ddlRI_DataBound"
                                                            DataSourceID="SqlDataSource7" DataTextField="Descripcion" DataValueField="IdRiesgoInherente">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label20" runat="server" CssClass="Apariencia" Text="Calificación de Control:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCC" runat="server" CssClass="Apariencia" Width="300px" OnDataBound="ddlCC_DataBound"
                                                            DataSourceID="SqlDataSource6" DataTextField="Descripcion" DataValueField="IdCalificacionControl">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label28" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUsuarioRICC" runat="server" CssClass="Apariencia" Enabled="False"
                                                            Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label29" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaCreacionRICC" runat="server" CssClass="Apariencia" Enabled="False"
                                                            Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <table class="tablaSinBordes">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnImgInsertarRICC" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        OnClick="btnImgInsertarRICC_Click" Visible="False" Style="height: 20px" ToolTip="Guardar" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnImgActualizarRICC" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        OnClick="btnImgActualizarRICC_Click" Style="text-align: right" ToolTip="Guardar" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnImgCancelarRICC" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        OnClick="btnImgCancelarRICC_Click" ToolTip="Cancelar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="filaGridOF" align="center" runat="server">
                                        <td bgcolor="#EEEEEE" runat="server">
                                            <table width="100%" class="tabla">
                                                <tr align="center">
                                                    <td>
                                                        <table width="100%">
                                                            <tr align="center" bgcolor="#5D7B9D">
                                                                <td>
                                                                    <asp:Label ID="Label33" runat="server" ForeColor="White" Text="Otros Factores" Font-Bold="False"
                                                                        Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                                <br />
                                                                                <asp:GridView ID="GridView12" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource12"
                                                                                    Font-Bold="False" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center"
                                                                                    OnRowCommand="GridView12_RowCommand" OnSelectedIndexChanged="GridView12_SelectedIndexChanged"
                                                                                    CssClass="Apariencia" ShowHeaderWhenEmpty="True" DataKeyNames="IdAuditoria,IdOtrosFactores,Usuario,IdCiclo">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="IdCiclo" HeaderText="IdCiclo" ReadOnly="True" SortExpression="IdCiclo"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="NombreCiclo" HeaderText="Ciclo" ReadOnly="True" SortExpression="NombreCiclo">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" ReadOnly="True"
                                                                                            SortExpression="IdAuditoria" Visible="False" />
                                                                                        <asp:BoundField DataField="IdOtrosFactores" HeaderText="IdOtrosFactores" ReadOnly="True"
                                                                                            SortExpression="IdOtrosFactores" Visible="False" />
                                                                                        <asp:BoundField DataField="Descripcion" HeaderText="Otro Factor" SortExpression="Descripcion">
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                                                            Visible="False" />
                                                                                        <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                                                    CommandArgument="Editar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" ToolTip="Editar" /><asp:ImageButton
                                                                                                        ID="btnImgEliminarOF" runat="server" CausesValidation="False" OnClick="btnImgEliminarOF_Click"
                                                                                                        CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                                        Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:ImageButton ID="imgBtnInsertarOF" runat="server" CausesValidation="False" CommandName="Insert"
                                                                                    ToolTip="Insertar" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarOF_Click"
                                                                                    Text="Insert" /><br />
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="filaDetalleOF" runat="server" x align="left" visible="False">
                                        <td runat="server">
                                            <table bgcolor="#EEEEEE" class="tabla" width="100%">
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label45" runat="server" CssClass="Apariencia" Text="Ciclo:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCicloOF" runat="server" CssClass="Apariencia" Width="500px"
                                                            DataSourceID="SqlDataSource5" DataTextField="NombreCiclo" OnDataBound="ddlCicloOF_DataBound"
                                                            DataValueField="IdCiclo">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCiclo" runat="server" ControlToValidate="ddlCicloOF"
                                                            ErrorMessage="Debe ingresar el ciclo." ToolTip="Debe ingresar el ciclo." CssClass="Apariencia"
                                                            ValidationGroup="iOFac" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label30" runat="server" CssClass="Apariencia" Text="Otros Factores:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlOF" runat="server" CssClass="Apariencia" Width="500px" DataSourceID="SqlDataSource13"
                                                            DataTextField="Descripcion" OnDataBound="ddlOF_DataBound" DataValueField="IdOtrosFactores">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvOFac" runat="server" ControlToValidate="ddlOF"
                                                            ErrorMessage="Debe ingresar otro factor." ToolTip="Debe ingresar otro factor." CssClass="Apariencia"
                                                            ValidationGroup="iOFac" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label34" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtUsuarioOF" runat="server" CssClass="Apariencia" Enabled="False"
                                                            Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label35" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaCreacionOF" runat="server" CssClass="Apariencia" Enabled="False"
                                                            Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <table class="tablaSinBordes">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnImgInsertarOF" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" OnClick="btnImgInsertarOF_Click" Visible="False" Style="height: 20px"
                                                                        ValidationGroup="iOFac" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnImgActualizarOF" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" OnClick="btnImgActualizarOF_Click" Style="text-align: right"
                                                                        ValidationGroup="iOFac" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnImgCancelarOF" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="btnImgCancelarOF_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel6" runat="server" Font-Underline="True" HeaderText="Objetivos">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr id="filaGridObjetivo" align="center" runat="server">
                                        <td bgcolor="#EEEEEE" runat="server">
                                            <table width="100%" class="tabla">
                                                <tr align="center">
                                                    <td>
                                                        <table class="tabla">
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label11" runat="server" CssClass="Apariencia" Text="Planeación:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPlaneacionObj" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label12" runat="server" CssClass="Apariencia" Text="Auditoria:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAuditoriaObj" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label13" runat="server" CssClass="Apariencia" Text="Estandar:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEstandarObj" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="FilaGridObjetivoSE" align="center" runat="server" width="100%">
                                                    <td runat="server">
                                                        <table width="100%">
                                                            <tr align="center" bgcolor="#5D7B9D">
                                                                <td>
                                                                    <asp:Label ID="Label31" runat="server" ForeColor="White" Text="Lista de Objetivos"
                                                                        Font-Bold="False" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                                <br />
                                                                                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource2"
                                                                                    Font-Bold="False" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center"
                                                                                    OnRowCommand="GridView3_RowCommand" OnSelectedIndexChanged="GridView3_SelectedIndexChanged"
                                                                                    CssClass="Apariencia" ShowHeaderWhenEmpty="True" DataKeyNames="IdObjetivo,Alcance,Usuario,IdGrupoAuditoria">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="IdObjetivo" HeaderText="IdObjetivo" SortExpression="IdObjetivo"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="Nombre" HeaderText="Objetivo" SortExpression="Nombre">
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Alcance" HeaderText="Alcance" SortExpression="Alcance"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="FechaInicial" HeaderText="Fecha Inicial" ReadOnly="True"
                                                                                            SortExpression="FechaInicial" Visible="False">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="FechaFinal" HeaderText="Fecha Final" SortExpression="FechaFinal"
                                                                                            ReadOnly="True" Visible="False">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro"
                                                                                            ReadOnly="True">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="IdGrupoAuditoria" HeaderText="IdGrupoAuditoria" SortExpression="IdGrupoAuditoria"
                                                                                            Visible="False" NullDisplayText=" " />
                                                                                        <asp:TemplateField HeaderText="Actividades">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnImgEnfoque" runat="server" CausesValidation="False" CommandName="Select"
                                                                                                    CommandArgument="Actividades" ImageUrl="~/Imagenes/Icons/Literal.png" Text="Actividades"
                                                                                                    ToolTip="Actividades" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Recursos" Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnImRecursos" runat="server" CausesValidation="False" CommandName="Select"
                                                                                                    CommandArgument="Recursos" ImageUrl="~/Imagenes/Icons/UserTime.png" Text="Actividades"
                                                                                                    ToolTip="Recursos" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                                                    CommandArgument="Editar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar"
                                                                                                    ToolTip="Editar" /><asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False"
                                                                                                        OnClick="btnImgEliminar_Click" CommandArgument="Eliminar" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                                        Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" CommandName="Insert"
                                                                                    ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarObjetivo_Click" Text="Insert"
                                                                                    ToolTip="Insertar" /><br />
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="FilaGridObjEnfoque" align="center" runat="server">
                                                    <td runat="server">
                                                        <table width="100%" class="tablaSinBordes">
                                                            <tr align="center" bgcolor="#5D7B9D">
                                                                <td>
                                                                    <asp:Label ID="Label32" runat="server" ForeColor="White" Text="Lista de Actividades a Auditar por Objetivo"
                                                                        Font-Bold="False" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                                <%--DataSourceID="SqlDataSource15"--%>
                                                                                <br />
                                                                                <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Bold="False"
                                                                                    ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" 
                                                                                    OnRowCommand="GridView4_RowCommand" OnSelectedIndexChanged="GridView4_SelectedIndexChanged"
                                                                                    CssClass="Apariencia" ShowHeaderWhenEmpty="True" DataKeyNames="IdAuditoria,IdObjetivo,IdEnfoque,IdUsuario,Usuario">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" SortExpression="IdAuditoria"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="IdObjetivo" HeaderText="IdObjetivo" SortExpression="IdObjetivo"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="Nombre" HeaderText="Objetivo" SortExpression="Nombre">
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="IdEnfoque" HeaderText="IdEnfoque" SortExpression="IdEnfoque"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="Descripcion" HeaderText="Actividad" SortExpression="Descripcion"
                                                                                            HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro"
                                                                                            ReadOnly="True" />
                                                                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                                            Visible="False" />
                                                                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                                                            Visible="False" />
                                                                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                                                    CommandArgument="Editar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar"
                                                                                                    ToolTip="Editar" /><asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False"
                                                                                                        OnClick="btnImgEliminar_Click" CommandArgument="Eliminar" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                                        Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:ImageButton ID="imgBtnInsertarObjEnfoque" runat="server" CausesValidation="False"
                                                                                    CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarObjEnfoque_Click"
                                                                                    Text="Insert" ToolTip="Insertar" /><br />
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="filaDetalleObjEnfoque" runat="server" align="left" visible="False">
                                        <td id="Td7" runat="server">
                                            <table width="100%" bgcolor="#EEEEEE">
                                                <tr>
                                                    <td>
                                                        <table bgcolor="#EEEEEE" class="tabla" width="100%">
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label37" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivo:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:TextBox ID="txtObjetivoEnf" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Enabled="False" Width="377px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label36" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Enfoque:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="ddlEnfoque" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                                                        OnDataBound="ddlEnfoque_DataBound" Font-Size="Small" Width="600px" DataSourceID="SqlDataSource14"
                                                                        DataTextField="Descripcion" DataValueField="IdEnfoque">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label40" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUsuarioObjEnf" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label41" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechaCreacionObjEnf" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnImgInsertarObjEnfoque" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    OnClick="btnImgInsertarObjEnfoque_Click" Visible="False" Style="height: 20px"
                                                                                    ToolTip="Guardar" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnImgActualizarObjEnfoque" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    OnClick="btnImgActualizarObjEnfoque_Click" Style="text-align: right" ToolTip="Guardar" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnImgCancelarObjEnfoque" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    OnClick="btnImgCancelarObjEnfoque_Click" ToolTip="Cancelar" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="filaDetalleRecursos" runat="server" align="left" visible="False">
                                        <td runat="server">
                                            <table width="100%" bgcolor="#EEEEEE">
                                                <tr>
                                                    <td>
                                                        <table bgcolor="#EEEEEE" class="tabla" width="100%">
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label43" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivo:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtObjetivoRec" runat="server" CssClass="Apariencia" Width="350px"
                                                                        Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label38" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Recurso:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td bgcolor="#EEEEEE">
                                                                                <asp:TextBox ID="txtRecurso" runat="server" CssClass="Apariencia" Width="250px" Enabled="False"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgGrupoRec" runat="server" ImageUrl="~/Imagenes/Icons/group.png"
                                                                                    OnClientClick="return false;" />
                                                                                <asp:PopupControlExtender ID="popupGrupoRec" runat="server" DynamicServicePath=""
                                                                                    ExtenderControlID="" TargetControlID="imgGrupoRec" BehaviorID="popupGA" PopupControlID="pnlGrupoAud">
                                                                                </asp:PopupControlExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgMR" runat="server" ImageUrl="~/Imagenes/Icons/timespan.png"
                                                                                    OnClientClick="return false;" />
                                                                                <asp:PopupControlExtender ID="popupMatrizRecursos" runat="server" DynamicServicePath=""
                                                                                    ExtenderControlID="" TargetControlID="imgMR" BehaviorID="popupMR" PopupControlID="pnlMatrizRecursos">
                                                                                </asp:PopupControlExtender>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label48" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Etapa:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="ddlEtapa" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                                                        Font-Size="Small" Width="150px" DataTextField="Nombre" DataValueField="IdGrupoAuditoria">
                                                                        <asp:ListItem></asp:ListItem>
                                                                        <asp:ListItem>PLANEACION</asp:ListItem>
                                                                        <asp:ListItem>EJECUCION</asp:ListItem>
                                                                        <asp:ListItem>HALLAZGOS</asp:ListItem>
                                                                        <asp:ListItem>INFORME</asp:ListItem>
                                                                        <asp:ListItem>CIERRE</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                        OnClick="imgBtnInsertarMasEtapa_Click" ToolTip="Agregar Etapa" />
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label42" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Inicial:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFecIniRec" runat="server" CssClass="Apariencia" Width="100px"></asp:TextBox><asp:CalendarExtender
                                                                        ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFecIniRec"
                                                                        BehaviorID="_content_CalendarExtender2"></asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label49" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Final:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFecFinRec" runat="server" CssClass="Apariencia" Width="100px"></asp:TextBox><asp:CalendarExtender
                                                                        ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFecFinRec"
                                                                        BehaviorID="_content_CalendarExtender1"></asp:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label50" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Número de Horas Planeadas:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:TextBox ID="txtHorasPlan" runat="server" CssClass="Apariencia" Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label46" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUsuarioRec" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label47" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFecCreacionRec" runat="server" CssClass="Apariencia" Enabled="False"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnImgInsertarRecursos" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    OnClick="btnImgInsertarRecursos_Click" Visible="False" ToolTip="Guardar" Style="height: 20px" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnImgActualizarRecursos" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    OnClick="btnImgActualizarRecursos_Click" Style="text-align: right" Height="20px"
                                                                                    ToolTip="Guardar" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnImgCancelarRecursos" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    OnClick="btnImgCancelarRecursos_Click" ToolTip="Cancelar" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="FilaGridObjRecursos" align="center" runat="server" visible="False">
                                        <td id="Td10" runat="server">
                                            <table width="100%">
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td>
                                                        <asp:Label ID="Label44" runat="server" ForeColor="White" Text="Recursos por Objetivo"
                                                            Font-Bold="False" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <table class="tablaSinBordes">
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    <br />
                                                                    <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Bold="False"
                                                                        ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" DataSourceID="SqlDataSource17"
                                                                        OnRowCommand="GridView5_RowCommand" OnSelectedIndexChanged="GridView5_SelectedIndexChanged"
                                                                        CssClass="Apariencia" ShowHeaderWhenEmpty="True" DataKeyNames="IdAuditoria,IdObjetivo,IdGrupoAuditoria,IdHijo,IdUsuario,Usuario">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdAuditoria" HeaderText="IdAuditoria" ReadOnly="True"
                                                                                SortExpression="IdAuditoria" Visible="False" />
                                                                            <asp:BoundField DataField="IdObjetivo" HeaderText="IdObjetivo" ReadOnly="True" SortExpression="IdObjetivo"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="ObjNombre" HeaderText="Objetivo" SortExpression="ObjNombre">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="IdGrupoAuditoria" HeaderText="IdGrupoAuditoria" ReadOnly="True"
                                                                                SortExpression="IdGrupoAuditoria" Visible="False" />
                                                                            <asp:BoundField DataField="IdHijo" HeaderText="IdHijo" SortExpression="IdHijo" ReadOnly="True"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="Nodo_Jerarquia" HeaderText="Nivel Jerarquia Org." SortExpression="Nodo_Jerarquia"
                                                                                ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="NombreResponsable" HeaderText="Responsable" SortExpression="NombreResponsable">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Etapa" HeaderText="Etapa" SortExpression="Etapa" ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FechaInicial" HeaderText="Fecha Inicial" SortExpression="FechaInicial"
                                                                                ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FechaFinal" HeaderText="Fecha Final" SortExpression="FechaFinal"
                                                                                ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="HorasPlaneadas" HeaderText="Horas Planeadas" SortExpression="HorasPlaneadas">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro"
                                                                                ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                                                Visible="False" />
                                                                            <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                                        CommandArgument="Editar" ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar"
                                                                                        ToolTip="Editar" /><asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False"
                                                                                            OnClick="btnImgEliminar_Click" CommandArgument="Eliminar" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                            Text="Eliminar" CommandName="Select" ToolTip="Eliminar" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:ImageButton ID="imgBtnInsertarObjRecursos" runat="server" CausesValidation="False"
                                                                        CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarObjRecursos_Click"
                                                                        Text="Insert" ToolTip="Insertar" /><br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </td>
            </tr>
            <tr id="filaDetalleAuditoria" runat="server" align="left" visible="false">
                <td id="Td9" runat="server">
                    <table width="100%" bgcolor="#EEEEEE">
                        <tr>
                            <td>
                                <table class="tabla" width="100%">
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Código:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtCodAuditoria" runat="server" Enabled="False" Font-Names="Calibri"
                                                Font-Size="Small" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Tema:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtNomAuditoria" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="377px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label75" runat="server" Text="Empresa:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="221px" CssClass="Apariencia"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label8" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Tipo:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                                Font-Size="Small" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" Width="221px">
                                                <asp:ListItem>Procesos</asp:ListItem>
                                                <asp:ListItem>Dependencia</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="filaDependencia" runat="server" align="left" visible="False" bgcolor="#EEEEEE">
                                        <td id="Td1" runat="server" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Dependencia:"></asp:Label>
                                        </td>
                                        <td id="Td2" runat="server">
                                            <table class="tablaSinBordes">
                                                <tr bgcolor="#EEEEEE">
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="txtDependencia" runat="server" Enabled="False" Font-Names="Calibri"
                                                            Font-Size="Small" Width="377px"></asp:TextBox>
                                                        <asp:Label ID="lblIdDependencia" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:ImageButton ID="imgDependencia" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                            OnClientClick="return false;" />
                                                        <asp:PopupControlExtender ID="popupDependencia" runat="server" BehaviorID="popup"
                                                            DynamicServicePath="" Enabled="True" ExtenderControlID="" OffsetY="-300" PopupControlID="pnlDependencia"
                                                            Position="Right" TargetControlID="imgDependencia">
                                                        </asp:PopupControlExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="filaProceso" runat="server" align="left" bgcolor="#EEEEEE">
                                        <td id="Td3" runat="server" align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Proceso:"></asp:Label>
                                        </td>
                                        <td id="Td4" runat="server">
                                            <table class="tablaSinBordes">
                                                <tr>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="txtProceso" runat="server" Enabled="False" Font-Names="Calibri"
                                                            Font-Size="Small" Width="377px"></asp:TextBox>
                                                    </td>
                                                    <td align="left" bgcolor="#EEEEEE">
                                                        <asp:ImageButton ID="imgProceso" runat="server" ImageUrl="~/Imagenes/Icons/FlowChart.png"
                                                            OnClientClick="return false;" />
                                                        <asp:PopupControlExtender ID="popupEProceso" runat="server" BehaviorID="popupProceso"
                                                            DynamicServicePath="" Enabled="True" ExtenderControlID="" PopupControlID="pnlProceso"
                                                            Position="Right" TargetControlID="imgProceso">
                                                        </asp:PopupControlExtender>
                                                        <asp:Label ID="lblIdProceso" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label4" runat="server" Font-Names="Calibri" Font-Size="Small" Text="acaPrograma/Estándar:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="ddlEstandar" runat="server" DataSourceID="SqlDataSource9" DataTextField="Nombre"
                                                DataValueField="IdEstandar" Font-Names="Calibri" Font-Size="Small" OnDataBound="ddlEstandar_DataBound"
                                                Width="221px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label68" runat="server" Text="Naturaleza:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="ddlNaturaleza" runat="server" Width="221px" Font-Names="Calibri"
                                                Font-Size="Small" DataSourceID="SqlDataSource19" DataTextField="NombreDetalle"
                                                DataValueField="IdDetalleTipo" OnDataBound="ddlNaturaleza_DataBound">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label69" runat="server" Text="Nivel de Importancia:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="ddlNivelImportancia" runat="server" Width="221px" Font-Names="Calibri"
                                                Font-Size="Small">
                                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                <asp:ListItem Value="Bajo" Text="Bajo"></asp:ListItem>
                                                <asp:ListItem Value="Medio" Text="Medio"></asp:ListItem>
                                                <asp:ListItem Value="Alto" Text="Alto"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label70" runat="server" Text="Objetivo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtObjetivo" runat="server" Enabled="True" Columns="100" TextMode="MultiLine"
                                                CssClass="Apariencia" Font-Names="Calibri" Rows="8"></asp:TextBox>
                                            <asp:htmleditorextender 
                                                ID="htmlEditorExtender3" 
                                                TargetControlID="txtObjetivo" 
                                                runat="server"
                                                EnableSanitization="false">
                                            </asp:htmleditorextender>
                                        </td>
                                    </tr>
                                    <tr align="left" runat="server" visible="false">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label71" runat="server" Text="Recursos:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtRecursos" runat="server" Enabled="True" Columns="100" TextMode="MultiLine"
                                                Font-Names="Calibri" Rows="8"></asp:TextBox>
                                            <asp:htmleditorextender 
                                                ID="htmlEditorExtender2" 
                                                TargetControlID="txtRecursos" 
                                                runat="server"
                                                EnableSanitization="false">
                                            </asp:htmleditorextender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table runat="server" id="TblGrupoAuditoria" class="tablaSinBordes">
                                                <tr>
                                                    <td align="center" bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label76" runat="server" Text="Grupo Auditoría" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td align="left" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label82" runat="server" Text="Grupo de Auditoría:" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlGrupoAud2" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                                                                    OnDataBound="ddlGrupoAud2_DataBound" Font-Size="Small" Width="400px" DataSourceID="SqlDataSource16"
                                                                                    DataTextField="Nombre" DataValueField="IdGrupoAuditoria">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="TrVerEtapas" visible="false" class="tablaSinBordes">
                                                    <td>
                                                        <asp:GridView ID="GdRecursos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                            BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                            OnRowCommand="GdRecursos_RowsCommand" AllowPaging="True" OnPageIndexChanging="GdRecursos_PageIndexChanging">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Etapa" DataField="Etapa" />
                                                                <asp:BoundField HeaderText="IdHijo" DataField="IdHijo" Visible="false" />
                                                                <asp:BoundField HeaderText="Recurso" DataField="NombreResponsable" />
                                                                <asp:BoundField HeaderText="Fecha Inicial" DataField="FechaInicial" />
                                                                <asp:BoundField HeaderText="Fecha Final" DataField="FechaFinal" />
                                                                <asp:BoundField HeaderText="Horas Asignadas" DataField="HorasPlaneadas" />
                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar"
                                                                    CommandName="Modificar" />
                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar"
                                                                    CommandName="Eliminar" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                        </asp:GridView>
                                                        <asp:ImageButton ID="ImageButton12" runat="server" CausesValidation="False" CommandName="Insert"
                                                            ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" ToolTip="Insertar" OnClick="ImageButton12_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="TbAddEtapas" runat="server" visible="false">
                                                            <tr>
                                                                <td align="center" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label77" runat="server" Text="Etapa" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label78" runat="server" Text="Recurso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label79" runat="server" Text="Fecha Inicio" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label80" runat="server" Text="Fecha Fin" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label81" runat="server" Text="Horas Asignadas" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                                                        Font-Size="Small">
                                                                        <asp:ListItem>---</asp:ListItem>
                                                                        <asp:ListItem>EJECUCIÓN</asp:ListItem>
                                                                        <asp:ListItem>PRE-INFORME</asp:ListItem>
                                                                        <asp:ListItem>INFORME</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList1"
                                                                        InitialValue="---" ForeColor="Red" ValidationGroup="AddRecursos">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DropDownList2" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList2"
                                                                        InitialValue="---" ForeColor="Red" ValidationGroup="AddRecursos">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="Apariencia" Width="90px" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        TargetControlID="TextBox1"></asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator124" runat="server" ControlToValidate="TextBox1"
                                                                        ForeColor="Red" ValidationGroup="AddRecursos">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="Apariencia" Width="90px" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                                        TargetControlID="TextBox2"></asp:CalendarExtender>
                                                                    <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                                                        runat="server" ControlToValidate="TextBox2" ControlToCompare="TextBox1" Font-Size="Small"
                                                                        ErrorMessage="Fecha Inválida. Fecha fin no puede ser menor a Fecha inicio" Type="Date"
                                                                        Operator="GreaterThanEqual" ValidationGroup="AddRecursos">
                                                                    </asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                                                        ForeColor="Red" ValidationGroup="AddRecursos">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" CssClass="Apariencia" Width="90px" ID="TextBox3" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"
                                                                        ForeColor="Red" ValidationGroup="AddRecursos">*</asp:RequiredFieldValidator>
                                                                    <asp:Label ID="lblCodNodoGA1" runat="server" Text="0" Visible="False"></asp:Label>
                                                                    <asp:ImageButton ID="imgMR1" runat="server" ImageUrl="~/Imagenes/Icons/timespan.png"
                                                                        OnClientClick="return false;" />
                                                                    <asp:PopupControlExtender ID="popupMatrizRecursos1" runat="server" DynamicServicePath=""
                                                                        Enabled="True" ExtenderControlID="" TargetControlID="imgMR1" BehaviorID="popupMR1"
                                                                        PopupControlID="pnlMatrizRecursos1">
                                                                    </asp:PopupControlExtender>
                                                                </td>
                                                            </tr>
                                                            <tr align="center" bgcolor="#EEEEEE">
                                                                <td colspan="5">
                                                                    <table class="tablaSinBordes">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    Visible="True" ToolTip="Guardar" OnClick="ImageButton11_Click" ValidationGroup="AddRecursos" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    Visible="True" ToolTip="Modificar" ValidationGroup="AddRecursos" OnClick="ImageButton10_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    ToolTip="Cancelar" OnClick="ImageButton13_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label72" runat="server" Text="Alcance:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtAlcance" runat="server" Enabled="True" Columns="100" TextMode="MultiLine"
                                                Font-Names="Calibri" Rows="8"></asp:TextBox>
                                             <asp:htmleditorextender 
                                                ID="htmlEditorExtender1" 
                                                TargetControlID="txtAlcance" 
                                                runat="server"
                                                EnableSanitization="false">
                                            </asp:htmleditorextender>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label73" runat="server" Text="Fecha Inicio:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtFecIniA" runat="server" Enabled="True" Width="100px" TextMode="SingleLine"
                                                Font-Names="Calibri" Font-Size="Small" Font-Bold="False"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                TargetControlID="txtFecIniA"></asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label74" runat="server" Text="Fecha Cierre:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtFecFinA" runat="server" Enabled="True" Width="100px" TextMode="SingleLine"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                TargetControlID="txtFecFinA"></asp:CalendarExtender>
                                            <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                                runat="server" ControlToValidate="txtFecFinA" ControlToCompare="txtFecIniA" Font-Size="Small"
                                                ErrorMessage="Fecha Inválida. Fecha fin no puede ser menor a Fecha inicio" Type="Date"
                                                Operator="GreaterThanEqual" ValidationGroup="AddRecursos2">
                                            </asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label2" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center" bgcolor="#EEEEEE">
                                        <td colspan="2">
                                            <table class="tablaSinBordes">
                                                <tr>
                                                    <td><asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            OnClick="btnImgInsertar_Click" Visible="False" ToolTip="Guardar" Height="20px" ValidationGroup="AddRecursos2" />
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            OnClick="btnImgActualizar_Click" Style="text-align: right" ToolTip="Guardar" ValidationGroup="AddRecursos2" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaDetalleObjetivo" runat="server" align="left" visible="False">
                <td id="Td8" runat="server">
                    <table bgcolor="#EEEEEE" class="tabla" width="100%">
                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label14" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivo:"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlObjetivo" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                    OnDataBound="ddlObjetivo_DataBound" Font-Size="Small" Width="400px" DataSourceID="SqlDataSource3"
                                    DataTextField="Nombre" DataValueField="IdObjetivo" OnSelectedIndexChanged="ddlObjetivo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label15" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Alcance:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAlcanceObj" runat="server" Width="800px" CssClass="Apariencia"
                                    Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                                <asp:htmleditorextender 
                                                ID="htmlEditorExtender9" 
                                                TargetControlID="txtAlcanceObj" 
                                                runat="server"
                                                EnableSanitization="false">
                                            </asp:htmleditorextender>
                            </td>
                        </tr>
                        <tr runat="server" visible="false">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label39" runat="server" Text="Grupo de Auditoría:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlGrupoAud" runat="server" AutoPostBack="True" Font-Names="Calibri"
                                                OnDataBound="ddlGrupoAud_DataBound" Font-Size="Small" Width="400px" DataSourceID="SqlDataSource16"
                                                DataTextField="Nombre" DataValueField="IdGrupoAuditoria" OnSelectedIndexChanged="ddlGrupoAud_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgGrupoAud" runat="server" ImageUrl="~/Imagenes/Icons/group.png"
                                                OnClientClick="return false;" Enabled="False" />
                                            <asp:PopupControlExtender ID="popupGrupoAud" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="imgGrupoAud" BehaviorID="popupGA"
                                                PopupControlID="pnlGrupoAud">
                                            </asp:PopupControlExtender>
                                            <asp:Label ID="lblCodNodoGA" runat="server" Text="0" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" visible="false">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" CssClass="Apariencia" Text="Fecha Inicial:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecIni" runat="server" CssClass="Apariencia" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFecIni_CalendarExtender" runat="server" Enabled="True"
                                    Format="yyyy-MM-dd" TargetControlID="txtFecIni"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr runat="server" visible="false">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label17" runat="server" CssClass="Apariencia" Text="Fecha Final:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecFin" runat="server" CssClass="Apariencia" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFecFin_CalendarExtender" runat="server" Enabled="True"
                                    Format="yyyy-MM-dd" TargetControlID="txtFecFin"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label22" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsuarioObj" runat="server" CssClass="Apariencia" Enabled="False"
                                    Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label23" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaCreacionObj" runat="server" CssClass="Apariencia" Enabled="False"
                                    Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertarObjetivo" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                OnClick="btnImgInsertarObjetivo_Click" Visible="False" Style="height: 20px" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizarObjetivo" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                OnClick="btnImgActualizarObjetivo_Click" Style="text-align: right" ToolTip="Guardar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelarObjetivo" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelarObjetivo_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="filaBtnTemas" align="center" bgcolor="#EEEEEE" runat="server" visible="false">
                <td>
                    <asp:Button ID="btnTemasAud" runat="server" Text="Volver a Temas de Auditoría" OnClick="btnTemasAud_Click" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
