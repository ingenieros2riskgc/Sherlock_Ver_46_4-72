<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Riesgos.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Riesgos" %>
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

    .autocomplete_completionListElement
    {
        margin: 0px !important;
        background-color: inherit;
        color: windowtext;
        border: buttonshadow;
        border-width: 1px;
        cursor: 'default';
        overflow: auto;
        height: 200px;
        text-align: left;
        list-style-type: none;
    }
    /* AutoComplete highlighted item */
    .autocomplete_highlightedListItem
    {
        background-color: #ffff99;
        color: black;
        padding: 1px;
    }
    /* AutoComplete item */
    .autocomplete_listItem
    {
        background-color: window;
        color: windowtext;
        padding: 1px;
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
<uc:OkMessageBox ID="omb" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="popupCausas" runat="server" CssClass="popup" Width="800px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
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
                <tr align="right" bgcolor="#5D7B9D">
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close3.png"
                            OnClientClick="$find('popupActividad2').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:GridView ID="GVcausasRiesgos" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                     ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False"
                                    OnPreRender="GVcausasRiesgos_PreRender"
                            >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdCausas" HeaderText="Código" ReadOnly="True" SortExpression="IdCausas" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Causa" DataField="NombreCausas" />
                                        <asp:TemplateField HeaderText="Asociar" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:CheckBox ID="CBasociarCausa" runat="server" ></asp:CheckBox> 
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
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
                    <td align="center">
                        <asp:Label ID="LtextoCausas" runat="server" Text="Riesgo sin causas asignadas" Visible="false"></asp:Label>
                        
                    </td>
                    
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:Button ID="Bok" runat="server" Text="Aceptar" CssClass="Apariencia" CausesValidation="true" ValidationGroup="GEvalorCalificacion" OnClick="Bok_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label140" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Riesgos"></asp:Label>
                    <asp:Label ID="LCodRiesgo" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text=""></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbGridRiesgos" runat="server">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label69" runat="server" Text="Código:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox11" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label70" runat="server" Text="Nombre:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox17" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label71" runat="server" Text="Cadena de valor" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList19" runat="server" Width="400px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList19_SelectedIndexChanged">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label72" runat="server" Text="Macroproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList20" runat="server" Width="400px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList20_SelectedIndexChanged">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label73" runat="server" Text="Proceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList21" runat="server" Width="400px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList21_SelectedIndexChanged">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label74" runat="server" Text="Subproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList22" runat="server" Width="400px" Font-Names="Calibri"
                                                Font-Size="Small">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label80" runat="server" Text="Riesgos globales" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList4" runat="server" Width="400px" Font-Names="Calibri"
                                                Font-Size="Small">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr hidden>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label94" runat="server" Text="Recalificar Riesgos" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:Button ID="BtnCalificacionMasiva" runat="server" Text="Recalificación Masiva de Riesgos" Width="100%" Height="30px"
                                                OnClick="BtnCalificacionMasiva_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="center">
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                                ToolTip="Consultar" OnClick="ImageButton12_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImageButton15_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table runat="server">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                    DataKeyNames="IdRiesgo,ListaCausas"
                                    OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Id" DataField="IdRiesgo" Visible="false" />
                                        <asp:BoundField HeaderText="Código" DataField="Codigo" />
                                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                        <asp:BoundField HeaderText="Riesgo global" DataField="NombreClasificacionRiesgo" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" Visible="false" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar"
                                            CommandName="Modificar" />
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
                                <asp:ImageButton ID="ImageButton8" runat="server" CommandName="Insert" ImageUrl="~/Imagenes/Icons/Add.png"
                                    OnClick="ImageButton8_Click" ToolTip="Insertar" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbModificarRiesgo" runat="server" visible="false">
                        <tr>
                            <td>
                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Font-Names="Calibri"
                                    Font-Size="Small" Width="900px">
                                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Riesgo" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <HeaderTemplate>
                                            Riesgo
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Image ID="Image1" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" Text="Ubicación" ForeColor="#000D26" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel111" runat="server">
                                                            <table bgcolor="#EEEEEE">
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label16" runat="server" Text="Región" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList47" runat="server" Width="200px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList47_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="DropDownList47"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label22" runat="server" Text="Pais" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList48" runat="server" Width="200px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList48_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="DropDownList48"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label23" runat="server" Text="Departamento/Región" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList49" runat="server" Width="200px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList49_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="DropDownList49"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label24" runat="server" Text="Ciudad" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList50" runat="server" Width="200px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList50_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="DropDownList50"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label25" runat="server" Text="Oficina/Sucursal" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList51" runat="server" Width="200px" Font-Names="Calibri"
                                                                            Font-Size="Small">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="DropDownList51"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="Panel111"
                                                            CollapsedSize="0" Collapsed="True" ExpandControlID="Image1" CollapseControlID="Image1"
                                                            ImageControlID="Image1" ExpandedImage="~/Imagenes/Icons/expand.jpg" CollapsedImage="~/Imagenes/Icons/collapse.jpg"
                                                            BehaviorID="_content_CollapsiblePanelExtender1"></asp:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Image ID="Image2" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="Cadena de valor" ForeColor="#000D26"
                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel222" runat="server">
                                                            <table>
                                                                <tr align="center">
                                                                    <td colspan="2">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr align="center">
                                                                                            <td bgcolor="#BBBBBB">
                                                                                                <asp:Label ID="Label26" runat="server" Text="Mapa de procesos" Font-Names="Calibri"
                                                                                                    Font-Size="Small"></asp:Label>
                                                                                            </td>
                                                                                            <td bgcolor="#EEEEEE">
                                                                                                <asp:ImageButton ID="ImageButton28" ToolTip="Ver" ImageUrl="~/Imagenes/Icons/mapaweb.png"
                                                                                                    runat="server"></asp:ImageButton>
                                                                                                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImageButton28"
                                                                                                    PopupControlID="Panel3" OkControlID="ImageButton29" BackgroundCssClass="FondoAplicacion"
                                                                                                    DropShadow="True" DynamicServicePath="" BehaviorID="_content_ModalPopupExtender2">
                                                                                                </asp:ModalPopupExtender>
                                                                                                <asp:Panel ID="Panel3" runat="server" Width="750px" Style="display: none;" BorderColor="#575757"
                                                                                                    BackColor="White" BorderStyle="Solid">
                                                                                                    <table width="100%">
                                                                                                        <tr class="topHandle" style="background-color: #5D7B9D">
                                                                                                            <td align="center">
                                                                                                                <asp:Label ID="Label51" runat="server" Text="Mapa de procesos" Font-Names="Calibri"
                                                                                                                    Font-Size="Small"></asp:Label>
                                                                                                                <br />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr align="center">
                                                                                                            <td>
                                                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Aplicacion/MapaProceso.png"></asp:Image>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr align="center">
                                                                                                            <td>
                                                                                                                <asp:ImageButton ID="ImageButton29" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                                                    ToolTip="Cancelar"></asp:ImageButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td bgcolor="#BBBBBB">
                                                                                                <asp:Label ID="Label150" runat="server" Text="Macroproceso estrategicos" Font-Names="Calibri"
                                                                                                    Font-Size="Small"></asp:Label>
                                                                                            </td>
                                                                                            <td bgcolor="#BBBBBB">
                                                                                                <asp:Label ID="Label151" runat="server" Text="Macroproceso core" Font-Names="Calibri"
                                                                                                    Font-Size="Small"></asp:Label>
                                                                                            </td>
                                                                                            <td bgcolor="#BBBBBB">
                                                                                                <asp:Label ID="Label152" runat="server" Text="Macroproceso de soporte" Font-Names="Calibri"
                                                                                                    Font-Size="Small"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:ListBox ID="ListBox4" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:ListBox ID="ListBox5" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:ListBox ID="ListBox6" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label153" runat="server" Text="Cadena de valor" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList52" runat="server" Width="400px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList52_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="DropDownList52"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label154" runat="server" Text="Macroproceso" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList53" runat="server" Width="400px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList53_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="DropDownList53"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label155" runat="server" Text="Proceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList54" runat="server" Width="400px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList54_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label44" runat="server" Text="Subproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList7" runat="server" Width="400px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList7_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label156" runat="server" Text="Actividad" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList55" runat="server" Width="400px" Font-Names="Calibri"
                                                                            Font-Size="Small">
                                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="Panel222"
                                                            CollapsedSize="0" Collapsed="True" ExpandControlID="Image2" CollapseControlID="Image2"
                                                            ImageControlID="Image2" ExpandedImage="~/Imagenes/Icons/expand.jpg" CollapsedImage="~/Imagenes/Icons/collapse.jpg"
                                                            BehaviorID="_content_CollapsiblePanelExtender2"></asp:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Image ID="Image4" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label118" runat="server" Text="Información del riesgo" ForeColor="#000D26"
                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel444" runat="server">
                                                            <table>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label157" runat="server" Text="Riesgos globales" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList56" runat="server" Width="455px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList56_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="DropDownList56"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label158" runat="server" Text="Clasificación general" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList57" runat="server" Width="455px" AutoPostBack="True"
                                                                            Font-Names="Calibri" Font-Size="Small" OnSelectedIndexChanged="DropDownList57_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label159" runat="server" Text="Clasificación particular" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList58" runat="server" Width="455px" Font-Names="Calibri"
                                                                            Font-Size="Small">
                                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trRiesgoOperativo3" runat="server" visible="False" align="left">
                                                                    <td colspan="2" runat="server" bgcolor="#BBBBBB">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label160" runat="server" Text="Factor de riesgo operativo" Font-Names="Calibri"
                                                                                        Font-Size="Small"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:DropDownList ID="DropDownList59" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                        Width="445px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList59_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="DropDownList59"
                                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label161" runat="server" Text="Sub factor riesgo operativo" Font-Names="Calibri"
                                                                                        Font-Size="Small"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:DropDownList ID="DropDownList60" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                        Width="445px">
                                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="DropDownList60"
                                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trRiesgoOperativo4" runat="server" visible="False" align="left">
                                                                    <td colspan="2" runat="server" bgcolor="#BBBBBB">
                                                                        <table>
                                                                            <tr id="trRO1" runat="server">
                                                                                <td runat="server">
                                                                                    <asp:Label ID="Label47" runat="server" Text="Tipo de evento" Font-Names="Calibri"
                                                                                        Font-Size="Small"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE" runat="server">
                                                                                    <asp:DropDownList ID="DropDownList15" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                        Width="400px">
                                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="DropDownList15"
                                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label48" runat="server" Text="Riesgo asociado" Font-Names="Calibri"
                                                                                        Font-Size="Small"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:DropDownList ID="DropDownList16" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                        Width="400px">
                                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="DropDownList16"
                                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trLavadoActivos2" runat="server" visible="False" align="left">
                                                                    <td colspan="2" runat="server" bgcolor="#BBBBBB">
                                                                        <table>
                                                                            <tr>
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label53" runat="server" Text="Riesgo asociado" Font-Names="Calibri"
                                                                                        Font-Size="Small"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:CheckBoxList ID="CheckBoxList7" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label54" runat="server" Text="Factor de riesgo LA/FT" Font-Names="Calibri"
                                                                                        Font-Size="Small"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:CheckBoxList ID="CheckBoxList8" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                                    </asp:CheckBoxList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label162" runat="server" Text="Código" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:TextBox ID="TextBox2" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"
                                                                            Enabled="False" Height="21px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label163" runat="server" Text="Nombre" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:TextBox ID="TextBox3" runat="server" Width="450px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="TextBox3"
                                                                            ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label164" runat="server" Text="Descripción del riesgo" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:TextBox ID="TextBox4" runat="server" Height="50px" TextMode="MultiLine" Width="450px"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="TextBox4"
                                                                            ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="Center">
                                                                    <td colspan="2" bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label88" runat="server" Text="Causas y Consecuencias Asociadas" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td colspan="2" bgcolor="#BBBBBB">
                                                                        <table>
                                                                            <tr align="left">
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label89" runat="server" Text="Causas" Font-Names="Calibri" Font-Size="Small"
                                                                                        Width="70px"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:Panel ID="PnlCausasAsoc" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList"
                                                                                        Width="300px">
                                                                                        <asp:CheckBoxList ID="ckbCausaAsoc" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                            Enabled="False">
                                                                                        </asp:CheckBoxList>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label90" runat="server" Text="Consecuencias" Font-Names="Calibri"
                                                                                        Font-Size="Small" Width="90px"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:Panel ID="PnlConsecuenciasAsoc" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList"
                                                                                        Width="350px">
                                                                                        <asp:CheckBoxList ID="ckbConsecuenciaAsoc" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                            Enabled="False">
                                                                                        </asp:CheckBoxList>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td colspan="2" bgcolor="#BBBBBB">
                                                                        <asp:Button ID="btnModCausasConsecuencias" runat="server" Text="Modificar" CssClass="Apariencia"
                                                                            OnClick="btnModCausasConsecuencias_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td colspan="2" bgcolor="#BBBBBB">
                                                                        <table>
                                                                            <tr align="left">
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label165" runat="server" Text="Causas" Font-Names="Calibri" Font-Size="Small"
                                                                                        Width="70px"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:Panel ID="checkBoxPanel1" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList"
                                                                                        Enabled="False">
                                                                                        <asp:TextBox ID="tbxCausas" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                            Width="250px"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="tbxCausas_AutoCompleteExtender" runat="server" BehaviorID="tbxCausas_content_AutoCompleteExtender"
                                                                                            DelimiterCharacters="" TargetControlID="tbxCausas" ServicePath="~/Formularios/WebService/AutoCompletar.asmx"
                                                                                            ServiceMethod="GetCausas" EnableCaching="False" CompletionSetCount="20" MinimumPrefixLength="2"
                                                                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                        <asp:Button ID="btnAsignarCausa" runat="server" Text="Asignar ..." OnClick="btnAsignarCausa_Click" />
                                                                                        <br />
                                                                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                            Width="300px">
                                                                                        </asp:CheckBoxList>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label166" runat="server" Text="Consecuencias" Font-Names="Calibri"
                                                                                        Font-Size="Small" Width="90px"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE">
                                                                                    <asp:Panel ID="checkBoxPanel2" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList"
                                                                                        Enabled="False" Width="350px">
                                                                                        <asp:TextBox ID="tbxConsecuencias" runat="server" Width="250px" Font-Names="Calibri"
                                                                                            Font-Size="Small"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="tbxConsecuencias_AutoCompleteExtender" runat="server"
                                                                                            BehaviorID="tbxConsecuencias_content_AutoCompleteExtender" DelimiterCharacters=""
                                                                                            TargetControlID="tbxConsecuencias" ServicePath="~/Formularios/WebService/AutoCompletar.asmx"
                                                                                            ServiceMethod="GetConsecuencias" EnableCaching="False" CompletionSetCount="20"
                                                                                            MinimumPrefixLength="2" CompletionListCssClass="autocomplete_completionListElement"
                                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                                            ShowOnlyCurrentWordInCompletionListItem="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                        <asp:Button ID="btnAsignarConsecuencia" runat="server" Text="Asignar ..." OnClick="btnAsignarConsecuencia_Click" />
                                                                                        <br />
                                                                                        <asp:CheckBoxList ID="CheckBoxList4" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                                        </asp:CheckBoxList>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr align="left">
                                                                                <td bgcolor="#BBBBBB">
                                                                                    <asp:Label ID="Label6" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                                                                </td>
                                                                                <td bgcolor="#EEEEEE" colspan="3">
                                                                                    <asp:TextBox ID="TextBox21" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                        Width="400px" Enabled="False"></asp:TextBox>
                                                                                    <asp:Label ID="lblIdDependencia3" runat="server" Visible="False"></asp:Label>
                                                                                    <asp:ImageButton ID="imgDependencia3" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                                                        OnClientClick="return false;" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox21"
                                                                                        ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                                    <asp:PopupControlExtender ID="popupDependencia3" runat="server" DynamicServicePath=""
                                                                                        ExtenderControlID="" TargetControlID="imgDependencia3" BehaviorID="popup3" PopupControlID="pnlDependencia3"
                                                                                        OffsetY="-200">
                                                                                    </asp:PopupControlExtender>
                                                                                    <asp:Panel ID="pnlDependencia3" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                                                        <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                                            <tr align="right" bgcolor="#5D7B9D">
                                                                                                <td>
                                                                                                    <asp:Label ID="dd" runat="server" Text="Seleccione un responsable" Font-Names="Calibri"
                                                                                                        Font-Size="Small"></asp:Label>
                                                                                                    <asp:ImageButton ID="btnClosepp3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                                                        OnClientClick="$find('popup3').hidePopup(); return false;" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:TreeView ID="TreeView3" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                                                        Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                                                        AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView3_SelectedNodeChanged">
                                                                                                        <SelectedNodeStyle BackColor="#E8E9EA" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                                                    </asp:TreeView>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr align="center">
                                                                                                <td>
                                                                                                    <asp:Button ID="BtnOk3" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup3').hidePopup(); return false;" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" TargetControlID="Panel444"
                                                            CollapsedSize="0" ExpandControlID="Image4" CollapseControlID="Image4" ImageControlID="Image4"
                                                            ExpandedImage="~/Imagenes/Icons/expand.jpg" CollapsedImage="~/Imagenes/Icons/collapse.jpg"
                                                            BehaviorID="_content_CollapsiblePanelExtender4"></asp:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Image ID="Image9" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label83" runat="server" Text="Tratamiento" ForeColor="#000D26" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel9" runat="server">
                                                            <table>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label84" runat="server" Text="Tratamiento" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:Panel ID="Panel10" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                                            <asp:CheckBoxList ID="CheckBoxList10" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                Width="150px">
                                                                            </asp:CheckBoxList>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" runat="server" TargetControlID="Panel9"
                                                            CollapsedSize="0" ExpandControlID="Image9" CollapseControlID="Image9" ImageControlID="Image9"
                                                            ExpandedImage="~/Imagenes/Icons/expand.jpg" CollapsedImage="~/Imagenes/Icons/collapse.jpg"
                                                            BehaviorID="_content_CollapsiblePanelExtender7"></asp:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Image ID="Image5" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label149" runat="server" Text="Medición" ForeColor="#000D26" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel555" runat="server">
                                                            <table>
                                                                <tr align="center">
                                                                    <td bgcolor="#BBBBBB" colspan="5">
                                                                        <asp:Label ID="Label169" runat="server" Text="Medición" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label170" runat="server" Text="Frecuencia-Cualitativa" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList66" runat="server" Width="105px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList66_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="DropDownList66"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label171" runat="server" Text="Frecuencia-Cuantitativa" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE" colspan="2">
                                                                        <asp:Label ID="Label172" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label173" runat="server" Text="Se esperaba la ocurrencia de un evento entre un"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:TextBox ID="TextBox5" runat="server" MaxLength="3" Width="100px" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="TextBox5"
                                                                            ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td bgcolor="#BBBBBB" align="center">
                                                                        <asp:Label ID="Label178" runat="server" Text="% y un " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:TextBox ID="TextBox6" runat="server" MaxLength="3" Width="100px" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="TextBox6"
                                                                            ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ForeColor="Red" ControlToValidate="TextBox5"
                                                                            ControlToCompare="TextBox6" ValidationGroup="modificarRiesgo" Operator="LessThanEqual"
                                                                            Display="Dynamic" Font-Names="Calibri" Font-Size="Small" Type="Integer">Rango invalido</asp:CompareValidator>
                                                                    </td>
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label179" runat="server" Text="% de los casos" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trImgFrecuenciaUps" runat="server">
                                                    <td colspan="3" align="center" runat="server">
                    <asp:Label runat="server" ID="Label92" Text="Para visualizar la imagen de frecuencia"></asp:Label>
                </td>
                <td colspan="2" runat="server">
                    <asp:ImageButton ID="ImbViewJPGfrecuenciaIns" runat="server" ImageUrl="~/Imagenes/Icons/jpg.png"
                                                                            ToolTip="Ver Imagen" Width="50px" Height="50px" OnClick="ImbViewJPGfrecuenciaIns_Click" ></asp:ImageButton>
                    
                </td>
                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label180" runat="server" Text="Impacto cualitativo" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:DropDownList ID="DropDownList68" runat="server" Width="105px" Font-Names="Calibri"
                                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList68_SelectedIndexChanged">
                                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="DropDownList68"
                                                                            InitialValue="---" ForeColor="Red" ValidationGroup="modificarRiesgo">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label181" runat="server" Text="Impacto cuantitativo" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE" colspan="2">
                                                                        <asp:Label ID="Label193" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr align="left">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label194" runat="server" Text="Pérdida económica entre" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE">
                                                                        <asp:TextBox ID="TextBox7" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                                                            MaxLength="20"></asp:TextBox>
                                                                    </td>
                                                                    <td bgcolor="#BBBBBB" align="center">
                                                                        <asp:Label ID="Label195" runat="server" Text="y" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td bgcolor="#EEEEEE" colspan="2">
                                                                        <asp:TextBox ID="TextBox10" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                                                            MaxLength="20"></asp:TextBox>
                                                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ForeColor="Red" ControlToValidate="TextBox7"
                                                                            ControlToCompare="TextBox10" ValidationGroup="modificarRiesgo" Operator="LessThanEqual"
                                                                            Display="Dynamic" Font-Names="Calibri" Font-Size="Small" Type="Integer">Rango invalido</asp:CompareValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trImgImpactoUps" runat="server">
                                                    <td colspan="3" align="center" runat="server">
                    <asp:Label runat="server" ID="Label93" Text="Para visualizar la imagen de Impacto"></asp:Label>
                </td>
                <td colspan="2" runat="server">
                    <asp:ImageButton ID="ImbViewJPGimpactoIns" runat="server" ImageUrl="~/Imagenes/Icons/jpg.png"
                                                                            ToolTip="Ver Imagen" Width="50px" Height="50px" OnClick="ImbViewJPGimpactoIns_Click" ></asp:ImageButton>
                    
                </td>
                                                </tr>
                                                                <tr align="center">
                                                                    <td bgcolor="#BBBBBB" colspan="5">
                                                                        <asp:Label ID="Label196" runat="server" Text="Riesgo inherente" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center" bgcolor="#EEEEEE">
                                                                    <td colspan="5">
                                                                        <asp:Panel ID="Panel4" runat="server" Width="80px" Height="50px">
                                                                            <table style="width: 100%; height: 100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label197" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server" TargetControlID="Panel555"
                                                            CollapsedSize="0" Collapsed="True" ExpandControlID="Image5" CollapseControlID="Image5"
                                                            ImageControlID="Image5" ExpandedImage="~/Imagenes/Icons/expand.jpg" CollapsedImage="~/Imagenes/Icons/collapse.jpg"
                                                            BehaviorID="_content_CollapsiblePanelExtender5"></asp:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Image ID="Image8" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label65" runat="server" Text="Justificación de los cambios" ForeColor="#000D26"
                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel777" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label67" runat="server" Text="Justificación:" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox16" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="TextBox16"
                                                                            ValidationGroup="modificarRiesgo" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr align="right">
                                                                    <td colspan="2">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                        ToolTip="Cancelar" Visible="False" OnClick="ImageButton17_Click"></asp:ImageButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td colspan="2">
                                                                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                            HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView6_RowCommand">
                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                            <Columns>
                                                                                <asp:BoundField DataField="IdComentario" HeaderText="IdComentario" Visible="False"></asp:BoundField>
                                                                                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario"></asp:BoundField>
                                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro"></asp:BoundField>
                                                                                <asp:BoundField DataField="ComentarioCorto" HeaderText="Justificación"></asp:BoundField>
                                                                                <asp:BoundField DataField="Comentario" HeaderText="Comentario" Visible="False"></asp:BoundField>
                                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver comentario"
                                                                                    CommandName="Ver"></asp:ButtonField>
                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#999999" />
                                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server" TargetControlID="Panel777"
                                                            CollapsedSize="0" ExpandControlID="Image8" CollapseControlID="Image8" ImageControlID="Image8"
                                                            ExpandedImage="~/Imagenes/Icons/expand.jpg" CollapsedImage="~/Imagenes/Icons/collapse.jpg"
                                                            BehaviorID="_content_CollapsiblePanelExtender6"></asp:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Image ID="Image6" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label63" runat="server" Text="Archivos adjuntos" ForeColor="#000D26"
                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel666" runat="server">
                                                            <table>
                                                                <tr align="center">
                                                                    <td bgcolor="#BBBBBB">
                                                                        <asp:Label ID="Label66" runat="server" Text="Adjuntar documento :" Font-Names="Calibri"
                                                                            Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:FileUpload>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                            ToolTip="Adjuntar" OnClick="ImageButton16_Click"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td colspan="3">
                                                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                            HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView4_RowCommand">
                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                            <Columns>
                                                                                <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="False"></asp:BoundField>
                                                                                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario"></asp:BoundField>
                                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro"></asp:BoundField>
                                                                                <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo"></asp:BoundField>
                                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                                                                    CommandName="Descargar"></asp:ButtonField>
                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#999999" />
                                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="Panel666"
                                                            CollapsedSize="0" ExpandControlID="Image6" CollapseControlID="Image6" ImageControlID="Image6"
                                                            ExpandedImage="~/Imagenes/Icons/expand.jpg" CollapsedImage="~/Imagenes/Icons/collapse.jpg"
                                                            BehaviorID="_content_CollapsiblePanelExtender3"></asp:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" ValidationGroup="modificarRiesgo" OnClick="ImageButton9_Click"></asp:ImageButton>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="ResetValues_ModificarRiesgo"></asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Control" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <HeaderTemplate>
                                            Control
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <table>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label36" runat="server" Text="Nombre del Riesgo:" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:Label ID="Label37" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB" colspan="2">
                                                                    <asp:Label ID="Label77" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Consultar Controles"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label75" runat="server" Text="Código:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:TextBox ID="TextBox18" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label76" runat="server" Text="Nombre:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:TextBox ID="TextBox19" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label81" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:TextBox ID="TextBox23" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="300px" Enabled="False"></asp:TextBox>
                                                                    <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                    <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                                        OnClientClick="return false;" />
                                                                    <asp:PopupControlExtender ID="popupDependencia4" runat="server" DynamicServicePath="" ExtenderControlID="" TargetControlID="imgDependencia4" BehaviorID="popup4"
                                                                        PopupControlID="pnlDependencia4" OffsetY="-200">
                                                                    </asp:PopupControlExtender>
                                                                    <asp:Panel ID="pnlDependencia4" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                                        <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                            <tr align="right" bgcolor="#5D7B9D">
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnClosepp4" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                                        OnClientClick="$find('popup4').hidePopup(); return false;" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TreeView ID="TreeView4" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                                        Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                                        AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView4_SelectedNodeChanged">
                                                                                        <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                                    </asp:TreeView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr align="center">
                                                                                <td>
                                                                                    <asp:Button ID="BtnOk4" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup4').hidePopup(); return false;" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton25" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                                                                    OnClick="ImageButton25_Click" ToolTip="Consultar"></asp:ImageButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    ToolTip="Cancelar" OnClick="ImageButton5_Click"></asp:ImageButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowCommand="GridView8_RowCommand">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="CodigoControl" HeaderText="Código"></asp:BoundField>
                                                                            <asp:BoundField DataField="NombreControl" HeaderText="Nombre"></asp:BoundField>
                                                                            <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnImgRelacionar" runat="server" CausesValidation="False" CommandName="Relacionar"
                                                                                        ImageUrl="~/Imagenes/Icons/select.png" Text="Relacionar" ToolTip="Relacionar"
                                                                                        CommandArgument="<%# Container.DataItemIndex %>" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB" colspan="2">
                                                                    <asp:Label ID="Label40" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Controles Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td colspan="2">
                                                                    <asp:Button ID="Button6" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="Button6_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        DataKeyNames="IdControl"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" OnRowCommand="GridView2_RowCommand" ShowHeaderWhenEmpty="True"
                                                                        OnRowDataBound="GridView2_RowDataBound">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="CodigoControl" HeaderText="Código"></asp:BoundField>
                                                                            <asp:BoundField DataField="NombreControl" HeaderText="Nombre"></asp:BoundField>
                                                                            <asp:ButtonField ButtonType="Image" CommandName="Borrar" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                Text="Eliminar"></asp:ButtonField>
                                                                            <asp:TemplateField HeaderText="Calificación Control">
                                                                                <ItemTemplate>
                                                                                    <asp:Panel ID="Panel1" runat="server" Width="80px" Height="50px">
                                                                                        <table style="width: 100%; height: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label14" runat="server" Font-Names="Calibri" Font-Size="Small" Text='<%# Bind("NombreEscala") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ShowHeader="False" HeaderText="Asociar Causas">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnImgRelacionar" runat="server" CausesValidation="False" CommandName="Asociar"
                                                                                        ImageUrl="~/Imagenes/Icons/select.png" Text="Asociar" ToolTip="Asociar"
                                                                                        CommandArgument="<%# Container.DataItemIndex %>" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                    <asp:HiddenField ID="hidForModel" runat="server" />
                                <asp:ModalPopupExtender ID="modalPopup" runat="server" PopupControlID="popupCausas" TargetControlID="hidForModel" BackgroundCssClass="modalBackground" DropShadow="True" BehaviorID="_content_modalPopup" DynamicServicePath="">
                                            </asp:ModalPopupExtender>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2"></td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2"></td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB" colspan="2">
                                                                    <asp:Label ID="Label87" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Asociaciones Eliminadas"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="False"></asp:BoundField>
                                                                            <asp:BoundField DataField="IdRiesgo" HeaderText="IdRiesgo" Visible="False"></asp:BoundField>
                                                                            <asp:BoundField DataField="CodigoRiesgo" HeaderText="Riesgo"></asp:BoundField>
                                                                            <asp:BoundField DataField="IdControl" HeaderText="IdControl" Visible="False"></asp:BoundField>
                                                                            <asp:BoundField DataField="CodigoControl" HeaderText="Control"></asp:BoundField>
                                                                            <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" Visible="False"></asp:BoundField>
                                                                            <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario"></asp:BoundField>
                                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha"></asp:BoundField>
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Calificación" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <ContentTemplate>
                                            <table>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label32" runat="server" Text="Nombre del Riesgo:" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:Label ID="Label33" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td bgcolor="#BBBBBB" colspan="2">
                                                        <asp:Label ID="Label27" runat="server" Text="Calificación Riesgo Inherente" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#EEEEEE">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel5" runat="server" Width="80px" Height="50px">
                                                            <table style="width: 100%; height: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label28" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td bgcolor="#BBBBBB" colspan="2">
                                                        <asp:Label ID="Label34" runat="server" Text="Calificación Controles" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#EEEEEE">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel6" runat="server" Width="80px" Height="50px">
                                                            <table style="width: 100%; height: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label35" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td bgcolor="#BBBBBB" colspan="2">
                                                        <asp:Label ID="Label38" runat="server" Text="Calificación Riesgo Residual" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#EEEEEE">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel7" runat="server" Width="80px" Height="50px">
                                                            <table style="width: 100%; height: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label39" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Objetivos" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <HeaderTemplate>
                                            Objetivos
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <table>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label55" runat="server" Text="Nombre del Riesgo:" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:Label ID="Label56" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label85" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Plan Estratégico:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="DropDownList5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                                                        <asp:ListItem Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="DropDownList5"
                                                                        ForeColor="Red" InitialValue="---" ValidationGroup="relacionarObjetivos">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label185" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivo Estratégico"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="DropDownList61" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px">
                                                                        <asp:ListItem Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="DropDownList61"
                                                                        ForeColor="Red" InitialValue="---" ValidationGroup="relacionarObjetivos">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    ToolTip="Guardar" ValidationGroup="relacionarObjetivos" OnClick="ImageButton11_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    OnClick="ResetValues_ModificarRiesgo" ToolTip="Cancelar" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB" colspan="2">
                                                                    <asp:Label ID="Label198" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivos Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowCommand="GridView7_RowCommand">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Nombre del Objetivo" DataField="NombreObjetivos" />
                                                                            <asp:ButtonField ButtonType="Image" CommandName="Borrar" ImageUrl="~/Imagenes/Icons/delete.png"
                                                                                Text="Eliminar" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Plan de Acción" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <table>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label50" runat="server" Text="Nombre del Riesgo:" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:Label ID="Label119" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB" colspan="2">
                                                                    <asp:Label ID="Label121" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Planes de Acción Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                                    CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                                    HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowCommand="GridView3_RowCommand">
                                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Estado" DataField="NombreEstadoPlanAccion"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Fecha Compromiso" DataField="FechaCompromiso"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Descripción" DataField="DescripcionAccion"></asp:BoundField>
                                                                                        <asp:BoundField HeaderText="Responsable" DataField="NombreHijo"></asp:BoundField>
                                                                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar"
                                                                                            CommandName="Modificar"></asp:ButtonField>
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#999999" />
                                                                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                                    <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Insertar" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                                    OnClick="ImageButton2_Click"></asp:ImageButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="trAddPlanAccion" runat="server" visible="False">
                                                                <td colspan="2" runat="server">
                                                                    <table>
                                                                        <tr align="left">
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label49" runat="server" Text="Descripción de la acción:" Font-Names="Calibri"
                                                                                                Font-Size="Small"></asp:Label>
                                                                                        </td>
                                                                                        <td bgcolor="#EEEEEE">
                                                                                            <asp:TextBox ID="TextBox12" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                                Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="TextBox12"
                                                                                                ForeColor="Red" ValidationGroup="validatePlanAccion">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label57" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                                                                        </td>
                                                                                        <td bgcolor="#EEEEEE">
                                                                                            <asp:TextBox ID="TextBox13" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                                Width="400px" Enabled="False"></asp:TextBox>
                                                                                            <asp:Label ID="lblIdDependencia1" runat="server" Visible="False"></asp:Label>
                                                                                            <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                                                                OnClientClick="return false;" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="TextBox13"
                                                                                                ForeColor="Red" ValidationGroup="validatePlanAccion">*</asp:RequiredFieldValidator>
                                                                                            <asp:PopupControlExtender ID="popupDependencia1" runat="server" DynamicServicePath="" ExtenderControlID="" TargetControlID="imgDependencia1" BehaviorID="popup1"
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
                                                                                    <tr>
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label58" runat="server" Text="Tipo recurso:" Font-Names="Calibri"
                                                                                                Font-Size="Small"></asp:Label>
                                                                                        </td>
                                                                                        <td bgcolor="#EEEEEE">
                                                                                            <asp:DropDownList ID="DropDownList17" runat="server" Width="155px" Font-Names="Calibri"
                                                                                                Font-Size="Small">
                                                                                                <asp:ListItem Value="---">---</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="DropDownList17"
                                                                                                InitialValue="---" ForeColor="Red" ValidationGroup="validatePlanAccion">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label59" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor recurso:"></asp:Label>
                                                                                        </td>
                                                                                        <td bgcolor="#EEEEEE">
                                                                                            <asp:TextBox ID="TextBox14" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                                Width="150px" MaxLength="15"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label61" runat="server" Text="Estado:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                                        </td>
                                                                                        <td bgcolor="#EEEEEE">
                                                                                            <asp:DropDownList ID="DropDownList18" runat="server" Width="155px" Font-Names="Calibri"
                                                                                                Font-Size="Small">
                                                                                                <asp:ListItem Value="---">---</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="DropDownList18"
                                                                                                InitialValue="---" ForeColor="Red" ValidationGroup="validatePlanAccion">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label60" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha compromiso:"></asp:Label>
                                                                                        </td>
                                                                                        <td bgcolor="#EEEEEE">
                                                                                            <asp:TextBox ID="TextBox15" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                                                Width="150px"></asp:TextBox>
                                                                                            <asp:CalendarExtender ID="TextBox15_CalendarExtender" runat="server"
                                                                                                TargetControlID="TextBox15" Format="yyyy-MM-dd" BehaviorID="_content_TextBox15_CalendarExtender"></asp:CalendarExtender>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="TextBox15"
                                                                                                ForeColor="Red" ValidationGroup="validatePlanAccion">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center" runat="server" visible="False" id="trAdjComPlaAcci">
                                                                            <td runat="server">
                                                                                <asp:TabContainer ID="TabContainer3" runat="server" ActiveTabIndex="1" Font-Names="Calibri"
                                                                                    Font-Size="Small" Width="600px">
                                                                                    <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="Justificación cambios" Font-Names="Calibri"
                                                                                        Font-Size="Small">
                                                                                        <ContentTemplate>
                                                                                            <table>
                                                                                                <tr align="center">
                                                                                                    <td bgcolor="#BBBBBB">
                                                                                                        <asp:Label ID="Label78" runat="server" Text="Justificación:" Font-Names="Calibri"
                                                                                                            Font-Size="Small"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="TextBox22" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                                                                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox22"
                                                                                                            ValidationGroup="validatePlanAccion" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr align="right">
                                                                                                    <td colspan="2">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                                                        ToolTip="Cancelar" Visible="False" OnClick="ImageButton7_Click"></asp:ImageButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr align="center">
                                                                                                    <td colspan="2">
                                                                                                        <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                                                            HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView9_RowCommand">
                                                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="IdComentario" HeaderText="IdComentario" Visible="False"></asp:BoundField>
                                                                                                                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario"></asp:BoundField>
                                                                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro"></asp:BoundField>
                                                                                                                <asp:BoundField DataField="ComentarioCorto" HeaderText="Justificación"></asp:BoundField>
                                                                                                                <asp:BoundField DataField="Comentario" HeaderText="Comentario" Visible="False"></asp:BoundField>
                                                                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver comentario"
                                                                                                                    CommandName="Ver"></asp:ButtonField>
                                                                                                            </Columns>
                                                                                                            <EditRowStyle BackColor="#999999" />
                                                                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                                                                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </ContentTemplate>
                                                                                    </asp:TabPanel>
                                                                                    <asp:TabPanel ID="TabPanel10" runat="server" HeaderText="Documentos adjuntos" Font-Names="Calibri"
                                                                                        Font-Size="Small">
                                                                                        <ContentTemplate>
                                                                                            <table>
                                                                                                <tr align="center">
                                                                                                    <td bgcolor="#BBBBBB">
                                                                                                        <asp:Label ID="Label79" runat="server" Text="Adjuntar documento .pdf:" Font-Names="Calibri"
                                                                                                            Font-Size="Small"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload ID="FileUpload2" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:FileUpload>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                                                            ToolTip="Adjuntar" OnClick="ImageButton15_Click1"></asp:ImageButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr align="center">
                                                                                                    <td colspan="3">
                                                                                                        <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                                                            HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView10_RowCommand">
                                                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="False"></asp:BoundField>
                                                                                                                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario"></asp:BoundField>
                                                                                                                <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro"></asp:BoundField>
                                                                                                                <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo"></asp:BoundField>
                                                                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                                                                                                    CommandName="Descargar"></asp:ButtonField>
                                                                                                            </Columns>
                                                                                                            <EditRowStyle BackColor="#999999" />
                                                                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                                                                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </ContentTemplate>
                                                                                    </asp:TabPanel>
                                                                                </asp:TabContainer>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                                ToolTip="Guardar" ValidationGroup="validatePlanAccion" Visible="False" OnClick="ImageButton13_Click"></asp:ImageButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                                ToolTip="Guardar" ValidationGroup="validatePlanAccion" Visible="False" OnClick="ImageButton3_Click"></asp:ImageButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                                ToolTip="Cancelar" OnClick="ImageButton14_Click"></asp:ImageButton>
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
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Eventos" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <HeaderTemplate>
                                            Eventos
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <table>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label52" runat="server" Text="Nombre del Riesgo:" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:Label ID="Label62" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB" colspan="2">
                                                                    <asp:Label ID="Label64" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Eventos Relacionados"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td colspan="2">
                                                                    <asp:Button ID="Button1" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                                                        Font-Size="Small" OnClick="Button1_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                                        CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                                        HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="CodigoEvento" HeaderText="Código" />
                                                                            <asp:BoundField DataField="DescripcionEvento" HeaderText="Descripcion" />
                                                                            <asp:BoundField DataField="FechaDescubrimiento" HeaderText="Fecha Descubrimiento"
                                                                                Visible="False" />
                                                                            <asp:BoundField DataField="ValorRecuperadoTotal" HeaderText="Valor Recuperado" Visible="False" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White" />
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
                    <table id="tbAgregarRiesgo" runat="server" visible="false">
                        <tr>
                            <td>
                                <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="1" Font-Names="Calibri"
                                    Font-Size="Small" Width="900px">
                                    <asp:TabPanel ID="TabPanel8" runat="server" HeaderText="Ubicación" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <HeaderTemplate>
                                            Ubicación
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table bgcolor="#EEEEEE">
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label141" runat="server" Text="Región" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList41" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList41_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList41"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label142" runat="server" Text="Pais" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList42" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList42_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList42"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label143" runat="server" Text="Departamento/Región" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList43" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList43_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList43"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label144" runat="server" Text="Ciudad" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList44" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList44_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList44"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label9" runat="server" Text="Oficina/Sucursal" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList63" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList63"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="Cadena de valor">
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr align="center">
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label191" runat="server" Text="Mapa de procesos" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                            <td bgcolor="#EEEEEE">
                                                                                <asp:ImageButton ID="ImageButton26" ToolTip="Ver" ImageUrl="~/Imagenes/Icons/mapaweb.png"
                                                                                    runat="server" />
                                                                                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="ImageButton26"
                                                                                    PopupControlID="Panel2" OkControlID="ImageButton27" BackgroundCssClass="FondoAplicacion"
                                                                                    Enabled="True" DropShadow="True" DynamicServicePath="">
                                                                                </asp:ModalPopupExtender>
                                                                                <asp:Panel ID="Panel2" runat="server" Width="750px" Style="display: none;" BorderColor="#575757"
                                                                                    BackColor="#FFFFFF" BorderStyle="Solid">
                                                                                    <table width="100%">
                                                                                        <tr class="topHandle" style="background-color: #5D7B9D">
                                                                                            <td align="center">
                                                                                                <asp:Label ID="Label68" runat="server" Text="Mapa de procesos" Font-Names="Calibri"
                                                                                                    Font-Size="Small"></asp:Label><br />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr align="center">
                                                                                            <td>
                                                                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Imagenes/Aplicacion/MapaProceso.png" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr align="center">
                                                                                            <td>
                                                                                                <asp:ImageButton ID="ImageButton27" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                                    ToolTip="Cancelar" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label188" runat="server" Text="Macroproceso estrategicos" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label189" runat="server" Text="Macroproceso core" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label190" runat="server" Text="Macroproceso de soporte" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ListBox ID="ListBox1" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ListBox ID="ListBox2" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ListBox ID="ListBox3" runat="server" Width="200px" Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label192" runat="server" Text="Cadena de valor" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList67" runat="server" Width="400px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList67_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="DropDownList67"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label18" runat="server" Text="Macroproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList9" runat="server" Width="400px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="DropDownList9"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label19" runat="server" Text="Proceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList10" runat="server" Width="400px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList10_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="DropDownList10"
                                                    InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label43" runat="server" Text="Subproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList6" runat="server" Width="400px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="DropDownList6"
                                                    InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label20" runat="server" Text="Actividad" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList11" runat="server" Width="400px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="DropDownList11"
                                                    InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel9" runat="server" HeaderText="Información del riesgos">
                                        <HeaderTemplate>
                                            Información del riesgos
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label2" runat="server" Text="Riesgos globales" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="455px" Font-Names="Calibri"
                                                            Font-Size="Small" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList1"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label3" runat="server" Text="Clasificación general" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList2" runat="server" Width="455px" AutoPostBack="True"
                                                            Font-Names="Calibri" Font-Size="Small" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label4" runat="server" Text="Clasificación particular" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList3" runat="server" Width="455px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="trRiesgoOperativo1" runat="server" visible="False" align="left">
                                                    <td colspan="2" runat="server" bgcolor="#BBBBBB">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label175" runat="server" Text="Factor de riesgo operativo" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="DropDownList8" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="445px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="DropDownList8"
                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label176" runat="server" Text="Sub factor riesgo operativo" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="DropDownList14" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="445px">
                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="DropDownList14"
                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="trRiesgoOperativo2" runat="server" visible="False" align="left">
                                                    <td colspan="2" runat="server" bgcolor="#BBBBBB">
                                                        <table>
                                                            <tr id="trRO2" runat="server">
                                                                <td runat="server">
                                                                    <asp:Label ID="Label41" runat="server" Text="Tipo de evento" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE" runat="server">
                                                                    <asp:DropDownList ID="DropDownList12" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px">
                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="DropDownList12"
                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label42" runat="server" Text="Riesgo asociado" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:DropDownList ID="DropDownList13" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px">
                                                                        <asp:ListItem Value="0">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="DropDownList13"
                                                                        InitialValue="0" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="trLavadoActivos1" runat="server" visible="False" align="left">
                                                    <td colspan="2" runat="server" bgcolor="#BBBBBB">
                                                        <table>
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label45" runat="server" Text="Riesgo asociado" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:CheckBoxList ID="CheckBoxList5" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label46" runat="server" Text="Factor de riesgo LA/FT" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:CheckBoxList ID="CheckBoxList6" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label1" runat="server" Text="Código" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="TextBox8" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label187" runat="server" Text="Nombre" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="TextBox9" runat="server" Width="450px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox9"
                                                            ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label7" runat="server" Text="Descripción del riesgo" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="TextBox1" runat="server" Height="50px" TextMode="MultiLine" Width="450px"
                                                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TextBox1"
                                                            ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td colspan="2" bgcolor="#BBBBBB">
                                                        <table>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label8" runat="server" Text="Causas" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:Panel ID="checkBoxPanel3" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                        </asp:CheckBoxList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label10" runat="server" Text="Consecuencias" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:Panel ID="checkBoxPanel4" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                                        </asp:CheckBoxList>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE" colspan="3">
                                                                    <asp:TextBox ID="TextBox20" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px" Enabled="False"></asp:TextBox>
                                                                    <asp:Label ID="lblIdDependencia2" runat="server" Visible="False"></asp:Label>
                                                                    <asp:ImageButton ID="imgDependencia2" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                                        OnClientClick="return false;" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                                                        ControlToValidate="TextBox20" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                                    <asp:PopupControlExtender ID="popupDependencia2" runat="server" DynamicServicePath="" ExtenderControlID="" TargetControlID="imgDependencia2" BehaviorID="popup2"
                                                                        PopupControlID="pnlDependencia2" OffsetY="-200">
                                                                    </asp:PopupControlExtender>
                                                                    <asp:Panel ID="pnlDependencia2" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                                        <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                            <tr align="right" bgcolor="#5D7B9D">
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnClosepp2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                                        OnClientClick="$find('popup2').hidePopup(); return false;" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TreeView ID="TreeView2" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                                        Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                                        AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                                                                        <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                                    </asp:TreeView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr align="center">
                                                                                <td>
                                                                                    <asp:Button ID="BtnOk2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup2').hidePopup(); return false;" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel11" runat="server" HeaderText="Tratamiento">
                                        <HeaderTemplate>
                                            Tratamiento
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label82" runat="server" Text="Tratamiento" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:Panel ID="Panel8" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                                            <asp:CheckBoxList ID="CheckBoxList9" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                Width="150px">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel14" runat="server" HeaderText="Medición">
                                        <HeaderTemplate>
                                            Medición
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td bgcolor="#BBBBBB" colspan="5">
                                                        <asp:Label ID="Label17" runat="server" Text="Medición" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label29" runat="server" Text="Frecuencia-Cualitativa" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList45" runat="server" Width="105px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList45_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="DropDownList45"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label12" runat="server" Text="Frecuencia-Cuantitativa" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE" colspan="2">
                                                        <asp:Label ID="Label13" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label31" runat="server" Text="Se esperaba la ocurrencia de un evento entre un"
                                                            Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="TextBox40" runat="server" MaxLength="3" Width="100px" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="TextBox40"
                                                            ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" align="center">
                                                        <asp:Label ID="Label145" runat="server" Text="% y un " Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="TextBox41" runat="server" MaxLength="3" Width="100px" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="TextBox41"
                                                            ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ControlToValidate="TextBox40"
                                                            ControlToCompare="TextBox41" ValidationGroup="agregarRiesgo" Operator="LessThanEqual"
                                                            Display="Dynamic" Font-Names="Calibri" Font-Size="Small" Type="Integer">Rango invalido</asp:CompareValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label146" runat="server" Text="% de los casos" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="trImgFrecuenciaIns" runat="server">
                                                    <td colspan="3" align="center" runat="server">
                    <asp:Label runat="server" ID="lbhlTextImg" Text="Para visualizar la imagen de frecuencia"></asp:Label>
                </td>
                <td colspan="2" runat="server">
                    <asp:ImageButton ID="ImbViewJPGfrecuencia" runat="server" ImageUrl="~/Imagenes/Icons/jpg.png"
                                                                            ToolTip="Ver Imagen" Width="50px" Height="50px" OnClick="ImbViewJPGfrecuencia_Click"></asp:ImageButton>
                    
                </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label30" runat="server" Text="Impacto cualitativo" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:DropDownList ID="DropDownList46" runat="server" Width="105px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList46_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="DropDownList46"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="agregarRiesgo">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label14" runat="server" Text="Impacto cuantitativo" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE" colspan="2">
                                                        <asp:Label ID="Label177" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label147" runat="server" Text="Pérdida económica entre" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="TextBox42" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" align="center">
                                                        <asp:Label ID="Label148" runat="server" Text="y" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE" colspan="2">
                                                        <asp:TextBox ID="TextBox43" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ForeColor="Red" ControlToValidate="TextBox42"
                                                            ControlToCompare="TextBox43" ValidationGroup="agregarRiesgo" Operator="LessThanEqual"
                                                            Display="Dynamic" Font-Names="Calibri" Font-Size="Small" Type="Integer">Rango invalido</asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trImgImpactoIns" runat="server">
                                                    <td colspan="3" align="center" runat="server">
                    <asp:Label runat="server" ID="Label91" Text="Para visualizar la imagen de Impacto"></asp:Label>
                </td>
                <td colspan="2" runat="server">
                    <asp:ImageButton ID="ImbViewJPGimpacto" runat="server" ImageUrl="~/Imagenes/Icons/jpg.png"
                                                                            ToolTip="Ver Imagen" Width="50px" Height="50px" OnClick="ImbViewJPGimpacto_Click"></asp:ImageButton>
                    
                </td>
                                                </tr>
                                                <tr align="center">
                                                    <td bgcolor="#BBBBBB" colspan="5">
                                                        <asp:Label ID="Label21" runat="server" Text="Riesgo inherente" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#EEEEEE">
                                                    <td colspan="5">
                                                        <asp:Panel ID="Panel1" runat="server" Width="80px" Height="50px">
                                                            <table style="width: 100%; height: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label174" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" ValidationGroup="agregarRiesgo" OnClick="ImageButton4_Click" />
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
        <asp:ModalPopupExtender ID="mpeMsgBox1" runat="server" TargetControlID="btndummy1"
            PopupControlID="pnlMsgBox1" OkControlID="btnAceptar1" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy1" runat="server" Text="Button1" Style="display: none" />
        <asp:Panel ID="pnlMsgBox1" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="td1">&nbsp;
                        <asp:Label ID="Label86" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo1" runat="server" ImageUrl="~/Imagenes/Icons/Alerta.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar1" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1" />
        <asp:PostBackTrigger ControlID="TabContainer2" />
    </Triggers>

</asp:UpdatePanel>

