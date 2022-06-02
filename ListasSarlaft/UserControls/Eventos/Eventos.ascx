<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Eventos.ascx.cs" Inherits="ListasSarlaft.UserControls.Eventos.Eventos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
<script src="../../Scripts/DownloadJquery.js"></script>

<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../../Imagenes/Icons/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../../Imagenes/Icons/plus.png");
        $(this).closest("tr").next().remove();
    });
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
    .auto-style1 {
        font-family: Calibri;
        font-size: small;
    }
</style>
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
<uc:OkMessageBox ID="omb" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                >
                                <ProgressTemplate>
                                    <div id="Background">
                                    </div>
                                    <div id="Progress" align="center">
                                        <asp:Label ID="Lbl11" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                            Font-Size="Small"></asp:Label>
                                        <br />
                                        <asp:Image ID="Img11" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
        <asp:Panel ID="popupCausas" runat="server" CssClass="popup" Width="800px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr>
                    <td align="center">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="0">
                                <ProgressTemplate>
                                    <div id="Background">
                                    </div>
                                    <div id="Progress">
                                        <asp:Label ID="Lbl11Pro" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                            Font-Size="Small"></asp:Label>
                                        <br />
                                        <asp:Image ID="Img11Pro" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                    </td>
                </tr>
                <tr align="right" bgcolor="#5D7B9D">
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close3.png"
                            OnClientClick="$find('popupActividad2').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:GridView ID="GVcausasRiesgos" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                     ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
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
                        <asp:Label ID="LtextoCausas" runat="server" Text="Seleccionar Asociar Riesgo sin Causas"></asp:Label>
                        <asp:CheckBox ID="CBriesgosSinCausa" runat="server" />
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
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Eventos"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbGridEventos" runat="server">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label69" runat="server" Text="Codigo Evento:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox29" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label70" runat="server" Text="Descripción del evento:" Font-Size="Small"
                                                Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox30" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
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
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="center">
                                        <td class="style1">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                                ToolTip="Consultar" OnClick="ImageButton12_Click" />
                                        </td>
                                        <td class="style1">
                                            <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImageButton15_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TbEventos" runat="server" visible="false">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="False" HeaderStyle-CssClass="gridViewHeader"
                                                CssClass="table-center"
                                                BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                DataKeyNames="IdEvento"
                                                OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Código" DataField="CodigoEvento" />
                                                    <asp:BoundField HeaderText="Descripción" DataField="DescripcionEvento" />
                                                    <asp:BoundField HeaderText="Clase" DataField="NombreClaseEvento" />
                                                    <asp:BoundField HeaderText="Tipo de perdida" DataField="NombreTipoPerdidaEvento" />
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
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <br />
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Style="font-family: Calibri; font-size: medium"
                                    OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                                    AutoPostBack="True" TextAlign="Left" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">No Hubo Eventos</asp:ListItem>
                                    <asp:ListItem Value="1">Registro de Eventos</asp:ListItem>
                                </asp:RadioButtonList>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table id="TbNohuboeventos" runat="server" visible="false" align="center">
                        <tr align="center" bgcolor="#BBBBBB">
                            <td colspan="2">
                                <asp:Label ID="Label48" runat="server" Text="Registro no hubo eventos" Font-Bold="False"
                                    Font-Names="Calibri" Font-Size="medium"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label112" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label113" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label50" runat="server" Text="Empresa:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEmpresa" Width="150px" runat="server" CssClass="Apariencia"
                                    AutoPostBack="false">
                                    <%-- <asp:ListItem Value="---">---</asp:ListItem>
                                    <asp:ListItem Value="1">Vida</asp:ListItem>
                                    <asp:ListItem Value="2">Generales</asp:ListItem>
                                    <asp:ListItem Value="3">Ambas</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ForeColor="Red" ControlToValidate="ddlEmpresa"
                                    ValidationGroup="Addne" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr runat="server" visible="false">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="LMailEvento" runat="server" Text="Tipo Correo:" CssClass="Apariencia" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLmailEvent" Width="150px" runat="server" CssClass="Apariencia"
                                    AutoPostBack="false">
                                     <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                               
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label53" runat="server" Text="Fecha Registro:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox41" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label52" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox42" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="BtnGuardaNoeventos" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ValidationGroup="Addne" ToolTip="Guardar" Style="height: 20px" OnClick="BtnGuardaNoeventos_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnCancelaNoeventos" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="BtnCancelaNoeventos_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="TbConEventos" runat="server" visible="false" align="center">
                        <tr>
                            <td align="center">
                                <asp:TabContainer ID="TabContainerEventos" runat="server" ActiveTabIndex="0" Font-Names="Calibri"
                                    Font-Size="Small" Width="800px" aling="left" AutoPostBack="true" OnActiveTabChanged="TabContainerEventos_ActiveTabChanged">
                                    <asp:TabPanel ID="TabPanelCreaEvento" runat="server" HeaderText="Creación Evento"
                                        Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Creación Evento
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table id="TbTab1" runat="server" align="center">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server" visible="False">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label154" runat="server" Text="Código:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:Label ID="Label55" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td align="left" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label56" runat="server" Text="Empresa:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="ddlEmpresa1" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                        </asp:DropDownList>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ControlToValidate="ddlEmpresa1"
                                                            ValidationGroup="Addne" ValueToCompare="---" Operator="NotEqual">*</asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr align="center" runat="server">
                                                    <td bgcolor="#BBBBBB" colspan="4" runat="server">
                                                        <asp:Label ID="Label26" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Ubicación Del Evento"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label4" runat="server" Text="Región" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label5" runat="server" Text="Pais" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList2" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList2"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label23" runat="server" Text="Departamento" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList3" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList3"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label24" runat="server" Text="Ciudad" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList4" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList4"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label25" runat="server" Text="Oficina/Sucursal" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList5" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DropDownList5"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td id="Td4" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label27" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Detalle Ubicación"></asp:Label>
                                                    </td>
                                                    <td id="Td5" runat="server">
                                                        <asp:TextBox ID="TextBox43" runat="server" Width="195px" TextMode="MultiLine" Height="50px"
                                                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label80" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descripción del evento"></asp:Label>
                                                    </td>
                                                    <td colspan="3" runat="server">
                                                        <asp:TextBox ID="TextBox44" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Height="50px" Width="550px" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="TextBox44"
                                                            ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label81" runat="server" Text="Servicio/Producto" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList24" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList24_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="DropDownList24"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label82" runat="server" Text="SubServicio/SubProducto" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList25" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="DropDownList25"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label83" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Inicio"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox45" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBox45"
                                                            Format="yyyy-MM-dd" BehaviorID="_content_CalendarExtender4"></asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator124" runat="server" ControlToValidate="TextBox45"
                                                            ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                        

                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label84" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Hora (HH:MM am/pm)"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList12" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList12"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="DropDownList13" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="0">00</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList13"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="DropDownList14" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="am">a.m</asp:ListItem>
                                                            <asp:ListItem Value="pm">p.m</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="DropDownList14"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label85" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Feha Finalización"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox47" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBox47"
                                                            Format="yyyy-MM-dd" BehaviorID="_content_CalendarExtender5"></asp:CalendarExtender>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label86" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Hora (HH:MM am/pm)"></asp:Label>
                                                    </td>
                                                    <td id="Td6" runat="server">
                                                        <asp:DropDownList ID="DropDownList68" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="50px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="White"></asp:Label>
                                                        <asp:DropDownList ID="DropDownList69" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="50px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="0">00</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label3" runat="server" Text="*" ForeColor="White"></asp:Label>
                                                        <asp:DropDownList ID="DropDownList70" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="50px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="am">a.m</asp:ListItem>
                                                            <asp:ListItem Value="pm">p.m</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label6" runat="server" Text="*" ForeColor="White"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label87" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Descubrimiento"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox49" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="TextBox49"
                                                            Format="yyyy-MM-dd" BehaviorID="_content_CalendarExtender6"></asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator126" runat="server" ControlToValidate="TextBox49"
                                                            ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label88" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Hora (HH:MM am/pm)"></asp:Label>
                                                    </td>
                                                    <td id="Td7" runat="server">
                                                        <asp:DropDownList ID="DropDownList71" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="50px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="DropDownList71"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="DropDownList72" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="50px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="0">00</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="DropDownList72"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="DropDownList73" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="50px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="am">a.m</asp:ListItem>
                                                            <asp:ListItem Value="pm">p.m</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="DropDownList73"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" align="left">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label89" runat="server" Text="Canal" CssClass="Apariencia"></asp:Label>
                                                    </td>
                                                    <td runat="server" align="left">
                                                        <asp:DropDownList ID="DropDownList26" runat="server" Width="200px" CssClass="Apariencia">
                                                            <asp:ListItem>---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator128" runat="server" ControlToValidate="DropDownList26"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" align="left">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label90" runat="server" Text="Generador del Evento" CssClass="Apariencia"></asp:Label>
                                                    </td>
                                                    <td runat="server" align="left">
                                                        <asp:DropDownList ID="DropDownList27" runat="server" Width="200px" CssClass="Apariencia"
                                                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList27_SelectedIndexChanged">
                                                            <asp:ListItem>---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator129" runat="server" ControlToValidate="DropDownList27"
                                                            ForeColor="Red" InitialValue="---" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server" id="lresponable" visible="False">
                                                        <asp:Label ID="Label91" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable evento:"></asp:Label>
                                                    </td>
                                                    <td runat="server" id="tresponsable" visible="False">
                                                        <asp:TextBox ID="TextBox51" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Enabled="False" Width="200px"></asp:TextBox>
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
                                                                            OnSelectedNodeChanged="TreeView4_SelectedNodeChanged" AutoGenerateDataBindings="False">
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
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBox51" runat="server" ControlToValidate="TextBox51"
                                                            ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        Cuantía bruta</td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox52" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox52_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator127" runat="server" ControlToValidate="TextBox52"
                                                            ForeColor="Red" ValidationGroup="Addne">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label57" runat="server" Text="Fecha Registro:" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox39" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label62" runat="server" Text="Usuario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox40" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="center" runat="server">
                                                    <td colspan="4" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ValidationGroup="Addne" ToolTip="Guardar" Style="height: 20px" OnClick="ImageButton6_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ValidationGroup="Addne" ToolTip="Actualizar" Style="height: 20px" OnClick="ImageButton8_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="ImageButton7_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanelDtosCom" runat="server" HeaderText="Datos Complementarios"
                                        Font-Names="Calibri" Font-Size="Small" Visible="false">
                                        <HeaderTemplate>
                                            Datos Complementarios
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table id="TbDatosComplementarios" runat="server" align="center">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr align="center" runat="server">
                                                    <td bgcolor="#BBBBBB" colspan="4" runat="server">
                                                        <asp:Label ID="Label54" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Cadena De Valor"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label192" runat="server" Text="Cadena de valor" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList67" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList67_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="DropDownList67"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label18" runat="server" Text="Macroproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList9" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="DropDownList9"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label19" runat="server" Text="Proceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList10" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList10_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="DropDownList67"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label43" runat="server" Text="Subproceso" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList6" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ControlToValidate="DropDownList6"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label20" runat="server" Text="Actividad" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList11" runat="server" Width="200px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="0">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator123" runat="server" ControlToValidate="DropDownList11"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="center" runat="server">
                                                    <td bgcolor="#BBBBBB" colspan="4" runat="server">
                                                        <asp:Label ID="Label7" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable evento"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        Responsable proceso afectado</td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox34" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="200px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                        <asp:Label ID="lblExisteResponsableNotificacion" runat="server" Visible="False" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                        <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                            OnClientClick="return false;" />
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
                                                                        <asp:TreeView ID="TreeViewlast" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                            Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                            OnSelectedNodeChanged="TreeViewlast_SelectedNodeChanged" AutoGenerateDataBindings="False">
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
                                                <tr runat="server" align="left">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        Clase de riesgo operacional</td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList33" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList33_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="DropDownList33"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label99" runat="server" Text="SubClase de Riesgo" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList34" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="DropDownList34"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server" align="left">
                                                        <asp:Label ID="Label10" runat="server" Text="Tipo de Pérdida" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td colspan="3" runat="server">
                                                        <asp:DropDownList ID="DropDownList8" runat="server" Width="550px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="DropDownList8"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label68" runat="server" Text="Línea Operativa" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList23" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList23_SelectedIndexChanged">
                                                            <asp:ListItem Value="NULL">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label77" runat="server" Text="SubLínea Operativa" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList29" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList29_SelectedIndexChanged">
                                                            <asp:ListItem Value="NULL">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="TrMasLNegocio" align="left" runat="server" visible="False">
                                                    <td id="Td12" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label92" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Más Líneas Operativas"></asp:Label>
                                                    </td>
                                                    <td id="Td13" colspan="3" runat="server">
                                                        <asp:TextBox ID="TextBox17" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="550px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Tr3" runat="server" align="left">
                                                    <td id="Td8" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label51" runat="server" Text="Afecta Continuidad" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td id="Td9" runat="server">
                                                        <asp:DropDownList ID="DropDownList15" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="DropDownList15"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="Tr4" runat="server" align="left">
                                                    <td id="Td10" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label63" runat="server" Text="Estado" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td id="Td11" runat="server">
                                                        <asp:DropDownList ID="DropDownList16" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="DropDownList16"
                                                            InitialValue="---" ForeColor="Red" ValidationGroup="Addne1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="Tr6" align="left" runat="server">
                                                    <td id="Td14" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label98" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Observaciones"></asp:Label>
                                                    </td>
                                                    <td id="Td15" colspan="3" runat="server">
                                                        <asp:TextBox ID="TextBox53" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="550px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Tr1" align="center" runat="server">
                                                    <td id="Td2" colspan="4" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ValidationGroup="Addne1" ToolTip="Guardar" Style="height: 20px" OnClick="ImageButton19_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton20" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="ImageButton7_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanelContab" runat="server" HeaderText="Contabilización" Font-Names="Calibri"
                                        Font-Size="Small" Visible="false">
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td colspan="4">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label36" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable contabilización:"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TextBox33" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="195px" Enabled="False"></asp:TextBox>
                                                        <asp:Label ID="lblIdDependencia2" runat="server" Visible="False" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                        <asp:ImageButton ID="imgDependencia2" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                            OnClientClick="return false;" />
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
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        Catálogo afectadas</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox14" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label30" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Cuenta de Orden No"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox15" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server" id="TrCuentaPerdida" visible="False">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label31" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Cuenta de la pérdida"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox16" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        Divisa</td>
                                                    <td runat="server">
                                                        <asp:DropDownList ID="DropDownList74" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="195px">
                                                            <asp:ListItem>---</asp:ListItem>
                                                            <asp:ListItem Value="Euro">Euros</asp:ListItem>
                                                            <asp:ListItem Value="Dolar">Dolares</asp:ListItem>
                                                            <asp:ListItem Value="Peso">Pesos</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr align="left" id="TrCuentaPerdida" visible="false">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label37" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Tasa de Cambio"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox21" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox21_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label38" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor en Pesos"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox22" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox22_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        Cuantía total recuperada</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox23" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox23_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" class="auto-style1">
                                                        Divisa</td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList75" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="195px">
                                                            <asp:ListItem>---</asp:ListItem>
                                                            <asp:ListItem Value="Euro">Euros</asp:ListItem>
                                                            <asp:ListItem Value="Dolar">Dolares</asp:ListItem>
                                                            <asp:ListItem Value="Peso">Pesos</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label41" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Tasa de Cambio"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox25" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox25_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB">
                                                        Cuantía bruta</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox26" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox26_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server" id="TrSeguro" visible="False">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label44" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Valor Recuperado por Seguros"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox27" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox27_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        Cuantía bruta</td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="TextBox28" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            AutoPostBack="True" OnTextChanged="TextBox28_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Tr7" runat="server" align="left">
                                                    <td id="Td16" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label65" runat="server" Text="Recuperación" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td id="Td17" runat="server">
                                                        <asp:DropDownList ID="DropDownList28" Width="200px" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DropDownList28_SelectedIndexChanged">
                                                            <asp:ListItem>---</asp:ListItem>
                                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server" id="lrecuperacio" visible="False">
                                                        <asp:Label ID="Label64" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fuente de la Recuperación"></asp:Label>
                                                    </td>
                                                    <td id="trecuperacion" runat="server" visible="False">
                                                        <asp:TextBox ID="TextBox46" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="195px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Tr5" align="left" runat="server">
                                                    <td id="Td18" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label8" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Contabilización"></asp:Label>
                                                    </td>
                                                    <td id="Td19" runat="server">
                                                        <asp:TextBox ID="TextBox1" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1"
                                                            Format="yyyy-MM-dd" BehaviorID="_content_CalendarExtender1"></asp:CalendarExtender>
                                                    </td>
                                                    <td id="Td20" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Hora (HH:MM am/pm)"></asp:Label>
                                                    </td>
                                                    <td id="Td21" runat="server">
                                                        <asp:DropDownList ID="DropDownList7" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DropDownList30" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="0">00</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="DropDownList31" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="am">a.m</asp:ListItem>
                                                            <asp:ListItem Value="pm">p.m</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                 <tr  align="left" runat="server">
                                                    <td id="Td22" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="lblFechaRecuperacion" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Recuperación"></asp:Label>
                                                    </td>
                                                    <td id="Td23" runat="server">
                                                        <asp:TextBox ID="txtFechaRecuperacion" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="ceFechaRecuperacion" runat="server" TargetControlID="txtFechaRecuperacion"
                                                            Format="yyyy-MM-dd" BehaviorID="_content_ceFechaRecuperacion"></asp:CalendarExtender>
                                                    </td>
                                                    <td id="Td24" bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="lblHoraHoraRecuperacion" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Hora (HH:MM am/pm)"></asp:Label>
                                                    </td>
                                                    <td id="Td25" runat="server">
                                                        <asp:DropDownList ID="ddlHoraRecuperacion" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlMinutoRecuperacion" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="0">00</asp:ListItem>
                                                            <asp:ListItem Value="1">01</asp:ListItem>
                                                            <asp:ListItem Value="2">02</asp:ListItem>
                                                            <asp:ListItem Value="3">03</asp:ListItem>
                                                            <asp:ListItem Value="4">04</asp:ListItem>
                                                            <asp:ListItem Value="5">05</asp:ListItem>
                                                            <asp:ListItem Value="6">06</asp:ListItem>
                                                            <asp:ListItem Value="7">07</asp:ListItem>
                                                            <asp:ListItem Value="8">08</asp:ListItem>
                                                            <asp:ListItem Value="9">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="29">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="51">51</asp:ListItem>
                                                            <asp:ListItem Value="52">52</asp:ListItem>
                                                            <asp:ListItem Value="53">53</asp:ListItem>
                                                            <asp:ListItem Value="54">54</asp:ListItem>
                                                            <asp:ListItem Value="55">55</asp:ListItem>
                                                            <asp:ListItem Value="56">56</asp:ListItem>
                                                            <asp:ListItem Value="57">57</asp:ListItem>
                                                            <asp:ListItem Value="58">58</asp:ListItem>
                                                            <asp:ListItem Value="59">59</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlHorarioRecuperacion" runat="server" Width="50px" Font-Names="Calibri"
                                                            Font-Size="Small">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                            <asp:ListItem Value="am">a.m</asp:ListItem>
                                                            <asp:ListItem Value="pm">p.m</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr align="left" runat="server">
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="lblCuantiaRecuperadaSeguros" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Cuantía recuperada por seguros"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="txtCuantiaRecuperadaSeguros" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                             ></asp:TextBox>
                                                    </td>
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="lblCuantiaOtrasRecuperaciones" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Cuantía de otras recuperaciones"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="txtCuantiaOtrasRecuperaciones" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            ></asp:TextBox>
                                                    </td>
                                                </tr>


                                                <tr align="left" runat="server" >
                                                    <td bgcolor="#BBBBBB" runat="server">
                                                        <asp:Label ID="lblCuantiaNetaRecuperaciones" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Cuantía neta de recuperaciones"></asp:Label>
                                                    </td>
                                                    <td runat="server">
                                                        <asp:TextBox ID="txtCuantiaNetaRecuperaciones" runat="server" Width="195px" Font-Names="Calibri" Font-Size="Small"
                                                            ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Tr2" align="center" runat="server">
                                                    <td id="Td3" colspan="4" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton21" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" Style="height: 20px" OnClick="ImageButton21_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton22" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="ImageButton7_Click" />
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
            <tr id="trRiesgosEventos" runat="server" visible="false" align="center">
                <td>
                    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="3" Font-Names="Calibri"
                        Font-Size="Small" Width="900px">
                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Justificación cambios" Font-Names="Calibri"
                            Font-Size="Small">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label67" runat="server" Text="Justificación:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox11" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator64" runat="server" ControlToValidate="TextBox11" ValidationGroup="Addne"
                                                    ForeColor="Red">*</asp:RequiredFieldValidator>
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
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Documentos adjuntos" Font-Names="Calibri"
                            Font-Size="Small">
                            <ContentTemplate>
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
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Riesgos" Font-Names="Calibri"
                            Font-Size="Small">
                            <HeaderTemplate>
                                Riesgos
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table>
                                    <tr align="center">
                                        <td bgcolor="#BBBBBB" colspan="2">
                                            <asp:Label ID="Label46" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Consultar Riesgos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label75" runat="server" Text="Código:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox31" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label76" runat="server" Text="Nombre:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox32" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                                            ToolTip="Consultar" OnClick="ImageButton9_Click"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" OnClick="ImageButton10_Click"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td bgcolor="#BBBBBB" colspan="2">
                                            <asp:Label ID="Label13" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Riesgos a Asociar"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                DataKeyNames="ListaCausas,IdRiesgo,IdProbabilidad,IdImpacto"
                                                HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowCommand="GridView8_RowCommand" Visible="False">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                <Columns>
                                                    
                                                    <asp:BoundField HeaderText="Código Riesgo" DataField="Codigo" />
                                                    <asp:BoundField HeaderText="Nombre Riesgo" DataField="Nombre" />
                                                    <asp:ButtonField ButtonType="Image" CommandName="Relacionar" ImageUrl="~/Imagenes/Icons/select.png"
                                                        Text="Asociar"></asp:ButtonField>
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
                                        <td bgcolor="#BBBBBB" colspan="2">
                                            <asp:Label ID="Label34" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Riesgos relacionados"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                HorizontalAlign="Center" ShowHeaderWhenEmpty="True" OnRowCommand="GridView2_RowCommand"
                                                OnRowDataBound="OnRowDataBound" DataKeyNames="IdRiesgo">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:TemplateField>
                <ItemTemplate>
                    <img alt = "" style="cursor: pointer" src="../../Imagenes/Icons/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="IdCausas" HeaderText="Código" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="NombreCausas" HeaderText="Causa" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
                                                    <asp:BoundField DataField="Codigo" HeaderText="Código Riesgo" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre Riesgo" />
                                                    <asp:ButtonField ButtonType="Image"  CommandName="Desasociar" ImageUrl="~/Imagenes/Icons/delete.png"
                                                        Text="Desasociar"></asp:ButtonField>
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
                                    <tr>
                                        <td>
                                            <br />
                                        </td>

                                    </tr>
                                    <tr align="center">
                                        <td bgcolor="#BBBBBB" colspan="2">
                                            <asp:Label ID="Label11" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Asociaciones Eliminadas"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <asp:GridView ID="GvDesasociar" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="IdEvento" HeaderText="IdRiesgo" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="CodigoEvento" HeaderText="Código Evento"></asp:BoundField>
                                                    <asp:BoundField DataField="IdRiesgo" HeaderText="IdControl" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="CodigoRiesgo" HeaderText="Código Riesgo"></asp:BoundField>
                                                    <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" Visible="False"></asp:BoundField>
                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario"></asp:BoundField>
                                                    <asp:BoundField DataField="Justificacion" HeaderText="Justificación"></asp:BoundField>
                                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha Desasociación"></asp:BoundField>
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
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Plan de Acción" Font-Names="Calibri"
                            Font-Size="Small">
                            <ContentTemplate>
                                <table>
                                    <tr align="center">
                                        <td>
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
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ToolTip="Insertar" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            OnClick="ImageButton5_Click"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddPlanAccion" runat="server" visible="False">
                                        <td id="Td1" runat="server">
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
                                                                    <asp:TextBox ID="TextBox20" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                                            ID="RequiredFieldValidator59" runat="server" ControlToValidate="TextBox20" ForeColor="Red"
                                                                            ValidationGroup="validatePlanAccion">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr align="left">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label47" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                                                </td>
                                                                <td bgcolor="#EEEEEE">
                                                                    <asp:TextBox ID="TextBox35" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="400px" Enabled="False"></asp:TextBox><asp:Label ID="lblIdDependencia3" runat="server"
                                                                            Visible="False"></asp:Label><asp:ImageButton ID="imgDependencia3" runat="server"
                                                                                ImageUrl="~/Imagenes/Icons/Organization-Chart.png" OnClientClick="return false;" /><asp:RequiredFieldValidator
                                                                                    ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBox35" ForeColor="Red"
                                                                                    ValidationGroup="validatePlanAccion">*</asp:RequiredFieldValidator><asp:PopupControlExtender
                                                                                        ID="popupDependencia3" runat="server" DynamicServicePath="" ExtenderControlID=""
                                                                                        TargetControlID="imgDependencia3" BehaviorID="popup3" PopupControlID="pnlDependencia3"
                                                                                        OffsetY="-200">
                                                                                    </asp:PopupControlExtender>
                                                                    <asp:Panel ID="pnlDependencia3" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                                        <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                            <tr align="right" bgcolor="#5D7B9D">
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnClosepp3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                                        OnClientClick="$find('popup3').hidePopup(); return false;" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TreeView ID="TreeView3" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                                        Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                                        AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView3_SelectedNodeChanged">
                                                                                        <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
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
                                                                    <asp:TextBox ID="TextBox36" runat="server" Font-Names="Calibri" Font-Size="Small"
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
                                                                    <asp:TextBox ID="TextBox37" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="150px"></asp:TextBox><asp:CalendarExtender ID="TextBox15_CalendarExtender"
                                                                            runat="server" TargetControlID="TextBox37" Format="yyyy-MM-dd" BehaviorID="_content_TextBox15_CalendarExtender"></asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="TextBox37"
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
                                                                                <asp:TextBox ID="TextBox38" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                                                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox><asp:RequiredFieldValidator
                                                                                        ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextBox38" ValidationGroup="validatePlanAccion"
                                                                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="right">
                                                                            <td colspan="2">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                                ToolTip="Cancelar" Visible="False" OnClick="ImageButton11_Click"></asp:ImageButton>
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
                                                                                <asp:Label ID="Label79" runat="server" Text="Adjuntar documento:" Font-Names="Calibri"
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
                                                                        ToolTip="Guardar" ValidationGroup="validatePlanAccion" Visible="False" OnClick="ImageButton13_Click"
                                                                        Style="height: 20px"></asp:ImageButton>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" ValidationGroup="validatePlanAccion" Visible="False" OnClick="ImageButton12_Click1"></asp:ImageButton>
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
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
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
            <table width="100%" >
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td align="center" runat="server" id="tdCaptionOkNo" colspan="2">&nbsp;
                        <asp:Label ID="lblCaptionOkNo" runat="server" Text="Atención" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center" rowspan="3">
                        <asp:Image ID="imgInfoOkNo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBoxOkNo" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br />
                        <asp:Label ID="Label12" runat="server" Text="Justificación:" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TBoxJustificacion" runat="server" Font-Names="Calibri" Font-Size="Small"
                            Height="50px" Width="90%" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TBoxJustificacionRequired" runat="server" ControlToValidate="TBoxJustificacion"
                            ValidationGroup="Justificacion" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr align="right">
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptarOkNo" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small"
                            OnClick="btnAceptarOkNo_Click" ValidationGroup="Justificacion" />
                        <asp:Button ID="btnCancelarOkNo" runat="server" Text="Cancelar" Font-Names="Calibri"
                            Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="TabContainer1" />
    </Triggers>
</asp:UpdatePanel>

