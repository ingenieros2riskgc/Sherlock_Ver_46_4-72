<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="ListasSarlaft.UserControls.Reporte" %>
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
<script type="text/javascript">
    function popUp(pagina) {
        hiddden = open(pagina, 'NewWindow', 'top=0,left=0,width=1280,height=1024,status=yes,resizable=yes,scrollbars=yes');
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Detalle del registro"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <table>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Id Lista" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Tipo Documento" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Documento Identidad" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Nombre Completo" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Id Tipo Lista" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="Nombre Tipo Lista" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label14" runat="server" Text="Fuente Consulta" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label16" runat="server" Text="Tipo Persona" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label17" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label18" runat="server" Text="Alias" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label19" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label20" runat="server" Text="Delito" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label21" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label22" runat="server" Text="Zona" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label23" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label26" runat="server" Text="Link" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label27" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label24" runat="server" Text="Otra Información" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Estado" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label28" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
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
