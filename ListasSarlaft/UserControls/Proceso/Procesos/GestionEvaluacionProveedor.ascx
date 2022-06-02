<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionEvaluacionProveedor.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.GestionEvaluacionProveedor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}
    </style>
<asp:UpdatePanel ID="GEPbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadGEP" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Gestión Evaluación Proveedor" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridGEP" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVevaluacionProveedor" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro,intCargoResponsable,strRealizadoPor,strObservaciones,strNombreResponsable"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVevaluacionProveedor_RowCommand" OnPageIndexChanging="GVevaluacionProveedor_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Nombre Proveedor" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreProveedor" runat="server" Text='<% # Bind("strNombreProveedor")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intCargoResponsable" HeaderText="Cargo Responsable" SortExpression="intCargoResponsable" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        
                                        <asp:BoundField DataField="dtFechaEvaluacion" HeaderText="Fecha Evaluación" ReadOnly="True" SortExpression="dtFechaEvaluacion" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="dtPeriodoEvaluadoInicial" HeaderText="Fecha Periodo Inicial Evaluado" ReadOnly="True" SortExpression="dtPeriodoEvaluadoInicial" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="dtPeriodoEvaluadoFinal" HeaderText="Fecha Periodo Final Evaluado" ReadOnly="True" SortExpression="dtPeriodoEvaluadoFinal" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Servicio Ofrecido" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="ServicioOfrecido" runat="server" Text='<% # Bind("strServicioOfrecido")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" ReadOnly="True" Visible="false" SortExpression="intIdUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strRealizadoPor" HeaderText="strRealizadoPor" ReadOnly="True" Visible="false" SortExpression="strRealizadoPor" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strObservaciones" HeaderText="strObservaciones" ReadOnly="True" Visible="false" SortExpression="strObservaciones" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strNombreResponsable" HeaderText="strNombreResponsable" ReadOnly="True" Visible="false" SortExpression="strNombreResponsable" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="dtFechaProximaEvaluacion" HeaderText="Fecha Próxima Evaluación" ReadOnly="True" SortExpression="dtFechaProximaEvaluacion" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
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
                <tr>
                    <td>
                        <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" />
                    </td>
                </tr>
            </Table>
        </div>
        <div id="BodyFormGEP" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lcodigo" runat="server" Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="False"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LnombreProveedor" runat="server" Text="Nombre del Proveedor:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXnombreProveedor" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVnombreProveedor" runat="server" ControlToValidate="TXnombreProveedor"
                                    ErrorMessage="Debe ingresar el Nombre del proveedor." ToolTip="Debe ingresar el Nombre del proveedor."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaEvapro" runat="server" Text="Fecha de la Evaluación:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaEva" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaProveedor" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaEvapro" runat="server" Enabled="true" TargetControlID="TXfechaEva"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFechaEva" runat="server" ControlToValidate="TXfechaEva"
                                    ErrorMessage="Debe ingresar la fecha de la evaluación." ToolTip="Debe ingresar la fecha de la evaluación."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaEva" runat="server" Text="Fecha de la Evaluación Periodo Inicial:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaEvaInicial" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaProveedor" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaEva" runat="server" Enabled="true" TargetControlID="TXfechaEvaInicial"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaeva" runat="server" ControlToValidate="TXfechaEvaInicial"
                                    ErrorMessage="Debe ingresar la fecha de la evaluación Periodo Inicial." ToolTip="Debe ingresar la fecha de la evaluación Periodo Inicial."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lfechafinal" runat="server" Text="Fecha de la Evaluación Periodo Final:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaEvaFin" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaProveedor" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaFinal" runat="server" Enabled="true" TargetControlID="TXfechaEvaFin"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFechaFinal" runat="server" ControlToValidate="TXfechaEvaFin"
                                    ErrorMessage="Debe ingresar la fecha de la evaluación Periodo Final." ToolTip="Debe ingresar la fecha de la evaluación Periodo Final."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lproducto" runat="server" Text="Producto o Servicio Ofrecido:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXproducto" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaProveedor" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvProducto" runat="server" ControlToValidate="TXproducto"
                                    ErrorMessage="Debe ingresar el producto o servicio ofrecido." ToolTip="Debe ingresar el producto o servicio ofrecido."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                        
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lusuario" runat="server" Text="Usuario Creación:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxUsuarioCreacion" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LfechaCreacion" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="300px" CssClass="Apariencia"
                                    Enabled="False"></asp:TextBox>
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
                    </tr>
                </Table>
        </div>
        <div class="TituloLabel" id="TituloProveedorEva" runat="server" visible="false">
                    <asp:Label ID="Ltexttitulo" runat="server" ForeColor="White" Text="Resultado de la Evaluación" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="ResultadoDesempeño" runat="server" visible="false" class="ColumnStyle">
            <Table class="TresultDesempeño" align="center" width="100%">
                        <tr align="center">
                            <td colspan="4">
            <asp:GridView ID="GVProveedorCriterios" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPreRender="GVProveedorCriterios_PreRender">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Código" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="codigo" runat="server" Text='<% # Bind("intId")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Aspecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="NombreAspecto" runat="server" Text='<% # Bind("strNombreAspecto")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor Porcentaje" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="ValorPorcentaje" runat="server" Text='<% # Bind("intValorPorcentaje")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Criterio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px;">
                                                    <asp:Label ID="DesCriterio" runat="server" Text='<% # Bind("strDesCriterio")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Parametro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px;">
                                                    <asp:Label ID="DesParametro" runat="server" Text='<% # Bind("strDesParametro")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Puntaje Asignado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 170px">
                                                    <asp:TextBox ID="TXpuntajeAsignado" runat="server" ></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="rfvPuntaje" runat="server" ControlToValidate="TXpuntajeAsignado"
                                    ErrorMessage="Debe ingresar el valo del punjate." ToolTip="Debe ingresar el valo del punjate."
                                    ValidationGroup="puntajeAsignado" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="170" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="170" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 170px">
                                                    <asp:Label ID="Lcalificacion" runat="server" ></asp:Label> 
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="170" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="170" HorizontalAlign="Center"/>
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
                <tr align="center">
                            <td colspan="4">
            <asp:GridView ID="GVresultProveedor" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPreRender="GVresultProveedor_PreRender">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nombre Aspecto" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="NombreAspecto" runat="server" Text='<% # Bind("strNombreAspecto")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Valor Porcentaje" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="ValorPorcentaje" runat="server" Text='<% # Bind("intValorPorcentaje")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Criterio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px;">
                                                    <asp:Label ID="DesCriterio" runat="server" Text='<% # Bind("strDesCriterio")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Parametro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px;">
                                                    <asp:Label ID="DesParametro" runat="server" Text='<% # Bind("strDesParametro")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Puntaje Asignado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px;">
                                                    <asp:Label ID="puntaje" runat="server" Text='<% # Bind("intValorPuntaje")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 170px">
                                                    <asp:Label ID="Lcalificacion" runat="server" Text='<% # Bind("intCalificacion")%>'></asp:Label> 
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="170" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="170" HorizontalAlign="Center"/>
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
                <tr id="trTituloTotal" runat="server">
                    <td colspan ="4">
                        <asp:Label ID="LprocessText" runat="server" Text="Para procesar los datos has click en el botón:"></asp:Label>
                    </td>
                </tr>
                <tr align="center" id="trValorTotal" runat="server">
                    <td colspan="4">
                        <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Procesar" ImageUrl="~/Imagenes/Aplicacion/Gears.png" Text="Procesar" ToolTip="Procesar" ValidationGroup="puntajeAsignado" OnClick="IBprocess_Click" />
                    </td>
                </tr>
                <tr align="center" id="cancel" runat="server">
                    <td colspan="4">
                        <asp:Label runat="server" ID="Lcancel" Text="Para cancelar has click en el botón:"></asp:Label>
                    </td>
                </tr>
                <tr align="cenet" id="Bcancel" runat="server">
                    <td colspan="4">
                        <asp:ImageButton ID="IBcancelProcess" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                    </td>
                </tr>
                <tr id="trTotal" runat="server" visible="false">
                    <td class="RowsText">
                        <asp:Label ID="Lpuntajetotal" runat="server" Text="Puntaje Total:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td>
                    <asp:TextBox ID="TXpuntajeTotal" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    <td class="RowsText">
                        <asp:Label ID="LcalificacionFinal" runat="server" Text="Calificación Total:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LvalorFinalCalificacion" runat="server"></asp:Label>
                        </td>
                </tr>
                </Table>
        </div>
        <div class="TituloLabel" id="DVtituloObservaciones" runat="server" visible="false">
                    <asp:Label ID="LTituloObservaciones" runat="server" ForeColor="White" Text="Observaciones Del Evaluador:" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="DVcontentObservaciones" class="ColumnStyle" runat="server" visible="false">
            <div id="FormObservaciones" class="TableContains">
                <Table class="controlRecomendacion" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lobservaciones" runat="server" Text="Observaciones:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXObservaciones" runat="server"  Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="LnombreEvaluador" runat="server" Text="Realizado por:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtrealizado" runat="server"  CssClass="Apariencia" MaxLength="150" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtrealizado"
                                    ErrorMessage="Debe ingresar el Nombre del realizador." ToolTip="Debe ingresar el Nombre del realizador."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lcargo" runat="server" Text="Cargo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxResponsable" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvCargo" runat="server" ControlToValidate="tbxResponsable"
                                    ErrorMessage="Debe ingresar el Cargo." ToolTip="Debe ingresar el Cargo."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:Label ID="lblIdDependencia4" runat="server" Visible="False" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <asp:ImageButton ID="imgDependencia4" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                    OnClientClick="return false;" />
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
                            <td class="RowsText">
                                <asp:Label ID="dtFechanext" runat="server" Text="Fecha próxima Evaluación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXfechanext" runat="server" Width="300px"></asp:TextBox>
                                <asp:CalendarExtender ID="CEfechanext" runat="server" Enabled="true" TargetControlID="TXfechanext"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVfechanext" runat="server" ControlToValidate="TXfechanext"
                                    ErrorMessage="Debe ingresar la fecha de la siguiente evaluación." ToolTip="Debe ingresar la fecha de la siguiente evaluación."
                                    ValidationGroup="GEvaProveedor" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="GEvaProveedor" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="GEvaProveedor" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                    </Table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>