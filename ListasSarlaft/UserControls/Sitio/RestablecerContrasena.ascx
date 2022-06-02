<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RestablecerContrasena.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Sitio.RestablecerContrasena" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<table align="center" bgcolor="#EEEEEE">
    <tr align="center" bgcolor="#333399">
        <td>
            <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Reestablecer contraseña"
                Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="0" align="center">
                <tr>
                    <td align="right" bgcolor="#BBBBBB">
                        <asp:Label ID="lblUsuario" runat="server" Font-Names="Calibri" Font-Size="Small">Usuario:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxUsername" runat="server" MaxLength="40" Font-Names="Calibri"
                            Font-Size="Small"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserRequired" runat="server" ControlToValidate="tbxUsername"
                            ErrorMessage="El usuario es obligatorio." ToolTip="El usuario es obligatorio."
                            ValidationGroup="ctl00$ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#BBBBBB">
                        <asp:Label ID="lblNroId" runat="server" Font-Names="Calibri" Font-Size="Small">Número de Identificación:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxNroId" runat="server" MaxLength="40" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <%--  --%>
                        <asp:RequiredFieldValidator ID="NroIdRequired" runat="server" ControlToValidate="tbxNroId"
                            ErrorMessage="El número de identificación es obligatorio." ToolTip="El número de identificación es obligatorio."
                            ValidationGroup="ctl00$ChangePassword1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnEnviar" runat="server" CommandName="Enviar" Text="Enviar" ValidationGroup="ctl00$ChangePassword1"
                                    OnClick="btnEnviar_Click" Font-Names="Calibri" Font-Size="Small" Width="70"/>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnCancelar" runat="server" CommandName="Cancelar" Text="Cancelar"
                                    OnClick="btnCancelar_Click" Font-Names="Calibri" Font-Size="Small" Width="70"/>
                            </td>
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
