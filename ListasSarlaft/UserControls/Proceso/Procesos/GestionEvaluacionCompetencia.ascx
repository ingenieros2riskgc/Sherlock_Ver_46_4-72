<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionEvaluacionCompetencia.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.GestionEvaluacionCompetencia" %>
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
<asp:UpdatePanel ID="GECbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadGEC" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Gestión Evaluación Competencias" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridGEC" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
            <asp:GridView ID="GVevaluacionCompetencias" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand" DataKeyNames="intIdMacroProceso, strUsuario, intIdTipoProceso"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Nombre Evaluado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreEvaluado" runat="server" Text='<% # Bind("strNombreEvaluado")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="150" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="150" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="strCargoResponsable" HeaderText="Cargo Responsable" SortExpression="strCargoResponsable" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:BoundField DataField="strNombreCargo" HeaderText="Cargo Responsable" ReadOnly="True" SortExpression="strNombreCargo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width ="300" />
                                        <asp:TemplateField HeaderText="Jefe Inmediato" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="JefeInmediato" runat="server" Text='<% # Bind("strJefeInmediato")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="200" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="200" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="FechaRegistro" ReadOnly="True" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdMacroProceso" Visible="false" HeaderText="intIdMacroProceso" SortExpression="intIdMacroProceso" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strNombreProceso" HeaderText="Macro Proceso" ReadOnly="True" SortExpression="strNombreProceso" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strUsuario" Visible="false" HeaderText="strUsuario" ReadOnly="True" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
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
        <div id="BodyFormGEC" class="ColumnStyle" runat="server" visible="false">
            <div id="form" class="TableContains">
                <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lcodigo" runat="server"  Text="Código:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtId" runat="server" Enabled="false"
                                    CssClass="Apariencia" Width="300px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LfechaRegistro" runat="server" Text="Fecha de Evaluación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXfecharegistro" runat="server"  Width="300px"></asp:TextBox>

                            </td>
                            <td>
                                <asp:CalendarExtender ID="CEfechaEvaluecion" runat="server" Enabled="true" TargetControlID="TXfecharegistro"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechaEvaluacion" runat="server" ControlToValidate="TXfecharegistro"
                                    ErrorMessage="Debe ingresar la fecha de la evaluación." ToolTip="Debe ingresar la fecha de la evaluación."
                                    ValidationGroup="GEvaCompetencia" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LnombreEvaluador" runat="server" Text="Nombre del Evaluado:" CssClass="Apariencia" Width="300px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreEva" runat="server"  CssClass="Apariencia" MaxLength="150" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td><asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreEva"
                                    ErrorMessage="Debe ingresar el Nombre del evaluador." ToolTip="Debe ingresar el Nombre del evaluador."
                                    ValidationGroup="GEvaCompetencia" ForeColor="Red">*</asp:RequiredFieldValidator></td>
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
                                <asp:RequiredFieldValidator ID="rfvCargo" runat="server" ControlToValidate="tbxResponsable"
                                                ErrorMessage="Debe ingresar el Cargo." ToolTip="Debe ingresar el Cargo."
                                                ValidationGroup="GEvaCompetencia" ForeColor="Red" >*</asp:RequiredFieldValidator>
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
                        <tr id="CadenaValor">
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
                                <asp:RequiredFieldValidator ID="rfvCadenaValor" runat="server" ControlToValidate="ddlCadenaValor"
                                                ErrorMessage="Debe ingresar la cadena de valor." ToolTip="Debe ingresar la cadena de valor."
                                                ValidationGroup="GEvaCompetencia" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox>
                            </td>
                                    </tr>
                                    <tr id="Macroproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                                OnSelectedIndexChanged="ddlMacroproceso_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td><asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                                ValidationGroup="GEvaCompetencia" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Proceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label1" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                                ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                                ValidationGroup="GEvaCompetencia" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="Subproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                                ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                                ValidationGroup="GEvaCompetencia" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                        
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Ljefe" runat="server" Text="Jefe Inmediato:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXjefe" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvJefe" runat="server" ControlToValidate="TXjefe"
                                    ErrorMessage="Debe ingresar el Nombre del Jefe Inmediato." ToolTip="Debe ingresar el Nombre del Jefe Inmediato."
                                    ValidationGroup="GEvaCompetencia" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label4" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
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
        <div class="TituloLabel" id="TituloCompetenciasEva" runat="server" visible="false">
                    <asp:Label ID="Ltexttitulo" runat="server" ForeColor="White" Text="Competencias a Evaluar" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="GVgestionCompetencia" runat="server" class="ColumnStyle" visible="false">
            <Table id="TgestionCompetencias" class="tabla" align="center" width="80%">
                <tr align="center">
                        <td>
            <asp:GridView ID="GVGestionCompetencias" runat="server" CellPadding="4"
                                    ForeColor="#333333" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" Visible ="false" PageSize="50">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nombre Competencia" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombreCompetencia" runat="server" Text='<% # Bind("strNombreCompetencia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Criterio Competencia" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 350px">
                                                    <asp:Label ID="DescripcionCompetencia" runat="server" Text='<% # Bind("strDescripcionCompetencia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="350" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="350" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Puntaje Asignado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:TextBox ID="TXpuntajeAsignado" runat="server"></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="rfvPuntaje" runat="server" ControlToValidate="TXpuntajeAsignado"
                                    ErrorMessage="Debe ingresar el valo del punjate." ToolTip="Debe ingresar el valo del punjate."
                                    ValidationGroup="puntajeAsignado" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="revCantPre" Display="Dynamic" ForeColor="Red"
                                    ControlToValidate="TXpuntajeAsignado" ValidationExpression="^[0-9]+([.,][0-9]{1,3})?$" ValidationGroup="Encuesta"
                                    ErrorMessage="Ingresar solamente números enteros" ToolTip="Ingresar solamente números enteros">*</asp:RegularExpressionValidator>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" />
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
                            <asp:GridView ID="GVGestionCompetenciasEvaVal" runat="server" CellPadding="4"
                                    ForeColor="#333333" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" Visible ="false" PageSize="50">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nombre Competencia" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="NombreCompetencia" runat="server" Text='<% # Bind("strNombreCompetencia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Criterio Competencia" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 350px">
                                                    <asp:Label ID="DescripcionCompetencia" runat="server" Text='<% # Bind("strDescripcionCompetencia")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="350" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="350" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Puntaje Asignado" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:Label ID="LpuntajeAsignado" runat="server" Text='<% # Bind("intPuntajeAsignado")%>'></asp:Label> 
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" />
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
                            <asp:GridView ID="GVprintEvaComp" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" Visible ="false">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="strNombreCompetencia" HeaderText="Nombre Competencia" SortExpression="strNombreCompetencia" HtmlEncodeFormatString="True" HtmlEncode="False"/>
                                        <asp:BoundField DataField="strDescripcionCompetencia" HeaderText="Descripcion Criterio Competencia" SortExpression="strDescripcionCompetencia" HtmlEncodeFormatString="True" HtmlEncode="False"/>
                                        <asp:BoundField DataField="intPuntajeAsignado" HeaderText="Puntaje Asignado" SortExpression="intPuntajeAsignado" HtmlEncodeFormatString="True" HtmlEncode="False"/>
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
                </table>
            </div>
        <div id="DVprocess" runat="server" class="ColumnStyle" visible="false">
            <Table id="Tprocess" class="tabla" align="center" width="80%">
                <tr>
                    <td>
                        <asp:Label ID="LprocessText" runat="server" Text="Para procesar los datos has click en el botón:"></asp:Label>
                    </td>
                    </tr>
                <tr align="center">
                    <td>
                        <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Procesar" ImageUrl="~/Imagenes/Aplicacion/Gears.png" OnClick="IBprocess_Click" Text="Procesar" ToolTip="Procesar" ValidationGroup="puntajeAsignado" />
                        
                    </td>
                    </tr>
                <tr runat="server" id="TRcancel">
                    <td>
                        <asp:Label ID="LcancelProcess" runat="server" Text="Para cancelar has click en el botón:"></asp:Label>
                    </td>
                    </tr>
                <tr align="center" runat="server" id="TRcancelButton">
                    <td><asp:ImageButton ID="btnImgCancelarProcess" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Cancelar" OnClick="btnImgCancelarProcess_Click" /></td>
                </tr>
            </table>
            </div>
        <div id="DVCompetencias" runat="server" class="ColumnStyle" visible="false">
            <Table id="TcompetenciasTotal" class="tabla" align="center" width="80%">
                <tr align="center" id="Total" runat="server">
                    <td>
                        <asp:GridView ID="GVcompentecia" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Nombre Competencia" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="Nombre" runat="server" Text='<% # Bind("strNombre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Puntaje Asignado Total" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 200px">
                                                    <asp:TextBox ID="TXpuntajeAsignadoTotal" runat="server" Enabled="false"></asp:TextBox> 
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ponderación" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="ponderacion" runat="server" Text='<% # Bind("intPonderacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="total" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
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
                        <asp:GridView ID="GVtotal" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
                                    DataKeyNames="intId" ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" Visible="false">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Código" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="intId" runat="server" Text='<% # Bind("intId")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Competencia" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="Nombre" runat="server" Text='<% # Bind("strNombre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Puntaje Asignado Total" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="intPuntajeTotal" runat="server" Text='<% # Bind("intPuntajeTotal")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Ponderación" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="intPonderacion" runat="server" Text='<% # Bind("intPonderacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="intTotal" runat="server" Text='<% # Bind("intTotal")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" Width="100" />
                                            <ItemStyle Wrap="false" Width="100" HorizontalAlign="Center"/>
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
            <Table id="TotalCompetencias" class="tabla" align="center" width="80%" runat="server">
                <tr align="center">
                    <td class="RowsText" colspan="2">
                        <asp:Label ID="LtextTotal" runat="server" Text="Valor total de la Evaluación: "></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label id="LvalorTotalCompetencia" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td class="RowsText" colspan="2">
                        <asp:Label ID="Lcalificacion" runat="server" Text="La calificación final de la Evaluación: "></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label id="LvalorCalificacion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="RowsText">
                                <asp:Label ID="Lobservaciones" runat="server" Text="Necesidades Detectadas:" CssClass="Apariencia"></asp:Label>
                            </td>
                    <td>
                        <asp:TextBox ID="TXobservaciones" runat="server" Width="300px" ValidationGroup="GEvaCompetencia" MaxLength="750"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RVobservaciones" runat="server" ControlToValidate="TXobservaciones"
                                    ErrorMessage="Debe ingresar la observación de la evaluación." ToolTip="Debe ingresar la observación de la evaluación."
                                    ValidationGroup="GEvaCompetencia" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="RowsText">
                        <asp:Label ID="LfechaProxima" runat="server" Text="Fecha de Próxima Evaluación:" CssClass="Apariencia"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TXfechanext" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaCompetencia"></asp:TextBox>
                        <asp:CalendarExtender ID="CEfechanext" runat="server" Enabled="true" TargetControlID="TXfechanext"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RVfechanext" runat="server" ControlToValidate="TXfechanext"
                                    ErrorMessage="Debe ingresar la fecha de la siguiente evaluación." ToolTip="Debe ingresar la fecha de la siguiente evaluación."
                                    ValidationGroup="GEvaCompetencia" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" visible="false">
                    <td class="RowsText" colspan="2">
                    <asp:Label ID="LloadFile" runat="server" Text="Cargar documento:" Font-Names="Calibri"
                        Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fuArchivoPerfil" runat="server" Font-Names="Calibri" Font-Size="Small" multiple="false">
                    </asp:FileUpload>                    
                </td>
                </tr>
                </Table>
            <table id="tbDocumentos" runat="server" align="center" visible="false">
                                    <tr align="center">
                                        <td>
                                            <table id="Table2" runat="server">
                                                <tr align="left">
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr align="center">
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label186" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Documentos adjuntos"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table>
                                                                                    <tr align="center">
                                                                                        <td>
                                                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HeaderStyle-CssClass="gridViewHeader" HorizontalAlign="Center" OnRowCommand="GridView2_RowCommand" ShowHeaderWhenEmpty="True">
                                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="false" />
                                                                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                                                    <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo" />
                                                                                                    <asp:ButtonField ButtonType="Image" CommandName="Descargar" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar" />
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
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="GEvaCompetencia" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="GEvaCompetencia" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
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