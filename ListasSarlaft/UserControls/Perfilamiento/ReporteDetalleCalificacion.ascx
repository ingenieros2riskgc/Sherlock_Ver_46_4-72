<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteDetalleCalificacion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Perfilamiento.ReporteDetalleCalificacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .gridViewHeader a:link {
        text-decoration: none;
    }

    .FondoAplicacion {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .scrollingControlContainer {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .style1 {
        width: 1200px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td class="style1">
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Reporte Detalle Calificación"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td class="style1">
                    <table>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lNumeroIdentificacion" runat="server" Text="Nro. Identificación:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtNumeroIdentificacion" runat="server" Width="200px" Font-Names="Calibri"
                                    Font-Size="Small"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumeroIdentificacion" ErrorMessage="Debe ingresar un valor" ForeColor="Red" ValidationGroup="DetalleCalificacion"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="lArchivo" runat="server" Text="Archivo:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlArchivo" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlArchivo"
                                            InitialValue="" ErrorMessage="*" ValidationGroup="DetalleCalificacion" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td class="style1">
                    <div style="margin-right: 150px;">
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnConsultar" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                        ToolTip="Consultar" ValidationGroup="DetalleCalificacion" OnClick="btnConsultar_Click" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                        ToolTip="Cancelar" OnClick="btnCancelar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr align="center" id="trRptPerfiles" runat="server" visible="false">
                <td class="style1">
                    <table>
                        <tr align="left">
                            <td>
                                <asp:GridView ID="gvDetalleCalificacion" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" Visible="true">
                                    <Columns>
                                        <asp:BoundField DataField="Posicion" HeaderText="No." ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="NombreVariable" HeaderText="Variable" />
                                        <asp:BoundField DataField="PesoVariable" HeaderText="Peso" />
                                        <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                        <asp:BoundField DataField="NombreCategoria" HeaderText="Categoria" />
                                        <asp:BoundField DataField="PesoCategoria" HeaderText="Valoración" />
                                        <asp:BoundField DataField="Calificacion" HeaderText="Calificación" />
                                        <asp:BoundField DataField="NumeroDocumento" HeaderText="Documento" />
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
                                <div style="text-align: center">
                                    <span style="font-size: medium; font-family: Calibri;">Total Calificación: </span>
                                    <asp:Label ID="lblCalificacion" runat="server" Font-Names="Calibri"
                                        Font-Size="Small"></asp:Label>
                                </div>
                                <div style="height: 20px;"></div>
                                <div style="text-align: center">
                                    <asp:ImageButton ID="ibtnDescargarArchivo" runat="server" ImageUrl="~/Imagenes/Icons/downloads.png"
                                        OnClick="ibtnDescargarArchivo_Click" Width="20px" ToolTip="Descargar" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
