<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Indicadores.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.CuadroMando.Indicadores.Indicadores" %>
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
        <div class="TituloLabel" id="HeadIndicadores" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Indicadores" Font-Bold="False"
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
                                    <asp:ListItem Text="Reporte Indicadores asociados a Riegos" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Reporte Indicadores por Proceso o Responsable" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvTipoReporte" runat="server" ControlToValidate="ddlTipoReporte"
                                                ErrorMessage="Debe seleccionar el Tipo del Reporte." ToolTip="Debe seleccionar el Tipo del Reporte."
                                                ValidationGroup="VGconsolidado" ForeColor="Red" InitialValue="---">*</asp:RequiredFieldValidator>
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
                    <tr runat="server" id="trEfectividad" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblEfectividades">Nivel de Riesgo:</asp:Label>
                        </td>
                        <td align="center">
                            <asp:CheckBoxList ID="cblEfectividades" runat="server">
                                <asp:ListItem Text="Extremo" Value="Extremo"></asp:ListItem>
                                <asp:ListItem Text="Alto" Value="Alto"></asp:ListItem>
                                <asp:ListItem Text="Moderado" Value="Moderado"></asp:ListItem>
                                <asp:ListItem Text="Bajo" Value="Bajo"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trJerarquia" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblJerarquia">Jerarquia Organizacional</asp:Label>
                        </td>
                        <td align="center">
                            <asp:CheckBoxList ID="cbJerarquia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbJerarquia_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trJerarquia2" visible="false">
                        <td class="RowsText">
                            <asp:Label runat="server" ID="lblJerarquia2">Jerarquia Organizacional</asp:Label>
                        </td>
                        <td align="center">
                             <asp:DropDownList ID="ddlJerarquiaO2" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True"
                                                DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                                OnSelectedIndexChanged="cbJerarquia_SelectedIndexChanged">
                                                
                             </asp:DropDownList>
                     
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td colspan="3" class="RowsText">
                            <asp:Label runat="server" ID="lblTextoProcess" Text="Haz click en el botón para generar el Reporte:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Aplicacion/Gears.png"  ValidationGroup="VGconsolidado" ToolTip="Insertar" OnClick="IBprocess_Click"  />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:ImageButton ID="ImbClean" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImbClean_Click"  />
                        </td>
                    </tr>
                    </table>
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
        </div>
        <div runat="server" id="dvTablaIndicadorRiesgo" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:GridView ID="GVindicadorRiesgo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Justify" Font-Names="Calibri" Font-Size="Small"
                                     AllowPaging="True" OnRowCommand="GVindicadorRiesgo_RowCommand" OnRowCancelingEdit="GVindicadorRiesgo_RowCancelingEdit" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Riesgo" DataField="Riesgo" />
                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
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
        <div runat="server" id="dvGraficoIndicadorRiesgo" visible="false">
            <table class="tabla" align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Chart ID="ChartIndicadorRiesgo" runat="server" Width="850px">
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