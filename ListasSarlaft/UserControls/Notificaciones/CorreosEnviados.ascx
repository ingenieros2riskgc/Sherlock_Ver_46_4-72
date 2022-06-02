<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CorreosEnviados.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Notificaciones.CorreosEnviados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .style1
    {
        width: 100%;
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
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario]) VALUES (@IdEvento, @Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT NE.IdEvento, NE.NombreEvento, NE.Modulo, CE.IdCorreosEnviados, CE.Destinatario, CE.Copia, CE.Otros, CE.Asunto, CE.Cuerpo, CE.Estado,CE.Tipo, CE.FechaEnvio, CE.IdUsuario, LU.Usuario, FechaRegistro
                    FROM Notificaciones.CorreosEnviados CE, Notificaciones.Evento AS NE, Listas.Usuarios AS LU
                    WHERE CE.IdEvento = NE.IdEvento
						  AND LU.IdUsuario = CE.IdUsuario" UpdateCommand="UPDATE [CorreosEnviados] SET [IdEvento] = @IdEvento, [Destinatario] = @Destinatario, [Copia] = @Copia, [Otros] = @Otros, [Asunto] = @Asunto, [Cuerpo] = @Cuerpo, [Estado] = @Estado, [IdRegistro] = @IdRegistro, [FechaEnvio] = @FechaEnvio, [FechaRegistro] = @FechaRegistro, [IdUsuario] = @IdUsuario WHERE [IdCorreosEnviados] = @IdCorreosEnviados">
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
        <asp:Parameter Name="IdRegistro" Type="Decimal" />
        <asp:Parameter Name="FechaEnvio" Type="DateTime" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdEvento" Type="Decimal" />
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
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdEvento], [NombreEvento] FROM [Notificaciones].[Evento]">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource200" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario]) VALUES (@IdEvento, @Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario) SET @NewParameter2=SCOPE_IDENTITY();"
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
        <asp:Panel ID="pnlDependencia" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
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
        <asp:Panel ID="pnlDependencia2" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup1').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TreeView ID="TreeView2" ExpandDepth="3" runat="server" Font-Names="Calibri"
                            Style="overflow: auto" Font-Size="Small" LineImagesFolder="~/TreeLineImages"
                            ForeColor="Black" ShowLines="True" Target="_self" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged"
                            Font-Bold="False" Height="400px">
                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                        </asp:TreeView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup1').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlDependencia3" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup2').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TreeView ID="TreeView3" ExpandDepth="3" runat="server" Font-Names="Calibri"
                            Style="overflow: auto" Font-Size="Small" LineImagesFolder="~/TreeLineImages"
                            ForeColor="Black" ShowLines="True" Target="_self" OnSelectedNodeChanged="TreeView3_SelectedNodeChanged"
                            Font-Bold="False" Height="400px">
                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                        </asp:TreeView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup2').hidePopup(); return false;" />
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
        <table align="center" width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Correos Destinatarios"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" bgcolor="#EEEEEE" id="filaGrid" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" DataKeyNames="Usuario,IdEvento,IdCorreosEnviados,Destinatario,Copia,Otros,Asunto,Cuerpo,Estado,FechaRegistro,FechaEnvio"
                                    BorderStyle="Solid" GridLines="Vertical" CssClass="Apariencia" Font-Bold="False"
                                    OnRowCommand="GridView1_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdEvento" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                            SortExpression="IdEvento" Visible="False">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Modulo" HeaderText="Modulo" InsertVisible="False" ReadOnly="True"
                                            SortExpression="Módulo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreEvento" HeaderText="Etapa" InsertVisible="False"
                                            ReadOnly="True" SortExpression="NombreEvento" HtmlEncode="False" HtmlEncodeFormatString="False">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdCorreosEnviados" HeaderText="Código" SortExpression="IdCorreosEnviados">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Destinatario" HeaderText="Destinatario" SortExpression="Destinatario"
                                            Visible="False" />
                                        <asp:BoundField DataField="Copia" HeaderText="Copia" SortExpression="Copia" Visible="False" />
                                        <asp:BoundField DataField="Otros" HeaderText="Otros" SortExpression="Otros" Visible="False" />
                                        <asp:BoundField DataField="Asunto" HeaderText="Asunto" SortExpression="Asunto" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cuerpo" HeaderText="Cuerpo" SortExpression="Cuerpo" 
                                            Visible="False" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                            Visible="False" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                            Visible="False" />
                                        <asp:BoundField DataField="FechaEnvio" HeaderText="Fecha de Envio" SortExpression="FechaEnvio">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" />
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
                    </table>
                    <br />
                </td>
            </tr>
            <tr id="filaBoton" bgcolor="#EEEEEE" runat="server">
                <td align="center">
                    <asp:Button ID="btnEnviarCorreos" runat="server" Text="Enviar Correos en Cola" OnClick="btnEnviarCorreos_Click" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                        <ProgressTemplate>
                            <div id="Background">
                            </div>
                            <div id="Progress">
                                <asp:Label ID="Label111" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <br />
                                <asp:Image ID="Image21" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table border="1" cellspacing="0" cellpadding="2" bordercolor="White" width="100%">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Modulo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtModulo" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Etapa:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEtapa" runat="server" Enabled="False" Width="300px" CssClass="Apariencia"></asp:TextBox>
                                <asp:Label ID="lblIdControl" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblIdCorreosEnviados" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Destinatario:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td2" runat="server">
                                <table>
                                    <tr>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtDestinatario" runat="server" Enabled="False" Font-Names="Calibri"
                                                Font-Size="Small" Width="300px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgDestinatario" runat="server" Visible="false" ImageUrl="~/Imagenes/Icons/Email2.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupDestinatario" runat="server" BehaviorID="popup"
                                                DynamicServicePath="" Enabled="True" ExtenderControlID="" OffsetY="-100" PopupControlID="pnlDependencia"
                                                Position="Right" TargetControlID="imgDestinatario">
                                            </asp:PopupControlExtender>
                                            <asp:ImageButton ID="imgBorrarDest" runat="server" Visible="false" ImageUrl="~/Imagenes/Icons/deletemail.png"
                                                OnClick="imgBorrarDest_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Copia1:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td1" runat="server">
                                <table>
                                    <tr>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtCopia1" runat="server" Enabled="False" Font-Names="Calibri" Font-Size="Small"
                                                Width="300px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgCopia1" runat="server" Visible="false" ImageUrl="~/Imagenes/Icons/Email2.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupCopia1" runat="server" BehaviorID="popup1" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" OffsetY="-100" PopupControlID="pnlDependencia2"
                                                Position="Right" TargetControlID="imgCopia1">
                                            </asp:PopupControlExtender>
                                            <asp:ImageButton ID="imgBorrarCopia1" runat="server" Visible="false" ImageUrl="~/Imagenes/Icons/deletemail.png"
                                                OnClick="imgBorrarCopia1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Otros:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOtros" runat="server" Width="500px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Asunto:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAsunto" runat="server" Width="500px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Cuerpo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtCuerpo" runat="server" Text="Label" CssClass="Apariencia" Font-Bold="false"
                                    Width="802px" ForeColor="#666666" BorderColor="Silver" BorderStyle="Ridge" BorderWidth="1px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Estado:" CssClass="Apariencia" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEstado" runat="server" Width="100px" Enabled="False" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Fecha de Envío:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaEnvio" runat="server" Width="150px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="150px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Insertar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ToolTip="Actualizar"
                                                Visible="False" />
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
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
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
