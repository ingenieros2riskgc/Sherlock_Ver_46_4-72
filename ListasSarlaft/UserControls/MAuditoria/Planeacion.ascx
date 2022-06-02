<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Planeacion.ascx.cs" Inherits="ListasSarlaft.UserControls.MAuditoria.Planeacion" %>
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
</style>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Auditoria].[Planeacion] WHERE [IdPlaneacion] = @IdPlaneacion"
    InsertCommand="INSERT INTO [Auditoria].[Planeacion] ([Nombre], [FechaPlaneacion], [FechaCierre], [IdUsuario], [FechaRegistro]) VALUES (@Nombre, @FechaPlaneacion, @FechaCierre, @IdUsuario, @FechaRegistro) SET @NewParameter=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdPlaneacion], [Nombre], CONVERT(VARCHAR(10),[Planeacion].[FechaPlaneacion],120) AS FechaPlaneacion, CONVERT(VARCHAR(10),[Planeacion].[FechaCierre],120) AS FechaCierre, [Planeacion].[IdUsuario],  [Usuario], CONVERT(VARCHAR(10),[Planeacion].[FechaRegistro],120) AS FechaRegistro
                    FROM   [Auditoria].[Planeacion], [Listas].[Usuarios]
                    WHERE  [Planeacion].[IdUsuario] = [Usuarios].[idUsuario]"
    UpdateCommand="UPDATE [Auditoria].[Planeacion] 
                   SET [Nombre] = @Nombre, [FechaPlaneacion] = @FechaPlaneacion, [FechaCierre] = @FechaCierre
                   WHERE [IdPlaneacion] = @IdPlaneacion"
    OnInserted="SqlDataSource1_On_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="IdPlaneacion" Type="Int64" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="FechaPlaneacion" Type="DateTime" />
        <asp:Parameter Name="FechaCierre" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Direction="Output" Name="NewParameter" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Nombre" Type="String" />
        <asp:Parameter Name="FechaPlaneacion" Type="DateTime" />
        <asp:Parameter Name="FechaCierre" Type="DateTime" />
        <asp:Parameter Name="IdPlaneacion" Type="Int64" />
    </UpdateParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource200" runat="server"
    ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdControlUsuario], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario]) VALUES (@IdControlUsuario, @Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario) SET @NewParameter2=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdCorreosEnviados], [IdControlUsuario], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosEnviados]"
    UpdateCommand="UPDATE [Notificaciones].[CorreosEnviados] SET [FechaEnvio] = @FechaEnvio, [Estado] = @Estado WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    OnInserted="SqlDataSource200_On_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdControlUsuario" Type="Decimal" />
        <asp:Parameter Name="Destinatario" Type="String" />
        <asp:Parameter Name="Copia" Type="String" />
        <asp:Parameter Name="Otros" Type="String" />
        <asp:Parameter Name="Asunto" Type="String" />
        <asp:Parameter Name="Cuerpo" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
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
                        <asp:Label ID="lblCaption" runat="server" Text="Atención"
                            Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server"
                            ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label>
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
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox" BehaviorID="mypopup"
            Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>

        <table width="80%" align="center">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Planeación" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" id="filaGrid" runat="server">
                <td bgcolor="#EEEEEE">
                    <table width="100%">
                        <tr align="center">
                            <td>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                                ForeColor="#333333" GridLines="Vertical" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                CssClass="Apariencia" Font-Bold="False"
                                                DataKeyNames="Usuario"
                                                OnRowCommand="GridView1_RowCommand">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdPlaneacion" HeaderText="Código"
                                                        ReadOnly="True" SortExpression="IdPlaneacion" InsertVisible="False">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre"
                                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaPlaneacion" HeaderText="Fecha de Planeación"
                                                        SortExpression="FechaPlaneacion" ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaCierre" HeaderText="Fecha de Cierre"
                                                        SortExpression="FechaCierre" ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario"
                                                        SortExpression="IdUsuario" Visible="False" />
                                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario"
                                                        SortExpression="Usuario" Visible="False" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación"
                                                        SortExpression="FechaRegistro" ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Auditorias">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnImgAuditoria" runat="server" CausesValidation="False" CommandName="Select"
                                                                ImageUrl="~/Imagenes/Icons/auditoria.png" Text="Seleccionar" ToolTip="Auditorias" PostBackUrl="~/Formularios/Auditoria/Admin/AudAdmAuditoria.aspx" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" />
                                                            <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" OnClick="btnImgEliminar_Click" CommandArgument="<%# Container.DataItemIndex %>"
                                                                ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" CommandName="Eliminar" ToolTip="Eliminar" />
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
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="imgBtnInsertar" runat="server" CausesValidation="False" CommandName="Insert"
                                                ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert"
                                                OnClick="imgBtnInsertar_Click" ToolTip="Insertar" />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr align="center" id="filaDetalle" runat="server" visible="False">
                <td bgcolor="#EEEEEE">


                    <table width="100%" class="tabla">
                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="False" Width="100px" TextMode="SingleLine"
                                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Nombre:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtNombre" runat="server" Enabled="True" Width="377px" TextMode="SingleLine"
                                    Font-Names="Calibri" Font-Size="Small" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="updatePlan"
                                    ControlToValidate="txtNombre" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label13" runat="server" Text="Fecha Inicio:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtFecIni" runat="server" Enabled="True" Width="100px" TextMode="SingleLine"
                                    Font-Names="Calibri" Font-Size="Small" Font-Bold="False"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFecIni_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                    TargetControlID="txtFecIni"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="updatePlan"
                                    ControlToValidate="txtFecIni" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label14" runat="server" Text="Fecha Cierre:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtFecFin" runat="server" Enabled="True" Width="100px" TextMode="SingleLine"
                                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFecFin_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                    TargetControlID="txtFecFin"></asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="txtFecFin" ControlToCompare="txtFecIni" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha fin no puede ser menor a Fecha inicio" Type="Date"
                                    Operator="GreaterThanEqual" ValidationGroup="updatePlan">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="updatePlan"
                                    ControlToValidate="txtFecFin" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label19" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Guardar" ValidationGroup="updatePlan"/>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ValidationGroup="updatePlan"
                                                ToolTip="Guardar" />
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
    </ContentTemplate>
    <%--                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
                </Triggers>--%>
</asp:UpdatePanel>

<%--                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
                </Triggers>--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
