<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteInformacionGeneral.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Reportes.ReporteInformacionGeneral" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="RIGDbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadRIG" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Reporte información para revisión gerencial" Font-Bold="False"
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
                                    ValidationGroup="metas" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                                    ValidationGroup="metas" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>                            
                    <tr>
                        <td colspan="6" align="center">
                            <asp:ImageButton ID="IBsearch" runat="server" CausesValidation="true" CommandName="Buscar"
                                    ImageUrl="~/Imagenes/Icons/Apps-Search-And-Replace-icon.png"  Text="Insert" ValidationGroup="metas" ToolTip="Buscar" OnClick="IBsearch_Click"/>
            
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                     ToolTip="Cancelar" OnClick="btnImgCancelar_Click" />
                        </td>
                    </tr>
                    </table>
            </div>
         <div id="BodyGridRIG" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVReporteInfoGeneral" runat="server" CellPadding="4"
                                    ForeColor="#333333" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPreRender="GVReporteInfoGeneral_PreRender" PageSize="50">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Clientes Encuestados" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumClientesEncuestados" runat="server" Text='<% # Bind("intNumClientesEncuestados")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Clientes Aprobados" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumClientesAprobados" runat="server" Text='<% # Bind("intNumClientesAprobados")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        
                                        
                                       
                                        <asp:TemplateField HeaderText="Cantidad Metas Cumplidas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumMetasCumplidas" runat="server" Text='<% # Bind("intNumMetasCumplidas")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad Metas Incumplidas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumMetasIncumplidas" runat="server" Text='<% # Bind("intNumMetasIncumplidas")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Cantidad No Conformidad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumNoConformidad" runat="server" Text='<% # Bind("intNumNoConformidad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad No Conformidad Cerradas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumNoConformidadCierre" runat="server" Text='<% # Bind("intNumNoConformidadCierre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad Auditorias Programadas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumAuditoria" runat="server" Text='<% # Bind("intNumAuditoria")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cantidad Auditorias Cumplidas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="intNumAuditoriaCumplida" runat="server" Text='<% # Bind("intNumAuditoriaCumplida")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
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
                        <asp:ImageButton ID="ImButtonPDFexport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexport_Click" style="height: 26px" />
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