<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SenalAlertaComparativa.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Perfilamiento.SenalAlertaComparativa" %>
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

    .scrollingControlContainer {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .style1 {
        width: 1200px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td class="style1">
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Configurar Consulta"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <caption>Archivo 1</caption>
                        <td style="width: 20%">
                            <table>
                                <tr>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="lArchivo1" runat="server" Text="Nombre archivo 1:" Font-Names="Calibri"
                                            Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:DropDownList ID="ddlArchivo1" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlArchivo1"
                                            InitialValue="" ErrorMessage="*" ValidationGroup="comparacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20%">
                            <table>
                                <tr>
                                    <td bgcolor="#BBBBBB">
                                        <asp:Label ID="lVariable1" runat="server" Text="Variable:" Font-Names="Calibri"
                                            Font-Size="Small"></asp:Label>
                                    </td>
                                    <td bgcolor="#EEEEEE">
                                        <asp:DropDownList ID="ddlVariable1" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlVariable1"
                                            InitialValue="" ErrorMessage="*" ValidationGroup="comparacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td style="width: 20%" colspan="2">
                    <table>
                        <caption>Seleccione el operador</caption>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Operador Relacional:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:DropDownList ID="ddlOperador" runat="server">
                                    <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                    <asp:ListItem Value=">">Mayor que</asp:ListItem>
                                    <asp:ListItem Value="<">Menor que</asp:ListItem>
                                    <asp:ListItem Value=">=">Mayor igual que</asp:ListItem>
                                    <asp:ListItem Value="<=">Menor igual que</asp:ListItem>
                                    <asp:ListItem Value="LIKE">Igual que</asp:ListItem>
                                    <asp:ListItem Value="<>">Diferente que</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlOperador"
                                    InitialValue="" ErrorMessage="*" ValidationGroup="comparacion" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table>
                        <caption>Archivo 2</caption>
                        <tr>
                            <td style="width: 20%">
                                <table>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label2" runat="server" Text="Nombre archivo 2:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="ddlArchivo2" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlArchivo2"
                                                InitialValue="" ErrorMessage="*" ValidationGroup="comparacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20%">
                                <table>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" Text="Variable:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="ddlVariable2" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlVariable2"
                                                InitialValue="" ErrorMessage="*" ValidationGroup="comparacion" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="text-align: center">
            <asp:ImageButton ID="ibtnAnalizar" runat="server" ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png" OnClick="ibtnAnalizar_Click" ValidationGroup="comparacion" />
            <asp:ImageButton ID="ibtnLimpiar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="ibtnLimpiar_Click" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
