<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfPerfilVariable.ascx.cs"
    Inherits="ListasSarlaft.UserControls.ConfigEstructura.ConfPerfilVariable" %>
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
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Configuración Relación Perfil - Variable"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr id="Tr2" align="left" runat="server">
                            <td id="Td2" bgcolor="#5D7B9D" style="color: #FFFFFF; font-weight: 700" runat="server">
                                <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Perfil:"></asp:Label>
                            </td>
                            <td id="Td3" runat="server">
                                <asp:DropDownList ID="ddlPerfiles" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPerfiles_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="Tr1" align="left" runat="server">
                            <td id="Td1" runat="server" colspan="2">
                                <table id="TblVarAsoc" runat="server" align="center">
                                    <tr id="Tr3" align="left" runat="server">
                                        <td id="Td4" bgcolor="#EEEEEE" runat="server">
                                            <asp:Panel ID="PnlVarAsoc" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList"
                                                Width="300px">
                                                <asp:CheckBoxList ID="chbVarAsoc" runat="server" Font-Names="Calibri" Font-Size="Small">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr id="Tr4" runat="server" align="center">
                                        <td id="Td5" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnGuardarPerfilVar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            OnClick="ibtnGuardarPerfilVar_Click" ToolTip="Guardar" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnCancelPerfilVar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            OnClick="ibtnCancelPerfilVar_Click" ToolTip="Cancelar" />
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
    </ContentTemplate>
</asp:UpdatePanel>
