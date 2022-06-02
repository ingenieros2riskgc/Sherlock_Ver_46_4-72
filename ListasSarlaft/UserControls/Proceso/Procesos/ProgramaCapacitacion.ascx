<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramaCapacitacion.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.ProgramaCapacitacion" %>
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
        <div class="TituloLabel" id="HeadPC" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Programación de Capacitaciones" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridPC" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVprogramaCapacitacion" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro,IdCargoResponsable,IdMacroproceso"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnPageIndexChanging="GVprogramaCapacitacion_PageIndexChanging" OnRowCommand="GVprogramaCapacitacion_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:BoundField DataField="IdMacroproceso" HeaderText="IdMacroproceso" SortExpression="IdMacroproceso" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Proceso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="proceso" runat="server" Text='<% # Bind("strNombreProceso")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IdCargoResponsable" HeaderText="Cargo Responsable" SortExpression="IdCargoResponsable" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Cargo Responsable" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreHijo" runat="server" Text='<% # Bind("strCargoResponsable")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actividad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="actividad" runat="server" Text='<% # Bind("strActividad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dirigido A" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="dirigido" runat="server" Text='<% # Bind("strDirigidoA")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFechaProgramada" HeaderText="Fecha Programada" ReadOnly="True" SortExpression="dtFechaProgramada" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Fecha Realizada" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="dtFechaRealizada" runat="server" Text='<% # Bind("dtFechaRealizada")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Evaluación" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="intEvaluacion" runat="server" Text='<% # Bind("intEvaluacion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Asistentes" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="asistentes" runat="server" Text='<% # Bind("strAsistentes")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
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
        <div id="BodyFormPC" class="ColumnStyle" runat="server" visible="false">
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
                                                ValidationGroup="VGprograma" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox>
                        </td>
                                    </tr>
                                    <tr id="Macroproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label22" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlMacroproceso" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdMacroproceso"
                                                >
                                            </asp:DropDownList></td>
                                        <td><asp:RequiredFieldValidator ID="rfvMacroProceso" runat="server" ControlToValidate="ddlMacroproceso"
                                                ErrorMessage="Debe ingresar el Macroproceso." ToolTip="Debe ingresar el Macroproceso."
                                                ValidationGroup="VGprograma" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lresponsable" runat="server" Text="Cargo Responsable:" CssClass="Apariencia" Width="300px"></asp:Label>
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
                            </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVresponsable" runat="server" ControlToValidate="tbxResponsable"
                                    ErrorMessage="Debe ingresar el nombre del Responsable." ToolTip="Debe ingresar el nombre del Responsable."
                                    ValidationGroup="VGprograma" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lactividad" runat="server" Text="Actividad:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXactividad" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GVprograma" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVactividad" runat="server" ControlToValidate="TXactividad"
                                    ErrorMessage="Debe ingresar la actividad a realizar." ToolTip="Debe ingresar la actividad a realizar."
                                    ValidationGroup="VGprograma" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Ldirigido" runat="server" Text="Dirigido A:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXdirigido" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVdirigido" runat="server" ControlToValidate="TXdirigido"
                                    ErrorMessage="Debe ingresar el Nombre del colaborador al que va Dirigido." ToolTip="Debe ingresar el Nombre del colaborador al que va Dirigido."
                                    ValidationGroup="VGprograma" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lfechapro" runat="server" Text="Fecha de Programación:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechapro" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="VGprograma" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaEvapro" runat="server" Enabled="true" TargetControlID="TXfechapro"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfvFechaEva" runat="server" ControlToValidate="TXfechapro"
                                    ErrorMessage="Debe ingresar la fecha de la programación." ToolTip="Debe ingresar la fecha de la programación."
                                    ValidationGroup="VGprograma" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lassitentes" runat="server" Text="Asistentes:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXasistentes" runat="server" Font-Names="Calibri" TextMode="MultiLine" Wrap="false" Font-Size="Small" ValidationGroup="VGprograma" Width="300px" Height="75px"></asp:TextBox>
                        
                        </td>
                        <td>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Levaluacion" runat="server" Text="Evaluación:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXevaluacion" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="VGprograma" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lfecharealizada" runat="server" Text="Fecha Realizada:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfecharealizada" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="VGprograma" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaFinal" runat="server" Enabled="true" TargetControlID="TXfecharealizada"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
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
                        <td colspan="3">
                            <asp:ImageButton ID="IBinsertGVC" runat="server" CausesValidation="true" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="VGprograma" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="VGprograma" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                    </table>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>