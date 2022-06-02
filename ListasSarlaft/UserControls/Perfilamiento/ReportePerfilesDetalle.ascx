<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportePerfilesDetalle.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Perfilamiento.ReportePerfilesDetalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
        width: 815px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td class="style1">
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Reporte Perfiles Detallado"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td class="style1">
                    <table>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Nro. Identificación:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="tbNroIdentificacion" runat="server" Width="100px" Font-Names="Calibri"
                                    Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Fecha inicial:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbFechaIni" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tbFechaIni_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                    Enabled="True" TargetControlID="tbFechaIni"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Fecha final:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="tbFechaFin" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                <asp:CalendarExtender ID="tbFechaFin_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                    Enabled="True" TargetControlID="tbFechaFin"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Perfil:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlPerfil" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Filtro Dinámico 1:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlFiltroDin1" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="Td4" runat="server" valign="top">
                                            <asp:GridView ID="gvOperadorFD1" runat="server" CellPadding="4" ForeColor="#333333"
                                                GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False" OnPageIndexChanging="gvOperadorFD1_PageIndexChanging"
                                                OnRowCommand="gvOperadorFD1_RowCommand" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="StrIdOperador" HeaderText="IdOperador" Visible="False" />
                                                    <asp:BoundField DataField="StrNombreOperador" HeaderText="Nombre Operador" />
                                                    <asp:BoundField DataField="StrIdentificadorOperador" HeaderText="Identificador" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="SelecOperador"
                                                        CommandName="SelecOperador" />
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
                                        <td id="Td5" runat="server"></td>
                                        <td id="Td6" runat="server" valign="top" align="left">
                                            <table>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lblOtroValorFD1" runat="server" Text="Otro Valor:" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbxOtroValorFD1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr align="left">
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDesdeFD1" runat="server" Text="Desde:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxDesdeFD1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblHastaFD1" runat="server" Text="Hasta:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxHastaFD1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="cbxFD1" runat="server" Enabled="False" Visible="False" />
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
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Filtro Dinámico 2:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlFiltroDin2" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="Td1" runat="server" valign="top">
                                            <asp:GridView ID="gvOperadorFD2" runat="server" CellPadding="4" ForeColor="#333333"
                                                GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False" OnPageIndexChanging="gvOperadorFD2_PageIndexChanging"
                                                OnRowCommand="gvOperadorFD2_RowCommand" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="StrIdOperador" HeaderText="IdOperador" Visible="False" />
                                                    <asp:BoundField DataField="StrNombreOperador" HeaderText="Nombre Operador" />
                                                    <asp:BoundField DataField="StrIdentificadorOperador" HeaderText="Identificador" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="SelecOperador"
                                                        CommandName="SelecOperador" />
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
                                        <td id="Td2" runat="server"></td>
                                        <td id="Td3" runat="server" valign="top" align="left">
                                            <table>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lblOtroValorFD2" runat="server" Text="Otro Valor:" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbxOtroValorFD2" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr align="left">
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDesdeFD2" runat="server" Text="Desde:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxDesdeFD2" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblHastaFD2" runat="server" Text="Hasta:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxHastaFD2" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="cbxFD2" runat="server" Enabled="False" Visible="False" />
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
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Filtro Dinámico 3:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:DropDownList ID="ddlFiltroDin3" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td runat="server" valign="top">
                                            <asp:GridView ID="gvOperadorFD3" runat="server" CellPadding="4" ForeColor="#333333"
                                                GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False" OnPageIndexChanging="gvOperadorFD3_PageIndexChanging"
                                                OnRowCommand="gvOperadorFD3_RowCommand" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="StrIdOperador" HeaderText="IdOperador" Visible="False" />
                                                    <asp:BoundField DataField="StrNombreOperador" HeaderText="Nombre Operador" />
                                                    <asp:BoundField DataField="StrIdentificadorOperador" HeaderText="Identificador" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="SelecOperador"
                                                        CommandName="SelecOperador" />
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
                                        <td runat="server"></td>
                                        <td runat="server" valign="top" align="left">
                                            <table>
                                                <tr align="left">
                                                    <td>
                                                        <asp:Label ID="lblOtroValorFD3" runat="server" Text="Otro Valor:" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbxOtroValorFD3" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr align="left">
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDesdeFD3" runat="server" Text="Desde:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxDesdeFD3" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblHastaFD3" runat="server" Text="Hasta:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="tbxHastaFD3" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="cbxFD3" runat="server" Enabled="False" Visible="False" />
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
                </td>
            </tr>
            <tr align="center">
                <td class="style1">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                    ToolTip="Consultar" OnClick="btnConsultar_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" OnClick="btnCancelar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="tbPerfilTemp" runat="server" Visible="false" Width="63px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center" id="trRptPerfiles" runat="server" >
                <td class="style1">
                    <table>
                        <tr align="left">
                            <td>
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="1300px" Width="750px"
                                    InteractiveDeviceInfos="(Colección)" ShowParameterPrompts="False" ShowPrintButton="False"
                                    ShowRefreshButton="False" ShowZoomControl="False" OnReportError="TheReport_ReportError" >
                                    <LocalReport ReportPath="Reportes\RptPerfilesDetalle.rdlc">
                                        <DataSources>
                                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1_DataTable3" />
                                        </DataSources>
                                    </LocalReport>
                                </rsweb:ReportViewer>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                                    TypeName="DataSet1TableAdapters."></asp:ObjectDataSource>
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
    </ContentTemplate>
</asp:UpdatePanel>
