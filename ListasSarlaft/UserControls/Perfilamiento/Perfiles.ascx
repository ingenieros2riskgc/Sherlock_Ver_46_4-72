<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Perfiles.ascx.cs" Inherits="ListasSarlaft.UserControls.Perfilamiento.Perfiles" %>
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
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Configuracion Perfiles"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="gvPerfiles" runat="server" CellPadding="4" EnableModelValidation="True"
                                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="True"
                                        OnPageIndexChanging="gvPerfiles_PageIndexChanging" OnRowCommand="gvPerfiles_RowCommand"
                                        ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid"
                                        HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="StrUsuario" HeaderText="IdUsuario" Visible="False" />
                                            <asp:BoundField DataField="StrIdPerfil" HeaderText="IdPerfil" Visible="False" />
                                            <asp:BoundField DataField="StrNombrePerfil" HeaderText="Nombre Perfil" />
                                            <asp:BoundField DataField="StrValorMinimo" HeaderText="Rango Mínimo >=" />
                                            <asp:BoundField DataField="StrValorMaximo" HeaderText="Rango Máximo <" />
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
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
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                                <asp:ImageButton ID="ibtnAgregar" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Agregar" OnClick="ibtnAgregar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="updateUser" runat="server" visible="false">
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Nombre Perfil" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbNombrePerfil" runat="server" MaxLength="25" Font-Names="Calibri"
                                    Font-Size="Small" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbNombrePerfil" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Rango Mínimo >=" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbValorMinimo" runat="server" MaxLength="10" Font-Names="Calibri"
                                    Font-Size="Small" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbValorMinimo" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Rango Máximo <" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbValorMaximo" runat="server" MaxLength="10" Font-Names="Calibri"
                                    Font-Size="Small" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="updateUser1"
                                    ControlToValidate="tbValorMaximo" Display="Dynamic" Font-Names="Calibri" Font-Size="Small"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ibtnGuardar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" OnClick="ibtnGuardar_Click" ValidationGroup="saveUser1" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnGuardarUpd" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Modificar" Visible="false" OnClick="ibtnGuardarUpd_Click" ValidationGroup="updateUser1" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibtnCancelUpd" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ibtnCancelUpd_Click" />
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
    </ContentTemplate>
</asp:UpdatePanel>
