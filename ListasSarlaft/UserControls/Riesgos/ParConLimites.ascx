<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParConLimites.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.ParConLimites" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Limites de calificación"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Escala" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Limite Inferior" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Limite Superior" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Excelente" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtExcelenteInferior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtExcelenteInferior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtExcelenteInferior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtExcelenteSuperior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtExcelenteSuperior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtExcelenteSuperior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Bueno" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtBuenoInferior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtBuenoInferior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtBuenoInferior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtBuenoSuperior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtBuenoSuperior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtBuenoSuperior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Regular" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtRegularInferior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtRegularInferior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtRegularInferior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtRegularSuperior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtRegularSuperior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtRegularSuperior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Deficiente" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtDeficienteInferior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtDeficienteInferior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtDeficienteInferior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="txtDeficienteSuperior" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtDeficienteSuperior" ValidationGroup="validarCampos"></asp:RequiredFieldValidator>
                                <asp:RangeValidator runat="server" Type="Double" ControlToValidate="txtDeficienteSuperior"
                                    ValidationGroup="validarCampos" ForeColor="Red" ErrorMessage="!" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3">
                                <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    ToolTip="Guardar" ValidationGroup="validarCampos" OnClick="btnGuardar_Click" />
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
