<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrigenNoConformidad.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.Param.OrigenNoConformidad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<link href="../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="updPanel" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <div class="TituloLabel" id="divTitulo" runat="server">
            <asp:Label ID="Ltitulo" runat="server" ForeColor="White" Text="Origen no Conformidad" Font-Bold="False"
                Font-Names="Calibri" Font-Size="Large"></asp:Label>

        </div>
        <div style="height: 20px"></div>
        <div id="divForm" class="ColumnStyle" runat="server" visible="true">
            <div class="TableContains">
                <table class="tabla" align="center" width="80%">
                    <tr>
                        <td class="RowsText">
                            <asp:Label ID="lCadenaValorFiltro" runat="server" Text="Nombre:" CssClass="Apariencia" Width="300px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Width="300px" CssClass="Apariencia" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="Formulario"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="btnInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" ToolTip="Insertar" ValidationGroup="Formulario" OnClick="BtnInsertar_Click"/>
                            <asp:ImageButton ID="btnLimpiar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Limpiar" OnClick="BtnLimpiar_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <tr align="center" bgcolor="#EEEEEE" id="filaGrid" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                                    ForeColor="#333333" AllowSorting="True" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"
                                    HeaderStyle-CssClass="gridViewHeader" BorderStyle="Solid" GridLines="Vertical"
                                    CssClass="Apariencia" Font-Bold="False" AllowPaging="false">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdOrigenNoConformidad" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-CssClass="no-visible" HeaderStyle-CssClass="no-visible">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre"
                                            HtmlEncode="False" HtmlEncodeFormatString="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" SortExpression="FechaRegistro" />
                                        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario Creación" SortExpression="NombreUsuario" />
                                        <asp:BoundField DataField="FechaModificacion" HeaderText="Última modificación" SortExpression="FechaModificacion">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
                                            HeaderText="Modificar" CommandName="Modificar" ItemStyle-HorizontalAlign="Center" />
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
                    <br />
                </td>
            </tr>
    </ContentTemplate>
</asp:UpdatePanel>
