<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Estadisticas.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Estadisticas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
</style>
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Tipo estadística"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            Width="155px">
                            <asp:ListItem Value="0">---</asp:ListItem>
                            <asp:ListItem Value="1">ROI en estudio</asp:ListItem>
                            <asp:ListItem Value="2">Conocimiento del cliente</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table id="tbROI" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Factor riesgo"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="Segmento"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Tipo segmento"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Atributo"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Clasificación"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="155px"
                                                    OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                    <asp:ListItem Value="---">---</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                    Width="155px">
                                                    <asp:ListItem Value="---">---</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged"
                                                    Width="155px">
                                                    <asp:ListItem Value="---">---</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged"
                                                    Width="155px">
                                                    <asp:ListItem Value="---">---</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList6" runat="server" Width="155px">
                                                    <asp:ListItem Value="---">---</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" Text="Consultar" OnClick="Button1_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button2" runat="server" Text="Limpiar" OnClick="Button2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EditRowStyle BackColor="#FF6418" />
                                                    <FooterStyle BackColor="#FF6418" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#FF6418" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#FF6418" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    <Columns>
                                                        <asp:BoundField DataField="NumeroRegistros" HeaderText="Número de registros" />
                                                        <asp:BoundField DataField="Segmento" HeaderText="Segmento" />
                                                        <asp:ButtonField ButtonType="Button" Text="Ver" HeaderText="Ver" CommandName="Ver" />
                                                    </Columns>
                                                </asp:GridView>
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
    <tr>
        <td>
            <table id="tbCliente" runat="server" visible="false">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="Tipo de persona"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Tipo de inusualidad"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="Estado"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownList9" runat="server">
                                                    <asp:ListItem Value="0">Natural</asp:ListItem>
                                                    <asp:ListItem Value="1">Juridica</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList7" runat="server">
                                                    <asp:ListItem Value="Conve1">Nombre y/o identificación no consistentes con soportes</asp:ListItem>
                                                    <asp:ListItem Value="Conve2">Nombre de representante legal no coincide con cámara de comercio</asp:ListItem>
                                                    <asp:ListItem Value="Conve3">Información representante legal no consistente con documento de identidad</asp:ListItem>
                                                    <asp:ListItem Value="Conve4">Dirección domicilio u oficina no consistente con soporte</asp:ListItem>
                                                    <asp:ListItem Value="Conve5">Actividad económica no consistente con soporte</asp:ListItem>
                                                    <asp:ListItem Value="Conve6">Socios o accionistas no consistentes con soporte</asp:ListItem>
                                                    <asp:ListItem Value="Conve7">Cifras financieras no consistentes con estados financieros</asp:ListItem>
                                                    <asp:ListItem Value="Conve8">Falta verificación de listas</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList8" runat="server">
                                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                                    <asp:ListItem Value="SI">SI</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button3" runat="server" Text="Consultar" OnClick="Button3_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Text="Limpiar" OnClick="Button4_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" OnRowCommand="GridView2_RowCommand">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EditRowStyle BackColor="#FF6418" />
                                                    <FooterStyle BackColor="#FF6418" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#FF6418" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#FF6418" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    <Columns>
                                                        <asp:BoundField DataField="NumeroRegistros" HeaderText="Número de registros" />
                                                        <asp:BoundField DataField="TipoPersona" HeaderText="Tipo de persona" />
                                                        <asp:BoundField DataField="Inusualidad" HeaderText="Inusualidad" />
                                                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                                        <asp:ButtonField ButtonType="Button" Text="Ver" HeaderText="Ver" CommandName="Ver" />
                                                    </Columns>
                                                </asp:GridView>
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
<asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
    PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
    Enabled="True" DropShadow="true">
</asp:ModalPopupExtender>
<asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
<asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
    BackColor="#FFFFFF" BorderStyle="Solid">
    <table width="100%">
        <tr class="topHandle" style="background-color: #5D7B9D">
            <td colspan="2" align="center" runat="server" id="tdCaption">
                &nbsp;
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
