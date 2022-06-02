<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargaMasiva.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CargaMasiva" %>
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
<uc:OkMessageBox ID="omb" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Ltitulo" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Carga Masiva de Riesgos,Controles y Eventos"></asp:Label>
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
                                            <asp:Label ID="Lopcion" runat="server" Text="Opciones" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DDLopciones" runat="server" Width="250px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DDLopciones_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                <asp:ListItem Value="1">Riesgos</asp:ListItem>
                                                <asp:ListItem Value="2">Controles</asp:ListItem>
                                                <asp:ListItem Value="3">Eventos</asp:ListItem>
                                                <asp:ListItem Value="4">Riesgo vs Control</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                    </td>
                </tr>
            <tr align="center" runat="server" visible="false" id="TrButtonsExportRiesgos">
                <td>
                    <table id="TbDownload" runat="server">
                        <tr align="center">
                            <td>
                            Para exportar el Excel de Datos Parametricos:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportDataRiesgo" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportDataRiesgo_Click"/>
                    </td>
                            <td>
                            Para exportar la plantilla de cargue en Excel:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExportPlantillaRiesgo" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExportPlantillaRiesgo_Click"/>
                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            <tr id="TrLoadFile" runat="server" visible="false" align="center">
                <td>
                    <table>
                        <tr align="center">
                            <td>
                                <asp:Label ID="LtextLoad" runat="server" Text="Cargar la plantilla de Excel"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="FUloadExcel" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="Bload" runat="server" Text="Analizar" OnClick="Bload_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TgridRiesgos" runat="server" visible="false">
            <tr>
                <td align="center">
<asp:GridView ID="GVriesgos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True" OnPageIndexChanging="GVriesgos_PageIndexChanging" PageSize="30">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="Codigo" />
                                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
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
                    <asp:GridView ID="GVcontrol" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True"  PageSize="30" OnPageIndexChanging="GVcontrol_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="CodigoControl" />
                                        <asp:BoundField HeaderText="Nombre" DataField="NombreControl" />
                                        <asp:BoundField HeaderText="Descripción" DataField="DescripcionControl" />
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
                    <asp:GridView ID="GVeventos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True" OnPageIndexChanging="GVeventos_PageIndexChanging" PageSize="30" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="CodigoEvento" />
                                        <asp:BoundField HeaderText="Descripción" DataField="DescripcionEvento" />
                                        <asp:BoundField HeaderText="Nombre Generador" DataField="NomGeneradorEvento" />
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
                    <asp:GridView ID="GVriesgovscontrol" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True"  PageSize="30" OnPageIndexChanging="GVriesgovscontrol_PageIndexChanging" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Id Riesgo vs Control" DataField="IdControlesRiesgo" />
                                        <asp:BoundField HeaderText="Código" DataField="Codigo" />
                                        <asp:BoundField HeaderText="Nombre Riesgo" DataField="Nombre" />
                                        <asp:BoundField HeaderText="Código" DataField="CodigoControl" />
                                        <asp:BoundField HeaderText="Nombre Control" DataField="NombreControl" />
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
                        <tr>
                            <td align="center">
                                <asp:ImageButton ID="ImbCancel" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbCancel_Click"/>
                            </td>
                        </tr>
        </table>
                </td>
            </tr>
            </table>
        
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
                             />
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
    </asp:UpdatePanel>