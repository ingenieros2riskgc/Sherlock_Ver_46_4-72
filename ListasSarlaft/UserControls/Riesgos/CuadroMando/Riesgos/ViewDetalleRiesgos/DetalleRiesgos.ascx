<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetalleRiesgos.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CuadroMando.Riesgos.ViewDetalleRiesgos.DetalleRiesgos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
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
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Detalle de riesgos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div>
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td bgcolor="#BBBBBB">
                                            <asp:Label ID="lblRiesgoInherente" runat="server" Text="Riesgo Residual:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
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
                        <asp:GridView ID="GVriesgos" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                            DataKeyNames="IdRiesgo"
                                     AllowPaging="True" OnPageIndexChanging="GVriesgos_PageIndexChanging" OnRowDataBound="GVriesgos_RowDataBound" 
                                    >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                    <asp:BoundField DataField="CodigoRiesgo" HeaderText="Código" SortExpression="CodigoRiesgo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="NombreRiesgo" HeaderText="Nombre Riesgo" SortExpression="NombreRiesgo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="IdProbabilidadResidual" HeaderText="Frecuencia Residual" SortExpression="IdProbabilidadResidual" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="IdImpactoResidual" HeaderText="Impacto Residual" SortExpression="IdImpactoResidual" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="RiesgoInherente" HeaderText="Riesgo Residual" SortExpression="RiesgoInherente" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="CantidadControles" HeaderText="Cantidad Controles" SortExpression="CantidadControles" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <img alt = "" style="cursor: pointer" src="../../../../../Imagenes/Icons/plus.png"/>
                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                <asp:GridView ID="gvControles" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CodigoControl" HeaderText="Código" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="NombreControl" HeaderText="Control" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="DescripcionControl" HeaderText="Descripción" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="NombreEscala" HeaderText="Efectividad" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="IdMitiga" HeaderText="Reduce" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="NombreHijo" HeaderText="Responsable Calificación" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="CantidadEventos" HeaderText="Cantidad Eventos" SortExpression="CantidadEventos" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                            <img alt = "" style="cursor: pointer" src="../../../../../Imagenes/Icons/plus.png"/>
                                            <asp:Panel ID="pnlEventos" runat="server" Style="display: none">
                                                <asp:GridView ID="gvEventos" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CodigoEvento" HeaderText="Código" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="DescripcionEvento" HeaderText="Descripción" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="NombreTipoPerdidaEvento" HeaderText="Tipo Perdida" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="CadenaValor" HeaderText="Cadena de Valor" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Macroproceso" HeaderText="Macroproceso" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Proceso" HeaderText="Proceso" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Subproceso" HeaderText="Subproceso" />

                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                            </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="CausasSinControl" HeaderText="Causas Sin Control" SortExpression="CausasSinControl" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        
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
                                            <asp:Label ID="lblCantidad" runat="server" Text="Cantidad Riesgos:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:Label runat="server" ID="lblCantRiesgo"></asp:Label>
                                        </td>
                    </tr>
            </table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>