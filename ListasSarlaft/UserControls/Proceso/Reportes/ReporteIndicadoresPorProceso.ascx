<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteIndicadoresPorProceso.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Reportes.ReporteIndicadoresPorProceso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="RIPPbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadRIPP" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Reporte Indicadores Por Proceso" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="form" class="TableContains" runat="server">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lfecha" runat="server" Text="Fecha Inicial:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXfechaInicial" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="CEfechaConsulta" runat="server" Enabled="true" TargetControlID="TXfechaInicial"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaConsul" runat="server" ControlToValidate="TXfechaInicial"
                                    ErrorMessage="Debe ingresar la fecha inicial." ToolTip="Debe ingresar la fecha inicial."
                                    ValidationGroup="IndicadorPorProceso" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="RowsText">
                                <asp:Label ID="LfechaFinal" runat="server" Text="Fecha Final:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXfechaFinal" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="CEfechaFinal" runat="server" Enabled="true" TargetControlID="TXfechaFinal"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFechaFinal" runat="server" ControlToValidate="TXfechaFinal"
                                    ErrorMessage="Debe ingresar la fecha final." ToolTip="Debe ingresar la fecha final."
                                    ValidationGroup="IndicadorPorProceso" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>    
                    
                        <tr id="CadenaValor">
                            <td class="RowsText" colspan="3">
                                <asp:Label ID="Label19" runat="server" Text="Cadena de Valor:" CssClass="Apariencia"></asp:Label></td>
                            <td colspan="2" align="center">
                                <asp:DropDownList ID="ddlCadenaValor" runat="server" Width="300px"
                                    CssClass="Apariencia" AutoPostBack="True"
                                    DataTextField="NombreCadenaValor" DataValueField="IdCadenaValor"
                                    OnSelectedIndexChanged="ddlCadenaValor_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                    ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                    ValidationGroup="IndicadorPorProceso" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Macroproceso">
                            <td class="RowsText" colspan="3">
                                <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                            <td colspan="2" align="center">
                                <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                    OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                    ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                    ValidationGroup="IndicadorPorProceso" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Proceso">
                            <td class="RowsText" colspan="3">
                                <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                            <td colspan="2" align="center">
                                <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                    ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                    ValidationGroup="IndicadorPorProceso" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Subproceso">
                            <td class="RowsText" colspan="3">
                                <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                            <td colspan="2" align="center">
                                <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                    CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                </asp:DropDownList>
                            </td>
                            <td><asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                    ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                    ValidationGroup="IndicadorPorProceso" ForeColor="Red" >*</asp:RequiredFieldValidator></td>
                        </tr>                      
                    <tr>
                        <td colspan="6" align="center">
                            <asp:ImageButton ID="IBsearch" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="IndicadorPorProceso" ToolTip="Buscar" OnClick="IBsearch_Click"/>
            
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                        </td>
                    </tr>

                    </table>
            </div>
        <div id="BodyGridRIPP" class="ColumnStyle" runat="server" visible="false">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVIndicadoresPorProceso" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" 
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPreRender="GVIndicadoresPorProceso_PreRender">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Id Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 50px">
                                                    <asp:Label ID="intIdIndicador" runat="server" Text='<% # Bind("intIdIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Nombre Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strNombreIndicador" runat="server" Text='<% # Bind("strNombreIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Proceso Indicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strProcesoIndicador" runat="server" Text='<% # Bind("strProcesoIndicador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Meta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 50px">
                                                    <asp:Label ID="intMeta" runat="server" Text='<% # Bind("intMeta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        
                                      
                                        <asp:TemplateField HeaderText="Periodo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 350px">
                                                    <asp:Label ID="strArrayPeriodo" runat="server" Text='<% # Bind("strArrayPeriodo")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cumplimiento" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 350px">
                                                    <asp:Label ID="strArrayResultado" runat="server" Text='<% # Bind("strArrayResultado")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Responsable del proceso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 350px">
                                                    <asp:Label ID="strResponsable" runat="server" Text='<% # Bind("strResponsable")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                    </Columns>
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
               
            </Table>
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
                    </tr>
                </Table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>