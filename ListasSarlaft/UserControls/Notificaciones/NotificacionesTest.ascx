<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotificacionesTest.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Notificaciones.NotificacionesTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .gridViewHeader a:link {
        text-decoration: none;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .ajax__html_editor_extender_texteditor {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlDependencia2" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr>
                    <td align="center">
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                            DisplayAfter="0">
                            <ProgressTemplate>
                                <div id="Background">
                                </div>
                                <div id="Progress">
                                    <asp:Label ID="Lbl11" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                        Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:Image ID="Img11" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox"
            BehaviorID="mypopup" Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>
        <table align="center" width="50%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Configuración notificaciones"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left" id="Tr2" runat="server">
                <td bgcolor="#EEEEEE">
                    <table class="tabla" width="100%">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="enableSsl:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Style="font-family: Calibri; font-size: medium"
                                    TextAlign="Left" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">true</asp:ListItem>
                                    <asp:ListItem Value="1">false</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>

                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="host:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td3" runat="server">
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="Apariencia" Width="600px"
                                    Height="15px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label7" runat="server" Text="port:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td4" runat="server">
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="Apariencia" Width="600px"
                                    Height="15px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="userName:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="600px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="password:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Width="600px" CssClass="Apariencia" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Insertar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table align="center" width="50%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Validar notificaciones"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left" id="filaDetalle" runat="server">
                <td bgcolor="#EEEEEE">
                    <table class="tabla" width="100%">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Para:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td1" runat="server">

                                <asp:TextBox ID="txtCopia1" runat="server" CssClass="Apariencia" Width="600px"
                                    Height="15px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Asunto:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAsunto" runat="server" Width="600px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Cuerpo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCuerpo" runat="server" Width="600px" CssClass="Apariencia" Columns="100"
                                    Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImgBtnSenMail" runat="server" ImageUrl="~/Imagenes/Icons/email.png"
                                                ToolTip="Enviar mensaje test" Width="30px" />
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
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
