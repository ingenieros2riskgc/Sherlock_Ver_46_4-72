<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Consolidado.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CuadroMando.Consolidado.Consolidado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<script src="../../../../Scripts/jsapi.js"></script>
<script src="../../../../Scripts/Chart.js"></script>
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    .auto-style1 {
        width: 308px;
    }
    .auto-style3 {
        text-align: "left";
        background-color: #BBBBBB;
        height: 23px;
    }
    .auto-style4 {
        width: 308px;
        height: 23px;
    }
    .auto-style5 {
        height: 23px;
    }

    .auto-style6 {
        width: 100%;
    }

    </style>
<asp:UpdatePanel ID="ConsolidadoBody" runat="server">
    <ContentTemplate>
        
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadConsolidado" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Consolidado" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyFormCPNC" class="ColumnStyle" runat="server">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblTipoReporte" runat="server" Text="Tipo Reporte:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList runat="server" ID="ddlTipoReporte" CssClass="Apariencia" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="Reporte Evolución Perfil del Riesgo Residual" Value="1"></asp:ListItem>
                                    <%--<asp:ListItem Text="Reporte Porcentaje Participación Riesgo" Value="2"></asp:ListItem>--%>
                                    <asp:ListItem Text="Reporte Riesgos por Severidad" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvTipoReporte" runat="server" ControlToValidate="ddlTipoReporte"
                                                ErrorMessage="Debe seleccionar el Tipo del Reporte." ToolTip="Debe seleccionar el Tipo del Reporte."
                                                ValidationGroup="VGconsolidado" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="lblRiesgoGlobal" runat="server" Text="Riesgos globales" CssClass="Apariencia"
                                    ></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlRiesgoGlobal" runat="server" Width="300px" Font-Names="Calibri"
                                    Font-Size="Small" AutoPostBack="True">
                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                    <tr id="CadenaValor">
                                        <td class="RowsText">
                                            <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                                        <td class="auto-style1">
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
                                    <tr id="Macroproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr id="Proceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr id="Subproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td class="auto-style1">
                                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                        <tr>
                        <td class="RowsText">
                            <asp:Label ID="lblAreas" runat="server" Text="Área:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList runat="server" ID="ddlAreas" CssClass="Apariencia" Width="300px"></asp:DropDownList>
                        </td>
                        <td>
                        
                        </td>
                    </tr>
                    <tr runat="server" id="trComparativo">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblComparativo" CssClass="Apariencia" Text="Comparativo"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:CheckBox ID="cbComparativo" runat="server" OnCheckedChanged="cbComparativo_CheckedChanged" AutoPostBack="True" />
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trHistoricoInicial" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblFechaInicial" Text="Fecha Inicial" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td class="auto-style1">
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
                        <td class="auto-style1">
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
                    <tr runat="server" id="trHistoricoInicial1" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="Label2" Text="Fecha Inicial" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td>
                  
                            <table class="auto-style6">
                                <tr>
                                    <td> Año</td>
                                    <td> <asp:DropDownList runat="server" ID="YearIni" CssClass="Apariencia" Width="112px" AutoPostBack="false">
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>                            
                                </asp:DropDownList></td>
                                    <td>Mes</td>
                                    <td><asp:DropDownList runat="server" ID="MesIni" CssClass="Apariencia" Width="112px" AutoPostBack="false">
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="Enero" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="Febrero" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="Marzo" Value="03"></asp:ListItem>
                                <asp:ListItem Text="Abril" Value="04"></asp:ListItem>
                                <asp:ListItem Text="Mayo" Value="05"></asp:ListItem>
                                <asp:ListItem Text="Junio" Value="06"></asp:ListItem>
                                <asp:ListItem Text="Julio" Value="07"></asp:ListItem>
                                <asp:ListItem Text="Agusto" Value="08"></asp:ListItem> 
                                        <asp:ListItem Text="Septiembre" Value="09"></asp:ListItem>   
                                        <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>   
                                        <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>   
                                        <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>   
                                </asp:DropDownList></td>
                                </tr>
                            </table>
                  
                        </td>
                       
                        <td>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="txbFechaInicial"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbFechaInicial"
                                    ErrorMessage="Debe ingresar la fecha inicial." ToolTip="Debe ingresar la fecha inicial."
                                    ValidationGroup="VGconsolidado" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>

                    </tr>
                    <tr runat="server" id="trHistoricoInicial2" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="Label3" Text="Fecha Final" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td class="auto-style1">
                            <table class="auto-style6">
                                <tr>
                                    <td> Año</td>
                                    <td> <asp:DropDownList runat="server" ID="yearFin" CssClass="Apariencia" Width="112px" AutoPostBack="false" >
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>                            
                                </asp:DropDownList></td>
                                    <td>Mes</td>
                                    <td><asp:DropDownList runat="server" ID="MesFin" CssClass="Apariencia" Width="112px" AutoPostBack="false">
                                    <asp:ListItem Text="" Value="---"></asp:ListItem>
                                    <asp:ListItem Text="Enero" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="Febrero" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="Marzo" Value="03"></asp:ListItem>
                                <asp:ListItem Text="Abril" Value="04"></asp:ListItem>
                                <asp:ListItem Text="Mayo" Value="05"></asp:ListItem>
                                <asp:ListItem Text="Junio" Value="06"></asp:ListItem>
                                <asp:ListItem Text="Julio" Value="07"></asp:ListItem>
                                <asp:ListItem Text="Agusto" Value="08"></asp:ListItem> 
                                        <asp:ListItem Text="Septiembre" Value="09"></asp:ListItem>   
                                        <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>   
                                        <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>   
                                        <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>   
                                </asp:DropDownList></td>
                                </tr>
                            </table></td>
                        <td>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="txbFechaInicial"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txbFechaInicial"
                                    ErrorMessage="Debe ingresar la fecha inicial." ToolTip="Debe ingresar la fecha inicial."
                                    ValidationGroup="VGconsolidado" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>

                    </tr>
                    <tr runat="server" id="trPerfilHistoricoInicial" visible="false">
                        <td class="auto-style3">
                            <asp:Label runat="server" ID="lblPefilInicial" Text="Fecha Inicial de la evaluación del perfil" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td class="auto-style4">
                            <asp:TextBox ID="txbPerfilInicial" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td class="auto-style5">
                            <asp:CalendarExtender ID="cePerfilFecha" runat="server" Enabled="true" TargetControlID="txbPerfilInicial"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvPerfilInicial" runat="server" ControlToValidate="txbPerfilInicial"
                                    ErrorMessage="Debe ingresar la fecha inicial." ToolTip="Debe ingresar la fecha inicial."
                                    ValidationGroup="VGconsolidado" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr runat="server" id="trPerfilHistoricoFinal" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblPerfilFinal" Text="Fecha Final de la evaluación del perfil" CssClass="Apariencia">
                            </asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txbPerfilFinal" runat="server" CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="cePerfilFinal" runat="server" Enabled="true" TargetControlID="txbPerfilFinal"
                            Format="yyyy-MM-dd" >
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
                        <td colspan="3">
                            <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Aplicacion/Gears.png"  ValidationGroup="VGconsolidado" ToolTip="Procesar" OnClick="IBprocess_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="ImbCleanFiltros" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbCleanFiltros_Click" />
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
                         <asp:Label ID="txbTextoExcel" runat="server" Text="Para exportar a Excel:"></asp:Label>
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
       <!-- <div>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="true"
                                    ShowHeaderWhenEmpty="True" 
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
        </div>-->
        <div runat="server" id="dvChartsReporteXYZ" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="Chart1" runat="server" Width="550px" OnLoad="Chart1_Load">
            <Series>
            <asp:series Name="Series1"></asp:series>
            </Series>
            <ChartAreas>
            <asp:ChartArea Name="ChartAreaBar">
            </asp:ChartArea>
            </ChartAreas>
            </asp:Chart>
                        
                    </td>
                </tr>
            </table>
            
        </div>
        <div runat="server" id="dvChartsReporteNLK" visible="false">
            <table class="tabla" align="center" width="80%">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GVcriticidad" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="true"
                                    ShowHeaderWhenEmpty="True" 
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Chart ID="Chart3" runat="server" Width="600px">
            <Series>             
            </Series>
                            <Legends>
                                <asp:Legend  Name="criticidad">
                                </asp:Legend>
                            </Legends>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
                        <asp:Chart ID="Chart4" runat="server" Width="550px">
                            <Legends>
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                LegendStyle="Row" />
        </Legends>
            <Series>
            <asp:series Name="Series1"></asp:series>
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
          <div runat="server" id="dvEvoPerfil" visible="false">
              <table class="tabla" align="center" width="80%">
                  <tr align="center" id="trMapaRiesgos" runat="server">
                <td align="center">
                    <table >
                        <tr>
                            <td>
                                <table>
                                    <tr align="center">
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="Frecuencia" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Aplicacion/probabilidad.png" />
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel51" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt51" runat="server" Font-Underline="False" ForeColor="Black"
                                                                            Font-Bold="True" Font-Names="Calibri"  CommandArgument="5,1"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel52" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt52" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,2"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel53" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt53" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,3"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel54" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt54" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,4"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel55" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt55" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="5,5"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel41" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt41" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,1"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel42" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt42" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,2"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel43" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt43" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,3"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel44" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt44" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,4"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel45" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt45" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="4,5"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel31" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt31" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,1"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel32" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt32" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,2"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel33" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt33" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,3"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel34" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt34" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,4"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel35" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt35" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="3,5"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel21" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt21" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,1"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel22" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt22" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,2"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel23" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt23" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,3"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel24" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt24" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,4"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel25" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Red"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt25" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="2,5"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel11" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt11" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,1"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel12" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Green"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt12" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,2"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel13" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Yellow"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt13" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,3"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel14" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt14" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,4"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel15" runat="server" BorderWidth="1px" BorderColor="#996633" BackColor="Orange"
                                                            Visible="true" Width="60px" Height="60px">
                                                            <table width="100%" height="100%">
                                                                <tr align="center" valign="middle">
                                                                    <td>
                                                                        <asp:LinkButton ID="LBt15" runat="server" Font-Underline="false" ForeColor="Black"
                                                                            Font-Bold="true" Font-Names="Calibri"  CommandArgument="1,5"
                                                                            OnCommand="coordenadaRiesgo"
                                                                            ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Aplicacion/impacto.png" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Impacto" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trDivMeses" visible="false">
                                        <td colspan="4" align="center">
                                            <asp:GridView ID="GVmeses" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="true"
                                    ShowHeaderWhenEmpty="True" 
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
              </table>
              </div>
        <div runat="server" id="dvLineChart" visible="false">
            <table class="tabla" align="center" width="80%">
                
                <tr>
                    <td align="center">
            <asp:Chart ID="Chart2" runat="server"  Width="550px">
                            <Legends>
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Efectividad"
                LegendStyle="Row" />
        </Legends>
            <Series>
            <asp:series Name="Series1">
            </asp:series>
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