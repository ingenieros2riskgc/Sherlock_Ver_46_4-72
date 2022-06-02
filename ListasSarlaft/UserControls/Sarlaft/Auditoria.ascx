<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Auditoria.ascx.cs" Inherits="ListasSarlaft.UserControls.Auditoria" %>
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
                    <asp:Label ID="Label8" runat="server" ForeColor="White" Text="Auditoria consulta de listas"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Nombres" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Apellidos" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Usuario Aplicación" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Fecha Desde" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" Font-Names="Calibri" Font-Size="Small" runat="server"
                                    onclick="return new JsDatePick({useMode: 2, target: 'ctl00_ContentPlaceHolder5_Auditoria_TextBox4', dateFormat: '%Y %m %d'})"
                                    onkeypress="return new JsDatePick({useMode: 2, target: 'ctl00_ContentPlaceHolder5_Auditoria_TextBox4', dateFormat: '%Y %m %d'})"></asp:TextBox>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Fecha Hasta" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    onclick="return new JsDatePick({useMode: 2, target: 'ctl00_ContentPlaceHolder5_Auditoria_TextBox5', dateFormat: '%Y %m %d'})"
                                    onkeypress="return new JsDatePick({useMode: 2, target: 'ctl00_ContentPlaceHolder5_Auditoria_TextBox5', dateFormat: '%Y %m %d'})"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Buscar" OnClick="Button1_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Limpiar" OnClick="Button2_Click" />
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
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="True"
                                            OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="True"
                                            HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" HorizontalAlign="Center"
                                            Font-Names="Calibri" Font-Size="Small">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                                <asp:BoundField DataField="FechaConsulta" HeaderText="Fecha Consulta" />
                                                <asp:BoundField DataField="NombreClaseLista" HeaderText="Lista" />
                                                <asp:BoundField DataField="Documento" HeaderText="Documento" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                <asp:BoundField DataField="Alias" HeaderText="Alias" />
                                                <asp:BoundField DataField="NumeroRegistros" HeaderText="Numero Registros" />
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
                                    </asp:Panel>
                                </asp:Panel>
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
</asp:UpdatePanel>
