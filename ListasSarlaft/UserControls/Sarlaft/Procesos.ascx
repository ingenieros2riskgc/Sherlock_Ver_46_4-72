<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Procesos.ascx.cs" Inherits="ListasSarlaft.UserControls.Procesos" %>
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
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Loading Page..." Font-Names="Calibri"
                                Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label7" runat="server" ForeColor="White" Text="Procesos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel1" runat="server" BorderWidth="2px" BorderColor="Black">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="1. ALISTAR INFORMACIÓN" Font-Bold="True"
                                                    Font-Names="Calibri" Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="Button1" runat="server" Text="Finalizar cargue" OnClick="Button1_Click"
                                                    Font-Names="Calibri" Font-Size="Small" Width="180px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" BorderWidth="2px" BorderColor="Black">
                                    <table>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="2. INSTRUMENTOS SARLAFT" Font-Bold="True"
                                                    Font-Names="Calibri" Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="2.1 ANÁLISIS DE OPERACIONES" Font-Names="Calibri"
                                                                Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Button ID="Button2" runat="server" Text="Analizar información" OnClick="Button2_Click"
                                                                Font-Names="Calibri" Font-Size="Small" Width="180px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="2.2 ANÁLISIS DE CLIENTES CONTRA LISTAS RESTRICTIVAS"
                                                                Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle">
                                                            <asp:Button ID="Button3" runat="server" Text="Analizar información" OnClick="Button3_Click"
                                                                Font-Names="Calibri" Font-Size="Small" Width="180px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" BorderWidth="2px" BorderColor="Black">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="3. REINICIAR PROCESO DE CARGUE" Font-Bold="True"
                                                    Font-Names="Calibri" Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="Button4" runat="server" Text="Reiniciar" Font-Names="Calibri" Font-Size="Small"
                                                    Width="180px" OnClick="Button4_Click" />
                                            </td>
                                        </tr>
                                    </table>
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
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
        <asp:PostBackTrigger ControlID="Button2" />
        <asp:PostBackTrigger ControlID="Button3" />
        <asp:PostBackTrigger ControlID="Button4" />
    </Triggers>
</asp:UpdatePanel>
