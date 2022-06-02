<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Eventos.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CuadroMando.Eventos.Eventos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<script src="../../../../Scripts/jsapi.js"></script>
<script src="../../../../Scripts/Chart.js"></script>
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="EventosBody" runat="server">
    <ContentTemplate>
        
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadEventos" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Eventos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyFormCPNC" class="ColumnStyle" runat="server">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblTipoReporte" runat="server" Text="Tipo Reporte:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTipoReporte" CssClass="Apariencia" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged" >
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="Reporte Estado actual de Eventos" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Reporte Eventos Consolidado" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvTipoReporte" runat="server" ControlToValidate="ddlTipoReporte"
                                                ErrorMessage="Debe seleccionar el Tipo del Reporte." ToolTip="Debe seleccionar el Tipo del Reporte."
                                                ValidationGroup="VGconsolidado" ForeColor="Red" InitialValue="---">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    
                    <tr runat="server" id="trInicial">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblFechaInicial" Text="Fecha Inicial" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbFechaInicial" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="ceFechaInicial" runat="server" Enabled="true" TargetControlID="txbFechaInicial"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                            </td>
                    </tr>
                    <tr runat="server" id="trFinal">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblFechaFinal" Text="Fecha Final" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbFechaFinal" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="ceFechaFianl" runat="server" Enabled="true" TargetControlID="txbFechaFinal"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                            </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="RowsText">
                            <asp:Label runat="server" ID="lblTextoProcess" Text="Haz click en el botón para generar el Reporte:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Aplicacion/Gears.png"  ValidationGroup="VGconsolidado" ToolTip="Insertar" OnClick="IBprocess_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="ImbClean" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbClean_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
        </div>
        <div id="Dbutton" runat="server" visible="false" class="ColumnStyle">
            <Table id="Tbuttons" class="tabla" align="center" width="25%">
                <tr align="center">
                        <td>
                            Para exportar a PDF:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonPDFexport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexport_Click"  />
                    </td>
                    <td>
                            Para exportar a Excel:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click" />
                    </td>
                    <td>
                            Limpiar:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImbCancel" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbCancel_Click" />
                    </td>
                    </tr>
                </Table>
        </div>
        <div runat="server" id="dvTablaEstadosEventos" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GVestadosEventos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Estado" DataField="Estado" />
                                        <asp:BoundField HeaderText="Cantidad Eventos" DataField="Cantidad" />
                                        <asp:BoundField HeaderText="Participación" DataField="Participacion" HtmlEncode="false" HtmlEncodeFormatString="false" />
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
                </table>
            </div>
        <div runat="server" id="dvGraficosEstadosEventos" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="ChartEstadosEventos" runat="server" Width="850px">
                            <Titles>
        <asp:Title  Name="Items" />
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
    </Legends>
    <Series>
        <asp:Series Name="Default" />
    </Series>
            <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            </ChartAreas>
            </asp:Chart>
                    </td>
                </tr>
            </table>
            
        </div>
        <div runat="server" id="dvGraficoEventosConsolidado" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="ChartEventosConsolidado" runat="server" Width="850px">
                            <Titles>
        <asp:Title  Name="Items" />
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
    </Legends>
    <Series>
        <asp:Series Name="Default" />
    </Series>
            <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            </ChartAreas>
            </asp:Chart>
                    </td>
                </tr>
            </table>
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>