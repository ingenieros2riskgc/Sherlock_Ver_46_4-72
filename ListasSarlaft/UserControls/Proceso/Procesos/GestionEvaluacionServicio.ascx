<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionEvaluacionServicio.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.GestionEvaluacionServicio" %>
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
        <div class="TituloLabel" id="HeadGES" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Gestión Evaluación Servicio" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridGES" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVevaluacionServicio" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVevaluacionServicio_RowCommand" OnPageIndexChanging="GVevaluacionServicio_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Ciudad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="ciudad" runat="server" Text='<% # Bind("strCiudad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFecha" HeaderText="Fecha" SortExpression="dtFecha" HtmlEncodeFormatString="True" HtmlEncode="False" />
                                        <asp:TemplateField HeaderText="Tipo Encuesta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="tipoEncuesta" runat="server" Text='<% # Bind("strTipoEncuesta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Cliente" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="nombreCliente" runat="server" Text='<% # Bind("strNombreCliente")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Encuestado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="nombre" runat="server" Text='<% # Bind("strNombre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cargo" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="cargo" runat="server" Text='<% # Bind("strCargo")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Funcionarios" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="funcionarios" runat="server" Text='<% # Bind("strFuncionarios")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" ReadOnly="True" Visible="false" SortExpression="intIdUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
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
        <div id="BodyFormGES" class="ColumnStyle" runat="server" visible="false">
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
                                <asp:Label ID="Lcuidad" runat="server" Text="Ciudad:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXciudad" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RFVciudad" runat="server" ControlToValidate="TXciudad"
                                    ErrorMessage="Debe ingresar la ciudad." ToolTip="Debe ingresar la ciudad."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaEvaSer" runat="server" Text="Fecha:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaEva" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaProveedor" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaEvaser" runat="server" Enabled="true" TargetControlID="TXfechaEva"
                            Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFechaEva" runat="server" ControlToValidate="TXfechaEva"
                                    ErrorMessage="Debe ingresar la fecha de la evaluación." ToolTip="Debe ingresar la fecha de la evaluación."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Ltipo" runat="server" Text="Tipo Encuenta:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RBtipoEncuesta" runat="server" ValidationGroup="GEvaServicio" AutoPostBack="True">
                                    <asp:ListItem Value="Presencial">Presencial</asp:ListItem>
                                    <asp:ListItem Value="Telefónica">Telefónica</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="LnombreCliente" runat="server" Text="Nombre del Cliente:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXnombreCliente" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVnombreCliente" runat="server" ControlToValidate="TXnombreCliente"
                                    ErrorMessage="Debe ingresar el Nombre del Cliente." ToolTip="Debe ingresar el Nombre del Cliente."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        
                    <tr><td class="RowsText">
                            <asp:Label ID="LnombreEncuestado" runat="server" Text="Nombre Encuestado:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        
                        <td>
                            <asp:TextBox ID="TXnombreEncuestado" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaServicio" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RVnombreEncuestado" runat="server" ControlToValidate="TXnombreEncuestado"
                                    ErrorMessage="Debe ingresar El nombre del Encuestado." ToolTip="Debe ingresar El nombre del Encuestado."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lcargo" runat="server" Text="Cargo del Encuestado:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXcargo" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaServicio" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="rfvCargo" runat="server" ControlToValidate="TXcargo"
                                    ErrorMessage="Debe ingresar el cargo." ToolTip="Debe ingresar el cargo."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lfuncionarios" runat="server" Text="Funcionarios:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfuncionarios" runat="server" TextMode="MultiLine" Wrap="false" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvFuncionarios" runat="server" ControlToValidate="TXfuncionarios"
                                    ErrorMessage="Debe ingresar los funcionarios que lo atienden." ToolTip="Debe ingresar los funcionarios que lo atienden."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lencuesta" runat="server" Text="Encuesta:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLencuestas" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True"
                                                DataTextField="NombreEncuesta" DataValueField="IdEncuesta" OnSelectedIndexChanged="DDLencuestas_SelectedIndexChanged"
                                                >
                                            </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
            </div>
        </div>
        <div id="DVprint" class="ColumnStyle" runat="server" visible="false">
            <Table class="tabla" align="center" width="50%">
                <tr>
                    <td>
                            Para exportar la Encuesta a PDF:
                        </td>
                    <td>
                        <asp:ImageButton ID="ImButtonPDFexport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFexport_Click" style="width: 26px" />
                    </td>
                </tr>
                </Table>
        </div>
         <div id="DVgridEncuestas" class="ColumnStyle" runat="server" visible="false">
             <Table class="tabla" align="center" width="80%">
                        <tr>
                            <td class="RowsText">
                                <asp:Label runat="server" ID="TextoQuestion" Text="Califique cada pregunta en la escala de 1 a 5, siendo 1 deficiente y 5 Excelente. NC:  No contesta."></asp:Label>
                            </td>
                                </tr>
                 <tr>
                     <td>
                         <asp:Label ID="LdescripcionEmpresa" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr align="Center" runat="server">
                     <td>
                         <asp:GridView ID="GVencuestas" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="intIdPregunta"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdPregunta" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                                        <asp:TemplateField HeaderText="Nombre Encuesta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreEncuesta" runat="server" Text='<% # Bind("strNombreEncuesta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intCantidadPregunta" HeaderText="Cantidad Pregunta" SortExpression="intCantidadPregunta" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Pregunta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis;">
                                                    <asp:Label ID="TextoPregunta" runat="server" Text='<% # Bind("strTextoPregunta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 170px">
                                                    <asp:TextBox ID="TXcalificacion" runat="server" ></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="rfvPuntaje" runat="server" ControlToValidate="TXcalificacion"
                                    ErrorMessage="Debe ingresar el valo de la calificacion." ToolTip="Debe ingresar el valo de la calificacion."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                         <asp:GridView ID="GVrespuestas" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Pregunta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="pregunta" runat="server" Text='<% # Bind("strPregunta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                    <asp:Label ID="respuesta" runat="server" Text='<% # Bind("strRespuesta")%>'></asp:Label>
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
                         <asp:GridView ID="GVprint" runat="server" CellPadding="4" Visible="false"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nombre Encuesta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreEncuesta" runat="server" Text='<% # Bind("strNombreEncuesta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pregunta" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis;">
                                                    <asp:Label ID="TextoPregunta" runat="server" Text='<% # Bind("strTextoPregunta")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 170px">
                                                    <asp:TextBox ID="TXcalificacion" runat="server" ></asp:TextBox> 
                                                    <asp:RequiredFieldValidator ID="rfvPuntaje" runat="server" ControlToValidate="TXcalificacion"
                                    ErrorMessage="Debe ingresar el valo de la calificacion." ToolTip="Debe ingresar el valo de la calificacion."
                                    ValidationGroup="GEvaServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                 <tr>
                     <td class="RowsText">
                         <asp:Label ID="Lobservaciones" runat="server" Text="Observaciones y Oportunidades de Mejora:"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:TextBox ID="TXobservaciones" runat="server" Wrap="false" Width="300px" Height="75px" TextMode="MultiLine"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                        <td>
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="GEvaServicio" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="GEvaServicio" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
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
                        <asp:ImageButton ID="ImButtonPDFEvaExport" runat="server" ImageUrl="~/Imagenes/Icons/pdfImg.jpg" OnClick="ImButtonPDFEvaExport_Click" />
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