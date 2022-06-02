<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamColores.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Parametrizacion.ParamColores" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />

        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" 
                            Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center" >
                        <asp:Image ID="imgInfo" runat="server" 
                            ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png"/>
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" 
                            Font-Size="11px"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox" BehaviorId="mypopup" 
            Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"  DropShadow="true" >
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" style="display:none"/>
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>

        <table align="center" width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Colores Gestión Auditoría"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" id="filaDetalle" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <table align="center" width="200px" >
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            </td>
                            <td align="center" bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Valor Mínimo" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                            </td>
                            <td align="center" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Valor Máximo" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="Green">
                                <asp:Label ID="Label5" runat="server" Text="Verde:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt1" runat="server" Width="50px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ActualizaColor"
                                    ControlToValidate="txt1" Display="Dynamic" ForeColor="Red" InitialValue="" Font-Size="Small" Font-Names="Calibri">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txt2" runat="server" Width="50px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ActualizaColor"
                                    ControlToValidate="txt2" Display="Dynamic" ForeColor="Red" InitialValue="" Font-Size="Small" Font-Names="Calibri">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="Yellow">
                                <asp:Label ID="Label1" runat="server" Text="Amarillo:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt3" runat="server" Width="50px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ActualizaColor"
                                    ControlToValidate="txt3" Display="Dynamic" ForeColor="Red" InitialValue="" Font-Size="Small" Font-Names="Calibri">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txt4" runat="server" Width="50px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="ActualizaColor"
                                    ControlToValidate="txt4" Display="Dynamic" ForeColor="Red" InitialValue="" Font-Size="Small" Font-Names="Calibri">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="Red">
                                <asp:Label ID="Label4" runat="server" Text="Rojo:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txt5" runat="server" Width="50px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="ActualizaColor"
                                    ControlToValidate="txt5" Display="Dynamic" ForeColor="Red" InitialValue="" Font-Size="Small" Font-Names="Calibri">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txt6" runat="server" Width="50px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="ActualizaColor"
                                    ControlToValidate="txt6" Display="Dynamic" ForeColor="Red" InitialValue="" Font-Size="Small" Font-Names="Calibri">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ToolTip="Guardar" ValidationGroup="ActualizaColor" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
