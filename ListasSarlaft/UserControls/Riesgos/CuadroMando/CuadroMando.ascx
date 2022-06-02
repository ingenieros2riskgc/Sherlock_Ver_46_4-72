<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CuadroMando.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CuadroMando.CuadroMando" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<%@ Register Src="~/UserControls/Riesgos/CuadroMando/Consolidado/Consolidado.ascx" TagPrefix="ContentConsolidado" TagName="Consolidado" %>
<%@ Register Src="~/UserControls/Riesgos/CuadroMando/Riesgos/Riesgos.ascx" TagPrefix="ContentRiesgos" TagName="Riesgos" %>
<%@ Register Src="~/UserControls/Riesgos/CuadroMando/Controles/Controles.ascx" TagPrefix="ContentControles" TagName="Controles" %>
<%@ Register Src="~/UserControls/Riesgos/CuadroMando/Eventos/Eventos.ascx" TagPrefix="ContentEventos" TagName="Eventos" %>
<%@ Register Src="~/UserControls/Riesgos/CuadroMando/Indicadores/Indicadores.ascx" TagPrefix="ContentIndicadores" TagName="Indicadores" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="CMbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCM" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Cuadro de Mando de Riesgos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="dvButtonsCuadro" class="TableContains" runat="server">
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImbConsolidado" runat="server" ImageUrl="~/Imagenes/Icons/Consoliado.png" Width="75px" Height="75px" OnClick="ImbConsolidado_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RowsText" align="center">
                                        <asp:Label runat="server" ID="lblConsolidado" Text="Consolidado"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImbRiesgo" runat="server" ImageUrl="~/Imagenes/Icons/Riesgos.png" Width="95px" Height="75px" OnClick="ImbRiesgo_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RowsText" align="center">
                                        <asp:Label runat="server" ID="lblRiesgos" Text="Riesgos"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImbControles" runat="server" ImageUrl="~/Imagenes/Icons/Controles.png" Width="95px" Height="75px" OnClick="ImbControles_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RowsText" align="center">
                                        <asp:Label runat="server" ID="lblControles" Text="Controles"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImbEventos" runat="server" ImageUrl="~/Imagenes/Icons/Eventos.png" Width="95px" Height="75px" OnClick="ImbEventos_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RowsText" align="center">
                                        <asp:Label runat="server" ID="lblEventos" Text="Eventos"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImbIndicador" runat="server" ImageUrl="~/Imagenes/Icons/Indicadores.png" Width="95px" Height="75px" OnClick="ImbIndicador_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RowsText" align="center">
                                        <asp:Label runat="server" ID="lblIndicador" Text="Indicadores"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
            </div>
        <div runat="server" id="dvConsolidado" visible="false">
            <ContentConsolidado:Consolidado ID="ContentConsolidado1" runat="server" />
        </div>
        <div runat="server" id="dvRiesgos" visible="false">
            <ContentRiesgos:Riesgos ID="ContentRiesgos1" runat="server" />
        </div>
        <div runat="server" id="dvControles" visible="false">
            <ContentControles:Controles ID="ContentControles1" runat="server" />
        </div>
        <div runat="server" id="dvEventos" visible="false">
            <ContentEventos:Eventos ID="ContentEventos1" runat="server" />
        </div>
        <div runat="server" id="dvIndicadores" visible="false">
            <ContentIndicadores:Indicadores ID="ContentIndicadores1" runat="server" />
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>