<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlProductoNoConforme.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.ControlProductoNoConforme" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="GEPbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCPNC" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Control Producto No Conforme" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridCPNC" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVcontrolSalida" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="strUsuario,dtFechaRegistro,intCargoResponsable,strObservaciones,intIdMacroProceso"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVcontrolSalida_RowCommand" OnPageIndexChanging="GVcontrolSalida_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intId" HeaderText="Código" SortExpression="intId" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Nombre Proceso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreProceso" runat="server" Text='<% # Bind("strNombreProceso")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NombreEstado" runat="server" Text='<% # Bind("strNombreEstado")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Conformidad" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="NoConformidad" runat="server" Text='<% # Bind("strNoConformidad")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acciones Tomadas" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="AccionesTomadas" runat="server" Text='<% # Bind("strAccionesTomadas")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="intCargoResponsable" HeaderText="Cargo Responsable" SortExpression="intCargoResponsable" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
                                        <asp:TemplateField HeaderText="Personal Autoriza" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="PersonaAutoriza" runat="server" Text='<% # Bind("strPersonaAutoriza")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" ReadOnly="True" Visible="false" SortExpression="intIdUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strObservaciones" HeaderText="Observaciones" ReadOnly="True" Visible="false" SortExpression="strObservaciones" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Seleccionar" HeaderText="Seleccionar" CommandName="Seleccionar" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdMacroProceso" HeaderText="intIdMacroProceso" SortExpression="intIdMacroProceso" HtmlEncodeFormatString="True" HtmlEncode="False" Visible="False" />
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
        <div id="BodyFormCPNC" class="ColumnStyle" runat="server" visible="false">
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
                                                ValidationGroup="CrlProNoConforme" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tbxProcIndica" runat="server" Width="90px" MaxLength="20" CssClass="Apariencia" Visible="false"></asp:TextBox>
                                            <asp:Label ID="LtexProceso" runat="server" Visible="false"></asp:Label>
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
                                                ValidationGroup="CrlProNoConforme" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Proceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label7" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlProceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td><asp:RequiredFieldValidator ID="rfvProceso" runat="server" ControlToValidate="ddlProceso"
                                                ErrorMessage="Debe ingresar el Proceso." ToolTip="Debe ingresar el Proceso." Enabled="False"
                                                ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr id="Subproceso">
                                        <td class="RowsText">
                                            <asp:Label ID="Label8" runat="server" Text="Subproceso:" CssClass="Apariencia"></asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubproceso" runat="server" Width="300px" OnSelectedIndexChanged="ddlSubproceso_SelectedIndexChanged"
                                                CssClass="Apariencia" AutoPostBack="True" DataTextField="Nombre" DataValueField="IdProceso">
                                            </asp:DropDownList></td>
                                        <td><asp:RequiredFieldValidator ID="rfvSubproceso" runat="server" ControlToValidate="ddlSubproceso"
                                                ErrorMessage="Debe ingresar el Subproceso." ToolTip="Debe ingresar el Subproceso." Enabled="False"
                                                ValidationGroup="iIndicador" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator></td>
                                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lestado" runat="server" Text="Estado:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLestados" runat="server" Width="300px"
                                                CssClass="Apariencia" AutoPostBack="True"
                                                DataTextField="NombreEstado" DataValueField="Id"
                                    ValidationGroup="CrlSalida"
                                                >
                                                
                                            </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVestado" runat="server" ControlToValidate="DDLestados"
                                    ErrorMessage="Debe ingresar el Estado." ToolTip="Debe ingresar el Estado."
                                    ValidationGroup="CrlProNoConforme" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                <asp:Label ID="LtextEstado" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lnoconformidad" runat="server" Text="No Conformidad:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXnoconformidad" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="rfvnoconformidad" runat="server" ControlToValidate="TXnoconformidad"
                                    ErrorMessage="Debe ingresar la No Conformidad." ToolTip="Debe ingresar la No Conformidad."
                                    ValidationGroup="CrlProNoConforme" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lacciones" runat="server" Text="Acciones Tomadas:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXacciones" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                           
                        <asp:RequiredFieldValidator ID="RVaciones" runat="server" ControlToValidate="TXacciones"
                                    ErrorMessage="Debe ingresar las acciones tomadas." ToolTip="Debe ingresar las acciones tomadas."
                                    ValidationGroup="CrlProNoConforme" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lresponsable" runat="server" Text="Cargo Responsable:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxResponsable" runat="server" Width="300px" Enabled="false" CssClass="Apariencia"></asp:TextBox>
                                
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
                                    ValidationGroup="CrlProNoConforme" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lobservaciones" runat="server" Text="Observaciones:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXobservaciones" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlProducto" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ControlToValidate="TXobservaciones"
                                    ErrorMessage="Debe ingresar las observaciones." ToolTip="Debe ingresar las observaciones."
                                    ValidationGroup="CrlProNoConforme" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="CrlProNoConforme" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="CrlProNoConforme" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                    </table>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>