<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="ListasSarlaft.UserControls.Login" %>
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
<table align="right">
    <tr>
        <td>
            <asp:Image ID="Fondo" runat="server" ImageUrl="~/Imagenes/Aplicacion/Fondo1.png"
                Width="80%" />
        </td>
        <td valign="top">
            <table>
                <tr>
                    <td bgcolor="#D2D3D5" align="center">
                        <table border="0" cellpadding="1" cellspacing="0" align="right" width="100%">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" align="center">
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Icons/Llave.png" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Font-Names="Calibri"
                                                    Font-Size="Small" ForeColor="DarkBlue">Usuario: </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" Width="150px" MaxLength="20" BorderWidth="1px"
                                                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio."
                                                    ValidationGroup="ctl00$Login1"  ForeColor="Red">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="UserNameExpVal" runat="server" ControlToValidate="UserName"
                                                    ValidationGroup="ctl00$Login1"  ValidationExpression="^[0-9A-Za.-zÑñ]*$" ForeColor="Red">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Font-Names="Calibri"
                                                    Font-Size="Small" ForeColor="DarkBlue">Contraseña: </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px" MaxLength="40"
                                                    BorderWidth="1px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                                    ValidationGroup="ctl00$Login1" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Aceptar" ValidationGroup="ctl00$Login1"
                                                    OnClick="LoginButton_Click" Font-Names="Calibri" Font-Size="Small" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="font-family: Calibri; font-size: small">
                                                 <a href="../../Formularios/Sitio/ReestablecerContrasena.aspx">Reestablecer Contraseña</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red" Font-Size="Small"
                                        Font-Names="Calibri" Visible="false"></asp:Label>
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
    BackColor="#D2D3D5" BorderStyle="Solid">
    <table width="100%">
        <tr class="topHandle" style="background-color: #D2D3D5">
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