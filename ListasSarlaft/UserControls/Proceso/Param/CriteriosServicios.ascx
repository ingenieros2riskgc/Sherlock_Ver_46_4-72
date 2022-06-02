<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CriteriosServicios.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Param.GestionEvaluacionServicio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="GEPbody" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="HeadCS" runat="server">
                    <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Criterios Evaluación Servicios" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
            </div>
        <div id="BodyGridCS" class="ColumnStyle" runat="server">
            <Table class="tabla" align="center" width="100%">
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GVcriteriosServicio" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="strNombreUsuario,dtFechaRegistro,intIdUsuario"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" OnRowCommand="GVcriteriosServicio_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="intIdCriterioServicio" HeaderText="Código" SortExpression="intIdCriterioServicio" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible" />
                                        <asp:BoundField DataField="intRangoInicial" HeaderText="Rango Inicial" SortExpression="intRangoInicial" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intRangoFinal" HeaderText="Rango Final" SortExpression="intRangoFinal" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Descripcion Aprobación" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 150px">
                                                    <asp:Label ID="descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Wrap="false"  />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtFechaRegistro" HeaderText="fechaRegistro" ReadOnly="True" Visible="false" SortExpression="dtFechaRegistro" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="intIdUsuario" HeaderText="IdUsuario" ReadOnly="True" Visible="false" SortExpression="intIdUsuario" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario" ReadOnly="True" Visible="false" SortExpression="strNombreUsuario" ItemStyle-HorizontalAlign="Center" />
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
        <div id="BodyFormCS" class="ColumnStyle" runat="server" visible="false">
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
                                <asp:Label ID="LrangoInicial" runat="server" Text="Rango Inicial:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXrangoIni" runat="server" Width="300px"></asp:TextBox>
                                
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RFVrangoIni" runat="server" ControlToValidate="TXrangoIni"
                                    ErrorMessage="Debe ingresar el Rango Inicial." ToolTip="Debe ingresar el Rango Inicial."
                                    ValidationGroup="GCriterioServicio" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="LrangoFinal" runat="server" Text="Rango Final:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXrangoFin" runat="server" Font-Names="Calibri" Font-Size="Small" ValidationGroup="GEvaProveedor" Width="300px"></asp:TextBox>
                        
                        </td>
                        <td>
                        <asp:RequiredFieldValidator ID="RFVrangoFin" runat="server" ControlToValidate="TXrangoFin"
                                    ErrorMessage="Debe ingresar el Rango Final." ToolTip="Debe ingresar el Rango Final."
                                    ValidationGroup="GCriterioServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                            <td class="RowsText">
                                <asp:Label ID="Ldescripcion" runat="server" Text="Descripcion Criterio Aceptación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXdescripcion" runat="server" Width="300px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RFVdescripcion" runat="server" ControlToValidate="TXdescripcion"
                                    ErrorMessage="Debe ingresar la descripcion de aceptación." ToolTip="Debe ingresar la descripcion de aceptación."
                                    ValidationGroup="GCriterioServicio" ForeColor="Red">*</asp:RequiredFieldValidator>
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
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Insert" ValidationGroup="GCriterioServicio" ToolTip="Insertar" Visible="false" OnClick="IBinsertGVC_Click"/>
            <asp:ImageButton ID="IBupdateGVC" runat="server" CausesValidation="true" CommandName="Update"
                                    ImageUrl="~/Imagenes/Icons/guardar.png" Text="Update" ValidationGroup="GCriterioServicio" ToolTip="Actualizar" Visible="false" OnClick="IBupdateGVC_Click"/>
            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                        </td>
                    </tr>
                    </table>
            </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>