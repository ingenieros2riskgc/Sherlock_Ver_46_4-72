<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MatrizIndicadores.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.MatrizIndicadores" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<link href="../../../Styles/AdminSitio.css" rel="stylesheet" />
<style type="text/css">
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}
</style>
<asp:UpdatePanel ID="GEPbody" runat="server">
    <ContentTemplate>

        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadMI" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Matriz de Indicadores" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyFormMI" class="ColumnStyle" runat="server">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lfecha" runat="server" Text="Año de consulta:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXfecha" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CalendarExtender ID="CEfechaConsulta" runat="server" Enabled="true" TargetControlID="TXfecha"
                            Format="yyyy" DefaultView="Years">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaConsul" runat="server" ControlToValidate="TXfecha"
                                    ErrorMessage="Debe ingresar la fecha de la consulta." ToolTip="Debe ingresar la fecha de la consulta."
                                    ValidationGroup="Matriz" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>                            
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="IBsearch" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="Matriz" ToolTip="Insertar" OnClick="IBsearch_Click"/>
            
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                            
                        </td>
                        
                    </tr>
                    </table>
            </div>
        </div>
        <div id="BodyGridMI" class="ColumnStyle" runat="server" visible="false">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVMatriz" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="intIdPeriodicidad,intIdProcesoIndicador"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPreRender="GVMatriz_PreRender" OnPageIndexChanging="GVMatriz_PageIndexChanging" OnRowCommand="GVMatriz_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Política Calidad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strPoliticaCalidad" runat="server" Text='<% # Bind("strPoliticaCalidad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdIndicador" HeaderText="Id Indicador" SortExpression="intIdIndicador" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Descripción Objetivo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strDescripcionObjetivo" runat="server" Text='<% # Bind("strDescripcionObjetivo")%>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Formula" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strFormula" runat="server" Text='<% # Bind("strFormula")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Inicador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strDescripcionInicador" runat="server" Text='<% # Bind("strDescripcionInicador")%>'></asp:Label>
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
                                        
                                        <asp:BoundField DataField="intMeta" HeaderText="Meta" SortExpression="intMeta" HtmlEncodeFormatString="True" HtmlEncode="False" />
                                        <asp:TemplateField HeaderText="Nombre Periodo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strNombrePeriodo" runat="server" Text='<% # Bind("strNombrePeriodo")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seguimiento" HeaderText="Seguimiento" CommandName="Seguimiento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdPeriodicidad" HeaderText="intIdPeriodicidad" ReadOnly="True" SortExpression="intIdPeriodicidad" ItemStyle-HorizontalAlign="Center" Visible ="false"/>
                                        <asp:BoundField DataField="intIdProcesoIndicador" HeaderText="intIdProcesoIndicador" ReadOnly="True" SortExpression="intIdProcesoIndicador" ItemStyle-HorizontalAlign="Center" Visible ="false"/>
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
        <div id="DVseguimiento" class="ColumnStyle" runat="server" visible="false">
            <Table class="tabla" align="center" width="50%">
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Lpolitica" runat="server" Text="Política de Calidad:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXpolitica" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Lobjetivo" runat="server" Text="Objetivo de Calidad:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabObjetivo" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Lindicador" runat="server" Text="Nombre Indicador:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXindicador" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Label16" runat="server" Text="Formula:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabFormula" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Ldescripcion" runat="server" Text="Descripción Indicador:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TXdescripcion" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Label14" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabProceso" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Label10" runat="server" Text="Meta:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabMeta" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                <tr>
                                        <td class="RowsText">
                                            <asp:Label ID="Label11" runat="server" Text="Periodo:" CssClass="Apariencia"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxCabFrecuencia" runat="server" Enabled="False" Width="400px" CssClass="Apariencia"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    
                                </table>
            </div>
        <div id="DVgridSeguimiento" class="ColumnStyle" runat="server" visible="false">
            <Table class="tabla" align="center" width="50%">
                <tr>
                    <td align="center">
            <asp:GridView ID="GVCumplimiento" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowSorting="True"
                                    ShowHeaderWhenEmpty="True" 
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" AutoGenerateColumns="False" PageSize="12">
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
                <Columns>
                    <asp:TemplateField HeaderText="Periodo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="periodo" runat="server" Text='<% # Bind("periodo")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cumplimiento" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="Cumplimiento" runat="server" Text='<% # Bind("Cumplimiento")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                    </Columns>
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
                <tr>
                    <td colspan="4">
                        <asp:ImageButton ID="btnRegresarFiltros" runat="server" CausesValidation="true" CommandName="Back"
                        ImageUrl="~/Imagenes/Icons/undo-icon32x32.png" Text="Regresar a Filtros" ValidationGroup="Matriz"  ToolTip="Regresar a Indicadores" OnClick="btnRegresarFiltros_Click" />
                    </td>
                </tr>
                </Table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>