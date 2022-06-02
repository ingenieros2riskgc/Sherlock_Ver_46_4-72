<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlanesAccion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.MAuditoria.PlanesAccion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
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
    
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
    .ajax__tab_header
    {
        text-align: center;
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
        padding: 5px;
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
    #Background
    {
        position: fixed;
        top: 0px;
        bottom: 0px;
        left: 0px;
        right: 0px;
        overflow: hidden;
        padding: 0;
        margin: 0;
        background: #EEEEEE;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 100000;
    }
    #Progress
    {
        position: fixed;
        top: 40%;
        left: 40%;
        height: 10%;
        width: 20%;
        z-index: 100001;
        background-color: #FFFFFF;
        border: 1px solid Gray;
        background-image: url('./Imagenes/Icons/loading.gif');
        background-repeat: no-repeat;
        background-position: center;
    }
    
    .style1
    {
        height: 74px;
    }
    
    .hideGridColumn
    {
        display: none;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="
        SELECT R.IdRiesgo, R.Nombre, O.Nombre NombreObj, R.Tipo, J.NombreHijo Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro 
        FROM Auditoria.Riesgo R 
        INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo  = H.IdHallazgo 
        INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria 
        INNER JOIN Parametrizacion.JerarquiaOrganizacional J ON J.idHijo = R.IdDependencia 
        INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque 
        INNER JOIN  Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque 
        INNER JOIN Auditoria.Objetivo O ON O.IdObjetivo = E.IdObjetivo 
        WHERE A.[IdPlaneacion] = @IdPlaneacion AND 
	        R.Tipo = 'Dependencia' AND 
            A.[Estado] = 'CUMPLIDA' 
        UNION
        SELECT R.IdRiesgo, R.Nombre, O.Nombre NombreObj, R.Tipo, P.Nombre Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro 
        FROM Auditoria.Riesgo R 
        INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo  = H.IdHallazgo 
        INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria 
        INNER JOIN Procesos.Proceso P ON P.IdProceso = R.IdSubproceso 
        INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque 
        INNER JOIN Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque 
        INNER JOIN Auditoria.Objetivo O  ON  O.IdObjetivo = E.IdObjetivo 
        WHERE A.[IdPlaneacion] = @IdPlaneacion AND 
	        R.Tipo = 'Procesos' AND 
            A.[Estado] = 'CUMPLIDA'" UpdateCommand="UPDATE [Auditoria].[Recomendacion] SET [Estado] = @Estado WHERE [IdRecomendacion] = @IdRecomendacion">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdRecomendacion" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    UpdateCommand="UPDATE [Auditoria].[PlanAccion] SET [FechaCompromiso] = @FechaCompromiso, [Descripcion] = @Descripcion WHERE [IdPlanAccion] = @IdPlanAccion">
    <UpdateParameters>
        <asp:Parameter Name="FechaCompromiso" Type="DateTime" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="IdPlanAccion" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[Auditoria] WHERE [IdAuditoria] = @IdAuditoria"
    InsertCommand="INSERT INTO [Auditoria].[Auditoria] ([Estado]) VALUES (@Estado)"
    SelectCommand="SELECT [IdAuditoria], [Estado] FROM [Auditoria].[Auditoria]" UpdateCommand="UPDATE [Auditoria].[Auditoria] SET [Estado] = @Estado WHERE [IdAuditoria] = @IdAuditoria">
    <DeleteParameters>
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Estado" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="IdAuditoria" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT COUNT(Q1.IdPlanAccion) AS Total
	                FROM (
                        SELECT  PA.IdPlanAccion
                        FROM Auditoria.Recomendacion AS R, Auditoria.Hallazgo AS H, Auditoria.Auditoria AS A, Auditoria.PlanAccion AS PA
                        WHERE PA.TipoForanea = 'RECOMENDACION' AND 
	                            PA.EstadoAuditor = 'ABIERTO' AND
                                PA.IdForanea = R.IdRecomendacion AND
                                R.IdHallazgo  = H.IdHallazgo AND 
                                H.IdAuditoria = A.IdAuditoria AND           
	                            A.IdAuditoria = @IdAuditoria
                        UNION
                        SELECT PA.IdPlanAccion
                        FROM Auditoria.Riesgo AS R, Auditoria.Hallazgo AS H, Auditoria.Auditoria AS A, Auditoria.PlanAccion AS PA
                        WHERE PA.TipoForanea = 'RIESGO' AND 
	                            PA.EstadoAuditor = 'ABIERTO' AND
                                PA.IdForanea = R.IdRiesgo AND
                                R.IdHallazgo  = H.IdHallazgo AND 
                                H.IdAuditoria = A.IdAuditoria AND           
	                            A.IdAuditoria = @IdAuditoria) as Q1">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaSel" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdPlaneacion], [Nombre] FROM [Auditoria].[Planeacion]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="
        SELECT H.[IdHallazgo], H.[Hallazgo] FROM [Auditoria].[Hallazgo] H
        INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria
        WHERE A.[IdPlaneacion] = @IdPlaneacion AND	            
            A.[Estado] = 'CUMPLIDA'">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource24" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="
        SELECT R.IdRecomendacion, CONVERT(VARCHAR(MAX), R.Observacion) Recomendacion, R.Estado, O.Nombre, R.Tipo, J.NombreHijo Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro 
        FROM Auditoria.Recomendacion R 
        INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo = H.IdHallazgo 
        INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria 
        INNER JOIN Parametrizacion.JerarquiaOrganizacional J ON J.idHijo = R.IdDependenciaAuditada 
        INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque 
        INNER JOIN Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque 
        INNER JOIN Auditoria.Objetivo O ON O.IdObjetivo = E.IdObjetivo 
        WHERE A.[IdPlaneacion] = @IdPlaneacion AND 
	        R.Tipo = 'Dependencia' AND 
            A.[Estado] = 'CUMPLIDA' 
        UNION 
        SELECT R.IdRecomendacion, CONVERT(VARCHAR(MAX), R.Observacion) Recomendacion, R.Estado, O.Nombre, R.Tipo, P.Nombre Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) FechaRegistro 
        FROM Auditoria.Recomendacion R 
        INNER JOIN Auditoria.Hallazgo H ON R.IdHallazgo = H.IdHallazgo 
        INNER JOIN Auditoria.Auditoria A ON H.IdAuditoria = A.IdAuditoria 
        INNER JOIN Procesos.Proceso P ON P.IdProceso = R.IdSubproceso 
        INNER JOIN Auditoria.DetalleEnfoque DE ON DE.IdDetalleEnfoque = H.IdDetalleEnfoque 
        INNER JOIN Auditoria.Enfoque E ON E.IdEnfoque = DE.IdEnfoque 
        INNER JOIN Auditoria.Objetivo O ON O.IdObjetivo = E.IdObjetivo 
        WHERE A.[IdPlaneacion] = @IdPlaneacion AND 
	        R.Tipo = 'Procesos' AND 
            A.[Estado] = 'CUMPLIDA'" UpdateCommand="UPDATE [Auditoria].[Recomendacion] SET [Estado] = @Estado WHERE [IdRecomendacion] = @IdRecomendacion">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdRecomendacion" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource25" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[PlanAccion] WHERE [IdPlanAccion] = @IdPlanAccion"
    InsertCommand="INSERT INTO [Auditoria].[PlanAccion] ([EstadoAuditado], [EstadoAuditor], [IdForanea], [Descripcion], [FechaCompromiso], [FechaRegistro], [IdUsuario], [TipoForanea]) VALUES (@EstadoAuditado, @EstadoAuditor, @IdForanea, @Descripcion, @FechaCompromiso, @FechaRegistro, @IdUsuario, @TipoForanea) SET @NewParameter=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdPlanAccion], [EstadoAuditado], [IdForanea], [Descripcion], CONVERT(VARCHAR(10), [FechaCompromiso],120) AS FechaCompromiso, CONVERT(VARCHAR(10), [FechaRegistro],120) AS FechaRegistro, [PlanAccion].[IdUsuario], [TipoForanea],[EstadoAuditor],[FechaCierreAuditado],[FechaCierreAuditor], [Usuario]
                   FROM   [Auditoria].[PlanAccion], [Listas].[Usuarios] AS LU
                   WHERE  [IdForanea] = @IdForanea AND
                          [TipoForanea] = @TipoForanea AND
                          [PlanAccion].[IdUsuario] = LU.[IdUsuario]" UpdateCommand="UPDATE [Auditoria].[PlanAccion] SET [EstadoAuditado] = @EstadoAuditado, [EstadoAuditor] = @EstadoAuditor, [FechaCierreAuditado] = @FechaCierreAuditado, [FechaCierreAuditor] = @FechaCierreAuditor WHERE [IdPlanAccion] = @IdPlanAccion"
    OnInserted="SqlDataSource25_On_Inserted">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodForaneaGen" Name="IdForanea" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="txtTipoForanea" Name="TipoForanea" PropertyName="Text"
            Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdPlanAccion" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="EstadoAuditado" Type="String" />
        <asp:Parameter Name="EstadoAuditor" Type="String" />
        <asp:Parameter Name="IdForanea" Type="Int64" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="TipoForanea" Type="String" />
        <asp:Parameter Name="FechaCompromiso" Type="DateTime" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Direction="Output" Name="NewParameter" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="EstadoAuditado" Type="String" />
        <asp:Parameter Name="EstadoAuditor" Type="String" />
        <asp:Parameter Name="FechaCierreAuditado" Type="DateTime" />
        <asp:Parameter Name="FechaCierreAuditor" Type="DateTime" />
        <asp:Parameter Name="IdPlanAccion" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource26" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[PlanAccionAvance] WHERE [IdPlanAccionAvance] = @IdPlanAccionAvance"
    InsertCommand="INSERT INTO [Auditoria].[PlanAccionAvance] ([IdPlanAccion], [Avance], [FechaRegistro], [IdUsuario]) VALUES (@IdPlanAccion, @Avance, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdPlanAccionAvance], [IdPlanAccion], [Avance], FechaRegistro, PAA.[IdUsuario], LU.Usuario
                   FROM [Auditoria].[PlanAccionAvance] AS PAA, [Listas].[Usuarios] AS LU
                   WHERE PAA.IdUsuario = LU.[IdUsuario] AND
                         PAA.IdPlanAccion = @IdPlanAccion" UpdateCommand="UPDATE [Auditoria].[PlanAccionAvance] SET [Avance] = @Avance WHERE [IdPlanAccionAvance] = @IdPlanAccionAvance">
    <SelectParameters>
        <asp:ControlParameter ControlID="lblIdPlanAccion" Name="IdPlanAccion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdPlanAccionAvance" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdPlanAccion" Type="Int64" />
        <asp:Parameter Name="Avance" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Avance" Type="String" />
        <asp:Parameter Name="IdPlanAccionAvance" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource100" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[Archivo] WHERE [IdArchivo] = @IdArchivo"
    InsertCommand="INSERT INTO [Auditoria].[Archivo] ([UrlArchivo], [Descripcion], [FechaRegistro], [IdUsuario], [IdRegistro], [IdControlUsuario]) 
                   VALUES (@UrlArchivo, @Descripcion, @FechaRegistro, @IdUsuario, @IdRegistro, @IdControlUsuario)"
    SelectCommand="SELECT A.[IdArchivo], A.[UrlArchivo], A.[Descripcion], CONVERT(VARCHAR(10), A.[FechaRegistro],120) AS FechaRegistro, A.[IdUsuario], LU.[Usuario] 
                   FROM   [Auditoria].[Archivo] AS A, [Listas].[Usuarios] AS LU 
                   WHERE  (A.[IdControlUsuario] = @IdControlUsuario AND A.[IdRegistro] = @IdRegistro AND A.IdUsuario = LU.[IdUsuario])"
    UpdateCommand="UPDATE [Auditoria].[Archivo] SET [UrlArchivo] = @UrlArchivo, [Descripcion] = @Descripcion, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario WHERE [IdArchivo] = @IdArchivo">
    <DeleteParameters>
        <asp:Parameter Name="IdArchivo" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="UrlArchivo" Type="String" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Name="IdRegistro" Type="Decimal" />
        <asp:Parameter Name="IdControlUsuario" Type="Decimal" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="8" Name="IdControlUsuario" Type="Decimal" />
        <asp:ControlParameter ControlID="txtCodPlanAccion" Name="IdRegistro" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="UrlArchivo" Type="String" />
        <asp:Parameter Name="Descripcion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Name="IdArchivo" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource101" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="Select IDENT_CURRENT('Auditoria.Archivo')+1 AS Maximo"></asp:SqlDataSource>
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
    InsertCommand="INSERT INTO [Notificaciones].[CorreosRecordatorio] ([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) VALUES (@IdCorreosEnviados, @NroDiasRecordatorio, @FechaFinal, @Estado, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdCorreosRecordatorio], [IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosRecordatorio]"
    UpdateCommand="UPDATE [Estado] = @Estado WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
        <asp:Parameter Name="NroDiasRecordatorio" Type="Int32" />
        <asp:Parameter Name="FechaFinal" Type="DateTime" />
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
        <asp:Panel ID="pnlPlaneacion" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupPlaneacion').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                            DisplayAfter="0">
                            <ProgressTemplate>
                                <div id="Background">
                                </div>
                                <div id="Progress">
                                    <asp:Label ID="Lbl11" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                        Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:Image ID="Img11" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <table>
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
                                            <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/Audit.png"
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
                    </td>
                </tr>
            </table>
        </asp:Panel>
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
                        <asp:Button ID="btnImgokActualizarPA" runat="server" Text="Ok" OnClick="btnImgokActualizarPA_Click" />
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
        <asp:Panel ID="pnlMsgBoxSN" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="td1">
                        <asp:Label ID="Label9" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBoxSN" runat="server" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnSI" runat="server" Text="Si" OnClick="btnSI_Click" />
                        <asp:Button ID="bntNO" runat="server" Text="No" OnClick="btnNO_Click" />
                        <asp:Button ID="Button3" runat="server" Text="Cancelar" OnClientClick="$find('mypopupSN').hide(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBoxSN" runat="server" PopupControlID="pnlMsgBoxSN"
            BehaviorID="mypopupSN" Enabled="True" TargetControlID="btndummy2" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy2" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBoxTXT" runat="server" Width="400px" CssClass="modalPopup" Style="display: none">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="td2">
                        <asp:Label ID="Label13" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr align="center">
                    <td valign="middle" align="right">
                        <asp:Label ID="Label22" runat="server" Text="Justificación:" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label>
                    </td>
                    <td valign="middle" align="center">
                        <asp:TextBox ID="txtJustificacion" runat="server" Width="300px" Rows="3" TextMode="MultiLine"
                            Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnOKTXT" runat="server" Text="Ok" OnClick="btnOKTXT_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBoxTXT" runat="server" PopupControlID="pnlMsgBoxTXT"
            BehaviorID="mypopupTXT" Enabled="True" TargetControlID="btndummy3" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy3" runat="server" Text="Button" Style="display: none" />
        <asp:TextBox ID="txtCodForaneaGen" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtTipoForanea" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCodPlanAccion" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCodAuditoriaSel" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtNomAuditoriaSel" runat="server" Visible="False"></asp:TextBox>
        <asp:Label ID="lblTipoCierre" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblFecCierreAuditado" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblEstadoAuditor" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblFecCierreAuditor" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblIdPlanAccion" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblAccion" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:TextBox ID="txtCodJerarquia" runat="server" Visible="False"></asp:TextBox>
        <table width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Planes de Acción" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr visible="true">
                <td>
                    <table align="center" width="100%">
                        <tr align="center" bgcolor="#EEEEEE" id="filaAuditoria" runat="server">
                            <td>
                                <table class="tabla">
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label67" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodPlaneacion" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="40px"></asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label66" runat="server" CssClass="Apariencia" Text="Planeación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomPlaneacion" runat="server" CssClass="Apariencia" Width="400px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnPlaneacion" runat="server" ImageUrl="~/Imagenes/Icons/table_select_row.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupPlanea" runat="server" BehaviorID="popupPlaneacion"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlPlaneacion" Position="Bottom"
                                                TargetControlID="imgBtnPlaneacion" OffsetX="-200">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaTabHallazgo" runat="server" bgcolor="#EEEEEE" visible="false">
                            <td>
                                <br />
                                <table class="tablaSinBordes">
                                    <tr align="center">
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" 
                                                Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center"
                                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Bold="False" OnRowCommand="GridView1_RowCommand"
                                                OnPageIndexChanging="GridView1_PageIndexChanging"
                                                ShowHeaderWhenEmpty="True" DataKeyNames="IdPlanAccion,IdAuditoria,NombreAuditoria">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
                                                <Columns>
                                                    <asp:BoundField DataField="IdAuditoria" HeaderText="Cod. Auditoría" SortExpression="IdAuditoria"
                                                        ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NombreAuditoria" HeaderText="Nombre Auditoría" SortExpression="NombreAuditoria">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdHallazgo" HeaderText="Código" ReadOnly="True" SortExpression="IdHallazgo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Hallazgo" HeaderText="Hallazgo" SortExpression="Hallazgo"
                                                        ReadOnly="True" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdPlanAccion" HeaderText="IdPlanAccion" SortExpression="IdPlanAccion"
                                                        HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Plan de Accion" SortExpression="Descripcion"
                                                        ReadOnly="True" HtmlEncode="False" HtmlEncodeFormatString="False" HeaderStyle-Width="620px" >
                                                        <ItemStyle HorizontalAlign="Left"/>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdNivelRiesgo" HeaderText="IdNivelRiesgo" SortExpression="IdNivelRiesgo"
                                                        ReadOnly="True" Visible="False">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NivelRiesgo" HeaderText="Nivel Riesgo" SortExpression="NivelRiesgo"
                                                        ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EdadHallazgo" HeaderText="Edad Hallazgo" SortExpression="EdadHallazgo"
                                                        ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Recomendación" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument="Recomendacion"
                                                                CommandName="Select" ImageUrl="~/Imagenes/Icons/Planner.png" ToolTip="Recomendación" /></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                </table>
                            </td>
                        </tr>
                        <tr id="filaTabPlanAccion" runat="server" bgcolor="#EEEEEE" visible="false">
                            <td>
                                <br />
                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
                                    AutoPostBack="True">
                                    <asp:TabPanel ID="TabPanel1" runat="server" Font-Underline="True" HeaderText="Recomendaciones">
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr align="center" bgcolor="#EEEEEE">
                                                    <td>
                                                        <table class="tablaSinBordes">
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:GridView ID="GridView1000" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri"
                                                                        Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center"
                                                                        OnSelectedIndexChanged="GridView1000_SelectedIndexChanged" Font-Bold="False"
                                                                        OnRowCommand="GridView1000_RowCommand" ShowHeaderWhenEmpty="True" DataKeyNames="Nombre,Nombre_DP">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdRecomendacion" HeaderText="Código" ReadOnly="True" SortExpression="IdRecomendacion">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Recomendacion" HeaderText="Recomendación" SortExpression="Recomendacion"
                                                                                ReadOnly="True" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Nombre" HeaderText="Objetivo" SortExpression="Nombre"
                                                                                ReadOnly="True" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Nombre_DP" HeaderText="Nombre_DP" SortExpression="Nombre_DP"
                                                                                ReadOnly="True" Visible="False" />
                                                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro"
                                                                                ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Plan de Acción" ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument="PlanAccion"
                                                                                        CommandName="Select" ImageUrl="~/Imagenes/Icons/Planner.png" ToolTip="Plan de Acción" /></ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                                        </table>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel2" runat="server" Font-Underline="True" HeaderText="Riesgos">
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr align="center" bgcolor="#EEEEEE">
                                                    <td>
                                                        <table class="tablaSinBordes">
                                                            <tr align="center">
                                                                <td>
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource1"
                                                                        Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" OnSelectedIndexChanged="GridView3_SelectedIndexChanged"
                                                                        Font-Bold="False" OnRowCommand="GridView3_RowCommand" ShowHeaderWhenEmpty="True"
                                                                        DataKeyNames="Nombre,Nombre_DP">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdRiesgo" HeaderText="Código" ReadOnly="True" SortExpression="IdRecomendacion">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Nombre" HeaderText="Riesgo" SortExpression="Nombre" ReadOnly="True"
                                                                                HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="NombreObj" HeaderText="Objetivo" SortExpression="Nombre"
                                                                                ReadOnly="True" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Nombre_DP" HeaderText="Nombre_DP" SortExpression="Nombre_DP"
                                                                                ReadOnly="True" Visible="False">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro"
                                                                                ReadOnly="True">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Plan de Acción" ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument="PlanAccion"
                                                                                        CommandName="Select" ImageUrl="~/Imagenes/Icons/Planner.png" ToolTip="Plan de Acción" /></ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                                        </table>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel3" runat="server" Font-Underline="True" HeaderText="Cierre">
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                            ForeColor="White" Text="Cierre de la Gestión de la Auditoría"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <br />
                                                        <br />
                                                        <table class="tabla">
                                                            <tr align="center">
                                                                <td>
                                                                    <asp:Button ID="btnCierre" runat="server" Text="Cerrar Auditoría" OnClick="btnCierre_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnRevertirEstado" runat="server" Text="Devolver a Recomendaciones"
                                                                        OnClick="btnRevertirEstado_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="Label23" runat="server" Text="Label" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#BBBBBB">
                                                    <td>
                                                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                            Text="Se efectuará el cierre si y solo si todos los planes de acción hayan sido cerrados."></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </td>
                        </tr>
                        <tr align="center" id="filaDetallePlan" runat="server" visible="false">
                            <td bgcolor="#EEEEEE">
                                <table class="tabla" width="100%">
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td colspan="2">
                                            <asp:Label ID="Label78" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Creación del Plan"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label1" runat="server" CssClass="Apariencia" Text="Objetivo:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtObjetivo" runat="server" CssClass="Apariencia" Width="402px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label79" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodRec" runat="server" CssClass="Apariencia" Width="50px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label86" runat="server" Text="Tipo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtTipoPoD" runat="server" CssClass="Apariencia" Width="150px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="lblPoD" runat="server" Text="Proceso:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtNombrePoD" runat="server" CssClass="Apariencia" Width="402px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="lblRR" runat="server" CssClass="Apariencia" Text="Recomendación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtRecomendacion" CssClass="Apariencia" Width="402px" runat="server"
                                                Text="" ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px"
                                                Font-Bold="False" Height="18px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label2" runat="server" Text="Fecha de Compromiso:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtFecPlan" runat="server"></asp:TextBox>
                                        </td>
                                        <asp:CalendarExtender ID="txtFecPlan_CalendarExtender" runat="server" Enabled="True"
                                            Format="yyyy-MM-dd" TargetControlID="txtFecPlan"></asp:CalendarExtender>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" CssClass="Apariencia" Text="Acción:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDescPlan" runat="server" Enabled="True" Columns="100" TextMode="MultiLine"
                                                Font-Names="Calibri" Rows="8"></asp:TextBox>
                                            <%-- <asp:HtmlEditorExtender ID="HtmlEditorExtender3" runat="server" Enabled="True" 
                                                TargetControlID="txtDescPlan">
                                                <Toolbar> 
                                                                <asp:Undo />
                                                                <asp:Redo />
                                                                <asp:Bold/>
                                                                <asp:Italic />
                                                                <asp:Underline />
                                                                <asp:StrikeThrough />
                                                                <asp:JustifyLeft />
                                                                <asp:JustifyCenter />
                                                                <asp:JustifyRight />
                                                                <asp:JustifyFull />
                                                                <asp:InsertOrderedList />
                                                                <asp:InsertUnorderedList />
                                                                <asp:CreateLink />
                                                                <asp:UnLink />
                                                                <asp:Delete />
                                                                <asp:Cut />
                                                                <asp:Copy />                                                       
                                                                <asp:Paste />
                                                                <asp:Indent />
                                                                <asp:Outdent />
                                                                <asp:InsertHorizontalRule />
                                                                <asp:HorizontalSeparator />
                                                            </Toolbar>
                                            </asp:HtmlEditorExtender>--%>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label84" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUsuarioPlan" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label85" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFecCreacionPlan" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table class="tablaSinBordes">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgInsertarPlan" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            OnClick="btnImgInsertarPlan_Click" Style="text-align: right" ToolTip="Guardar" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgActualizarPlan" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            OnClick="btnImgActualizarPlan_Click" Style="text-align: right" ToolTip="Guardar" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgCancelarDetPlan" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            OnClick="btnImgCancelarDetPlan_Click" ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaPlanAccion" align="center" bgcolor="#EEEEEE" runat="server" visible="false">
                            <td>
                                <table width="100%">
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Listado de Planes de Acción por Recomendación"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <br />
                                            <table class="tabla">
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label4" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCodRecPlan" runat="server" CssClass="Apariencia" Width="50px"
                                                            Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label5" runat="server" CssClass="Apariencia" Text="Recomendación:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtRecPlan" CssClass="Apariencia" Width="402px" runat="server" Text=""
                                                            ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px"
                                                            Font-Bold="False" Height="18px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <table class="tablaSinBordes">
                                                <tr align="center">
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource25"
                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" OnSelectedIndexChanged="GridView4_SelectedIndexChanged"
                                                            Font-Bold="False" OnRowCommand="GridView4_RowCommand" ShowHeaderWhenEmpty="True"
                                                            DataKeyNames="EstadoAuditado,EstadoAuditor,FechaCierreAuditado,FechaCierreAuditor,Usuario"
                                                            OnRowDataBound="GridView4_RowDataBound">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdPlanAccion" HeaderText="Código" InsertVisible="False"
                                                                    ReadOnly="True" SortExpression="IdPlanAccion">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EstadoAuditado" HeaderText="EstadoAuditado" SortExpression="EstadoAuditado"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="IdForanea" HeaderText="IdForanea" SortExpression="IdForanea"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"
                                                                    HtmlEncode="False" HtmlEncodeFormatString="False" HeaderStyle-Width="600px" >
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FechaCompromiso" HeaderText="Fecha de Compromiso" SortExpression="FechaCompromiso">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="TipoForanea" HeaderText="TipoForanea" SortExpression="TipoForanea"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="EstadoAuditor" HeaderText="EstadoAuditor" SortExpression="EstadoAuditor"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="FechaCierreAuditado" HeaderText="FechaCierreAuditado"
                                                                    SortExpression="FechaCierreAuditado" Visible="false" />
                                                                <asp:BoundField DataField="FechaCierreAuditor" HeaderText="FechaCierreAuditor" SortExpression="FechaCierreAuditor"
                                                                    Visible="false" />
                                                                <asp:TemplateField HeaderText="Estado Auditado" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Icons/unlock.png" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estado Auditor" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Icons/unlock.png" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cierre Auditado" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgActualizarEstado" runat="server" CausesValidation="False"
                                                                            CommandArgument="ActualizarEstado1" CommandName="Select" ImageUrl="~/Imagenes/Icons/KeychainAccess.png"
                                                                            ToolTip="Efectuar Cierre" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cierre Auditor" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgActualizarEstado2" runat="server" CausesValidation="False"
                                                                            CommandArgument="ActualizarEstado2" CommandName="Select" ImageUrl="~/Imagenes/Icons/KeychainAccess.png"
                                                                            ToolTip="Efectuar Cierre" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Registrar Avances" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnLog" runat="server" CausesValidation="False" CommandArgument="Avances"
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/history.png" ToolTip="Log de Avances" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Anexos" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgSubirDoc" runat="server" CausesValidation="False" ImageUrl="~/Imagenes/Icons/file.png"
                                                                            CommandArgument="Anexar" CommandName="Select" ToolTip="Anexar" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgActPlan" runat="server" CausesValidation="False" CommandArgument="Actualizar"
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/edit.png" ToolTip="Editar" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                                    Visible="False" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                                        <asp:ImageButton ID="imgBtnInsertarPlan" runat="server" CausesValidation="False"
                                                            ToolTip="Insertar" CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarPlan_Click"
                                                            Text="Insert" />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaAvance" align="center" bgcolor="#EEEEEE" runat="server" visible="false">
                            <td>
                                <table width="100%">
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Listado de Avances por Plan de Acción"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <br />
                                            <table class="tabla">
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="lblPARR" runat="server" CssClass="Apariencia" Text="Recomendación:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPlanRec" CssClass="Apariencia" Width="402px" runat="server" Text=""
                                                            ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px"
                                                            Font-Bold="False" Height="18px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label12" runat="server" CssClass="Apariencia" Text="Plan de Acción:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtPlanPlan" CssClass="Apariencia" Width="402px" runat="server" Text=""
                                                            ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px"
                                                            Font-Bold="False" Height="18px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <table class="tablaSinBordes">
                                                <tr align="center">
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource26"
                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" Font-Bold="False" ShowHeaderWhenEmpty="True" DataKeyNames="IdPlanAccionAvance">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdPlanAccionAvance" HeaderText="Código" InsertVisible="False"
                                                                    ReadOnly="True" SortExpression="IdPlanAccionAvance">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IdPlanAccion" HeaderText="IdPlanAccion" SortExpression="IdPlanAccion"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="Avance" HeaderText="Avance" SortExpression="Avance" HtmlEncode="False"
                                                                    HtmlEncodeFormatString="False" />
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                                        <asp:ImageButton ID="imgBtnInsertarAvance" runat="server" CausesValidation="False"
                                                            ToolTip="Insertar" CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarAvance_Click"
                                                            Text="Insert" />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:Button ID="btnVolverPlanes" runat="server" Text="Volver a Listado de Planes"
                                                OnClick="btnVolverPlanes_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaRegistrarAvances" runat="server" align="center" visible="false">
                            <td bgcolor="#EEEEEE">
                                <table class="tabla" width="100%">
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td colspan="2">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Registro de Avances"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="lblAvanceRR" runat="server" CssClass="Apariencia" Text="Recomendación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtAvanceRec" CssClass="Apariencia" Width="402px" runat="server" Text=""
                                                ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px"
                                                Font-Bold="False" Height="18px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" CssClass="Apariencia" Text="Plan de Acción:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtAvancePlan" CssClass="Apariencia" Width="402px" runat="server"
                                                Text="" ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px"
                                                Font-Bold="False" Height="18px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label15" runat="server" CssClass="Apariencia" Text="Avance:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAvance" runat="server" Enabled="True" Columns="100" TextMode="MultiLine"
                                                Font-Names="Calibri" Rows="8"></asp:TextBox>
                                            <%--<asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" Enabled="True" 
                                                TargetControlID="txtAvance">
                                                <Toolbar> 
                                                                <asp:Undo />
                                                                <asp:Redo />
                                                                <asp:Bold/>
                                                                <asp:Italic />
                                                                <asp:Underline />
                                                                <asp:StrikeThrough />
                                                                <asp:JustifyLeft />
                                                                <asp:JustifyCenter />
                                                                <asp:JustifyRight />
                                                                <asp:JustifyFull />
                                                                <asp:InsertOrderedList />
                                                                <asp:InsertUnorderedList />
                                                                <asp:CreateLink />
                                                                <asp:UnLink />
                                                                <asp:Delete />
                                                                <asp:Cut />
                                                                <asp:Copy />                                                       
                                                                <asp:Paste />
                                                                <asp:Indent />
                                                                <asp:Outdent />
                                                                <asp:InsertHorizontalRule />
                                                                <asp:HorizontalSeparator />
                                                            </Toolbar>
                                            </asp:HtmlEditorExtender>--%>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label16" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUsuarioAvance" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label17" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFecCreacionAvance" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table class="tablaSinBordes">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgInsertarAvance" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            OnClick="btnImgInsertarAvance_Click" Style="text-align: right" ToolTip="Guardar" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgCancelarRegAvances" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            OnClick="btnImgCancelarRegAvances_Click" ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaPlanAccionRiesgo" runat="server" align="center" bgcolor="#EEEEEE" visible="false">
                            <td>
                                <table width="100%">
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Listado de Planes de Acción por Riesgo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <br />
                                            <table class="tabla">
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label18" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCodRiesgoPlan" runat="server" CssClass="Apariencia" Width="50px"
                                                            Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label19" runat="server" CssClass="Apariencia" Text="Riesgo:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRiesgoPlan" runat="server" Enabled="False" CssClass="Apariencia"
                                                            Font-Names="Calibri" Rows="1" Width="400px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <table class="tablaSinBordes">
                                                <tr align="center">
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource25"
                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" OnSelectedIndexChanged="GridView5_SelectedIndexChanged"
                                                            Font-Bold="False" OnRowCommand="GridView5_RowCommand" ShowHeaderWhenEmpty="True"
                                                            DataKeyNames="EstadoAuditado,EstadoAuditor,FechaCierreAuditado,FechaCierreAuditor,Usuario"
                                                            OnRowDataBound="GridView5_RowDataBound">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdPlanAccion" HeaderText="Código" InsertVisible="False"
                                                                    ReadOnly="True" SortExpression="IdPlanAccion">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EstadoAuditado" HeaderText="EstadoAuditado" SortExpression="EstadoAuditado"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="IdForanea" HeaderText="IdForanea" SortExpression="IdForanea"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"
                                                                    HtmlEncode="False" HtmlEncodeFormatString="False"></asp:BoundField>
                                                                <asp:BoundField DataField="FechaCompromiso" HeaderText="Fecha de Compromiso" SortExpression="FechaCompromiso">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="TipoForanea" HeaderText="TipoForanea" SortExpression="TipoForanea"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="EstadoAuditor" HeaderText="EstadoAuditor" SortExpression="EstadoAuditor"
                                                                    Visible="false" />
                                                                <asp:BoundField DataField="FechaCierreAuditado" HeaderText="FechaCierreAuditado"
                                                                    SortExpression="FechaCierreAuditado" Visible="false" />
                                                                <asp:BoundField DataField="FechaCierreAuditor" HeaderText="FechaCierreAuditor" SortExpression="FechaCierreAuditor"
                                                                    Visible="false" />
                                                                <asp:TemplateField HeaderText="Estado Auditado" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Icons/unlock.png" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estado Auditor" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Icons/unlock.png" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cierre Auditado" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgActualizarEstado" runat="server" CausesValidation="False"
                                                                            CommandArgument="ActualizarEstado1" CommandName="Select" ImageUrl="~/Imagenes/Icons/KeychainAccess.png"
                                                                            ToolTip="Efectuar Cierre" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cierre Auditor" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgActualizarEstado2" runat="server" CausesValidation="False"
                                                                            CommandArgument="ActualizarEstado2" CommandName="Select" ImageUrl="~/Imagenes/Icons/KeychainAccess.png"
                                                                            ToolTip="Efectuar Cierre" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Registrar Avances" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnLog" runat="server" CausesValidation="False" CommandArgument="Avances"
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/history.png" ToolTip="Log de Avances" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Anexos" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgSubirDoc2" runat="server" CausesValidation="False" ImageUrl="~/Imagenes/Icons/file.png"
                                                                            CommandArgument="Anexar" CommandName="Select" ToolTip="Anexar" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgActPlan2" runat="server" CausesValidation="False" CommandArgument="Actualizar"
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/edit.png" ToolTip="Actualizar" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                                                    Visible="False" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
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
                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ToolTip="Insertar"
                                                            CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarPlan_Click"
                                                            Text="Insert" />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaVolverRecomendacion" runat="server" align="center" visible="false" bgcolor="#EEEEEE">
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnVolverRecomendacion" runat="server" OnClick="btnVolverRecomendacion_Click"
                                                Text="Volver a Recomendaciones" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaVolverHallazgo" runat="server" align="center" visible="false" bgcolor="#EEEEEE">
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnVolverHallazgo" runat="server" OnClick="btnVolverHallazgo_Click"
                                                Text="Volver a Plan de Acción" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaInformeAuditoria" runat="server" visible="false" align="center" bgcolor="#EEEEEE">
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <%--<asp:Button ID="bntInformeAud" runat="server" Text="Informe de Auditoria" 
                                                PostBackUrl="~/Formularios/Auditoria/Admin/AudAdmReporteAuditoriaSeg.aspx" 
                                                />--%>
                                            <asp:Button ID="bntInformeAud" runat="server" Text="Informe de Auditoria" OnClick="bntInformeAud_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="filaAnexos" runat="server" align="center" visible="false">
                            <td>
                                <table width="100%" bgcolor="#EEEEEE" class="tabla">
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Documentos Adjuntos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="filaGridAnexos" align="center">
                                        <td>
                                            <table class="tablaSinBordes">
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView100" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                            DataSourceID="SqlDataSource100" ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True"
                                                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" HorizontalAlign="Center"
                                                            Font-Names="Calibri" Font-Size="Small" OnSelectedIndexChanged="GridView100_SelectedIndexChanged"
                                                            AllowPaging="True" AllowSorting="True" Font-Bold="False" DataKeyNames="IdArchivo">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" SortExpression="IdArchivo"
                                                                    InsertVisible="False" ReadOnly="True" Visible="False" />
                                                                <asp:BoundField DataField="UrlArchivo" HeaderText="Archivo" SortExpression="UrlArchivo" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"
                                                                    HtmlEncode="False" HtmlEncodeFormatString="False" />
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Carga" SortExpression="FechaRegistro">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnImgDownloadDoc" runat="server" CausesValidation="False" ImageUrl="~/Imagenes/Icons/download_folder.png"
                                                                            Text="Descargar" ToolTip="Descargar Archivo" CommandArgument="Descargar" CommandName="Select" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
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
                                                        <asp:ImageButton ID="imgBtnInsertarArchivo" runat="server" CausesValidation="False"
                                                            ToolTip="Insertar" CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png" OnClick="imgBtnInsertarArchivo_Click"
                                                            Text="Insert" />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <asp:Button ID="btnVolverArchivo" runat="server" Text="Volver" OnClick="btnVolverArchivo_Click" />
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <br>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="filaSubirAnexos" align="center" visible="false">
                                        <td>
                                            <br />
                                            <table class="tabla">
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label26" runat="server" Text="Adjuntar documento:" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="400px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label28" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDescArchivo" runat="server" Width="400" MaxLength="100"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <table class="tablaSinBordes">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgBtnAdjuntar" runat="server" ImageUrl="~/Imagenes/Icons/uploads_folder (1).png"
                                                                        ToolTip="Adjuntar" OnClick="imgBtnAdjuntar_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnImgCancelarArchivo" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="btnImgCancelarArchivo_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
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
    <Triggers>
        <asp:PostBackTrigger ControlID="bntInformeAud" />
        <asp:PostBackTrigger ControlID="GridView1" />
    </Triggers>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
