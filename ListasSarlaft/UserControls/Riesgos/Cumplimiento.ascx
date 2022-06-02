<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cumplimiento.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Riesgos.Cumplimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
<style type="text/css">
    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList
    {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .style1
    {
        height: 74px;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource200" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) VALUES (@IdEvento, 

@Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario, @Tipo) SET @NewParameter2=SCOPE_IDENTITY();"
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
    InsertCommand="INSERT INTO [Notificaciones].[CorreosRecordatorio] ([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) VALUES (@IdCorreosEnviados, @NroDiasRecordatorio, @FechaFinal, 

@Estado, @FechaRegistro, @IdUsuario)"
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
<uc:OkMessageBox ID="omb" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Legislación"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbGridLegislacion" runat="server">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    PageSize="10">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="CodigoLegislacion" />
                                        <asp:BoundField HeaderText="Nombre" DataField="NombreLegislacion" />
                                        <asp:BoundField HeaderText="Tipo" DataField="NombreTipoLegislacion" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />
                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnImgModificar" runat="server" CausesValidation="False" CommandName="Modificar"
                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" ToolTip="Editar"
                                                    CommandArgument="<%# Container.DataItemIndex %>" />
                                                <asp:ImageButton ID="btnImgEliminar" runat="server" CausesValidation="False" CommandName="Borrar"
                                                    ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar" ToolTip="Eliminar"
                                                    CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--<asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar"
                                            CommandName="Modificar" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar"
                                            CommandName="Borrar" />--%>
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
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Insertar" OnClick="ImageButton2_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tbCampos" runat="server" visible="false">
                        <tr>
                            <td>
                                <table>
                                    <tr align="center">
                                        <td colspan="4" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label11" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Información de la legislación"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label2" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox1" runat="server" Width="150px" Enabled="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" Text="Nombre:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox2" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                                InitialValue="" ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label4" runat="server" Text="Tipo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="155px" Font-Names="Calibri"
                                                Font-Size="Small">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1"
                                                InitialValue="---" ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label5" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox4" runat="server" Height="50px" TextMode="MultiLine" Width="405px"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox4"
                                                InitialValue="" ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label6" runat="server" Text="Vigencia desde:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox5" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextBox5_CalendarExtender" runat="server" Enabled="True"
                                                TargetControlID="TextBox5" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox5"
                                                InitialValue="" ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label7" runat="server" Text="Vigencia hasta:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox6" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextBox6_CalendarExtender" runat="server" Enabled="True"
                                                TargetControlID="TextBox6" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox6"
                                        InitialValue="" ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label13" runat="server" Text="Fecha de cierre:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox3" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="TextBox3"
                                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label30" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Estado:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList7" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="155px">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="DropDownList7"
                                                ForeColor="Red" ValidationGroup="validatelegislacion" InitialValue="---">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label123" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE" colspan="3">
                                            <asp:TextBox ID="TextBox34" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="400px" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                                Font-Size="Small">0</asp:Label>
                                            <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                OnClientClick="return false;" />
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="TextBox34"
                                        ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>--%>
                                            <asp:PopupControlExtender ID="popupDependencia1" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia1" BehaviorID="popup1"
                                                PopupControlID="pnlDependencia1" OffsetY="-200">
                                            </asp:PopupControlExtender>
                                            <asp:Panel ID="pnlDependencia1" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                    <tr align="right" bgcolor="#5D7B9D">
                                                        <td>
                                                            <asp:ImageButton ID="btnClosepp1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                OnClientClick="$find('popup1').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" AutoGenerateDataBindings="False">
                                                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Button ID="BtnOk1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup1').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label8" runat="server" Text="Actualizacion:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox7" runat="server" Width="400px" Font-Names="Calibri" Font-Size="Small" MaxLength="50"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox7"
                                        InitialValue="" ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr align="left" id="trUsuario" runat="server" visible="false">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label1" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left" id="trFecha" runat="server" visible="false">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="Fecha registro:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:Label ID="Label12" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr id="trComentariosArchivos" runat="server" visible="false">
                                        <td>
                                            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Font-Names="Calibri"
                                                Font-Size="Small" Width="500px">
                                                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Justificación cambios" Font-Names="Calibri"
                                                    Font-Size="Small">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label28" runat="server" Text="Justificación De Los Cambios" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label29" runat="server" Text="Justificación:" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="TextBox12" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                                                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox12"
                                                                                    ForeColor="Red" ValidationGroup="validatelegislacion">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="right">
                                                                            <td colspan="2">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                                ToolTip="Cancelar" Visible="False" OnClick="ImageButton9_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td colspan="2">
                                                                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView4_RowCommand">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="IdComentario" HeaderText="IdComentario" Visible="False" />
                                                                                        <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                                        <asp:BoundField DataField="ComentarioCorto" HeaderText="Justificación" />
                                                                                        <asp:BoundField DataField="Comentario" HeaderText="Comentario" Visible="False" />
                                                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver comentario"
                                                                                            CommandName="Ver" />
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
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
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Documentos adjuntos" Font-Names="Calibri"
                                                    Font-Size="Small">
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label26" runat="server" Text="Documentos adjuntos" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td>
                                                                    <table>
                                                                        <tr align="center">
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label27" runat="server" Text="Adjuntar documento :" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                                    ToolTip="Adjuntar" OnClick="ImageButton7_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td colspan="3">
                                                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                                    HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView3_RowCommand">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="False" />
                                                                                        <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                                        <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo" />
                                                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                                                                            CommandName="Descargar" />
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
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
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                            </asp:TabContainer>
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
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" ValidationGroup="validatelegislacion" OnClick="ImageButton4_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" ValidationGroup="validatelegislacion" OnClick="ImageButton5_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImageButton6_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBoxOkNo" runat="server" TargetControlID="btndummyOkNo"
            PopupControlID="pnlMsgBoxOkNo" OkControlID="btnCancelarOkNo" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummyOkNo" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lbldummyOkNo" runat="server" Text="" Visible="False"></asp:Label>
        <asp:Panel ID="pnlMsgBoxOkNo" runat="server" Width="400px" Style="display: none;"
            BorderColor="#575757" BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaptionOkNo">&nbsp;
                        <asp:Label ID="lblCaptionOkNo" runat="server" Text="Atención" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfoOkNo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBoxOkNo" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptarOkNo" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small"
                            OnClick="btnAceptarOkNo_Click" />
                        <asp:Button ID="btnCancelarOkNo" runat="server" Text="Cancelar" Font-Names="Calibri"
                            Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
