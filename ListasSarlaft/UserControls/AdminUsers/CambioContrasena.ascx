<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CambioContrasena.ascx.cs" Inherits="ListasSarlaft.UserControls.AdminUsers.CambioContrasena" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table align="center" bgcolor="#EEEEEE">
    <tr align="center" bgcolor="#333399">
        <td>
            <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Cambio contraseña"
                Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="0" align="center">
                <tr>
                    <td align="right" bgcolor="#BBBBBB">
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword"
                            Font-Names="Calibri" Font-Size="Small">Contraseña actual:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" MaxLength="40"
                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                            ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                            ValidationGroup="ctl00$ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#BBBBBB">
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword"
                            Font-Names="Calibri" Font-Size="Small">Nueva contraseña:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" MaxLength="40" Font-Names="Calibri"
                            Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            ErrorMessage="La nueva contraseña es obligatoria." ToolTip="La nueva contraseña es obligatoria."
                            ValidationGroup="ctl00$ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#BBBBBB">
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword"
                            Font-Names="Calibri" Font-Size="Small">Confirmar la nueva contraseña:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" MaxLength="40"
                            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                            ErrorMessage="Confirmar la nueva contraseña es obligatorio." ToolTip="Confirmar la nueva contraseña es obligatorio."
                            ValidationGroup="ctl00$ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="Confirmar la nueva contraseña debe coincidir con la entrada Nueva contraseña."
                            ValidationGroup="ctl00$ChangePassword1" Font-Names="Calibri" Font-Size="Small"
                            ForeColor="Red"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="Label11" runat="server" Visible="False" Font-Bold="False" Font-Names="Calibri"
                            Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                            Text="Cambiar contraseña" ValidationGroup="ctl00$ChangePassword1" OnClick="ChangePasswordPushButton_Click"
                            Font-Names="Calibri" Font-Size="Small" />
                    </td>
                    <td>
                        &nbsp;
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
