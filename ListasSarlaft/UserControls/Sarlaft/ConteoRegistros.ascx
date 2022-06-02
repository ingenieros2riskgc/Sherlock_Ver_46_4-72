<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConteoRegistros.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Sarlaft.ConteoRegistros" %>
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Consulta de cantidad de registros por cargue de información"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Fecha registro desde" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td colspan="2" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Fecha registro hasta" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="150px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox1"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                    InitialValue="" ValidationGroup="Consultar" Font-Bold="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="TextBox2" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="150px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox2"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                    InitialValue="" ValidationGroup="Consultar" Font-Bold="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Consultar" ToolTip="Consultar" Font-Names="Calibri"
                                                Font-Size="Small" ValidationGroup="Consultar" OnClick="Button1_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text="Limpiar" ToolTip="Limpiar" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button2_Click" />
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
                    <table>
                        <tr align="left">
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                    Font-Size="Small" Visible="false" OnClick="Button3_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" 
                                    Font-Size="Small" AllowPaging="True" 
                                    onpageindexchanging="GridView1_PageIndexChanging" PageSize="10">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdConteo" HeaderText="IdConteo" Visible="false" />
                                        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario Que Realizo El Cargue" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                        <asp:BoundField DataField="RegistrosCargue" HeaderText="Cantidad Registros Cargue" />
                                        <asp:BoundField DataField="RegistrosOperacion" HeaderText="Cantidad Registros Detectados" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripciíon" />
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
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="Button3" />
    </Triggers>
</asp:UpdatePanel>
