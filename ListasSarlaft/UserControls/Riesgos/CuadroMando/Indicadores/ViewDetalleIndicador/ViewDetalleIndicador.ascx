<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewDetalleIndicador.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CuadroMando.Indicadores.ViewDetalleIndicador.ViewDetalleIndicador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<meta http-equiv="Content-Security-Policy" content="upgrade-insecure-requests">
<link href="../../../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />

<script src="../../../../../Scripts/DownloadJquery.js"></script>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../../../../../Imagenes/Icons/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../../../../../Imagenes/Icons/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="ConsolidadoBody" runat="server">
    <ContentTemplate>
        
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadConsolidado" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Detalle de Indicador" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div>
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td bgcolor="#BBBBBB">
                                            <asp:Label ID="lblEfectividad" runat="server" Text="Severidad:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:Label runat="server" ID="lblRiesgo"></asp:Label>
                                        </td>
                    </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="ConsolidadoBody"
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
                <tr>
                    <td align="center" colspan="2">
                        <asp:GridView ID="GVindicadores" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                            DataKeyNames="intIdRiesgoAsociado"
                                     AllowPaging="True" OnPageIndexChanging="GVindicadores_PageIndexChanging" OnRowDataBound="GVindicadores_RowDataBound" OnPreRender="GVindicadores_PreRender"
                                    >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                    <asp:BoundField DataField="intIdRiesgoIndicador" HeaderText="Código" SortExpression="intIdRiesgoIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strNombreIndicador" HeaderText="Nombre Indicador" SortExpression="strNombreIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strObjetivoIndicador" HeaderText="Objetivo Indicador" SortExpression="strObjetivoIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText ="Mostrar Riesgos" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img alt = "" style="cursor: pointer" src="../../../../../Imagenes/Icons/plus.png"/>
                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                <asp:GridView ID="gvRiesgos" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Codigo" HeaderText="Código" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Nombre" HeaderText="Nombre Riesgo" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CadenaValor" HeaderText="Cadena de Valor" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Macroproceso" HeaderText="Macroproceso" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Proceso" HeaderText="Proceso" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Subproceso" HeaderText="Subproceso" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rango Cumplimiento" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strColor" runat="server" Text='<% # Bind("strColor")%>'></asp:Label>
                                            <asp:ImageButton runat="server" ID="ImbRango" ImageUrl="~/Imagenes/Aplicacion/Arriba.png" Width="20px" Height="20px"  CommandName="Activar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                        </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                        <asp:GridView ID="GVindicadoresResponsables" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                            DataKeyNames="intIdRiesgoAsociado"
                                     AllowPaging="True" OnPageIndexChanging="GVindicadoresResponsables_PageIndexChanging" OnRowDataBound="GVindicadoresResponsables_RowDataBound" OnPreRender="GVindicadoresResponsables_PreRender"
                                    >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                    <asp:BoundField DataField="intIdRiesgoIndicador" HeaderText="Código" SortExpression="intIdRiesgoIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strNombreIndicador" HeaderText="Nombre Indicador" SortExpression="strNombreIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strObjetivoIndicador" HeaderText="Objetivo Indicador" SortExpression="strObjetivoIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strResponsable" HeaderText="Responsable" SortExpression="strResponsable" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText ="Mostrar Riesgos" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img alt = "" style="cursor: pointer" src="../../../../../Imagenes/Icons/plus.png"/>
                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                <asp:GridView ID="gvRiesgos" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Codigo" HeaderText="Código" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Nombre" HeaderText="Nombre Riesgo" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CadenaValor" HeaderText="Cadena de Valor" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Macroproceso" HeaderText="Macroproceso" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Proceso" HeaderText="Proceso" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Subproceso" HeaderText="Subproceso" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rango Cumplimiento" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                            <asp:Label ID="strColor" runat="server" Text='<% # Bind("strColor")%>'></asp:Label>
                                            <asp:ImageButton runat="server" ID="ImbRango" ImageUrl="~/Imagenes/Aplicacion/Arriba.png" Width="20px" Height="20px"  CommandName="Activar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                        </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#BBBBBB">
                                            <asp:Label ID="lblCantidad" runat="server" Text="Cantidad Indicadores:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:Label runat="server" ID="lblCantRiesgo"></asp:Label>
                                        </td>
                    </tr>
            </table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>