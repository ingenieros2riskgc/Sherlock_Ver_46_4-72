<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParConDesviaciones.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Riesgos.ParConDesviaciones" %>
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
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Desviaciones frecuencia e impacto"></asp:Label>
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
                                <asp:Label ID="Label3" runat="server" Text="Desviaciones de frecuencia" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Desviaciones de impacto" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Excelente" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox1" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" ControlToValidate="TextBox1" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox2" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" ControlToValidate="TextBox2" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Bueno" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox3" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator3" ControlToValidate="TextBox3" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox4" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox4"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator4" ControlToValidate="TextBox4" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="Regular" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox5" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox5"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator5" ControlToValidate="TextBox5" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox6" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox6"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator6" ControlToValidate="TextBox6" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Deficiente" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox7" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox7"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator7" ControlToValidate="TextBox7" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                            <td bgcolor="#EEEEEE">
                                <asp:TextBox ID="TextBox8" runat="server" Width="100px" Font-Names="Calibri" Font-Size="Small"
                                    MaxLength="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox8"
                                    InitialValue="" ForeColor="Red" ValidationGroup="validarCampos">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator8" ControlToValidate="TextBox8" MinimumValue="0"
                                    MaximumValue="5" Type="Integer" EnableClientScript="false" Text="El valor debe estar entre 0 y 5"
                                    runat="server" ForeColor="Red" Font-Names="Calibri" Font-Size="Small"
                                    ValidationGroup="validarCampos" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                    ToolTip="Guardar" ValidationGroup="validarCampos"
                                    OnClick="ImageButton1_Click" />
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
