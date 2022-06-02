<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReporteSeguimientoEvaluacionDesempeño.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Reportes.ReporteSeguimientoEvaluacionDesempeño" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="RSEDbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadRSED" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Reporte Seguimiento Evaluación Desempeño" Font-Bold="False"
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
                        <td colspan="2" align="center" class="RowsText">
                            <asp:Label ID="LEvaluado" runat="server" Text="Cargo Evaluado:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td colspan="2" align="center">
                            <asp:TextBox ID="TXevaluado" runat="server"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" />
                                <asp:RequiredFieldValidator ID="rfvCargo" runat="server" ControlToValidate="TXevaluado"
                                                ErrorMessage="Debe ingresar el Cargo." ToolTip="Debe ingresar el Cargo."
                                                ValidationGroup="metas2" ForeColor="Red" >*</asp:RequiredFieldValidator>
                                <asp:PopupControlExtender ID="popupDependencia4" runat="server" TargetControlID="imgDependencia4" BehaviorID="popup4"
                                    PopupControlID="pnlDependencia4" OffsetY="-200">
                                </asp:PopupControlExtender>
                                <asp:Panel ID="pnlDependencia4" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                    <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                        <tr align="right" bgcolor="#5D7B9D">
                                            <td>
                                                <asp:ImageButton ID="btnClosepp4" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close3.png"
                                                    OnClientClick="$find('popup4').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TreeView ID="TreeView4" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                    Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                    AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView4_SelectedNodeChanged">
                                                    <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="BtnOk4" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup4').hidePopup(); return false;" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
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
         <div id="BodyGridRSED" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVReporteSeguimientoEvaDesempeño" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Nombre Evaluado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreEvaluado" runat="server" Text='<% # Bind("strNombre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recomendación Capacitación" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 250px">
                                                    <asp:Label ID="strRecomendacionCapacitacion" runat="server" Text='<% # Bind("strRecomendacionCapacitacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Recomendación Compromisos" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 250px">
                                                    <asp:Label ID="strRecomendacionCompromisos" runat="server" Text='<% # Bind("strRecomendacionCompromisos")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Otros" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strOtros" runat="server" Text='<% # Bind("strOtros")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Otros" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strDescripcionOtros" runat="server" Text='<% # Bind("strDescripcionOtros")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Próxima Evaluación" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="dtFechaProximaEvaluacion" runat="server" Text='<% # Bind("dtFechaProximaEvaluacion")%>'></asp:Label>
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