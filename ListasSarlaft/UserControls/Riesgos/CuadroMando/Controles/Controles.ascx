<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Controles.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CuadroMando.Controles.Controles" %>
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
<asp:UpdatePanel ID="RiesgosBody" runat="server">
    <ContentTemplate>
        
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadControles" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Controles" Font-Bold="False"
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
                                <asp:DropDownList runat="server" ID="ddlTipoReporte" CssClass="Apariencia" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="Reporte Efectividad de Controles" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Reporte Controles por Responsable de Calificación" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvTipoReporte" runat="server" ControlToValidate="ddlTipoReporte"
                                                ErrorMessage="Debe seleccionar el Tipo del Reporte." ToolTip="Debe seleccionar el Tipo del Reporte."
                                                ValidationGroup="VGconsolidado" ForeColor="Red" InitialValue="---">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr runat="server" id="trRiesgoInherente" visible="false">
                        <td class="RowsText">
                                <asp:Label ID="lblRiesgoInherente" runat="server" Text="Riesgo Inherente:" CssClass="Apariencia"></asp:Label>
                            </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlRiesgoInherente" CssClass="Apariencia" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="Bajo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Moderado" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Alto" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Extremo" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr runat="server" id="trEfectividad" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblEfectividades">Efectividades de Control</asp:Label>
                        </td>
                        <td align="center">
                            <asp:CheckBoxList ID="cblEfectividades" runat="server">
                                <asp:ListItem Text="Excelente" Value="Excelente"></asp:ListItem>
                                <asp:ListItem Text="Bueno" Value="Bueno"></asp:ListItem>
                                <asp:ListItem Text="Regular" Value="Regular"></asp:ListItem>
                                <asp:ListItem Text="Deficiente" Value="Deficiente"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trJerarquia" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblJerarquia">Jerarquia Organizacional</asp:Label>
                        </td>
                        <td align="center">
                            <asp:CheckBoxList ID="cbJerarquia" runat="server">
                            </asp:CheckBoxList>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trRiesgoGlobal" visible="false">
                            <td class="RowsText">
                                <asp:Label ID="lblRiesgoGlobal" runat="server" Text="Riesgos globales" CssClass="Apariencia"
                                    ></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRiesgoGlobal" runat="server" Width="300px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="ddlRiesgoGlobal_SelectedIndexChanged">
                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                    <tr id="CadenaValor" runat="server" visible="false">
                                        <td class="RowsText">
                                            <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True"
                                                DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                                OnSelectedIndexChanged="ddlCadenaValor_SelectedIndexChanged">
                                                
                                            </asp:DropDownList>
                                            </td>
                        <td>
                                                
                                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox>
                                            <asp:Label ID="LtexProceso" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Macroproceso" runat="server" visible="false">
                                        <td class="RowsText">
                                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr id="Proceso" runat="server" visible="false">
                                        <td class="RowsText">
                                            <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr id="Subproceso" runat="server" visible="false">
                                        <td class="RowsText">
                                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                    <tr runat="server" id="trClasificacionGeneral" visible="false">
                            <td class="RowsText">
                                <asp:Label ID="Label158" runat="server" Text="Clasificación general" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlClasificacionGeneral" runat="server" Width="300px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True" >
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    <tr runat="server" id="trFactor" visible="false">
                            <td class="RowsText">
                                <asp:Label ID="Label17" runat="server" Text="Factor de Riesgo LA/FT" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlFactorRiesgo" runat="server" Width="300px" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    <tr runat="server" id="trPlanEstrategico" visible="false">
                                                                <td class="RowsText">
                                                                    <asp:Label ID="Label85" runat="server" CssClass="Apariencia" Text="Plan Estratégico:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDLPlanEstrategico" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="300px" AutoPostBack="True" OnSelectedIndexChanged="DDLPlanEstrategico_SelectedIndexChanged">
                                                                        <asp:ListItem Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="trObjetivos" visible="false">
                                                                <td class="RowsText">
                                                                    <asp:Label ID="Label185" runat="server" CssClass="Apariencia" Text="Objetivo Estratégico"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDLObjetivoEstrategico" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                                        Width="300px">
                                                                        <asp:ListItem Value="---">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                        <tr runat="server" id="trArea" visible="false">
                        <td class="RowsText">
                            <asp:Label ID="lblAreas" runat="server" Text="Área:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlAreas" CssClass="Apariencia" Width="300px"></asp:DropDownList>
                        </td>
                        <td>
                        
                        </td>
                    </tr>
                    <tr runat="server" id="trComparativo" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblComparativo" CssClass="Apariencia" Text="Comparativo"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbComparativo" runat="server" OnCheckedChanged="cbComparativo_CheckedChanged" AutoPostBack="True" />
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trHistoricoInicial" visible="false">
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
                        <asp:RequiredFieldValidator ID="rfvFechaInicial" runat="server" ControlToValidate="txbFechaInicial"
                                    ErrorMessage="Debe ingresar la fecha inicial." ToolTip="Debe ingresar la fecha inicial."
                                    ValidationGroup="VGconsolidado" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr runat="server" id="trHistoricoFinal" visible="false">
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
                        <asp:RequiredFieldValidator ID="rfvFechaFinal" runat="server" ControlToValidate="txbFechaFinal"
                                    ErrorMessage="Debe ingresar la fecha final." ToolTip="Debe ingresar la fecha final."
                                    ValidationGroup="VGconsolidado" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr runat="server" id="trPerfilHistoricoInicial" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblPefilInicial" Text="Fecha Inicial de la Evoluación del perfil" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbPerfilInicial" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="cePerfilFecha" runat="server" Enabled="true" TargetControlID="txbPerfilInicial"
                            Format="yyyy-MM" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvPerfilInicial" runat="server" ControlToValidate="txbPerfilInicial"
                                    ErrorMessage="Debe ingresar la fecha inicial." ToolTip="Debe ingresar la fecha inicial."
                                    ValidationGroup="VGconsolidado" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr runat="server" id="trPerfilHistoricoFinal" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblPerfilFinal" Text="Fecha Final de la Evoluación del perfil" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txbPerfilFinal" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="cePerfilFinal" runat="server" Enabled="true" TargetControlID="txbPerfilFinal"
                            Format="yyyy-MM" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvPerfilFinal" runat="server" ControlToValidate="txbPerfilFinal"
                                    ErrorMessage="Debe ingresar la fecha final." ToolTip="Debe ingresar la fecha final."
                                    ValidationGroup="VGconsolidado" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="RowsText">
                            <asp:Label runat="server" ID="lblTextoProcess" Text="Haz click en el botón para generar el Reporte:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Aplicacion/Gears.png"  ValidationGroup="VGconsolidado" ToolTip="Insertar" OnClick="IBprocess_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="ImbCleanFiltro" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbCleanFiltro_Click" />
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
                        <asp:ImageButton ID="ImButtonPDFexport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexport_Click" />
                    </td>
                    <td>
                            Para exportar a Excel:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonExcelExport" runat="server" ImageUrl="~/Imagenes/Icons/excel.png" OnClick="ImButtonExcelExport_Click"/>
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
        <div runat="server" id="dvTablaEfectividad" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GVefectividad" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True" OnRowCommand="GVefectividad_RowCommand" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Efectividad" DataField="Efectividad" />
                                        <asp:BoundField HeaderText="Cantidad Control" DataField="CantidadControl" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar"
                                            CommandName="Modificar" />
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
        <div runat="server" id="dvTableJerarquias" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                       <asp:GridView ID="GVjerarquias" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True" OnRowCommand="GVjerarquias_RowCommand" PageSize="50" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                                        <asp:BoundField HeaderText="Jerarquia" DataField="Jerarquia" />
                                        <asp:BoundField HeaderText="Deficiente" DataField="Deficiente" />
                                        <asp:BoundField HeaderText="Regular" DataField="Regular" />
                                        <asp:BoundField HeaderText="Bueno" DataField="Bueno" />
                                        <asp:BoundField HeaderText="Excelente" DataField="Excelente" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar" HeaderText="Seleccionar"
                                            CommandName="Modificar" ItemStyle-HorizontalAlign="Center"/>
                                        
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
        <div runat="server" id="dvGraficosEficacia" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="ChartEficacia" runat="server" Width="850px">
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
        <div runat="server" id="dvJerarquias" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="ChartJerarquias" runat="server" Width="850px">
                            <Titles>
        <asp:Title  Name="Items" />
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
    </Legends>
            <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            </ChartAreas>
            </asp:Chart>
                    </td>
                </tr>
            </table>
            
        </div>
        <div runat="server" id="dvResponsables" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="ChartResponsable" runat="server" Width="1250px" Height="600px">
                            <Titles>
        <asp:Title  Name="Items" />
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
    </Legends>
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