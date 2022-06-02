<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultarProcesos.ascx.cs" Inherits="ListasSarlaft.UserControls.Proceso.ConsultarProcesos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }

    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .style1
    {
        width: 100%;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table id="TablaConsulta" runat="server" align="center" width="80%">
            <tr id="Titulo" align="center" bgcolor="#333399"  runat="server">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Consultar Proceso" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="left" id="Cabecera" runat="server">
                <td bgcolor="#EEEEEE">
                    <table class="tabla">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="lblTitCV" runat="server" Text="Cadena Valor:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxCadenaValor" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Macroproceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxMacroproceso" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Proceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxProceso" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Sub-Proceso:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxSubProceso" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Descripción:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescripcion" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Objetivo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxObjetivo" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Cargo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tbxCargo" runat="server" Width="200px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="left" id="Detalles" runat="server">
                <td bgcolor="#EEEEEE">
                    <table class="tabla">
                        <tr>
                            <td>
                                <asp:TabContainer ID="TabDetalles" runat="server" ActiveTabIndex="0" Font-Names="Calibri"
                                    Font-Size="Small" Width="800px">
                                    <asp:TabPanel ID="tpEntrada" runat="server" HeaderText="Entradas" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Entradas
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                            CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                                            <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                                                    <ItemStyle Wrap="false" Width="300" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estado" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="strProveedor" HeaderText="Proveedor" SortExpression="strProveedor" />
                                                                <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="tpActividades" runat="server" HeaderText="Actividades" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Actividades
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                            CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId"
                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                                            <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                                                    <ItemStyle Wrap="false" Width="300" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estado" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="intCargoResponsable" HeaderText="IdCargo" SortExpression="intCargoResponsable"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="strNombreCargoResponsable" HeaderText="Cargo" SortExpression="strNombreCargoResponsable" />
                                                                <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="tpSalidas" runat="server" HeaderText="Salidas" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Salidas
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                            CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="intId" HeaderText="Código" ReadOnly="True" SortExpression="intId"
                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField HeaderText="Descripción">
                                                                    <ItemTemplate>
                                                                        <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 300px">
                                                                            <asp:Label ID="Descripcion" runat="server" Text='<% # Bind("strDescripcion")%>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Wrap="false" Width="300" HorizontalAlign="center" />
                                                                    <ItemStyle Wrap="false" Width="300" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estado" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chbActivo" runat="server" Checked='<% # Bind("booEstado")%>' Enabled="false" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="strCliente" HeaderText="Cliente" SortExpression="strCliente" />
                                                                <asp:BoundField DataField="intIdUsuario" HeaderText="Región" SortExpression="intIdUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="strNombreUsuario" HeaderText="Usuario Creación" SortExpression="strNombreUsuario"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="dtFechaRegistro" HeaderText="Fecha de Creación" SortExpression="dtFechaRegistro"
                                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="tpIndicadores" runat="server" HeaderText="Indicadores" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Indicadores
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                            CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                            <Columns>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="tpRiesgos" runat="server" HeaderText="Riesgos" Font-Names="Calibri" Font-Size="Small">
                                        <HeaderTemplate>
                                            Riesgos
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
                                                            CellPadding="4" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical"
                                                            HorizontalAlign="Center" ShowHeaderWhenEmpty="True">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                            <Columns>
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                            <HeaderStyle BackColor="#5D7B9D" CssClass="gridViewHeader" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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
