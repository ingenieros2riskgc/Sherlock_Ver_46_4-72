<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionEvaluacionDesempeño.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.GestionEvaluacionDesempeño" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
     .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="GEDbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlActividad" runat="server" CssClass="popup" Width="800px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
                <tr align="right" bgcolor="#5D7B9D">
                    <td colspan="2">
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close3.png"
                            OnClientClick="$find('popupActividad2').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:GridView ID="GVfactorDesempeño" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Descr. Detalle" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 250px">
                                                    <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="250" />
                                            <ItemStyle Wrap="false" Width="250" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdFactoresDesempeno" HeaderText="Código" ReadOnly="True" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:TemplateField HeaderText="Nombre Factor" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 250px">
                                                    <asp:Label ID="NombreFactor" runat="server" Text='<% # Bind("strNombreFactor")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="250" />
                                            <ItemStyle Wrap="false" Width="250" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdCalificacion" HeaderText="Id. Calificación" Visible="False" />
                                        <asp:TemplateField HeaderText="Calificación" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombreCalificacion" runat="server" Text='<% # Bind("strNombreCalificacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario" Visible="False" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" Visible="False" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false"/>
                                        <asp:BoundField DataField="decCriterioMinimo" HeaderText="Criterio Minimo" SortExpression="decCriterioMinimo"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="decCriterioMaximo" HeaderText="Criterio Maximo" SortExpression="decCriterioMaximo"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
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
                    <td>
                        <asp:Label ID="LValorCalificacion" runat="server" Text="Digite el valor de la Calificación"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TXvalorCalificacion" runat="server"></asp:TextBox> 
						<asp:RequiredFieldValidator ID="RVvalorCalificacion" runat="server" ControlToValidate="TXvalorCalificacion"
                                    ErrorMessage="Debe ingresar el valor de la calificación." ToolTip="Debe ingresar el valor de la calificación."
                                    ValidationGroup="GEvalorCalificacion" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="revValor" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="TXvalorCalificacion" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="GEvalorCalificacion"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:Button ID="Bok" runat="server" Text="Aceptar" CssClass="Apariencia" CausesValidation="true" ValidationGroup="GEvalorCalificacion" OnClick="Bok_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <div class="TituloLabel" id="HeadGEC" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Gestión Evaluación Desempeño" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridGED" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVevaluacionDesempeño" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="intCargoResponsable,strUsuario,dtFechaRegistro,intCalificacion,strRecomendacionCapacitacion,strRecomendacionCompromisos,strOtros,strDescripcionOtros"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVevaluacionDesempeño_RowCommand" OnPageIndexChanging="GVevaluacionDesempeño_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Nombre Evaluado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreEvaluado" runat="server" Text='<% # Bind("strNombre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intCargoResponsable" HeaderText="Cargo Responsable" SortExpression="intCargoResponsable" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Cargo Responsable" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="strCargo" runat="server" Text='<% # Bind("strCargo")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="200" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="200" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFechaEvaluacion" HeaderText="Fecha Evaluación" ReadOnly="True" SortExpression="dtFechaEvaluacion" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Nombre Evaluador" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="nombreEvaluador" runat="server" Text='<% # Bind("strEvaluador")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="200" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="200" />
                                        </asp:TemplateField>
                                        <asp:BoundField Visible="false" DataField="intCalificacion" HeaderText="Calificación" ReadOnly="True" SortExpression="intCalificacion" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField Visible="false" DataField="strRecomendacionCapacitacion" HeaderText="Recomendación Capacitación" ReadOnly="True" SortExpression="strRecomendacionCapacitacion" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField Visible="false" DataField="strRecomendacionCompromisos" HeaderText="Recomendación Compromisos" ReadOnly="True" SortExpression="strRecomendacionCompromisos" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField Visible="false" DataField="strOtros" HeaderText="Otros" ReadOnly="True" SortExpression="strOtros" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField Visible="false" DataField="strDescripcionOtros" HeaderText="Descripción Otros" ReadOnly="True" SortExpression="strDescripcionOtros" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="dtFechaProximaEvaluacion" HeaderText="Fecha Próxima Evaluación" ReadOnly="True" SortExpression="dtFechaProximaEvaluacion" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="registro" ReadOnly="True" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" Visible="false" />
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
        <div id="BodyFormGED" class="ColumnStyle" runat="server" visible="false">
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
                                <asp:Label ID="LnombreEvaluado" runat="server" Text="Nombre y Apellido:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXnombreEvaluado" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVnombreEvaluado" runat="server" ControlToValidate="TXnombreEvaluado"
                                    ErrorMessage="Debe ingresar el Nombre del evaluado." ToolTip="Debe ingresar el Nombre del evaluado."
                                    ValidationGroup="GEvaDesempeño" ForeColor="Red">*</asp:RequiredFieldValidator>

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
                                <asp:RequiredFieldValidator ID="rfvCargo" runat="server" ControlToValidate="tbxResponsable"
                                    ErrorMessage="Debe ingresar el Cargo." ToolTip="Debe ingresar el Cargo."
                                    ValidationGroup="GEvaDesempeño" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LnombreEvaluador" runat="server" Text="Nombre del Evaluador:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreEva" runat="server"  CssClass="Apariencia" MaxLength="150" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreEva"
                                    ErrorMessage="Debe ingresar el Nombre del evaluador." ToolTip="Debe ingresar el Nombre del evaluador."
                                    ValidationGroup="GEvaDesempeño" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr >
                        <td class="RowsText">
                            <asp:Label ID="LfechaEva" runat="server" Text="Fecha de la Evaluación:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaEva" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaCompetencia" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaEva" runat="server" Enabled="true" TargetControlID="TXfechaEva"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaeva" runat="server" ControlToValidate="TXfechaEva"
                                    ErrorMessage="Debe ingresar la fecha de la evaluación." ToolTip="Debe ingresar la fecha de la evaluación."
                                    ValidationGroup="GEvaDesempeño" ForeColor="Red">*</asp:RequiredFieldValidator>
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
        <div class="TituloLabel" id="TituloDesempeñoEva" runat="server" visible="false">
                    <asp:Label ID="Ltexttitulo" runat="server" ForeColor="White" Text="Resultado de la Evaluación" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="ResultadoDesempeño" runat="server" visible="false" class="ColumnStyle">
            <Table class="TresultDesempeño" align="center" width="100%">
                        <tr align="center">
                            <td colspan="4">
            <asp:GridView ID="GVfactoresDesempeño" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVfactoresDesempeño_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="intId" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Factor Desempeño" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="Factor" runat="server" Text='<% # Bind("strFactoresEvaluacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Id. Usuario" DataField="intIdUsuario" Visible="False" />
                                        <asp:BoundField HeaderText="Usuario Creación" DataField="strNombreUsuario" Visible="False" />
                                        <asp:BoundField HeaderText="Fecha de Creación" DataField="dtFechaRegistro" SortExpression="dtFechaRegistro"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Criterios"
                                            HeaderText="Criterios" CommandName="Criterios" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Puntaje Asignado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 170px">
                                                    <asp:TextBox ID="TXpuntajeAsignado" runat="server" Enabled="false"></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="rfvPuntaje" runat="server" ControlToValidate="TXpuntajeAsignado"
                                    ErrorMessage="Debe ingresar el valo del punjate." ToolTip="Debe ingresar el valo del punjate."
                                    ValidationGroup="puntajeAsignado" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="revCantPre" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="TXpuntajeAsignado" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="puntajeAsignado"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
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
                                <asp:GridView ID="GVfactoresDesempeñoPrint" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" Visible="false">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Factor Desempeño" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="Factor" runat="server" Text='<% # Bind("strFactoresEvaluacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="120" />
                                            <ItemStyle Wrap="false" Width="120" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Puntaje Asignado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 120px">
                                                    <asp:Label ID="puntaje" runat="server" Text='<% # Bind("intPuntajeAsignado")%>'></asp:Label>
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
                                <asp:HiddenField ID="hidForModel" runat="server" />
                                <asp:ModalPopupExtender ID="modalPopup" runat="server" PopupControlID="pnlActividad"
                                                Enabled="True" TargetControlID="hidForModel" BackgroundCssClass="modalBackground" DropShadow="true">
                                            </asp:ModalPopupExtender>
                                </td>
                            </tr>
                <tr id="trTituloTotal" runat="server" visible="false">
                    <td colspan ="4">
                        <asp:Label ID="LprocessText" runat="server" Text="Para procesar los datos has click en el botón:"></asp:Label>
                    </td>
                </tr>
                <tr align="center" id="trValorTotal" runat="server" visible="false">
                    <td colspan="4">
                        <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Procesar" ImageUrl="~/Imagenes/Aplicacion/Gears.png" OnClick="IBprocess_Click" Text="Procesar" ToolTip="Procesar" ValidationGroup="puntajeAsignado" />
                    </td>
                </tr>
                <tr runat="server" id="TRcancel">
                    <td colspan="4">
                        <asp:Label ID="LcancelProcess" runat="server" Text="Para cancelar has click en el botón:"></asp:Label>
                    </td>
                    </tr>
                <tr align="center" runat="server" id="TRcancelButton">
                    <td colspan="4"><asp:ImageButton ID="btnImgCancelarProcess" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Cancelar" OnClick="btnImgCancelarProcess_Click" /></td>
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
        <div class="TituloLabel" id="DVtituloRecomendacion" runat="server" visible="false">
                    <asp:Label ID="LTitulorecomendacion" runat="server" ForeColor="White" Text="Recomendaciones Del Evaluador:" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="DVcontentRecomendacion" class="ColumnStyle" runat="server" visible="false">
            <div id="FormRecomendacion" class="TableContains">
                <Table class="controlRecomendacion" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lrecomendacion" runat="server" Text="Seleccione una Recomendación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLrecomendacion" runat="server" OnSelectedIndexChanged="DDLrecomendacion_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="0" Text="--Seleccione--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="CAPACITACION"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="GENERAR COMPROMISOS"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="OTROS"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                    <tr id="trEspecifique" runat="server" visible="false">
                            <td class="RowsText">
                                <asp:Label ID="Lespeficique" runat="server" Text="Especifique:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXespecifique" runat="server" Width="80%" MaxLength="850"></asp:TextBox>
                            </td>
                        </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lcompromisos" runat="server" Text="Compromisos adquiridos por el Evaluado:" CssClass="Apariencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXcompromisos" runat="server" TextMode="MultiLine" Wrap="false" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="dtFechanext" runat="server" Text="Fecha próxima Evaluación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXfechanext" runat="server" Width="80%"></asp:TextBox>
                                <asp:CalendarExtender ID="CEfechanext" runat="server" Enabled="true" TargetControlID="TXfechanext"
                                    Format="yyyy-MM-dd">
                                </asp:CalendarExtender>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVfechanext" runat="server" ControlToValidate="TXfechanext"
                                    ErrorMessage="Debe ingresar la fecha de la siguiente evaluación." ToolTip="Debe ingresar la fecha de la siguiente evaluación."
                                    ValidationGroup="GEvaDesempeño" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="GEvaDesempeño" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="GEvaDesempeño" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                    </Table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>