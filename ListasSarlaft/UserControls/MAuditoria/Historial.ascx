<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Historial.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Historial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .gridViewHeader a:link {
        text-decoration: none;
    }

    .ajax__html_editor_extender_texteditor {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource1" runat="server"
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdDetalleTipo], [NombreDetalle] FROM [Parametrizacion].[DetalleTipos] WHERE ([IdTipo] = @IdTipo)">
    <SelectParameters>
        <asp:Parameter DefaultValue="10" Name="IdTipo" Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdPlaneacion], [Nombre] FROM [Auditoria].[Planeacion]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource18" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdAuditoria], [Tema] 
                   FROM [Auditoria].[Auditoria] 
                   WHERE ([IdPlaneacion] = @IdPlaneacion AND [Estado] = 'CERRADA')"><%-- CUMPLIDA --%>
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodPlaneacion" Name="IdPlaneacion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource19" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [AuditoriaObjetivo].[IdObjetivo], [Objetivo].Nombre
                   FROM [Auditoria].[AuditoriaObjetivo], [Auditoria].[Objetivo]
                   WHERE ([IdAuditoria] = @IdAuditoria) AND
                   [AuditoriaObjetivo].IdObjetivo = [Objetivo].IdObjetivo">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaSel" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource20" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [Enfoque].[IdEnfoque], [Enfoque].Descripcion 
                   FROM [Auditoria].[AudObjEnfoque], [Auditoria].[Enfoque] 
                   WHERE (([AudObjEnfoque].[IdAuditoria] = @IdAuditoria) AND ([AudObjEnfoque].[IdObjetivo] = @IdObjetivo)
                   AND [Enfoque].[IdEnfoque] = [AudObjEnfoque].[IdEnfoque]
                   AND [Enfoque].[IdObjetivo] = [AudObjEnfoque].[IdObjetivo])">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaSel" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="txtCodObjetivoSel" Name="IdObjetivo" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource21" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdDetalleEnfoque], [Descripcion] FROM [Auditoria].[DetalleEnfoque] WHERE ([IdEnfoque] = @IdEnfoque)">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodEnfoqueSel" Name="IdEnfoque" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdHallazgo], [Hallazgo] FROM [Auditoria].[Hallazgo] 
    WHERE ([IdAuditoria] = @IdAuditoria AND
           [IdDetalleEnfoque] = @IdDetalleEnfoque)">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaSel" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="txtCodLiteralSel" Name="IdDetalleEnfoque" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource24" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT R.IdRecomendacion,  CONVERT(VARCHAR(MAX), R.Observacion) AS Recomendacion, R.Estado, O.Nombre, R.Tipo, J.NombreHijo AS Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) AS FechaRegistro
                    FROM Auditoria.Recomendacion AS R, Auditoria.Hallazgo AS H, Auditoria.Auditoria AS A, Parametrizacion.JerarquiaOrganizacional AS J, Auditoria.DetalleEnfoque AS DE, Auditoria.Enfoque AS E, Auditoria.Objetivo AS O
                    WHERE A.IdAuditoria = @IdAuditoria AND
	                      H.IdAuditoria = A.IdAuditoria AND
	                      R.IdHallazgo  = H.IdHallazgo AND
                          R.IdHallazgo = @IdHallazgo AND
	                      DE.IdDetalleEnfoque = H.IdDetalleEnfoque AND
	                      E.IdEnfoque = DE.IdEnfoque AND
	                      O.IdObjetivo = E.IdObjetivo AND  
	                      J.idHijo = R.IdDependenciaAuditada AND
	                      R.Tipo = 'Dependencia'
                    UNION
                    SELECT R.IdRecomendacion, CONVERT(VARCHAR(MAX), R.Observacion) AS Recomendacion, R.Estado, O.Nombre, R.Tipo, P.Nombre AS Nombre_DP, CONVERT(VARCHAR(10), R.[FechaRegistro],120) AS FechaRegistro
                    FROM Auditoria.Recomendacion AS R, Auditoria.Hallazgo AS H, Auditoria.Auditoria AS A, Procesos.Proceso AS P, Auditoria.DetalleEnfoque AS DE, Auditoria.Enfoque AS E, Auditoria.Objetivo AS O
                    WHERE A.IdAuditoria = @IdAuditoria AND
	                      H.IdAuditoria = A.IdAuditoria AND
	                      R.IdHallazgo  = H.IdHallazgo AND
                          R.IdHallazgo = @IdHallazgo AND
	                      DE.IdDetalleEnfoque = H.IdDetalleEnfoque AND
	                      E.IdEnfoque = DE.IdEnfoque AND
	                      O.IdObjetivo = E.IdObjetivo AND 
	                      P.IdProceso = R.IdSubproceso AND 
	                      R.Tipo = 'Procesos'"
    UpdateCommand="UPDATE [Auditoria].[Recomendacion] SET [Estado] = @Estado WHERE [IdRecomendacion] = @IdRecomendacion">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodAuditoriaSel" Name="IdAuditoria" PropertyName="Text"
            Type="Int64" />
        <asp:ControlParameter ControlID="txtCodHallazgoSel" Name="IdHallazgo" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdRecomendacion" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource25" runat="server"
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[RecomendacionEstados] WHERE [IdRecomendacionEstados] = @IdRecomendacionEstados"
    InsertCommand="INSERT INTO [Auditoria].[RecomendacionEstados] ([IdRecomendacion], [Estado], [Observacion], [FechaRegistro], [IdUsuario]) VALUES (@IdRecomendacion, @Estado, @Observacion, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdRecomendacionEstados], [Estado], [Observacion], FechaRegistro, RE.[IdUsuario], Usuario
                   FROM [Auditoria].[RecomendacionEstados] AS RE, [Listas].[Usuarios] AS U
                   WHERE RE.[IdRecomendacion] = @IdRecomendacion AND
                         RE.[IdUsuario] = U.[IdUsuario]
                   ORDER BY IdRecomendacionEstados"
    UpdateCommand="UPDATE [Auditoria].[RecomendacionEstados] SET [IdRecomendacion] = @IdRecomendacion, [Estado] = @Estado, [Observacion] = @Observacion, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario WHERE [IdRecomendacionEstados] = @IdRecomendacionEstados">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCodRecGen" Name="IdRecomendacion" PropertyName="Text"
            Type="Int64" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="IdRecomendacionEstados" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdRecomendacion" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdRecomendacion" Type="Int64" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Observacion" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Name="IdRecomendacionEstados" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource26" runat="server"
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[Auditoria] WHERE [IdAuditoria] = @IdAuditoria"
    InsertCommand="INSERT INTO [Auditoria].[Auditoria] ([Estado]) VALUES (@Estado)"
    SelectCommand="SELECT [Estado], [IdAuditoria] FROM [Auditoria].[Auditoria]"
    UpdateCommand="UPDATE [Auditoria].[Auditoria] SET [Estado] = @Estado WHERE [IdAuditoria] = @IdAuditoria">
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
<asp:SqlDataSource ID="SqlDataSource100" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[ArchivoHallazgoAuditoria] WHERE [IdArchivo] = @IdArchivo"
    InsertCommand="INSERT INTO [Auditoria].[ArchivoHallazgoAuditoria] ([IdRegistroAuditoria], [NombreUsuario],[Descripcion],[UrlArchivo],[Archivo]) 
                   VALUES ( @IdRegistroAuditoria, @Descripcion, @UrlArchivo, @Archivo)"
    SelectCommand="SELECT A.[IdArchivo], A.[UrlArchivo], A.[Descripcion], CONVERT(VARCHAR(10), A.[FechaRegistro],120) AS FechaRegistro] 
                   FROM   [Auditoria].[ArchivoHallazgoAuditoria] AS A
                   WHERE  (A.[IdRegistroAuditoria] = @IdRegistroAuditoria AND A.[IdRegistro] = @IdRegistro)"
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
        <asp:ControlParameter ControlID="txtCodAuditoriaSel" Name="IdRegistro" PropertyName="Text"
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
<asp:SqlDataSource ID="SqlDataSource200" runat="server"
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
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
<asp:SqlDataSource ID="SqlDataSource201" runat="server"
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
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
        <asp:Label ID="lblAccion" runat="server" Text="Label" Visible="False"></asp:Label>
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
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HtmlEncode="False" HtmlEncodeFormatString="False">
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
        <asp:Panel ID="pnlAuditoria" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupAuditoria').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView6" runat="server" DataSourceID="SqlDataSource18" Font-Names="Calibri"
                            Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                            BorderStyle="Solid" DataKeyNames="IdAuditoria" AllowPaging="True" AllowSorting="True"
                            ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" GridLines="Vertical"
                            CellPadding="4" OnSelectedIndexChanged="GridView6_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="IdAuditoria" HeaderText="Código" InsertVisible="False"
                                    ReadOnly="True" SortExpression="IdAuditoria">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Tema" HeaderText="Tema" SortExpression="Tema" HtmlEncode="False" HtmlEncodeFormatString="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/audit.png"
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
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button4" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupAuditoria').hidePopup(); return false;" />
                    </td>
                </tr>

            </table>

        </asp:Panel>

        <asp:Panel ID="pnlObjetivo" runat="server" CssClass="popup" Style="display: none" Width="400px">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupObjetivo').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView7" runat="server" DataSourceID="SqlDataSource19" Font-Names="Calibri"
                            Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                            BorderStyle="Solid" DataKeyNames="IdObjetivo" AllowPaging="True" AllowSorting="True"
                            ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" GridLines="Vertical"
                            CellPadding="4" OnSelectedIndexChanged="GridView7_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="IdObjetivo" HeaderText="Código" InsertVisible="False"
                                    ReadOnly="True" SortExpression="IdObjetivo">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HtmlEncode="False" HtmlEncodeFormatString="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/audit.png"
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
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button5" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupObjetivo').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEnfoque" runat="server" CssClass="popup" Style="display: none" Width="400px">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupEnfoque').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView9" runat="server" DataSourceID="SqlDataSource20" Font-Names="Calibri"
                            Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                            BorderStyle="Solid" DataKeyNames="IdEnfoque" AllowPaging="True" AllowSorting="True"
                            ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" GridLines="Vertical"
                            CellPadding="4" OnSelectedIndexChanged="GridView9_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="IdEnfoque" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                    SortExpression="IdEnfoque">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion"
                                    HtmlEncode="False" HtmlEncodeFormatString="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/audit.png"
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
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button6" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupEnfoque').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlLiteral" runat="server" CssClass="popup" Style="display: none"
            Width="600px">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupLiteral').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView10" runat="server" DataSourceID="SqlDataSource21" Font-Names="Calibri"
                            Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                            BorderStyle="Solid" DataKeyNames="IdDetalleEnfoque" AllowPaging="True" AllowSorting="True"
                            ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" GridLines="Vertical"
                            CellPadding="4" OnSelectedIndexChanged="GridView10_SelectedIndexChanged" PageSize="3">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="IdDetalleEnfoque" HeaderText="Código" InsertVisible="False"
                                    ReadOnly="True" SortExpression="IdDetalleEnfoque">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion"
                                    HtmlEncode="False" HtmlEncodeFormatString="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/audit.png"
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
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button7" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupLiteral').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlHallazgo" runat="server" CssClass="popup" Style="display: none"
            Width="600px">
            <table width="100%" class="tabla">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popupHallazgo').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:GridView ID="GridView3" runat="server" DataSourceID="SqlDataSource22" Font-Names="Calibri"
                            Font-Size="Small" HeaderStyle-CssClass="gridViewHeader" AutoGenerateColumns="False"
                            BorderStyle="Solid" DataKeyNames="IdHallazgo" AllowPaging="True" AllowSorting="True"
                            ShowHeaderWhenEmpty="True" ForeColor="#333333" Font-Bold="False" GridLines="Vertical"
                            CellPadding="4" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" PageSize="3">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="IdHallazgo" HeaderText="Código" InsertVisible="False"
                                    ReadOnly="True" SortExpression="IdHallazgo">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Hallazgo" HeaderText="Hallazgo" SortExpression="Hallazgo"
                                    HtmlEncode="False" HtmlEncodeFormatString="False">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" HeaderText="Acción" SelectImageUrl="~/Imagenes/Icons/audit.png"
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
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupHallazgo').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <table width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Historial de Auditorias"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
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
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label55" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodAuditoriaSel" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="40px"></asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label68" runat="server" CssClass="Apariencia" Text="Auditoría:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomAuditoriaSel" runat="server" CssClass="Apariencia" Width="400px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnAuditoria" runat="server" ImageUrl="~/Imagenes/Icons/table_select_row.png"
                                                OnClientClick="return false;" OnClick="imgBtnAuditoria_Click" />
                                            <asp:PopupControlExtender ID="popupAuditoria" runat="server" BehaviorID="popupAuditoria"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlAuditoria" Position="Bottom"
                                                TargetControlID="imgBtnAuditoria" OffsetX="-200">
                                            </asp:PopupControlExtender>
                                            <asp:TextBox ID="txtCodRecGen" runat="server" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label69" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodObjetivoSel" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="40px"></asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label70" runat="server" CssClass="Apariencia" Text="Objetivo:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomObjetivoSel" runat="server" CssClass="Apariencia" Width="400px"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnObjetivo" runat="server" ImageUrl="~/Imagenes/Icons/table_select_row.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupObjetivo" runat="server" BehaviorID="popupObjetivo"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlObjetivo" Position="Bottom"
                                                TargetControlID="imgBtnObjetivo" OffsetX="-200">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label71" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodEnfoqueSel" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="40px"></asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label72" runat="server" CssClass="Apariencia" Text="Enfoque:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtNomEnfoqueSel" CssClass="Apariencia" Width="402px" runat="server"
                                                ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px" Font-Bold="False" Height="18px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnEnfoque" runat="server" ImageUrl="~/Imagenes/Icons/table_select_row.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupEnfoque" runat="server" BehaviorID="popupEnfoque"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlEnfoque" Position="Bottom"
                                                TargetControlID="imgBtnEnfoque" OffsetX="-200">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label73" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodLiteralSel" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="40px"></asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label74" runat="server" CssClass="Apariencia" Text="Literal:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtNomLiteralSel" runat="server" CssClass="Apariencia" Width="402px"
                                                ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px" Font-Bold="False" Height="18px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnLiteral" runat="server" ImageUrl="~/Imagenes/Icons/table_select_row.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupLiteral" runat="server" BehaviorID="popupLiteral"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlLiteral" Position="Left"
                                                TargetControlID="imgBtnLiteral" OffsetX="-300">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label4" runat="server" CssClass="Apariencia" Text="Código:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodHallazgoSel" runat="server" CssClass="Apariencia" Enabled="False"
                                                Width="40px"></asp:TextBox>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label5" runat="server" CssClass="Apariencia" Text="Hallazgo:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtNomHallazgoSel" runat="server" CssClass="Apariencia" Width="402px"
                                                ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px" Font-Bold="False" Height="18px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnHallazgo" runat="server" ImageUrl="~/Imagenes/Icons/table_select_row.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupHallazgo" runat="server" BehaviorID="popupHallazgo"
                                                Enabled="True" ExtenderControlID="" PopupControlID="pnlHallazgo" Position="Left"
                                                TargetControlID="imgBtnHallazgo" OffsetX="-300">
                                            </asp:PopupControlExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center" bgcolor="#EEEEEE" id="filaGridRec" runat="server" visible="false">
                            <td>
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            <table class="tablaSinBordes">
                                                <tr align="center">
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource24"
                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                            Font-Bold="False" OnRowCommand="GridView1_RowCommand"
                                                            ShowHeaderWhenEmpty="True" DataKeyNames="Nombre,Nombre_DP">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdRecomendacion" HeaderText="Código"
                                                                    ReadOnly="True" SortExpression="IdRecomendacion">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Recomendacion" HeaderText="Recomendación"
                                                                    SortExpression="Recomendacion" ReadOnly="True" HtmlEncode="False"
                                                                    HtmlEncodeFormatString="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado"
                                                                    ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Nombre" HeaderText="Objetivo"
                                                                    SortExpression="Nombre" ReadOnly="True" HtmlEncode="False"
                                                                    HtmlEncodeFormatString="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo"
                                                                    ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Nombre_DP" HeaderText="Nombre_DP"
                                                                    SortExpression="Nombre_DP" ReadOnly="True" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación"
                                                                    SortExpression="FechaRegistro" ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Log de Estados" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnLog" runat="server" CausesValidation="False" CommandArgument="LogEstados"
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/history.png" ToolTip="Log de Estados" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandArgument="ActualizarEstado"
                                                                            CommandName="Select" ImageUrl="~/Imagenes/Icons/edit.png" ToolTip="Editar Estado" />
                                                                    </ItemTemplate>
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
                                                <tr align="center">
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
                        <tr align="center" bgcolor="#EEEEEE" id="filaCierreRec" runat="server" visible="false">
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:Button ID="bntInformeRec" runat="server" Text="Informe de Recomendaciones" OnClick="bntInformeRec_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="bntAdjuntos" runat="server" Text="Adjuntos" OnClick="bntAdjuntos_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center" id="filaDetalleRecomendacion" runat="server" visible="false">
                            <td bgcolor="#EEEEEE">
                                <br />
                                <table class="tabla"
                                    width="100%">
                                    <tr align="center" bgcolor="#5D7B9D">
                                        <td colspan="2">
                                            <asp:Label ID="Label78" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Small"
                                                ForeColor="White" Text="Estado"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label1" runat="server" CssClass="Apariencia" Text="Objetivo:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtObjetivo" runat="server" CssClass="Apariencia"
                                                Width="377px" Enabled="false"></asp:TextBox>
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
                                            <asp:TextBox ID="txtNombrePoD" runat="server" CssClass="Apariencia" Width="377px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label81" runat="server" CssClass="Apariencia" Text="Recomendación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtRecomendacion" CssClass="Apariencia" Width="402px" runat="server" Text=""
                                                ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px" Font-Bold="False" Height="18px"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label2" runat="server"
                                                Text="Estado:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="ddlEstado" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="221px" DataSourceID="SqlDataSource1" DataTextField="NombreDetalle"
                                                DataValueField="NombreDetalle" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" CssClass="Apariencia" Text="Observación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDescAct" runat="server" Enabled="false" Columns="100" TextMode="MultiLine"
                                                Font-Names="Calibri" Rows="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label84" runat="server" CssClass="Apariencia" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUsuarioRec" runat="server" CssClass="Apariencia"
                                                Enabled="False" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td align="left" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label85" runat="server" CssClass="Apariencia" Text="Fecha de Creación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFecCreacionRec" runat="server" CssClass="Apariencia"
                                                Enabled="False" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table class="tablaSinBordes">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnImgCancelarRec" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            OnClick="btnImgCancelarRec_Click" ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr align="center" bgcolor="#EEEEEE" id="filaLogEstados" runat="server" visible="false">
                            <td>
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            <table class="tablaSinBordes">
                                                <tr align="center">
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" DataSourceID="SqlDataSource25"
                                                            Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                            Font-Bold="False" OnRowCommand="GridView1_RowCommand"
                                                            ShowHeaderWhenEmpty="True" DataKeyNames="IdRecomendacionEstados">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField DataField="IdRecomendacionEstados"
                                                                    HeaderText="IdRecomendacionEstados" InsertVisible="False" ReadOnly="True"
                                                                    SortExpression="IdRecomendacionEstados" Visible="False" />
                                                                <asp:BoundField DataField="Estado" HeaderText="Estado"
                                                                    SortExpression="Estado" />
                                                                <asp:BoundField DataField="Observacion" HeaderText="Observación"
                                                                    SortExpression="Observacion" HtmlEncode="False"
                                                                    HtmlEncodeFormatString="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación"
                                                                    SortExpression="FechaRegistro" ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario"
                                                                    SortExpression="IdUsuario" Visible="False" />
                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario"
                                                                    SortExpression="Usuario">
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
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            OnClick="btnImgCancelarLog_Click" ToolTip="Cancelar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" id="filaGridAnexos" align="center" visible="false">
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:GridView ID="GridView100" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader"
                                                HorizontalAlign="Center" OnRowCommand="GridView100_RowCommand" ShowHeaderWhenEmpty="True">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" InsertVisible="False" ReadOnly="True" SortExpression="IdArchivo" Visible="False" />
                                                    <asp:BoundField DataField="UrlArchivo" HeaderText="Archivo" SortExpression="UrlArchivo" />
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HtmlEncode="False" HtmlEncodeFormatString="False" SortExpression="Descripcion" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Carga" SortExpression="FechaRegistro">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario" Visible="False" />
                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" SortExpression="NombreUsuario">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar" CommandName="Descargar" />
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
                                    <tr align="center">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            OnClick="btnImgCancelarAdjunto_Click" ToolTip="Cancelar" />
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
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="bntInformeRec" />
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
