<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CrlPropiedadClienteProveedor.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Procesos.CrlPropiedadClienteProveedor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    
div.ajax__calendar_days table tr td{padding-right: 0px;}
div.ajax__calendar_body{width: 225px;}
div.ajax__calendar_container{width: 225px;}

    </style>
<asp:UpdatePanel ID="CPCPbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCPCP" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Control propiedad del cliente o proveedores externos" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
         <div id="BodyGridCPCP" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVpropiedadClienteProveedor" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="dtFechaRegistro,intIdUsuario,strUsuario"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVpropiedadClienteProveedor_RowCommand" Visible="false" OnPageIndexChanging="GVpropiedadClienteProveedor_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                                    <Columns>
                                        <asp:BoundField DataField="intIdCrlPropiedad" HeaderText="Código" SortExpression="intIdCrlPropiedad" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strDescripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Características" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strCaracteristicas" runat="server" Text='<% # Bind("strCaracteristicas")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Proveedor-Cliente" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strProveedorCliente" runat="server" Text='<% # Bind("strProveedorCliente")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="strNombre" runat="server" Text='<% # Bind("strNombre")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Ingreso" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="dtFechaIngreso" runat="server" Text='<% # Bind("dtFechaIngreso")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false"  HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Salida" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="dtFechaSalida" runat="server" Text='<% # Bind("dtFechaSalida")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observaciones" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                                    <asp:Label ID="strObservaciones" runat="server" Text='<% # Bind("strObservaciones")%>'></asp:Label>
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
        <div id="BodyFormCPCP" class="ColumnStyle" runat="server" visible="false">
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
                            <asp:Label ID="LDescripcion" runat="server" Text="Descripción:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXDescripcion" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlCP" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RFVDescripcion" runat="server" ControlToValidate="TXDescripcion"
                                    ErrorMessage="Debe ingresar la descripción a realizar." ToolTip="Debe ingresar la descripción a realizar."
                                    ValidationGroup="CrlCP" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                        <tr>
                            <td class="RowsText">
                                <asp:Label ID="Lcarac" runat="server" Text="Caracteristicas:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXcaracteristicas" runat="server" Font-Names="Calibri" TextMode="MultiLine" Wrap="false" Font-Size="Small" ValidationGroup="CrlCP" Width="300px" Height="75px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RVdcaracteristicas" runat="server" ControlToValidate="TXcaracteristicas"
                                    ErrorMessage="Debe ingresar las caracteristicas." ToolTip="Debe ingresar las caracteristicas."
                                    ValidationGroup="CrlCP" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                        <td class="RowsText">
                            <asp:Label ID="LclienteProveedor" runat="server" Text="Cliente o Proveedor:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlClienteProveedor" runat="server" Width="300px" ValidationGroup="CrlCP">
                                <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                <asp:ListItem Value="Cliente">Cliente</asp:ListItem>
                                <asp:ListItem Value="Proveedor">Proveedor</asp:ListItem>
                            </asp:DropDownList>
                        
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="rfvClienteProveedor" runat="server" ControlToValidate="ddlClienteProveedor"
                                    ErrorMessage="Debe seleccionar si es Cliente o Proveedor." ToolTip="Debe seleccionar si es Cliente o Proveedor."
                                    ValidationGroup="CrlCP" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lnombre" runat="server" Text="Nombre:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXnombre" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlCP" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="TXnombre"
                                    ErrorMessage="Debe ingresar el nombre del cliente o proveedor." ToolTip="Debe ingresar el nombre del cliente o proveedor."
                                    ValidationGroup="CrlCP" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaIngreso" runat="server" Text="Fecha Ingreso:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechaingreso" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlCP" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechaIngreso" runat="server" Enabled="true" TargetControlID="TXfechaingreso"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                        
                            <asp:RequiredFieldValidator ID="RFVfechaIngresa" runat="server" ControlToValidate="TXfechaingreso"
                                    ErrorMessage="Debe ingresar la fecha ingreso." ToolTip="Debe ingresar la fecha ingreso."
                                    ValidationGroup="CrlCP" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LfechaSalida" runat="server" Text="Fecha Salida:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXfechasalida" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlCP" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CEfechasalida" runat="server" Enabled="true" TargetControlID="TXfechasalida"
                            Format="yyyy-MM-dd" >
                        </asp:CalendarExtender>
                        
                       
                            <asp:RequiredFieldValidator ID="RFVfechaSalida" runat="server" ControlToValidate="TXfechasalida"
                                    ErrorMessage="Debe ingresar la fecha salida." ToolTip="Debe ingresar la fecha salida."
                                    ValidationGroup="CrlCP1" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="Lobservaciones" runat="server" Text="Observaciones:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXobservaciones" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="CrlCP" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                        
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
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="CrlCP" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="CrlCP" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                    </table>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>